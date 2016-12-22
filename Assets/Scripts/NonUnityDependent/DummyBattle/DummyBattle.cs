using System.Collections;
using System.Collections.Generic;

[System.Obsolete]
public class DummyBattle
{
    //Subscribable events
    public delegate void BasicEventHandler();

    public event BasicEventHandler OnBattleStart;       //Called when the battle starts
    public event BasicEventHandler OnTurnEnded;         //Called when the current turn is over and the battle is awaiting the player's next move.

    public delegate void CommandExecutedHanlder(DummyBattleCommand command);

    public event CommandExecutedHanlder OnPlayerCommandExecuted;
    public event CommandExecutedHanlder OnEnemyCommandExecuted;


    //Fields
    public DummyPokemon playerPokemon { get; private set; }
    public DummyPokemon enemyPokemon { get; private set; }


    //Constructors
    public DummyBattle()
    {
        //Start out the dummy pokemon
        playerPokemon = new DummyPokemon();
        enemyPokemon = new DummyPokemon();
    }


    //Interface
    /*public System.Collections.IEnumerator ResumeTurn(DummyBattleCommand playerCommand, DummyBattleCommand enemyCommand)
    {
        //TODO: Decide who goes first

        ExecuteCommand(playerCommand);
        OnPlayerCommandExecuted.Invoke(playerCommand);
        yield return null;

        ExecuteCommand(enemyCommand);
        OnEnemyCommandExecuted.Invoke(enemyCommand);
        yield return null;

        //End the turn
        isThreadRunning = false;
        OnTurnEnded.Invoke();

        yield break;
    }*/

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