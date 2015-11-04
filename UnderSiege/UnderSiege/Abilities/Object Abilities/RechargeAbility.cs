using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;

namespace UnderSiege.Abilities.Object_Abilities
{
    public class RechargeAbility : AddOnAbility
    {
        #region Properties and Fields

        private ShipShield ShipShield { get; set; }

        private float totalRecharged = 0;

        #endregion

        public RechargeAbility(string dataAsset, ShipShield parentShield)
            : base(dataAsset, parentShield)
        {
            ShipShield = parentShield;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void CheckCanRun()
        {
            base.CheckCanRun();

            float diff = ShipShield.ShipShieldData.ShieldStrength - ShipShield.CurrentShieldStrength;
            CanRun = CanRun && diff != 0;
        }

        protected override void CheckIsDone()
        {
            Done = ShipShield.CurrentShieldStrength == ShipShield.ShipShieldData.ShieldStrength && totalRecharged > 75;
        }

        protected override void End()
        {
            base.End();

            totalRecharged = 0;
        }

        protected override void AbilityEvent(object sender, EventArgs e)
        {
            base.AbilityEvent(sender, e);

            ShipShield.CurrentShieldStrength += 1f;
            totalRecharged += 1f;
            ShipShield.CurrentShieldStrength = MathHelper.Clamp(ShipShield.CurrentShieldStrength, 0, ShipShield.ShipShieldData.ShieldStrength);
        }

        #endregion
    }
}
