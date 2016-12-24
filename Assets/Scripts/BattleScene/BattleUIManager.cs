using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    public float commandTextDelay = 1f;     //How long to pause after showing a command's text
    public float useMoveDelay = 1f;         //How long to pause after using a move

    public DiffableHealthBar playerHealthBar;
    public DiffableHealthBar enemyHealthBar;

    public ScrollingTextbox playerTextbox;
    public ScrollingTextbox enemyTextbox;

    public BattlePanel initialPanel;
    private BattlePanel currentPanel;

    private DummyPokemon playerPokemon = new DummyPokemon();
    private DummyPokemon enemyPokemon = new DummyPokemon();

    private Queue<DummyBattleCommand> commandQueue = new Queue<DummyBattleCommand>();

    private bool executingCommand = false;      //Whether or not we're currently executing a command.


    //Events

    void Awake()
    {
    }

    void Start()        //Using start instead of awake so initialPanel can rut its Awake first.
    {
        //Change to the initial panel
        ChangePanels(initialPanel);
    }

    void Update()
    {
        //Skip if space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WaitForSecondsOrSkip.Skip();
        }

        //Update the health bars
        playerHealthBar.currentValue = (float)playerPokemon.currentHealth / playerPokemon.maxHealth;
        enemyHealthBar.currentValue = (float)enemyPokemon.currentHealth / enemyPokemon.maxHealth;
    }


    //Interface

    public void UseMove(int moveIndex)
    {
        //Create a battle command to use this move
        DummyBattleCommand playerCommand = DummyBattleCommand.CreateUseCommand(playerPokemon, enemyPokemon, moveIndex);

        //Do the turn
        DoTurn(playerCommand);
    }

    public void ChangePanels(BattlePanel targetPanel)
    {
        //Hide the old panel
        if (currentPanel != null)
        {
            currentPanel.Hide();
        }

        //Show the new panel
        currentPanel = targetPanel;

        if (currentPanel != null)
        {
            currentPanel.Show();
        }
    }


    //Coroutines
    
    private IEnumerator ExecuteCommands()
    {
        //Clear the textboxes
        playerTextbox.text = "";
        enemyTextbox.text = "";

        //Execute every command
        while (commandQueue.Count > 0)
        {
            DummyBattleCommand command = commandQueue.Dequeue();

            //Execute the command we just popped
            do { yield return ExecuteCommand(command); } while (executingCommand);

            //Wait
            yield return new WaitForSecondsOrSkip(useMoveDelay);
        }

        //Commands are done.  Show menus again
        ChangePanels(initialPanel);
    }
    
    private IEnumerator ExecuteCommand(DummyBattleCommand command)
    {
        //Actually execute the command
        executingCommand = true;

        //Display the command text
        ScrollingTextbox textbox = playerTextbox;
        if (command.userPokemon == enemyPokemon)
        {
            textbox = enemyTextbox;
        }
        textbox.text = command.text;
        yield return new WaitForSecondsOrSkip(commandTextDelay);


        if (command.commandType == DummyBattleCommandType.useMove)
        {
            //TODO: Start move animation

            //TODO: Wait for a minimum amount of time, specified by the animation

            //Execute the move
            DummyPokemon user = command.userPokemon;
            DummyPokemon target = command.targetPokemon;

            switch (command.moveToUse)
            {
                case 0: user.Tackle(target); break;
                case 1: user.PoisonSting(target); break;
            }
        }

        executingCommand = false;
        yield return null;
    }

    //Misc methods

    private DummyBattleCommand DecideEnemyCommand()
    {
        //TODO: Choose a random command for the enemy to use

        return DummyBattleCommand.CreateUseCommand(enemyPokemon, playerPokemon, 0);
    }

    private void DoTurn(DummyBattleCommand playerCommand)
    {
        //Starts executing the commands.

        //Decide the enemy command
        DummyBattleCommand enemyCommand = DecideEnemyCommand();

        //Put the commands in the queue
        //TODO: Decide which one should go first
        commandQueue.Enqueue(playerCommand);
        commandQueue.Enqueue(enemyCommand);

        //Hide the battle panels until the commands are done executing
        ChangePanels(null);

        //Start the coroutine
        StartCoroutine(ExecuteCommands());
    }
}
