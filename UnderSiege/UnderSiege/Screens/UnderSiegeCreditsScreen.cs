using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderSiege.Screens
{
    public class UnderSiegeCreditsScreen : MenuScreen
    {
        #region Properties and Fields

        #endregion

        public UnderSiegeCreditsScreen(ScreenManager screenManager, string dataAsset = "Data\\Screens\\TrailerFadeOutScreen")
            : base(screenManager, dataAsset)
        {
            
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Initialize()
        {
            base.Initialize();

            AddScript(new AddUIObjectScript(new Label("To be continued...", ScreenCentre, Color.Cyan, null, 5.0f)));
            AddScript(new TransitionToScreenScript<UnderSiegeMainMenuScreen>(this));
        }

        #endregion
    }
}
