using _2DGameEngine.Cutscenes;
using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderSiege.Cutscenes.Level_2
{
    public class Level2EndCutScene : Cutscene
    {
        #region Properties and Fields

        #endregion

        public Level2EndCutScene(ScreenManager screenManager, string dataAsset, GameplayScreen gameplayScreen)
            : base(screenManager, dataAsset, gameplayScreen)
        {
            
        }

        #region Methods

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
    }
}
