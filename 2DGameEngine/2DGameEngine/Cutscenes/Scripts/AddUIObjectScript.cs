using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Cutscenes.Scripts
{
    public class AddUIObjectScript : Script
    {
        #region Properties and Fields

        protected UIObject UIObject { get; set; }

        #endregion

        public AddUIObjectScript(UIObject uiobject, bool shouldUpdateGame = false, bool canRun = true)
            : base(shouldUpdateGame, canRun)
        {
            UIObject = uiobject;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadAndInit(ContentManager content)
        {
            UIObject.LoadContent();
            UIObject.Initialize();
        }

        public override void Run(GameTime gameTime)
        {
            UIObject.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        public override void DrawUI(SpriteBatch spriteBatch)
        {
            UIObject.Draw(spriteBatch);
        }

        public override void HandleInput()
        {
            UIObject.HandleInput();
        }

        public override void CheckDone()
        {
            Done = UIObject.Alive == false;
        }

        public override void IfDone()
        {
            base.IfDone();

            // Want to flush the input handler to avoid skipping over dialog boxes
            ScreenManager.GameMouse.Flush();
        }

        public override void PerformImmediately()
        {
            UIObject.Alive = false;
            Done = true;
        }

        #endregion
    }
}
