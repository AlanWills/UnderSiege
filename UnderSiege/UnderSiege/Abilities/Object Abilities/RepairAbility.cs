using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;
using UnderSiege.Player_Data;

namespace UnderSiege.Abilities.Object_Abilities
{
    public class RepairAbility : AddOnAbility
    {
        #region Properties and Fields

        #endregion

        public RepairAbility(string dataAsset, ShipAddOn parentAddOn)
            : base(dataAsset, parentAddOn)
        {

        }

        #region Methods

        #endregion

        #region Virtual Methods

        protected override bool CanRun()
        {
            float diff = ParentAddOn.ShipAddOnData.Health - ParentAddOn.CurrentHealth;
            return (diff != 0 && Session.Money >= diff);
        }

        protected override void CheckIsDone()
        {
            bool done = ParentAddOn.CurrentHealth == ParentAddOn.ShipAddOnData.Health;
            if (done)
            {
                ParentAddOn.AbilityEventQueue -= AbilityEvent;
            }
        }

        protected override void AbilityEvent(object sender, EventArgs e)
        {
            base.AbilityEvent(sender, e);

            ParentAddOn.CurrentHealth += 1f;
            ParentAddOn.CurrentHealth = MathHelper.Clamp(ParentAddOn.CurrentHealth, 0, ParentAddOn.ShipAddOnData.Health);

            // This is not going to take into account if the player goes into negative money - need to do a check for that
            Session.Money -= 1;
        }

        #endregion
    }
}
