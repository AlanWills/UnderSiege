using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Cutscenes
{
    public abstract class Cutscene : BaseScreen
    {
        #region Properties and Fields

        public bool IsDone { get; protected set; }
        protected GameplayScreen GameplayScreen { get; private set; }

        #endregion

        public Cutscene(ScreenManager screenManager, string dataAsset, GameplayScreen gameplayScreen)
            : base(screenManager, dataAsset)
        {
            IsDone = false;
            GameplayScreen = gameplayScreen;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void AddScripts()
        {

        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            ScriptManager.LoadAndAddScripts(content);
        }

        public override void AddMusic(QueueType queueType = QueueType.PlayImmediately)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            CheckIsDone();
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if (InputHandler.KeyPressed(Keys.M))
            {
                Skip();
            }
        }

        protected abstract void CheckIsDone();

        public virtual void Skip()
        {
            foreach (Script script in ScriptManager.RunningScripts)
            {
                script.PerformImmediately();
            }

            IsDone = true;
        }

        #endregion
    }
}
