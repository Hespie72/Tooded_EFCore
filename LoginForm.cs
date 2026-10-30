namespace Tooded_EFCore;

public partial class LoginForm : Form
{
    public LoginForm()
    {
        InitializeComponent();

        AcceptButton = btnLogin;
    }

    private void btnLogin_Click(object sender, EventArgs e)
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
                lblVeateade.Text = "Vale e-post või parool!";
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
                "Sisselogimine ebaõnnestus:\n" + viga.Message,
                "Viga");
        }
    }
}

/*
admin	admin@admin.ee	admin123
kasutaja	kasutaja@kasutaja.ee	kasutaja123
*/