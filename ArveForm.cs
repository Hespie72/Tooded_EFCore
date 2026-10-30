using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Tooded_EFCore;

public partial class ArveForm : Form
{
    private const string SaatjaEmail =
        "vitalya.abdulov.1979@gmail.com";

    private const string GmailiRakenduseParool =
        "iccp iixp npok vsnp";

    private readonly Button btnPluss = new()
    {
        Text = "+",
        Size = new Size(48, 38)
    };

    private readonly Button btnMiinus = new()
    {
        Text = "−",
        Size = new Size(48, 38)
    };

    private readonly Button btnAvaPdf = new()
    {
        Text = "Ava PDF",
        Size = new Size(140, 38)
    };

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

        Controls.Clear();

        Text = "Ostukorv ja arve";
        ClientSize = new Size(950, 650);
        MinimumSize = new Size(900, 580);

        FormBorderStyle = FormBorderStyle.Sizable;
        MaximizeBox = true;

        var paigutus = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 5,
            Padding = new Padding(20)
        };

        paigutus.ColumnStyles.Add(
            new ColumnStyle(SizeType.Percent, 100));

        paigutus.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 60));

        paigutus.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 75));

        paigutus.RowStyles.Add(
            new RowStyle(SizeType.Percent, 100));

        paigutus.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 50));

        paigutus.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 65));

        paigutus.Controls.Add(
            Kujundus.Pealkiri("Ostukorv ja arve"),
            0,
            0);

        var valik = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill
        };

        lblToode.Text = "Vali toode:";
        lblToode.Margin = new Padding(3, 10, 10, 3);

        comboToode.Width = 440;

        btnLisaKorvi.Size = new Size(150, 38);

        valik.Controls.AddRange(new Control[]
        {
            lblToode,
            comboToode,
            btnLisaKorvi
        });

        paigutus.Controls.Add(valik, 0, 1);

        listOstukorv.Dock = DockStyle.Fill;
        listOstukorv.HorizontalScrollbar = true;
        listOstukorv.IntegralHeight = false;

        paigutus.Controls.Add(listOstukorv, 0, 2);

        lblKokku.Margin = new Padding(3, 14, 3, 3);

        paigutus.Controls.Add(lblKokku, 0, 3);

        var nupud = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill
        };

        btnEemalda.Size = new Size(145, 38);
        btnGenPDF.Size = new Size(150, 38);
        btnSaadaEmail.Size = new Size(155, 38);

        btnEemalda.Text = "Eemalda rida";
        btnGenPDF.Text = "Loo PDF";
        btnSaadaEmail.Text = "Saada e-postiga";

        nupud.Controls.AddRange(new Control[]
        {
            btnMiinus,
            btnPluss,
            btnEemalda,
            btnGenPDF,
            btnAvaPdf,
            btnSaadaEmail
        });

        paigutus.Controls.Add(nupud, 0, 4);

        Controls.Add(paigutus);

        btnPluss.Click +=
            (s, e) => MuudaKogust(1);

        btnMiinus.Click +=
            (s, e) => MuudaKogust(-1);

        btnAvaPdf.Click += (s, e) => Teosta(() =>
        {
            if (!PdfKehtib())
            {
                throw new InvalidOperationException(
                    "Loo esmalt praeguse ostukorvi PDF!");
            }

            Process.Start(new ProcessStartInfo(
                viimanePdf!)
            {
                UseShellExecute = true
            });
        });

        Kujundus.Rakenda(this);
        Kujundus.Rohuta(btnLisaKorvi);
        Kujundus.Rohuta(btnGenPDF);

        btnEemalda.ForeColor = Color.Salmon;
        btnAvaPdf.Enabled = false;

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

    private bool PdfKehtib()
    {
        return viimanePdf != null
            && File.Exists(viimanePdf)
            && pdfVersioon == korviVersioon;
    }

    private void MuudaKogust(int muutus)
    {
        int indeks = listOstukorv.SelectedIndex;

        if (indeks < 0)
        {
            MessageBox.Show(
                "Vali ostukorvist rida!");

            return;
        }

        Teosta(() =>
        {
            var rida = ostukorv[indeks];

            long uusKogus =
                (long)rida.Kogus + muutus;

            if (muutus > 0)
            {
                using var db = new ToodedContext();

                var toode = db.Toodetabel
                    .AsNoTracking()
                    .FirstOrDefault(
                        t => t.Id == rida.Toode.Id)
                    ?? throw new InvalidOperationException(
                        "Toodet enam ei ole.");

                long kokku = ostukorv
                    .Where(r => r.Toode.Id == toode.Id)
                    .Sum(r => (long)r.Kogus);

                if (kokku + muutus > toode.Kogus ||
                    uusKogus > int.MaxValue)
                {
                    throw new InvalidOperationException(
                        $"Laos on ainult {toode.Kogus} tk.");
                }
            }

            if (uusKogus <= 0)
            {
                ostukorv.RemoveAt(indeks);
            }
            else
            {
                rida.Kogus = (int)uusKogus;
            }

            korviVersioon++;

            UuendaKorv();

            if (ostukorv.Count > 0)
            {
                listOstukorv.SelectedIndex =
                    Math.Min(
                        indeks,
                        ostukorv.Count - 1);
            }
        });
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
            MessageBox.Show("Vali toode!");
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
        {
            listOstukorv.Items.Add(rida);
        }

        lblKokku.Text =
            $"Kokku: {ostukorv.Sum(r => r.Summa):0.00} €";

        if (pdfVersioon != korviVersioon)
        {
            viimanePdf = null;
        }

        btnAvaPdf.Enabled = PdfKehtib();
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

#if DEBUG
            string kaust = Path.Combine(
                AppContext.BaseDirectory,
                "Arved");
#else
            string kaust = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments),
                "Tooded_EFCore",
                "Arved");
#endif

            Directory.CreateDirectory(kaust);

            string fail = Path.Combine(
                kaust,
                $"Arve_{DateTime.Now:yyyyMMdd_HHmmss}_" +
                $"{Guid.NewGuid():N}.pdf");

            LooPdf(fail);

            viimanePdf = fail;
            pdfVersioon = korviVersioon;

            btnAvaPdf.Enabled = true;

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
        {
            yield return rida.ToString();
        }
    }

    private static void LooPdf(string fail)
    {
        using var dokument = new PdfDocument();

        dokument.Info.Title = "Arve";

        var font = new XFont("Verdana", 11);

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
                new XRect(35, 25, laius - 70, 35),
                XStringFormats.TopLeft);

            joonis.DrawString(
                $"Kuupäev: {DateTime.Now:dd.MM.yyyy HH:mm}",
                font,
                XBrushes.Black,
                new XRect(35, 65, laius - 70, 20),
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
                    {
                        UusLeht();
                    }

                    joonis!.DrawString(
                        tekst,
                        font,
                        XBrushes.Black,
                        new XRect(35, y, laius - 70, 20),
                        XStringFormats.TopLeft);

                    y += 19;
                }

                y += 8;
            }

            if (y + 50 > korgus - 50)
            {
                UusLeht();
            }

            joonis!.DrawString(
                $"Kokku: {ostukorv.Sum(r => r.Summa):0.00} €",
                new XFont("Verdana", 14),
                XBrushes.Black,
                new XRect(35, y + 15, laius - 70, 25),
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

        if (!PdfKehtib())
        {
            MessageBox.Show(
                "Loo esmalt praeguse ostukorvi PDF!");

            return;
        }

        string parool =
            GmailiRakenduseParool.Replace(" ", "");

        if (string.IsNullOrWhiteSpace(parool) ||
            string.Equals(
                parool,
                "SIIA_RAKENDUSE_PAROOL",
                StringComparison.Ordinal))
        {
            MessageBox.Show(
                "E-posti saatmine pole seadistatud.");

            return;
        }

        string saaja =
            Microsoft.VisualBasic.Interaction.InputBox(
                "Sisesta saaja e-posti aadress",
                "Arve saatmine",
                "");

        if (string.IsNullOrWhiteSpace(saaja))
            return;

        try
        {
            var aadress = new MailAddress(
                saaja.Trim());

            saatmine = true;

            foreach (var nupp in new[]
                     {
                         btnLisaKorvi,
                         btnEemalda,
                         btnPluss,
                         btnMiinus,
                         btnGenPDF,
                         btnAvaPdf,
                         btnSaadaEmail
                     })
            {
                nupp.Enabled = false;
            }

            using var kiri = new MailMessage
            {
                From = new MailAddress(SaatjaEmail),
                Subject = "Arve",
                Body = "Tere!\n\nArve on lisatud manusena."
            };

            kiri.To.Add(aadress);

            kiri.Attachments.Add(
                new Attachment(viimanePdf!));

            using var smtp = new SmtpClient(
                "smtp.gmail.com",
                587)
            {
                UseDefaultCredentials = false,

                Credentials = new NetworkCredential(
                    SaatjaEmail,
                    parool),

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
            saatmine = false;

            foreach (var nupp in new[]
                     {
                         btnLisaKorvi,
                         btnEemalda,
                         btnPluss,
                         btnMiinus,
                         btnGenPDF,
                         btnSaadaEmail
                     })
            {
                nupp.Enabled = true;
            }

            btnAvaPdf.Enabled = PdfKehtib();
        }
    }
}