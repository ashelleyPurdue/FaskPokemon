using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    public BattlePanel initialPanel;
    private BattlePanel currentPanel;

    private DummyBattle dummyBattle = new DummyBattle();

    //Events

    void Awake()
    {
        //Subscribe to events
        dummyBattle.OnBattleStart += NullEventHandler;
        dummyBattle.OnTurnEnded += NullEventHandler;
    }

    void Start()        //Using start instead of awake so initialPanel can rut its Awake first.
    {
        //Change to the initial panel
        currentPanel = initialPanel;
        currentPanel.Show();
    }

    private void NullEventHandler()
    {
        //Handles an event by doing nothing.
    }


    //Interface

    public void ChangePanels(BattlePanel targetPanel)
    {
        //Hide the old panel, if there is one.
        if (currentPanel != null)
        {
            currentPanel.Hide();
        }

        //Show the new panel
        currentPanel = targetPanel;
        currentPanel.Show();
    }

    public void UseDummyMove(int moveNumber)
    {
        //Makes the player's pokemon use the given move from its move list

        //Create the command
        DummyBattleCommand playerCommand = DummyBattleCommand.CreateUseCommand(dummyBattle.playerPokemon, dummyBattle.enemyPokemon, moveNumber);

        //Use it
        DoTurn(playerCommand);
    }


    //Misc methods

    private void DoTurn(DummyBattleCommand playerCommand)
    {
        //Generates the enemy's command and then tell the underlying battle to do the turn.

        //Generate random enemy command
        const int maxMoveInclusive = 2;
        int enemyMove = (int)(Random.Range(0, maxMoveInclusive));

        DummyBattleCommand enemyCommand = DummyBattleCommand.CreateUseCommand(dummyBattle.enemyPokemon, dummyBattle.playerPokemon, enemyMove);

        //Switch to no panel
        currentPanel.Hide();
        currentPanel = null;

        //Do the turn
        dummyBattle.DoTurn(playerCommand, enemyCommand);
    }
}
