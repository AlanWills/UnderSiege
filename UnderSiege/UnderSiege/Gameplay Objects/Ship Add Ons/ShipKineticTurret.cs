using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.Gameplay_Objects.Ship_Add_Ons
{
    public class ShipKineticTurret : ShipTurret
    {
        #region Properties and Fields

        public ShipKineticTurretData ShipKineticTurretData { get; set; }
        public BaseObjectManager<Bullet> BulletManager { get; private set; }
        private Bullet Bullet { get; set; }

        private float currentFireTimer;
        private long nameCounter = 0;

        #endregion

        public ShipKineticTurret(Vector2 hardPointOffset, string dataAsset, Ship parent, bool addRigidBody = true)
            : base(hardPointOffset, dataAsset, parent, addRigidBody)
        {
            BulletManager = new BaseObjectManager<Bullet>();
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            Debug.Assert(BaseData != null);
            ShipKineticTurretData = BaseData as ShipKineticTurretData;

            Debug.Assert(ShipKineticTurretData != null);
        }

        public override void Initialize()
        {
            base.Initialize();

            currentFireTimer = ShipKineticTurretData.FireTimer;

            // Can't parent bullet to turret because re-orienting makes it change bullets too so have to parent to scene root and set position manually
            Bullet = new Bullet(WorldPosition, this, ShipTurretData.BulletAsset);
            Bullet.LoadContent();
            Bullet.Initialize();
            Bullet.LocalRotation = WorldRotation;

            // Make sure we do not draw or update the bullet template which we will clone from
            Bullet.Visible = false;
            Bullet.Active = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            currentFireTimer += (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;
            if (currentFireTimer >= ShipKineticTurretData.FireTimer)
            {
                if (Target != null)
                    Fire();
            }

            BulletManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BulletManager.Draw(spriteBatch);

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
            BulletManager.AddObject(Bullet.Clone(), "Bullet" + nameCounter);

            Debug.Assert(FiringSoundEffect != null);
            firingSoundEffectInstance = FiringSoundEffect.CreateInstance();
            firingSoundEffectInstance.Volume = Options.SFXVolume;
            firingSoundEffectInstance.Play();
        }

        public override void CheckIfDamagedTarget()
        {
            foreach (Bullet bullet in BulletManager.Values)
            {
                // We have a target a ship addon that isn't a shield - so we need to check for shield interactions
                if (!(Target is ShipShield) && !(Target is Ship))
                {
                    foreach (ShipShield shipShield in (Target as ShipAddOn).ParentShip.ShipAddOns.ShipShields)
                    {
                        if (shipShield.Collider.CheckCollisionWith(bullet.WorldPosition))
                        {
                            shipShield.Damage(ShipTurretData.Damage);

                            bullet.Alive = false;
                            break;
                        }
                    }

                    // We haven't collided
                    if (bullet.Alive == false)
                    {
                        continue;
                    }
                    else
                    {
                        if (Target.Collider.CheckCollisionWith(bullet.WorldPosition))
                        {
                            Target.Damage(ShipTurretData.Damage);

                            bullet.Alive = false;
                        }
                    }
                }
                // Continue normally
                else
                {
                    if (Target.Collider.CheckCollisionWith(bullet.WorldPosition))
                    {
                        Target.Damage(ShipTurretData.Damage);

                        bullet.Alive = false;
                    }
                }
            }
        }

        #endregion
    }
}