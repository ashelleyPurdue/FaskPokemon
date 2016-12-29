using System;

public class StandardAttackMove : MovedexEntry
{
    public int power;
    public int accuracy;        //For reference, most moves have 100 accuracy

    public StandardAttackMove(DexID id, string moveName, string moveDescription, PokemonType type, MoveCategory moveCategory, int basePP, int power, int accuracy)
        : base(id, moveName, moveDescription, type, moveCategory, basePP)
    {
        this.power = power;
        this.accuracy = accuracy;
    }

    public override void Use(IndividualPokemon user, IndividualPokemon target)
    {
        //TODO: Deal damage using the damage formula

        target.ChangeHP(target.maxHP / 5 * -1);
    }
}
