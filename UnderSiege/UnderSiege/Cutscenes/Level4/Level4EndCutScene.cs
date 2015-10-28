using _2DGameEngine.Cutscenes;
using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderSiege.Cutscenes.Level4
{
    public class Level4EndCutScene : Cutscene
    {
        #region Properties and Fields

        #endregion

        public Level4EndCutScene(ScreenManager screenManager, string dataAsset, GameplayScreen gameplayScreen)
            : base(screenManager, dataAsset, gameplayScreen)
        {
            
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void AddScripts()
        {
            base.AddScripts();

            AddDialogBoxScript.defaultPosition = new Vector2(Viewport.Width * 0.75f, Viewport.Height * 0.25f);

            AddScript(new AddDialogBoxScript("Captain, the command ship's hull is critical."));
        }

        protected override void CheckIsDone()
        {
            IsDone = ScriptManager.NoMoreScripts;
        }

        #endregion
    }
}
