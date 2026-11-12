#nullable disable

using System.Drawing;
using System.Windows.Forms;

namespace Tooded_EFCore
{
    partial class Form3
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            paigutus = new TableLayoutPanel();
            lblPealkiri = new Label();
            filtrid = new FlowLayoutPanel();
            lblOtsing = new Label();
            txtOtsing = new TextBox();
            lblAlates = new Label();
            txtHindAlates = new TextBox();
            lblKuni = new Label();
            txtHindKuni = new TextBox();
            lblJarjestus = new Label();
            comboJarjestus = new ComboBox();
            chkLaos = new CheckBox();
            btnPuhasta = new Button();
            btnKorv = new Button();
            lblTulemus = new Label();
            tabPood = new TabControl();
            paigutus.SuspendLayout();
            filtrid.SuspendLayout();
            SuspendLayout();
            // 
            // paigutus
            // 
            resources.ApplyResources(paigutus, "paigutus");
            paigutus.Controls.Add(lblPealkiri, 0, 0);
            paigutus.Controls.Add(filtrid, 0, 1);
            paigutus.Controls.Add(lblTulemus, 0, 2);
            paigutus.Controls.Add(tabPood, 0, 3);
            paigutus.Name = "paigutus";
            // 
            // lblPealkiri
            // 
            resources.ApplyResources(lblPealkiri, "lblPealkiri");
            lblPealkiri.BackColor = Color.Transparent;
            lblPealkiri.Name = "lblPealkiri";
            // 
            // filtrid
            // 
            resources.ApplyResources(filtrid, "filtrid");
            filtrid.Controls.Add(lblOtsing);
            filtrid.Controls.Add(txtOtsing);
            filtrid.Controls.Add(lblAlates);
            filtrid.Controls.Add(txtHindAlates);
            filtrid.Controls.Add(lblKuni);
            filtrid.Controls.Add(txtHindKuni);
            filtrid.Controls.Add(lblJarjestus);
            filtrid.Controls.Add(comboJarjestus);
            filtrid.Controls.Add(chkLaos);
            filtrid.Controls.Add(btnPuhasta);
            filtrid.Controls.Add(btnKorv);
            filtrid.Name = "filtrid";
            // 
            // lblOtsing
            // 
            resources.ApplyResources(lblOtsing, "lblOtsing");
            lblOtsing.BackColor = Color.Transparent;
            lblOtsing.Name = "lblOtsing";
            // 
            // txtOtsing
            // 
            resources.ApplyResources(txtOtsing, "txtOtsing");
            txtOtsing.BackColor = Color.FromArgb(29, 35, 47);
            txtOtsing.BorderStyle = BorderStyle.FixedSingle;
            txtOtsing.ForeColor = Color.FromArgb(235, 239, 247);
            txtOtsing.Name = "txtOtsing";
            txtOtsing.TextChanged += Filtrid_Changed;
            // 
            // lblAlates
            // 
            resources.ApplyResources(lblAlates, "lblAlates");
            lblAlates.BackColor = Color.Transparent;
            lblAlates.Name = "lblAlates";
            // 
            // txtHindAlates
            // 
            resources.ApplyResources(txtHindAlates, "txtHindAlates");
            txtHindAlates.BackColor = Color.FromArgb(29, 35, 47);
            txtHindAlates.BorderStyle = BorderStyle.FixedSingle;
            txtHindAlates.ForeColor = Color.FromArgb(235, 239, 247);
            txtHindAlates.Name = "txtHindAlates";
            txtHindAlates.TextChanged += Filtrid_Changed;
            // 
            // lblKuni
            // 
            resources.ApplyResources(lblKuni, "lblKuni");
            lblKuni.BackColor = Color.Transparent;
            lblKuni.Name = "lblKuni";
            // 
            // txtHindKuni
            // 
            resources.ApplyResources(txtHindKuni, "txtHindKuni");
            txtHindKuni.BackColor = Color.FromArgb(29, 35, 47);
            txtHindKuni.BorderStyle = BorderStyle.FixedSingle;
            txtHindKuni.ForeColor = Color.FromArgb(235, 239, 247);
            txtHindKuni.Name = "txtHindKuni";
            txtHindKuni.TextChanged += Filtrid_Changed;
            // 
            // lblJarjestus
            // 
            resources.ApplyResources(lblJarjestus, "lblJarjestus");
            lblJarjestus.BackColor = Color.Transparent;
            lblJarjestus.Name = "lblJarjestus";
            // 
            // comboJarjestus
            // 
            resources.ApplyResources(comboJarjestus, "comboJarjestus");
            comboJarjestus.BackColor = Color.FromArgb(29, 35, 47);
            comboJarjestus.DropDownStyle = ComboBoxStyle.DropDownList;
            comboJarjestus.ForeColor = Color.FromArgb(235, 239, 247);
            comboJarjestus.Items.AddRange(new object[] { resources.GetString("comboJarjestus.Items"), resources.GetString("comboJarjestus.Items1"), resources.GetString("comboJarjestus.Items2"), resources.GetString("comboJarjestus.Items3") });
            comboJarjestus.Name = "comboJarjestus";
            comboJarjestus.SelectedIndexChanged += Filtrid_Changed;
            // 
            // chkLaos
            // 
            resources.ApplyResources(chkLaos, "chkLaos");
            chkLaos.Name = "chkLaos";
            chkLaos.CheckedChanged += Filtrid_Changed;
            // 
            // btnPuhasta
            // 
            resources.ApplyResources(btnPuhasta, "btnPuhasta");
            btnPuhasta.BackColor = Color.FromArgb(29, 35, 47);
            btnPuhasta.Cursor = Cursors.Hand;
            btnPuhasta.FlatAppearance.BorderColor = Color.FromArgb(52, 63, 82);
            btnPuhasta.FlatAppearance.MouseOverBackColor = Color.FromArgb(48, 65, 94);
            btnPuhasta.ForeColor = Color.FromArgb(235, 239, 247);
            btnPuhasta.Name = "btnPuhasta";
            btnPuhasta.UseVisualStyleBackColor = false;
            btnPuhasta.Click += btnPuhasta_Click;
            // 
            // btnKorv
            // 
            resources.ApplyResources(btnKorv, "btnKorv");
            btnKorv.BackColor = Color.FromArgb(29, 35, 47);
            btnKorv.Cursor = Cursors.Hand;
            btnKorv.FlatAppearance.BorderColor = Color.FromArgb(52, 63, 82);
            btnKorv.FlatAppearance.MouseOverBackColor = Color.FromArgb(48, 65, 94);
            btnKorv.ForeColor = Color.FromArgb(235, 239, 247);
            btnKorv.Name = "btnKorv";
            btnKorv.UseVisualStyleBackColor = false;
            btnKorv.Click += btnKorv_Click;
            // 
            // lblTulemus
            // 
            resources.ApplyResources(lblTulemus, "lblTulemus");
            lblTulemus.BackColor = Color.Transparent;
            lblTulemus.ForeColor = Color.FromArgb(158, 171, 193);
            lblTulemus.Name = "lblTulemus";
            // 
            // tabPood
            // 
            resources.ApplyResources(tabPood, "tabPood");
            tabPood.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabPood.Name = "tabPood";
            tabPood.SelectedIndex = 0;
            tabPood.SizeMode = TabSizeMode.Fixed;
            tabPood.DrawItem += tabPood_DrawItem;
            // 
            // Form3
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 22, 30);
            Controls.Add(paigutus);
            ForeColor = Color.FromArgb(235, 239, 247);
            Name = "Form3";
            Load += Form3_Load;
            paigutus.ResumeLayout(false);
            paigutus.PerformLayout();
            filtrid.ResumeLayout(false);
            filtrid.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel paigutus;
        private Label lblPealkiri;
        private FlowLayoutPanel filtrid;
        private Label lblOtsing;
        private TextBox txtOtsing;
        private Label lblAlates;
        private TextBox txtHindAlates;
        private Label lblKuni;
        private TextBox txtHindKuni;
        private Label lblJarjestus;
        private ComboBox comboJarjestus;
        private CheckBox chkLaos;
        private Button btnPuhasta;
        private Button btnKorv;
        private Label lblTulemus;
        private TabControl tabPood;
    }
}