using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderSiegeData.Gameplay_Objects
{
    public class ShipKineticTurretData : ShipTurretData
    {
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
    }
}
