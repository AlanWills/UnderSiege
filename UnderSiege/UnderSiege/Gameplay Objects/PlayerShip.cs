using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Screens;
using UnderSiege.UI;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.Gameplay_Objects
{
    public class PlayerShip : Ship
    {
        #region Properties and Fields

        public PlayerShipData PlayerShipData { get; private set; }

        public bool DrawOtherHardPoints
        {
            set
            {
                HardPointUI.DrawOtherHardPointUI = value;

                if (value)
                    SetOpacity(0.35f);
                else
                    SetOpacity(1f);
            }
        }

        public bool DrawEngineHardPoints
        {
            set
            {
                HardPointUI.DrawEngineHardPointUI = value;

                if (value)
                    SetOpacity(0.35f);
                else
                    SetOpacity(1f);
            }
        }

        public HardPointUIManager HardPointUI { get; private set; }

        #endregion

        public PlayerShip(Vector2 position, string dataAsset, BaseObject parent = null)
            : base(position, dataAsset, parent)
        {
            ShipType = ShipType.AlliedShip;
            HardPointUI = new HardPointUIManager(this);
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            PlayerShipData = AssetManager.GetData<PlayerShipData>(DataAsset);
        }

        public override void Initialize()
        {
            base.Initialize();

            HardPointUI.Initialize(ShipData);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            HardPointUI.Update(gameTime);
        }

        protected override void IfSelected()
        {
            base.IfSelected();

            Vector2 delta = Vector2.Zero;
            float angleDelta = 0;

            if (InputHandler.KeyDown(Keys.W))
            {
                delta.Y += TotalThrust;
            }
            if (InputHandler.KeyDown(Keys.S))
            {
                delta.Y -= TotalThrust;
            }
            if (InputHandler.KeyDown(Keys.A))
            {
                angleDelta -= TotalThrust * 0.01f;
            }
            if (InputHandler.KeyDown(Keys.D))
            {
                angleDelta += TotalThrust * 0.01f;
            }

            RigidBody.LinearAcceleration = delta;
            RigidBody.AngularAcceleration = angleDelta;
        }

        public override void DrawInGameUI(SpriteBatch spriteBatch)
        {
            base.DrawInGameUI(spriteBatch);

            if (Visible)
            {
                HardPointUI.Draw(spriteBatch);
            }
        }

        public override void DealWithHardPoint(Vector2 hardPoint, bool isEngine)
        {
            base.DealWithHardPoint(hardPoint, isEngine);

            if (isEngine)
            {
                HardPointUI.Disable(hardPoint, HardPointType.Engine);
            }
            else
            {
                HardPointUI.Disable(hardPoint, HardPointType.Other);
            }
        }

        public override void RemoveAddOn(ShipAddOn addOn)
        {
            base.RemoveAddOn(addOn);

            if (addOn.ShipAddOnData.AddOnType == "ShipEngine")
            {
                HardPointUI.Enable(addOn.HardPointOffset, HardPointType.Engine);
            }
            else
            {
                HardPointUI.Enable(addOn.HardPointOffset, HardPointType.Other);
            }
        }

        public override void Die()
        {
            base.Die();

            UnderSiegeGameplayScreen.Allies.Remove(this.Tag);
        }

        #endregion
    }
}
