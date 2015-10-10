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

        protected ScriptManager ScriptManager { get; private set; }

        public bool CanRun { get; set; }

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

        public event EventHandler CanRunEvent;

        #endregion

        public Script(ScriptManager scriptManager)
        {
            ScriptManager = scriptManager;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public abstract void LoadAndInit(ContentManager content);

        public virtual void CheckCanRun()
        {
            if (CanRunEvent != null)
            {
                CanRunEvent(this, EventArgs.Empty);
            }
        }

        public abstract void Update(GameTime gameTime);
        public virtual bool ShouldUpdateGame()
        {
            return true;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void DrawUI(SpriteBatch spriteBatch);

        public abstract void HandleInput();

        public virtual void CheckDone()
        {
            Done = true;
        }

        public virtual void IfDone()
        {

        }

        #endregion
    }
}
