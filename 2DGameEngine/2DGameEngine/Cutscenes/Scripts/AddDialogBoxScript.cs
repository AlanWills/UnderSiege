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

        private DialogBox DialogBox { get; set; }

        #endregion

        public AddDialogBoxScript(ScriptManager scriptManager, string text, Vector2 localPosition, BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(scriptManager)
        {
            DialogBox = new DialogBox(text, localPosition, new Vector2(300, 50), "Sprites\\UI\\Menus\\default", parent, lifeTime);
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadAndInit(ContentManager content)
        {
            DialogBox.LoadContent();
            DialogBox.Initialize();
        }

        public override void CheckCanRun()
        {
            base.CheckCanRun();

            CanRun = true;
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
            
        }

        public override void CheckDone()
        {
            Done = DialogBox.Alive == false;
        }

        #endregion
    }
}
