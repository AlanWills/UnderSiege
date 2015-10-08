using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.UI_Objects
{
    public class FlashingLabel : Label
    {
        #region Properties and Fields

        private bool increaseOpacity;
        private float stepAmount;

        #endregion

        public FlashingLabel(string text, Vector2 localPosition, Color colour, BaseObject parent = null, float lifeTime = float.MaxValue, float stepAmount = 0.03f)
            : base(text, localPosition, colour, parent, lifeTime)
        {
            this.stepAmount = stepAmount;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (increaseOpacity)
            {
                Opacity += stepAmount;
                if (Opacity >= 1)
                {
                    Opacity = 1;
                    increaseOpacity = false;
                }
            }
            else
            {
                Opacity -= stepAmount;
                if (Opacity <= 0)
                {
                    Opacity = 0;
                    increaseOpacity = true;
                }
            }
        }

        public override void HandleInput()
        {
            
        }

        #endregion
    }
}
