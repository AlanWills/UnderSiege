using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Screens
{
    public class MenuScreen : BaseScreen
    {
        #region Properties and Fields
        
        #endregion

        public MenuScreen(ScreenManager screenManager, string backgroundAsset = "")
            : base(screenManager, backgroundAsset)
        {
            ScreenManager.Camera.CameraMode = CameraMode.Fixed;
            Camera.Position = Vector2.Zero;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        #endregion
    }
}
