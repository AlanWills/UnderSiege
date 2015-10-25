using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;
using UnderSiege.Player_Data;
using UnderSiege.UI;

namespace UnderSiege.Managers
{
    public class ShipAddOnManager : BaseObjectManager<ShipAddOn>
    {
        #region Properties and Fields

        public List<ShipAddOn> ShipTurrets
        {
            get { return Values.Where(x => x.ShipAddOnData.AddOnType == "ShipTurret").ToList(); }
        }

        public List<ShipAddOn> ShipShields
        {
            get { return Values.Where(x => x.ShipAddOnData.AddOnType == "ShipShield").ToList(); }
        }

        public List<ShipAddOn> ShipEngines
        {
            get { return Values.Where(x => x.ShipAddOnData.AddOnType == "ShipEngines").ToList(); }
        }

        private Ship ParentShip
        {
            get;
            set;
        }

        private static uint addOnTagID = 0;

        #endregion

        public ShipAddOnManager(Ship parentShip)
            : base()
        {
            ParentShip = parentShip;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public void AddObject(ShipAddOn addOn, bool load = true)
        {
            AddObject(addOn, "AddOn" + addOnTagID, load);
            addOnTagID++;
        }

        public override void RemoveObject(KeyValuePair<string, ShipAddOn> shipAddOnPair)
        {
            base.RemoveObject(shipAddOnPair);

            ShipAddOn addOn = shipAddOnPair.Value;

            if (addOn.ShipAddOnData.AddOnType == "ShipEngine")
            {
                ParentShip.EngineHardPoints.Add(addOn.HardPointOffset);
            }
            else
            {
                ParentShip.OtherHardPoints.Add(addOn.HardPointOffset);
            }

            PlayerShip playerShip = ParentShip as PlayerShip;
            EnemyShip enemyShip = ParentShip as EnemyShip;

            if (playerShip != null)
            {
                if (addOn.ShipAddOnData.AddOnType == "ShipEngine")
                {
                    playerShip.HardPointUI.Enable(addOn.HardPointOffset, HardPointType.Engine);
                }
                else
                {
                    playerShip.HardPointUI.Enable(addOn.HardPointOffset, HardPointType.Other);
                }
            }
            else
            {
                Session.Money += (int)(addOn.ShipAddOnData.Price * 0.1f);
            }
        }

        #endregion
    }
}
