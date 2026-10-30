namespace Tooded_EFCore;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        Controls.Clear();

        Text = "Peamenüü";
        ClientSize = new Size(450, 370);

        var paneel = Kujundus.Veerg();
        paneel.Padding = new Padding(35);

        paneel.Controls.Add(
            Kujundus.Pealkiri("Toodete pood"));

        paneel.Controls.Add(new Label
        {
            Text =
                $"{AktiivneKasutaja.Nimi} · " +
                $"{AktiivneKasutaja.Roll}",

            AutoSize = true,
            Margin = new Padding(3, 0, 3, 20)
        });

        btnTooted.Text =
            "Toodete ja kategooriate haldus";

        btnPood.Text = "Ava pood";
        btnArve.Text = "Ostukorv ja arve";

        foreach (var nupp in new[]
                 {
                     btnPood,
                     btnArve,
                     btnTooted
                 })
        {
            nupp.Size = new Size(370, 52);
            nupp.Margin = new Padding(3, 3, 3, 10);

            paneel.Controls.Add(nupp);
        }

        btnTooted.Enabled =
            AktiivneKasutaja.OnAdmin;

        Controls.Add(paneel);

        Kujundus.Rakenda(this);
        Kujundus.Rohuta(btnPood);
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
                "Vormi avamine ebaõnnestus:\n" +
                viga.Message,
                "Viga");
        }
    }

    private void btnTooted_Click(
        object sender,
        EventArgs e)
    {
        if (!AktiivneKasutaja.OnAdmin)
            return;

        AvaVorm(() => new Form2());
    }

    private void btnPood_Click(
        object sender,
        EventArgs e)
    {
        AvaVorm(() => new Form3());
    }

    private void btnArve_Click(
        object sender,
        EventArgs e)
    {
        AvaVorm(() => new ArveForm());
    }
}