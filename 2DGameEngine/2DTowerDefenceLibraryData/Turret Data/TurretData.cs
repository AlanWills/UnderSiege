using _2DGameEngineData;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DTowerDefenceLibraryData.Turret_Data
{
    public class TurretData : BaseData
    {
        // Full path name (without content) for the bullet data file
        public string BulletAsset
        {
            get;
            set;
        }

        public float Range
        {
            get;
            set;
        }

        public float ArcWidth
        {
            get;
            set;
        }

        // Determines how much the bullets spread from current firing line - to give that nice effect of fast firing bullets being slightly off target
        public float Spread
        {
            get;
            set;
        }

        // Time between shots
        public float FireTimer
        {
            get;
            set;
        }

        // Damage per turret shot
        public int Damage
        {
            get;
            set;
        }

        // Dictates how many hard points the turret will take up in each dimension
        public Vector2 HardPointDimensions
        {
            get;
            set;
        }
    }
}
