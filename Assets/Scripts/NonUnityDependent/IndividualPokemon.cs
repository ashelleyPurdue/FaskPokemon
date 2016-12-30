using System.Collections;
using System.Collections.Generic;

public class IndividualPokemon
{
    //Events
    public delegate void PokemonFaintedHandler(IndividualPokemon pokemon);
    public static event PokemonFaintedHandler OnPokemonFainted;

    //Fields
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
    public PokemonStats speciesBaseStats { get { return pokedexEntry.baseStats; } }
    public PokemonStats individualValues;
    public PokemonStats effortValues;

    public int attack { get { return CalculateStat(PokemonStatID.attack); } }
    public int defense { get { return CalculateStat(PokemonStatID.defense); } }
    public int spAttack { get { return CalculateStat(PokemonStatID.spAttack); } }
    public int spDefense { get { return CalculateStat(PokemonStatID.spDefense); } }
    public int speed { get { return CalculateStat(PokemonStatID.speed); } }
    public int maxHP { get { return CalculateStat(PokemonStatID.maxHP); } }

    public int level { get; private set; }
    public int exp { get; private set; }

    public const int MAX_KNOWN_MOVES = 4;
    private List<IndividualPokemonMove> knownMoves;

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

    public void ChangeHP(int delta)
    {
        //Heals/takes damage by the given amount

        currentHP += delta;

        //Cap at max hp
        int maxHP = CalculateStat(PokemonStatID.maxHP);
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        //Faint if zero
        if (currentHP <= 0)
        {
            currentHP = 0;
            OnPokemonFainted(this);
        }
    }

    public int CalculateStat(PokemonStatID stat)
    {
        //Calculates the pokemon's current stat

        //If it's maxHP, use the HP formula
        if (stat == PokemonStatID.maxHP)
        {
            return CalculateMaxHP(speciesBaseStats.maxHP, individualValues.maxHP, effortValues.maxHP, level);
        }

        //Use the normal formula
        //TODO: Determine nature multiplier
        float natureMult = 1f;
        return CalculateNonHPStat(speciesBaseStats[stat], individualValues[stat], effortValues[stat], natureMult, level);
    }

    public static int CalculateNonHPStat(int baseStat, int iv, int ev, float natureMult, int level)
    {
        //Uses the stat formula to calculate a non-hp stat
        //Taken from Bulbapedia

        int beforeNature = 2 * baseStat + iv + (ev / 4);
        beforeNature *= level;
        beforeNature /= 100;
        beforeNature += 5;

        float afterNature = (float)beforeNature * natureMult;
        return (int)afterNature;
    }

    public static int CalculateMaxHP(int baseStat, int iv, int ev, int level)
    {
        //Calculates max HP
        //Taken from Bulbapedia

        int result = 2 * baseStat + iv + (ev / 4);
        result *= level;
        result /= 100;
        result += level + 10;

        return result;
    }

    //Exceptions
    public class TooManyMovesException : System.Exception { }
}

