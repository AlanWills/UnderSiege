using _2DGameEngine.Abstract_Object_Classes;
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

        }

        #region Methods

        #endregion

        #region Virtual Methods

        #endregion
    }
}
