using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.UI_Objects
{
    public class ScrollBar : Bar
    {
        #region Properties and Fields

        #endregion

        public ScrollBar(Vector2 position, Vector2 size, string barAsset, float maxValue, string barBackgroundAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(position, size, barAsset, maxValue, barBackgroundAsset, parent, lifeTime)
        {
            
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void HandleInput()
        {
            base.HandleInput();

            // We have clicked on the bar
            if (ScreenManager.GameMouse.IsLeftClicked && MouseOver)
            {
                Vector2 mouseClickedPosition = ScreenManager.GameMouse.LastLeftClickedPosition;
                float percentage = (mouseClickedPosition.X - WorldPosition.X) / Size.X;
                UpdateValue(percentage * MaxValue);
            }
        }

        #endregion
    }
}
