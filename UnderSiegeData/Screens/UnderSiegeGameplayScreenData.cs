using _2DGameEngineData.Screen_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderSiegeData.Screens
{
    public class UnderSiegeGameplayScreenData : BaseScreenData
    {
        public string CommandShipName
        {
            get;
            set;
        }

        public List<string> WaveNames
        {
            get;
            set;
        }
    }
}
