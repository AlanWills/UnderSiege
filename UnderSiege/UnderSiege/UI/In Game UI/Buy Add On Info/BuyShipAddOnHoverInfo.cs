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

        protected const int padding = 5;

        #endregion

        public BuyShipAddOnHoverInfo(ShipAddOnData shipAddOnData, Vector2 localPosition, BaseObject parent, float lifeTime = float.MaxValue)
            : base(localPosition, 0, 0, 0, 0, "Sprites\\UI\\Panels\\default", parent, lifeTime)
        {
            
        }

        #region Methods

        #endregion

        #region Virtual Methods

        #endregion
    }
}
