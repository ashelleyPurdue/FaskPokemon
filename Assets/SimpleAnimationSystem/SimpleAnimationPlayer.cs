using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAnimationSystem
{
    public class SimpleAnimationPlayer : MonoBehaviour
    {
		public Vector3 positionScale = Vector3.one;     //How much to scale each keyframe's position by.
		public Vector3 scaleScale = Vector3.one;		//How much to scale each keyframe's scale by.

        private SimpleAnimation currentAnimation;

        private int nextFrameIndex = 0;
        private KeyFrame nextFrame;
        private KeyFrame prevFrame;

        private float frameTimer = 0;
        private float globalTimer = 0;

        private bool isPlaying = false;


        //Events

        void Update()
        {
            if (isPlaying)
            {
                //Increment the timers
                frameTimer += Time.deltaTime;
                globalTimer += Time.deltaTime;

                //If the frame timer has gone over, move to the next set of keyframes.
                if (frameTimer >= nextFrame.timeOffset)
                {
                    NextKeyframe();
                }

                //If the animation didn't end, update the transform
                if (isPlaying)
                {
                    UpdateTransform();
                }
            }
        }


        //Interface

        public void PlayAnimation(SimpleAnimation animation)
        {
            //Start playing the given animation from the beginning
            currentAnimation = animation;
            isPlaying = true;

            //Get the prev/next frames
            //TODO: Handle edge cases with <= 1 keyframe in the animation
            nextFrameIndex = 1;
            nextFrame = animation.GetKeyframe(nextFrameIndex);
            prevFrame = animation.GetKeyframe(0);

            //Reset timers
            frameTimer = 0;
            globalTimer = 0;

            //Apply the first frame
            UpdateTransform();
        }


        //Misc methods

        private void UpdateTransform()
        {
            //Updates the position/rotation/scale based on the current time in the animaiton

			//Tween the transformations and apply them.
            Transformation newTrans = Transformation.Lerp(prevFrame.transformation, nextFrame.transformation, frameTimer / nextFrame.timeOffset);

			//Scale the new transformation
			newTrans.position = ScaleVector(newTrans.position, positionScale);
			newTrans.scale = ScaleVector(newTrans.scale, scaleScale);

            newTrans.ApplyLocal(transform);
        }

        private void NextKeyframe()
        {
            //Moves to the next keyframe, stopping the animation if we've reached the last one.

            //Stop the animation if there are no more keyframes
            nextFrameIndex++;
            if (nextFrameIndex >= currentAnimation.numKeyframes)
            {
                //Set the transform to the end of the animation
                nextFrame.transformation.ApplyLocal(transform);

                //Stop the animation
                isPlaying = false;

                frameTimer = 0;
                globalTimer = 0;
                currentAnimation = null;

                return;
            }

            //Move to the next set of keyframes
            frameTimer -= nextFrame.timeOffset;

            prevFrame = nextFrame;
            nextFrame = currentAnimation.GetKeyframe(nextFrameIndex);
        }

		private Vector3 ScaleVector(Vector3 original, Vector3 multiplier)
		{
			//Multiplies the vectors' components.

			Vector3 scaled = original;
			original.x *= multiplier.x;
			original.y *= multiplier.y;
			original.z *= multiplier.z;

			return original;
		}
	}
}
