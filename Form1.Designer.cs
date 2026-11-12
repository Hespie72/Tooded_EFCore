#nullable disable

using System.Drawing;
using System.Windows.Forms;

namespace Tooded_EFCore
{
    partial class Form1
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
            paneel = new FlowLayoutPanel();
            lblPealkiri = new Label();
            lblKasutaja = new Label();
            btnPood = new Button();
            btnArve = new Button();
            btnTooted = new Button();

            paneel.SuspendLayout();
            SuspendLayout();

            // paneel
            paneel.Name = "paneel";
            paneel.Dock = DockStyle.Fill;
            paneel.FlowDirection = FlowDirection.TopDown;
            paneel.WrapContents = false;
            paneel.AutoScroll = true;
            paneel.Padding = new Padding(35);

            // lblPealkiri
            lblPealkiri.Name = "lblPealkiri";
            lblPealkiri.Text = "Toodete pood";
            lblPealkiri.AutoSize = true;
            lblPealkiri.BackColor = Color.Transparent;
            lblPealkiri.Font = new Font(
                "Segoe UI", 19F, FontStyle.Bold);
            lblPealkiri.Margin = new Padding(3, 3, 3, 16);

            // lblKasutaja
            lblKasutaja.Name = "lblKasutaja";
            lblKasutaja.Text = "Kasutaja · roll";
            lblKasutaja.AutoSize = true;
            lblKasutaja.BackColor = Color.Transparent;
            lblKasutaja.Margin = new Padding(3, 0, 3, 20);

            // btnPood
            btnPood.Name = "btnPood";
            btnPood.Text = "Ava pood";
            btnPood.Size = new Size(370, 52);
            btnPood.Margin = new Padding(3, 3, 3, 10);
            btnPood.BackColor = Color.FromArgb(77, 130, 245);
            btnPood.ForeColor = Color.FromArgb(235, 239, 247);
            btnPood.FlatStyle = FlatStyle.Flat;
            btnPood.FlatAppearance.BorderColor =
                Color.FromArgb(52, 63, 82);
            btnPood.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(48, 65, 94);
            btnPood.UseVisualStyleBackColor = false;
            btnPood.Cursor = Cursors.Hand;
            btnPood.TabIndex = 0;
            btnPood.Click += btnPood_Click;

            // btnArve
            btnArve.Name = "btnArve";
            btnArve.Text = "Ostukorv ja arve";
            btnArve.Size = new Size(370, 52);
            btnArve.Margin = new Padding(3, 3, 3, 10);
            btnArve.BackColor = Color.FromArgb(29, 35, 47);
            btnArve.ForeColor = Color.FromArgb(235, 239, 247);
            btnArve.FlatStyle = FlatStyle.Flat;
            btnArve.FlatAppearance.BorderColor =
                Color.FromArgb(52, 63, 82);
            btnArve.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(48, 65, 94);
            btnArve.UseVisualStyleBackColor = false;
            btnArve.Cursor = Cursors.Hand;
            btnArve.TabIndex = 1;
            btnArve.Click += btnArve_Click;

            // btnTooted
            btnTooted.Name = "btnTooted";
            btnTooted.Text = "Toodete ja kategooriate haldus";
            btnTooted.Size = new Size(370, 52);
            btnTooted.Margin = new Padding(3, 3, 3, 10);
            btnTooted.BackColor = Color.FromArgb(29, 35, 47);
            btnTooted.ForeColor = Color.FromArgb(235, 239, 247);
            btnTooted.FlatStyle = FlatStyle.Flat;
            btnTooted.FlatAppearance.BorderColor =
                Color.FromArgb(52, 63, 82);
            btnTooted.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(48, 65, 94);
            btnTooted.UseVisualStyleBackColor = false;
            btnTooted.Cursor = Cursors.Hand;
            btnTooted.TabIndex = 2;
            btnTooted.Click += btnTooted_Click;

            paneel.Controls.Add(lblPealkiri);
            paneel.Controls.Add(lblKasutaja);
            paneel.Controls.Add(btnPood);
            paneel.Controls.Add(btnArve);
            paneel.Controls.Add(btnTooted);

            // Form1
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.FromArgb(18, 22, 30);
            ForeColor = Color.FromArgb(235, 239, 247);
            ClientSize = new Size(450, 370);
            Controls.Add(paneel);
            StartPosition = FormStartPosition.CenterScreen;
            Name = "Form1";
            Text = "Peamenüü";

            paneel.ResumeLayout(false);
            paneel.PerformLayout();
            ResumeLayout(false);
        }

        private FlowLayoutPanel paneel;
        private Label lblPealkiri;
        private Label lblKasutaja;
        private Button btnPood;
        private Button btnArve;
        private Button btnTooted;
    }
}