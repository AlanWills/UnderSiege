using _2DGameEngine.Extra_Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Cutscenes.Scripts
{
    public abstract class Script
    {
        #region Properties and Fields

        public bool CanRun { get; set; }
        public bool ShouldUpdateGame { get; set; }
        public bool Running { get; private set; }

        private bool done = false;
        public bool Done 
        {
            get { return done; }
            set
            {
                done = value;
                if (done)
                    IfDone();
            }
        }

        public Script PreviousScript { get; set; }
        public float TimeRunFor { get; private set; }

        public event EventHandler CanRunEvent;
        public event EventHandler OnEndEvent;

        #endregion

        public Script(bool shouldUpdateGame = true, bool canRun = true)
        {
            CanRun = canRun;
            ShouldUpdateGame = shouldUpdateGame;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public abstract void LoadAndInit(ContentManager content);

        public virtual void CheckCanRun()
        {
            if (Running)
                return;

            if (PreviousScript != null)
            {
                CanRun = PreviousScript.Done;
            }

            if (CanRunEvent != null)
            {
                CanRunEvent(this, EventArgs.Empty);
            }
        }

        public virtual void Run(GameTime gameTime)
        {
            Running = true;
            TimeRunFor += (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;
        }

        public virtual void CheckShouldUpdateGame()
        {
            
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void DrawUI(SpriteBatch spriteBatch);

        public abstract void HandleInput();

        public abstract void CheckDone();

        public virtual void IfDone()
        {
            if (OnEndEvent != null)
            {
                OnEndEvent(this, EventArgs.Empty);
            }
        }

        // This function is designed for skipping through tutorials etc. - allows us to perform the action of the script instantaneously
        public abstract void PerformImmediately();

        #endregion
    }
}
