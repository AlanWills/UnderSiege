using _2DGameEngine.Cutscenes;
using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using Microsoft.Xna.Framework;
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

        public override void AddScripts()
        {
            base.AddScripts();

            AddDialogBoxScript.defaultPosition = new Vector2(Viewport.Width * 0.75f, Viewport.Height * 0.75f);

            AddScript(new AddDialogBoxScript("Damn.", true, true));
            AddScript(new AddDialogBoxScript("The gate appears to be offline.", true, true));
            AddScript(new AddDialogBoxScript("Engineering, run a scan and see if we can activate it somehow.", true, true));
            AddScript(new WaitScript(5));
            AddScript(new AddDialogBoxScript("Captain, this is Chief Engineer Troth.", true, true));
            AddScript(new AddDialogBoxScript("We can override the gate's systems,\nbut we are going to need TIME.", true, true));
            AddScript(new AddDialogBoxScript("How long do you need Troth?", true, true));
            AddScript(new AddDialogBoxScript("As much as you can give me captain.", true, true));
            AddScript(new WaitScript(20));
            AddScript(new AddDialogBoxScript("Captain, I have accessed the gate's internal systems.", true, true));
            AddScript(new AddDialogBoxScript("We should be able to leave shortly.", true, true));
            AddScript(new WaitScript(15));
            AddScript(new AddDialogBoxScript("Hyperspace gate fully functional.", true, true));
            AddScript(new AddDialogBoxScript("Now let's get out of here!", true, true));
        }

        protected override void CheckIsDone()
        {
            IsDone = ScriptManager.NoMoreScripts;
        }

        #endregion
    }
}