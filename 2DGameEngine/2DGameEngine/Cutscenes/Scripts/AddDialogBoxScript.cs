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
        protected Button NextDialogButton { get; private set; }
        protected Boolean Skippable { get; set; }

        private static Vector2 defaultPosition;

        #endregion

        public AddDialogBoxScript(string text, bool skippable = true, bool canRun = true, bool shouldUpdateGame = false, BaseObject parent = null, float lifeTime = 7.0f)
            : base(shouldUpdateGame, canRun)
        {
            defaultPosition = new Vector2(ScreenManager.Viewport.Width * 0.75f, ScreenManager.Viewport.Height * 0.75f);
            DialogBox = new DialogBox(text, defaultPosition, "Sprites\\UI\\Menus\\DialogBox", parent, lifeTime);
            Skippable = skippable;
        }

        public AddDialogBoxScript(string text, Vector2 localPosition, bool skippable = true, bool canRun = true, bool shouldUpdateGame = false, BaseObject parent = null, float lifeTime = 7.0f)
            : base(shouldUpdateGame, canRun)
        {
            DialogBox = new DialogBox(text, localPosition, "Sprites\\UI\\Menus\\DialogBox", parent, lifeTime);
            Skippable = skippable;
        }

        public AddDialogBoxScript(string text, Vector2 localPosition, Vector2 size, bool skippable = true, bool canRun = true, bool shouldUpdateGame = false, BaseObject parent = null, float lifeTime = 7.0f)
            : base(shouldUpdateGame, canRun)
        {
            DialogBox = new DialogBox(text, localPosition, size, "Sprites\\UI\\Menus\\DialogBox", parent, lifeTime);
            Skippable = skippable;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadAndInit(ContentManager content)
        {
            DialogBox.LoadContent();
            DialogBox.Initialize();

            if (Skippable)
            {
                NextDialogButton = new Button(new Vector2(0, DialogBox.Size.Y * 0.5f + 30), new Vector2(100, 30), "Next", DialogBox);
                NextDialogButton.OnSelect += NextDialogButton_OnSelect;
                NextDialogButton.LoadContent();
                NextDialogButton.Initialize();
            }
        }

        void NextDialogButton_OnSelect(object sender, EventArgs e)
        {
            DialogBox.Alive = false;
        }

        public override void Run(GameTime gameTime)
        {
            DialogBox.Update(gameTime);

            if (Skippable)
            {
                NextDialogButton.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public override void DrawUI(SpriteBatch spriteBatch)
        {
            DialogBox.Draw(spriteBatch);

            if (Skippable)
            {
                NextDialogButton.Draw(spriteBatch);
            }
        }

        public override void HandleInput()
        {
            DialogBox.HandleInput();

            if (Skippable)
            {
                NextDialogButton.HandleInput();
            }
        }

        public override void CheckDone()
        {
            Done = DialogBox.Alive == false;
        }

        public override void IfDone()
        {
            base.IfDone();

            // Want to flush the input handler to avoid skipping over dialog boxes
            ScreenManager.GameMouse.Flush();
        }

        public override void PerformImmediately()
        {
            DialogBox.Alive = false;
            Done = true;
        }

        #endregion
    }
}
