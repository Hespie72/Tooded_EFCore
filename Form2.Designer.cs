#nullable disable

using System.Drawing;
using System.Windows.Forms;

namespace Tooded_EFCore
{
    partial class Form2
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            paigutus = new TableLayoutPanel();
            lblPealkiri = new Label();
            dataGridViewTooted = new DataGridView();
            valjad = new FlowLayoutPanel();
            lblNimetus = new Label();
            txtNimetus = new TextBox();
            lblKogus = new Label();
            txtKogus = new TextBox();
            lblHind = new Label();
            txtHind = new TextBox();
            lblKategooria = new Label();
            comboKategooria = new ComboBox();
            picturePilt = new PictureBox();
            btnValiPilt = new Button();
            btnLisa = new Button();
            btnUuenda = new Button();
            btnKustuta = new Button();
            button1 = new Button();
            btnArve = new Button();
            paigutus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTooted).BeginInit();
            valjad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picturePilt).BeginInit();
            SuspendLayout();
            // 
            // paigutus
            // 
            paigutus.ColumnCount = 2;
            paigutus.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            paigutus.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 330F));
            paigutus.Controls.Add(lblPealkiri, 0, 0);
            paigutus.Controls.Add(dataGridViewTooted, 0, 1);
            paigutus.Controls.Add(valjad, 1, 1);
            paigutus.Dock = DockStyle.Fill;
            paigutus.Location = new Point(0, 0);
            paigutus.Name = "paigutus";
            paigutus.Padding = new Padding(15);
            paigutus.RowCount = 2;
            paigutus.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            paigutus.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            paigutus.Size = new Size(1180, 800);
            paigutus.TabIndex = 0;
            // 
            // lblPealkiri
            // 
            lblPealkiri.AutoSize = true;
            lblPealkiri.BackColor = Color.Transparent;
            paigutus.SetColumnSpan(lblPealkiri, 2);
            lblPealkiri.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblPealkiri.Location = new Point(18, 18);
            lblPealkiri.Margin = new Padding(3, 3, 3, 16);
            lblPealkiri.Name = "lblPealkiri";
            lblPealkiri.Size = new Size(201, 36);
            lblPealkiri.TabIndex = 0;
            lblPealkiri.Text = "Toodete haldus";
            // 
            // dataGridViewTooted
            // 
            dataGridViewTooted.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(24, 30, 41);
            dataGridViewTooted.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewTooted.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewTooted.BackgroundColor = Color.FromArgb(29, 35, 47);
            dataGridViewTooted.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(52, 63, 82);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(235, 239, 247);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(52, 63, 82);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(235, 239, 247);
            dataGridViewTooted.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewTooted.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(29, 35, 47);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(235, 239, 247);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(44, 72, 117);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridViewTooted.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewTooted.Dock = DockStyle.Fill;
            dataGridViewTooted.EnableHeadersVisualStyles = false;
            dataGridViewTooted.GridColor = Color.FromArgb(52, 63, 82);
            dataGridViewTooted.Location = new Point(18, 78);
            dataGridViewTooted.MultiSelect = false;
            dataGridViewTooted.Name = "dataGridViewTooted";
            dataGridViewTooted.ReadOnly = true;
            dataGridViewTooted.RowHeadersVisible = false;
            dataGridViewTooted.RowTemplate.Height = 35;
            dataGridViewTooted.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewTooted.Size = new Size(814, 704);
            dataGridViewTooted.TabIndex = 0;
            dataGridViewTooted.CellClick += dataGridViewTooted_CellClick;
            // 
            // valjad
            // 
            valjad.AutoScroll = true;
            valjad.Controls.Add(lblNimetus);
            valjad.Controls.Add(txtNimetus);
            valjad.Controls.Add(lblKogus);
            valjad.Controls.Add(txtKogus);
            valjad.Controls.Add(lblHind);
            valjad.Controls.Add(txtHind);
            valjad.Controls.Add(lblKategooria);
            valjad.Controls.Add(comboKategooria);
            valjad.Controls.Add(picturePilt);
            valjad.Controls.Add(btnValiPilt);
            valjad.Controls.Add(btnLisa);
            valjad.Controls.Add(btnUuenda);
            valjad.Controls.Add(btnKustuta);
            valjad.Controls.Add(button1);
            valjad.Controls.Add(btnArve);
            valjad.Dock = DockStyle.Fill;
            valjad.FlowDirection = FlowDirection.TopDown;
            valjad.Location = new Point(838, 78);
            valjad.Name = "valjad";
            valjad.Padding = new Padding(15, 0, 5, 10);
            valjad.Size = new Size(324, 704);
            valjad.TabIndex = 1;
            valjad.WrapContents = false;
            // 
            // lblNimetus
            // 
            lblNimetus.AutoSize = true;
            lblNimetus.BackColor = Color.Transparent;
            lblNimetus.Location = new Point(18, 10);
            lblNimetus.Margin = new Padding(3, 10, 3, 5);
            lblNimetus.Name = "lblNimetus";
            lblNimetus.Size = new Size(96, 19);
            lblNimetus.TabIndex = 0;
            lblNimetus.Text = "Toote nimetus";
            // 
            // txtNimetus
            // 
            txtNimetus.BackColor = Color.FromArgb(29, 35, 47);
            txtNimetus.BorderStyle = BorderStyle.FixedSingle;
            txtNimetus.ForeColor = Color.FromArgb(235, 239, 247);
            txtNimetus.Location = new Point(18, 37);
            txtNimetus.Name = "txtNimetus";
            txtNimetus.PlaceholderText = "Toote nimetus";
            txtNimetus.Size = new Size(280, 25);
            txtNimetus.TabIndex = 1;
            // 
            // lblKogus
            // 
            lblKogus.AutoSize = true;
            lblKogus.BackColor = Color.Transparent;
            lblKogus.Location = new Point(18, 75);
            lblKogus.Margin = new Padding(3, 10, 3, 5);
            lblKogus.Name = "lblKogus";
            lblKogus.Size = new Size(75, 19);
            lblKogus.TabIndex = 2;
            lblKogus.Text = "Kogus laos";
            // 
            // txtKogus
            // 
            txtKogus.BackColor = Color.FromArgb(29, 35, 47);
            txtKogus.BorderStyle = BorderStyle.FixedSingle;
            txtKogus.ForeColor = Color.FromArgb(235, 239, 247);
            txtKogus.Location = new Point(18, 102);
            txtKogus.Name = "txtKogus";
            txtKogus.PlaceholderText = "Kogus";
            txtKogus.Size = new Size(280, 25);
            txtKogus.TabIndex = 2;
            // 
            // lblHind
            // 
            lblHind.AutoSize = true;
            lblHind.BackColor = Color.Transparent;
            lblHind.Location = new Point(18, 140);
            lblHind.Margin = new Padding(3, 10, 3, 5);
            lblHind.Name = "lblHind";
            lblHind.Size = new Size(58, 19);
            lblHind.TabIndex = 3;
            lblHind.Text = "Hind (€)";
            // 
            // txtHind
            // 
            txtHind.BackColor = Color.FromArgb(29, 35, 47);
            txtHind.BorderStyle = BorderStyle.FixedSingle;
            txtHind.ForeColor = Color.FromArgb(235, 239, 247);
            txtHind.Location = new Point(18, 167);
            txtHind.Name = "txtHind";
            txtHind.PlaceholderText = "Hind €";
            txtHind.Size = new Size(280, 25);
            txtHind.TabIndex = 3;
            // 
            // lblKategooria
            // 
            lblKategooria.AutoSize = true;
            lblKategooria.BackColor = Color.Transparent;
            lblKategooria.Location = new Point(18, 205);
            lblKategooria.Margin = new Padding(3, 10, 3, 5);
            lblKategooria.Name = "lblKategooria";
            lblKategooria.Size = new Size(75, 19);
            lblKategooria.TabIndex = 4;
            lblKategooria.Text = "Kategooria";
            // 
            // comboKategooria
            // 
            comboKategooria.BackColor = Color.FromArgb(29, 35, 47);
            comboKategooria.DropDownStyle = ComboBoxStyle.DropDownList;
            comboKategooria.FlatStyle = FlatStyle.Flat;
            comboKategooria.ForeColor = Color.FromArgb(235, 239, 247);
            comboKategooria.Location = new Point(18, 232);
            comboKategooria.Name = "comboKategooria";
            comboKategooria.Size = new Size(280, 25);
            comboKategooria.TabIndex = 4;
            // 
            // picturePilt
            // 
            picturePilt.BackColor = Color.FromArgb(29, 35, 47);
            picturePilt.BorderStyle = BorderStyle.FixedSingle;
            picturePilt.Location = new Point(18, 273);
            picturePilt.Margin = new Padding(3, 15, 3, 8);
            picturePilt.Name = "picturePilt";
            picturePilt.Size = new Size(280, 170);
            picturePilt.SizeMode = PictureBoxSizeMode.Zoom;
            picturePilt.TabIndex = 5;
            picturePilt.TabStop = false;
            // 
            // btnValiPilt
            // 
            btnValiPilt.BackColor = Color.FromArgb(29, 35, 47);
            btnValiPilt.Cursor = Cursors.Hand;
            btnValiPilt.FlatAppearance.BorderColor = Color.FromArgb(52, 63, 82);
            btnValiPilt.FlatAppearance.MouseOverBackColor = Color.FromArgb(48, 65, 94);
            btnValiPilt.FlatStyle = FlatStyle.Flat;
            btnValiPilt.ForeColor = Color.FromArgb(235, 239, 247);
            btnValiPilt.Location = new Point(18, 454);
            btnValiPilt.Margin = new Padding(3, 3, 3, 5);
            btnValiPilt.Name = "btnValiPilt";
            btnValiPilt.Size = new Size(280, 38);
            btnValiPilt.TabIndex = 5;
            btnValiPilt.Text = "Vali pilt";
            btnValiPilt.UseVisualStyleBackColor = false;
            btnValiPilt.Click += btnValiPilt_Click;
            // 
            // btnLisa
            // 
            btnLisa.BackColor = Color.FromArgb(77, 130, 245);
            btnLisa.Cursor = Cursors.Hand;
            btnLisa.FlatAppearance.BorderColor = Color.FromArgb(52, 63, 82);
            btnLisa.FlatAppearance.MouseOverBackColor = Color.FromArgb(48, 65, 94);
            btnLisa.FlatStyle = FlatStyle.Flat;
            btnLisa.ForeColor = Color.FromArgb(235, 239, 247);
            btnLisa.Location = new Point(18, 500);
            btnLisa.Margin = new Padding(3, 3, 3, 5);
            btnLisa.Name = "btnLisa";
            btnLisa.Size = new Size(280, 38);
            btnLisa.TabIndex = 6;
            btnLisa.Text = "Lisa";
            btnLisa.UseVisualStyleBackColor = false;
            btnLisa.Click += btnLisa_Click;
            // 
            // btnUuenda
            // 
            btnUuenda.BackColor = Color.FromArgb(29, 35, 47);
            btnUuenda.Cursor = Cursors.Hand;
            btnUuenda.FlatAppearance.BorderColor = Color.FromArgb(52, 63, 82);
            btnUuenda.FlatAppearance.MouseOverBackColor = Color.FromArgb(48, 65, 94);
            btnUuenda.FlatStyle = FlatStyle.Flat;
            btnUuenda.ForeColor = Color.FromArgb(235, 239, 247);
            btnUuenda.Location = new Point(18, 546);
            btnUuenda.Margin = new Padding(3, 3, 3, 5);
            btnUuenda.Name = "btnUuenda";
            btnUuenda.Size = new Size(280, 38);
            btnUuenda.TabIndex = 7;
            btnUuenda.Text = "Uuenda";
            btnUuenda.UseVisualStyleBackColor = false;
            btnUuenda.Click += btnUuenda_Click;
            // 
            // btnKustuta
            // 
            btnKustuta.BackColor = Color.FromArgb(29, 35, 47);
            btnKustuta.Cursor = Cursors.Hand;
            btnKustuta.FlatAppearance.BorderColor = Color.FromArgb(52, 63, 82);
            btnKustuta.FlatAppearance.MouseOverBackColor = Color.FromArgb(48, 65, 94);
            btnKustuta.FlatStyle = FlatStyle.Flat;
            btnKustuta.ForeColor = Color.Salmon;
            btnKustuta.Location = new Point(18, 592);
            btnKustuta.Margin = new Padding(3, 3, 3, 5);
            btnKustuta.Name = "btnKustuta";
            btnKustuta.Size = new Size(280, 38);
            btnKustuta.TabIndex = 8;
            btnKustuta.Text = "Kustuta";
            btnKustuta.UseVisualStyleBackColor = false;
            btnKustuta.Click += btnKustuta_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(29, 35, 47);
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderColor = Color.FromArgb(52, 63, 82);
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(48, 65, 94);
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.FromArgb(235, 239, 247);
            button1.Location = new Point(18, 638);
            button1.Margin = new Padding(3, 3, 3, 5);
            button1.Name = "button1";
            button1.Size = new Size(280, 38);
            button1.TabIndex = 9;
            button1.Text = "Kategooriad";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // btnArve
            // 
            btnArve.BackColor = Color.FromArgb(29, 35, 47);
            btnArve.Cursor = Cursors.Hand;
            btnArve.FlatAppearance.BorderColor = Color.FromArgb(52, 63, 82);
            btnArve.FlatAppearance.MouseOverBackColor = Color.FromArgb(48, 65, 94);
            btnArve.FlatStyle = FlatStyle.Flat;
            btnArve.ForeColor = Color.FromArgb(235, 239, 247);
            btnArve.Location = new Point(18, 684);
            btnArve.Margin = new Padding(3, 3, 3, 5);
            btnArve.Name = "btnArve";
            btnArve.Size = new Size(280, 38);
            btnArve.TabIndex = 10;
            btnArve.Text = "Lisa arvele";
            btnArve.UseVisualStyleBackColor = false;
            btnArve.Click += btnArve_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 22, 30);
            ClientSize = new Size(1180, 800);
            Controls.Add(paigutus);
            Font = new Font("Segoe UI", 10F);
            ForeColor = Color.FromArgb(235, 239, 247);
            MinimumSize = new Size(980, 700);
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Toodete haldus";
            Load += Form2_Load;
            paigutus.ResumeLayout(false);
            paigutus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTooted).EndInit();
            valjad.ResumeLayout(false);
            valjad.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picturePilt).EndInit();
            ResumeLayout(false);
        }

        private TableLayoutPanel paigutus;
        private Label lblPealkiri;
        private DataGridView dataGridViewTooted;
        private FlowLayoutPanel valjad;
        private Label lblNimetus;
        private TextBox txtNimetus;
        private Label lblKogus;
        private TextBox txtKogus;
        private Label lblHind;
        private TextBox txtHind;
        private Label lblKategooria;
        private ComboBox comboKategooria;
        private PictureBox picturePilt;
        private Button btnValiPilt;
        private Button btnLisa;
        private Button btnUuenda;
        private Button btnKustuta;
        private Button button1;
        private Button btnArve;
    }
}