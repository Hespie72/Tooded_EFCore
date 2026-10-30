namespace Tooded_EFCore;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        btnTooted.Enabled = AktiivneKasutaja.OnAdmin;

        Text = $"Peamenüü — {AktiivneKasutaja.Nimi}";
    }

    private void AvaVorm(Func<Form> looVorm)
    {
        try
        {
            using var vorm = looVorm();
            vorm.ShowDialog(this);
        }
        catch (Exception viga)
        {
            MessageBox.Show(
                "Vormi avamine ebaõnnestus:\n" + viga.Message,
                "Viga");
        }
    }

    private void btnTooted_Click(object sender, EventArgs e)
    {
        if (!AktiivneKasutaja.OnAdmin)
        {
            MessageBox.Show(
                "Tootehaldus on lubatud ainult administraatorile.");

            return;
        }

        AvaVorm(() => new Form2());
    }

    private void btnPood_Click(object sender, EventArgs e)
    {
        AvaVorm(() => new Form3());
    }

    private void btnArve_Click(object sender, EventArgs e)
    {
        AvaVorm(() => new ArveForm());
    }
}