using System.Collections.Generic;

public class IndividualPokemonMove
{
    public DexID moveID;
    public MovedexEntry entry { get { return Movedex.GetEntry(moveID); } }

    public int currentPP { get; private set; }
    public int maxPP
    {
        get
        {
            //TODO: Implement PP-max
            return entry.basePP + (entry.basePP / 5) * ppUpsUsed;
        }
    }

    public const int MAX_PPUPS = 3;
    public int ppUpsUsed { get; private set; }


    //Constructors

    public IndividualPokemonMove(DexID moveID, int currentPP, int ppUpsUsed = 0)
    {
        Construct(moveID, currentPP, ppUpsUsed);
    }

    public IndividualPokemonMove(DexID moveID)
    {
        //Constructor for having just learned the move
        Construct(moveID, 0);
        RestorePP(maxPP);
    }

    private void Construct(DexID moveID, int currentPP, int ppUpsUsed = 0)
    {
        this.moveID = moveID;
        this.currentPP = currentPP;

        this.ppUpsUsed = ppUpsUsed;
    }


    //Interface

    public void RestorePP(int pp)
    {
        //Restores the given amount of PP
        //Caps it at max

        currentPP = pp;
        if (currentPP > maxPP)
        {
            currentPP = maxPP;
        }
    }

    public void UsePPUp()
    {
        //Uses a PP-up on this move

        //Throw an error if too many PP-ups
        if (ppUpsUsed >= MAX_PPUPS)
        {
            throw new TooManyPPUpsException();
        }

        //Use it
        ppUpsUsed++;
    }

    public void Use(IndividualPokemon user, IndividualPokemon target)
    {
        //Uses the move

        //Throw an error if not enough PP
        if (currentPP <= 0)
        {
            throw new NotEnoughPPException();
        }

        //Use the move
        currentPP--;
        entry.Use(user, target);
    }


    //Exceptions
    public class NotEnoughPPException : System.Exception {}
    public class TooManyPPUpsException : System.Exception { }
}