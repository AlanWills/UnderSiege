using _2DGameEngine.Cutscenes;
using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;
using UnderSiege.Screens;
using UnderSiege.Screens.Level_Screens;
using UnderSiege.UI.HUD_Menus;

namespace UnderSiege.Cutscenes.Level_2
{
    public class Level2StartCutScene : Cutscene
    {
        #region Properties and Fields

        #endregion

        public Level2StartCutScene(ScreenManager screenManager, string dataAsset, GameplayScreen gameplayScreen)
            : base(screenManager, dataAsset, gameplayScreen)
        {
            
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void AddScripts()
        {
            base.AddScripts();

            AddDialogBoxScript.defaultPosition = new Vector2(Viewport.Width * 0.75f, Viewport.Height * 0.75f);

            AddScript(new AddDialogBoxScript("WARNING!"));
            AddScript(new AddDialogBoxScript("Unexpected hyperdrive malfunction."));
            AddScript(new AddDialogBoxScript("Coolant leak detected.\nContainment protocols enacted."));
            AddScript(new AddDialogBoxScript("No backup impulse engines detected.\nTaking navigation systems offline."));
            AddScript(new RunEventScript(ActivateWaveManager));
            AddScript(new WaitScript(1));
            AddScript(new AddDialogBoxScript("Attention crew, this is the captain speaking."));
            AddScript(new AddDialogBoxScript("We have experienced a severe problem with our hyperdrive."));
            AddScript(new AddDialogBoxScript("I am bringing our status up to yellow alert.", true, true, true));
            AddScript(new AddDialogBoxScript("I don't know what has caused this,\nbut I will attempt to find a solution.", true, true, true));
            AddScript(new RunEventScript(OnFirstWaveSpawned));
            AddScript(new AddDialogBoxScript("Hostiles!"));
            AddScript(new AddDialogBoxScript("What are they DOING here!"));
            AddScript(new AddDialogBoxScript("Bringing the ship up to red alert.\nEngineering, all power to defence systems."));
            AddScript(new AddDialogBoxScript("Looks like we've got a fight on our hands."));
            AddScript(new WaitScript(90));
            AddScript(new AddDialogBoxScript("Attention crew.", true, true));
            AddScript(new AddDialogBoxScript("Working with an engineering team,\nwe have devised an exit strategy.", true, true));
            AddScript(new AddDialogBoxScript("They are confident that the ship can\nbe outfitted with make-shift thrusters.", true, true));
            AddScript(new AddDialogBoxScript("Calculations have estimated that we require\n3 engines to reach the nearest exit gate.", true, true));
            AddScript(new AddDialogBoxScript("I will issue construction orders\nas soon as I am able.", true, true));
            AddScript(new ShowAndActivateObjectScript(UnderSiegeGameplayScreen.HUD.GetUIObject<BuyShipEngineMenu>("Buy Engines UI")));
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