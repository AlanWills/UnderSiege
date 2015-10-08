using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Maths;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Physics_Components.Movement_Behaviours
{
    public class PointAtTargetArgs : MovementBehaviourArgs
    {
        public PointAtTargetArgs(GameObject objectToRotate, GameObject target)
            : base()
        {
            Args.Add("Object To Rotate", objectToRotate);
            Args.Add("Target", target);
        }
    }

    public class PointAtTarget : MovementBehaviour
    {
        #region Properties and Fields

        private GameObject ObjectToRotate
        {
            get;
            set;
        }

        private GameObject Target
        {
            get;
            set;
        }

        private const float acceleration = 0.75f;
        private bool lockedOn = false;

        #endregion

        public PointAtTarget(PointAtTargetArgs args)
        {
            ObjectToRotate = args.Args["Object To Rotate"] as GameObject;
            Target = (GameObject)args.Args["Target"];
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Execute(GameTime gameTime)
        {
            base.Execute(gameTime);

            float targetAngle = Trigonometry.GetAngleOfLineBetweenPositionAndTarget(ObjectToRotate.WorldPosition, Target.WorldPosition);
            float distance = Trigonometry.GetAngleOfLineBetweenObjectAndTarget(ObjectToRotate, Target.WorldPosition);

            // Once we get sufficiently close to our target, we lock on and no longer use physics to drive the rotation
            if (distance < 0.005f || lockedOn)
            {
                lockedOn = true;
                // ObjectToRotate.WorldRotation = targetAngle;
                ObjectToRotate.RigidBody.FullAngularStop();

                return;
            }

            float directionToRotate = Trigonometry.GetRotateDirectionForShortestRotation(ObjectToRotate, Target.WorldPosition);

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
