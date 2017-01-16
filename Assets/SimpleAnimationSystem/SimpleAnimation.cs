using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAnimationSystem
{
    public class SimpleAnimation
    {
		public const string OFFSET_COMMAND = "offset";
		public const string POS_COMMAND = "pos";
		public const string SCALE_COMMAND = "scale";
		public const string EULER_ROT_COMMAND = "rotEuler";

        public int numKeyframes { get { return keyframes.Count; } }
        public float animationLen { get; private set; }

        private List<KeyFrame> keyframes;


        //Constructors

        public SimpleAnimation(List<KeyFrame> keyframes)
        {
            this.keyframes = keyframes;
            animationLen = CalculateAnimationLength();
        }

		public SimpleAnimation(string srcStr)
		{
			//Creates the animation from the given source string

			keyframes = new List<KeyFrame>();
			KeyFrame currentKeyframe = new KeyFrame(0, new Transformation());

			//Clean out the spaces
			//Taken from http://stackoverflow.com/questions/6219454/efficient-way-to-remove-all-whitespace-from-string/14591148#14591148
			string noSpaces = string.Join("", srcStr.Split(default(string[]), System.StringSplitOptions.RemoveEmptyEntries));

			//Split into commands
			string[] commands = noSpaces.Split(';');

			//Execute every command
			for (int i = 0; i < commands.Length; i++)
			{
				//Separate the command from the argument
				string[] splitCommand = commands[i].Split('=');
				string command = splitCommand[0];
				string argument = splitCommand[1];

				//Execute the command
				if (command.Equals(OFFSET_COMMAND))
				{
					//Add the the keyframe to the list
					keyframes.Add(currentKeyframe);

					//Start a new keyframe with the specified offset and identical transformation to the previous one
					float timeOffset = float.Parse(argument);
					currentKeyframe = new KeyFrame(timeOffset, currentKeyframe.transformation);
				}
				else if (command.Equals(POS_COMMAND))
				{
					//Set the pos
					currentKeyframe.transformation.position = Vector3FromString(argument);
				}
				else if (command.Equals(SCALE_COMMAND))
				{
					//Set the scale
					currentKeyframe.transformation.scale = Vector3FromString(argument);
				}
				else if (command.Equals(EULER_ROT_COMMAND))
				{
					//Set the rot
					currentKeyframe.transformation.rotation = Quaternion.Euler(Vector3FromString(argument));
				}
				else
				{
					//Throw an exception, because this is not a known command
					throw new System.Exception("No such SimpleAnimation command");
				}
			}

			//Add the final keyframe to the list, since the loop exits before doing that.
			keyframes.Add(currentKeyframe);

			//Calculate animation len
			animationLen = CalculateAnimationLength();
		}

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

		public override string ToString()
		{
			//Converts this animation to a string

			StringBuilder builder = new StringBuilder();

			//Append the first frame
			Transformation trans = keyframes[0].transformation;
			builder.AppendLine("\t" + POS_COMMAND + " = " + Vector3ToString(trans.position) + ";");
			builder.AppendLine("\t" + SCALE_COMMAND + " = " + Vector3ToString(trans.scale) + ";");
			builder.AppendLine("\t" + EULER_ROT_COMMAND + " = " + Vector3ToString(trans.rotation.eulerAngles) + ";");
			builder.AppendLine("");

			//Append all remaining frames
			for (int i = 1; i < numKeyframes; i++)
			{
				KeyFrame currentFrame = keyframes[i];
				KeyFrame prevFrame = keyframes[i - 1];

				//Append the time offset, unless it's the first frame
				builder.AppendLine(OFFSET_COMMAND + " = " + currentFrame.timeOffset + ";");

				//Append the pos, scale, rot unless it's the same as the previous frame
				trans = currentFrame.transformation;
				Transformation prevTrans = prevFrame.transformation;

				if (prevTrans.position != trans.position)
				{
					builder.AppendLine("\t" + POS_COMMAND + " = " + Vector3ToString(trans.position) + ";");
				}
				if (prevTrans.scale != trans.scale)
				{
					builder.AppendLine("\t" + SCALE_COMMAND + " = " + Vector3ToString(trans.scale) + ";");
				}
				if (prevTrans.rotation != trans.rotation)
				{
					builder.AppendLine("\t" + EULER_ROT_COMMAND + " = " + Vector3ToString(trans.rotation.eulerAngles) + ";");
				}

				//Append an extra line
				builder.AppendLine("");
			}

			return builder.ToString();
		}


		//Misc methods

		private float CalculateAnimationLength()
        {
            //Returns the length of the animation in seconds

            float len = 0;
            for (int i = 0; i < numKeyframes; i++)
            {
                len += keyframes[i].timeOffset;
            }

            return len;
        }

		private string Vector3ToString(Vector3 vec)
		{
			//Converts a given vector3 into a string

			return "(" + vec.x + ", " + vec.y + ", " + vec.z + ")";
		}

		private Vector3 Vector3FromString(string str)
		{
			//Converts the given string to a vector 3

			//Throw an exception if it doesn't start and end with parentheses.
			if (!str.StartsWith("(") || !str.EndsWith(")"))
			{
				throw new System.Exception("Invalid SimpleAnimation Vector3 argument");
			}

			//Split into components
			string[] components = str.Split(',');
			string xStr = components[0];
			string yStr = components[1];
			string zStr = components[2];

			//Remove the beginning/ending parens from the components
			xStr = xStr.TrimStart('(');
			zStr = zStr.TrimEnd(')');

			//Parse the components
			return new Vector3(float.Parse(xStr), float.Parse(yStr), float.Parse(zStr));
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
