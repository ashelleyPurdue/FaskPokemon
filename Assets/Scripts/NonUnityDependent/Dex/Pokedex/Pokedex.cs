using System.Collections.Generic;

public static class Pokedex
{
    private static GenericDex<PokedexEntry> dex = new GenericDex<PokedexEntry>();

    static Pokedex()
    {
        //Create some pokemon

        //Missingno
        PokemonStats missingnoBaseStats = new PokemonStats
            (
                56,
                35,
                25,
                35,
                72,
                30
            );

        PokedexEntry missingno = new PokedexEntry
            (
                new DexID("", 0),
                "Missingno.",
                missingnoBaseStats,
                PokemonType.bug
            );

        AddEntry(missingno);
    }

    public static void AddEntry(PokedexEntry entry)
    {
        //Throw an exception if there's a duplicate id
        dex.AddEntry(entry.id, entry);
    }

    public static PokedexEntry GetEntry(DexID id)
    {
        return dex.GetEntry(id);
    }
}
