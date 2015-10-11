using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.UI_Objects;
using _2DTowerDefenceEngine.Waves;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Screens;

namespace UnderSiege.Waves
{
    public class WaveManager<T> where T : GameObject
    {
        #region Properties and Fields

        public bool Paused { get; set; }

        public Queue<Wave<T>> Waves
        {
            get;
            private set;
        }

        public UnderSiegeGameplayScreen GameplayScreen { get; private set; }
        public int CurrentWaveNumber { get; set; }

        private Queue<T> QueuedEnemies { get; set; }

        private float currentTimeBetweenWaves = 0;
        private float currentTimeBetweenEnemySpawns = 0;
        private int currentEnemyInWave = 0;

        private const float timeBetweenEnemySpawns = 2;

        #endregion

        public WaveManager(UnderSiegeGameplayScreen gameplayScreen)
        {
            GameplayScreen = gameplayScreen;
            Waves = new Queue<Wave<T>>();
            QueuedEnemies = new Queue<T>();
        }

        #region Methods

        public void LoadContent()
        {
            foreach (string waveDataAsset in GameplayScreen.GameplayScreenData.WaveNames)
            {
                Wave<T> wave = new Wave<T>(waveDataAsset);
                wave.LoadContent();
                Waves.Enqueue(wave);
            }
        }

        public void Initialize()
        {
            foreach (Wave<T> wave in Waves)
            {
                wave.Initialize();
            }
        }

        public void NewWave()
        {
            if (Waves.Count > 0)
            {
                CurrentWaveNumber++;

                Wave<T> wave = Waves.Dequeue();
                QueuedEnemies = wave.Enemies;

                FlashingLabel warningLabel = new FlashingLabel("Enemies Detected!", new Vector2(ScreenManager.ScreenCentre.X, ScreenManager.ScreenCentre.Y * 0.25f), Color.Red, null, 4.8f);
                UnderSiegeGameplayScreen.HUD.AddUIObject(warningLabel, "New Wave Label");

                int spawnCounter = 0;
                foreach (Vector2 spawnPoint in wave.WaveData.SpawnPoints)
                {
                    UnderSiegeGameplayScreen.HUD.AddUIObject(new Marker(new Vector2(-wave.WaveData.SpawnPoints.Count * 0.5f * 32 + spawnCounter * 32, 32), Camera.GameToScreenCoords(spawnPoint), "Sprites\\UI\\Markers\\DirectionMarker", warningLabel), spawnPoint.ToString() + "Marker");
                    spawnCounter++;
                }

                currentTimeBetweenWaves = 0;
                currentTimeBetweenEnemySpawns = timeBetweenEnemySpawns;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (!Paused)
            {
                currentTimeBetweenWaves += (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;

                if (Waves.Count > 0)
                {
                    if (currentTimeBetweenWaves >= Waves.Peek().WaveData.TimeUntilWave)
                        NewWave();
                }

                if (QueuedEnemies.Count > 0)
                {
                    currentTimeBetweenEnemySpawns += (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;
                    if (currentTimeBetweenEnemySpawns >= timeBetweenEnemySpawns)
                    {
                        currentEnemyInWave++;
                        GameplayScreen.AddEnemyShip(QueuedEnemies.Dequeue(), "Wave " + CurrentWaveNumber + " Enemy " + currentEnemyInWave);
                        currentTimeBetweenEnemySpawns = 0;
                    }
                }
            }
        }

        #endregion

        #region Virtual Methods

        #endregion
    }
}
