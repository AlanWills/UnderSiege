using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using _2DGameEngine.Maths;
using _2DTowerDefenceEngine.Turrets;
using _2DTowerDefenceLibraryData.Bullet_Data;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DTowerDefenceEngine.Bullets
{
    public class Bullet : GameObject
    {
        #region Properties and Fields

        public BulletData BulletData { get; private set; }
        public Turret ParentTurret { get; set; }

        private TimeSpan currentLifeTimer = TimeSpan.FromSeconds(0);

        #endregion

        public Bullet(Vector2 position, Turret parentTurret, string dataAsset = "")
            : base(position, dataAsset)
        {
            ParentTurret = parentTurret;   
        }

        #region Methods

        public Bullet Clone()
        {
            Bullet clone = new Bullet(ParentTurret.WorldPosition, ParentTurret, DataAsset);
            // We will create a rigidbody and when we initialize, will set the appropriate values
            clone.BulletData = BulletData;
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

            if (BaseData != null)
            {
                try
                {
                    BulletData = AssetManager.GetData<BulletData>(DataAsset);
                }
                catch { Console.WriteLine("No texture or data could be loaded."); }
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            LocalRotation = RandomNumberGenerator.RandomFloat(-ParentTurret.TurretData.Spread, ParentTurret.TurretData.Spread);
            RigidBody.MaxLinearVelocity = new Vector2(RigidBody.MaxLinearVelocity.X, BulletData.MaxSpeed);

            // If the acceleration is non-zero, we change the acceleration of the bullet.  Otherwise, we set the bullet's T velocity Y component to be it's max value
            if (BulletData.LinearAcceleration != 0)
                RigidBody.LinearAcceleration = new Vector2(RigidBody.LinearAcceleration.X, BulletData.LinearAcceleration);
            else
                RigidBody.LinearVelocity = new Vector2(RigidBody.LinearVelocity.X, BulletData.MaxSpeed);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            currentLifeTimer += gameTime.ElapsedGameTime;
            if (currentLifeTimer >= TimeSpan.FromSeconds(BulletData.BulletLifeTime))
                Die();
        }

        #endregion
    }
}
