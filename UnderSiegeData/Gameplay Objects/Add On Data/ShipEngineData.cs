using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderSiegeData.Gameplay_Objects
{
    public class ShipEngineData : ShipAddOnData
    {
        public string EngineSoundAsset
        {
            get;
            set;
        }

        public float Thrust
        {
            get;
            set;
        }
    }
}
