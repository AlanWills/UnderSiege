using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Physics_Components.Movement_Behaviours
{
    public class MoveToPositionArgs : MovementBehaviourArgs
    {
        public MoveToPositionArgs(GameObject objectToMove, Vector2 position)
            : base()
        {
            Args.Add("Object To Move", objectToMove);
            Args.Add("Position", position);
        }
    }

    public class MoveToPosition : MovementBehaviour
    {
        #region Properties and Fields

        private GameObject ObjectToMove
        {
            get;
            set;
        }

        private Vector2 Position
        {
            get;
            set;
        }

        private const float acceleration = 100f;

        #endregion
        
        public MoveToPosition(MovementBehaviourArgs args)
        {
            ObjectToMove = args.Args["Object To Move"] as GameObject;
            Position = (Vector2)args.Args["Position"];
        }

        #region Methods

        #endregion
        
        #region Virtual Methods

        public override void AddDependentBehaviours()
        {
            base.AddDependentBehaviours();

            DependentBehaviours.Add(new KeyValuePair<string, MovementBehaviour>("Rotate To Position", new RotateToPosition(new RotateToPositionArgs(ObjectToMove, Position))));

            foreach (KeyValuePair<string, MovementBehaviour> pair in DependentBehaviours)
            {
                ObjectToMove.MovementBehaviours.AddBehaviour(pair);
            }
        }

        public override void Execute(GameTime gameTime)
        {
            base.Execute(gameTime);

            float distance = (Position - ObjectToMove.WorldPosition).Length();
            if (distance < (ObjectToMove.RigidBody.LinearVelocity.Y + acceleration) * (float)gameTime.ElapsedGameTime.Milliseconds / 1000f)
            {
                Completed = true;
                return;
            }

            if (!GlobalVariables.SIMPLEPHYSICS)
            {
                float distanceToStartDeccelerating = ObjectToMove.RigidBody.LinearVelocity.Y * (float)Math.Ceiling(ObjectToMove.RigidBody.LinearVelocity.Y / acceleration) * 0.5f;
                if (distance > distanceToStartDeccelerating)
                {
                    ObjectToMove.RigidBody.LinearAcceleration = new Vector2(ObjectToMove.RigidBody.LinearAcceleration.X, acceleration);
                }
                else
                {
                    ObjectToMove.RigidBody.LinearAcceleration = new Vector2(ObjectToMove.RigidBody.LinearAcceleration.X, -acceleration);
                }
            }
            else
            {
                ObjectToMove.RigidBody.LinearVelocity = new Vector2(0, ObjectToMove.RigidBody.MaxLinearVelocity.Y);
            }
        }

        public override void End(MovementBehaviourManager behaviourManager)
        {
            base.End(behaviourManager);

            ObjectToMove.LocalPosition = Position;
            ObjectToMove.RigidBody.FullLinearStop();
        }

        #endregion
    }
}
