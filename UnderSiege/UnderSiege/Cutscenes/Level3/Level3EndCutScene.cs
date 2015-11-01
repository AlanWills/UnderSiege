using _2DGameEngine.Cutscenes;
using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderSiege.Cutscenes.Level3
{
    public class Level3EndCutScene : Cutscene
    {
        #region Properties and Fields

        #endregion

        public Level3EndCutScene(ScreenManager screenManager, string dataAsset, GameplayScreen gameplayScreen)
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

            AddScript(new WaitScript(5));
            AddScript(new AddDialogBoxScript("We have analysed the latest Bushi craft.", true, true, true, null, 3.0f));
            AddScript(new AddDialogBoxScript("They are extremely powerful dreadnought-class ships.", true, true, true, null, 3.0f));
            AddScript(new AddDialogBoxScript("We have neither the firepower, nor the craft to deal with such these vessels.", true, true, true, null, 3.0f));
            AddScript(new AddDialogBoxScript("Captain, we have only one logical choice.", true, true, true, null, 3.0f));
            AddScript(new AddDialogBoxScript("We have finished all necessary repairs to your vessel.", true, true, true, null, 3.0f));
            AddScript(new AddDialogBoxScript("You must travel to Redoubt, the nearest UNI colony world.", true, true, true, null, 3.0f));
            AddScript(new AddDialogBoxScript("It is no doubt the Buthi's next target.", true, true, true, null, 3.0f));
            AddScript(new AddDialogBoxScript("The warning you can give them will save the lives of countless innocents.", true, true, true, null, 3.0f));
            AddScript(new AddDialogBoxScript("Admiral...", true, true, true, null, 3.0f));
            AddScript(new AddDialogBoxScript("Captain, there is no time to talk about this.", true, true, true, null, 3.0f));
            AddScript(new AddDialogBoxScript("Now go!", true, true, true, null, 3.0f));
            AddScript(new WaitScript(2));
        }

        protected override void CheckIsDone()
        {
            IsDone = ScriptManager.NoMoreScripts;
        }

        #endregion
    }
}
