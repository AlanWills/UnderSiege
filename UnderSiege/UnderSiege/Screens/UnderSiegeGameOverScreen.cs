using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderSiege.Screens
{
    public class UnderSiegeGameOverScreen<T> : GameOverScreen where T : UnderSiegeGameplayScreen
    {
        #region Properties and Fields

        #endregion

        public UnderSiegeGameOverScreen(ScreenManager screenManager)
            : base(screenManager, "Data\\Screens\\GameOverScreen")
        {

        }

        #region Methods

        #endregion

        #region Virtual Methods

        protected override void backToMainMenuButton_OnSelect(object sender, EventArgs e)
        {
            Transition(new UnderSiegeMainMenuScreen(ScreenManager));
        }

        protected override void restartLevelButton_OnSelect(object sender, EventArgs e)
        {
            Transition((T)Activator.CreateInstance(typeof(T), ScreenManager));
        }

        #endregion
    }
}
