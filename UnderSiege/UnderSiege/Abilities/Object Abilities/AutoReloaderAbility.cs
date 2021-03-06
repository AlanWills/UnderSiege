﻿using System;
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
            Done = abilityTimer > 10;
        }

        protected override void AbilityEvent(object sender, EventArgs e)
        {
            base.AbilityEvent(sender, e);

            abilityTimer += 0.1f;
            fireTimer += 0.1f;

            if (fireTimer >= ShipTurret.ShipKineticTurretData.FireTimer)
            {
                ShipTurret.Fire();
                fireTimer = 0;
            }
        }

        protected override void End()
        {
            base.End();

            abilityTimer = 0;
            fireTimer = 0;
        }

        #endregion
    }
}
