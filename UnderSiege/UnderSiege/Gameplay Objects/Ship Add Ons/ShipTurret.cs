﻿using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.Maths;
using _2DGameEngine.Maths.Primitives;
using _2DGameEngine.Physics_Components.Movement_Behaviours;
using _2DTowerDefenceLibraryData.Turret_Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Screens;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.Gameplay_Objects
{
    // This class has exactly the same functionality as the turret in 2DTowerDefenceLibrary, but to preserve all our ship add ons coming from one base class, we couldn't re-use that turret class here, so had to make this one instead
    public abstract class ShipTurret : ShipAddOn
    {
        #region Properties and Fields

        public ShipTurretData ShipTurretData { get; private set; }
        private FiringArc FiringArc { get; set; }
        public DamageableGameObject Target { get; set; }

        public override float LocalRotation
        {
            get { return base.LocalRotation; }
            set
            {
                base.LocalRotation = value;//(float)MathHelper.Clamp(value, LocalOrientation - ShipTurretData.ArcWidth * 0.5f, LocalOrientation + ShipTurretData.ArcWidth * 0.5f);
            }
        }

        public UIObject TargetingLine { get; private set; }
        protected SoundEffect FiringSoundEffect { get; private set; }
        protected SoundEffectInstance firingSoundEffectInstance;

        #endregion

        public ShipTurret(Vector2 hardPointOffset, string dataAsset, Ship parent, bool addRigidBody = true)
            : base(hardPointOffset, dataAsset, parent, addRigidBody)
        {
            ParentShip = parent;
        }

        #region Methods

        public void ResetRotation()
        {
            LocalRotation = LocalOrientation;
        }

        #endregion

        #region Events

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            if (BaseData != null)
            {
                ShipTurretData = AssetManager.GetData<ShipTurretData>(DataAsset);
            }

            if (ShipTurretData.TurretFiringAsset != "")
            {
                FiringSoundEffect = ScreenManager.SFX.SoundEffects[ShipTurretData.TurretFiringAsset];
            }

            TargetingLine = new InGameUIObject(new Vector2(0, -ShipTurretData.Range * 0.5f), "Sprites\\UI\\InGameUI\\FiringLineUI", this);
            TargetingLine.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();

            FiringArc = new FiringArc(this);
            FiringArc.Initialize();

            TargetingLine.Initialize();
            TargetingLine.Size = new Vector2(TargetingLine.Size.X, ShipTurretData.Range);
            TargetingLine.Opacity = 0.25f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            FindTarget();

            if (Target != null)
            {
                CheckIfDamagedTarget();

                // Rotate to target
                float angle = Trigonometry.GetAngleOfLineBetweenObjectAndTarget(this, Target.WorldPosition);
                if (Math.Abs(angle - WorldRotation) > 0.1f)
                {
                    RigidBody.AngularVelocity = 3.5f * Trigonometry.GetRotateDirectionForShortestRotation(this, Target.WorldPosition);
                }
                else
                {
                    LocalRotation = angle - Parent.WorldRotation;
                    RigidBody.FullAngularStop();
                }
            }
       
            FiringArc.Visible = MouseOver || IsSelected;
            FiringArc.Update(gameTime);
        }

        public override void DrawInGameUI(SpriteBatch spriteBatch)
        {
            base.DrawInGameUI(spriteBatch);

            // Draw the firing arc only if we have a PlayerShip
            if (ParentShip.ShipType == ShipType.AlliedShip)
            {
                TargetingLine.Draw(spriteBatch);
            }
        }

        public abstract void Fire();
        public abstract void CheckIfDamagedTarget();

        private void FindTarget()
        {
            Ship target = null;
            float currentRangeToTargetSquared = float.MaxValue;

            bool useEnemies = ParentShip.ShipType == ShipType.AlliedShip;
            foreach (Ship ship in useEnemies ? UnderSiegeGameplayScreen.Enemies.Values.AsEnumerable<Ship>() : UnderSiegeGameplayScreen.Allies.Values.AsEnumerable<Ship>())
            {
                float distanceToTargetSquared = Vector2.Subtract(WorldPosition, ship.WorldPosition).LengthSquared();
                // Add buffer amount of 50 to stop rapid target changing
                if (distanceToTargetSquared <= ShipTurretData.Range * ShipTurretData.Range && distanceToTargetSquared + 50 <= currentRangeToTargetSquared)
                {
                    // We have a new target
                    target = ship;
                    currentRangeToTargetSquared = distanceToTargetSquared;
                }
            }

            Target = target;

            // Target the closest add ons now
            if (target != null)
            {
                ShipAddOn targetAddOn = null;
                currentRangeToTargetSquared = float.MaxValue;

                foreach (ShipAddOn shipAddOn in target.ShipAddOns.Values)
                {
                    float distanceToTargetSquared = Vector2.Subtract(WorldPosition, shipAddOn.WorldPosition).LengthSquared();
                    // Add buffer amount of 50 to stop rapid target changing
                    if (distanceToTargetSquared <= ShipTurretData.Range * ShipTurretData.Range && distanceToTargetSquared + 50 <= currentRangeToTargetSquared)
                    {
                        // We have a new target
                        targetAddOn = shipAddOn;
                        currentRangeToTargetSquared = distanceToTargetSquared;
                    }
                }

                if (targetAddOn != null)
                {
                    Target = targetAddOn;
                }
            }
        }

        #endregion
    }
}
