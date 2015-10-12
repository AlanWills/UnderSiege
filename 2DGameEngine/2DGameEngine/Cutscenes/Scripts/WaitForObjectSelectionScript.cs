﻿using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Cutscenes.Scripts
{
    public class WaitForObjectSelectionScript : Script
    {
        #region Properties and Fields

        private BaseObject ObjectToSelect { get; set; }

        #endregion

        public WaitForObjectSelectionScript(BaseObject objectToSelect, bool shouldUpdateGame = true, bool canRun = true)
            : base(shouldUpdateGame, canRun)
        {
            ObjectToSelect = objectToSelect;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadAndInit(ContentManager content)
        {
            
        }

        public override void Run(GameTime gameTime)
        {
            ObjectToSelect.Active = true;
            ObjectToSelect.Visible = true;
        }

        public override void CheckShouldUpdateGame()
        {
            ShouldUpdateGame = true;
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
            Done = ObjectToSelect.IsSelected;
        }

        public override void PerformImmediately()
        {
            ObjectToSelect.IsSelected = true;
        }

        #endregion
    }
}
