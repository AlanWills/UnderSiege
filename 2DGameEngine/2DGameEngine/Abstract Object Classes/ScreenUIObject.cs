using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Abstract_Object_Classes
{
    public class ScreenUIObject : UIObject
    {
        #region Properties and Fields

        #endregion

        public ScreenUIObject(string dataAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(dataAsset, parent, lifeTime)
        {
            
        }

        public ScreenUIObject(Vector2 position, string dataAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(position, dataAsset, parent, lifeTime)
        {
            
        }

        public ScreenUIObject(Vector2 position, Vector2 size, string dataAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
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
                Debug.Assert(Collider != null);
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

        #endregion
    }
}
