using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Maths
{
    public static class Trigonometry
    {
        public static float GetAngleOfLineBetweenPositionAndTarget(Vector2 position, Vector2 target, bool wrap = true)
        {
            Vector2 diff = target - position;
            float angle = (float)Math.Atan2(diff.X, -diff.Y);

            if (wrap)
                angle = MathHelper.WrapAngle(angle);

            return angle;
        }

        // Returns the distance a BaseObject must rotate to be pointing at a point
        public static float GetAngleOfLineBetweenObjectAndTarget(BaseObject objectToRotate, Vector2 target)
        {
            return GetAngleOfLineBetweenPositionAndTarget(objectToRotate.WorldPosition, target);
        }

        // Return 1 if we should rotate clockwise and -1 if we should rotate anti-clockwise
        public static float GetRotateDirectionForShortestRotation(BaseObject objectToRotate, Vector2 target)
        {
            // Because rotation is between -Pi and Pi we need to work out which way to rotate to go the shortest distance
            // Here we test which direction is shorter, but add the two pi in case there is an overlap over the discontinuity in rotation range
            float targetAngle = GetAngleOfLineBetweenPositionAndTarget(objectToRotate.WorldPosition, target);
            float distance = GetAngleOfLineBetweenObjectAndTarget(objectToRotate, target);

            float targetSign = (float)Math.Sign(targetAngle);
            float objectRotationSign = (float)Math.Sign(objectToRotate.WorldRotation);

            // If either is 0, Math.Sign returns 0, so we set the sign to 1.
            if (targetSign == 0) { targetSign = 1; }
            if (objectRotationSign == 0) { objectRotationSign = 1; }

            return distance < Math.Abs(targetAngle - objectToRotate.WorldRotation + targetSign * objectRotationSign * MathHelper.TwoPi) ? 1 : -1;
        }
    }
}
