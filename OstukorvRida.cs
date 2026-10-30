namespace Tooded_EFCore;

public class OstukorviRida
{
    public Toode Toode { get; set; } = null!;

    public int Kogus { get; set; }

    // Andmebaasi hinnatüüp säilib.
    // Rahasumma arvutatakse decimal-tüübiga.
    public decimal Summa =>
        Math.Round(
            (decimal)Toode.Hind * Kogus,
            2);

    public override string ToString()
    {
        return
            $"{Toode.Toodenimetus} — " +
            $"{Kogus} tk × " +
            $"{Toode.Hind:0.00} € = " +
            $"{Summa:0.00} €";
    }
}