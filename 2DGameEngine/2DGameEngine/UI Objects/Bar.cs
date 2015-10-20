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

        private Texture2D BarTexture
        {
            get;
            set;
        }

        private string BarBackgroundAsset
        {
            get;
            set;
        }

        private float MaxValue { get; set; }
        public float CurrentValue { get; set; }

        private Vector2 startingSize;

        #endregion

        public Bar(Vector2 position, Vector2 size, string barAsset, float maxValue, string barBackgroundAsset = "", BaseObject parent = null)
            : base(position - size * 0.5f, size, barAsset, parent)
        {
            BarBackgroundAsset = barBackgroundAsset;
            MaxValue = maxValue;
            CurrentValue = MaxValue;

            startingSize = size;
        }

        #region Methods

        public void UpdateValue(float currentValue)
        {
            CurrentValue = currentValue;
            Size = new Vector2(startingSize.X * CurrentValue / MaxValue, startingSize.Y);
        }

        #endregion

        #region Virtual Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                if (Texture != null)
                {
                    spriteBatch.Draw(Texture, DestinationRectangle, SourceRectangle, Colour * Opacity, (float)WorldRotation, Vector2.Zero, SpriteEffects.None, 0);
                }

                IfVisible();

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
