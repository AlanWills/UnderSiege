using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.Maths;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Abilities.Object_Abilities;
using UnderSiege.Gameplay_Objects.Ship_Add_Ons;
using UnderSiege.Player_Data;
using UnderSiege.Screens;
using UnderSiege.UI;
using UnderSiege.UI.In_Game_UI;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.Gameplay_Objects
{
    public class ShipAddOn : DamageableGameObject
    {
        #region Properties and Fields

        public ShipAddOnData ShipAddOnData { get; set; }

        public Ship ParentShip { get; set; }
        public Vector2 HardPointOffset { get; private set; }

        // Our object has a rotation - but that determines the actual turret sprite's rotation
        // We also need to save the direction the turret firing arc is facing - this cannot move freely
        // This orientation is a local rotation offset from the parent ship's rotation
        public float LocalOrientation
        {
            get;
            set;
        }

        protected Bar HealthBar { get; set; }
        public List<AddOnAbility> Abilities
        {
            get;
            private set;
        }

        private ShipAddOnAbilityMenu AbilityUI { get; set; }
        private static int abilityUITagID = 0;

        #endregion

        public ShipAddOn(Vector2 hardPointOffset, string dataAsset, Ship parent, bool addRigidBody = true)
            : base(hardPointOffset, dataAsset, parent, addRigidBody)
        {
            ParentShip = parent;
            HardPointOffset = hardPointOffset;
            Abilities = new List<AddOnAbility>();
        }

        #region Methods

        private void SetUpAbilities()
        {
            foreach (string abilityString in ShipAddOnData.Abilities)
            {
                AddOnAbility ability = null;

                switch (abilityString)
                {
                    case "Repair":
                        ability = new RepairAbility("Data\\Abilities\\ShipAddOnAbilities\\Repair", this);
                        break;

                    case "Sell":
                        ability = new SellAbility("Data\\Abilities\\ShipAddOnAbilities\\Sell", this);
                        break;

                    case "Recharge":
                        ability = new RechargeAbility("Data\\Abilities\\ShipAddOnAbilities\\Recharge", (this as ShipShield));
                        break;

                    case "Auto Reloader":
                        ability = new AutoReloaderAbility("Data\\Abilities\\ShipAddOnAbilities\\AutoReloader", (this as ShipKineticTurret));
                        break;
                }

                ability.LoadContent();
                Abilities.Add(ability);
            }
        }

        #endregion

        #region Events

        protected override void IfSelected()
        {
            base.IfSelected();

            if (ShipAddOnData.Orientable && InputHandler.KeyDown(Keys.Space))
            {
                Orient();
            }
        }

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            ShipAddOnData = AssetManager.GetData<ShipAddOnData>(DataAsset);
            CurrentHealth = ShipAddOnData.Health;

            if (ParentShip.ShipType == ShipType.AlliedShip)
            {
                SetUpAbilities();

                AbilityUI = new ShipAddOnAbilityMenu(Vector2.Zero, this);
                AbilityUI.Active = false;
                AbilityUI.Visible = false;
                AbilityUI.LoadContent();
                AbilityUI.Initialize();
                UnderSiegeGameplayScreen.HUD.AddUIObject(AbilityUI, ShipAddOnData.DisplayName + " Ability UI " + abilityUITagID, false);
                abilityUITagID++;
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            // Add it here so that if we are altering the size of the sprite, we will add the bar at an appropriate place
            HealthBar = new Bar(new Vector2(0, -Size.Y * 0.5f - 3), new Vector2(20, 3), "Sprites\\UI\\Bars\\AddOnHullBar", ShipAddOnData.Health, "", this);
            HealthBar.Visible = false;
            HealthBar.LoadContent();
            HealthBar.Initialize();
        }

        public virtual void Orient()
        {
            // Here we are changing the orientation of the turret, so it should not fire
            LocalOrientation = Trigonometry.GetAngleOfLineBetweenObjectAndTarget(this, ScreenManager.GameMouse.InGamePosition) - Parent.WorldRotation;
            LocalRotation = LocalOrientation;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Active)
            {
                HealthBar.Update(gameTime);
                HealthBar.UpdateValue(CurrentHealth);
                HealthBar.Visible = MouseOver || IsSelected;

                foreach (AddOnAbility ability in Abilities)
                {
                    ability.Update(gameTime);
                }
            }
        }

        public override void HandleInput()
        {
            if (Active)
            {
                // This rectangle is used so that with something like the shield we do not select it if we select the shield collider
                // Rather we have to select the hardpoint region
                Rectangle hardPointRectangle = new Rectangle((int)(WorldPosition.X - HardPointUI.HardPointDimension * 0.5f), (int)(WorldPosition.Y - HardPointUI.HardPointDimension * 0.5f), (int)(HardPointUI.HardPointDimension), (int)(HardPointUI.HardPointDimension));
                MouseOver = _2DGeometry.RectangleContainsPoint(hardPointRectangle, ScreenManager.GameMouse.InGamePosition);

                // If mouse isn't clicked we don't need to change the selection state, as we haven't selected anything!
                if (ScreenManager.GameMouse.IsLeftClicked)
                {
                    // We have clicked on the object
                    if (MouseOver)
                    {
                        // If something is selected, we might be building UI or something, so for optimisation don't allow whatever we do on selecting to be done if already selected
                        if (IsSelected)
                            return;

                        // The object wasn't selected, so select it
                        if (clickResetTime >= TimeSpan.FromSeconds(0.2f))
                            Select();

                        return;
                    }
                    // We have clicked elsewhere so should clear selection
                    else
                    {
                        // If something is not selected, we might be building UI or something, so for optimisation don't allow whatever we do on de-selecting to be done if already de-selected
                        if (!IsSelected)
                            return;

                        DeSelect();
                    }
                }
                else if (ScreenManager.GameMouse.IsRightClicked)
                {
                    if (MouseOver)
                    {
                        if (AbilityUI != null)
                        {
                            AbilityUI.Active = true;
                            AbilityUI.Visible = true;
                        }
                    }
                    else
                    {
                        if (AbilityUI != null)
                        {
                            AbilityUI.Active = false;
                            AbilityUI.Visible = false;
                        }
                    }
                }
            }
        }

        public override void DrawInGameUI(SpriteBatch spriteBatch)
        {
            base.DrawInGameUI(spriteBatch);

            if (Visible)
            {
                HealthBar.Draw(spriteBatch);
            }
        }

        #endregion
    }
}
