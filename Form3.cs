using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Globalization;

namespace Tooded_EFCore;

public partial class Form3 : Form
{
    private readonly List<Image> pildid = new();
    private List<Kategooria> kategooriad = new();

    private bool andmedLaetud;
    private bool languageChanging;

    private ComboBox languageSelector = null!;

    private static readonly string LanguageFile = Path.Combine(
        Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData),
        "Tooded_EFCore",
        "language.txt");

    private bool IsEnglish =>
        CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "en";

    public Form3()
    {
        InitializeComponent();

        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            return;

        InitializeLanguage();
    }

    private string T(string key, string estonian, string english)
    {
        var resources =
            new ComponentResourceManager(typeof(Form3));

        string? value = resources.GetString(
            key, CultureInfo.CurrentUICulture);

        if (IsEnglish)
        {
            var englishResources = resources.GetResourceSet(
                CultureInfo.GetCultureInfo("en"),
                true,
                false);

            value = englishResources?.GetString(key);
        }

        return value ?? (IsEnglish ? english : estonian);
    }

    private void InitializeLanguage()
    {
        languageSelector = filtrid.Controls
            .OfType<ComboBox>()
            .FirstOrDefault(c => c.Name == "comboLanguage")
            ?? new ComboBox
            {
                Name = "comboLanguage",
                Width = 110,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(29, 35, 47),
                ForeColor = Color.FromArgb(235, 239, 247),
                FlatStyle = FlatStyle.Flat
            };

        if (languageSelector.Parent == null)
            filtrid.Controls.Add(languageSelector);

        languageSelector.Items.Clear();
        languageSelector.Items.AddRange(new object[]
        {
            "Eesti",
            "English"
        });

        string language = "et-EE";

        try
        {
            if (File.Exists(LanguageFile))
                language = File.ReadAllText(LanguageFile).Trim();
        }
        catch (IOException)
        {
            language = "et-EE";
        }
        catch (UnauthorizedAccessException)
        {
            language = "et-EE";
        }

        ChangeLanguage(language, false);

        languageSelector.SelectedIndexChanged +=
            LanguageSelector_SelectedIndexChanged;
    }

    private void LanguageSelector_SelectedIndexChanged(
        object? sender, EventArgs e)
    {
        if (languageChanging)
            return;

        ChangeLanguage(
            languageSelector.SelectedIndex == 1
                ? "en-US"
                : "et-EE",
            true);
    }

    private void ChangeLanguage(string language, bool save)
    {
        language = language == "en-US" ? "en-US" : "et-EE";

        bool wasLoaded = andmedLaetud;
        int sortIndex = comboJarjestus.SelectedIndex;

        languageChanging = true;
        andmedLaetud = false;

        try
        {
            var culture = CultureInfo.GetCultureInfo(language);

            CultureInfo.CurrentUICulture = culture;
            CultureInfo.CurrentCulture = culture;

            // Меняем только тексты, не трогая оформление.
            Text = T("$this.Text", "Pood", "Store");

            lblPealkiri.Text =
                T("lblPealkiri.Text", "Pood", "Store");

            lblOtsing.Text =
                T("lblOtsing.Text", "Otsi:", "Search:");

            lblAlates.Text =
                T("lblAlates.Text", "Hind alates:", "Price from:");

            lblKuni.Text =
                T("lblKuni.Text", "kuni:", "to:");

            lblJarjestus.Text =
                T("lblJarjestus.Text", "Sorteeri:", "Sort by:");

            chkLaos.Text =
                T("chkLaos.Text", "Ainult laos", "In stock only");

            btnPuhasta.Text = T(
                "btnPuhasta.Text",
                "Puhasta filtrid",
                "Clear filters");

            btnKorv.Text = T(
                "btnKorv.Text",
                "Ostukorv ja arve",
                "Cart and invoice");

            txtOtsing.PlaceholderText = T(
                "txtOtsing.PlaceholderText",
                "Sisesta toote nimi",
                "Enter product name");

            txtHindAlates.PlaceholderText = T(
                "txtHindAlates.PlaceholderText",
                "Alates (€)",
                "From (€)");

            txtHindKuni.PlaceholderText = T(
                "txtHindKuni.PlaceholderText",
                "Kuni (€)",
                "To (€)");

            comboJarjestus.Items.Clear();
            comboJarjestus.Items.AddRange(new object[]
            {
                T("SortNameAscending", "Nimetus A–Z", "Name A–Z"),
                T("SortNameDescending", "Nimetus Z–A", "Name Z–A"),
                T("SortPriceAscending",
                    "Hind kasvavalt", "Price: low to high"),
                T("SortPriceDescending",
                    "Hind kahanevalt", "Price: high to low")
            });

            comboJarjestus.SelectedIndex =
                sortIndex >= 0 && sortIndex < 4 ? sortIndex : 0;

            languageSelector.SelectedIndex = IsEnglish ? 1 : 0;
        }
        finally
        {
            andmedLaetud = wasLoaded;
            languageChanging = false;
        }

        if (andmedLaetud)
        {
            RakendaFiltrid();
        }
        else
        {
            lblTulemus.Text = string.Format(
                T("ProductsFound",
                    "Leitud tooteid: {0}",
                    "Products found: {0}"),
                0);
        }

        if (save)
        {
            try
            {
                Directory.CreateDirectory(
                    Path.GetDirectoryName(LanguageFile)!);

                File.WriteAllText(LanguageFile, language);
            }
            catch (IOException)
            {
                ShowLanguageSaveError();
            }
            catch (UnauthorizedAccessException)
            {
                ShowLanguageSaveError();
            }
        }
    }

    private void ShowLanguageSaveError()
    {
        MessageBox.Show(
            T("LanguageSaveError",
                "Keelevaliku salvestamine ebaõnnestus.",
                "Could not save the language preference."),
            T("ErrorTitle", "Viga", "Error"));
    }

    private void Form3_Load(object? sender, EventArgs e)
    {
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
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
            ShowError(
                T("LoadError",
                    "Poe laadimine ebaõnnestus:",
                    "Failed to load the store:"),
                viga);
        }
    }

    private void ShowError(string message, Exception error)
    {
        MessageBox.Show(
            message + "\n" + error.Message,
            T("ErrorTitle", "Viga", "Error"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    private void Filtrid_Changed(object? sender, EventArgs e)
    {
        RakendaFiltrid();
    }

    private void btnPuhasta_Click(object? sender, EventArgs e)
    {
        bool wasLoaded = andmedLaetud;
        andmedLaetud = false;

        try
        {
            txtOtsing.Clear();
            txtHindAlates.Clear();
            txtHindKuni.Clear();
            comboJarjestus.SelectedIndex = 0;
            chkLaos.Checked = false;
        }
        finally
        {
            andmedLaetud = wasLoaded;
        }

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
            ShowError(
                T("InvoiceError",
                    "Arve avamine ebaõnnestus:",
                    "Failed to open the invoice:"),
                viga);
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

    private static bool LoeHind(string tekst, out decimal? hind)
    {
        hind = null;

        if (string.IsNullOrWhiteSpace(tekst))
            return true;

        string value = tekst.Trim().Replace(',', '.');

        if (!decimal.TryParse(
                value,
                NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture,
                out decimal result)
            || result < 0)
        {
            return false;
        }

        hind = result;
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
            lblTulemus.Text = T(
                "InvalidPrice",
                "Sisesta korrektne mittenegatiivne hind.",
                "Enter a valid non-negative price.");
            return;
        }

        if (alates.HasValue && kuni.HasValue && alates > kuni)
        {
            lblTulemus.ForeColor = Color.Salmon;
            lblTulemus.Text = T(
                "InvalidRange",
                "Alghind ei tohi olla lõpphinnast suurem.",
                "The minimum price cannot exceed the maximum price.");
            return;
        }

        string otsing = txtOtsing.Text.Trim();

        bool filtreeritud =
            otsing.Length > 0 ||
            alates.HasValue ||
            kuni.HasValue ||
            chkLaos.Checked;

        string? aktiivne = tabPood.SelectedTab?.Name;

        tabPood.SuspendLayout();

        try
        {
            while (tabPood.TabPages.Count > 0)
            {
                var page = tabPood.TabPages[0];
                tabPood.TabPages.Remove(page);
                page.Dispose();
            }

            foreach (var image in pildid)
                image.Dispose();

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

                var page = new TabPage(kat.Kategooria_nimetus)
                {
                    Name = kat.Id.ToString()
                };

                var layout = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true
                };

                page.Controls.Add(layout);
                tabPood.TabPages.Add(page);

                Kujundus.Rakenda(page);

                foreach (var toode in tooted)
                {
                    layout.Controls.Add(LooTooteKaart(toode));
                    arv++;
                }
            }

            if (aktiivne != null &&
                tabPood.TabPages.ContainsKey(aktiivne))
            {
                tabPood.SelectedTab =
                    tabPood.TabPages[aktiivne];
            }

            lblTulemus.ForeColor = Kujundus.Vaikne;
            lblTulemus.Text = string.Format(
                T("ProductsFound",
                    "Leitud tooteid: {0}",
                    "Products found: {0}"),
                arv);

            if (tabPood.TabPages.Count == 0)
            {
                var emptyPage = new TabPage(
                    filtreeritud
                        ? T("NoProducts",
                            "Sobivaid tooteid ei leitud",
                            "No matching products found")
                        : T("NoCategories",
                            "Kategooriaid pole veel lisatud",
                            "No categories added yet"));

                Kujundus.Rakenda(emptyPage);
                tabPood.TabPages.Add(emptyPage);
            }
        }
        finally
        {
            tabPood.ResumeLayout(true);
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

        string[] paths =
        {
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                "Tooded_EFCore",
                "Images",
                nimi),

            Path.Combine(AppContext.BaseDirectory, "Images", nimi),

            Path.Combine("Images", nimi)
        };

        string? path = paths.FirstOrDefault(File.Exists);

        try
        {
            if (path != null)
            {
                using var original = Image.FromFile(path);
                var copy = new Bitmap(original);

                pildid.Add(copy);
                pildikast.Image = copy;
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
                $"{T("Price", "Hind", "Price")}: " +
                $"{toode.Hind:0.00} €\n" +
                $"{T("Stock", "Laos", "In stock")}: " +
                $"{toode.Kogus} {T("Pieces", "tk", "pcs")}",
                T("ProductDetails", "Toote andmed", "Product details"));
        };

        var nupp = new Button
        {
            Text = "+",
            AccessibleName = string.Format(
                T("AddToCart",
                    "Lisa {0} ostukorvi",
                    "Add {0} to cart"),
                toode.Toodenimetus),
            Size = new Size(185, 32),
            Enabled = toode.Kogus > 0
        };

        nupp.Click += (s, e) =>
        {
            try
            {
                ArveForm.LisaKorvi(toode.Id);

                lblTulemus.ForeColor = Kujundus.Vaikne;
                lblTulemus.Text = string.Format(
                    T("AddedToCart",
                        "Lisatud ostukorvi: {0} (+1)",
                        "Added to cart: {0} (+1)"),
                    toode.Toodenimetus);
            }
            catch (Exception viga)
            {
                ShowError(
                    T("AddError",
                        "Toote lisamine ebaõnnestus:",
                        "Failed to add the product:"),
                    viga);
            }
        };

        kaart.Controls.Add(pildikast);

        kaart.Controls.Add(new Label
        {
            Text =
                $"{toode.Toodenimetus}\n" +
                $"{toode.Hind:0.00} € | " +
                $"{T("Stock", "Laos", "In stock")}: " +
                $"{toode.Kogus} {T("Pieces", "tk", "pcs")}",

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
        foreach (var image in pildid)
            image.Dispose();

        pildid.Clear();

        base.OnFormClosed(e);
    }
}