using _2DGameEngine.Cutscenes;
using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Screens;

namespace UnderSiege.Cutscenes.Level_2
{
    public class Level2StartCutScene : Cutscene
    {
        #region Properties and Fields

        #endregion

        #region Methods

        public Level2StartCutScene(ScreenManager screenManager, string dataAsset, GameplayScreen gameplayScreen)
            : base(screenManager, dataAsset, gameplayScreen)
        {
            
        }

        #endregion

        #region Virtual Methods

        protected override void AddScripts()
        {
            
        }

        protected override void CheckIsDone()
        {
            IsDone = ScriptManager.NoMoreScripts;
        }

        #endregion

        #region Events

        private void ActivateWaveManager(object sender, EventArgs e)
        {
            (GameplayScreen as UnderSiegeGameplayScreen).WaveManager.Paused = false;
            (sender as Script).Done = true;
        }

        #endregion
    }
}
