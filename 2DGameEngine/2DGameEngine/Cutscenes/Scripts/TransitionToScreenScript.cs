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
    public class TransitionToScreenScript : Script
    {
        #region Properties and Fields

        private BaseScreen CurrentScreen { get; set; }
        private BaseScreen ScreenToTransitionTo { get; set; }

        #endregion

        public TransitionToScreenScript(BaseScreen currentScreen, BaseScreen screenToTransitionTo, bool shouldUpdateGame = false, bool canRun = true)
            : base(shouldUpdateGame, canRun)
        {
            CurrentScreen = currentScreen;
            ScreenToTransitionTo = screenToTransitionTo;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadAndInit(ContentManager content)
        {
            ScreenToTransitionTo.LoadContent(content);
            ScreenToTransitionTo.Initialize();
        }

        public override void Run(GameTime gameTime)
        {
            CurrentScreen.Transition(ScreenToTransitionTo);
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
            CurrentScreen.Transition(ScreenToTransitionTo);
        }

        #endregion
    }
}
