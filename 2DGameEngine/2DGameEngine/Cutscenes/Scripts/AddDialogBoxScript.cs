using _2DGameEngine.Abstract_Object_Classes;
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

        private Label Label { get; set; }

        #endregion

        public AddDialogBoxScript(ScriptManager scriptManager, string text, Vector2 localPosition, BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(scriptManager)
        {
            Label = new Label(text, localPosition, Color.White, parent, lifeTime);
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadAndInit(ContentManager content)
        {
            Label.LoadContent();
            Label.Initialize();
        }

        public override void CheckCanRun()
        {
            base.CheckCanRun();

            CanRun = true;
        }

        public override void Update(GameTime gameTime)
        {
            Label.Update(gameTime);
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
            Label.Draw(spriteBatch);
        }

        public override void HandleInput()
        {
            
        }

        public override void CheckDone()
        {
            Done = Label.Alive == false;
        }

        #endregion
    }
}
