using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Tooded_EFCore;

public partial class Form3 : Form
{
    private readonly List<Image> pildid = new();
    private List<Kategooria> kategooriad = new();
    private bool andmedLaetud;

    public Form3()
    {
        InitializeComponent();
        comboJarjestus.SelectedIndex = 0;
    }

    private void Form3_Load(object? sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode ==
            System.ComponentModel.LicenseUsageMode.Designtime)
            return;

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
                "Poe laadimine ebaõnnestus:\n" + viga.Message,
                "Viga");
        }
    }

    private void Filtrid_Changed(object? sender, EventArgs e)
    {
        RakendaFiltrid();
    }

    private void btnPuhasta_Click(object? sender, EventArgs e)
    {
        andmedLaetud = false;

        txtOtsing.Clear();
        txtHindAlates.Clear();
        txtHindKuni.Clear();
        comboJarjestus.SelectedIndex = 0;
        chkLaos.Checked = false;

        andmedLaetud = true;
        RakendaFiltrid();
    }

    private void btnKorv_Click(object? sender, EventArgs e)
    {
        try
        {
            using var arve = new ArveForm();
            arve.ShowDialog(this);
        }
        catch (Exception viga)
        {
            MessageBox.Show(
                "Arve avamine ebaõnnestus:\n" + viga.Message,
                "Viga");
        }
    }

    private void tabPood_DrawItem(
        object? sender, DrawItemEventArgs e)
    {
        if (e.Index < 0)
            return;

        using var brush = new SolidBrush(
            tabPood.SelectedIndex == e.Index
                ? Kujundus.Aktsent
                : Kujundus.Pind);

        e.Graphics.FillRectangle(brush, e.Bounds);

        TextRenderer.DrawText(
            e.Graphics,
            tabPood.TabPages[e.Index].Text,
            tabPood.Font,
            e.Bounds,
            Kujundus.Tekst,
            TextFormatFlags.HorizontalCenter |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.EndEllipsis);
    }

    private static bool LoeHind(
        string tekst, out decimal? hind)
    {
        hind = null;

        if (string.IsNullOrWhiteSpace(tekst))
            return true;

        string vaartus = tekst.Trim().Replace(',', '.');

        if (!decimal.TryParse(
                vaartus,
                NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture,
                out decimal tulemus)
            || tulemus < 0)
            return false;

        hind = tulemus;
        return true;
    }

    private void RakendaFiltrid()
    {
        if (!andmedLaetud)
            return;

        if (!LoeHind(txtHindAlates.Text, out decimal? alates)
            || !LoeHind(txtHindKuni.Text, out decimal? kuni))
        {
            lblTulemus.ForeColor = Color.Salmon;
            lblTulemus.Text =
                "Sisesta korrektne mittenegatiivne hind.";
            return;
        }

        if (alates.HasValue && kuni.HasValue && alates > kuni)
        {
            lblTulemus.ForeColor = Color.Salmon;
            lblTulemus.Text =
                "Alghind ei tohi olla lõpphinnast suurem.";
            return;
        }

        string otsing = txtOtsing.Text.Trim();

        bool filtreeritud =
            otsing.Length > 0 ||
            alates.HasValue ||
            kuni.HasValue ||
            chkLaos.Checked;

        string? aktiivne = tabPood.SelectedTab?.Name;

        while (tabPood.TabPages.Count > 0)
        {
            var leht = tabPood.TabPages[0];
            tabPood.TabPages.Remove(leht);
            leht.Dispose();
        }

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
                        t.Hind <= (float)kuni.Value)
                    && (!chkLaos.Checked || t.Kogus > 0))
                .ToList();

            tooted = comboJarjestus.SelectedIndex switch
            {
                1 => tooted
                    .OrderByDescending(t => t.Toodenimetus)
                    .ToList(),

                2 => tooted
                    .OrderBy(t => t.Hind)
                    .ThenBy(t => t.Toodenimetus)
                    .ToList(),

                3 => tooted
                    .OrderByDescending(t => t.Hind)
                    .ThenBy(t => t.Toodenimetus)
                    .ToList(),

                _ => tooted
                    .OrderBy(t => t.Toodenimetus)
                    .ToList()
            };

            if (filtreeritud && tooted.Count == 0)
                continue;

            var leht = new TabPage(kat.Kategooria_nimetus)
            {
                Name = kat.Id.ToString()
            };

            var paigutus = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            leht.Controls.Add(paigutus);
            tabPood.TabPages.Add(leht);

            Kujundus.Rakenda(leht);

            foreach (var toode in tooted)
            {
                paigutus.Controls.Add(LooTooteKaart(toode));
                arv++;
            }
        }

        if (aktiivne != null &&
            tabPood.TabPages.ContainsKey(aktiivne))
        {
            tabPood.SelectedTab = tabPood.TabPages[aktiivne];
        }

        lblTulemus.ForeColor = Kujundus.Vaikne;
        lblTulemus.Text = $"Leitud tooteid: {arv}";

        if (tabPood.TabPages.Count == 0)
        {
            var tyhiLeht = new TabPage(
                filtreeritud
                    ? "Sobivaid tooteid ei leitud"
                    : "Kategooriaid pole veel lisatud");

            Kujundus.Rakenda(tyhiLeht);
            tabPood.TabPages.Add(tyhiLeht);
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

        string nimi = Path.GetFileName(toode.Pilt ?? "");

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
            tee = Path.Combine("Images", nimi);
        }

        try
        {
            if (File.Exists(tee))
            {
                using var algne = Image.FromFile(tee);

                var koopia = new Bitmap(algne);

                pildid.Add(koopia);
                pildikast.Image = koopia;
            }
        }
        catch (ArgumentException)
        {
            pildikast.BackColor = Color.LightGray;
        }
        catch (IOException)
        {
            pildikast.BackColor = Color.LightGray;
        }

        pildikast.Click += (s, e) =>
        {
            MessageBox.Show(
                $"{toode.Toodenimetus}\n" +
                $"Hind: {toode.Hind:0.00} €\n" +
                $"Laos: {toode.Kogus} tk",
                "Toote andmed");
        };

        var nupp = new Button
        {
            Text = "+",
            AccessibleName =
                $"Lisa {toode.Toodenimetus} ostukorvi",
            Size = new Size(185, 32),
            Enabled = toode.Kogus > 0
        };

        nupp.Click += (s, e) =>
        {
            try
            {
                ArveForm.LisaKorvi(toode.Id);

                lblTulemus.ForeColor = Kujundus.Vaikne;
                lblTulemus.Text =
                    $"Lisatud ostukorvi: {toode.Toodenimetus} (+1)";
            }
            catch (Exception viga)
            {
                MessageBox.Show(
                    "Toote lisamine ebaõnnestus:\n" + viga.Message,
                    "Viga");
            }
        };

        kaart.Controls.Add(pildikast);

        kaart.Controls.Add(new Label
        {
            Text =
                $"{toode.Toodenimetus}\n" +
                $"{toode.Hind:0.00} € | Laos: {toode.Kogus} tk",

            Size = new Size(185, 55)
        });

        kaart.Controls.Add(nupp);

        Kujundus.Rakenda(kaart);
        kaart.BackColor = Kujundus.Pind;
        Kujundus.Rohuta(nupp);

        return kaart;
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        foreach (var pilt in pildid)
            pilt.Dispose();

        pildid.Clear();

        base.OnFormClosed(e);
    }
}