using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HardpointCalculator
{
    public class HardpointCalculatorScreen : BaseScreen
    {
        #region Properties and Fields

        private string shipAssetName = "Sprites\\Ships\\bushi_odachi";
        private Label positionLabel;

        #endregion

        public HardpointCalculatorScreen(ScreenManager screenManager)
            : base(screenManager, "")
        {

        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            AddImage(ScreenCentre, "Ship Image", shipAssetName, true);
            positionLabel = new Label((ScreenManager.GameMouse.WorldPosition - ScreenCentre).ToString(), new Vector2(200, 100), Color.White);
            AddScreenUIObject(positionLabel, "Position Label", true);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            positionLabel.Text = (ScreenManager.GameMouse.WorldPosition - ScreenCentre).ToString();
        }

        #endregion
    }
}
