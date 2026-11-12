#nullable disable

using System.Drawing;
using System.Windows.Forms;

namespace Tooded_EFCore
{
    partial class ArveForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            paigutus = new TableLayoutPanel();
            lblPealkiri = new Label();
            valik = new FlowLayoutPanel();
            lblToode = new Label();
            btnLisaKorvi = new Button();
            listOstukorv = new ListBox();
            lblKokku = new Label();
            nupud = new FlowLayoutPanel();
            btnMiinus = new Button();
            btnPluss = new Button();
            btnEemalda = new Button();
            btnGenPDF = new Button();
            btnAvaPdf = new Button();
            btnSaadaEmail = new Button();

            paigutus.SuspendLayout();
            valik.SuspendLayout();
            nupud.SuspendLayout();
            SuspendLayout();

            // paigutus
            paigutus.Name = "paigutus";
            paigutus.Dock = DockStyle.Fill;
            paigutus.Padding = new Padding(20);
            paigutus.ColumnCount = 1;
            paigutus.RowCount = 5;
            paigutus.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100F));
            paigutus.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 60F));
            paigutus.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 75F));
            paigutus.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));
            paigutus.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 50F));
            paigutus.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65F));

            // lblPealkiri
            lblPealkiri.Name = "lblPealkiri";
            lblPealkiri.Text = "Ostukorv ja arve";
            lblPealkiri.AutoSize = true;
            lblPealkiri.BackColor = Color.Transparent;
            lblPealkiri.Font = new Font(
                "Segoe UI", 19F, FontStyle.Bold);
            lblPealkiri.Margin = new Padding(3, 3, 3, 16);

            // valik
            valik.Name = "valik";
            valik.Dock = DockStyle.Fill;

            // lblToode
            lblToode.Name = "lblToode";
            lblToode.Text =
                "Lisa tooteid poes toote kõrval oleva + nupuga.";
            lblToode.AutoSize = true;
            lblToode.BackColor = Color.Transparent;
            lblToode.Margin = new Padding(3, 10, 10, 3);

            // btnLisaKorvi
            btnLisaKorvi.Name = "btnLisaKorvi";
            btnLisaKorvi.Text = "Ava pood";
            btnLisaKorvi.Size = new Size(150, 38);
            btnLisaKorvi.BackColor = Color.FromArgb(77, 130, 245);
            btnLisaKorvi.ForeColor = Color.FromArgb(235, 239, 247);
            btnLisaKorvi.FlatStyle = FlatStyle.Flat;
            btnLisaKorvi.FlatAppearance.BorderColor =
                Color.FromArgb(52, 63, 82);
            btnLisaKorvi.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(48, 65, 94);
            btnLisaKorvi.UseVisualStyleBackColor = false;
            btnLisaKorvi.Cursor = Cursors.Hand;
            btnLisaKorvi.TabIndex = 0;
            btnLisaKorvi.Click += btnLisaKorvi_Click;

            // listOstukorv
            listOstukorv.Name = "listOstukorv";
            listOstukorv.Dock = DockStyle.Fill;
            listOstukorv.HorizontalScrollbar = true;
            listOstukorv.IntegralHeight = false;
            listOstukorv.BackColor = Color.FromArgb(29, 35, 47);
            listOstukorv.ForeColor = Color.FromArgb(235, 239, 247);
            listOstukorv.BorderStyle = BorderStyle.FixedSingle;
            listOstukorv.TabIndex = 1;

            // lblKokku
            lblKokku.Name = "lblKokku";
            lblKokku.Text = "Kokku: 0 €";
            lblKokku.AutoSize = true;
            lblKokku.BackColor = Color.Transparent;
            lblKokku.Font = new Font(
                "Segoe UI", 12F, FontStyle.Bold);
            lblKokku.Margin = new Padding(3, 14, 3, 3);

            // nupud
            nupud.Name = "nupud";
            nupud.Dock = DockStyle.Fill;

            // btnMiinus
            btnMiinus.Name = "btnMiinus";
            btnMiinus.Text = "−";
            btnMiinus.Size = new Size(48, 38);
            btnMiinus.BackColor = Color.FromArgb(29, 35, 47);
            btnMiinus.ForeColor = Color.FromArgb(235, 239, 247);
            btnMiinus.FlatStyle = FlatStyle.Flat;
            btnMiinus.FlatAppearance.BorderColor =
                Color.FromArgb(52, 63, 82);
            btnMiinus.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(48, 65, 94);
            btnMiinus.UseVisualStyleBackColor = false;
            btnMiinus.Cursor = Cursors.Hand;
            btnMiinus.TabIndex = 2;
            btnMiinus.Click += btnMiinus_Click;

            // btnPluss
            btnPluss.Name = "btnPluss";
            btnPluss.Text = "+";
            btnPluss.Size = new Size(48, 38);
            btnPluss.BackColor = Color.FromArgb(29, 35, 47);
            btnPluss.ForeColor = Color.FromArgb(235, 239, 247);
            btnPluss.FlatStyle = FlatStyle.Flat;
            btnPluss.FlatAppearance.BorderColor =
                Color.FromArgb(52, 63, 82);
            btnPluss.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(48, 65, 94);
            btnPluss.UseVisualStyleBackColor = false;
            btnPluss.Cursor = Cursors.Hand;
            btnPluss.TabIndex = 3;
            btnPluss.Click += btnPluss_Click;

            // btnEemalda
            btnEemalda.Name = "btnEemalda";
            btnEemalda.Text = "Eemalda rida";
            btnEemalda.Size = new Size(145, 38);
            btnEemalda.BackColor = Color.FromArgb(29, 35, 47);
            btnEemalda.ForeColor = Color.Salmon;
            btnEemalda.FlatStyle = FlatStyle.Flat;
            btnEemalda.FlatAppearance.BorderColor =
                Color.FromArgb(52, 63, 82);
            btnEemalda.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(48, 65, 94);
            btnEemalda.UseVisualStyleBackColor = false;
            btnEemalda.Cursor = Cursors.Hand;
            btnEemalda.TabIndex = 4;
            btnEemalda.Click += btnEemalda_Click;

            // btnGenPDF
            btnGenPDF.Name = "btnGenPDF";
            btnGenPDF.Text = "Loo PDF";
            btnGenPDF.Size = new Size(150, 38);
            btnGenPDF.BackColor = Color.FromArgb(77, 130, 245);
            btnGenPDF.ForeColor = Color.FromArgb(235, 239, 247);
            btnGenPDF.FlatStyle = FlatStyle.Flat;
            btnGenPDF.FlatAppearance.BorderColor =
                Color.FromArgb(52, 63, 82);
            btnGenPDF.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(48, 65, 94);
            btnGenPDF.UseVisualStyleBackColor = false;
            btnGenPDF.Cursor = Cursors.Hand;
            btnGenPDF.TabIndex = 5;
            btnGenPDF.Click += btnGenPDF_Click;

            // btnAvaPdf
            btnAvaPdf.Name = "btnAvaPdf";
            btnAvaPdf.Text = "Ava PDF";
            btnAvaPdf.Size = new Size(140, 38);
            btnAvaPdf.Enabled = false;
            btnAvaPdf.BackColor = Color.FromArgb(29, 35, 47);
            btnAvaPdf.ForeColor = Color.FromArgb(235, 239, 247);
            btnAvaPdf.FlatStyle = FlatStyle.Flat;
            btnAvaPdf.FlatAppearance.BorderColor =
                Color.FromArgb(52, 63, 82);
            btnAvaPdf.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(48, 65, 94);
            btnAvaPdf.UseVisualStyleBackColor = false;
            btnAvaPdf.Cursor = Cursors.Hand;
            btnAvaPdf.TabIndex = 6;
            btnAvaPdf.Click += btnAvaPdf_Click;

            // btnSaadaEmail
            btnSaadaEmail.Name = "btnSaadaEmail";
            btnSaadaEmail.Text = "Saada e-postiga";
            btnSaadaEmail.Size = new Size(155, 38);
            btnSaadaEmail.BackColor = Color.FromArgb(29, 35, 47);
            btnSaadaEmail.ForeColor = Color.FromArgb(235, 239, 247);
            btnSaadaEmail.FlatStyle = FlatStyle.Flat;
            btnSaadaEmail.FlatAppearance.BorderColor =
                Color.FromArgb(52, 63, 82);
            btnSaadaEmail.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(48, 65, 94);
            btnSaadaEmail.UseVisualStyleBackColor = false;
            btnSaadaEmail.Cursor = Cursors.Hand;
            btnSaadaEmail.TabIndex = 7;
            btnSaadaEmail.Click += btnSaadaEmail_Click;

            valik.Controls.Add(lblToode);
            valik.Controls.Add(btnLisaKorvi);

            nupud.Controls.Add(btnMiinus);
            nupud.Controls.Add(btnPluss);
            nupud.Controls.Add(btnEemalda);
            nupud.Controls.Add(btnGenPDF);
            nupud.Controls.Add(btnAvaPdf);
            nupud.Controls.Add(btnSaadaEmail);

            paigutus.Controls.Add(lblPealkiri, 0, 0);
            paigutus.Controls.Add(valik, 0, 1);
            paigutus.Controls.Add(listOstukorv, 0, 2);
            paigutus.Controls.Add(lblKokku, 0, 3);
            paigutus.Controls.Add(nupud, 0, 4);

            // ArveForm
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.FromArgb(18, 22, 30);
            ForeColor = Color.FromArgb(235, 239, 247);
            ClientSize = new Size(950, 650);
            MinimumSize = new Size(900, 580);
            Controls.Add(paigutus);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Name = "ArveForm";
            Text = "Ostukorv ja arve";
            Load += ArveForm_Load;
            FormClosing += ArveForm_FormClosing;

            valik.ResumeLayout(false);
            valik.PerformLayout();
            nupud.ResumeLayout(false);
            paigutus.ResumeLayout(false);
            paigutus.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel paigutus;
        private Label lblPealkiri;
        private FlowLayoutPanel valik;
        private Label lblToode;
        private Button btnLisaKorvi;
        private ListBox listOstukorv;
        private Label lblKokku;
        private FlowLayoutPanel nupud;
        private Button btnMiinus;
        private Button btnPluss;
        private Button btnEemalda;
        private Button btnGenPDF;
        private Button btnAvaPdf;
        private Button btnSaadaEmail;
    }
}