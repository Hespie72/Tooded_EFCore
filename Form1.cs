namespace Tooded_EFCore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnTooted_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
        }
        private void btnPood_Click(object sender, EventArgs e)
        {
            Form3 poodForm = new Form3();
            poodForm.ShowDialog();
        }
        private void btnArve_Click(object sender, EventArgs e)
        {
            ArveForm arve = new ArveForm();
            arve.ShowDialog();
        }

    }
}
