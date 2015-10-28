using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using _2DGameEngine.Object_Properties;
using _2DGameEngine.UI_Objects;
using _2DTowerDefenceLibraryData.Turret_Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects.Ship_Add_Ons;
using UnderSiege.Managers;
using UnderSiege.Screens;
using UnderSiege.UI;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.Gameplay_Objects
{
    public enum ShipType { AlliedShip, EnemyShip }

    public class Ship : DamageableGameObject
    {
        #region Properties and Fields

        public ShipData ShipData
        {
            get;
            private set;
        }

        public Ship TargetShip { get; set; }

        public ShipAddOnManager ShipAddOns
        {
            get;
            set;
        }

        public float TotalThrust
        {
            get
            {
                float totalThrust = 0;
                foreach (ShipEngine engine in ShipAddOns.ShipEngines)
                {
                    totalThrust += engine.ShipEngineData.Thrust;
                }

                return totalThrust;
            }
        }

        public List<Vector2> OtherHardPoints { get; set; }
        public List<Vector2> EngineHardPoints { get; set; }

        public ShipType ShipType { get; set; }

        private Bar HullHealthBar { get; set; }

        #endregion

        public Ship(Vector2 position, string dataAsset, BaseObject parent = null)
            : base(position, dataAsset, parent, true)
        {
            ShipAddOns = new ShipAddOnManager(this);
        }

        #region Methods

        public void AddTurret(Vector2 hardPoint, ShipAddOnData addOnData)
        {
            ShipTurretData turretData = addOnData as ShipTurretData;
            string dataAsset = AssetManager.GetKeyFromData(turretData);

            ShipTurret turret = null;

            switch (turretData.TurretType)
            {
                case "Kinetic" :
                    turret = new ShipKineticTurret(hardPoint, dataAsset, this, true);
                    break;
                case "Missile" :
                    turret = new ShipMissileTurret(hardPoint, dataAsset, this, true);
                    break;
                case "Beam" :
                    turret = new ShipBeamTurret(hardPoint, dataAsset, this, true);
                    break;
            }

            ShipAddOns.AddObject(turret);

            DealWithHardPoint(hardPoint, false);
        }

        public void AddShield(Vector2 hardPoint, ShipAddOnData addOnData)
        {
            ShipShield shield = new ShipShield(hardPoint, AssetManager.GetKeyFromData(addOnData), this, true);
            ShipAddOns.AddObject(shield);

            DealWithHardPoint(hardPoint, false);
        }

        public void AddEngine(Vector2 hardPoint, ShipAddOnData addOnData)
        {
            ShipEngine engine = new ShipEngine(hardPoint, AssetManager.GetKeyFromData(addOnData), this, true);
            ShipAddOns.AddObject(engine);

            DealWithHardPoint(hardPoint, true);
        }

        public virtual void DealWithHardPoint(Vector2 hardPoint, bool isEngine)
        {
            if (isEngine)
            {
                EngineHardPoints.Remove(hardPoint);
            }
            else
            {
                OtherHardPoints.Remove(hardPoint);
            }
        }

        public void SetOpacity(float opacity)
        {
            Opacity = opacity;

            foreach (ShipAddOn shipAddOn in ShipAddOns.Values)
            {
                shipAddOn.Opacity = opacity;
            }
        }

        public void FindNearestTargetShip()
        {
            Ship target = null;
            float currentLength = float.MaxValue;
            List<Ship> ships = ShipType == ShipType.AlliedShip ? UnderSiegeGameplayScreen.Enemies.Values.ToList<Ship>() : UnderSiegeGameplayScreen.Allies.Values.ToList<Ship>();
            foreach (Ship ship in ships)
            {
                float length = (ship.WorldPosition - WorldPosition).LengthSquared();
                // Add in a buffer amount to stop rapid changing of target
                if (length + 50 <= currentLength)
                {
                    target = ship;
                    currentLength = length;
                }
            }

            TargetShip = target;
        }

        public bool AddOnExists(ShipAddOn addOn)
        {
            if (addOn != null)
                return ShipAddOns.Values.Find(x => x == addOn) != null;

            return false;
        }

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            ShipData = AssetManager.GetData<ShipData>(DataAsset);

            OtherHardPoints = new List<Vector2>();
            foreach (Vector2 hardPoint in ShipData.OtherHardPoints)
            {
                // Create a complete instance copy for the hardpoints, because the data is shared so cannot remove from there
                OtherHardPoints.Add(new Vector2(hardPoint.X, hardPoint.Y));
            }

            EngineHardPoints = new List<Vector2>();
            foreach (Vector2 hardPoint in ShipData.EngineHardPoints)
            {
                EngineHardPoints.Add(new Vector2(hardPoint.X, hardPoint.Y));
            }

            HullHealthBar = new Bar(new Vector2(0, Size.Y * 0.5f + 5), new Vector2(Size.X, 5), "Sprites\\UI\\Bars\\ShipHullBar", ShipData.Health, "", this);
            HullHealthBar.Colour = Color.LightGreen;
            HullHealthBar.LoadContent();
            HullHealthBar.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Active)
            {
                // When we move we want a nice drag feel
                RigidBody.AngularVelocity *= 0.95f;
                RigidBody.LinearVelocity *= 0.95f;
                RigidBody.AngularAcceleration *= 0.95f;
                RigidBody.LinearAcceleration *= 0.95f;

                if (TargetShip == null || !TargetShip.Alive)
                {
                    FindNearestTargetShip();
                }

                ShipAddOns.Update(gameTime);

                HullHealthBar.UpdateValue(CurrentHealth);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Visible)
            {
                ShipAddOns.Draw(spriteBatch);
            }
        }

        public override void DrawInGameUI(SpriteBatch spriteBatch)
        {
            base.DrawInGameUI(spriteBatch);

            ShipAddOns.DrawInGameUI(spriteBatch);

            HullHealthBar.Draw(spriteBatch);
        }

        public override void HandleInput()
        {
            base.HandleInput();

            ShipAddOns.HandleInput();
        }

        #endregion
    }
}
