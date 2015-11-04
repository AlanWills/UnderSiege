using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Screens.Level_Screens;

namespace UnderSiege.Screens
{
    public class UnderSiegeMainMenuScreen : MainMenuScreen
    {
        #region Properties and Fields

        #endregion

        public UnderSiegeMainMenuScreen(ScreenManager screenManager)
            : base(screenManager, true)
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

        protected override void LoadGame(object sender, EventArgs e)
        {
            switch (Options.Level)
            {
                case 1:
                    Transition(new LoadingScreen<UnderSiegeGameplayScreenLevel1>(ScreenManager));
                    break;
                case 2:
                    Transition(new LoadingScreen<UnderSiegeGameplayScreenLevel2>(ScreenManager));
                    break;
                case 3:
                    Transition(new LoadingScreen<UnderSiegeGameplayScreenLevel3>(ScreenManager));
                    break;
                case 4:
                    Transition(new LoadingScreen<UnderSiegeGameplayScreenLevel4>(ScreenManager));
                    break;
            }
        }

        private void AddPlayTutorialDialogBox(object sender, EventArgs e)
        {
            ScriptManager.AddScript(new AddOptionsDialogBoxScript("Play Tutorial?", ScreenCentre, PlayGame, PlayTutorial, "No", "Yes"));
        }

        private void PlayGame(object sender, EventArgs e)
        {
            Transition(new LoadingScreen<DebugScreen>(ScreenManager));
        }

        private void PlayTutorial(object sender, EventArgs e)
        {
            Transition(new LoadingScreen<UnderSiegeGameplayTutorialScreen>(ScreenManager));
        }

        #endregion
    }
}
