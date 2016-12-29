using System;

public class StandardAttackMove : MovedexEntry
{
    public int power;
    public int accuracy;

    public StandardAttackMove(DexID id, string moveName, string moveDescription, MoveCategory moveCategory, int basePP, int power, int accuracy)
        : base(id, moveName, moveDescription, moveCategory, basePP)
    {
        this.power = power;
        this.accuracy = accuracy;
    }

    public override void Use(IndividualPokemon user, IndividualPokemon target)
    {
        //TODO: Deal damage using the damage formula
        throw new NotImplementedException();
    }
}
