using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlashEffect))]
public class BattlePokemonDisplay : MonoBehaviour
{
    public IndividualPokemon pokemon { get; private set; }

    private FlashEffect flasher;

    //Events

    void Awake()
    {
        flasher = GetComponent<FlashEffect>();
    }

    //Interface

    public void SendOut(IndividualPokemon pokemon)
    {
        //TODO: Start the send-out animation
    }

    public void ComeBack()
    {
        //TODO: Start the come back animation
    }

    public void TakeDamage()
    {
        //Start the take damage animation

        flasher.StartFlashing();
    }

    public float GenericMoveAnimation(int animID)
    {
        //TODO: Start one of the generic animations
        //Returns the length of the animation in seconds

        return 0.25f;
    }

    public void Faint()
    {
        //TODO: Start fainting animation
    }

}
