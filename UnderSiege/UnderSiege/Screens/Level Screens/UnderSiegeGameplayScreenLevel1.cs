﻿using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using _2DGameEngine.UI_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Cutscenes;
using UnderSiege.UI.HUD_Menus;

namespace UnderSiege.Screens.Level_Screens
{
    public class UnderSiegeGameplayScreenLevel1 : UnderSiegeGameplayScreen
    {
        #region Properties and Fields

        #endregion

        public UnderSiegeGameplayScreenLevel1(ScreenManager screenManager, string dataAsset = "Data\\Screens\\LevelScreens\\Level1")
            : base(screenManager, dataAsset)
        {
            WaveManager.Paused = true;

            HUD.GetUIObject<BuyShipEngineMenu>("Buy Engines UI").Active = false;
            HUD.GetUIObject<BuyShipEngineMenu>("Buy Engines UI").Visible = false;
            HUD.GetUIObject<Button>("Buy Ships Button").Active = false;
            HUD.GetUIObject<Button>("Buy Ships Button").Visible = false;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Initialize()
        {
            base.Initialize();

            AddScript(new AddCutsceneScript(new Level1StartCutScene(ScreenManager, "Data\\Screens\\LevelScreens\\Level1", this), this));

            AddCutsceneScript endCutScene = new AddCutsceneScript(new Level1EndCutScene(ScreenManager, "Data\\Screens\\LevelScreens\\Level1End", this), this);
            endCutScene.CanRunEvent += endOfLevelCutscene;
            AddScript(endCutScene);

            AddScript(new TransitionToScreenScript(this, new UnderSiegeGameplayScreenLevel2(ScreenManager)));
        }

        #endregion

        #region Events

        private void endOfLevelCutscene(object sender, EventArgs e)
        {
            (sender as Script).CanRun = WaveManager.Waves.Count == 0 && UnderSiegeGameplayScreen.Enemies.Count == 0;
        }

        #endregion
    }
}