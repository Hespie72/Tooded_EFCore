#nullable disable

using System.Drawing;
using System.Windows.Forms;

namespace Tooded_EFCore
{
    partial class Form3
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
            filtrid = new FlowLayoutPanel();
            lblOtsing = new Label();
            txtOtsing = new TextBox();
            lblAlates = new Label();
            txtHindAlates = new TextBox();
            lblKuni = new Label();
            txtHindKuni = new TextBox();
            lblJarjestus = new Label();
            comboJarjestus = new ComboBox();
            chkLaos = new CheckBox();
            btnPuhasta = new Button();
            btnKorv = new Button();
            lblTulemus = new Label();
            tabPood = new TabControl();

            paigutus.SuspendLayout();
            filtrid.SuspendLayout();
            SuspendLayout();

            //
            // paigutus
            //
            paigutus.ColumnCount = 1;
            paigutus.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100F));

            paigutus.RowCount = 4;
            paigutus.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 60F));
            paigutus.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 110F));
            paigutus.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 30F));
            paigutus.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            paigutus.Controls.Add(lblPealkiri, 0, 0);
            paigutus.Controls.Add(filtrid, 0, 1);
            paigutus.Controls.Add(lblTulemus, 0, 2);
            paigutus.Controls.Add(tabPood, 0, 3);

            paigutus.Dock = DockStyle.Fill;
            paigutus.Location = new Point(0, 0);
            paigutus.Name = "paigutus";
            paigutus.Size = new Size(1160, 800);
            paigutus.TabIndex = 0;

            //
            // lblPealkiri
            //
            lblPealkiri.AutoSize = true;
            lblPealkiri.BackColor = Color.Transparent;
            lblPealkiri.Font =
                new Font("Segoe UI", 19F, FontStyle.Bold);
            lblPealkiri.Location = new Point(10, 8);
            lblPealkiri.Margin = new Padding(10, 8, 3, 3);
            lblPealkiri.Name = "lblPealkiri";
            lblPealkiri.TabIndex = 0;
            lblPealkiri.Text = "Pood";

            //
            // filtrid
            //
            filtrid.Controls.Add(lblOtsing);
            filtrid.Controls.Add(txtOtsing);
            filtrid.Controls.Add(lblAlates);
            filtrid.Controls.Add(txtHindAlates);
            filtrid.Controls.Add(lblKuni);
            filtrid.Controls.Add(txtHindKuni);
            filtrid.Controls.Add(lblJarjestus);
            filtrid.Controls.Add(comboJarjestus);
            filtrid.Controls.Add(chkLaos);
            filtrid.Controls.Add(btnPuhasta);
            filtrid.Controls.Add(btnKorv);

            filtrid.AutoScroll = true;
            filtrid.Dock = DockStyle.Fill;
            filtrid.FlowDirection = FlowDirection.LeftToRight;
            filtrid.Location = new Point(3, 63);
            filtrid.Name = "filtrid";
            filtrid.Padding = new Padding(8);
            filtrid.Size = new Size(1154, 104);
            filtrid.TabIndex = 1;
            filtrid.WrapContents = true;

            //
            // lblOtsing
            //
            lblOtsing.AutoSize = true;
            lblOtsing.BackColor = Color.Transparent;
            lblOtsing.Margin = new Padding(3, 7, 3, 3);
            lblOtsing.Name = "lblOtsing";
            lblOtsing.TabIndex = 0;
            lblOtsing.Text = "Otsi:";

            //
            // txtOtsing
            //
            txtOtsing.BackColor = Color.FromArgb(29, 35, 47);
            txtOtsing.BorderStyle = BorderStyle.FixedSingle;
            txtOtsing.ForeColor = Color.FromArgb(235, 239, 247);
            txtOtsing.Name = "txtOtsing";
            txtOtsing.PlaceholderText = "Sisesta toote nimi";
            txtOtsing.Size = new Size(220, 25);
            txtOtsing.TabIndex = 1;
            txtOtsing.TextChanged += Filtrid_Changed;

            //
            // lblAlates
            //
            lblAlates.AutoSize = true;
            lblAlates.BackColor = Color.Transparent;
            lblAlates.Margin = new Padding(3, 7, 3, 3);
            lblAlates.Name = "lblAlates";
            lblAlates.TabIndex = 2;
            lblAlates.Text = "Hind alates:";

            //
            // txtHindAlates
            //
            txtHindAlates.BackColor = Color.FromArgb(29, 35, 47);
            txtHindAlates.BorderStyle = BorderStyle.FixedSingle;
            txtHindAlates.ForeColor = Color.FromArgb(235, 239, 247);
            txtHindAlates.Name = "txtHindAlates";
            txtHindAlates.PlaceholderText = "Alates (€)";
            txtHindAlates.Size = new Size(90, 25);
            txtHindAlates.TabIndex = 3;
            txtHindAlates.TextChanged += Filtrid_Changed;

            //
            // lblKuni
            //
            lblKuni.AutoSize = true;
            lblKuni.BackColor = Color.Transparent;
            lblKuni.Margin = new Padding(3, 7, 3, 3);
            lblKuni.Name = "lblKuni";
            lblKuni.TabIndex = 4;
            lblKuni.Text = "kuni:";

            //
            // txtHindKuni
            //
            txtHindKuni.BackColor = Color.FromArgb(29, 35, 47);
            txtHindKuni.BorderStyle = BorderStyle.FixedSingle;
            txtHindKuni.ForeColor = Color.FromArgb(235, 239, 247);
            txtHindKuni.Name = "txtHindKuni";
            txtHindKuni.PlaceholderText = "Kuni (€)";
            txtHindKuni.Size = new Size(90, 25);
            txtHindKuni.TabIndex = 5;
            txtHindKuni.TextChanged += Filtrid_Changed;

            //
            // lblJarjestus
            //
            lblJarjestus.AutoSize = true;
            lblJarjestus.BackColor = Color.Transparent;
            lblJarjestus.Margin = new Padding(3, 7, 3, 3);
            lblJarjestus.Name = "lblJarjestus";
            lblJarjestus.TabIndex = 6;
            lblJarjestus.Text = "Sorteeri:";

            //
            // comboJarjestus
            //
            comboJarjestus.BackColor = Color.FromArgb(29, 35, 47);
            comboJarjestus.DropDownStyle =
                ComboBoxStyle.DropDownList;
            comboJarjestus.FlatStyle = FlatStyle.Flat;
            comboJarjestus.ForeColor =
                Color.FromArgb(235, 239, 247);
            comboJarjestus.FormattingEnabled = true;
            comboJarjestus.Items.AddRange(new object[]
            {
                "Nimetus A–Z",
                "Nimetus Z–A",
                "Hind kasvavalt",
                "Hind kahanevalt"
            });
            comboJarjestus.Name = "comboJarjestus";
            comboJarjestus.Size = new Size(180, 25);
            comboJarjestus.TabIndex = 7;
            comboJarjestus.SelectedIndexChanged += Filtrid_Changed;

            //
            // chkLaos
            //
            chkLaos.AutoSize = true;
            chkLaos.Margin = new Padding(12, 6, 3, 3);
            chkLaos.Name = "chkLaos";
            chkLaos.TabIndex = 8;
            chkLaos.Text = "Ainult laos";
            chkLaos.UseVisualStyleBackColor = true;
            chkLaos.CheckedChanged += Filtrid_Changed;

            //
            // btnPuhasta
            //
            btnPuhasta.AutoSize = true;
            btnPuhasta.BackColor = Color.FromArgb(29, 35, 47);
            btnPuhasta.Cursor = Cursors.Hand;
            btnPuhasta.FlatAppearance.BorderColor =
                Color.FromArgb(52, 63, 82);
            btnPuhasta.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(48, 65, 94);
            btnPuhasta.FlatStyle = FlatStyle.Flat;
            btnPuhasta.ForeColor = Color.FromArgb(235, 239, 247);
            btnPuhasta.Name = "btnPuhasta";
            btnPuhasta.Size = new Size(115, 32);
            btnPuhasta.TabIndex = 9;
            btnPuhasta.Text = "Puhasta filtrid";
            btnPuhasta.UseVisualStyleBackColor = false;
            btnPuhasta.Click += btnPuhasta_Click;

            //
            // btnKorv
            //
            btnKorv.AutoSize = true;
            btnKorv.BackColor = Color.FromArgb(29, 35, 47);
            btnKorv.Cursor = Cursors.Hand;
            btnKorv.FlatAppearance.BorderColor =
                Color.FromArgb(52, 63, 82);
            btnKorv.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(48, 65, 94);
            btnKorv.FlatStyle = FlatStyle.Flat;
            btnKorv.ForeColor = Color.FromArgb(235, 239, 247);
            btnKorv.Name = "btnKorv";
            btnKorv.Size = new Size(135, 32);
            btnKorv.TabIndex = 10;
            btnKorv.Text = "Ostukorv ja arve";
            btnKorv.UseVisualStyleBackColor = false;
            btnKorv.Click += btnKorv_Click;

            //
            // lblTulemus
            //
            lblTulemus.BackColor = Color.Transparent;
            lblTulemus.Dock = DockStyle.Fill;
            lblTulemus.ForeColor = Color.FromArgb(158, 171, 193);
            lblTulemus.Location = new Point(3, 170);
            lblTulemus.Name = "lblTulemus";
            lblTulemus.Padding = new Padding(10, 0, 0, 0);
            lblTulemus.Size = new Size(1154, 30);
            lblTulemus.TabIndex = 2;
            lblTulemus.Text = "Leitud tooteid: 0";
            lblTulemus.TextAlign = ContentAlignment.MiddleLeft;

            //
            // tabPood
            //
            tabPood.Dock = DockStyle.Fill;
            tabPood.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabPood.ItemSize = new Size(160, 36);
            tabPood.Location = new Point(3, 203);
            tabPood.Name = "tabPood";
            tabPood.SelectedIndex = 0;
            tabPood.Size = new Size(1154, 594);
            tabPood.SizeMode = TabSizeMode.Fixed;
            tabPood.TabIndex = 3;
            tabPood.DrawItem += tabPood_DrawItem;

            //
            // Form3
            //
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 22, 30);
            ClientSize = new Size(1160, 800);
            Controls.Add(paigutus);
            Font = new Font("Segoe UI", 10F);
            ForeColor = Color.FromArgb(235, 239, 247);
            MinimumSize = new Size(950, 650);
            Name = "Form3";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pood";
            Load += Form3_Load;

            paigutus.ResumeLayout(false);
            paigutus.PerformLayout();
            filtrid.ResumeLayout(false);
            filtrid.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel paigutus;
        private Label lblPealkiri;
        private FlowLayoutPanel filtrid;
        private Label lblOtsing;
        private TextBox txtOtsing;
        private Label lblAlates;
        private TextBox txtHindAlates;
        private Label lblKuni;
        private TextBox txtHindKuni;
        private Label lblJarjestus;
        private ComboBox comboJarjestus;
        private CheckBox chkLaos;
        private Button btnPuhasta;
        private Button btnKorv;
        private Label lblTulemus;
        private TabControl tabPood;
    }
}