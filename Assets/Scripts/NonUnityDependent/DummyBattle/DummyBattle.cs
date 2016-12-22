using System.Threading;
using System.Collections.Generic;

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

    private bool isThreadRunning = false;


    //Constructors
    public DummyBattle()
    {
        //Start out the dummy pokemon
        playerPokemon = new DummyPokemon();
        enemyPokemon = new DummyPokemon();
    }


    //Interface
    public void DoTurn(DummyBattleCommand playerCommand, DummyBattleCommand enemyCommand)
    {
        //Throw an exception if the turn thread is already running
        if (isThreadRunning)
        {
            throw new System.Exception("Turn thread already running.  Please wait for it to finish!");
        }

        //Start the turn thread
        isThreadRunning = true;

        Thread turnThread = new Thread(DoTurnThread);
        turnThread.Start(new DoTurnArgs(playerCommand, enemyCommand));
    }


    //Misc methods

    private void DoTurnThread(object args)
    {
        //The the parameters from the arg
        DoTurnArgs turnArgs = (DoTurnArgs)args;
        DummyBattleCommand playerCommand = turnArgs.playerCommand;
        DummyBattleCommand enemyCommand = turnArgs.enemyCommand;

        //TODO: Decide who goes first

        ExecuteCommand(playerCommand);
        ExecuteCommand(enemyCommand);

        //Send the event
        isThreadRunning = false;
        OnTurnEnded.Invoke();
    }
    
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


    //Nested classes
    class DoTurnArgs
    {
        public DummyBattleCommand playerCommand;
        public DummyBattleCommand enemyCommand;

        public DoTurnArgs(DummyBattleCommand playerCommand, DummyBattleCommand enemyCommand)
        {
            this.playerCommand = playerCommand;
            this.enemyCommand = enemyCommand;
        }
    }
}
