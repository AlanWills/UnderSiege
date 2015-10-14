using _2DGameEngine.Extra_Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Abstract_Object_Classes
{
    public class InGameUIObject : BaseObject
    {
        #region Properties and Fields

        public static SpriteFont SpriteFont
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public override Vector2 Centre
        {
            get
            {
                if (Texture != null)
                    return base.Centre;
                else if (!string.IsNullOrEmpty(Text))
                    return SpriteFont.MeasureString(Text) * 0.5f;
                else
                    return Vector2.Zero;
            }
        }

        public override Vector2 Scale
        {
            get
            {
                if (Texture != null)
                    return base.Scale;
                else
                    return new Vector2(Size.X / Centre.X, Size.Y / Centre.Y) * 0.5f;
            }
        }

        public object StoredObject
        {
            get;
            set;
        }

        private float LifeTime { get; set; }

        private float currentLifeTimer = 0;
        public static string defaultSpriteAsset = "SpriteFonts\\UISpriteFont";

        #endregion

        public InGameUIObject(string dataAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(dataAsset, parent)
        {
            LifeTime = lifeTime;
        }

        public InGameUIObject(Vector2 position, string dataAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(position, dataAsset, parent)
        {
            LifeTime = lifeTime;
        }

        public InGameUIObject(Vector2 position, Vector2 size, string dataAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(position, size, dataAsset, parent)
        {
            LifeTime = lifeTime;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            currentLifeTimer += (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;
            if (currentLifeTimer > LifeTime)
                Alive = false;

            if (Collider != null)
                Collider.UpdateCollider();
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

        #endregion
    }
}
