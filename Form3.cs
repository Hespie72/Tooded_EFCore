using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Tooded_EFCore;

public partial class Form3 : Form
{
    private readonly List<Image> pildid = new();
    private List<Kategooria> kategooriad = new();
    private bool andmedLaetud;

    private readonly TextBox txtOtsing = new()
    {
        Width = 220,
        PlaceholderText = "Sisesta toote nimi"
    };

    private readonly TextBox txtHindAlates = new()
    {
        Width = 90,
        PlaceholderText = "Alates (€)"
    };

    private readonly TextBox txtHindKuni = new()
    {
        Width = 90,
        PlaceholderText = "Kuni (€)"
    };

    private readonly Label lblTulemus = new()
    {
        Dock = DockStyle.Fill,
        AutoSize = false,
        Padding = new Padding(10, 0, 0, 0)
    };

    public Form3()
    {
        InitializeComponent();
        LisaFiltrid();

        Load += (s, e) =>
        {
            try
            {
                using var db = new ToodedContext();

                kategooriad = db.Kategooriatabel
                    .AsNoTracking()
                    .Include(k => k.Tooded)
                    .OrderBy(k => k.Kategooria_nimetus)
                    .ToList();

                andmedLaetud = true;

                RakendaFiltrid();
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

    private void LisaFiltrid()
    {
        var filtrid = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(8),
            WrapContents = true
        };

        Label Silt(string tekst) => new()
        {
            Text = tekst,
            AutoSize = true,
            Margin = new Padding(3, 7, 3, 3)
        };

        var puhastaNupp = new Button
        {
            Text = "Puhasta filtrid",
            AutoSize = true
        };

        puhastaNupp.Click += (s, e) =>
        {
            // Väldib vaate korduvat uuendamist väljade puhastamisel.
            andmedLaetud = false;

            txtOtsing.Clear();
            txtHindAlates.Clear();
            txtHindKuni.Clear();

            andmedLaetud = true;

            RakendaFiltrid();
        };

        filtrid.Controls.AddRange(new Control[]
        {
            Silt("Otsi:"),
            txtOtsing,

            Silt("Hind alates:"),
            txtHindAlates,

            Silt("kuni:"),
            txtHindKuni,

            puhastaNupp
        });

        var paigutus = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3
        };

        paigutus.ColumnStyles.Add(
            new ColumnStyle(SizeType.Percent, 100));

        paigutus.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 75));

        paigutus.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 28));

        paigutus.RowStyles.Add(
            new RowStyle(SizeType.Percent, 100));

        // Paigutab olemasoleva kategooriate vaate filtrite alla.
        Controls.Remove(tabPood);

        tabPood.Dock = DockStyle.Fill;

        paigutus.Controls.Add(filtrid, 0, 0);
        paigutus.Controls.Add(lblTulemus, 0, 1);
        paigutus.Controls.Add(tabPood, 0, 2);

        Controls.Add(paigutus);

        txtOtsing.TextChanged +=
            (s, e) => RakendaFiltrid();

        txtHindAlates.TextChanged +=
            (s, e) => RakendaFiltrid();

        txtHindKuni.TextChanged +=
            (s, e) => RakendaFiltrid();
    }

    private static bool LoeHind(
        string tekst,
        out decimal? hind)
    {
        hind = null;

        // Tühi väli tähendab, et vastavat hinnapiiri ei rakendata.
        if (string.IsNullOrWhiteSpace(tekst))
            return true;

        // Lubab kümnendmärgina nii koma kui ka punkti.
        string vaartus = tekst
            .Trim()
            .Replace(',', '.');

        if (!decimal.TryParse(
                vaartus,
                NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture,
                out decimal tulemus)
            || tulemus < 0)
        {
            return false;
        }

        hind = tulemus;

        return true;
    }

    private void RakendaFiltrid()
    {
        if (!andmedLaetud)
            return;

        if (!LoeHind(
                txtHindAlates.Text,
                out decimal? alates)
            || !LoeHind(
                txtHindKuni.Text,
                out decimal? kuni))
        {
            lblTulemus.ForeColor = Color.Firebrick;

            lblTulemus.Text =
                "Sisesta korrektne mittenegatiivne hind.";

            return;
        }

        if (alates.HasValue &&
            kuni.HasValue &&
            alates > kuni)
        {
            lblTulemus.ForeColor = Color.Firebrick;

            lblTulemus.Text =
                "Alghind ei tohi olla lõpphinnast suurem.";

            return;
        }

        string otsing = txtOtsing.Text.Trim();

        bool filtreeritud =
            otsing.Length > 0 ||
            alates.HasValue ||
            kuni.HasValue;

        // Eemaldab eelmise vaate.
        while (tabPood.TabPages.Count > 0)
        {
            var leht = tabPood.TabPages[0];

            tabPood.TabPages.Remove(leht);
            leht.Dispose();
        }

        // Vabastab eelmises vaates kasutatud pildid.
        foreach (var pilt in pildid)
            pilt.Dispose();

        pildid.Clear();

        int arv = 0;

        foreach (var kat in kategooriad)
        {
            var tooted = kat.Tooded
                .Where(t =>
                    (t.Toodenimetus ?? "").Contains(
                        otsing,
                        StringComparison.CurrentCultureIgnoreCase)

                    && (!alates.HasValue ||
                        t.Hind >= (float)alates.Value)

                    && (!kuni.HasValue ||
                        t.Hind <= (float)kuni.Value))
                .OrderBy(t => t.Toodenimetus)
                .ToList();

            // Filtreerimisel peidab sobivate toodeteta kategooriad.
            if (filtreeritud && tooted.Count == 0)
                continue;

            var leht = new TabPage(
                kat.Kategooria_nimetus);

            var paigutus = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            leht.Controls.Add(paigutus);
            tabPood.TabPages.Add(leht);

            foreach (var toode in tooted)
            {
                paigutus.Controls.Add(
                    LooTooteKaart(toode));

                arv++;
            }
        }

        lblTulemus.ForeColor =
            SystemColors.ControlText;

        lblTulemus.Text =
            $"Leitud tooteid: {arv}";

        if (tabPood.TabPages.Count == 0)
        {
            tabPood.TabPages.Add(new TabPage(
                filtreeritud
                    ? "Sobivaid tooteid ei leitud"
                    : "Kategooriaid pole veel lisatud"));
        }
    }

    private Control LooTooteKaart(Toode toode)
    {
        var kaart = new FlowLayoutPanel
        {
            Size = new Size(200, 280),
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            BorderStyle = BorderStyle.FixedSingle,
            Padding = new Padding(5)
        };

        var pildikast = new PictureBox
        {
            Size = new Size(185, 150),
            SizeMode = PictureBoxSizeMode.Zoom
        };

        string nimi = Path.GetFileName(
            toode.Pilt ?? "");

        string tee = Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
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

        return kaart;
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