using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;
using UnderSiege.Gameplay_Objects.Ship_Add_Ons;

namespace UnderSiege.Abilities.Object_Abilities
{
    public class MissileBarrageAbility : AddOnAbility
    {
        #region Properties and Fields

        private ShipMissileTurret ShipMissileTurret { get; set; }

        private bool IsFiringIncreased { get { return abilityTimer <= 20; } }

        private float abilityTimer;
        private float fireTimer;
        private float noFireTimer;

        #endregion

        public MissileBarrageAbility(string dataAsset, ShipMissileTurret shipMissileTurret)
            : base(dataAsset, shipMissileTurret)
        {
            ShipMissileTurret = shipMissileTurret;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void CheckCanRun()
        {
            base.CheckCanRun();

            CanRun = CanRun && (ShipMissileTurret.Target != null);
        }

        protected override void CheckIsDone()
        {
            Done = noFireTimer > 10;
        }

        protected override void AbilityEvent(object sender, EventArgs e)
        {
            base.AbilityEvent(sender, e);

            abilityTimer += 0.1f;

            if (IsFiringIncreased)
            {
                fireTimer += 0.1f;
                if (fireTimer >= ShipMissileTurret.ShipMissileTurretData.FireTimer)
                {
                    ShipMissileTurret.Fire();
                    fireTimer = 0;
                }
            }
            else
            {
                ShipMissileTurret.currentFireTimer = 0;
                noFireTimer += 0.1f;
            }
        }

        protected override void End()
        {
            base.End();

            abilityTimer = 0;
            fireTimer = 0;
            noFireTimer = 0;
        }

        #endregion
    }
}
