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

        public bool CanRun { get; private set; }
        public bool Done { get; private set; }

        #endregion

        #region Methods

        #endregion

        #region Virtual Methods

        public virtual void LoadAndInit(ContentManager content);

        public virtual void CheckCanRun()
        {
            CanRun = true;
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

        #endregion
    }
}
