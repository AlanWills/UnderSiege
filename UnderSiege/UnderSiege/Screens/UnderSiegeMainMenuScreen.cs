﻿using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderSiege.Screens
{
    public class UnderSiegeMainMenuScreen : MainMenuScreen
    {
        #region Properties and Fields

        #endregion

        public UnderSiegeMainMenuScreen(ScreenManager screenManager, bool addDefaultUI = true)
            : base(screenManager, addDefaultUI)
        {
            
        }

        #region Methods

        #endregion

        #region Virtual Methods

        protected override void AddStartGameButton()
        {
            AddButton(NextButtonPosition, "Start Game Button", AddPlayTutorialDialogBox, false, "Start Game");
            NextButtonPosition += new Vector2(0, Viewport.Height / 6);
        }

        private void AddPlayTutorialDialogBox(object sender, EventArgs e)
        {
            ScriptManager.AddScript(new AddOptionsDialogBoxScript("Play Tutorial?", ScreenCentre, PlayGame, PlayTutorial, "No", "Yes"));
        }

        private void PlayGame(object sender, EventArgs e)
        {
            Transition(new UnderSiegeGameplayScreen(ScreenManager, "Data\\Screens\\GameplayScreen"));
        }

        private void PlayTutorial(object sender, EventArgs e)
        {
            Transition(new UnderSiegeGameplayTutorialScreen(ScreenManager, "Data\\Screens\\GameplayTutorialScreen"));
        }

        public override void Begin(GameTime gameTime)
        {
            base.Begin(gameTime);

            MusicManager.AddSong("Dream Raid");
        }

        #endregion
    }
}
