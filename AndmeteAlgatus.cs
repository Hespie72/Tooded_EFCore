using System.Linq;

namespace Tooded_EFCore
{
    // Loob esimesel käivitamisel admin- ja testkasutaja (7.3)
    public static class AndmeteAlgatus
    {
        public static void LooKasutajad()
        {
            using var db = new ToodedContext();

            if (db.Kasutajad.Any())
                return;

            db.Kasutajad.Add(new Kasutaja
            {
                Nimi = "Admin",
                Email = "admin@admin.ee",
                ParoolHash = PasswordHelper.Hash("admin123"),
                Roll = "admin"
            });

            db.Kasutajad.Add(new Kasutaja
            {
                Nimi = "Kasutaja",
                Email = "kasutaja@kasutaja.ee",
                ParoolHash = PasswordHelper.Hash("kasutaja123"),
                Roll = "kasutaja"
            });

            db.SaveChanges();
        }
    }
}
