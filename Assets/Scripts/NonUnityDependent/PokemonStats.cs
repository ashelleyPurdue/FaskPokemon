using System.Collections.Generic;

public class PokemonStats
{
    public const int NUM_STATS = 6;

    public int attack { get; set; }
    public int defense { get; set; }
    public int spAttack { get; set; }
    public int spDefense { get; set; }
    public int speed { get; set; }
    public int maxHP { get; set; }

    public PokemonStats()
    {
        //Initialize everything to zero
        for (int i = 0; i < NUM_STATS; i++)
        {
            this[(PokemonStatID)i] = 0;
        }
    }

    public int this[PokemonStatID stat]
    {
        get
        {
            switch (stat)
            {
                case PokemonStatID.attack: return attack; break;
                case PokemonStatID.defense: return defense; break;
                case PokemonStatID.spAttack: return spAttack; break;
                case PokemonStatID.spDefense: return spDefense; break;
                case PokemonStatID.speed: return speed; break;
                case PokemonStatID.maxHP: return maxHP; break;
            }

            //Shouldn't reach here
            throw new System.Exception("Bad PokemonStatID");
        }

        set
        {
            switch (stat)
            {
                case PokemonStatID.attack: attack = value; break;
                case PokemonStatID.defense: defense = value; break;
                case PokemonStatID.spAttack: spAttack = value; break;
                case PokemonStatID.spDefense: spDefense = value; break;
                case PokemonStatID.speed: speed = value; break;
                case PokemonStatID.maxHP: maxHP = value; break;
            }
        }
    }
}

public enum PokemonStatID   //Used for programatically accessing different stats
{
    attack,
    defense,
    spAttack,
    spDefense,
    speed,
    maxHP
}
