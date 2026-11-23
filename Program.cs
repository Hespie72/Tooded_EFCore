using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Tooded_EFCore
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            string language = Properties.Settings.Default.UserLanguage;

            if (string.IsNullOrWhiteSpace(language))
                language = "et-EE"; // vaikimisi

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(language);

            var culture =
                System.Globalization.CultureInfo.GetCultureInfo(language);

            System.Globalization.CultureInfo.CurrentUICulture = culture;
            System.Globalization.CultureInfo.CurrentCulture = culture;
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

            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());
        }
    }
}