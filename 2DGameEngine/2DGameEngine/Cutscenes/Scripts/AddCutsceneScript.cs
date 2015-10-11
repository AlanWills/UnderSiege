using _2DGameEngine.Managers;
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
    public class AddCutsceneScript : Script
    {
        #region Properties and Fields

        private Cutscene Cutscene { get; set; }
        private BaseScreen CurrentScreen { get; set; }

        #endregion

        public AddCutsceneScript(Cutscene cutscene, BaseScreen currentScreen, bool canRun = true)
            : base(canRun)
        {
            Cutscene = cutscene;
            CurrentScreen = currentScreen;
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

        public override void Run(GameTime gameTime)
        {
            Cutscene.Update(gameTime);
        }

        public override void CheckShouldUpdateGame()
        {
            ShouldUpdateGame = Cutscene.ScriptManager.UpdateGame;
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

            CurrentScreen.Active = true;
            CurrentScreen.Visible = true;
        }

        public override void PerformImmediately()
        {
            Cutscene.LoadContent(CurrentScreen.Content);
            Cutscene.Initialize();
            Cutscene.Skip();
        }

        #endregion
    }
}
