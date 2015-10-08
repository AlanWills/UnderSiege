using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Maths
{
    public static class RandomNumberGenerator
    {
        static Random random = new Random();

        public static float RandomFloat(float min, float max)
        {
            float r = (float)random.NextDouble();
            float result =  r * (max - min) + min;

            return result;
        }

        // Exclusive upper bound
        public static int RandomInt(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
