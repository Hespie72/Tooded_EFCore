using System;
using System.Linq;
using System.Windows.Forms;

namespace Tooded_EFCore
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            AcceptButton = btnLogin; // Enter = Logi sisse
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using var db = new ToodedContext();

            string email = txtEmail.Text.Trim();
            string parool = txtParool.Text;

            var kasutaja = db.Kasutajad.FirstOrDefault(k => k.Email == email);

            if (kasutaja == null)
            {
                lblVeateade.Text = "Vale email!";
                return;
            }

            if (!PasswordHelper.Verify(parool, kasutaja.ParoolHash))
            {
                lblVeateade.Text = "Vale parool!";
                return;
            }

            // roll salvestatakse globaalselt
            AktiivneKasutaja.Roll = kasutaja.Roll;
            AktiivneKasutaja.Nimi = kasutaja.Nimi;
            AktiivneKasutaja.Email = kasutaja.Email;

            // peale login
            Form1 main = new Form1();
            main.FormClosed += (s, args) =>
            {
                AktiivneKasutaja.Logout();
                Close(); // sulgeb ka peidetud LoginForm'i -> rakendus lõpetab
            };
            main.Show();
            Hide();
        }
    }
}


/*
admin	admin@admin.ee	admin123
kasutaja	kasutaja@kasutaja.ee	kasutaja123
*/