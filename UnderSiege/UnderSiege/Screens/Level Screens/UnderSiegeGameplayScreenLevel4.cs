using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Cutscenes.Level4;

namespace UnderSiege.Screens.Level_Screens
{
    public class UnderSiegeGameplayScreenLevel4 : UnderSiegeGameplayScreen
    {
        #region Properties and Fields

        #endregion

        public UnderSiegeGameplayScreenLevel4(ScreenManager screenManager, string dataAsset = "Data\\Screens\\LevelScreens\\Level4")
            : base(screenManager, dataAsset)
        {
            WaveManager.Paused = true;
            WaveManager.Continuous = true;

            HUD.DisableAndHideUIObject("Buy Ships Button");
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void AddScripts()
        {
            base.AddScripts();

            AddScript(new AddCutsceneScript(new Level4StartCutScene(ScreenManager, "Data\\Screens\\LevelScreens\\Level4", this), this));

            AddCutsceneScript endCutScene = new AddCutsceneScript(new Level4EndCutScene(ScreenManager, "Data\\Screens\\LevelScreens\\Level4", this), this);
            endCutScene.CanRunEvent += endCutScene_CanRunEvent;
            AddScript(endCutScene);

            AddScript(new TransitionToScreenScript(this, new UnderSiegeCreditsScreen(ScreenManager)));
        }

        #endregion

        #region

        void endCutScene_CanRunEvent(object sender, EventArgs e)
        {
            (sender as Script).CanRun = UnderSiegeGameplayScreen.CommandShip.CurrentHealth <= 50;
        }

        #endregion
    }
}
