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
using UnderSiege.Screens;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.UI
{
    public class BuyShipObjectUIMenu<T, K> : UIObject where T : ShipAddOn where K : ShipAddOnData
    {
        #region Properties and Fields

        protected HUD HUD { get; set; }
        private Button ToggleButton { get; set; }
        private Menu ItemMenu { get; set; }

        protected float previousCameraZoom;

        #endregion

        public BuyShipObjectUIMenu(Vector2 position, Vector2 size, HUD hud, string itemType, string dataAsset = "")
            : base(position, size, dataAsset, hud)
        {
            HUD = hud;
            AddUI(itemType);
        }

        #region Methods

        protected void AddUI(string itemType)
        {
            // Add Buy Button
            // Populate with all elements with data type T
            // Add events from UnderSiegeGameplay Screen to here - like show hardpoint and reset UI
            // We can handle on the turning on and off here rather than in the gameplay screen
            ToggleButton = new Button(new Vector2(0, Size.Y * 0.5f - Button.defaultTexture.Height * 0.5f), itemType, this);
            ToggleButton.OnSelect += ToggleItemsVisibility;

            ItemMenu = new Menu(new Vector2(0, -Button.defaultTexture.Height), new Vector2(Size.X, Size.Y - Button.defaultTexture.Height), 0, 0, 0, 0, "", this);
            ItemMenu.Opacity = 0.35f;

            int counter = 0;
            int padding = 10;
            foreach (K data in AssetManager.GetAllData<K>())
            {
                int row = 1 + (counter / 5);
                int column = 1 + counter % 5;
                Image objectImage = new Image(new Vector2(-Size.X * 0.5f + column * (HardPointUI.HardPointDimension + padding), ItemMenu.Size.Y * 0.5f - row * (HardPointUI.HardPointDimension + padding)), new Vector2(HardPointUI.HardPointDimension, HardPointUI.HardPointDimension), data.TextureAsset, ItemMenu);
                objectImage.StoredObject = data;
                objectImage.OnSelect += ShowHardPointsEvent;
                objectImage.WhileSelected += CheckForPlaceObjectEvent;

                if (data.AddOnType == "ShipShield")
                {
                    ShipShieldData shieldData = data as ShipShieldData;
                    objectImage.Colour = new Color(shieldData.Colour);
                }

                ItemMenu.AddUIObject(objectImage, "Buy " + data.DisplayName + " Image");

                counter++;
            }

            ItemMenu.Visible = false;
            ItemMenu.Active = false;
        }

        protected virtual void ResetPurchaseObjectUI()
        {
            foreach (PlayerShip ship in UnderSiegeGameplayScreen.Allies.Values)
            {
                ship.DrawOtherHardPoints = false;
            }

            HUD.GameplayScreen.RemoveInGameUIObject("Purchase Object UI");
            Camera.Zoom = previousCameraZoom;
        }

        #endregion

        #region Events

        private void ToggleItemsVisibility(object sender, EventArgs e)
        {
            // Toggle the active and visible state of the menu
            ItemMenu.Visible = !ItemMenu.Visible;
            ItemMenu.Active = !ItemMenu.Active;
        }

        protected virtual void ShowHardPointsEvent(object sender, EventArgs e)
        {
            Image objectImage = sender as Image;
            if (objectImage != null)
            {
                foreach (PlayerShip ally in UnderSiegeGameplayScreen.Allies.Values)
                    ally.DrawOtherHardPoints = true;

                ShipAddOnData dataOfObject = objectImage.StoredObject as ShipAddOnData;
                if (dataOfObject != null)
                {
                    // This is going to need to be generalised - how are we going to store/get the ShipAddOn type name?
                    PurchaseItemUI purchaseShipObjectUI = new PurchaseItemUI(objectImage.Texture, dataOfObject, dataOfObject.AddOnType);
                    if (dataOfObject.AddOnType == "ShipShield")
                    {
                        ShipShieldData shieldData = dataOfObject as ShipShieldData;
                        purchaseShipObjectUI.Colour = new Color(shieldData.Colour);
                        purchaseShipObjectUI.Size = new Vector2(shieldData.ShieldRange, shieldData.ShieldRange);
                    }

                    HUD.GameplayScreen.AddInGameUIObject(purchaseShipObjectUI, "Purchase Object UI");
                    purchaseShipObjectUI.Initialize();

                    previousCameraZoom = Camera.Zoom;
                    Camera.Zoom = 1;
                }
            }
        }

        protected virtual void CheckForPlaceObjectEvent(object sender, EventArgs e)
        {
            PurchaseItemUI shipObjectToPurchase = HUD.GameplayScreen.InGameUIManager.GetItem<PurchaseItemUI>("Purchase Object UI");
            if (shipObjectToPurchase != null)
            {
                // This is going to need optimising
                foreach (PlayerShip ally in UnderSiegeGameplayScreen.Allies.Values)
                {
                    if (ally.Collider.CheckCollisionWith(GameMouse.InGamePosition))
                    {
                        // We have clicked inside a ship
                        foreach (HardPointUI hardPoint in ally.HardPointUI.OtherHardPointUI)
                        {
                            if (hardPoint.Collider.CheckCollisionWith(GameMouse.InGamePosition))
                            {
                                shipObjectToPurchase.LockedToPosition = true;
                                shipObjectToPurchase.LocalPosition = hardPoint.WorldPosition;
                                if (GameMouse.IsLeftClicked)
                                {
                                    ShipAddOnData addOnData = shipObjectToPurchase.DataAssetOfObject as ShipAddOnData;
                                    UnderSiegeGameplayScreen.Session.Money -= addOnData.Price;
                                    switch (shipObjectToPurchase.StoredObjectTypeName)
                                    {
                                        // We have clicked on a hardpoint - add the object
                                        case "ShipTurret":
                                            ally.AddTurret(hardPoint.HardPoint, addOnData);
                                            break;
                                        case "ShipShield":
                                            ally.AddShield(hardPoint.HardPoint, addOnData);
                                            break;
                                    }

                                    ResetPurchaseObjectUI();
                                }

                                return;
                            }
                        }

                        shipObjectToPurchase.LockedToPosition = false;
                        if (GameMouse.IsLeftClicked)
                        {
                            ResetPurchaseObjectUI();
                        }

                        // We have found out where the mouse is so we can exit the loop
                        return;
                    }
                }

                shipObjectToPurchase.LockedToPosition = false;
            }

            // Mouse isn't over a ship so we've clicked elsewhere so reset UI
            if (GameMouse.IsLeftClicked)
            {
                // Ideally don't want to reset the UI if we click on a ship, but not on a hardpoint.  However, this is proving tricky as when we click on the ship it stops calling this function.  Maybe not being selected or something...?
                ResetPurchaseObjectUI();
            }
        }

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

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
            base.Draw(spriteBatch);

            if (Visible)
            {
                ToggleButton.Draw(spriteBatch);
                ItemMenu.Draw(spriteBatch);
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
