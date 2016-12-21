using System.Collections.Generic;

public class DummyBattle
{
    //Subscribable events
    public delegate void BasicEventHandler();

    public event BasicEventHandler OnBattleStart;       //Called when the battle starts
    public event BasicEventHandler OnTurnEnded;         //Called when the current turn is over and the battle is awaiting the player's next move.


    //Fields
    public DummyPokemon playerPokemon { get; private set; }
    public DummyPokemon enemyPokemon { get; private set; }


    //Factories
    public DummyBattle()
    {
        //Start out the dummy pokemon
        playerPokemon = new DummyPokemon();
        enemyPokemon = new DummyPokemon();
    }


    //Interface
    public void DoTurn(DummyBattleCommand playerCommand, DummyBattleCommand enemyCommand)
    {
        //TODO: Decide who goes first

        ExecuteCommand(playerCommand);
        ExecuteCommand(enemyCommand);

        //Send the event
        OnTurnEnded.Invoke();
    }


    //Misc methods

    private void ExecuteCommand(DummyBattleCommand command)
    {
        //If it's a move, use it.
        if (command.commandType == DummyBattleCommandType.useMove)
        {
            UseMove(command.moveToUse, command.userPokemon, command.targetPokemon);
        }
    }

    private void UseMove(int moveIndex, DummyPokemon user, DummyPokemon target)
    {
        switch(moveIndex)
        {
            case 0: user.Tackle(target); break;
            case 1: user.PoisonSting(target); break;
        }
    }
}
