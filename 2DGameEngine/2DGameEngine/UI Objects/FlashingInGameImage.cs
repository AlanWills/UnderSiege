using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.UI_Objects
{
    public class FlashingInGameImage : InGameImage
    {
        #region Properties and Fields

        private bool opacityIncreasing;
        private const float increment = 0.01f;

        #endregion

        public FlashingInGameImage(string dataAsset = "", BaseObject parent = null)
            : base(dataAsset, parent)
        {

        }

        public FlashingInGameImage(Vector2 position, string dataAsset = "", BaseObject parent = null)
            : base(position, dataAsset, parent)
        {

        }

        public FlashingInGameImage(Vector2 position, Vector2 size, string dataAsset = "", BaseObject parent = null)
            : base(position, size, dataAsset, parent)
        {

        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Visible)
            {
                if (opacityIncreasing)
                {
                    Opacity += increment;
                    if (Opacity >= 1)
                    {
                        Opacity = 1;
                        opacityIncreasing = false;
                    }
                }
                else
                {
                    Opacity -= increment;
                    if (Opacity <= 0)
                    {
                        Opacity = 0;
                        opacityIncreasing = true;
                    }
                }
            }
        }

        public override void Hide()
        {
            base.Hide();

            opacityIncreasing = false;
            Opacity = 1f;
        }

        #endregion
    }
}
