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

    //Training info
    public PokemonStats individualValues;
    public PokemonStats effortValues;

    public int level { get; private set; }
    public int exp { get; private set; }

    public const int MAX_KNOWN_MOVES = 4;
    private List<IndividualPokemonMove> knownMoves;     //TODO: Add getters/setters 

    //Battle info
    public int currentHP { get; private set; }
    public StatusCondition currentCondition { get; private set; }


    //Constructors

    public IndividualPokemon(DexID species, PokemonStats individualValues, List<IndividualPokemonMove> knownMoves, int level)
    {
        //Basic constructor for a pokemon encountered in the wild

        this.species = species;
        this.individualValues = individualValues;
        this.knownMoves = knownMoves;
        this.level = level;

        effortValues = new PokemonStats();
        exp = 0;

        currentHP = CalculateStat(PokemonStatID.maxHP);
        currentCondition = StatusCondition.none;
    }


    //Moves

    public void LearnMove(DexID moveID)
    {
        //Learns a move

        //Throw an error if too many moves
        if (knownMoves.Count >= MAX_KNOWN_MOVES)
        {
            throw new TooManyMovesException();
        }

        //Learn the move
        IndividualPokemonMove indMove = new IndividualPokemonMove(moveID, 0);
        knownMoves.Add(indMove);
    }

    public void ForgetMove(IndividualPokemonMove move)
    {
        //Forgets a move
        knownMoves.Remove(move);
    }

    public IndividualPokemonMove GetMove(int i)
    {
        //Gets the ith move
        //If the pokemon doesn't know that many moves, returns null.

        //Return null if the pokemon doesn't know that many moves
        if (i >= knownMoves.Count)
        {
            return null;
        }

        return knownMoves[i];
    }


    //Misc methods

    public int CalculateStat(PokemonStatID stat)
    {
        //TODO: Actually use the proper formula

        return (pokedexEntry.baseStats[stat] + individualValues[stat] + effortValues[stat] / 4) * level;
    }


    //Exceptions
    public class TooManyMovesException : System.Exception { }
}

