namespace Tooded_EFCore;

public static class AktiivneKasutaja
{
    public static string Roll { get; set; } = "";
    public static string Nimi { get; set; } = "";
    public static string Email { get; set; } = "";

    public static bool OnAdmin => Roll == "admin";

    public static void Logout()
    {
        Roll = Nimi = Email = "";
        ArveForm.TuhjendaKorv();
    }
}

// Ühine kujundus asub olemasolevas failis.
internal static class Kujundus
{
    public static readonly Color Taust =
        Color.FromArgb(18, 22, 30);

    public static readonly Color Pind =
        Color.FromArgb(29, 35, 47);

    public static readonly Color Tekst =
        Color.FromArgb(235, 239, 247);

    public static readonly Color Vaikne =
        Color.FromArgb(158, 171, 193);

    public static readonly Color Aktsent =
        Color.FromArgb(77, 130, 245);

    private static readonly Color Piir =
        Color.FromArgb(52, 63, 82);

    private static readonly Font Tavatekst =
        new("Segoe UI", 10);

    private static readonly Font Suurtekst =
        new("Segoe UI", 19, FontStyle.Bold);

    public static FlowLayoutPanel Veerg() => new()
    {
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.TopDown,
        WrapContents = false,
        AutoScroll = true,
        Padding = new Padding(20)
    };

    public static Label Pealkiri(string tekst) => new()
    {
        Text = tekst,
        AutoSize = true,
        Font = Suurtekst,
        Margin = new Padding(3, 3, 3, 16)
    };

    public static void LisaVali(
        FlowLayoutPanel paneel,
        string nimi,
        Control vali)
    {
        paneel.Controls.Add(new Label
        {
            Text = nimi,
            AutoSize = true,
            Margin = new Padding(3, 10, 3, 5)
        });

        vali.Width = 280;
        paneel.Controls.Add(vali);
    }

    public static Button Nupp(
        string tekst,
        EventHandler tegevus)
    {
        var nupp = new Button
        {
            Text = tekst,
            Size = new Size(150, 38)
        };

        nupp.Click += tegevus;

        return nupp;
    }

    public static void Rohuta(Button nupp)
    {
        nupp.BackColor = Aktsent;
    }

    public static void Rakenda(Control juht)
    {
        juht.BackColor = Taust;
        juht.ForeColor = Tekst;

        switch (juht)
        {
            case Form vorm:
                vorm.Font = Tavatekst;
                vorm.StartPosition =
                    FormStartPosition.CenterScreen;
                break;

            case Button nupp:
                nupp.FlatStyle = FlatStyle.Flat;
                nupp.FlatAppearance.BorderColor = Piir;
                nupp.FlatAppearance.BorderSize = 1;

                nupp.FlatAppearance.MouseOverBackColor =
                    Color.FromArgb(48, 65, 94);

                nupp.BackColor = Pind;
                nupp.Cursor = Cursors.Hand;
                break;

            case TextBox vali:
                vali.BackColor = Pind;
                vali.BorderStyle = BorderStyle.FixedSingle;
                break;

            case ComboBox valik:
                valik.BackColor = Pind;
                valik.FlatStyle = FlatStyle.Flat;
                break;

            case ListBox loend:
                loend.BackColor = Pind;
                loend.BorderStyle = BorderStyle.FixedSingle;
                break;

            case PictureBox pilt:
                pilt.BackColor = Pind;
                break;

            case Label silt:
                silt.BackColor = Color.Transparent;
                break;

            case DataGridView tabel:
                tabel.BackgroundColor = Pind;
                tabel.BorderStyle = BorderStyle.None;
                tabel.EnableHeadersVisualStyles = false;
                tabel.RowHeadersVisible = false;
                tabel.GridColor = Piir;

                tabel.ColumnHeadersHeight = 40;
                tabel.RowTemplate.Height = 35;

                tabel.ColumnHeadersDefaultCellStyle.BackColor =
                    Piir;

                tabel.ColumnHeadersDefaultCellStyle.ForeColor =
                    Tekst;

                tabel.DefaultCellStyle.BackColor = Pind;
                tabel.DefaultCellStyle.ForeColor = Tekst;

                tabel.DefaultCellStyle.SelectionBackColor =
                    Color.FromArgb(44, 72, 117);

                tabel.DefaultCellStyle.SelectionForeColor =
                    Color.White;

                tabel.AlternatingRowsDefaultCellStyle.BackColor =
                    Color.FromArgb(24, 30, 41);
                break;

            case TabControl vahelehed:
                vahelehed.DrawMode =
                    TabDrawMode.OwnerDrawFixed;

                vahelehed.SizeMode =
                    TabSizeMode.Fixed;

                vahelehed.ItemSize =
                    new Size(160, 36);

                vahelehed.DrawItem -= JoonistaVahekaart;
                vahelehed.DrawItem += JoonistaVahekaart;
                break;
        }

        foreach (Control laps in juht.Controls)
        {
            Rakenda(laps);
        }
    }

    private static void JoonistaVahekaart(
        object? saatja,
        DrawItemEventArgs e)
    {
        if (saatja is not TabControl vahelehed ||
            e.Index < 0)
        {
            return;
        }

        using var pintsel = new SolidBrush(
            vahelehed.SelectedIndex == e.Index
                ? Aktsent
                : Pind);

        e.Graphics.FillRectangle(
            pintsel,
            e.Bounds);

        TextRenderer.DrawText(
            e.Graphics,
            vahelehed.TabPages[e.Index].Text,
            vahelehed.Font,
            e.Bounds,
            Tekst,
            TextFormatFlags.HorizontalCenter |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.EndEllipsis);
    }
}