using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Cutscenes.Level4;
using UnderSiege.Player_Data;

namespace UnderSiege.Screens.Level_Screens
{
    public class UnderSiegeGameplayScreenLevel4 : UnderSiegeGameplayScreen
    {
        #region Properties and Fields

        #endregion

        public UnderSiegeGameplayScreenLevel4(ScreenManager screenManager)
            : base(screenManager, "Data\\Screens\\LevelScreens\\Level4")
        {
            Session.Money = 3000;
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

            AddScript(new TransitionToScreenScript<UnderSiegeCreditsScreen>(this));
        }

        #endregion

        #region

        void endCutScene_CanRunEvent(object sender, EventArgs e)
        {
            (sender as Script).CanRun = CommandShip.CurrentHealth <= 50;
        }

        #endregion
    }
}
