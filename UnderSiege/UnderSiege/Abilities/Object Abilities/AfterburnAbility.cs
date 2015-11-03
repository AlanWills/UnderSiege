using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects.Ship_Add_Ons;

namespace UnderSiege.Abilities.Object_Abilities
{
    public class AfterburnAbility : AddOnAbility
    {
        #region Properties and Fields

        private ShipEngine ShipEngine { get; set; }

        private float abilityTimer;

        #endregion

        public AfterburnAbility(string dataAsset, ShipEngine shipEngine)
            : base(dataAsset, shipEngine)
        {
            ShipEngine = shipEngine;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        protected override void CheckIsDone()
        {
            Done = abilityTimer > 30;
        }

        protected override void Begin()
        {
            ShipEngine.ThrustModifier = 2.5f;
        }

        protected override void AbilityEvent(object sender, EventArgs e)
        {
            base.AbilityEvent(sender, e);

            abilityTimer += 0.1f;
        }

        protected override void End()
        {
            base.End();

            ShipEngine.ThrustModifier = 1;
            abilityTimer = 0;
        }

        #endregion
    }
}
