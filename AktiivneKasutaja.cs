namespace Tooded_EFCore
{
    public static class AktiivneKasutaja
    {
        public static string Roll { get; set; }
        public static string Nimi { get; set; }
        public static string Email { get; set; }

        public static bool OnAdmin => Roll == "admin";

        public static void Logout()
        {
            Roll = null;
            Nimi = null;
            Email = null;
        }
    }
}
