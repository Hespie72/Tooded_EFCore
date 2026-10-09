using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Tooded_EFCore
{


    public partial class Form2 : Form
    {
        private readonly ToodedContext _db;
        private string valitudPilt;

        public Form2()
        {
            InitializeComponent();

            _db = new ToodedContext();

            if (!Directory.Exists("Images"))
            {
                Directory.CreateDirectory("Images");
            }

            LaeKategooriad();
            LaeTooted();
        }

        // Laeme kategooriad ComboBoxi
        private void LaeKategooriad()
        {
            comboKategooria.DataSource = null;
            comboKategooria.DataSource = _db.Kategooriatabel.ToList();
            comboKategooria.DisplayMember = "Kategooria_nimetus";
            comboKategooria.ValueMember = "Id";
            comboKategooria.DropDownStyle = ComboBoxStyle.DropDown;
        }

        // Laeme tooted DataGridView-sse
        private void LaeTooted()
        {
            dataGridViewTooted.DataSource = _db.Toodetabel
                .Include(t => t.Kategooria)
                .Select(t => new
                {
                    t.Id,
                    t.Toodenimetus,
                    t.Kogus,
                    t.Hind,
                    Kategooria = t.Kategooria.Kategooria_nimetus,
                    t.Pilt
                })
                .ToList();
        }

        // Pildi valimine
        private void btnValiPilt_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Pildid|*.jpg;*.png;*.jpeg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                valitudPilt = ofd.FileName;

                using (var pilt = Image.FromFile(valitudPilt))
                {
                    picturePilt.Image?.Dispose();
                    picturePilt.Image = new Bitmap(pilt);
                }
            }
        }

        // Lisa toode
        private void btnLisa_Click(object sender, EventArgs e)
        {
            if (!KontrolliAndmeid())
                return;

            string pildiNimi = "";

            if (!string.IsNullOrEmpty(valitudPilt))
            {
                pildiNimi = Path.GetFileName(valitudPilt);

                File.Copy(
                    valitudPilt,
                    Path.Combine("Images", pildiNimi),
                    true
                );
            }

            var uus = new Toode
            {
                Toodenimetus = txtNimetus.Text,
                Kogus = int.Parse(txtKogus.Text),
                Hind = float.Parse(txtHind.Text, CultureInfo.CurrentCulture),
                KategooriaId = (int)comboKategooria.SelectedValue,
                Pilt = pildiNimi
            };

            _db.Toodetabel.Add(uus);
            _db.SaveChanges();

            LaeTooted();
            PuhastaValjad();
        }

        // DataGridView rea valimine
        private void dataGridViewTooted_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var rida = dataGridViewTooted.Rows[e.RowIndex];

            txtNimetus.Text = rida.Cells["Toodenimetus"].Value?.ToString();
            txtKogus.Text = rida.Cells["Kogus"].Value?.ToString();
            txtHind.Text = rida.Cells["Hind"].Value?.ToString();

            string pilt = rida.Cells["Pilt"].Value?.ToString();

            picturePilt.Image?.Dispose();
            picturePilt.Image = null;

            if (!string.IsNullOrEmpty(pilt))
            {
                string pildiTee = Path.Combine("Images", pilt);

                if (File.Exists(pildiTee))
                {
                    using (var img = Image.FromFile(pildiTee))
                    {
                        picturePilt.Image = new Bitmap(img);
                    }
                }
            }

            valitudPilt = null;
        }

        // Uuenda toodet
        private void btnUuenda_Click(object sender, EventArgs e)
        {
            if (dataGridViewTooted.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vali kõigepealt toode!");
                return;
            }

            if (!KontrolliAndmeid())
                return;

            int id = Convert.ToInt32(
                dataGridViewTooted.SelectedRows[0].Cells["Id"].Value
            );

            var toode = _db.Toodetabel.Find(id);

            if (toode == null)
                return;

            toode.Toodenimetus = txtNimetus.Text;
            toode.Kogus = int.Parse(txtKogus.Text);
            toode.Hind = float.Parse(txtHind.Text, CultureInfo.CurrentCulture);
            toode.KategooriaId = (int)comboKategooria.SelectedValue;

            if (!string.IsNullOrEmpty(valitudPilt))
            {
                toode.Pilt = Path.GetFileName(valitudPilt);

                File.Copy(
                    valitudPilt,
                    Path.Combine("Images", toode.Pilt),
                    true
                );
            }

            _db.SaveChanges();

            LaeTooted();
            PuhastaValjad();
        }

        // Kustuta toode
        private void btnKustuta_Click(object sender, EventArgs e)
        {
            if (dataGridViewTooted.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vali kõigepealt toode!");
                return;
            }

            int id = Convert.ToInt32(
                dataGridViewTooted.SelectedRows[0].Cells["Id"].Value
            );

            var toode = _db.Toodetabel.Find(id);

            if (toode == null)
                return;

            _db.Toodetabel.Remove(toode);
            _db.SaveChanges();

            LaeTooted();
            PuhastaValjad();
        }

        // Andmete kontrollimine
        private bool KontrolliAndmeid()
        {
            if (string.IsNullOrWhiteSpace(txtNimetus.Text))
            {
                MessageBox.Show("Sisesta toote nimetus!");
                return false;
            }

            if (!int.TryParse(txtKogus.Text, out int kogus) || kogus < 0)
            {
                MessageBox.Show("Sisesta korrektne kogus!");
                return false;
            }

            if (!float.TryParse(
                    txtHind.Text,
                    NumberStyles.Float,
                    CultureInfo.CurrentCulture,
                    out float hind) || hind < 0)
            {
                MessageBox.Show("Sisesta korrektne hind!");
                return false;
            }

            if (comboKategooria.SelectedValue == null)
            {
                MessageBox.Show("Vali kategooria!");
                return false;
            }

            return true;
        }

        // Väljade puhastamine
        private void PuhastaValjad()
        {
            txtNimetus.Clear();
            txtKogus.Clear();
            txtHind.Clear();

            picturePilt.Image?.Dispose();
            picturePilt.Image = null;

            valitudPilt = null;
            dataGridViewTooted.ClearSelection();
        }

        // Sulgeme andmebaasiühenduse
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            picturePilt.Image?.Dispose();
            _db.Dispose();

            base.OnFormClosed(e);
        }
        // Lisa uus kategooria
        private void button1_Click(object sender, EventArgs e)
        {
            Form menu = new Form();
            menu.Text = "Lisa kategooria";
            menu.Size = new Size(350, 280);
            menu.StartPosition = FormStartPosition.CenterParent;
            menu.FormBorderStyle = FormBorderStyle.FixedDialog;
            menu.MaximizeBox = false;
            menu.MinimizeBox = false;

            Label labelNimi = new Label();
            labelNimi.Text = "Kategooria nimi:";
            labelNimi.Location = new Point(20, 20);
            labelNimi.AutoSize = true;

            TextBox txtNimi = new TextBox();
            txtNimi.Location = new Point(20, 45);
            txtNimi.Width = 290;

            Label labelKirjeldus = new Label();
            labelKirjeldus.Text = "Kirjeldus:";
            labelKirjeldus.Location = new Point(20, 85);
            labelKirjeldus.AutoSize = true;

            TextBox txtKirjeldus = new TextBox();
            txtKirjeldus.Location = new Point(20, 110);
            txtKirjeldus.Width = 290;
            txtKirjeldus.Height = 50;
            txtKirjeldus.Multiline = true;

            Button btnSalvesta = new Button();
            btnSalvesta.Text = "Salvesta";
            btnSalvesta.Location = new Point(20, 180);
            btnSalvesta.Width = 100;

            btnSalvesta.Click += (s, args) =>
            {
                string nimetus = txtNimi.Text.Trim();

                if (string.IsNullOrWhiteSpace(nimetus))
                {
                    MessageBox.Show("Sisesta kategooria nimi!");
                    return;
                }

                bool olemas = _db.Kategooriatabel
                    .Any(k => k.Kategooria_nimetus == nimetus);

                if (olemas)
                {
                    MessageBox.Show("See kategooria on juba olemas!");
                    return;
                }

                var uusKategooria = new Kategooria
                {
                    Kategooria_nimetus = nimetus,
                    Kirjeldus = txtKirjeldus.Text.Trim()
                };

                _db.Kategooriatabel.Add(uusKategooria);
                _db.SaveChanges();

                LaeKategooriad();
                comboKategooria.SelectedValue = uusKategooria.Id;

                MessageBox.Show("Kategooria lisatud!");
                menu.Close();
            };

            menu.Controls.Add(labelNimi);
            menu.Controls.Add(txtNimi);
            menu.Controls.Add(labelKirjeldus);
            menu.Controls.Add(txtKirjeldus);
            menu.Controls.Add(btnSalvesta);

            menu.ShowDialog(this);
        }

    }
}
