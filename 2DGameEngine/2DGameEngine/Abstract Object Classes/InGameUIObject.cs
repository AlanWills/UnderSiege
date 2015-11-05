using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Abstract_Object_Classes
{
    public class InGameUIObject : UIObject
    {
        #region Properties and Fields

        #endregion

        public InGameUIObject(string dataAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(dataAsset, parent, lifeTime)
        {
            
        }

        public InGameUIObject(Vector2 position, string dataAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(position, dataAsset, parent, lifeTime)
        {
            
        }

        public InGameUIObject(Vector2 position, Vector2 size, string dataAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(position, size, dataAsset, parent, lifeTime)
        {
            
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void HandleInput()
        {
            if (Active)
            {
                bool mouseClicked = ScreenManager.GameMouse.IsLeftClicked;
                MouseOver = Collider.CheckCollisionWith(ScreenManager.GameMouse.InGamePosition);

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
