using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAnimationSystem
{
    public class SimpleAnimation
    {
        public int numKeyframes { get { return keyframes.Count; } }

        private List<KeyFrame> keyframes;


        //Interface

        public KeyFrame GetKeyframe(int index)
        {
            return keyframes[index];
        }

        public void GetBeforeAfterKeyframes(float time, out KeyFrame before, out KeyFrame after)
        {
            //Returns the keyframes before and after the given time value.

            //If there are no keyframes, set both to null
            if (numKeyframes == 0)
            {
                before = null;
                after = null;
                return;
            }
            
            //If there is only one keyframe, before should be it, and after should be null
            if (numKeyframes == 1)
            {
                before = keyframes[0];
                after = null;
                return;
            }

            //Begin looping to find it.
            float currentTime = 0;
            for (int i = 1; i < numKeyframes; i++)
            {
                //Increment current time
                currentTime += keyframes[i].timeOffset;

                //If we've gone past the target time, the current frame is after and the previous frame is before
                if (time <= currentTime)
                {
                    before = keyframes[i - 1];
                    after = keyframes[i];
                    return;
                }
            }

            //If we haven't found it, before should be the last keyframe, and after should be null
            before = keyframes[numKeyframes - 1];
            after = null;

            return;
        }
    }

    public class KeyFrame
    {
        public float timeOffset;    //How long after the previous keyframe this is.
        public Transformation transformation;

        public KeyFrame(float timeOffset, Transformation transformation)
        {
            this.timeOffset = timeOffset;
            this.transformation = transformation;
        }
    }
}
