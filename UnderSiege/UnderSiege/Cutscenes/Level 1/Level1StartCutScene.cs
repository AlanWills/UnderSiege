using _2DGameEngine.Cutscenes;
using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Screens;

namespace UnderSiege.Cutscenes
{
    public class Level1StartCutScene : Cutscene
    {
        #region Properties and Fields

        #endregion

        #region Methods

        public Level1StartCutScene(ScreenManager screenManager, string dataAsset, GameplayScreen gameplayScreen)
            : base(screenManager, dataAsset, gameplayScreen)
        {
            
        }

        #endregion

        #region Virtual Methods

        public override void AddScripts()
        {
            AddScript(new AddDialogBoxScript("Sir, this is Lieutenant Drake\nwith your daily report."));
            AddScript(new AddDialogBoxScript("We've had strange energy readings\nall around the sector,\nbut no activity out of the ordinary."));
            AddScript(new AddDialogBoxScript("They are probably just residual trails\nleft from a hyperspace jump."));
            AddScript(new AddDialogBoxScript("I will make sure we keep monitoring them."));
            AddScript(new AddDialogBoxScript("In the mean time, it would be best to begin\nconstruction on new defences where we can."));
            AddScript(new WaitScript(15));
            AddScript(new AddDialogBoxScript("We have completed analysis on those readings sir."));
            AddScript(new AddDialogBoxScript("They do not appear to be residual energy,\n or any energy waveform that we now of."));
            AddScript(new AddDialogBoxScript("It appears that we are dealing with an unknown phenomena."));
            AddScript(new RunEventScript(ActivateWaveManager));
            AddScript(new RunEventScript(OnFirstWaveSpawned));
            AddScript(new AddDialogBoxScript("Those are not unknown phenomena!"));
            AddScript(new AddDialogBoxScript("Those are enemy ships!"));
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

        private void OnFirstWaveSpawned(object sender, EventArgs e)
        {
            (sender as Script).Done = UnderSiegeGameplayScreen.Enemies.Count > 0;
        }

        #endregion
    }
}
