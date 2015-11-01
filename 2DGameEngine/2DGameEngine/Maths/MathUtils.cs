using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Maths
{
    public static class MathUtils
    {
        static int uniqueTagNumber = 0;

        public static bool FloatInRange(float number, float min, float max)
        {
            if (number >= min && number <= max)
            {
                return true;
            }

            return false;
        }

        public static int GetUniqueTagNumber()
        {
            uniqueTagNumber++;
            return uniqueTagNumber;
        }
    }
}
