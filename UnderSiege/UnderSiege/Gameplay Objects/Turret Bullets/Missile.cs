using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Game_Objects;
using _2DGameEngine.Managers;
using _2DGameEngine.Maths;
using _2DGameEngine.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects.Ship_Add_Ons;
using UnderSiege.Screens;
using UnderSiege.UI.In_Game_UI;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.Gameplay_Objects
{
    public class Missile : GameObject
    {
        #region Properties and Fields

        public MissileData MissileData { get; private set; }
        public ShipMissileTurret ParentTurret { get; set; }
        private EngineBlaze EngineBlaze { get; set; }
        private Explosion Explosion { get; set; }
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

            EngineBlaze = new EngineBlaze(this, new Vector2(0, 1.5f * Size.Y), new Vector2(Size.X, 2 * Size.Y), "Sprites\\GameObjects\\FX\\EngineBlaze", 8, 1, 0.1f, false, true, this);
            EngineBlaze.LoadContent();
            EngineBlaze.Initialize();

            Explosion = new Explosion(Vector2.Zero, new Vector2(20, 20), "Sprites\\GameObjects\\FX\\Explosion", 4, 4, 0.025f);
            Explosion.LoadContent();
            Explosion.Initialize();
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
                if (Math.Abs(angle - WorldRotation) > 0.1f)
                {
                    RigidBody.AngularVelocity = 15 * Trigonometry.GetRotateDirectionForShortestRotation(this, Target.WorldPosition);
                }
                else
                {
                    // Bad that we are assuming it is not parented to anything, but I think that is a valid assumption
                    LocalRotation = angle;
                    RigidBody.FullAngularStop();
                }
            }
            else
            {
                RigidBody.FullAngularStop();
            }

            EngineBlaze.Update(gameTime);

            if (Explosion.Animation.IsPlaying)
            {
                Explosion.Update(gameTime);
            }

            if (Explosion.Animation.Finished)
            {
                Alive = false;
            }
        }

        public override void HandleInput()
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                EngineBlaze.Draw(spriteBatch);
            }

            if (Explosion.Animation.IsPlaying)
            {
                Explosion.Draw(spriteBatch);
            }

            base.Draw(spriteBatch);
        }

        public void Explode()
        {
            Active = false;
            Visible = false;

            Explosion.LocalPosition = WorldPosition;
            Explosion.Animation.IsPlaying = true;
        }

        #endregion
    }
}
