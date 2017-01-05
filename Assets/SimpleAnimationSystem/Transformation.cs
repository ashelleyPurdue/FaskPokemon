using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAnimationSystem
{
    public struct Transformation
    {
        public Vector3 position;
        public Vector3 scale;
        public Quaternion rotation;
        

        //Static methods

        public static Transformation FromTransformGlobal(Transform obj)
        {
            //Returns a transformation from the given transform, in global space

            Transformation trans = new Transformation();

            trans.position = obj.position;
            trans.scale = obj.localScale;
            trans.rotation = obj.rotation;

            return trans;
        }

        public static Transformation FromTransformLocal(Transform obj)
        {
            //Returns a transformation from the given transform, in local space

            Transformation trans = new Transformation();

            trans.position = obj.localPosition;
            trans.scale = obj.localScale;
            trans.rotation = obj.localRotation;

            return trans;
        }

        public static Transformation Lerp(Transformation start, Transformation end, float t)
        {
            //Tweens between two transformations
            Transformation newTrans = new Transformation();

            newTrans.position = Vector3.Lerp(start.position, end.position, t);
            newTrans.scale = Vector3.Lerp(start.scale, end.scale, t);
            newTrans.rotation = Quaternion.Slerp(start.rotation, end.rotation, t);

            return newTrans;
        }


        //Constructors

        public Transformation(bool invisibleParameter = false)      //This parameter does nothing.  It's just to simulate the default constructor.
        {
            position = Vector3.zero;
            scale = Vector3.one;
            rotation = Quaternion.identity;
        }

        public Transformation(Vector3 position, Vector3 scale, Quaternion rotation)
        {
            this.position = position;
            this.scale = scale;
            this.rotation = rotation;
        }


        //Interface

        public void ApplyGlobal(Transform target)
        {
            //Applies this transformation to the target's global position/rotation

            target.position = position;
            target.rotation = rotation;
            target.localScale = scale;
        }

        public void ApplyLocal(Transform target)
        {
            //Applies this transformation to the target's local position/rotation

            target.localPosition = position;
            target.localRotation = rotation;
            target.localScale = scale;
        }
    }
}
