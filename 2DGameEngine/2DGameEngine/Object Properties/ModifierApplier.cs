using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Object_Properties
{
    public class ModifierApplier<T>
    {
        public bool Done
        {
            get { return currentTime > TotalTimeToApplyOver; }
        }

        private BaseObject BaseObject
        {
            get;
            set;
        }

        private string PropertyToModify
        {
            get;
            set;
        }

        private T modifyValue;
        private T ModifyValue
        {
            get
            {
                if (ModifierFunction != null)
                    return ModifierFunction();
                else
                    return modifyValue;
            }
            set { modifyValue = value; }
        }

        // A function which can be defined in the class which will use this modifier applier.
        // E.g. a weapon class which does more damage over time will have a ModifierFunction that looks like:
        /*
         * T MoreDOT()
         * {
         *      if (time > x)
         *          return 2 * damage;
         * }
         * 
         * or something.
         * Basically so that a function using the properties of the class and any base classes that use this class can be defined rather than having to specify arguments to this class.
         * Lets the class which uses this class calculate the damage to apply.
        */
        private Func<T> ModifierFunction
        {
            get;
            set;
        }

        private TimeSpan TotalTimeToApplyOver
        {
            get;
            set;
        }

        private TimeSpan currentTime = TimeSpan.FromSeconds(0);

        // Passing in totalTime = TimeSpan.FromSeconds(0) will apply the modifier just once
        public ModifierApplier(string propertyToModify, T modifyValue, TimeSpan totalTime, BaseObject objectToModify)
        {
            BaseObject = objectToModify;
            PropertyToModify = propertyToModify;
            TotalTimeToApplyOver = totalTime;
            this.modifyValue = modifyValue;
        }

        public ModifierApplier(string propertyToModify, Func<T> modifierFunction, TimeSpan totalTime, BaseObject objectToModify)
        {
            BaseObject = objectToModify;
            PropertyToModify = propertyToModify;
            TotalTimeToApplyOver = totalTime;
            ModifierFunction = modifierFunction;
        }

        protected void ApplyModifier()
        {
            // BaseObject.Properties.AddModifier<T>(PropertyToModify, ModifyValue);
        }

        public virtual void Update(GameTime gameTime)
        {
            currentTime += gameTime.ElapsedGameTime;
        }
    }
}
