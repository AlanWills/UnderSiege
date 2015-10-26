using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using _2DGameEngine.Maths;
using _2DGameEngine.Physics_Components.Movement_Behaviours;
using _2DGameEngine.Screens;
using _2DGameEngineData;
using _2DTowerDefenceLibraryData.Waves_Data;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DTowerDefenceEngine.Waves
{
    public class Wave<T> where T : GameObject
    {
        #region Properties and Fields

        private string DataAsset { get; set; }
        public WaveData WaveData { get; set; }
        public Queue<T> Enemies { get; set; }

        #endregion

        public Wave(string dataAsset)
        {
            DataAsset = dataAsset;
            Enemies = new Queue<T>();
        }

        #region Methods

        public void LoadContent()
        {
            WaveData = AssetManager.GetData<WaveData>(DataAsset);
            SetUpEnemies();
        }

        public void Initialize()
        {
            foreach (T enemy in Enemies)
            {
                enemy.Initialize();
            }
        }

        public Wave<T> Clone()
        {
            Wave<T> wave = new Wave<T>(DataAsset);
            wave.DataAsset = DataAsset;
            wave.WaveData = WaveData;
            wave.Enemies = Enemies;

            return wave;
        }

        #endregion

        #region Virtual Methods

        public virtual void SetUpEnemies()
        {
            int enemyCounter = 0;
            int spawnPointCounter = 0;

            foreach (string enemyDataAsset in WaveData.EnemyNames)
            {
                if (enemyCounter >= Math.Ceiling((float)WaveData.EnemyNames.Count / (float)WaveData.SpawnPoints.Count))
                {
                    enemyCounter = 0;
                    spawnPointCounter++;
                }

                T enemy = (T)Activator.CreateInstance(typeof(T), WaveData.SpawnPoints[spawnPointCounter], enemyDataAsset, null);

                enemy.LoadContent();
                Enemies.Enqueue(enemy);

                enemyCounter++;
            }
        }

        #endregion
    }
}