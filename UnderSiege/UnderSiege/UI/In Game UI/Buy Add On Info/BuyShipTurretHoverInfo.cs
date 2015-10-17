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

            ImageAndLabel health = new ImageAndLabel("Sprites\\UI\\Icons\\Health", "Health: " + shipTurretData.Health.ToString(), new Vector2(0, SpriteFont.LineSpacing + padding), Color.White, name);
            AddUIObject(health, "Turret Health", true);
            size = new Vector2(Math.Max(size.X, health.Dimensions.X), size.Y + SpriteFont.LineSpacing + padding);

            string turretTypeIconAsset = "";
            switch (shipTurretData.TurretType)
            {
                case "Kinetic":
                    turretTypeIconAsset = "Sprites\\UI\\Icons\\KineticType";
                    break;
                case "Missile":
                    turretTypeIconAsset = "Sprites\\UI\\Icons\\MissileType";
                    break;
                case "Beam":
                    turretTypeIconAsset = "Sprites\\UI\\Icons\\BeamType";
                    break;
            }

            ImageAndLabel type = new ImageAndLabel(turretTypeIconAsset, "Type: " + shipTurretData.TurretType, new Vector2(0, SpriteFont.LineSpacing + padding), Color.White, health);
            AddUIObject(type, "Turret Type", true);
            size = new Vector2(Math.Max(size.X, type.Dimensions.X), size.Y + SpriteFont.LineSpacing + padding);

            Label range = new Label("Range: " + shipTurretData.Range.ToString(), new Vector2(0, SpriteFont.LineSpacing + padding), Color.White, type);
            size = new Vector2(Math.Max(size.X, range.TextDimensions.X), size.Y + SpriteFont.LineSpacing + padding);
            AddUIObject(range, "Turret Range");

            ImageAndLabel damage = new ImageAndLabel("Sprites\\UI\\Icons\\Damage", "Damage: " + shipTurretData.Damage.ToString(), new Vector2(0, SpriteFont.LineSpacing + padding), Color.White, range);
            AddUIObject(damage, "Turret Damage", true);
            size = new Vector2(Math.Max(size.X, damage.Dimensions.X), size.Y + SpriteFont.LineSpacing + padding);

            ImageAndLabel price = new ImageAndLabel("Sprites\\UI\\Icons\\MoneyIcon", shipTurretData.Price.ToString(), new Vector2(0, SpriteFont.LineSpacing + padding), damage);
            size = new Vector2(Math.Max(size.X, price.Dimensions.X), size.Y + SpriteFont.LineSpacing + padding);
            AddUIObject(price, "Turret Price");

            Size = size + new Vector2(padding, padding) * 2;
            LocalPosition += new Vector2(0, -Size.Y * 0.5f);

            // Position the first UI element correctly
            name.LocalPosition = new Vector2(0, -Size.Y * 0.5f + SpriteFont.LineSpacing * 0.5f + padding);
        }

        #endregion
    }
}
