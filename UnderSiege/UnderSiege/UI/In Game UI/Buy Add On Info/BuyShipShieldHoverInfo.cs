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
    public class BuyShipShieldHoverInfo : BuyShipAddOnHoverInfo
    {
        #region Properties and Fields

        #endregion

        public BuyShipShieldHoverInfo(ShipShieldData shipShieldData, Vector2 localPosition, BaseObject parent, float lifeTime = float.MaxValue)
            : base(shipShieldData, localPosition, parent, lifeTime)
        {
            AddUI(shipShieldData);
        }

        #region Methods

        #endregion

        #region Virtual Methods

        private void AddUI(ShipShieldData shipShieldData)
        {
            Vector2 size = Vector2.Zero;

            // For now don't set the position of this label, because we do not know the size yet
            // However, with correct parenting we will only need to set this position at the very end when the size is calculated
            // And everything else will be correctly position
            Label name = new Label(shipShieldData.DisplayName, Vector2.Zero, Color.Cyan, this);
            size = name.TextDimensions;
            AddUIObject(name, "Shield Name");

            Label health = new Label("Health: " + shipShieldData.Health.ToString(), new Vector2(0, SpriteFont.LineSpacing + padding), Color.White, name);
            size = new Vector2(Math.Max(size.X, health.TextDimensions.X), size.Y + SpriteFont.LineSpacing + padding);
            AddUIObject(health, "Shield Health");

            Label range = new Label("Range: " + shipShieldData.ShieldRange.ToString(), new Vector2(0, SpriteFont.LineSpacing + padding), Color.White, health);
            size = new Vector2(Math.Max(size.X, range.TextDimensions.X), size.Y + SpriteFont.LineSpacing + padding);
            AddUIObject(range, "Shield Range");

            Label strength = new Label("Strength: " + shipShieldData.ShieldStrength.ToString(), new Vector2(0, SpriteFont.LineSpacing + padding), Color.White, range);
            size = new Vector2(Math.Max(size.X, strength.TextDimensions.X), size.Y + SpriteFont.LineSpacing + padding);
            AddUIObject(strength, "Shield Strength");

            Label depletedRechargeDelay = new Label("Depleted Recharge Delay: " + shipShieldData.ShieldDepletedRechargeDelay.ToString(), new Vector2(0, SpriteFont.LineSpacing + padding), Color.White, strength);
            size = new Vector2(Math.Max(size.X, depletedRechargeDelay.TextDimensions.X), size.Y + SpriteFont.LineSpacing + padding);
            AddUIObject(depletedRechargeDelay, "Shield Depleted Recharge Delay");

            Label damagedRechargeDelay = new Label("Damaged Recharge Delay: " + shipShieldData.ShieldDamagedRechargeDelay.ToString(), new Vector2(0, SpriteFont.LineSpacing + padding), Color.White, depletedRechargeDelay);
            size = new Vector2(Math.Max(size.X, damagedRechargeDelay.TextDimensions.X), size.Y + SpriteFont.LineSpacing + padding);
            AddUIObject(damagedRechargeDelay, "Shield Damaged Recharge Delay");

            Label rechargePerSecond = new Label("Recharge Per Second: " + shipShieldData.ShieldRechargePerSecond.ToString(), new Vector2(0, SpriteFont.LineSpacing + padding), Color.White, damagedRechargeDelay);
            size = new Vector2(Math.Max(size.X, rechargePerSecond.TextDimensions.X), size.Y + SpriteFont.LineSpacing + padding);
            AddUIObject(rechargePerSecond, "Recharge Per Second");

            Size = size + new Vector2(padding, padding) * 2;
            LocalPosition += new Vector2(0, -Size.Y * 0.5f);

            // Position the first UI element correctly
            name.LocalPosition = new Vector2(0, -Size.Y * 0.5f + SpriteFont.LineSpacing * 0.5f + padding);
        }

        #endregion
    }
}
