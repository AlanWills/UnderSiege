using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2DGameEngine.UI_Objects
{
    public class Bar : PictureBox
    {
        #region Properties and Fields

        protected Texture2D FrontBarTexture
        {
            get;
            private set;
        }

        private string FrontBarAsset
        {
            get;
            set;
        }

        protected float MaxValue { get; private set; }
        public float CurrentValue { get; set; }
        public float FrontOpacity { get; set; }

        protected Vector2 currentFrontSize;
        private Vector2 frontBarScale;
        private Vector2 frontBarLocalPosition;

        #endregion

        public Bar(Vector2 position, Vector2 size, string barAsset, float maxValue, string frontBarAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(position, size, barAsset, parent, lifeTime)
        {
            FrontBarAsset = string.IsNullOrEmpty(frontBarAsset) ? barAsset : frontBarAsset;
            MaxValue = maxValue;
            CurrentValue = MaxValue;

            currentFrontSize = size;

            Opacity = 0.5f;
            FrontOpacity = 1f;
        }

        #region Methods

        public void UpdateValue(float currentValue)
        {
            CurrentValue = currentValue;
            currentFrontSize = Vector2.Multiply(Size, new Vector2(CurrentValue / MaxValue, 1));
            frontBarScale = new Vector2(currentFrontSize.X / FrontBarTexture.Width, Scale.Y);
            // This horrible expression moves the bar so it stays in the same place when change the value
            frontBarLocalPosition = Vector2.Multiply(new Vector2(Size.X, 0), (currentValue * 0.01f - 1) * 0.5f);
        }

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            FrontBarTexture = AssetManager.GetTexture(FrontBarAsset);
            frontBarScale = new Vector2(currentFrontSize.X / FrontBarTexture.Width, Scale.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Visible)
            {
                Debug.Assert(FrontBarTexture != null);
                spriteBatch.Draw(FrontBarTexture, Vector2.Add(WorldPosition, frontBarLocalPosition), null, Colour * FrontOpacity, (float)WorldRotation, Centre, frontBarScale, SpriteEffects.None, 0);
            }
        }

        #endregion
    }
}
