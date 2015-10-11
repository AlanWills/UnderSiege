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
    public class AddCutsceneScript : Script
    {
        #region Properties and Fields

        private Cutscene Cutscene { get; set; }

        #endregion

        public AddCutsceneScript(ScriptManager scriptManager, Cutscene cutscene, bool canRun = false)
            : base(scriptManager, canRun)
        {
            Cutscene = cutscene;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadAndInit(ContentManager content)
        {
            Cutscene.LoadContent(content);
            Cutscene.Initialize();
        }

        public override void CheckCanRun()
        {
            base.CheckCanRun();

            CanRun = true;
        }

        public override void Update(GameTime gameTime)
        {
            Cutscene.Update(gameTime);
        }

        public override bool ShouldUpdateGame()
        {
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Cutscene.Draw();
        }

        public override void DrawUI(SpriteBatch spriteBatch)
        {
            Cutscene.DrawScreenUI();
        }

        public override void HandleInput()
        {
            Cutscene.HandleInput();
        }

        public override void CheckDone()
        {
            Done = Cutscene.IsDone;
        }

        public override void IfDone()
        {
            base.IfDone();

            ScriptManager.ParentScreen.Active = true;
            ScriptManager.ParentScreen.Visible = true;
        }

        #endregion
    }
}
