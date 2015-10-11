using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Cutscenes;

namespace UnderSiege.Screens
{
    public class UnderSiegeGameplayTutorialScreen : UnderSiegeGameplayScreen
    {
        #region Properties and Fields

        #endregion

        public UnderSiegeGameplayTutorialScreen(ScreenManager screenManager, string dataAsset)
            : base(screenManager, dataAsset)
        {
            WaveManager.Paused = true;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Initialize()
        {
 	         base.Initialize();

             HUD.DisableAndHideAll();
        }

        public override void Begin(GameTime gameTime)
        {
            base.Begin(gameTime);

            ScriptManager.AddScript(new AddCutsceneScript(new GameplayTutorialCutscene(ScreenManager, this, ""), this));
        }

        #endregion
    }
}
