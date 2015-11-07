using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using _2DGameEngine.Maths;
using _2DGameEngine.Screens;
using _2DTowerDefenceLibraryData.Bullet_Data;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects.Ship_Add_Ons;

namespace UnderSiege.Gameplay_Objects
{
    public class Bullet : GameObject
    {
        #region Properties and Fields

        public BulletData BulletData { get; private set; }
        public ShipKineticTurret ParentTurret { get; set; }

        private TimeSpan currentLifeTimer = TimeSpan.FromSeconds(0);

        #endregion

        public Bullet(Vector2 position, ShipKineticTurret parentTurret, string dataAsset)
            : base(position, dataAsset, GameplayScreen.SceneRoot)
        {
            ParentTurret = parentTurret;
        }

        #region Methods

        public Bullet Clone()
        {
            // Can't parent bullet to turret because re-orienting makes it change bullets too so have to parent to scene root and set position manually
            Bullet clone = new Bullet(ParentTurret.WorldPosition, ParentTurret, DataAsset);
            clone.LocalRotation = ParentTurret.WorldRotation;
            clone.BulletData = BulletData;
            clone.Texture = Texture;
            clone.Size = Size;
            clone.SourceRectangle = SourceRectangle;

            // We will create a rigidbody and when we initialize, will set the appropriate values
            clone.Initialize();

            return clone;
        }

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();
            Debug.Assert(BaseData != null);

            BulletData = BaseData as BulletData;
            Debug.Assert(BulletData != null);
        }

        public override void Initialize()
        {
            base.Initialize();

            LocalRotation += RandomNumberGenerator.RandomFloat(-ParentTurret.ShipKineticTurretData.Spread, ParentTurret.ShipKineticTurretData.Spread);
            RigidBody.MaxLinearVelocity = new Vector2(RigidBody.MaxLinearVelocity.X, BulletData.MaxSpeed);

            // If the acceleration is non-zero, we change the acceleration of the bullet.  Otherwise, we set the bullet's T velocity Y component to be it's max value
            if (BulletData.LinearAcceleration != 0)
                RigidBody.LinearAcceleration = new Vector2(RigidBody.LinearAcceleration.X, BulletData.LinearAcceleration);
            else
                RigidBody.LinearVelocity = new Vector2(RigidBody.LinearVelocity.X, BulletData.MaxSpeed);
        }

        public override void AddCollider()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            currentLifeTimer += gameTime.ElapsedGameTime;
            if (currentLifeTimer >= TimeSpan.FromSeconds(BulletData.BulletLifeTime))
                Die();
        }

        public override void HandleInput()
        {
            
        }

        #endregion
    }
}
