using System.Collections.Generic;

public class PokedexEntry
{
    public DexID id;

    public string speciesName;
    public string speciesDescription = "no description given";

    public PokemonType typeA;
    public PokemonType typeB;

    public PokemonStats baseStats;

    public PokedexEntry(DexID id, string speciesName, PokemonStats baseStats, PokemonType typeA, PokemonType typeB = PokemonType.none)
    {
        this.id = id;
        this.speciesName = speciesName;
        this.baseStats = baseStats;

        this.typeA = typeA;
        this.typeB = typeB;
    }
}
