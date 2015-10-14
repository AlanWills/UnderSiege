using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.Object_Properties;
using _2DGameEngine.Physics_Components;
using _2DGameEngine.Physics_Components.Movement_Behaviours;
using _2DGameEngineData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Abstract_Object_Classes
{
    public class GameObject : BaseObject
    {
        #region Properties and Fields

        public RigidBody RigidBody
        {
            get;
            set;
        }

        public MovementBehaviourManager MovementBehaviours
        {
            get;
            private set;
        }

        public event EventHandler AbilityEventQueue;

        #endregion

        public GameObject(string dataAsset = "", BaseObject parent = null, bool addRigidBody = true)
            : base(dataAsset, parent)
        {
            MovementBehaviours = new MovementBehaviourManager();

            if (addRigidBody)
            {
                RigidBody = new RigidBody(this);
            }
        }

        public GameObject(Vector2 position, string dataAsset = "", BaseObject parent = null, bool addRigidBody = true)
            : base(position, dataAsset, parent)
        {
            MovementBehaviours = new MovementBehaviourManager();

            if (addRigidBody)
            {
                RigidBody = new RigidBody(this);
            }
        }

        public GameObject(Vector2 position, Vector2 size, string dataAsset = "", BaseObject parent = null, bool addRigidBody = true)
            : base(position, size, dataAsset, parent)
        {
            MovementBehaviours = new MovementBehaviourManager();

            if (addRigidBody)
            {
                RigidBody = new RigidBody(this);
            }
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // MovementBehaviours.Update(gameTime);

            if (Active)
            {
                if (RigidBody != null)
                {
                    RigidBody.Update(gameTime);
                }

                if (Collider != null)
                    Collider.UpdateCollider();

                if (AbilityEventQueue != null)
                {
                    AbilityEventQueue(this, EventArgs.Empty);
                }
            }
        }

        public override void HandleInput()
        {
            if (Active)
            {
                bool mouseClicked = GameMouse.IsLeftClicked;
                MouseOver = Collider.CheckCollisionWith(GameMouse.InGamePosition);

                // If mouse isn't clicked we don't need to change the selection state, as we haven't selected anything!
                if (mouseClicked)
                {
                    // We have clicked on the object
                    if (MouseOver)
                    {
                        // The object wasn't selected, so select it
                        if (clickResetTime >= TimeSpan.FromSeconds(0.2f))
                            IsSelected = true;
                    }
                    // We have clicked elsewhere so should clear selection
                    else
                    {
                        IsSelected = false;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                if (Texture != null)
                {
                    spriteBatch.Draw(Texture, WorldPosition, SourceRectangle, Colour * Opacity, (float)WorldRotation, Centre, Scale, SpriteEffects.None, 0);
                }

                IfVisible();
            }
        }

        #endregion
    }
}