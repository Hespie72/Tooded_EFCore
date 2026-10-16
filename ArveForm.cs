using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace Tooded_EFCore
{
    public partial class ArveForm : Form
    {
        private readonly ToodedContext _db;
        private string viimanePdf;

        // Toode, mis valiti Form2-s (võib olla null)
        public Toode SelectedToode { get; set; }

        // 6.3.2 — ostukorv ei ole enam List<string>
        List<OstukorviRida> ostukorv = new List<OstukorviRida>();

        public ArveForm()
        {
            InitializeComponent();
            _db = new ToodedContext();
            Load += ArveForm_Load;
        }

        // Laeme KÕIK tooted andmebaasist ComboBoxi
        private void ArveForm_Load(object sender, EventArgs e)
        {
            var tooted = _db.Toodetabel.AsNoTracking().ToList();

            comboToode.DisplayMember = "Toodenimetus";
            comboToode.ValueMember = "Id";
            comboToode.DataSource = tooted;

            // Kui Form2-s oli toode valitud, valime selle kohe
            if (SelectedToode != null)
                comboToode.SelectedValue = SelectedToode.Id;

            ArvutaKoguSumma();
        }

        // 6.3.3 — Lisa korvi, küsime kogust
        private void btnLisaKorvi_Click(object sender, EventArgs e)
        {
            SelectedToode = comboToode.SelectedItem as Toode;

            if (SelectedToode == null)
            {
                MessageBox.Show("Vali toode enne ostukorvi lisamist.");
                return;
            }

            string kogusStr = Microsoft.VisualBasic.Interaction.InputBox(
                "Sisesta kogus", "Kogus", "1");

            // Cancel / tühi sisend — vaikselt katkestame
            if (string.IsNullOrWhiteSpace(kogusStr))
                return;

            if (!int.TryParse(kogusStr, out int kogus) || kogus <= 0)
            {
                MessageBox.Show("Vale kogus.");
                return;
            }

            // Kontrollime laoseisu (arvestades juba korvis olevat kogust)
            int korvis = ostukorv
                .Where(r => r.Toode.Id == SelectedToode.Id)
                .Sum(r => r.Kogus);

            if (korvis + kogus > SelectedToode.Kogus)
            {
                MessageBox.Show($"Laos on ainult {SelectedToode.Kogus} tk!");
                return;
            }

            var rida = new OstukorviRida
            {
                Toode = SelectedToode,
                Kogus = kogus
            };

            ostukorv.Add(rida);
            UuendaKorv();
        }

        private void btnEemalda_Click(object sender, EventArgs e)
        {
            if (listOstukorv.SelectedIndex < 0)
                return;

            ostukorv.RemoveAt(listOstukorv.SelectedIndex);
            UuendaKorv();
        }

        private void UuendaKorv()
        {
            listOstukorv.Items.Clear();

            foreach (var rida in ostukorv)
                listOstukorv.Items.Add(rida);

            ArvutaKoguSumma();
        }

        // 6.3.4 — kogusumma
        private void ArvutaKoguSumma()
        {
            float kokku = ostukorv.Sum(r => r.Summa);

            lblKokku.Text = $"Kokku: {kokku:0.00} €";
        }

        // 6.3.5 — PDF koos kogusummaga
        private void btnGenPDF_Click(object sender, EventArgs e)
        {
            if (ostukorv.Count == 0)
            {
                MessageBox.Show("Ostukorv on tühi!");
                return;
            }

            Directory.CreateDirectory("Arved");

            viimanePdf = Path.Combine("Arved", $"Arve_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont font = new XFont("Verdana", 12);

            gfx.DrawString("Arve", new XFont("Verdana", 20), XBrushes.Black,
                new XRect(0, 20, page.Width, 40), XStringFormats.TopCenter);

            int y = 80;
            foreach (var rida in ostukorv)
            {
                gfx.DrawString(rida.ToString(), font, XBrushes.Black,
                    new XRect(20, y, page.Width - 40, 20), XStringFormats.TopLeft);
                y += 30;
            }

            gfx.DrawString($"Kokku: {ostukorv.Sum(r => r.Summa):0.00} €",
                new XFont("Verdana", 14), XBrushes.Black,
                new XRect(20, y + 20, page.Width, 20), XStringFormats.TopLeft);

            document.Save(viimanePdf);

            MessageBox.Show($"PDF on loodud:\n{viimanePdf}");
        }

        private void btnSaadaEmail_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(viimanePdf) || !File.Exists(viimanePdf))
            {
                MessageBox.Show("Loo kõigepealt PDF!");
                return;
            }

            string email = Microsoft.VisualBasic.Interaction.InputBox(
                "Sisesta e-mail", "Saaja", "nimi@example.com");

            if (string.IsNullOrWhiteSpace(email)) return;

            try
            {
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress("sinu_gmail@gmail.com");
                    mail.To.Add(email);
                    mail.Subject = "Arve";
                    mail.Body = "Arve on manuses.";
                    mail.Attachments.Add(new Attachment(viimanePdf));

                    using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        // Gmaili jaoks on vaja "rakenduse parooli", mitte tavalist parooli
                        smtp.Credentials = new NetworkCredential("sinu_gmail@gmail.com", "parool");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }

                MessageBox.Show("Arve saadetud!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Viga: " + ex.Message);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _db.Dispose();
            base.OnFormClosed(e);
        }
    }
}