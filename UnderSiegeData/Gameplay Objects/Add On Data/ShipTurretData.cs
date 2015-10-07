using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderSiegeData.Gameplay_Objects
{
    public class ShipTurretData : ShipAddOnData
    {
        public string TurretType
        {
            get;
            set;
        }

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

        // Damage per turret shot
        public float Damage
        {
            get;
            set;
        }
    }
}
