using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Object_Attributes
{
    public sealed class Property<T>
    {
        public T Value
        {
            get;
            private set;
        }

        public List<Type> AllowedTypes
        {
            get;
            private set;
        }

        public Property()
        {
            Value = default(T);
        }

        public Property(T value)
        {
            Value = value;
        }

        public void SetValue(T newValue)
        {
            Value = newValue;
        }

        public void Add(object modifier)
        {
            /*foreach (Type t in AllowedTypes)
            {
                if (Value.GetType() == t)
                {
                    Value += (modifier as t);
                }
            }*/
        }
    }
}
