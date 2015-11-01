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
    public abstract class GameOverScreen : BaseScreen
    {
        #region Properties and Fields

        #endregion

        public GameOverScreen(ScreenManager screenManager, string dataAsset)
            : base(screenManager, dataAsset)
        {
            Button backToMainMenuButton = new Button(new Vector2(Viewport.Width * 0.25f, Viewport.Height * 0.5f), new Vector2(Button.SpriteFont.MeasureString("Back to Main Menu").X + 10, Button.defaultTexture.Height), "Back to Main Menu");
            backToMainMenuButton.OnSelect += backToMainMenuButton_OnSelect;
            AddScreenUIObject(backToMainMenuButton, "Back to Main Menu Button");

            Button restartLevelButton = new Button(new Vector2(Viewport.Width * 0.75f, Viewport.Height * 0.5f), new Vector2(Button.SpriteFont.MeasureString("Restart Level").X + 10, Button.defaultTexture.Height), "Restart Level");
            restartLevelButton.OnSelect += restartLevelButton_OnSelect;
            AddScreenUIObject(restartLevelButton, "Restart Level Button");

            AddScreenUIObject(new Label("Game Over", new Vector2(Viewport.Width * 0.5f, Viewport.Height * 0.15f), Color.Cyan), "Game Over Label");
        }

        #region Methods

        #endregion

        #region Events

        protected abstract void backToMainMenuButton_OnSelect(object sender, EventArgs e);
        protected abstract void restartLevelButton_OnSelect(object sender, EventArgs e);

        #endregion

        #region Virtual Methods

        #endregion
    }
}
