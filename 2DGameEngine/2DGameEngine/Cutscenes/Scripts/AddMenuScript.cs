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
    public class AddMenuScript : Script
    {
        #region Properties and Fields

        private Menu Menu { get; set; }

        #endregion

        public AddMenuScript(Menu menu, bool shouldUpdateGame = false, bool canRun = true)
            : base(shouldUpdateGame, canRun)
        {
            Menu = menu;   
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadAndInit(ContentManager content)
        {
            Menu.LoadContent();
            Menu.Initialize();
        }

        public override void Run(GameTime gameTime)
        {
            Menu.Update(gameTime);
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
            Menu.Draw(spriteBatch);
        }

        public override void HandleInput()
        {
            Menu.HandleInput();
        }

        public override void CheckDone()
        {
            Done = Menu.Alive == false;
        }

        public override void PerformImmediately()
        {
            Done = true;
        }

        #endregion
    }
}