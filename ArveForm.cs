using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Tooded_EFCore;

public partial class ArveForm : Form
{
    // Korv säilib sama sisselogimise ajal vormide vahel.
    private static readonly List<OstukorviRida>
        ostukorv = new();

    private static long korviVersioon;

    private string? viimanePdf;
    private long pdfVersioon = -1;
    private bool saatmine;

    public Toode? SelectedToode { get; set; }

    public ArveForm()
    {
        InitializeComponent();

        listOstukorv.HorizontalScrollbar = true;

        Load += ArveForm_Load;

        FormClosing += (s, e) =>
        {
            if (!saatmine)
                return;

            e.Cancel = true;

            MessageBox.Show(
                "Oota e-kirja saatmise lõppu.");
        };
    }

    public static void TuhjendaKorv()
    {
        ostukorv.Clear();

        korviVersioon++;
    }

    private static void Teosta(Action tegevus)
    {
        try
        {
            tegevus();
        }
        catch (Exception viga)
        {
            MessageBox.Show(
                "Toiming ebaõnnestus:\n" +
                viga.Message,
                "Viga");
        }
    }

    private void ArveForm_Load(
        object? sender,
        EventArgs e)
    {
        Teosta(() =>
        {
            using var db = new ToodedContext();

            comboToode.DisplayMember =
                nameof(Toode.Toodenimetus);

            comboToode.ValueMember =
                nameof(Toode.Id);

            comboToode.DataSource = db.Toodetabel
                .AsNoTracking()
                .OrderBy(t => t.Toodenimetus)
                .ToList();

            if (SelectedToode != null)
            {
                comboToode.SelectedValue =
                    SelectedToode.Id;
            }

            UuendaKorv();
        });
    }

    private void btnLisaKorvi_Click(
        object sender,
        EventArgs e)
    {
        if (comboToode.SelectedValue is not int id)
        {
            MessageBox.Show(
                "Vali toode!");

            return;
        }

        string sisend =
            Microsoft.VisualBasic.Interaction.InputBox(
                "Sisesta kogus",
                "Kogus",
                "1");

        if (string.IsNullOrWhiteSpace(sisend))
            return;

        if (!int.TryParse(sisend, out int kogus) ||
            kogus <= 0)
        {
            MessageBox.Show(
                "Sisesta positiivne täisarv!");

            return;
        }

        Teosta(() =>
        {
            // Loeb enne lisamist värske hinna ja laoseisu.
            using var db = new ToodedContext();

            var toode = db.Toodetabel
                .AsNoTracking()
                .FirstOrDefault(t => t.Id == id)
                ?? throw new InvalidOperationException(
                    "Toodet ei leitud!");

            if (!float.IsFinite(toode.Hind) ||
                toode.Hind < 0 ||
                toode.Hind > 100000000)
            {
                throw new InvalidOperationException(
                    "Toote hind on vigane.");
            }

            long korvis = ostukorv
                .Where(r => r.Toode.Id == id)
                .Sum(r => (long)r.Kogus);

            if (korvis + kogus > toode.Kogus)
            {
                throw new InvalidOperationException(
                    $"Laos on {toode.Kogus} tk. " +
                    $"Korvis on juba {korvis} tk.");
            }

            ostukorv.Add(new OstukorviRida
            {
                Toode = toode,
                Kogus = kogus
            });

            korviVersioon++;

            UuendaKorv();
        });
    }

    private void btnEemalda_Click(
        object sender,
        EventArgs e)
    {
        if (listOstukorv.SelectedIndex < 0)
            return;

        ostukorv.RemoveAt(
            listOstukorv.SelectedIndex);

        korviVersioon++;

        UuendaKorv();
    }

    private void UuendaKorv()
    {
        listOstukorv.Items.Clear();

        foreach (var rida in ostukorv)
            listOstukorv.Items.Add(rida);

        lblKokku.Text =
            $"Kokku: {ostukorv.Sum(r => r.Summa):0.00} €";

        // Muudetud ostukorvi jaoks tuleb luua uus PDF.
        if (pdfVersioon != korviVersioon)
            viimanePdf = null;
    }

    private void btnGenPDF_Click(
        object sender,
        EventArgs e)
    {
        Teosta(() =>
        {
            if (ostukorv.Count == 0)
            {
                throw new InvalidOperationException(
                    "Ostukorv on tühi!");
            }

            // Salvestab PDF-id rakenduse käivituskausta alamkausta Arved.
            string kaust = Path.Combine(
                AppContext.BaseDirectory,
                "Arved");

            Directory.CreateDirectory(kaust);

            string fail = Path.Combine(
                kaust,
                $"Arve_{DateTime.Now:yyyyMMdd_HHmmss}_" +
                $"{Guid.NewGuid():N}.pdf");

            LooPdf(fail);

            viimanePdf = fail;
            pdfVersioon = korviVersioon;

            MessageBox.Show(
                $"PDF on loodud:\n{fail}",
                "Arve");
        });
    }

    private static IEnumerable<string> JagaRead(
        string tekst,
        XGraphics joonis,
        XFont font,
        double laius)
    {
        var rida = new StringBuilder();

        foreach (char taht in tekst
                     .Replace('\r', ' ')
                     .Replace('\n', ' '))
        {
            if (rida.Length > 0 &&
                joonis.MeasureString(
                    rida.ToString() + taht,
                    font).Width > laius)
            {
                yield return rida.ToString();

                rida.Clear();
            }

            rida.Append(taht);
        }

        if (rida.Length > 0)
            yield return rida.ToString();
    }

    private static void LooPdf(string fail)
    {
        using var dokument = new PdfDocument();

        dokument.Info.Title = "Arve";

        var font = new XFont(
            "Verdana",
            11);

        XGraphics? joonis = null;

        double y = 0;
        double laius = 0;
        double korgus = 0;

        void UusLeht()
        {
            joonis?.Dispose();

            var leht = dokument.AddPage();

            laius = leht.Width.Point;
            korgus = leht.Height.Point;

            joonis = XGraphics.FromPdfPage(leht);

            joonis.DrawString(
                "Arve",
                new XFont("Verdana", 20),
                XBrushes.Black,
                new XRect(
                    35,
                    25,
                    laius - 70,
                    35),
                XStringFormats.TopLeft);

            joonis.DrawString(
                $"Kuupäev: {DateTime.Now:dd.MM.yyyy HH:mm}",
                font,
                XBrushes.Black,
                new XRect(
                    35,
                    65,
                    laius - 70,
                    20),
                XStringFormats.TopLeft);

            y = 105;
        }

        try
        {
            UusLeht();

            foreach (var rida in ostukorv)
            {
                foreach (string tekst in JagaRead(
                             rida.ToString(),
                             joonis!,
                             font,
                             laius - 70))
                {
                    if (y + 20 > korgus - 50)
                        UusLeht();

                    joonis!.DrawString(
                        tekst,
                        font,
                        XBrushes.Black,
                        new XRect(
                            35,
                            y,
                            laius - 70,
                            20),
                        XStringFormats.TopLeft);

                    y += 19;
                }

                y += 8;
            }

            if (y + 50 > korgus - 50)
                UusLeht();

            joonis!.DrawString(
                $"Kokku: {ostukorv.Sum(r => r.Summa):0.00} €",
                new XFont("Verdana", 14),
                XBrushes.Black,
                new XRect(
                    35,
                    y + 15,
                    laius - 70,
                    25),
                XStringFormats.TopLeft);
        }
        finally
        {
            joonis?.Dispose();
        }

        dokument.Save(fail);
    }

    private async void btnSaadaEmail_Click(
        object sender,
        EventArgs e)
    {
        if (saatmine)
            return;

        if (viimanePdf == null ||
            !File.Exists(viimanePdf) ||
            pdfVersioon != korviVersioon)
        {
            MessageBox.Show(
                "Genereeri esmalt praeguse ostukorvi PDF!");

            return;
        }

        using var aken = new Form
        {
            Text = "E-posti seaded",
            ClientSize = new Size(390, 440),
            StartPosition =
                FormStartPosition.CenterParent,
            FormBorderStyle =
                FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false
        };

        TextBox LisaVali(
            string tekst,
            int y,
            string vaartus = "",
            bool peida = false)
        {
            aken.Controls.Add(new Label
            {
                Text = tekst,
                AutoSize = true,
                Location = new Point(20, y)
            });

            var vali = new TextBox
            {
                Location = new Point(20, y + 22),
                Width = 345,
                Text = vaartus,
                UseSystemPasswordChar = peida
            };

            aken.Controls.Add(vali);

            return vali;
        }

        var saaja = LisaVali(
            "Saaja e-post",
            15);

        var saatja = LisaVali(
            "Saatja e-post ja SMTP kasutajanimi",
            80);

        var server = LisaVali(
            "SMTP-server (STARTTLS)",
            145,
            "smtp.gmail.com");

        var port = LisaVali(
            "Port",
            210,
            "587");

        var parool = LisaVali(
            "Rakenduse parool",
            275,
            peida: true);

        var kinnita = new Button
        {
            Text = "Saada",
            Location = new Point(20, 350),
            Size = new Size(130, 35),
            DialogResult = DialogResult.OK
        };

        aken.Controls.Add(kinnita);

        aken.AcceptButton = kinnita;

        if (aken.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            var saajaAadress = new MailAddress(
                saaja.Text.Trim());

            var saatjaAadress = new MailAddress(
                saatja.Text.Trim());

            if (!int.TryParse(
                    port.Text,
                    out int pordiNumber)
                || pordiNumber < 1
                || pordiNumber > 65535)
            {
                throw new InvalidOperationException(
                    "Sisesta korrektne port!");
            }

            if (string.IsNullOrWhiteSpace(server.Text) ||
                string.IsNullOrWhiteSpace(parool.Text))
            {
                throw new InvalidOperationException(
                    "Sisesta SMTP-server ja rakenduse parool!");
            }

            saatmine = true;

            btnSaadaEmail.Enabled = false;
            btnGenPDF.Enabled = false;
            btnLisaKorvi.Enabled = false;
            btnEemalda.Enabled = false;

            using var kiri = new MailMessage
            {
                From = saatjaAadress,
                Subject = "Arve",
                Body =
                    "Tere!\n\nArve on lisatud manusena."
            };

            kiri.To.Add(saajaAadress);

            kiri.Attachments.Add(
                new Attachment(viimanePdf));

            using var smtp = new SmtpClient(
                server.Text.Trim(),
                pordiNumber)
            {
                UseDefaultCredentials = false,

                Credentials = new NetworkCredential(
                    saatjaAadress.Address,
                    parool.Text),

                EnableSsl = true
            };

            using var katkestus =
                new CancellationTokenSource(
                    TimeSpan.FromSeconds(60));

            await smtp.SendMailAsync(
                kiri,
                katkestus.Token);

            MessageBox.Show(
                "Arve on saadetud!",
                "E-post");
        }
        catch (Exception viga)
        {
            MessageBox.Show(
                "Saatmine ebaõnnestus:\n" +
                viga.Message,
                "Viga");
        }
        finally
        {
            parool.Clear();

            saatmine = false;

            btnSaadaEmail.Enabled = true;
            btnGenPDF.Enabled = true;
            btnLisaKorvi.Enabled = true;
            btnEemalda.Enabled = true;
        }
    }
}