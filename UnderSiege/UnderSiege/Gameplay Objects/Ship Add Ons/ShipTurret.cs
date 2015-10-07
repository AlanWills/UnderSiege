using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.Maths;
using _2DGameEngine.Maths.Primitives;
using _2DGameEngine.Physics_Components.Movement_Behaviours;
using _2DTowerDefenceLibraryData.Turret_Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.Gameplay_Objects
{
    // This class has exactly the same functionality as the turret in 2DTowerDefenceLibrary, but to preserve all our ship add ons coming from one base class, we couldn't re-use that turret class here, so had to make this one instead
    public abstract class ShipTurret : ShipAddOn
    {
        #region Properties and Fields

        public ShipTurretData ShipTurretData { get; private set; }
        private FiringArc FiringArc { get; set; }
        public GameObject Target { get; set; }

        public override float LocalRotation
        {
            get { return base.LocalRotation; }
            set
            {
                base.LocalRotation = value;//(float)MathHelper.Clamp(value, LocalOrientation - ShipTurretData.ArcWidth * 0.5f, LocalOrientation + ShipTurretData.ArcWidth * 0.5f);
            }
        }

        public float TargetRotation
        {
            get
            {
                // Maybe add a clamping thing here
                if (Target != null)
                    return MathHelper.WrapAngle(Trigonometry.GetAngleOfLineBetweenPositionAndTarget(WorldPosition, Target.WorldPosition) - Parent.WorldRotation);

                return LocalOrientation;
            }
        }

        public UIObject TargetingLine { get; private set; }

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

            TargetingLine = new UIObject(new Vector2(0, -ShipTurretData.Range * 0.5f), "Sprites\\UI\\InGameUI\\FiringLineUI", this);
            TargetingLine.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();

            FiringArc = new FiringArc(this);
            FiringArc.LoadContent();
            FiringArc.Initialize();

            TargetingLine.Initialize();
            TargetingLine.Size = new Vector2(TargetingLine.Size.X, ShipTurretData.Range);
            TargetingLine.Opacity = 0.25f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (ParentShip.TargetShip != null)
            {
                FindTarget();
            }
            else
            {
                Target = null;
            }

            LocalRotation = MathHelper.Lerp(LocalRotation, TargetRotation, (float)gameTime.ElapsedGameTime.Milliseconds / 300f);
       
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
                FiringArc.Draw(spriteBatch);
            }
        }

        public abstract void Fire();
        public abstract void CheckIfDamagedShip(Ship ship);

        private void FindTarget()
        {
            // If we have a valid ship add on, then do no continue with finding a target
            if (Target != null && ParentShip.TargetShip.AddOnExists(Target as ShipAddOn))
                return;

            // If the target ship has no addons check the ship to see if it is in range
            if (ParentShip.TargetShip.TotalAddOns == 0)
            {
                float distanceToTargetSquared = (WorldPosition - ParentShip.TargetShip.WorldPosition).LengthSquared();
                if (distanceToTargetSquared <= ShipTurretData.Range * ShipTurretData.Range)
                {
                    // We have a new target
                    Target = ParentShip.TargetShip;
                }
                else
                {
                    Target = null;
                }

                return;
            }

            GameObject target = null;
            float currentRangeToTargetSquared = float.MaxValue;

            foreach (List<ShipAddOn> addOnList in ParentShip.TargetShip.ShipAddOns.Values)
            {
                foreach (ShipAddOn shipAddOn in addOnList)
                {
                    // This current maths is horrendously inefficient - I can't think of a better way at the moment
                    /*if (!gameObject.Collider.CheckCollisionWith(FiringArc.RangeArc))
                    {
                        continue;
                    }*/

                    // If the firing arc does not contain the gameObject position then continue
                    if (!FiringArc.ContainsPoint(shipAddOn.WorldPosition))
                        continue;

                    // Target is in firing arc, so we now need to check if it is in range
                    float distanceToTargetSquared = (WorldPosition - shipAddOn.WorldPosition).LengthSquared();
                    if (distanceToTargetSquared <= ShipTurretData.Range * ShipTurretData.Range && distanceToTargetSquared <= currentRangeToTargetSquared)
                    {
                        // We have a new target
                        target = shipAddOn;
                        currentRangeToTargetSquared = distanceToTargetSquared;
                    }
                }
            }

            // If we don't have a ship add on in range, check the target to be the ship
            if (target == null)
            {
                float distanceToTargetSquared = (WorldPosition - ParentShip.TargetShip.WorldPosition).LengthSquared();
                if (distanceToTargetSquared <= ShipTurretData.Range * ShipTurretData.Range)
                {
                    // We have a new target
                    Target = ParentShip.TargetShip;
                }
                else
                {
                    Target = null;
                }
            }
            else
            {
                Target = target;
            }
        }

        #endregion
    }
}
