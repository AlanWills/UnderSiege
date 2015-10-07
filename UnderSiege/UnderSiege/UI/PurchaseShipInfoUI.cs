using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;
using UnderSiege.Screens;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.UI
{
    public class PurchaseShipInfoUI : Menu
    {
        #region Properties and Fields

        private PlayerShipData PlayerShipData { get; set; }
        private PurchaseShipsScreen PurchaseShipsScreen { get; set; }
        private HardPointUIManager HardPointUI { get; set; }

        #endregion

        public PurchaseShipInfoUI(PurchaseShipsScreen purchaseShipsScreen, PlayerShipData playerShipData, Vector2 position, Vector2 size, string dataAsset = "Sprites\\UI\\Menus\\default", BaseObject parent = null)
            : base(position, size, 0, 0, 0, 0, dataAsset, parent)
        {
            PurchaseShipsScreen = purchaseShipsScreen;
            PlayerShipData = playerShipData;
            Opacity = 0.5f;
        }

        #region Methods

        private void AddUI()
        {
            float padding = 5;

            UIManager.Clear();

            Image shipImage = new Image(new Vector2(0, -Size.Y * 0.25f), PlayerShipData.TextureAsset, this);
            AddUIObject(shipImage, "Ship Image", true);

            HardPointUI = new HardPointUIManager(shipImage);
            HardPointUI.Initialize(PlayerShipData);
            HardPointUI.DrawEngineHardPointUI = true;
            HardPointUI.DrawOtherHardPointUI = true;

            Label shipName = new Label("Name: " + PlayerShipData.DisplayName, new Vector2(0, shipImage.Size.Y * 0.5f + SpriteFont.LineSpacing + padding), Color.White, shipImage);
            AddUIObject(shipName, "Ship Name", true);

            Label shipHull = new Label("Hull Strength: " + PlayerShipData.HullHealth.ToString(), new Vector2(0, SpriteFont.LineSpacing + padding), Color.White, shipName);
            AddUIObject(shipHull, "Ship Hull Health", true);

            IconAndLabel money = new IconAndLabel("Sprites\\UI\\Icons\\MoneyIcon", PlayerShipData.Price.ToString(), new Vector2(0, SpriteFont.LineSpacing + padding), shipHull);
            AddUIObject(money, "Ship Price", true);

            Button buyButton = new Button(new Vector2(0, SpriteFont.LineSpacing + Button.defaultTexture.Height * 0.5f + padding), "Buy", money);
            buyButton.OnSelect += BuyShipEvent;
            AddUIObject(buyButton, "Buy Ship Button", true);
        }

        public void UpdateUI(PlayerShipData playerShipData)
        {
            // Change this later to merely change the data rather than destroying and creating new data
            if (playerShipData != PlayerShipData)
            {
                PlayerShipData = playerShipData;
                AddUI();
            }
        }

        #endregion

        #region Events

        private void BuyShipEvent(object sender, EventArgs e)
        {
            Texture2D shipTexture = UIManager.GetItem<Image>("Ship Image").Texture;

            PurchaseShipsScreen.GameplayScreen.Show();

            PurchaseItemUI purchaseShipUI = new PurchaseItemUI(shipTexture, PlayerShipData, "Ship");
            purchaseShipUI.MouseOverObject += CheckForShipPlacementEvent;
            PurchaseShipsScreen.GameplayScreen.AddInGameUIObject(purchaseShipUI, "Purchase Ship UI", true);

            PurchaseShipsScreen.GameplayScreen.ScreenManager.RemoveScreen(PurchaseShipsScreen);
        }

        private void CheckForShipPlacementEvent(object sender, EventArgs e)
        {
            PurchaseItemUI purchaseShipUI = sender as PurchaseItemUI;
            if (purchaseShipUI != null)
            {
                if (GameMouse.IsLeftClicked)
                {
                    PurchaseShipsScreen.GameplayScreen.AddAlliedShip(new PlayerShip(GameMouse.LastLeftClickedPosition, AssetManager.GetKeyFromData(purchaseShipUI.DataAssetOfObject), UnderSiegeGameplayScreen.SceneRoot), "Ally " + UnderSiegeGameplayScreen.Allies.Values.Count + 1, true);
                    UnderSiegeGameplayScreen.Session.Money -= (purchaseShipUI.DataAssetOfObject as PlayerShipData).Price;
                    ResetPurchaseObjectUI();
                }
                else if (InputHandler.KeyPressed(Keys.Escape))
                {
                    ResetPurchaseObjectUI();
                }
            }
        }

        public void ResetPurchaseObjectUI()
        {
            PurchaseShipsScreen.GameplayScreen.RemoveInGameUIObject("Purchase Ship UI");
            PurchaseShipsScreen.GameplayScreen.Show();
        }

        #endregion

        #region Virtual Methods

        public override void Initialize()
        {
            base.Initialize();

            AddUI();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            HardPointUI.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            HardPointUI.Draw(spriteBatch);
        }

        #endregion
    }
}
