using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.UI.In_Game_UI
{
    public abstract class BuyShipAddOnHoverInfo : Menu
    {
        #region Properties and Fields

        private const int padding = 5;

        #endregion

        public BuyShipAddOnHoverInfo(ShipAddOnData shipAddOnData, Vector2 localPosition, BaseObject parent, float lifeTime = float.MaxValue)
            : base(localPosition, 0, 0, 0, 0, "Sprites\\UI\\Panels\\default", parent, lifeTime)
        {
            AddUI(shipAddOnData);
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public virtual void AddUI(ShipAddOnData shipAddOnData)
        {
            Vector2 size = Vector2.Zero;

            // For now don't set the position of this label, because we do not know the size yet
            // However, with correct parenting we will only need to set this position at the very end when the size is calculated
            // And everything else will be correctly position
            Label name = new Label(shipAddOnData.DisplayName, Vector2.Zero, Color.White, this);
            size = SpriteFont.MeasureString(name.Text);
            AddUIObject(name, "Add On Name");

            Size = size + new Vector2(padding, padding) * 2;
            LocalPosition += new Vector2(0, -Size.Y * 0.5f);
            
            // Position the first UI element correctly
            name.LocalPosition = new Vector2(0, -Size.Y * 0.5f + SpriteFont.LineSpacing * 0.5f + padding);
        }

        #endregion
    }
}
