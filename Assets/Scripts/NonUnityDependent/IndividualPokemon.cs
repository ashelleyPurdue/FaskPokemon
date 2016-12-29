using System.Collections;
using System.Collections.Generic;

public class IndividualPokemon
{
    public DexID species;
    public PokedexEntry pokedexEntry { get { return Pokedex.GetEntry(species); } }

    public string nickname = null;

    public string displayName
    {
        get
        {
            //If it's nicknamed, use that.  Else, use species name
            if (nickname == null)
            {
                return Pokedex.GetEntry(species).speciesName;
            }

            return nickname;
        }
    }

    public PokemonStats individualValues;
    public PokemonStats effortValues;

    public int level { get; private set; }
    public int exp { get; private set; }

    public int currentHP { get; private set; }
    public StatusCondition currentCondition { get; private set; }


    //Constructors

    public IndividualPokemon(DexID species, PokemonStats individualValues, int level)
    {
        //Basic constructor

        this.species = species;
        this.individualValues = individualValues;
        this.level = level;

        effortValues = new PokemonStats();
        exp = 0;
        currentCondition = StatusCondition.none;
    }


    //Misc methods

    public int CalculateStat(PokemonStatID stat)
    {
        //TODO: Actually use the proper formula

        return (pokedexEntry.baseStats[stat] + individualValues[stat] + effortValues[stat] / 4) * level;
    }
}
