using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    public float useMoveDelay = 0.75f;         //How long to pause after using a move

    public DiffableHealthBar playerHealthBar;
    public DiffableHealthBar enemyHealthBar;

    public BattlePokemonDisplay playerDisplay;
    public BattlePokemonDisplay enemyDisplay;

    public ScrollingTextbox playerTextbox;
    public ScrollingTextbox enemyTextbox;

    public BattlePanel initialPanel;
    private BattlePanel currentPanel;

    public IndividualPokemon playerPokemon { get; private set; }
    public IndividualPokemon enemyPokemon { get; private set; }

    private Queue<BattleCommand> commandQueue = new Queue<BattleCommand>();

    private bool executingCommand = false;      //Whether or not we're currently executing a command.


    //Events

    void Awake()
    {
        //Give both trainers a missingno
        PlayerSwitchIn(GenerateTestMissingno(5));
        EnemySwitchIn(GenerateTestMissingno(5));
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
        playerHealthBar.currentValue = (float)playerPokemon.currentHP / playerPokemon.maxHP;
        enemyHealthBar.currentValue = (float)enemyPokemon.currentHP / enemyPokemon.maxHP;
    }


    //Interface

    public void UseMove(int moveIndex)
    {
        //Create a battle command to use this move
        BattleCommand playerCommand = BattleCommand.CreateUseCommand(playerPokemon, enemyPokemon, moveIndex);

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
            BattleCommand command = commandQueue.Dequeue();

            //Execute the command we just popped
            do { yield return ExecuteCommand(command); } while (executingCommand);

            //Wait
            yield return new WaitForSecondsOrSkip(useMoveDelay);
        }

        //Commands are done.  Show menus again
        ChangePanels(initialPanel);
    }
    
    private IEnumerator ExecuteCommand(BattleCommand command)
    {
        //Actually execute the command
        executingCommand = true;

        //Determine which set of display objects to use
        BattlePokemonDisplay userDisplay = playerDisplay;
        ScrollingTextbox userTextbox = playerTextbox;

        BattlePokemonDisplay targetDisplay = enemyDisplay;

        if (command.userPokemon == enemyPokemon)
        {
            userTextbox = enemyTextbox;
            userDisplay = enemyDisplay;

            targetDisplay = playerDisplay;
        }

        //Display the command text
        userTextbox.text = command.text;

        //Execute the move
        if (command.commandType == BattleCommandType.useMove)
        {
            IndividualPokemon user = command.userPokemon;
            IndividualPokemon target = command.targetPokemon;

            IndividualPokemonMove move = user.GetMove(command.moveToUse);

			//Start the command's animation
			float waitTime = userDisplay.GenericMoveAnimation(move.entry.genericAnimationID);
            yield return new WaitForSecondsOrSkip(waitTime);

            //Use the move
            user.GetMove(command.moveToUse).Use(user, target);

            //Start flashing
            targetDisplay.TakeDamage();
        }

        executingCommand = false;
        yield return null;
    }


    //Misc methods

    private void PlayerSwitchIn(IndividualPokemon pokemon)
    {
        //Switches the given pokemon in for the player
        playerPokemon = pokemon;

        //TODO: Change sprite, play animation
    }

    private void EnemySwitchIn(IndividualPokemon pokemon)
    {
        //Switches the given pokemon in for the enemy
        enemyPokemon = pokemon;

        //TODO: Change sprite, play animation
        
    }

    private BattleCommand DecideEnemyCommand()
    {
        //TODO: Choose a random command for the enemy to use

        return BattleCommand.CreateUseCommand(enemyPokemon, playerPokemon, 0);
    }

    private void DoTurn(BattleCommand playerCommand)
    {
        //Starts executing the commands.

        //Decide the enemy command
        BattleCommand enemyCommand = DecideEnemyCommand();

        //Put the commands in the queue
        //TODO: Decide which one should go first
        commandQueue.Enqueue(playerCommand);
        commandQueue.Enqueue(enemyCommand);

        //Hide the battle panels until the commands are done executing
        ChangePanels(null);

        //Start the coroutine
        StartCoroutine(ExecuteCommands());
    }

    private IndividualPokemon GenerateTestMissingno(int level)
    {
        //Generates a missingno to be used for testing purposes

        //Set each IV to 15
        PokemonStats ivs = new PokemonStats();
        for (int i = 0; i < PokemonStats.NUM_STATS; i++)
        {
            ivs[(PokemonStatID)i] = 15;
        }

        //Give it tackle
        List<IndividualPokemonMove> moves = new List<IndividualPokemonMove>();
        moves.Add(new IndividualPokemonMove(new DexID("", 1)));

        //Create the pokemon
        IndividualPokemon pokemon = new IndividualPokemon(new DexID("", 0), ivs, moves, level);

        return pokemon;
    }
}
