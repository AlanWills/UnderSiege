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
    public abstract class UIObject : BaseObject
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
