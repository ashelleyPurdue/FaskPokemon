using System.Collections.Generic;

public static class Movedex
{
    private static GenericDex<MovedexEntry> dex = new GenericDex<MovedexEntry>();

    static Movedex()
    {
        //Add some basic moves

        //Tackle
        StandardAttackMove tackle = new StandardAttackMove
            (
                new DexID("", 1),
                "Tackle",
                "The goomba of Pokemon moves",
                1,
                PokemonType.normal,
                MoveCategory.physical,
                35,
                40,
                100
            );
        AddEntry(tackle);
    }

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
