using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using _2DGameEngine.Maths;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects.Ship_Add_Ons;
using UnderSiege.Player_Data;
using UnderSiege.Screens;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.Gameplay_Objects
{
    public class EnemyShip : Ship
    {
        #region Properties and Fields

        public EnemyShipData EnemyShipData { get; set; }

        #endregion

        public EnemyShip(Vector2 position, string dataAsset, BaseObject parent = null)
            : base(position, dataAsset, parent)
        {
            ShipType = ShipType.EnemyShip;
        }

        private const float minDistance = 300;

        #region Methods
        
        private void LoadShipAddOns()
        {
            int otherAddOnCounter = 0, engineCounter = 0;
            foreach (string addOnName in EnemyShipData.ShipAddOns)
            {
                if (!string.IsNullOrEmpty(addOnName))
                {
                    ShipAddOnData addOnData = AssetManager.GetData<ShipAddOnData>(addOnName);
                    switch (addOnData.AddOnType)
                    {
                        case "ShipTurret":
                            AddTurret(ShipData.OtherHardPoints[otherAddOnCounter], addOnData);
                            otherAddOnCounter++;
                            break;

                        case "ShipShield":
                            AddShield(ShipData.OtherHardPoints[otherAddOnCounter], addOnData);
                            otherAddOnCounter++;
                            break;

                        case "ShipEngine":
                            AddEngine(ShipData.EngineHardPoints[engineCounter], addOnData);
                            engineCounter++;
                            break;
                    }
                }
            }
        }

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            EnemyShipData = ScreenManager.Content.Load<EnemyShipData>(DataAsset);
            LoadShipAddOns();
        }

        public override void Initialize()
        {
            base.Initialize();

            foreach (ShipAddOn shipAddOn in ShipAddOns.Values)
            {
                if (shipAddOn.ShipAddOnData.Orientable)
                {
                    // If our add on is orientable, orient it so that it is facing along the line made by the centre of the ship and the addon position - the order of stuff here matters!
                    // Crude, but I think it will suffice
                    shipAddOn.LocalOrientation = Trigonometry.GetAngleOfLineBetweenPositionAndTarget(WorldPosition, shipAddOn.WorldPosition);
                    shipAddOn.LocalRotation = shipAddOn.LocalOrientation;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (TargetShip != null)
            {
                // If we are not driven by another ship, we should update the position
                if ((Parent == UnderSiegeGameplayScreen.SceneRoot || Parent == null))
                {
                    float angle = Trigonometry.GetAngleOfLineBetweenObjectAndTarget(this, TargetShip.WorldPosition);
                    if (Math.Abs(angle - WorldRotation) > 0.1f)
                    {
                        RigidBody.AngularVelocity = TotalThrust * 0.01f; /* * Trigonometry.GetRotateDirectionForShortestRotation(this, TargetShip.WorldPosition);*/
                    }
                    else
                    {
                        RigidBody.FullAngularStop();
                    }

                    if ((TargetShip.WorldPosition - WorldPosition).LengthSquared() >= minDistance * minDistance)
                    {
                        RigidBody.LinearVelocity = new Vector2(RigidBody.LinearVelocity.X, TotalThrust);
                    }
                    else
                    {
                        RigidBody.FullLinearStop();
                    }
                }
            }
            else
            {
                FindNearestTargetShip();
            }
        }

        public override void Die()
        {
            base.Die();

            UnderSiegeGameplayScreen.Enemies.Remove(this.Tag);
            Session.Money += ShipData.Price;
        }

        #endregion
    }
}
