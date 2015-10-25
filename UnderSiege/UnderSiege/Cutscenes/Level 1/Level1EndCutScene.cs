using _2DGameEngine.Cutscenes;
using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderSiege.Cutscenes
{
    public class Level1EndCutScene : Cutscene
    {
        #region Properties and Fields

        #endregion

        public Level1EndCutScene(ScreenManager screenManager, string dataAsset, GameplayScreen gameplayScreen)
            : base(screenManager, dataAsset, gameplayScreen)
        {
            
        }

        #region Methods

        #endregion

        #region Virtual Methods

        protected override void AddScripts()
        {
            AddScript(new WaitScript(5));
            AddScript(new AddDialogBoxScript("Sir, the energy readings have dissipated."));
            AddScript(new AddDialogBoxScript("During the battle, our scientists were able to\ndeciper the information from the scans we performed."));
            AddScript(new AddDialogBoxScript("They believe that the readings were\na type of quantum waveform."));
            AddScript(new AddDialogBoxScript("The concept has been theorised,\nbut no-one has ever managed to\nsuccessfully stabilise a sample."));
            AddScript(new AddDialogBoxScript("If one ever existed, it could be used to \ninstantly transport matter through great distances."));
            AddScript(new AddDialogBoxScript("This attack was no mere band of renegades,\nwe must get as much information as we can \nto know what we are dealing with."));
            AddScript(new AddDialogBoxScript("A small craft has been outfitted for your\nimmediate departure to the nearest UNI base."));
            AddScript(new AddDialogBoxScript("I do not know what will happen when you arrive,\nbut rest assured that this station's safety\nwill be my personal responsibility."));
            AddScript(new AddDialogBoxScript("Good luck sir."));
        }

        protected override void CheckIsDone()
        {
            IsDone = ScriptManager.NoMoreScripts;
        }

        #endregion
    }
}
