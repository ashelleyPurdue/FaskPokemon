using System;

public class StandardAttackMove : MovedexEntry
{
    private const float CRIT_MULTIPLIER = 2;
    private const float STAB_MULTIPLIER = 1.5f;

    private const float MIN_RAND_MULTIPLIER = 0.85f;
    private const float MAX_RAND_MULTIPLIER = 1;

    private static System.Random randGen = new System.Random();

    public int power;
    public int accuracy;        //For reference, most moves have 100 accuracy

    public StandardAttackMove(DexID id, string moveName, string moveDescription, int genericAnimationID, PokemonType type, MoveCategory moveCategory, int basePP, int power, int accuracy)
        : base(id, moveName, moveDescription, genericAnimationID, type, moveCategory, basePP)
    {
        this.power = power;
        this.accuracy = accuracy;
    }


    //Interface

    public override void Use(IndividualPokemon user, IndividualPokemon target)
    {
        //Deals damage to the target based on the damage formula

        //Determine the random multiplier
        float randMult = RandomBetween(MIN_RAND_MULTIPLIER, MAX_RAND_MULTIPLIER);

        //TODO: Determine if it's a crit
        bool crit = false;

        //Determine how much damage to deal
        int damage = CalculateDamage(user, target, randMult, crit);

        //Deal the damage
        target.ChangeHP(damage * -1);
    }


    //Misc methods

    private int CalculateDamage(IndividualPokemon user, IndividualPokemon target, float randomMultiplier, bool crit = false, float extraModifier = 1)
    {
        //Calculates the damange this move will do

        float modifier = CalculateDamageModifier(user, target, crit) * randomMultiplier * extraModifier;

        //Choose the appropriate attack/defense stats
        float attack = user.attack;
        float defense = target.defense;
        
        if (moveCategory == MoveCategory.special)
        {
            attack = user.spAttack;
            defense = target.spDefense;
        }

        //Do the calculations
        float damageUnrounded = ((2f * user.level + 10) / 250) * (attack / defense) * power + 2;
        damageUnrounded *= modifier;

        int damage = (int)damageUnrounded;

        //Always do atleast 1 damage, unless the multiplier is zero
        if (damage == 0 && modifier != 0f)
        {
            damage = 1;
        }

        return damage;
    }

    private float CalculateDamageModifier(IndividualPokemon user, IndividualPokemon target, bool crit = false)
    {
        //Calculates the damage modifier, before the random modifier is applied

        float modifier = 1;

        //Apply same-type attack bonus
        if (type == user.pokedexEntry.typeA || type == user.pokedexEntry.typeB)
        {
            modifier *= STAB_MULTIPLIER;
        }

        //Apply critical hit multiplier
        if (crit)
        {
            modifier *= CRIT_MULTIPLIER;
        }

        //Apply type effectiveness multiplier
        modifier *= TypeEffectiveness.GetEffectiveness(type, target.pokedexEntry.typeA, target.pokedexEntry.typeB);

        return modifier;
    }

    private float RandomBetween(float lower, float upper)
    {
        //Returns a random float between lower and upper, inclusive.

        float diff = upper - lower;
        float offsetScaler = (float)(randGen.NextDouble());

        return lower + diff * offsetScaler;
    }
}
