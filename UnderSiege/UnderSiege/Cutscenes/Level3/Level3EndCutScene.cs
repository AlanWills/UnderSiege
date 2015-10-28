﻿using _2DGameEngine.Cutscenes;
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

            AddScript(new AddDialogBoxScript("Bushi dreadnought-class ship detected."));
            AddScript(new AddDialogBoxScript("We have neither the firepower, nor the craft to deal with such a ship."));
            AddScript(new AddDialogBoxScript("Captain, we have only one logical choice."));
            AddScript(new AddDialogBoxScript("We have finished all necessary repairs to your vessel."));
            AddScript(new AddDialogBoxScript("You should travel to Redoubt, the nearest UNI colony world."));
            AddScript(new AddDialogBoxScript("It is no doubt the Buthi's next target."));
            AddScript(new AddDialogBoxScript("The warning you can give them will save the lives of countless innocents."));
            AddScript(new AddDialogBoxScript("Admiral..."));
            AddScript(new AddDialogBoxScript("Captain, there is no time to talk about this."));
            AddScript(new AddDialogBoxScript("Now go!"));
        }

        protected override void CheckIsDone()
        {
            IsDone = ScriptManager.NoMoreScripts;
        }

        #endregion
    }
}
