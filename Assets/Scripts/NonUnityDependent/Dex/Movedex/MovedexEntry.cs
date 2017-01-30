using System.Collections.Generic;

public abstract class MovedexEntry
{
    public DexID id;
    public string moveName;
    public string moveDescription;

    public DexID genericAnimationID;

    public PokemonType type;
    public MoveCategory moveCategory;

    public int basePP;

    //Constructors

    public MovedexEntry(DexID id, string moveName, string moveDescription, DexID genericAnimationID, PokemonType type, MoveCategory moveCategory, int basePP)
    {
        this.id = id;
        this.moveName = moveName;
        this.moveDescription = moveDescription;

		this.genericAnimationID = genericAnimationID;

        this.type = type;
        this.moveCategory = moveCategory;

        this.basePP = basePP;
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
