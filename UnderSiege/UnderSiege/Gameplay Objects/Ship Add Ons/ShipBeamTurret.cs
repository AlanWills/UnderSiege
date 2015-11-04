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

        private bool BeamCharged { get { return Beam.Opacity == 1; } }

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

            // Create the firing sound effect instance - we will just pause and unpause this
            // Bit of a hack, we play, but with 0 volume so that we can just pause and resume later, rather than having to check in Fire() whether it has been played yet
            firingSoundEffectInstance = FiringSoundEffect.CreateInstance();
            firingSoundEffectInstance.IsLooped = true;
            firingSoundEffectInstance.Volume = 0;
            firingSoundEffectInstance.Play();

            firing = false;
        }

        public override void Fire()
        {
            // Change the opacity
            firing = true;
            Beam.Size = new Vector2(Size.X, (WorldPosition - Target.WorldPosition).Length());

            if (BeamCharged)
            {
                firingSoundEffectInstance.Volume = Options.SFXVolume;
                firingSoundEffectInstance.Resume();
            }
        }

        public override void CheckIfDamagedTarget()
        {
            // Only do the damaging if the beam has reached full intensity
            if (BeamCharged)
            {
                // We have a target a ship addon that isn't a shield - so we need to check for shield interactions
                if (!(Target is ShipShield) && !(Target is Ship))
                {
                    foreach (ShipShield shipShield in (Target as ShipAddOn).ParentShip.ShipAddOns.ShipShields)
                    {
                        if (shipShield.Collider.CheckCollisionWith(Beam.BeamLine))
                        {
                            shipShield.Damage(ShipTurretData.Damage);
                            break;
                        }
                    }

                    if (Target.Collider.CheckCollisionWith(Beam.BeamLine))
                    {
                        Target.Damage(ShipTurretData.Damage);
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
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Beam.Update(gameTime);

            if (Target != null)
            {
                float angleToTarget = Trigonometry.GetAngleOfLineBetweenObjectAndTarget(this, Target.WorldPosition);
                if (Math.Abs(angleToTarget - WorldRotation) <= 0.1f)
                {
                    Fire();
                }
                else
                {
                    firing = false;

                    firingSoundEffectInstance.Pause();
                }
            }
            else
            {
                firing = false;

                firingSoundEffectInstance.Pause();
            }

            if (firing)
                Beam.Opacity = (float)Math.Min(Beam.Opacity + 0.02f, 1);
            else
                Beam.Opacity = (float)Math.Max(Beam.Opacity - 0.05f, 0);

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
