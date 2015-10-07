using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.Gameplay_Objects.Ship_Add_Ons
{
    public class ShipEngine : ShipAddOn
    {
        #region Properties and Fields

        public ShipEngineData ShipEngineData { get; set; }

        #endregion

        public ShipEngine(Vector2 hardPointOffset, string dataAsset, Ship parent, bool addRigidBody = true)
            : base(hardPointOffset, dataAsset, parent, addRigidBody)
        {

        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            ShipEngineData = AssetManager.GetData<ShipEngineData>(DataAsset);
            Ship parent = Parent as Ship;
            parent.TotalThrust += ShipEngineData.Thrust;
            // Parent = parent; ?? Do we need this??
        }

        public override void Die()
        {
            base.Die();

            Ship parent = Parent as Ship;
            parent.TotalThrust -= ShipEngineData.Thrust;
        }

        #endregion
    }
}
