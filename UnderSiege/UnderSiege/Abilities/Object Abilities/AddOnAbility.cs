using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;
using UnderSiegeData.Abilities.Object_Abilities;

namespace UnderSiege.Abilities.Object_Abilities
{
    /*public class AbilityEventArgs : EventArgs
    {
        public GameTime GameTime { get; set; }

        public AbilityEventArgs(GameTime gameTime)
        {
            GameTime = gameTime;
        }
    }*/

    public abstract class AddOnAbility
    {
        #region Properties and Fields

        private string DataAsset { get; set; }
        public AddOnAbilityData AddOnAbilityData { get; set; }

        protected ShipAddOn ParentAddOn { get; private set; }

        #endregion

        public AddOnAbility(string dataAsset, ShipAddOn shipAddOn)
        {
            DataAsset = dataAsset;
            ParentAddOn = shipAddOn;
        }

        #region Methods

        public void LoadContent()
        {
            AddOnAbilityData = ScreenManager.Content.Load<AddOnAbilityData>(DataAsset);
        }

        #endregion

        #region Virtual Methods

        // A condition for which we are able to run this ability - either money, cooldown or health not 100% for repair
        protected abstract bool CanRun();
        protected abstract void CheckIsDone();

        public virtual void Run()
        {
            if (CanRun())
            {
                ParentAddOn.AbilityEventQueue += AbilityEvent;
            }
        }

        // The functionality of the ability will be contained in this - it MUST be specified
        protected virtual void AbilityEvent(object sender, EventArgs e)
        {
            CheckIsDone();
        }

        #endregion
    }
}
