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
        public static int GetRotateDirectionForShortestRotation(BaseObject objectToRotate, Vector2 target)
        {
            // Because rotation is between -Pi and Pi we need to work out which way to rotate to go the shortest distance
            // Here we test which direction is shorter, but add the two pi in case there is an overlap over the discontinuity in rotation range
            /*float targetAngle = GetAngleOfLineBetweenPositionAndTarget(objectToRotate.WorldPosition, target);
            float distance = GetAngleOfLineBetweenObjectAndTarget(objectToRotate, target);

            float targetSign = (float)Math.Sign(targetAngle);
            float objectRotationSign = (float)Math.Sign(objectToRotate.WorldRotation);

            // If either is 0, Math.Sign returns 0, so we set the sign to 1.
            if (targetSign == 0) { targetSign = 1; }
            if (objectRotationSign == 0) { objectRotationSign = 1; }

            return distance < Math.Abs(targetAngle - objectToRotate.WorldRotation + targetSign * objectRotationSign * MathHelper.TwoPi) ? 1 : -1;*/

            Vector2 diff = target - objectToRotate.WorldPosition;

            float currAngle = objectToRotate.WorldRotation;
            int currAngleSign = Math.Sign(currAngle);
            float targetAngle = (float)Math.Atan2(diff.X, -diff.Y);
            int targetAngleSign = Math.Sign(targetAngle);

            if (currAngleSign >= 0 && targetAngleSign >= 0 ||
                currAngleSign <= 0 && targetAngleSign <= 0)
            {
                return Math.Sign(targetAngle - currAngle);
            }
            else if (currAngleSign >= 0 && targetAngleSign <= 0)
            {
                return (currAngle - targetAngle) <= MathHelper.Pi ? -1 : 1;
            }
            else if (currAngleSign <= 0 && targetAngleSign >= 0)
            {
                return (targetAngle - currAngle) <= MathHelper.Pi ? 1 : -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
