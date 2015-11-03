using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;
using UnderSiege.Gameplay_Objects.Ship_Add_Ons;

namespace UnderSiege.Abilities.Object_Abilities
{
    public class AutoReloaderAbility : AddOnAbility
    {
        #region Properties and Fields

        private ShipKineticTurret ShipTurret { get; set; }

        private float abilityTimer;
        private float fireTimer;

        #endregion

        public AutoReloaderAbility(string dataAsset, ShipKineticTurret shipTurret)
            : base(dataAsset, shipTurret)
        {
            ShipTurret = shipTurret;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void CheckCanRun()
        {
            base.CheckCanRun();

            CanRun = CanRun && (ShipTurret.Target != null);
        }

        protected override void CheckIsDone()
        {
            abilityTimer += 0.1f;
            bool done = abilityTimer > 10;
            if (done)
            {
                abilityTimer = 0;
                ParentAddOn.AbilityEventQueue -= AbilityEvent;
            }
        }

        protected override void AbilityEvent(object sender, EventArgs e)
        {
            base.AbilityEvent(sender, e);

            fireTimer += 0.1f;
            if (fireTimer >= ShipTurret.ShipKineticTurretData.FireTimer)
            {
                ShipTurret.Fire();
                fireTimer = 0;
            }
        }

        #endregion
    }
}
