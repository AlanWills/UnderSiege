using _2DGameEngine.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Screens;

namespace UnderSiege.UI
{
    public class UnderSiegeGameplayScreenPauseMenu : GameplayScreenPauseMenu
    {
        #region Properties and Fields

        #endregion

        public UnderSiegeGameplayScreenPauseMenu(BaseScreen currentScreen)
            : base(currentScreen)
        {

        }

        #region Methods

        #endregion

        #region Virtual Methods

        protected override void exitToMainMenu_OnSelect(object sender, EventArgs e)
        {
            Alive = false;
            currentScreen.Transition(new UnderSiegeMainMenuScreen(currentScreen.ScreenManager));
        }

        #endregion
    }
}
