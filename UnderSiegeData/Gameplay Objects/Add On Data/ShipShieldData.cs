using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderSiegeData.Gameplay_Objects
{
    public class ShipShieldData : ShipAddOnData
    {
        public float ShieldRange
        {
            get;
            set;
        }

        public float ShieldStrength
        {
            get;
            set;
        }

        public float ShieldDepletedRechargeDelay
        {
            get;
            set;
        }

        public float ShieldDamagedRechargeDelay
        {
            get;
            set;
        }

        public float ShieldRechargePerSecond
        {
            get;
            set;
        }

        public Vector4 Colour
        {
            get;
            set;
        }
    }
}
