using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Object_Properties
{
    public class DurationApplier<T> : ModifierApplier<T>
    {
        #region Properties and Fields

        #endregion

        public DurationApplier(string propertyToModify, T modifyValue, TimeSpan totalTime, BaseObject objectToModify)
            : base(propertyToModify, modifyValue, totalTime, objectToModify)
        {
            
        }

        public DurationApplier(string propertyToModify, Func<T> modifierFunction, TimeSpan totalTime, BaseObject objectToModify)
            : base(propertyToModify, modifierFunction, totalTime, objectToModify)
        {
            
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Update(GameTime gameTime)
        {
            if (!Done)
            {
                ApplyModifier();
            }

            base.Update(gameTime);
        }

        #endregion
    }
}
