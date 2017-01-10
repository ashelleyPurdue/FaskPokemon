using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAnimationSystem;

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

public static class TestAnimations
{
	//A bunch of hard-coded test animations

	public static SimpleAnimation tackle;

	static TestAnimations()
	{
		//Create the tackle animation
		List<KeyFrame> frames = new List<KeyFrame>();
		KeyFrame start = new KeyFrame(0, new Transformation());
		KeyFrame end = new KeyFrame(0.5f, new Transformation(new Vector3(1, 1), Vector3.one, Quaternion.identity));
		KeyFrame comeBack = new KeyFrame(1f, start.transformation);

		frames.Add(start);
		frames.Add(end);
		frames.Add(comeBack);

		tackle = new SimpleAnimation(frames);
	}
}