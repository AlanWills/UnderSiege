using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using _2DGameEngine.Maths;
using _2DGameEngine.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects.Ship_Add_Ons;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.Gameplay_Objects
{
    public class Missile : GameObject
    {
        #region Properties and Fields

        public MissileData MissileData { get; private set; }
        public ShipMissileTurret ParentTurret { get; set; }
        private BaseObject Target { get; set; }

        private TimeSpan currentLifeTimer = TimeSpan.FromSeconds(0);

        #endregion

        public Missile(Vector2 position, ShipMissileTurret parentTurret, string dataAsset = "")
            : base(position, dataAsset, GameplayScreen.SceneRoot)
        {
            ParentTurret = parentTurret;
            Target = ParentTurret.Target;
        }

        #region Methods

        public Missile Clone()
        {
            Missile clone = new Missile(ParentTurret.WorldPosition, ParentTurret, DataAsset);
            clone.LocalRotation = ParentTurret.WorldRotation;
            clone.MissileData = MissileData;
            clone.Texture = Texture;
            clone.Size = Size;
            clone.SourceRectangle = SourceRectangle;

            clone.Initialize();

            return clone;
        }

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            MissileData = AssetManager.GetData<MissileData>(DataAsset);
        }

        public override void Initialize()
        {
            base.Initialize();

            RigidBody.MaxLinearVelocity = new Vector2(RigidBody.MaxLinearVelocity.X, MissileData.MaxSpeed);

            // If the acceleration is non-zero, we change the acceleration of the bullet.  Otherwise, we set the bullet's T velocity Y component to be it's max value
            if (MissileData.LinearAcceleration != 0)
                RigidBody.LinearAcceleration = new Vector2(RigidBody.LinearAcceleration.X, MissileData.LinearAcceleration);
            else
                RigidBody.LinearVelocity = new Vector2(RigidBody.LinearVelocity.X, MissileData.MaxSpeed);
        }

        public override void AddCollider()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            currentLifeTimer += gameTime.ElapsedGameTime;
            if (currentLifeTimer >= TimeSpan.FromSeconds(MissileData.BulletLifeTime))
                Die();

            if (RigidBody.LinearVelocity.X < 0)
            {
                RigidBody.LinearAcceleration = new Vector2(500, RigidBody.LinearAcceleration.Y);
            }
            else
            {
                RigidBody.LinearVelocity = new Vector2(0, RigidBody.LinearVelocity.Y);
                RigidBody.LinearAcceleration = new Vector2(0, RigidBody.LinearAcceleration.Y);
            }

            if (Target != null && Target.Alive)
            {
                float angle = Trigonometry.GetAngleOfLineBetweenObjectAndTarget(this, Target.WorldPosition);
                if (Math.Abs(angle - WorldRotation) > 0.05f)
                {
                    RigidBody.AngularVelocity = 10 * Trigonometry.GetRotateDirectionForShortestRotation(this, Target.WorldPosition);
                }
                else
                {
                    RigidBody.FullAngularStop();
                }
            }
            else
            {
                RigidBody.FullAngularStop();
            }
        }

        public override void HandleInput()
        {
            
        }

        #endregion
    }
}
