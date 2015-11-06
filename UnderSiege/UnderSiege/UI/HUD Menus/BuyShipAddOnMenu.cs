using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.Maths;
using _2DGameEngine.Physics_Components.Colliders;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;
using UnderSiege.Gameplay_Objects.Ship_Add_Ons;
using UnderSiege.Player_Data;
using UnderSiege.Screens;
using UnderSiege.UI.In_Game_UI.Buy_Add_On_Info;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.UI.HUD_Menus
{
    public abstract class BuyShipAddOnMenu : UIObject
    {
        #region Properties and Fields

        protected HUD HUD { get; set; }
        protected Button ToggleButton { get; set; }
        public Menu ItemMenu { get; protected set; }

        protected float previousCameraZoom = 1;
        protected const int padding = 10;
        protected const int columns = 3;

        #endregion

        public BuyShipAddOnMenu(Vector2 position, HUD hud, string dataAsset = "Sprites\\UI\\Menus\\default")
            : base(position, dataAsset, hud)
        {
            HUD = hud;
            AddUI();
        }

        #region Methods

        protected abstract void AddUI();

        protected abstract void CheckForPlaceObjectEvent(object sender, EventArgs e);

        protected virtual void ResetPlaceObjectUI()
        {
            HUD.GameplayScreen.RemoveInGameUIObject("Purchase Object UI");
            Camera.Zoom = previousCameraZoom;
        }

        #endregion

        #region Events

        protected void ToggleItemsVisibility(object sender, EventArgs e)
        {
            // Toggle the active and visible state of the menu
            IsSelected = !IsSelected;
            ItemMenu.Visible = !ItemMenu.Visible;
            ItemMenu.Active = !ItemMenu.Active;
        }

        protected abstract void ShowHardPointsEvent(object sender, EventArgs e);

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            ToggleButton.LoadContent();
            ItemMenu.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();

            ToggleButton.Initialize();
            ItemMenu.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Active)
            {
                ToggleButton.Update(gameTime);
                ItemMenu.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                ToggleButton.Draw(spriteBatch);
                ItemMenu.Draw(spriteBatch);
            }
        }

        public override void DrawInGameUI(SpriteBatch spriteBatch)
        {
            base.DrawInGameUI(spriteBatch);

            if (Visible)
            {
                ToggleButton.DrawInGameUI(spriteBatch);
                ItemMenu.DrawInGameUI(spriteBatch);
            }
        }

        public override void DrawScreenUI(SpriteBatch spriteBatch)
        {
            base.DrawScreenUI(spriteBatch);

            if (Visible)
            {
                ToggleButton.DrawScreenUI(spriteBatch);
                ItemMenu.DrawScreenUI(spriteBatch);
            }
        }

        public override void HandleInput()
        {
            if (Active)
            {
                ToggleButton.HandleInput();
                ItemMenu.HandleInput();
            }
        }

        #endregion
    }
}
