using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Abstract_Object_Classes
{
    public class UIObject : BaseObject
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
                    return new Vector2(1, 1);
            }
        }

        public object StoredObject
        {
            get;
            set;
        }

        public Keys HotKey
        {
            get;
            set;
        }

        private float LifeTime { get; set; }

        private float currentLifeTimer = 0;
        public static string defaultSpriteAsset = "SpriteFonts\\UISpriteFont";

        #endregion

        public UIObject(string dataAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(dataAsset, parent)
        {
            LifeTime = lifeTime;
        }

        public UIObject(Vector2 position, string dataAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(position, dataAsset, parent)
        {
            LifeTime = lifeTime;
        }

        public UIObject(Vector2 position, Vector2 size, string dataAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(position, size, dataAsset, parent)
        {
            LifeTime = lifeTime;
        }

        #region Methods

        public static void LoadPresetContent()
        {
            SpriteFont = AssetManager.GetSpriteFont(defaultSpriteAsset);
        }

        #endregion

        #region Virtual Methods

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            currentLifeTimer += (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;
            if (currentLifeTimer > LifeTime)
                Alive = false;
        }

        public override void HandleInput()
        {
            if (Active)
            {
                MouseOver = Collider.CheckCollisionWith(ScreenManager.GameMouse.WorldPosition);

                // Check hotkey click
                /*if (HotKey != Keys.None)
                {
                    if (InputHandler.KeyPressed(HotKey))
                    {
                        // If something is selected, we might be building UI or something, so for optimisation don't allow whatever we do on selecting to be done if already selected
                        if (IsSelected)
                            return;

                        // The object wasn't selected, so select it
                        if (clickResetTime >= TimeSpan.FromSeconds(0.2f))
                            Select();

                        return;
                    }
                }*/

                // Check mouse click
                bool mouseClicked = ScreenManager.GameMouse.IsLeftClicked;
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
            base.Draw(spriteBatch);

            if (Visible)
            {
                if (!string.IsNullOrEmpty(Text))
                {
                    // Always have text opacity as 1?
                    spriteBatch.DrawString(SpriteFont, Text, WorldPosition, (Texture != null ? Color.White : Colour), (float)WorldRotation, SpriteFont.MeasureString(Text) * 0.5f, new Vector2(1, 1), SpriteEffects.None, 0);
                }
            }
        }

        #endregion
    }
}
