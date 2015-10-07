using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderSiege.Screens
{
    public class UnderSiegeMainMenuScreen : MainMenuScreen
    {
        #region Properties and Fields

        #endregion

        public UnderSiegeMainMenuScreen(ScreenManager screenManager, bool addDefaultUI = true)
            : base(screenManager, addDefaultUI)
        {
            
        }

        #region Methods

        #endregion

        #region Virtual Methods

        protected override void AddStartGameButton()
        {
            AddButton(NextButtonPosition, "Start Game Button", StartGame, false, "Start Game");
            NextButtonPosition += new Vector2(0, Viewport.Height / 6);
        }

        protected override void StartGame(object sender, EventArgs e)
        {
            Transition(new UnderSiegeGameplayScreen(ScreenManager, "Data\\Screens\\GameplayScreen"));
        }

        #endregion
    }
}
