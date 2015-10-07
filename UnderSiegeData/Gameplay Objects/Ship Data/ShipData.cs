﻿using _2DGameEngineData;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderSiegeData.Gameplay_Objects
{
    public class ShipData : BaseData
    {
        public float HullHealth
        {
            get;
            set;
        }

        public List<Vector2> OtherHardPoints
        {
            get;
            set;
        }

        public List<Vector2> EngineHardPoints
        {
            get;
            set;
        }

        public int Price
        {
            get;
            set;
        }
    }
}
