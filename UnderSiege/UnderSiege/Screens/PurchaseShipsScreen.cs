using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.UI;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.Screens
{
    public class PurchaseShipsScreen : MenuScreen
    {
        #region Properties and Fields

        public UnderSiegeGameplayScreen GameplayScreen { get; private set; }

        #endregion

        public PurchaseShipsScreen(UnderSiegeGameplayScreen gameplayScreen, ScreenManager screenManager, string dataAsset = "Data\\Screens\\PurchaseShipsScreen")
            : base(screenManager, dataAsset)
        {
            GameplayScreen = gameplayScreen;
            AddUI();
        }

        #region Methods

        private void AddUI()
        {
            int counter = 0;
            int padding = 25;
            foreach (PlayerShipData shipData in AssetManager.GetAllData<PlayerShipData>())
            {
                // If we do not have enough money, do not show the UI
                if (UnderSiegeGameplayScreen.Session.Money < shipData.Price)
                {
                    continue;
                }

                int row = 1 + (counter / 5);
                int column = 1 + counter % 5;
                // Add these to a menu instead
                Image objectImage = new Image(new Vector2(padding + (padding + 100) * column, padding + (padding + 100) * row), shipData.TextureAsset);
                objectImage.StoredObject = shipData;
                objectImage.OnSelect += ShowShipInfo;

                AddUIObject(objectImage, "Buy " + shipData.DisplayName + " Image", false);

                counter++;
            }
        }

        #endregion

        #region Events

        private void ShowShipInfo(object sender, EventArgs e)
        {
            Image shipImage = sender as Image;
            PlayerShipData playerShipData = shipImage.StoredObject as PlayerShipData;

            PurchaseShipInfoUI currentShipInfo = UIManager.GetItem<PurchaseShipInfoUI>("Selected Ship Info");
            if (currentShipInfo == null)
            {
                AddUIObject(new PurchaseShipInfoUI(this, playerShipData, new Vector2(Viewport.Width - 150, ScreenCentre.Y), new Vector2(300, Viewport.Height)), "Selected Ship Info", true);
            }
            else
            {
                currentShipInfo.UpdateUI(playerShipData);
            }
        }

        #endregion

        #region Virtual Methods

        public override void HandleInput()
        {
            base.HandleInput();

            if (InputHandler.KeyPressed(Keys.Escape))
            {
                ScreenManager.RemoveScreen(this);
                GameplayScreen.Visible = true;
                GameplayScreen.Active = true;
            }
        }

        #endregion
    }
}
