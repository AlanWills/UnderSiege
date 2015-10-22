using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Screens
{
    public class GameplayScreenPauseMenu : Menu
    {
        #region Properties and Fields

        private const int padding = 10;
        protected int numberOfButtons = 0;

        #endregion

        public GameplayScreenPauseMenu()
            : base(ScreenManager.ScreenCentre)
        {
            AddUI();
        }

        #region Methods

        public virtual void AddUI()
        {
            Button resumeGameButton = new Button(new Vector2(0, -ScreenManager.ScreenCentre.Y * 0.5f), "Resume Game", this);
            resumeGameButton.OnSelect += resumeGameButton_OnSelect;
            AddUIObject(resumeGameButton, "Resume Game Button");

            numberOfButtons++;

            Button quitToMainMenu = new Button(new Vector2(0, Button.defaultTexture.Height + padding), "Exit To Main Menu", resumeGameButton);
            quitToMainMenu.OnSelect += quitToMainMenu_OnSelect;
            AddUIObject(quitToMainMenu, "Quit To Main Menu Button");

            numberOfButtons++;

            Button exitGameButton = new Button(new Vector2(0, Button.defaultTexture.Height + padding), "Exit Game", quitToMainMenu);
            exitGameButton.OnSelect += exitGameButton_OnSelect;
            AddUIObject(exitGameButton, "Exit Game Button");

            numberOfButtons++;
        }

        public void SetSizeAndPosition()
        {
            Size = new Vector2(2 * padding + Button.defaultTexture.Width, padding + numberOfButtons * (padding + Button.defaultTexture.Height));

            UIObject resumeGameButton = UIManager.GetItem("Resume Game Button");
            resumeGameButton.LocalPosition = new Vector2(0, -Size.Y * 0.5f + padding + Button.defaultTexture.Height * 0.5f);
        }

        #endregion

        #region Events

        void resumeGameButton_OnSelect(object sender, EventArgs e)
        {
            Alive = false;
        }

        void quitToMainMenu_OnSelect(object sender, EventArgs e)
        {
            Alive = false;
        }

        void exitGameButton_OnSelect(object sender, EventArgs e)
        {
            Alive = false;
            ScreenManager.Game.Exit();
        }

        #endregion

        #region Virtual Methods

        public override void Initialize()
        {
            // Important we do this first before creating the colliders
            SetSizeAndPosition();

            base.Initialize();
        }

        #endregion
    }
}
