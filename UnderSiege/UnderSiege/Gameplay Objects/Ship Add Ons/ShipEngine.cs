using _2DGameEngine.Extra_Components;
using _2DGameEngine.Game_Objects;
using _2DGameEngine.Managers;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.UI.In_Game_UI;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.Gameplay_Objects.Ship_Add_Ons
{
    public class ShipEngine : ShipAddOn
    {
        #region Properties and Fields

        public ShipEngineData ShipEngineData { get; set; }
        private EngineBlaze EngineBlaze { get; set; }
        private SoundEffect EngineSoundEffect { get; set; }

        private SoundEffectInstance soundEffectInstance;

        #endregion

        public ShipEngine(Vector2 hardPointOffset, string dataAsset, Ship parent, bool addRigidBody = true)
            : base(hardPointOffset, dataAsset, parent, addRigidBody)
        {
            ParentShip = parent;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            ShipEngineData = AssetManager.GetData<ShipEngineData>(DataAsset);
            Ship parent = Parent as Ship;

            if (ShipEngineData.EngineSoundAsset != "")
            {
                EngineSoundEffect = ScreenManager.SFX.SoundEffects[ShipEngineData.EngineSoundAsset];
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            EngineBlaze = new EngineBlaze(ParentShip, new Vector2(0, 35), new Vector2(25, 70), "Sprites\\GameObjects\\FX\\EngineBlaze", 8, 1, 0.1f, false, true, this);
            EngineBlaze.LoadContent();
            EngineBlaze.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (ParentShip.RigidBody.LinearVelocity.LengthSquared() > 1)
            {
                if (soundEffectInstance == null || soundEffectInstance.State == SoundState.Stopped)
                {
                    soundEffectInstance = EngineSoundEffect.CreateInstance();
                    soundEffectInstance.Volume = Options.SFXVolume;
                    soundEffectInstance.Play();
                }
            }
            else
            {
                if (soundEffectInstance != null)
                {
                    soundEffectInstance.Stop();
                }

                soundEffectInstance = null;
            }

            EngineBlaze.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                EngineBlaze.Draw(spriteBatch);
            }

            base.Draw(spriteBatch);
        }

        #endregion
    }
}
