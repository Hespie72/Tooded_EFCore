namespace Tooded_EFCore;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        lblKasutaja.Text =
            $"{AktiivneKasutaja.Nimi} · {AktiivneKasutaja.Roll}";

        btnTooted.Enabled = AktiivneKasutaja.OnAdmin;
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
            return;

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