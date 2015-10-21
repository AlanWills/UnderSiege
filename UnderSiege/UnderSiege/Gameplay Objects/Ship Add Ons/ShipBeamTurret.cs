using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.Maths;
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
    public class ShipBeamTurret : ShipTurret
    {
        #region Properties and Fields

        public ShipBeamTurretData ShipBeamTurretData { get; private set; }
        private Beam Beam { get; set; }

        private bool firing = false;

        #endregion

        public ShipBeamTurret(Vector2 hardPointOffset, string dataAsset, Ship parent, bool addRigidBody = true)
            : base(hardPointOffset, dataAsset, parent, addRigidBody)
        {
            
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            ShipBeamTurretData = AssetManager.GetData<ShipBeamTurretData>(DataAsset);
        }

        public override void Initialize()
        {
            base.Initialize();

            Beam = new Beam(this, ShipBeamTurretData.BulletAsset, true);
            Beam.LoadContent();
            Beam.Initialize();
            Beam.Opacity = 0;

            firing = false;
        }

        public override void Fire()
        {
            // Change the opacity
            firing = true;
            Beam.Size = new Vector2(Size.X, (WorldPosition - Target.WorldPosition).Length());

            if (firingSoundEffectInstance == null || firingSoundEffectInstance.State == SoundState.Stopped)
            {
                firingSoundEffectInstance = FiringSoundEffect.CreateInstance();
                firingSoundEffectInstance.Volume = Options.SFXVolume;
            }
        }

        public override void CheckIfDamagedTarget()
        {
            // We have a target a ship addon that isn't a shield - so we need to check for shield interactions
            if (!(Target is ShipShield) && !(Target is Ship))
            {
                foreach (ShipShield shipShield in (Target as ShipAddOn).ParentShip.ShipAddOns.ShipShields)
                {
                    if (shipShield.Collider.CheckCollisionWith(Beam.BeamLine))
                    {
                        shipShield.Damage(ShipTurretData.Damage);
                    }
                }
            }
            // Continue normally
            else
            {
                if (Target.Collider.CheckCollisionWith(Beam.BeamLine))
                {
                    Target.Damage(ShipTurretData.Damage);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Beam.Update(gameTime);

            if (Target != null)
            {
                float angleToTarget = Trigonometry.GetAngleOfLineBetweenObjectAndTarget(this, Target.WorldPosition);
                if (Math.Abs(angleToTarget - WorldRotation) < 0.05f)
                {
                    Fire();
                }
                else
                {
                    firing = false;

                    if (firingSoundEffectInstance != null)
                    {
                        firingSoundEffectInstance.Stop();
                        firingSoundEffectInstance = null;
                    }
                }
            }
            else
            {
                firing = false;
            }

            if (firing)
                Beam.Opacity = (float)Math.Min(Beam.Opacity + 0.01f, 1);
            else
                Beam.Opacity = (float)Math.Max(Beam.Opacity - 0.01f, 0);

            TargetingLine.Visible = Opacity == 0 ? true : false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Beam.Draw(spriteBatch);
        }

        #endregion
    }
}
