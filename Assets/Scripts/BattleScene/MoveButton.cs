using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MoveButton : MonoBehaviour
{
    public BattleUIManager battleUIManager;
    public Text text;

    public int moveNum;

    private Button button;

    //Events

    void Awake()
    {
        //Get the button
        button = GetComponent<Button>();
    }

    void Update()
    {
        //TODO: Find another event to call this in, one that's less frequent
        UpdateButton();
    }

    //Misc methods

    private void UpdateButton()
    {
        //Updates the button text, clickability, etc.

		//If it's out of bounds, disable the button
		if (moveNum >= battleUIManager.playerPokemon.knownMovesCount)
		{
			DisableButton();
			return;
		}

		//Get the move
        IndividualPokemonMove move = battleUIManager.playerPokemon.GetMove(moveNum);

        //If the move is null, disable button
        if (move == null)
        {
			DisableButton();
            return;
        }

        //Change the button's text to match the move name
        text.text = move.entry.moveName;
        button.interactable = true;

        //If there's no PP left, disable button
        if (move.currentPP <= 0)
        {
            text.text += "(No PP)";
            button.interactable = false;
        }
    }

	private void DisableButton()
	{
		text.text = "--";
		button.interactable = false;
	}
}
