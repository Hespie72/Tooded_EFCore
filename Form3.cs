using Microsoft.EntityFrameworkCore;

namespace Tooded_EFCore;

public partial class Form3 : Form
{
    private readonly List<Image> pildid = new();

    public Form3()
    {
        InitializeComponent();

        Load += (s, e) =>
        {
            try
            {
                LaeKategooriadJaPildid();
            }
            catch (Exception viga)
            {
                MessageBox.Show(
                    "Poe laadimine ebaõnnestus:\n" +
                    viga.Message,
                    "Viga");
            }
        };
    }

    private void LaeKategooriadJaPildid()
    {
        using var db = new ToodedContext();

        var kategooriad = db.Kategooriatabel
            .AsNoTracking()
            .Include(k => k.Tooded)
            .OrderBy(k => k.Kategooria_nimetus)
            .ToList();

        foreach (var kat in kategooriad)
        {
            var leht = new TabPage(
                kat.Kategooria_nimetus);

            var paigutus = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            leht.Controls.Add(paigutus);

            tabPood.TabPages.Add(leht);

            foreach (var toode in kat.Tooded
                         .OrderBy(t => t.Toodenimetus))
            {
                var kaart = new FlowLayoutPanel
                {
                    Size = new Size(200, 280),
                    FlowDirection =
                        FlowDirection.TopDown,
                    WrapContents = false,
                    BorderStyle =
                        BorderStyle.FixedSingle,
                    Padding = new Padding(5)
                };

                var pildikast = new PictureBox
                {
                    Size = new Size(185, 150),
                    SizeMode =
                        PictureBoxSizeMode.Zoom
                };

                string nimi = Path.GetFileName(
                    toode.Pilt ?? "");

                string tee = Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder
                            .LocalApplicationData),
                    "Tooded_EFCore",
                    "Images",
                    nimi);

                if (!File.Exists(tee))
                {
                    tee = Path.Combine(
                        AppContext.BaseDirectory,
                        "Images",
                        nimi);
                }

                if (!File.Exists(tee))
                {
                    tee = Path.Combine(
                        "Images",
                        nimi);
                }

                try
                {
                    if (File.Exists(tee))
                    {
                        using var algne =
                            Image.FromFile(tee);

                        var koopia = new Bitmap(algne);

                        pildid.Add(koopia);
                        pildikast.Image = koopia;
                    }
                }
                catch (ArgumentException)
                {
                    pildikast.BackColor =
                        Color.LightGray;
                }
                catch (IOException)
                {
                    pildikast.BackColor =
                        Color.LightGray;
                }

                pildikast.Click += (s, e) =>
                    MessageBox.Show(
                        $"{toode.Toodenimetus}\n" +
                        $"Hind: {toode.Hind:0.00} €\n" +
                        $"Laos: {toode.Kogus} tk",
                        "Toote andmed");

                var nupp = new Button
                {
                    Text = "Vali ja lisa korvi",
                    Size = new Size(185, 32),
                    Enabled = toode.Kogus > 0
                };

                nupp.Click += (s, e) =>
                {
                    try
                    {
                        using var arve = new ArveForm
                        {
                            SelectedToode = toode
                        };

                        arve.ShowDialog(this);
                    }
                    catch (Exception viga)
                    {
                        MessageBox.Show(
                            "Arve avamine ebaõnnestus:\n" +
                            viga.Message,
                            "Viga");
                    }
                };

                kaart.Controls.Add(pildikast);

                kaart.Controls.Add(new Label
                {
                    Text =
                        $"{toode.Toodenimetus}\n" +
                        $"{toode.Hind:0.00} € | " +
                        $"Laos: {toode.Kogus} tk",
                    Size = new Size(185, 55)
                });

                kaart.Controls.Add(nupp);

                paigutus.Controls.Add(kaart);
            }
        }

        if (kategooriad.Count == 0)
        {
            tabPood.TabPages.Add(
                new TabPage(
                    "Kategooriaid pole veel lisatud"));
        }
    }

    protected override void OnFormClosed(
        FormClosedEventArgs e)
    {
        foreach (var pilt in pildid)
            pilt.Dispose();

        pildid.Clear();

        base.OnFormClosed(e);
    }
}