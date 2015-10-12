using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Cutscenes.Scripts
{
    public class WaitScript : Script
    {
        #region Properties and Fields

        private float WaitTime { get; set; }

        private float currentTime = 0;
        
        #endregion

        public WaitScript(float waitTime, bool shouldUpdateGame = true, bool canRun = true)
            : base(shouldUpdateGame, canRun)
        {
            WaitTime = waitTime;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadAndInit(ContentManager content)
        {
            
        }

        public override void Run(GameTime gameTime)
        {
            currentTime += (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;
        }

        public override void CheckShouldUpdateGame()
        {
            
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

        public override void CheckDone()
        {
            Done = currentTime >= WaitTime;
        }

        public override void PerformImmediately()
        {
            currentTime = WaitTime;
            Done = true;
        }

        #endregion
    }
}
