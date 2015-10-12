using _2DGameEngineData;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DTowerDefenceLibraryData.Waves_Data
{
    public class WaveData : BaseData
    {
        public List<string> EnemyNames
        {
            get;
            set;
        }

        public List<Vector2> SpawnPoints
        {
            get;
            set;
        }

        public float TimeUntilWave
        {
            get;
            set;
        }

        public float TimeBetweenEnemySpawns
        {
            get;
            set;
        }
    }
}
