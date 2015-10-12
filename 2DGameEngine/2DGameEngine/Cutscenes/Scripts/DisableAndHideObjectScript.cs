using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Cutscenes.Scripts
{
    public class DisableAndHideObjectScript : Script
    {
        #region Properties and Fields

        private BaseObject ObjectToSelect { get; set; }

        #endregion

        public DisableAndHideObjectScript(BaseObject objectToSelect, bool shouldUpdateGame = true, bool canRun = true)
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
            ObjectToSelect.Active = false;
            ObjectToSelect.Visible = false;
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
            Done = !ObjectToSelect.Active && !ObjectToSelect.Visible;
        }

        public override void PerformImmediately()
        {
            ObjectToSelect.Active = false;
            ObjectToSelect.Visible = false;
        }

        #endregion
    }
}
