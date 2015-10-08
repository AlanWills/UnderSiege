using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Maths;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Physics_Components.Movement_Behaviours
{
    public class RotateToPositionArgs : MovementBehaviourArgs
    {
        public RotateToPositionArgs(GameObject objectToRotate, Vector2 position)
            : base()
        {
            Args.Add("Object To Rotate", objectToRotate);
            Args.Add("Position", position);
        }
    }

    public class RotateToPosition : MovementBehaviour
    {
        #region Properties and Fields

        private GameObject ObjectToRotate
        {
            get;
            set;
        }

        private Vector2 Position
        {
            get;
            set;
        }

        private const float acceleration = 4f;

        #endregion

        public RotateToPosition(RotateToPositionArgs args)
        {
            ObjectToRotate = args.Args["Object To Rotate"] as GameObject;
            Position = (Vector2)args.Args["Position"];
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Execute(GameTime gameTime)
        {
            base.Execute(gameTime);

            float targetAngle = Trigonometry.GetAngleOfLineBetweenPositionAndTarget(ObjectToRotate.WorldPosition, Position);
            float distance = Trigonometry.GetAngleOfLineBetweenObjectAndTarget(ObjectToRotate, Position);

            if (distance < 0.005f)
            {
                // ObjectToRotate.WorldRotation = targetAngle;
                Completed = true;
                return;
            }

            float directionToRotate = Trigonometry.GetRotateDirectionForShortestRotation(ObjectToRotate, Position);

            if (!GlobalVariables.SIMPLEPHYSICS)
            {
                float distanceToStartDeccelerating = ObjectToRotate.RigidBody.AngularVelocity * (float)Math.Ceiling(ObjectToRotate.RigidBody.AngularVelocity / acceleration) * 0.5f;
                if (distance > distanceToStartDeccelerating)
                {
                    ObjectToRotate.RigidBody.AngularAcceleration = directionToRotate * Math.Sign(targetAngle - ObjectToRotate.WorldRotation) * acceleration;
                }
                else
                {
                    ObjectToRotate.RigidBody.AngularAcceleration = directionToRotate * Math.Sign(ObjectToRotate.WorldRotation - targetAngle) * acceleration;
                }
            }
            else
            {
                ObjectToRotate.RigidBody.AngularVelocity = directionToRotate * Math.Sign(targetAngle - ObjectToRotate.WorldRotation) * ObjectToRotate.RigidBody.MaxAngularVelocity;
            }
        }

        public override void End(MovementBehaviourManager behaviourManager)
        {
            base.End(behaviourManager);

            ObjectToRotate.RigidBody.FullAngularStop();
        }

        #endregion
    }
}
