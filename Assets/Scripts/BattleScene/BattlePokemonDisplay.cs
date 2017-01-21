using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAnimationSystem;

[RequireComponent(typeof(FlashEffect))]
[RequireComponent(typeof(SimpleAnimationPlayer))]
public class BattlePokemonDisplay : MonoBehaviour
{
    public IndividualPokemon pokemon { get; private set; }

    private FlashEffect flasher;
	private SimpleAnimationPlayer animPlayer;

    //Events

    void Awake()
    {
        flasher = GetComponent<FlashEffect>();
		animPlayer = GetComponent<SimpleAnimationPlayer>();
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
		//Returns the animation's early finish length

		//TODO: Decide which animation to start based on animID
		SimpleAnimation anim = TestAnimations.tackle;

		//Start the animation
		animPlayer.PlayAnimation(anim);

		return anim.earlyFinishLen;
    }

    public void Faint()
    {
        //TODO: Start fainting animation
    }

}

public static class TestAnimations
{
	//A bunch of hard-coded test animations

	public static SimpleAnimation tackle;

	static TestAnimations()
	{
		//Load the tackle animation
		string tackleText = System.IO.File.ReadAllText("moddable_data/tackle.txt");
		tackle = new SimpleAnimation(tackleText);
	}

}