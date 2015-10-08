using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.Maths;
using _2DGameEngine.Physics_Components.Movement_Behaviours;
using _2DTowerDefenceEngine.Bullets;
using _2DTowerDefenceLibraryData.Turret_Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DTowerDefenceEngine.Turrets
{
    public class Turret : GameObject
    {
        #region Properties and Fields

        public TurretData TurretData { get; private set; }
        public BaseObjectManager<Bullet> BulletManager { get; private set; }
        private Bullet Bullet { get; set; }
        private FiringArc FiringArc { get; set; }
        public Vector2 HardPointOffset { get; private set; }

        private GameObject target = null;
        public GameObject Target 
        {
            get { return target; }
            set
            {
                if (value != null)
                {
                    // Add follow target behaviour
                    MovementBehaviours.AddBehaviour("Point At Target", new PointAtTarget(new PointAtTargetArgs(this, value)));
                }
                else
                {
                    MovementBehaviours.RemoveAllBehaviours("Point At Target");
                }

                target = value;
            }
        }

        public override float LocalRotation
        {
            get { return base.LocalRotation; }
            set
            {
                // This is going to be wrong at the moment - need to incorporate parent hierarchy
                if (TurretData != null)
                    value = (float)MathHelper.Clamp(value, Orientation - TurretData.ArcWidth * 0.5f, Orientation + TurretData.ArcWidth * 0.5f);

                base.LocalRotation = value;
            }
        }

        // Our object has a rotation - but that determines the actual turret sprite's rotation
        // We also need to save the direction the turret firing arc is facing - this cannot move freely
        public float Orientation
        {
            get;
            set;
        }

        private float currentFireTimer;
        private long nameCounter = 0;

        #endregion

        public Turret(Vector2 position, float orientation, string dataAsset, bool addRigidBody = true)
            : base(position, dataAsset, null, addRigidBody)
        {
            LocalPosition = position;
            Parent = null;
            Orientation = orientation;
            LocalRotation = orientation;
            BulletManager = new BaseObjectManager<Bullet>();
        }

        public Turret(Vector2 hardPointOffset, float orientation, string dataAsset, GameObject parent, bool addRigidBody = true)
            : base(dataAsset, parent, addRigidBody)
        {
            HardPointOffset = hardPointOffset;
            Orientation = orientation;
            LocalRotation = orientation;
            BulletManager = new BaseObjectManager<Bullet>();
        }

        #region Methods

        #endregion

        #region Events

        protected override void IfSelected()
        {
            base.IfSelected();

            // Here we are changing the orientation of the turret, so it should not fire
            Orientation = Trigonometry.GetAngleOfLineBetweenObjectAndTarget(this, GameMouse.InGamePosition);
            LocalRotation = Orientation;
            currentFireTimer = 0;
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
                    TurretData = AssetManager.GetData<TurretData>(DataAsset);
                }
                catch { Console.WriteLine("No texture or data could be loaded."); }
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            currentFireTimer = TurretData.FireTimer;

            FiringArc = new FiringArc(this);

            Bullet = new Bullet(WorldPosition, this, TurretData.BulletAsset);
            Bullet.LoadContent();
            Bullet.Initialize();

            // Make sure we do not draw or update the bullet template which we will clone from
            Bullet.Visible = false;
            Bullet.Active = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            currentFireTimer += (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;
            if (currentFireTimer >= TurretData.FireTimer)
            {
                FireBullet();
            }

            BulletManager.Update(gameTime);
            FiringArc.Visible = MouseOver || IsSelected;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BulletManager.Draw(spriteBatch);

            base.Draw(spriteBatch);

            FiringArc.Draw(spriteBatch);
        }

        public virtual void FireBullet()
        {
            currentFireTimer = 0;
            // We don't care what our bullets are called as we will never refer to them by name
            nameCounter++;
            BulletManager.AddObject(Bullet.Clone(), "Bullet" + nameCounter);
        }

        #endregion
    }
}
