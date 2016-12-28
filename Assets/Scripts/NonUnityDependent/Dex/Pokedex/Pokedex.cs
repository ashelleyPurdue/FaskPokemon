using System.Collections.Generic;

public static class Pokedex
{
    private static GenericDex<PokedexEntry> dex = new GenericDex<PokedexEntry>();

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
