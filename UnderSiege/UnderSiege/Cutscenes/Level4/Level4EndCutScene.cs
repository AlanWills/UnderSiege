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
            AddScript(new AddDialogBoxScript("We cannot systain any more damage."));
            AddScript(new AddDialogBoxScript("Shutting down all non-essential subsystems."));
            AddScript(new WaitScript(2, false));
            AddScript(new AddDialogBoxScript("Crew of the UNI vessel Saari, I am ordering a ship-wide evacuation."));
            AddScript(new AddDialogBoxScript("You have just minutes before this vessel will be destroyed."));
            AddScript(new AddDialogBoxScript("I suggest you make it quick."));
            AddScript(new WaitScript(1, false));
            AddScript(new AddDialogBoxScript("I will be staying behind in my duty as captain,\nand will assist you however I can."));
            AddScript(new AddDialogBoxScript("Good luck."));
            AddScript(new WaitScript(2, false));
            AddScript(new AddDialogBoxScript("UNI command, this is the captain of the UNI Saari."));
            AddScript(new WaitScript(2.5f, false));
            AddScript(new AddDialogBoxScript("Redoubt has fallen."));
            AddScript(new WaitScript(2, false));
        }

        protected override void CheckIsDone()
        {
            IsDone = ScriptManager.NoMoreScripts;
        }

        #endregion
    }
}
