using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;
using UnderSiege.Player_Data;
using UnderSiege.Screens;
using UnderSiege.UI.In_Game_UI.Buy_Add_On_Info;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.UI.HUD_Menus
{
    public class BuyShipTurretMenu : BuyShipAddOnMenu
    {
        #region Properties and Fields

        #endregion

        public BuyShipTurretMenu(Vector2 position, HUD hud, string dataAsset = "")
            : base(position, hud, dataAsset)
        {
            
        }

        #region Methods

        #endregion

        #region Virtual Methods

        protected override void AddUI()
        {
            // Add Buy Button
            // Populate with all elements with data type T
            // Add events from UnderSiegeGameplay Screen to here - like show hardpoint and reset UI
            // We can handle on the turning on and off here rather than in the gameplay screen
            ToggleButton = new Button(new Vector2(0, Button.defaultTexture.Height * 0.5f), "Turrets", this);
            ToggleButton.OnSelect += ToggleItemsVisibility;

            // Set the size of the menu based on the number of objects which have to fill it
            List<ShipTurretData> allData = AssetManager.GetAllData<ShipTurretData>();
            int totalObjects = allData.Count;
            int totalRows = (int)Math.Ceiling((float)totalObjects / (float)columns);
            Vector2 itemMenuSize = new Vector2(columns * (HardPointUI.HardPointDimension + padding) + padding, totalRows * (HardPointUI.HardPointDimension + padding) + padding);

            ItemMenu = new Menu(new Vector2(0, -itemMenuSize.Y * 0.5f), itemMenuSize, 0, 0, 0, 0, "", this);
            ItemMenu.Opacity = 1;

            int counter = 0;
            foreach (ShipTurretData data in allData)
            {
                int row = (counter / columns);
                int column = counter % columns;
                // The 0.5f in the X is to pad the object correctly along the x axis
                Image objectImage = new Image(new Vector2(-ItemMenu.Size.X * 0.5f + (column + 0.5f) * (HardPointUI.HardPointDimension + padding) + 0.5f * padding, -ItemMenu.Size.Y * 0.5f + (row + 0.5f) * (HardPointUI.HardPointDimension + padding) + 0.5f * padding), new Vector2(HardPointUI.HardPointDimension, HardPointUI.HardPointDimension), data.TextureAsset, ItemMenu);
                BuyShipTurretHoverInfo hoverUI = new BuyShipTurretHoverInfo(data, new Vector2(0, -objectImage.Size.Y * 0.5f - 2 * padding), objectImage);

                hoverUI.LoadContent();
                hoverUI.Initialize();

                objectImage.ScreenHoverUI = hoverUI;
                objectImage.StoredObject = data;
                objectImage.OnSelect += ShowHardPointsEvent;

                ItemMenu.AddUIObject(objectImage, "Buy " + data.DisplayName + " Image");

                counter++;
            }

            ItemMenu.Visible = false;
            ItemMenu.Active = false;
        }

        protected override void ShowHardPointsEvent(object sender, EventArgs e)
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
                    PurchaseItemUI purchaseShipObjectUI = new PurchaseItemUI(objectImage.Texture, dataOfObject, dataOfObject.AddOnType, ScreenManager.GameMouse.InGameMouse);
                    purchaseShipObjectUI.OnSelect += CheckForPlaceObjectEvent;

                    HUD.GameplayScreen.AddInGameUIObject(purchaseShipObjectUI, "Purchase Object UI");
                    purchaseShipObjectUI.Initialize();

                    previousCameraZoom = Camera.Zoom;
                    Camera.Zoom = 1;
                }
            }

            ScreenManager.GameMouse.Flush();
        }

        // Bit of a hack, but basically when we place the purchase object UI, the engine perceives us as "selecting" it so we just run this function when selected
        protected override void CheckForPlaceObjectEvent(object sender, EventArgs e)
        {
            PurchaseItemUI shipObjectToPurchase = sender as PurchaseItemUI;
            if (shipObjectToPurchase != null)
            {
                // This is going to need optimising
                foreach (PlayerShip ally in UnderSiegeGameplayScreen.Allies.Values)
                {
                    if (ally.Collider.CheckCollisionWith(ScreenManager.GameMouse.InGamePosition))
                    {
                        // We have clicked inside a ship
                        foreach (HardPointUI hardPoint in ally.HardPointUI.OtherHardPointUI)
                        {
                            if (hardPoint.Collider.CheckCollisionWith(ScreenManager.GameMouse.InGamePosition))
                            {
                                if (ScreenManager.GameMouse.IsLeftClicked)
                                {
                                    ShipAddOnData addOnData = shipObjectToPurchase.DataAssetOfObject as ShipAddOnData;
                                    Session.Money -= addOnData.Price;
                                    ally.AddTurret(hardPoint.HardPoint, addOnData);
                                }

                                return;
                            }
                        }

                        // Removes the purchase item UI from the HUD
                        ResetPlaceObjectUI();

                        // We have found out which ship the mouse is over, so we can exit the loop
                        return;
                    }
                }
            }

            // Removes the purchase item UI from the HUD
            ResetPlaceObjectUI();
        }

        protected override void ResetPlaceObjectUI()
        {
            base.ResetPlaceObjectUI();

            foreach (PlayerShip ship in UnderSiegeGameplayScreen.Allies.Values)
            {
                ship.DrawOtherHardPoints = false;
            }
        }

        #endregion
    }
}
