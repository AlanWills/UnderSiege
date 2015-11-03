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
    public abstract class AddOnAbility
    {
        #region Properties and Fields

        private string DataAsset { get; set; }
        public AddOnAbilityData AddOnAbilityData { get; set; }

        protected ShipAddOn ParentAddOn { get; private set; }

        public float Cooldown { get; protected set; }
        public bool OffCooldown { get { return Cooldown == 0; } }
        public bool CanRun { get; protected set; }

        protected bool Begun { get; set; }

        private bool done = false;
        protected bool Done 
        { 
            get { return done; } 
            set
            {
                done = value;
                if (done)
                {
                    End();
                }
            }
        }

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
        public virtual void CheckCanRun()
        {
            CanRun = OffCooldown;
        }

        protected abstract void CheckIsDone();

        public virtual void Run()
        {
            if (CanRun)
            {
                Cooldown = AddOnAbilityData.Cooldown;
                ParentAddOn.AbilityEventQueue += AbilityEvent;
            }
        }

        // The functionality of the ability will be contained in this - it MUST be specified
        protected virtual void AbilityEvent(object sender, EventArgs e)
        {
            if (!Begun)
            {
                Begin();
            }

            CheckIsDone();
        }

        protected virtual void Begin()
        {
            Begun = true;
        }

        protected virtual void End()
        {
            ParentAddOn.AbilityEventQueue -= AbilityEvent;
        }

        public virtual void Update(GameTime gameTime)
        {
            Cooldown = Math.Max(Cooldown - (float)gameTime.ElapsedGameTime.Milliseconds / 1000f, 0);
            CheckCanRun();
        }

        #endregion
    }
}
