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
    public class MainMenuScreen : MenuScreen
    {
        #region Properties and Fields

        private Menu MainMenuPanel
        {
            get;
            set;
        }

        public static Vector2 NextButtonPosition = new Vector2(ScreenManager.ScreenCentre.X, ScreenManager.ScreenCentre.Y * 0.5f);
        public static Vector2 TitlePosition = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 0.5f, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.1f);

        #endregion

        public MainMenuScreen(ScreenManager screenManager, bool addDefaultUI)
            : base(screenManager, "Data\\Screens\\MainMenuScreen")
        {
            if (addDefaultUI)
            {
                AddMainMenu(new Vector2(Viewport.Width * 0.2f, Viewport.Height * 0.5f));
                AddStartGameButton();
                AddLoadGameButton();
                AddSettingsScreenButton();
                AddExitGameButton();
            }
        }

        #region Methods

        #endregion

        #region Virtual Methods

        #region Main Menu UI

        public virtual void AddTitle(string titleAsset)
        {
            AddImage(TitlePosition, "Title", titleAsset);
        }

        protected virtual void AddMainMenu(Vector2 size, string panelAsset = "")
        {
            // Change Next Button Position to be relative to this now
            // NextButtonPosition = 
        }

        protected virtual void AddStartGameButton()
        {
            AddButton(NextButtonPosition, "Start Game Button", StartGame, false, "Start Game");
            NextButtonPosition += new Vector2(0, Viewport.Height / 6);
        }

        protected virtual void AddLoadGameButton()
        {
            AddButton(NextButtonPosition, "Load Game Button", LoadGame, false, "Load Game");
            NextButtonPosition += new Vector2(0, Viewport.Height / 6);
        }

        protected virtual void AddSettingsScreenButton()
        {
            AddButton(NextButtonPosition, "Settings Button", GoToSettingsScreen, false, "Settings");
            NextButtonPosition += new Vector2(0, Viewport.Height / 6);
        }

        protected virtual void AddExitGameButton()
        {
            AddButton(NextButtonPosition, "Exit Game Button", ExitGame, false, "Exit To Desktop");
            NextButtonPosition += new Vector2(0, Viewport.Height / 6);
        }

        protected virtual void StartGame(object sender, EventArgs e)
        {
            // The game MainMenuScreen will override this with the appropriate screen to go to
        }

        protected virtual void LoadGame(object sender, EventArgs e)
        {
            // Add Load Game functionality here
            // Again may have to do this from the actual Game Project
        }

        protected virtual void GoToSettingsScreen(object sender, EventArgs e)
        {
            // TO DO
            // Add in changing to Options Screen here which we will create in this Engine
        }

        protected virtual void ExitGame(object sender, EventArgs e)
        {
            ScreenManager.Game.Exit();
        }

        #endregion

        #endregion
    }
}
