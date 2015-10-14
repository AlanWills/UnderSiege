using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Cutscenes.Scripts
{
    public class AddDialogBoxScript : Script
    {
        #region Properties and Fields

        protected DialogBox DialogBox { get; set; }

        #endregion

        public AddDialogBoxScript(string text, Vector2 localPosition, bool canRun = true, BaseObject parent = null, float lifeTime = float.MaxValue, bool shouldUpdateGame = false)
            : base(shouldUpdateGame, canRun)
        {
            DialogBox = new DialogBox(text, localPosition, "Sprites\\UI\\Menus\\DialogBox", parent, lifeTime);
        }

        public AddDialogBoxScript(string text, Vector2 localPosition, Vector2 size, bool canRun = true, BaseObject parent = null, float lifeTime = float.MaxValue, bool shouldUpdateGame = false)
            : base(shouldUpdateGame, canRun)
        {
            DialogBox = new DialogBox(text, localPosition, size, "Sprites\\UI\\Menus\\DialogBox", parent, lifeTime);
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadAndInit(ContentManager content)
        {
            DialogBox.LoadContent();
            DialogBox.Initialize();
        }

        public override void Run(GameTime gameTime)
        {
            DialogBox.Update(gameTime);
        }

        public override void CheckShouldUpdateGame()
        {
            ShouldUpdateGame = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public override void DrawUI(SpriteBatch spriteBatch)
        {
            DialogBox.Draw(spriteBatch);
        }

        public override void HandleInput()
        {
            DialogBox.HandleInput();
        }

        public override void CheckDone()
        {
            Done = DialogBox.Alive == false || GameMouse.IsLeftClicked;
        }

        public override void IfDone()
        {
            base.IfDone();

            // Want to flush the input handler to avoid skipping over dialog boxes
            GameMouse.Flush();
        }

        public override void PerformImmediately()
        {
            Done = true;
        }

        #endregion
    }
}
