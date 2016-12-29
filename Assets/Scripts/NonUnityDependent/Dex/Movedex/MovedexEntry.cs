using System.Collections.Generic;

public abstract class MovedexEntry
{
    public DexID id;
    public string moveName;
    public string moveDescription;

    public MoveCategory moveCategory;


    //Constructors

    public MovedexEntry(DexID id, string moveName, string moveDescription, MoveCategory moveCategory)
    {
        this.id = id;
        this.moveName = moveName;
        this.moveDescription = moveDescription;

        this.moveCategory = moveCategory;
    }
    

    //Abstract methods

    public abstract void Use(IndividualPokemon user, IndividualPokemon target);
}

public enum MoveCategory
{
    physical,
    special,
    status
}
