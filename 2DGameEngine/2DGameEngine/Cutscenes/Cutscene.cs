using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

        #endregion

        public Cutscene(ScreenManager screenManager, string dataAsset)
            : base(screenManager, dataAsset)
        {
            IsDone = false;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            ScriptManager.LoadAndAddScripts(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            CheckIsDone();
        }

        protected abstract void CheckIsDone();

        #endregion
    }
}
