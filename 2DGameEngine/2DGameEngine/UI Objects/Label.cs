using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2DGameEngine.UI_Objects
{
    public class Label : ScreenUIObject
    {
        #region Properties and Fields

        public Vector2 TextDimensions
        {
            get { return SpriteFont.MeasureString(Text); }
        }

        #endregion

        public Label(string text, Color colour, BaseObject parent = null, float lifeTime = float.MaxValue)
            : base("", parent, lifeTime)
        {
            Text = text;
            Colour = colour;

        }

        public Label(string text, Vector2 localPosition, Color colour, BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(localPosition, "", parent, lifeTime)
        {
            Text = text;
            Colour = colour;

        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                if (!string.IsNullOrEmpty(Text))
                {
                    // Always have text opacity as 1?
                    spriteBatch.DrawString(SpriteFont, Text, WorldPosition, (Texture != null ? Color.White : Colour) * Opacity, (float)WorldRotation, SpriteFont.MeasureString(Text) * 0.5f, Scale, SpriteEffects.None, 0);
                }
            }
        }

        public override void HandleInput()
        {
            
        }

        #endregion
    }
}
