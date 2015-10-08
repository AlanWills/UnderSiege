using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Object_Properties
{
    public class TickApplier<T> : ModifierApplier<T>
    {
        #region Properties and Fields

        private TimeSpan TickTimer
        {
            get;
            set;
        }

        private TimeSpan currentTickTimer = TimeSpan.FromSeconds(0);

        #endregion

        public TickApplier(string propertyToModify, T modifyValue, TimeSpan tickTimer, TimeSpan totalTime, BaseObject objectToModify)
            : base(propertyToModify, modifyValue, totalTime, objectToModify)
        {
            TickTimer = tickTimer;
        }

        public TickApplier(string propertyToModify, Func<T> modifierFunction, TimeSpan tickTimer, TimeSpan totalTime, BaseObject objectToModify)
            : base(propertyToModify, modifierFunction, totalTime, objectToModify)
        {
            TickTimer = tickTimer;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Update(GameTime gameTime)
        {
            currentTickTimer += gameTime.ElapsedGameTime;

            if (currentTickTimer >= TickTimer && !Done)
            {
                ApplyModifier();
                currentTickTimer = TimeSpan.FromSeconds(0);
            }

            base.Update(gameTime);
        }

        #endregion
    }
}
