using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Cutscenes.Scripts
{
    public class RunEventScript : Script
    {
        #region Properties and Fields

        private event EventHandler EventToFulfill;

        #endregion

        public RunEventScript(EventHandler eventToFulfill, bool shouldUpdateGame = true, bool canRun = true)
            : base(shouldUpdateGame, canRun)
        {
            EventToFulfill = eventToFulfill;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadAndInit(ContentManager content)
        {
            
        }

        public override void Run(GameTime gameTime)
        {
            if (EventToFulfill != null)
            {
                EventToFulfill(this, EventArgs.Empty);
            }
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
            
        }

        public override void PerformImmediately()
        {
            Done = true;
        }

        #endregion
    }
}
