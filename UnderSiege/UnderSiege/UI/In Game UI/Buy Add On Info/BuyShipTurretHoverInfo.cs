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
    public class BuyShipTurretHoverInfo : BuyShipAddOnHoverInfo
    {
        #region Properties and Fields

        #endregion

        public BuyShipTurretHoverInfo(ShipTurretData shipTurretData, Vector2 localPosition, BaseObject parent, float lifeTime = float.MaxValue)
            : base(shipTurretData, localPosition, parent, lifeTime)
        {
            AddUI(shipTurretData);
        }

        #region Methods

        #endregion

        #region Virtual Methods

        private void AddUI(ShipTurretData shipTurretData)
        {
            Vector2 size = Vector2.Zero;

            // For now don't set the position of this label, because we do not know the size yet
            // However, with correct parenting we will only need to set this position at the very end when the size is calculated
            // And everything else will be correctly position
            Label name = new Label(shipTurretData.DisplayName, Vector2.Zero, Color.Red, this);
            size = name.TextDimensions;
            AddUIObject(name, "Turret Name");

            Label health = new Label("Health: " + shipTurretData.Health.ToString(), new Vector2(0, SpriteFont.LineSpacing + padding), Color.White, name);
            size = new Vector2(Math.Max(size.X, health.TextDimensions.X), size.Y + SpriteFont.LineSpacing + padding);
            AddUIObject(health, "Turret Health");

            Label type = new Label("Type: " + shipTurretData.TurretType, new Vector2(0, SpriteFont.LineSpacing + padding), Color.White, health);
            size = new Vector2(Math.Max(size.X, type.TextDimensions.X), size.Y + SpriteFont.LineSpacing + padding);
            AddUIObject(type, "Turret Type");

            Label range = new Label("Range: " + shipTurretData.Range.ToString(), new Vector2(0, SpriteFont.LineSpacing + padding), Color.White, type);
            size = new Vector2(Math.Max(size.X, range.TextDimensions.X), size.Y + SpriteFont.LineSpacing + padding);
            AddUIObject(range, "Turret Range");

            Label damage = new Label("Damage: " + shipTurretData.Damage.ToString(), new Vector2(0, SpriteFont.LineSpacing + padding), Color.White, range);
            size = new Vector2(Math.Max(size.X, damage.TextDimensions.X), size.Y + SpriteFont.LineSpacing + padding);
            AddUIObject(damage, "Turret Damage");

            ImageAndLabel price = new ImageAndLabel("Sprites\\UI\\Icons\\MoneyIcon", shipTurretData.Price.ToString(), new Vector2(0, SpriteFont.LineSpacing + padding), damage);
            size = new Vector2(Math.Max(size.X, price.Size.X), size.Y + SpriteFont.LineSpacing + padding);
            AddUIObject(price, "Turret Price");

            Size = size + new Vector2(padding, padding) * 2;
            LocalPosition += new Vector2(0, -Size.Y * 0.5f);

            // Position the first UI element correctly
            name.LocalPosition = new Vector2(0, -Size.Y * 0.5f + SpriteFont.LineSpacing * 0.5f + padding);
        }

        #endregion
    }
}
