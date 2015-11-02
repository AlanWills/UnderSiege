using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using _2DGameEngine.UI_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Cutscenes;
using UnderSiege.Player_Data;
using UnderSiege.UI.HUD_Menus;

namespace UnderSiege.Screens.Level_Screens
{
    public class UnderSiegeGameplayScreenLevel1 : UnderSiegeGameplayScreen
    {
        #region Properties and Fields

        #endregion

        public UnderSiegeGameplayScreenLevel1(ScreenManager screenManager)
            : base(screenManager, "Data\\Screens\\LevelScreens\\Level1")
        {
            //Session.Money = 1200;
            Session.Money = 3500;
            WaveManager.Paused = true;

            HUD.DisableAndHideUIObject("Buy Engines UI");
            HUD.DisableAndHideUIObject("Buy Ships Button");
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void AddScripts()
        {
            base.AddScripts();

            AddScript(new AddCutsceneScript(new Level1StartCutScene(ScreenManager, "Data\\Screens\\LevelScreens\\Level1", this), this));

            TransitionToScreenScript<UnderSiegeGameOverScreen<UnderSiegeGameplayScreenLevel1>> onCommandShipDeath = new TransitionToScreenScript<UnderSiegeGameOverScreen<UnderSiegeGameplayScreenLevel1>>(this);
            onCommandShipDeath.CanRunEvent += checkCommandShipDead;
            AddScript(onCommandShipDeath);

            AddCutsceneScript endCutScene = new AddCutsceneScript(new Level1EndCutScene(ScreenManager, "Data\\Screens\\LevelScreens\\Level1End", this), this);
            endCutScene.CanRunEvent += endOfLevelCutscene;
            AddScript(endCutScene);

            AddScript(new TransitionToScreenScript<UnderSiegeGameplayScreenLevel2>(this));
        }

        #endregion

        #region Events

        private void checkCommandShipDead(object sender, EventArgs e)
        {
            (sender as Script).CanRun = CommandShip.Alive == false;
        }

        private void endOfLevelCutscene(object sender, EventArgs e)
        {
            (sender as Script).CanRun = WaveManager.Waves.Count == 0 && UnderSiegeGameplayScreen.Enemies.Count == 0;
        }

        #endregion
    }
}