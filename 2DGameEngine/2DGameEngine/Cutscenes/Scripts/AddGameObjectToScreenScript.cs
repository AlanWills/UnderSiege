using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Cutscenes.Scripts
{
    public class AddGameObjectToScreenScript : Script
    {
        #region Properties and Fields

        protected BaseScreen BaseScreen { get; set; }
        protected GameObject GameObject { get; set; }
        protected string Tag { get; set; }

        #endregion

        public AddGameObjectToScreenScript(GameObject gameObject, string tag, BaseScreen baseScreen, bool shouldUpdateGame = true, bool canRun = true)
            : base(shouldUpdateGame, canRun)
        {
            GameObject = gameObject;
            Tag = tag;
            BaseScreen = baseScreen;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadAndInit(ContentManager content)
        {
            GameObject.LoadContent();
            GameObject.Initialize();
        }

        public override void Run(GameTime gameTime)
        {
            BaseScreen.AddGameObject(GameObject, Tag);
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
            Done = true;
        }

        public override void PerformImmediately()
        {
            BaseScreen.AddGameObject(GameObject, Tag);
            Done = true;
        }

        #endregion
    }
}
