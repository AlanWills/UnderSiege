using _2DGameEngine.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Player_Data;

namespace UnderSiege.Screens
{
    public class DebugScreen : UnderSiegeGameplayScreen
    {
        #region Properties and Fields

        #endregion

        public DebugScreen(ScreenManager screenManager)
            : base(screenManager, "Data\\Screens\\Debug")
        {
            Session.Money = 15000000;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        #endregion
    }
}
