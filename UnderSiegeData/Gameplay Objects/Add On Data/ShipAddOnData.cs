﻿using _2DGameEngineData;
using _2DGameEngineData.GameObject_Data;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderSiegeData.Gameplay_Objects
{
    public class ShipAddOnData : DamageableGameObjectData
    {
        public string AddOnType
        {
            get;
            set;
        }

        public int Price
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

        // Determines whether we have an object that can be oriented
        public bool Orientable
        {
            get;
            set;
        }

        public List<string> Abilities
        {
            get;
            set;
        }
    }
}
