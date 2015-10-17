using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.UI.In_Game_UI.Buy_Add_On_Info
{
    public class BuyShipEngineHoverInfo : BuyShipAddOnHoverInfo
    {
        #region Properties and Fields

        #endregion

        public BuyShipEngineHoverInfo(ShipEngineData shipEngineData, Vector2 localPosition, BaseObject parent, float lifeTime = float.MaxValue)
            : base(shipEngineData, localPosition, parent, lifeTime)
        {
            AddUI(shipEngineData);
        }

        #region Methods

        #endregion

        #region Virtual Methods

        private void AddUI(ShipEngineData shipEngineData)
        {
            Vector2 size = Vector2.Zero;

            // For now don't set the position of this label, because we do not know the size yet
            // However, with correct parenting we will only need to set this position at the very end when the size is calculated
            // And everything else will be correctly position
            Label name = new Label(shipEngineData.DisplayName, Vector2.Zero, Color.Yellow, this);
            size = name.TextDimensions;
            AddUIObject(name, "Engine Name");

            ImageAndLabel health = new ImageAndLabel("Sprites\\UI\\Icons\\Health", "Health: " + shipEngineData.Health.ToString(), new Vector2(0, SpriteFont.LineSpacing + padding), Color.White, name);
            AddUIObject(health, "Engine Health", true);
            size = new Vector2(Math.Max(size.X, health.Dimensions.X), size.Y + SpriteFont.LineSpacing + padding);

            Label thrust = new Label("Thrust: " + shipEngineData.Thrust.ToString(), new Vector2(0, SpriteFont.LineSpacing + padding), Color.White, health);
            size = new Vector2(Math.Max(size.X, thrust.TextDimensions.X), size.Y + SpriteFont.LineSpacing + padding);
            AddUIObject(thrust, "Engine Thrust");

            Size = size + new Vector2(padding, padding) * 2;
            LocalPosition += new Vector2(0, -Size.Y * 0.5f);

            // Position the first UI element correctly
            name.LocalPosition = new Vector2(0, -Size.Y * 0.5f + SpriteFont.LineSpacing * 0.5f + padding);
        }

        #endregion
    }
}
