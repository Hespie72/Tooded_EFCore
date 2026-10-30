using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Tooded_EFCore;

public partial class Form2 : Form
{
    private string? valitudPilt;
    private int? valitudId;

    public Toode? SelectedToode { get; set; }

    private static string Pildikaust => Path.Combine(
        Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData),
        "Tooded_EFCore",
        "Images");

    public Form2()
    {
        InitializeComponent();

        dataGridViewTooted.AllowUserToAddRows = false;

        comboKategooria.DropDownStyle =
            ComboBoxStyle.DropDownList;

        button1.Text = "Kategooriad";

        bool admin = AktiivneKasutaja.OnAdmin;

        btnLisa.Enabled = admin;
        btnUuenda.Enabled = admin;
        btnKustuta.Enabled = admin;
        btnValiPilt.Enabled = admin;
        button1.Enabled = admin;

        txtNimetus.ReadOnly = !admin;
        txtKogus.ReadOnly = !admin;
        txtHind.ReadOnly = !admin;

        comboKategooria.Enabled = admin;

        var arveNupp = new Button
        {
            Text = "Lisa arvele",
            Location = new Point(12, 330),
            Size = new Size(140, 35)
        };

        arveNupp.Click += btnArve_Click;

        Controls.Add(arveNupp);

        Load += (s, e) => Teosta(() =>
        {
            Directory.CreateDirectory(Pildikaust);

            LaeKategooriad();
            LaeTooted();
            PuhastaValjad();
        });
    }

    private static void Teosta(Action tegevus)
    {
        try
        {
            tegevus();
        }
        catch (Exception viga)
        {
            MessageBox.Show(
                "Toiming ebaõnnestus:\n" + viga.Message,
                "Viga");
        }
    }

    private static bool KontrolliAdmini()
    {
        if (AktiivneKasutaja.OnAdmin)
            return true;

        MessageBox.Show(
            "Seda toimingut saab teha ainult administraator.");

        return false;
    }

    private void LaeKategooriad()
    {
        using var db = new ToodedContext();

        comboKategooria.DataSource = null;

        comboKategooria.DisplayMember =
            nameof(Kategooria.Kategooria_nimetus);

        comboKategooria.ValueMember =
            nameof(Kategooria.Id);

        comboKategooria.DataSource = db.Kategooriatabel
            .AsNoTracking()
            .OrderBy(k => k.Kategooria_nimetus)
            .ToList();
    }

    private void LaeTooted()
    {
        using var db = new ToodedContext();

        dataGridViewTooted.DataSource = db.Toodetabel
            .AsNoTracking()
            .OrderBy(t => t.Toodenimetus)
            .Select(t => new
            {
                t.Id,
                t.Toodenimetus,
                t.Kogus,
                t.Hind,
                t.KategooriaId,
                Kategooria =
                    t.Kategooria.Kategooria_nimetus,
                t.Pilt
            })
            .ToList();

        dataGridViewTooted
            .Columns["KategooriaId"]
            .Visible = false;
    }

    private void dataGridViewTooted_CellClick(
        object sender,
        DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
            return;

        Teosta(() =>
        {
            var rida =
                dataGridViewTooted.Rows[e.RowIndex];

            valitudId = Convert.ToInt32(
                rida.Cells["Id"].Value);

            txtNimetus.Text =
                rida.Cells["Toodenimetus"]
                    .Value?.ToString() ?? "";

            txtKogus.Text =
                rida.Cells["Kogus"]
                    .Value?.ToString() ?? "";

            txtHind.Text =
                rida.Cells["Hind"]
                    .Value?.ToString() ?? "";

            // Taastab valitud toote tegeliku kategooria.
            comboKategooria.SelectedValue =
                Convert.ToInt32(
                    rida.Cells["KategooriaId"].Value);

            valitudPilt = null;

            string nimi = Path.GetFileName(
                rida.Cells["Pilt"].Value?.ToString() ?? "");

            string tee = Path.Combine(
                Pildikaust,
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

            AsendaPilt(null);

            if (File.Exists(tee))
            {
                using var pilt = Image.FromFile(tee);
                AsendaPilt(new Bitmap(pilt));
            }
        });
    }

    private bool LoeAndmed(
        out int kogus,
        out float hind,
        out int kategooriaId)
    {
        kogus = 0;
        hind = 0;
        kategooriaId = 0;

        if (string.IsNullOrWhiteSpace(txtNimetus.Text))
        {
            MessageBox.Show(
                "Sisesta toote nimetus!");

            return false;
        }

        if (!int.TryParse(txtKogus.Text, out kogus) ||
            kogus < 0)
        {
            MessageBox.Show(
                "Sisesta korrektne mittenegatiivne kogus!");

            return false;
        }

        if (!float.TryParse(
                txtHind.Text,
                NumberStyles.Float,
                CultureInfo.CurrentCulture,
                out hind)
            || !float.IsFinite(hind)
            || hind < 0
            || hind > 100000000)
        {
            MessageBox.Show(
                "Sisesta korrektne hind vahemikus 0 kuni 100000000!");

            return false;
        }

        if (comboKategooria.SelectedValue is not int id)
        {
            MessageBox.Show(
                "Vali kategooria!");

            return false;
        }

        kategooriaId = id;
        return true;
    }

    private void btnValiPilt_Click(
        object sender,
        EventArgs e)
    {
        if (!KontrolliAdmini())
            return;

        using var valija = new OpenFileDialog
        {
            Title = "Vali toote pilt",
            Filter = "Pildid|*.jpg;*.jpeg;*.png",
            CheckFileExists = true
        };

        if (valija.ShowDialog(this) != DialogResult.OK)
            return;

        Teosta(() =>
        {
            using var pilt =
                Image.FromFile(valija.FileName);

            AsendaPilt(new Bitmap(pilt));

            valitudPilt = valija.FileName;
        });
    }

    private void btnLisa_Click(
        object sender,
        EventArgs e)
    {
        Salvesta(false);
    }

    private void btnUuenda_Click(
        object sender,
        EventArgs e)
    {
        Salvesta(true);
    }

    private void Salvesta(bool uuenda)
    {
        if (!KontrolliAdmini())
            return;

        if (uuenda && valitudId == null)
        {
            MessageBox.Show(
                "Vali kõigepealt toode!");

            return;
        }

        if (!LoeAndmed(
                out int kogus,
                out float hind,
                out int kategooriaId))
        {
            return;
        }

        Teosta(() =>
        {
            using var db = new ToodedContext();

            if (!db.Kategooriatabel.Any(
                    k => k.Id == kategooriaId))
            {
                throw new InvalidOperationException(
                    "Kategooriat enam ei ole.");
            }

            var toode = uuenda
                ? db.Toodetabel.Find(valitudId!.Value)
                : new Toode();

            if (toode == null)
            {
                throw new InvalidOperationException(
                    "Toodet ei leitud!");
            }

            toode.Toodenimetus =
                txtNimetus.Text.Trim();

            toode.Kogus = kogus;
            toode.Hind = hind;
            toode.KategooriaId = kategooriaId;

            string? uusPilt = null;

            if (valitudPilt != null)
            {
                Directory.CreateDirectory(Pildikaust);

                uusPilt =
                    Guid.NewGuid().ToString("N")
                    + Path.GetExtension(valitudPilt)
                        .ToLowerInvariant();

                File.Copy(
                    valitudPilt,
                    Path.Combine(
                        Pildikaust,
                        uusPilt));

                toode.Pilt = uusPilt;
            }
            else if (!uuenda)
            {
                toode.Pilt = "";
            }

            if (!uuenda)
                db.Toodetabel.Add(toode);

            try
            {
                db.SaveChanges();
            }
            catch
            {
                if (uusPilt != null)
                {
                    File.Delete(
                        Path.Combine(
                            Pildikaust,
                            uusPilt));
                }

                throw;
            }

            LaeTooted();
            PuhastaValjad();
        });
    }

    private void btnKustuta_Click(
        object sender,
        EventArgs e)
    {
        if (!KontrolliAdmini())
            return;

        if (valitudId == null)
        {
            MessageBox.Show(
                "Vali kõigepealt toode!");

            return;
        }

        if (MessageBox.Show(
                "Kas kustutada valitud toode?",
                "Kinnitus",
                MessageBoxButtons.YesNo)
            != DialogResult.Yes)
        {
            return;
        }

        Teosta(() =>
        {
            using var db = new ToodedContext();

            var toode = db.Toodetabel.Find(
                valitudId.Value);

            if (toode == null)
            {
                throw new InvalidOperationException(
                    "Toodet ei leitud!");
            }

            db.Toodetabel.Remove(toode);
            db.SaveChanges();

            LaeTooted();
            PuhastaValjad();
        });
    }

    private void PuhastaValjad()
    {
        valitudId = null;
        valitudPilt = null;
        SelectedToode = null;

        txtNimetus.Clear();
        txtKogus.Clear();
        txtHind.Clear();

        comboKategooria.SelectedIndex = -1;

        dataGridViewTooted.ClearSelection();

        AsendaPilt(null);
    }

    private void AsendaPilt(Image? pilt)
    {
        var eelmine = picturePilt.Image;

        picturePilt.Image = pilt;

        eelmine?.Dispose();
    }

    // Kategooriate haldus avaneb olemasoleva nupu kaudu.
    private void button1_Click(
        object sender,
        EventArgs e)
    {
        if (!KontrolliAdmini())
            return;

        Teosta(() =>
        {
            using var aken = new Form
            {
                Text = "Kategooriate haldus",
                ClientSize = new Size(640, 350),
                StartPosition =
                    FormStartPosition.CenterParent,
                FormBorderStyle =
                    FormBorderStyle.FixedDialog,
                MaximizeBox = false
            };

            var loend = new ListBox
            {
                Location = new Point(15, 15),
                Size = new Size(230, 315),
                DisplayMember =
                    nameof(Kategooria.Kategooria_nimetus)
            };

            var nimi = new TextBox
            {
                Location = new Point(265, 45),
                Width = 350
            };

            var kirjeldus = new TextBox
            {
                Location = new Point(265, 110),
                Size = new Size(350, 100),
                Multiline = true
            };

            aken.Controls.AddRange(new Control[]
            {
                loend,
                nimi,
                kirjeldus,

                new Label
                {
                    Text = "Kategooria nimi:",
                    AutoSize = true,
                    Location = new Point(265, 20)
                },

                new Label
                {
                    Text = "Kirjeldus:",
                    AutoSize = true,
                    Location = new Point(265, 85)
                }
            });

            void Lae()
            {
                using var db = new ToodedContext();

                loend.DataSource = db.Kategooriatabel
                    .AsNoTracking()
                    .OrderBy(k => k.Kategooria_nimetus)
                    .ToList();

                loend.SelectedIndex = -1;

                nimi.Clear();
                kirjeldus.Clear();
            }

            loend.SelectedIndexChanged += (s, args) =>
            {
                if (loend.SelectedItem is not Kategooria kat)
                    return;

                nimi.Text = kat.Kategooria_nimetus;
                kirjeldus.Text = kat.Kirjeldus;
            };

            void SalvestaKategooria(bool uuenda)
            {
                if (!KontrolliAdmini())
                    return;

                string nimetus = nimi.Text.Trim();

                if (nimetus.Length == 0)
                {
                    throw new InvalidOperationException(
                        "Sisesta kategooria nimi!");
                }

                var valitud =
                    loend.SelectedItem as Kategooria;

                if (uuenda && valitud == null)
                {
                    throw new InvalidOperationException(
                        "Vali kategooria!");
                }

                using var db = new ToodedContext();

                int id = uuenda
                    ? valitud!.Id
                    : 0;

                bool olemas = db.Kategooriatabel.Any(
                    k => k.Kategooria_nimetus == nimetus
                         && k.Id != id);

                if (olemas)
                {
                    throw new InvalidOperationException(
                        "Sama nimega kategooria on juba olemas!");
                }

                var kat = uuenda
                    ? db.Kategooriatabel.Find(id)
                    : new Kategooria();

                if (kat == null)
                {
                    throw new InvalidOperationException(
                        "Kategooriat ei leitud!");
                }

                kat.Kategooria_nimetus = nimetus;
                kat.Kirjeldus = kirjeldus.Text.Trim();

                if (!uuenda)
                    db.Kategooriatabel.Add(kat);

                db.SaveChanges();

                Lae();
            }

            void LisaNupp(
                string tekst,
                int x,
                Action tegevus)
            {
                var nupp = new Button
                {
                    Text = tekst,
                    Location = new Point(x, 240),
                    Size = new Size(105, 35)
                };

                nupp.Click += (s, args) =>
                    Teosta(tegevus);

                aken.Controls.Add(nupp);
            }

            LisaNupp(
                "Lisa",
                265,
                () => SalvestaKategooria(false));

            LisaNupp(
                "Uuenda",
                380,
                () => SalvestaKategooria(true));

            LisaNupp("Kustuta", 495, () =>
            {
                if (!KontrolliAdmini())
                    return;

                if (loend.SelectedItem is not Kategooria valitud)
                {
                    throw new InvalidOperationException(
                        "Vali kategooria!");
                }

                using var db = new ToodedContext();

                if (db.Toodetabel.Any(
                        t => t.KategooriaId == valitud.Id))
                {
                    throw new InvalidOperationException(
                        "Kategoorias on tooteid. " +
                        "Muuda esmalt nende kategooriat.");
                }

                if (MessageBox.Show(
                        "Kas kustutada kategooria?",
                        "Kinnitus",
                        MessageBoxButtons.YesNo)
                    != DialogResult.Yes)
                {
                    return;
                }

                var kat = db.Kategooriatabel.Find(
                    valitud.Id);

                if (kat != null)
                {
                    db.Kategooriatabel.Remove(kat);
                    db.SaveChanges();
                }

                Lae();
            });

            Lae();

            aken.ShowDialog(this);

            LaeKategooriad();
            LaeTooted();
            PuhastaValjad();
        });
    }

    private void btnArve_Click(
        object sender,
        EventArgs e)
    {
        Teosta(() =>
        {
            using var db = new ToodedContext();

            var toode = valitudId.HasValue
                ? db.Toodetabel
                    .AsNoTracking()
                    .FirstOrDefault(
                        t => t.Id == valitudId.Value)
                : null;

            using var arve = new ArveForm
            {
                SelectedToode = toode
            };

            arve.ShowDialog(this);
        });
    }

    protected override void OnFormClosed(
        FormClosedEventArgs e)
    {
        AsendaPilt(null);

        base.OnFormClosed(e);
    }
}