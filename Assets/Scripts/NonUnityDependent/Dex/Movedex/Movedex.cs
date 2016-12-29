using System.Collections.Generic;

public static class Movedex
{
    private static GenericDex<MovedexEntry> dex;

    public static void AddEntry(MovedexEntry entry)
    {
        //Throw an exception if there's a duplicate id
        dex.AddEntry(entry.id, entry);
    }

    public static MovedexEntry GetEntry(DexID id)
    {
        return dex.GetEntry(id);
    }
}
