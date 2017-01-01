using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePokemonDisplay : MonoBehaviour
{
    public IndividualPokemon pokemon { get; private set; }

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
        //TODO: Start the take damage animation
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
