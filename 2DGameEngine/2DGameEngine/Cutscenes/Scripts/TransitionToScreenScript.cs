using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Cutscenes.Scripts
{
    public class TransitionToScreenScript<T> : Script where T : BaseScreen
    {
        #region Properties and Fields

        private BaseScreen CurrentScreen { get; set; }

        #endregion

        public TransitionToScreenScript(BaseScreen currentScreen, bool shouldUpdateGame = false, bool canRun = true)
            : base(shouldUpdateGame, canRun)
        {
            CurrentScreen = currentScreen;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadAndInit(ContentManager content)
        {
            
        }

        public override void Run(GameTime gameTime)
        {
            // Currently have to do this like this, because otherwise it loads EVERYTHING at the start of the first screen and fucks everything up
            CurrentScreen.Transition(new LoadingScreen<T>(CurrentScreen.ScreenManager));
        }

        public override void CheckShouldUpdateGame()
        {
            ShouldUpdateGame = false;
        }

        public override void CheckDone()
        {
            Done = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public override void DrawUI(SpriteBatch spriteBatch)
        {
            
        }

        public override void HandleInput()
        {
            
        }

        public override void PerformImmediately()
        {
            CurrentScreen.Transition(new LoadingScreen<T>(CurrentScreen.ScreenManager));
        }

        #endregion
    }
}
