using Microsoft.EntityFrameworkCore;

namespace Tooded_EFCore
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            try
            {
                using (var db = new ToodedContext())
                {
                    db.Database.Migrate();
                }

                AndmeteAlgatus.LooKasutajad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Andmebaasi ettevalmistamine ebaõnnestus:\n" + ex.Message,
                    "Viga",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            Application.Run(new LoginForm());
        }
    }
}