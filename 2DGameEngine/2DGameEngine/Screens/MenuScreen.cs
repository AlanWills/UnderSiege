using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Screens
{
    public class MenuScreen : BaseScreen
    {
        #region Properties and Fields

        public static Vector2 TitlePosition = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 0.5f, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.1f);

        #endregion

        public MenuScreen(ScreenManager screenManager, string dataAsset = "")
            : base(screenManager, dataAsset)
        {
            ScreenManager.Camera.CameraMode = CameraMode.Fixed;
            Camera.Position = Vector2.Zero;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public virtual void AddTitle(string titleAsset)
        {
            AddImage(TitlePosition, "Title", titleAsset);
        }

        #endregion
    }
}
