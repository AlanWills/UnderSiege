using _2DGameEngine.Cutscenes.Scripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;
using UnderSiege.Screens;

namespace UnderSiege.Cutscenes.Scripts
{
    public class AddAlliedShipScript : Script
    {
        #region Properties and Fields

        protected UnderSiegeGameplayScreen GameplayScreen { get; set; }
        protected PlayerShip Ship { get; set; }
        protected string Tag { get; set; }

        #endregion

        public AddAlliedShipScript(PlayerShip alliedShip, string tag, UnderSiegeGameplayScreen gameplayScreen, bool shouldUpdateGame = true, bool canRun = true)
            : base(shouldUpdateGame, canRun)
        {
            Ship = alliedShip;
            Tag = tag;
            GameplayScreen = gameplayScreen;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadAndInit(ContentManager content)
        {
            Ship.LoadContent();
            Ship.Initialize();
        }

        public override void Run(GameTime gameTime)
        {
            GameplayScreen.AddAlliedShip(Ship, Tag);
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
            GameplayScreen.AddAlliedShip(Ship, Tag);
            Done = true;
        }

        #endregion
    }
}
