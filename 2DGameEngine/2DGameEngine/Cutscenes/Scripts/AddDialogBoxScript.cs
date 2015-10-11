﻿using _2DGameEngine.Abstract_Object_Classes;
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

        private DialogBox DialogBox { get; set; }

        #endregion

        public AddDialogBoxScript(string text, Vector2 localPosition, bool canRun = true, BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(canRun)
        {
            DialogBox = new DialogBox(text, localPosition, Label.SpriteFont.MeasureString(text) +  new Vector2(20, 10), "Sprites\\UI\\Menus\\DialogBox", parent, lifeTime);
        }

        public AddDialogBoxScript(string text, Vector2 localPosition, Vector2 size, bool canRun = true, BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(canRun)
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

        public override void Update(GameTime gameTime)
        {
            DialogBox.Update(gameTime);
        }

        public override bool ShouldUpdateGame()
        {
            return false;
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
            Done = DialogBox.Alive == false || DialogBox.IsSelected;
        }

        #endregion
    }
}
