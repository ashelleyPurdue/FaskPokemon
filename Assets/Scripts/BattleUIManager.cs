using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    public BattlePanel initialPanel;

    private BattlePanel currentPanel;
    
    //Events

    void Start()        //Using start instead of awake so initialPanel can rut its Awake first.
    {
        //Change to the initial panel
        currentPanel = initialPanel;
        currentPanel.Show();
    }

    //Interface

    public void ChangePanels(BattlePanel targetPanel)
    {
        //Hide the old panel
        currentPanel.Hide();

        //Show the new panel
        currentPanel = targetPanel;
        currentPanel.Show();
    }

    //Misc methods
}
