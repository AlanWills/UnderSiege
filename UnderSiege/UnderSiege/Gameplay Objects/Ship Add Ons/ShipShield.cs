using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using _2DGameEngine.Physics_Components;
using _2DGameEngine.Physics_Components.Colliders;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.Gameplay_Objects
{
    public class ShipShield : ShipAddOn
    {
        #region Properties and Fields

        public ShipShieldData ShipShieldData { get; private set; }
        public float CurrentShieldStrength { get; set; }
        private Bar ShieldStrengthBar { get; set; }

        private const float defaultOpacity = 0.4f;
        private const float onHitOpacity = 1f;

        private float timeSinceDamaged = 0;
        private float rechargeTimer = 0;

        #endregion

        public ShipShield(Vector2 hardPointOffset, string dataAsset, Ship parent, bool addRigidBody = true)
            : base(hardPointOffset, dataAsset, parent, addRigidBody)
        {

        }

        #region Methods

        private void RegenerateShields(GameTime gameTime)
        {
            timeSinceDamaged += (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;
            if (timeSinceDamaged >= ShipShieldData.ShieldDamagedRechargeDelay)
            {
                rechargeTimer += (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;
                if (rechargeTimer >= 1 && CurrentShieldStrength != ShipShieldData.ShieldStrength)
                {
                    CurrentShieldStrength = MathHelper.Clamp(CurrentShieldStrength + ShipShieldData.ShieldRechargePerSecond, 0, ShipShieldData.ShieldStrength);

                    HealthBar.LocalPosition = new Vector2(-HealthBar.Size.X * 0.5f, -Size.Y * 0.5f - 3);
                    rechargeTimer = 0;
                }
            }
        }

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            ShipShieldData = AssetManager.GetData<ShipShieldData>(DataAsset);
            CurrentShieldStrength = ShipShieldData.ShieldStrength;
            Size = new Vector2(ShipShieldData.ShieldRange, ShipShieldData.ShieldRange);
            Colour = new Color(ShipShieldData.Colour);
            Opacity = defaultOpacity;
        }

        public override void Initialize()
        {
            base.Initialize();

            // Add it here so that if we are altering the size of the sprite, we will add the bar at an appropriate place
            ShieldStrengthBar = new Bar(Vector2.Zero, new Vector2(35, 5), "Sprites\\UI\\Bars\\ShieldBar", ShipShieldData.ShieldStrength, "", this);
            ShieldStrengthBar.LoadContent();
            ShieldStrengthBar.Initialize();
        }

        public override void AddCollider()
        {
            // Radius of the collider is half the total range of the shield
            Collider = new CircleCollider(this, ShipShieldData.ShieldRange * 0.5f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            RegenerateShields(gameTime);

            ShieldStrengthBar.UpdateValue(CurrentShieldStrength);
            Opacity = MathHelper.Lerp(Opacity, defaultOpacity, (float)gameTime.ElapsedGameTime.Milliseconds / 250f);
        }

        public override void Damage(float damage)
        {
            if (CurrentShieldStrength > damage)
            {
                CurrentShieldStrength -= damage;
            }
            else
            {
                // If shield health is less than the damage, we apply the difference to the shield itself and then set the shield health to zero
                base.Damage(damage - CurrentShieldStrength);

                CurrentShieldStrength = 0;

                HealthBar.LocalPosition = Vector2.Zero;
            }

            Opacity = onHitOpacity;
            timeSinceDamaged = 0;
            rechargeTimer = 0;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (CurrentShieldStrength > 0)
            {
                base.Draw(spriteBatch);
            }
        }

        public override void DrawInGameUI(SpriteBatch spriteBatch)
        {
            base.DrawInGameUI(spriteBatch);

            ShieldStrengthBar.Draw(spriteBatch);
        }

        #endregion
    }
}
