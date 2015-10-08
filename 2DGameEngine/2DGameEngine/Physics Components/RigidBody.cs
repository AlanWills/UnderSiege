using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Physics_Components
{
    public class RigidBody
    {
        #region Properties and Fields

        private BaseObject ParentObject
        {
            get;
            set;
        }

        private Vector2 maxLinearVelocity = new Vector2(400, 400);
        public Vector2 MaxLinearVelocity
        {
            get { return maxLinearVelocity; }
            set { maxLinearVelocity = value; }
        }

        private Vector2 minLinearVelocity = new Vector2(-400, -400);
        public Vector2 MinLinearVelocity
        {
            get { return minLinearVelocity; }
            set { minLinearVelocity = value; }
        }

        private Vector2 linearVelocity;
        public Vector2 LinearVelocity
        {
            get { return linearVelocity; }
            set
            {
                linearVelocity = value;

                float x = MathHelper.Clamp(linearVelocity.X, MinLinearVelocity.X, MaxLinearVelocity.X);
                float y = MathHelper.Clamp(linearVelocity.Y, MinLinearVelocity.Y, MaxLinearVelocity.Y);

                linearVelocity = new Vector2(x, y);
            }
        }

        public Vector2 LinearAcceleration
        {
            get;
            set;
        }

        private float maxAngularVelocity = 10f;
        public float MaxAngularVelocity
        {
            get { return maxAngularVelocity; }
            set { maxAngularVelocity = value; }
        }

        private float minAngularVelocity = -10f;
        public float MinAngularVelocity
        {
            get { return minAngularVelocity; }
            set { minAngularVelocity = value; }
        }

        private float angularVelocity;
        public float AngularVelocity
        {
            get { return angularVelocity; }
            set
            {
                angularVelocity = (float)MathHelper.Clamp(value, MinAngularVelocity, MaxAngularVelocity);
            }
        }

        public float AngularAcceleration
        {
            get;
            set;
        }

        #endregion

        #region Events

        public event EventHandler MovementBehaviours;

        #endregion

        public RigidBody(BaseObject parentObject)
        {
            ParentObject = parentObject;
        }

        #region Methods

        public void FullStop()
        {
            FullLinearStop();
            FullAngularStop();
        }

        public void FullLinearStop()
        {
            LinearAcceleration = Vector2.Zero;
            LinearVelocity = Vector2.Zero;
        }

        public void FullAngularStop()
        {
            AngularAcceleration = 0;
            AngularVelocity = 0;
        }

        public void Update(GameTime gameTime)
        {
            float elapsedMilliseconds = (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;

            // Adjust the velocities and accelerations
            // DoMovementBehaviours();

            AngularVelocity += AngularAcceleration * elapsedMilliseconds;
            ParentObject.LocalRotation += AngularVelocity * elapsedMilliseconds;

            LinearVelocity += LinearAcceleration * elapsedMilliseconds;

            float sinRot = (float)Math.Sin(ParentObject.LocalRotation);
            float cosRot = (float)Math.Cos(ParentObject.LocalRotation);

            ParentObject.LocalPosition += new Vector2(cosRot * LinearVelocity.X + sinRot * LinearVelocity.Y, -cosRot * LinearVelocity.Y + sinRot * LinearVelocity.X) * elapsedMilliseconds;
        }

        #endregion

        #region Virtual Methods

        public virtual void DoMovementBehaviours()
        {
            if (MovementBehaviours != null)
            {
                MovementBehaviours(ParentObject, EventArgs.Empty);
            }
        }

        #endregion
    }
}
