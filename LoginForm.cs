namespace Tooded_EFCore;

public partial class LoginForm : Form
{
    public LoginForm()
    {
        InitializeComponent();

        Controls.Clear();

        Text = "Sisselogimine";
        ClientSize = new Size(410, 410);

        var paneel = Kujundus.Veerg();
        paneel.Padding = new Padding(35);

        paneel.Controls.Add(
            Kujundus.Pealkiri("Tere tulemast!"));

        paneel.Controls.Add(new Label
        {
            Text = "Jätkamiseks logi oma kontole sisse.",
            AutoSize = true
        });

        lblEmail.Text = "E-post";
        lblEmail.Margin = new Padding(3, 22, 3, 5);

        paneel.Controls.Add(lblEmail);

        txtEmail.Width = 320;
        txtEmail.PlaceholderText = "nimi@example.com";

        paneel.Controls.Add(txtEmail);

        lblParool.Margin = new Padding(3, 14, 3, 5);

        paneel.Controls.Add(lblParool);

        txtParool.Width = 320;
        txtParool.UseSystemPasswordChar = true;

        paneel.Controls.Add(txtParool);

        btnLogin.Size = new Size(320, 44);
        btnLogin.Margin = new Padding(3, 22, 3, 8);

        paneel.Controls.Add(btnLogin);
        paneel.Controls.Add(lblVeateade);

        Controls.Add(paneel);

        AcceptButton = btnLogin;

        Kujundus.Rakenda(this);
        Kujundus.Rohuta(btnLogin);

        lblVeateade.ForeColor = Color.Salmon;
    }

    private void btnLogin_Click(
        object sender,
        EventArgs e)
    {
        try
        {
            using var db = new ToodedContext();

            string email = txtEmail.Text.Trim();

            var kasutaja = db.Kasutajad
                .FirstOrDefault(k => k.Email == email);

            if (kasutaja == null ||
                !PasswordHelper.Verify(
                    txtParool.Text,
                    kasutaja.ParoolHash))
            {
                lblVeateade.Text =
                    "Vale e-post või parool!";

                return;
            }

            AktiivneKasutaja.Roll = kasutaja.Roll;
            AktiivneKasutaja.Nimi = kasutaja.Nimi;
            AktiivneKasutaja.Email = kasutaja.Email;

            Hide();

            try
            {
                using var peamenuu = new Form1();
                peamenuu.ShowDialog();
            }
            finally
            {
                AktiivneKasutaja.Logout();
                Close();
            }
        }
        catch (Exception viga)
        {
            MessageBox.Show(
                "Sisselogimine ebaõnnestus:\n" +
                viga.Message,
                "Viga");
        }
    }
}
/*
admin	admin@admin.ee	admin123
kasutaja	kasutaja@kasutaja.ee	kasutaja123
*/