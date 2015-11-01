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
using UnderSiege.Player_Data;
using UnderSiege.UI;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.Screens
{
    public class PurchaseShipsScreen : MenuScreen
    {
        #region Properties and Fields

        public UnderSiegeGameplayScreen GameplayScreen { get; private set; }

        // This DOES determine the number of columns
        private const int columns = 4;
        // This will not determine the max number of rows - the aim is to keep adding ships then move the camera to see the extra ones
        // This just merely determines the spacing
        private const int rows = 5;

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
            foreach (PlayerShipData shipData in AssetManager.GetAllData<PlayerShipData>())
            {
                if (shipData.CanBuy)
                {
                    float currRow = 1 + (counter / columns);
                    float currColumn = 0.5f + counter % columns;
                    // Add these to a menu instead
                    Image objectImage = new Image(new Vector2((Viewport.Width / columns) * currColumn, (Viewport.Height / rows) * currRow), shipData.TextureAsset);
                    objectImage.StoredObject = shipData;
                    objectImage.OnSelect += ShowShipInfo;

                    AddScreenUIObject(objectImage, "Buy " + shipData.DisplayName + " Image", false);

                    counter++;
                }
            }
        }

        #endregion

        #region Events

        private void ShowShipInfo(object sender, EventArgs e)
        {
            Image shipImage = sender as Image;
            PlayerShipData playerShipData = shipImage.StoredObject as PlayerShipData;

            PurchaseShipInfoUI currentShipInfo = UIManager.GetObject<PurchaseShipInfoUI>("Selected Ship Info");
            if (currentShipInfo == null)
            {
                AddScreenUIObject(new PurchaseShipInfoUI(this, playerShipData, new Vector2(Viewport.Width - 150, ScreenCentre.Y), new Vector2(300, Viewport.Height)), "Selected Ship Info", true);
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
                ScreenManager.Camera.CameraMode = CameraMode.Free;
            }
        }

        #endregion
    }
}
