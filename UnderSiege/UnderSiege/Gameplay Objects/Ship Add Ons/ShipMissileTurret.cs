using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.Gameplay_Objects.Ship_Add_Ons
{
    public class ShipMissileTurret : ShipTurret
    {
        #region Properties and Fields

        public ShipMissileTurretData ShipMissileTurretData { get; private set; }
        public BaseObjectManager<Missile> MissileManager { get; private set; }
        private Missile Missile { get; set; }

        private float currentFireTimer = 0;
        private long nameCounter = 0;

        #endregion

        public ShipMissileTurret(Vector2 hardPointOffset, string dataAsset, Ship parent, bool addRigidBody = true)
            : base(hardPointOffset, dataAsset, parent, addRigidBody)
        {
            MissileManager = new BaseObjectManager<Missile>();
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            ShipMissileTurretData = AssetManager.GetData<ShipMissileTurretData>(DataAsset);
        }

        public override void Initialize()
        {
            base.Initialize();

            currentFireTimer = ShipMissileTurretData.FireTimer;

            Missile = new Missile(WorldPosition, this, ShipTurretData.BulletAsset);
            Missile.LocalRotation = WorldRotation;
            Missile.LoadContent();
            Missile.Initialize();

            Missile.Visible = false;
            Missile.Active = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            currentFireTimer += (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;
            if (currentFireTimer >= ShipMissileTurretData.FireTimer)
            {
                if (Target != null)
                    Fire();
            }

            MissileManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            MissileManager.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        public override void Orient()
        {
            base.Orient();

            currentFireTimer = 0;
        }

        public override void Fire()
        {
            currentFireTimer = 0;
            // We don't care what our bullets are called as we will never refer to them by name
            nameCounter++;
            Missile clone = Missile.Clone();
            clone.RigidBody.LinearVelocity = new Vector2(-30, clone.RigidBody.LinearVelocity.Y);
            MissileManager.AddObject(clone, "Missile" + nameCounter);

            firingSoundEffectInstance = FiringSoundEffect.CreateInstance();
            firingSoundEffectInstance.Volume = Options.SFXVolume;
            firingSoundEffectInstance.Play();
        }

        public override void CheckIfDamagedTarget()
        {
            foreach (Missile missile in MissileManager.Values)
            {
                if (Target.Collider.CheckCollisionWith(missile.WorldPosition))
                {
                    Target.Damage(ShipTurretData.Damage);

                    missile.Alive = false;
                }
            }
        }

        #endregion
    }
}
