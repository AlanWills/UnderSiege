using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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

        public override Vector2 Centre
        {
            get
            {
                return Vector2.Zero;
            }
        }

        protected float MaxValue { get; private set; }
        public float CurrentValue { get; set; }
        public float FrontOpacity { get; set; }

        protected Vector2 currentFrontSize;
        private Vector2 frontBarScale;

        #endregion

        public Bar(Vector2 position, Vector2 size, string barAsset, float maxValue, string frontBarAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(position - size * 0.5f, size, barAsset, parent, lifeTime)
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
            currentFrontSize = new Vector2(Size.X * CurrentValue / MaxValue, Size.Y);
            frontBarScale = new Vector2(currentFrontSize.X / FrontBarTexture.Width, Scale.Y);
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
                if (FrontBarTexture != null)
                {
                    spriteBatch.Draw(FrontBarTexture, WorldPosition, null, Colour * FrontOpacity, (float)WorldRotation, Centre, frontBarScale, SpriteEffects.None, 0);
                }
            }
        }

        #endregion
    }
}
