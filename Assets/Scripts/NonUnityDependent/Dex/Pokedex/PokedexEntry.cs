using System.Collections.Generic;

public class PokedexEntry
{
    public DexID id;

    public string speciesName;
    public string speciesDescription;

    public PokemonStats baseStats;

    public PokedexEntry(DexID id, string speciesName, PokemonStats baseStats)
    {
        this.id = id;
        this.speciesName = speciesName;
        this.baseStats = baseStats;
    }
}
