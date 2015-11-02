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
using UnderSiege.Player_Data;

namespace UnderSiege.Screens
{
    public class UnderSiegeTrailer : UnderSiegeGameplayScreen
    {
        #region Properties and Fields

        #endregion

        public UnderSiegeTrailer(ScreenManager screenManager)
            : base(screenManager, "Data\\Screens\\Trailer")
        {
            Session.Money = 150000;
            WaveManager.Paused = true;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void AddScripts()
        {
            base.AddScripts();

            AddDialogBoxScript.defaultPosition = new Vector2(Viewport.Width * 0.25f, Viewport.Height * 0.25f);

            AddScript(new RunEventScript(WaitUntilAllDefencesBuilt));
            AddScript(new RunEventScript(ActivateWaveManager));
            AddScript(new WaitScript(3));
            AddScript(new AddDialogBoxScript("Multiple hostiles directly off our battle cluster.", false, true, false, null, 2.0f));
            AddScript(new WaitScript(1, false));
            AddScript(new AddDialogBoxScript("Powering up defence grid.", false, true, false, null, 2.0f));
            AddScript(new WaitScript(1, false));
            AddScript(new AddDialogBoxScript("Fire.", false, true, false, null, 2.0f));
            AddScript(new WaitScript(30));
            AddScript(new RunEventScript(FadeOut));
        }

        #endregion

        #region Events

        private void WaitUntilAllDefencesBuilt(object sender, EventArgs e)
        {
            (sender as Script).Done = CommandShip.ShipAddOns.Count == (CommandShip.ShipData.OtherHardPoints.Count + CommandShip.ShipData.EngineHardPoints.Count);
        }

        private void ActivateWaveManager(object sender, EventArgs e)
        {
            HUD.DisableAndHideAll();

            WaveManager.Paused = false;
            (sender as Script).Done = true;
        }

        private void FadeOut(object sender, EventArgs e)
        {
            Transition(new UnderSiegeTrailerFadeOutScreen(ScreenManager));
            (sender as Script).Done = true;
        }

        #endregion
    }

    public class UnderSiegeTrailerFadeOutScreen : MenuScreen
    {
        #region Properties and Fields

        #endregion

        public UnderSiegeTrailerFadeOutScreen(ScreenManager screenManager, string dataAsset = "Data\\Screens\\TrailerFadeOutScreen")
            : base(screenManager, dataAsset)
        {
            
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Initialize()
        {
            base.Initialize();

            AddScript(new AddUIObjectScript(new Label("Looks like we're just getting started...", ScreenCentre, Color.Cyan, null, 5.0f)));
            AddScript(new AddUIObjectScript(new Label("Under Siege", ScreenCentre, Color.Cyan)));
        }

        #endregion
    }
}
