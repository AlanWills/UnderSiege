using _2DGameEngine.Cutscenes;
using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Screens;

namespace UnderSiege.Cutscenes
{
    public class BeginGameCutscene : Cutscene
    {
        #region Properties and Fields

        private UnderSiegeGameplayScreen GameplayScreen { get; set; }

        #endregion

        public BeginGameCutscene(ScreenManager screenManager, UnderSiegeGameplayScreen gameplayScreen, string dataAsset)
            : base(screenManager, dataAsset)
        {
            GameplayScreen = gameplayScreen;
            Camera.Position = new Vector2(0, -1000);
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Initialize()
        {
            base.Initialize();
            
            AddDialogBoxScript welcomeBox = new AddDialogBoxScript(ScriptManager, "Welcome To Under Siege", ScreenCentre, true);
            AddScript(welcomeBox);
            AddDialogBoxScript nextBox = new AddDialogBoxScript(ScriptManager, "As Commander for this sector's defence systems,\nyou must prevent ANY enemy incursion into civilian space.", ScreenCentre);
            AddScript(nextBox, welcomeBox);
        }

        protected override void CheckIsDone()
        {
            IsDone = ScriptManager.NoMoreScripts;
        }

        #endregion
    }
}
