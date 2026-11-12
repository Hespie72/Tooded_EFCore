#nullable disable

using System.Drawing;
using System.Windows.Forms;

namespace Tooded_EFCore
{
    partial class LoginForm
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
            lblJuhis = new Label();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblParool = new Label();
            txtParool = new TextBox();
            btnLogin = new Button();
            lblVeateade = new Label();

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
            lblPealkiri.Text = "Tere tulemast!";
            lblPealkiri.AutoSize = true;
            lblPealkiri.BackColor = Color.Transparent;
            lblPealkiri.Font = new Font(
                "Segoe UI", 19F, FontStyle.Bold);
            lblPealkiri.Margin = new Padding(3, 3, 3, 16);

            // lblJuhis
            lblJuhis.Name = "lblJuhis";
            lblJuhis.Text = "Jätkamiseks logi oma kontole sisse.";
            lblJuhis.AutoSize = true;
            lblJuhis.BackColor = Color.Transparent;

            // lblEmail
            lblEmail.Name = "lblEmail";
            lblEmail.Text = "E-post";
            lblEmail.AutoSize = true;
            lblEmail.BackColor = Color.Transparent;
            lblEmail.Margin = new Padding(3, 22, 3, 5);

            // txtEmail
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(320, 25);
            txtEmail.PlaceholderText = "nimi@example.com";
            txtEmail.BackColor = Color.FromArgb(29, 35, 47);
            txtEmail.ForeColor = Color.FromArgb(235, 239, 247);
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.TabIndex = 0;

            // lblParool
            lblParool.Name = "lblParool";
            lblParool.Text = "Parool:";
            lblParool.AutoSize = true;
            lblParool.BackColor = Color.Transparent;
            lblParool.Margin = new Padding(3, 14, 3, 5);

            // txtParool
            txtParool.Name = "txtParool";
            txtParool.Size = new Size(320, 25);
            txtParool.UseSystemPasswordChar = true;
            txtParool.BackColor = Color.FromArgb(29, 35, 47);
            txtParool.ForeColor = Color.FromArgb(235, 239, 247);
            txtParool.BorderStyle = BorderStyle.FixedSingle;
            txtParool.TabIndex = 1;

            // btnLogin
            btnLogin.Name = "btnLogin";
            btnLogin.Text = "Logi sisse";
            btnLogin.Size = new Size(320, 44);
            btnLogin.Margin = new Padding(3, 22, 3, 8);
            btnLogin.BackColor = Color.FromArgb(77, 130, 245);
            btnLogin.ForeColor = Color.FromArgb(235, 239, 247);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderColor =
                Color.FromArgb(52, 63, 82);
            btnLogin.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(48, 65, 94);
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.TabIndex = 2;
            btnLogin.Click += btnLogin_Click;

            // lblVeateade
            lblVeateade.Name = "lblVeateade";
            lblVeateade.AutoSize = true;
            lblVeateade.BackColor = Color.Transparent;
            lblVeateade.ForeColor = Color.Salmon;

            paneel.Controls.Add(lblPealkiri);
            paneel.Controls.Add(lblJuhis);
            paneel.Controls.Add(lblEmail);
            paneel.Controls.Add(txtEmail);
            paneel.Controls.Add(lblParool);
            paneel.Controls.Add(txtParool);
            paneel.Controls.Add(btnLogin);
            paneel.Controls.Add(lblVeateade);

            // LoginForm
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.FromArgb(18, 22, 30);
            ForeColor = Color.FromArgb(235, 239, 247);
            ClientSize = new Size(410, 410);
            Controls.Add(paneel);
            AcceptButton = btnLogin;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Name = "LoginForm";
            Text = "Sisselogimine";

            paneel.ResumeLayout(false);
            paneel.PerformLayout();
            ResumeLayout(false);
        }

        private FlowLayoutPanel paneel;
        private Label lblPealkiri;
        private Label lblJuhis;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblParool;
        private TextBox txtParool;
        private Button btnLogin;
        private Label lblVeateade;
    }
}