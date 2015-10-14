using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;
using UnderSiege.Gameplay_Objects.Ship_Add_Ons;
using UnderSiege.Player_Data;
using UnderSiege.Screens;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.UI
{
    public class BuyShipEngineUIMenu : BuyShipObjectUIMenu<ShipEngine, ShipEngineData>
    {
        #region Properties and Fields

        public BuyShipEngineUIMenu(Vector2 position, HUD hud, string dataAsset = "")
            : base(position, hud, dataAsset)
        {
            
        }

        #endregion

        #region Methods

        #endregion

        #region Virtual Methods

        protected override void ResetPurchaseObjectUI()
        {
            foreach (PlayerShip ship in UnderSiegeGameplayScreen.Allies.Values)
            {
                ship.DrawEngineHardPoints = false;
            }

            HUD.GameplayScreen.RemoveInGameUIObject("Purchase Object UI");
            Camera.Zoom = previousCameraZoom;
        }

        protected override void ShowHardPointsEvent(object sender, EventArgs e)
        {
            Image objectImage = sender as Image;
            if (objectImage != null)
            {
                foreach (PlayerShip ally in UnderSiegeGameplayScreen.Allies.Values)
                    ally.DrawEngineHardPoints = true;

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

        protected override void CheckForPlaceObjectEvent(object sender, EventArgs e)
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
                        foreach (HardPointUI hardPoint in ally.HardPointUI.EngineHardPointUI)
                        {
                            if (hardPoint.Collider.CheckCollisionWith(GameMouse.InGamePosition))
                            {
                                shipObjectToPurchase.LockedToPosition = true;
                                shipObjectToPurchase.LocalPosition = hardPoint.WorldPosition;
                                if (GameMouse.IsLeftClicked)
                                {
                                    ShipAddOnData addOnData = shipObjectToPurchase.DataAssetOfObject as ShipAddOnData;
                                    Session.Money -= addOnData.Price;
                                    ally.AddEngine(hardPoint.HardPoint, addOnData);

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
    }
}
