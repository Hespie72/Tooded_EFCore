using Microsoft.EntityFrameworkCore;

namespace Tooded_EFCore
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            string language = Properties.Settings.Default.UserLanguage;

            if (language != "en-US" && language != "et-EE")
                language = "et-EE";

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

            Application.Run(new LoginForm());
        }
    }
}