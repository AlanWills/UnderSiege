using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;
using UnderSiege.Player_Data;

namespace UnderSiege.Abilities.Object_Abilities
{
    public class SellAbility : AddOnAbility
    {
        #region Properties and Fields

        #endregion

        public SellAbility(string dataAsset, ShipAddOn parentAddOn)
            : base(dataAsset, parentAddOn)
        {

        }

        #region Methods

        #endregion

        #region Virtual Methods

        protected override bool CanRun()
        {
            return ParentAddOn.CurrentHealth > 0;
        }

        protected override void CheckIsDone()
        {
            bool done = true;
            if (done)
            {
                ParentAddOn.AbilityEventQueue -= AbilityEvent;
            }
        }

        protected override void AbilityEvent(object sender, EventArgs e)
        {
            base.AbilityEvent(sender, e);

            ParentAddOn.Alive = false;
            Session.Money += (int)(ParentAddOn.ShipAddOnData.Price * (ParentAddOn.CurrentHealth / ParentAddOn.ShipAddOnData.Health));
        }

        #endregion
    }
}
