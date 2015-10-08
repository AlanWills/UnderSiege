using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Screens
{
    public class GameplayScreen : BaseScreen
    {
        #region Properties and Fields

        // Root which all game objects will be childs of
        public static GameObject SceneRoot { get; set; }

        #endregion

        public GameplayScreen(ScreenManager screenManager, string dataAsset)
            : base(screenManager, dataAsset)
        {
            Camera.Position = Vector2.Zero;
            ScreenManager.Camera.CameraMode = CameraMode.Free;
            SceneRoot = new GameObject("", null, false);
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Show()
        {
            base.Show();

            ScreenManager.Camera.CameraMode = CameraMode.Free;
        }

        #endregion
    }
}
