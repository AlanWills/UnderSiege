using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    public class HUD : UIObject
    {
        #region Properties and Fields

        public UnderSiegeGameplayScreen GameplayScreen { get; private set; }

        public BaseObjectManager<UIObject> UIManager { get; private set; }

        #endregion

        public HUD(UnderSiegeGameplayScreen gameplayScreen)
            : base("", null)
        {
            GameplayScreen = gameplayScreen;
            UIManager = new BaseObjectManager<UIObject>();

            SetUpUI();
        }

        #region Methods

        private void SetUpUI()
        {
            UIManager.AddObject(new BuyShipObjectUIMenu<ShipTurret, ShipTurretData>(new Vector2(150, ScreenManager.Viewport.Height - 100), new Vector2(200, 200), this, "Turrets"), "Buy Turrets UI", false);
            UIManager.AddObject(new BuyShipObjectUIMenu<ShipShield, ShipShieldData>(new Vector2(400, ScreenManager.Viewport.Height - 100), new Vector2(200, 200), this, "Shields"), "Buy Shields UI", false);
            UIManager.AddObject(new BuyShipEngineUIMenu(new Vector2(650, ScreenManager.Viewport.Height - 100), new Vector2(200, 200), this, "Engines"), "Buy Engines UI", false);
            UIManager.AddObject(new Label("Money: " + UnderSiegeGameplayScreen.Session.Money, new Vector2(ScreenManager.Viewport.Width - 100, ScreenManager.Viewport.Height - SpriteFont.LineSpacing), Color.White, this), "Money UI", false);

            Button buyShipsButton = new Button(new Vector2(900, ScreenManager.Viewport.Height - Button.defaultTexture.Height * 0.5f), "Ships", this);
            buyShipsButton.OnSelect += buyShipsButton_OnSelect;
            AddUIObject(buyShipsButton, "Buy Ships Button", false);
        }

        private void buyShipsButton_OnSelect(object sender, EventArgs e)
        {
            GameplayScreen.Hide();
            GameplayScreen.ScreenManager.AddScreen(new PurchaseShipsScreen(GameplayScreen, GameplayScreen.ScreenManager));
        }

        public void AddUIObject(UIObject uiObject, string tag, bool load = true, bool linkWithObject = true)
        {
            if (uiObject.Parent == null)
                uiObject.Parent = this;

            UIManager.AddObject(uiObject, tag, load, linkWithObject);
        }

        public UIObject GetUIObject(string tag)
        {
            return UIManager.GetItem(tag);
        }

        public T GetUIObject<T>(string tag) where T : UIObject
        {
            return UIManager.GetItem<T>(tag);
        }

        public void DisableAndHideUIObject(string tag)
        {
            UIObject uiobject = UIManager.GetItem(tag);
            uiobject.Active = false;
            uiobject.Visible = false;
        }

        public void ActivateAndShowUIObject(string tag)
        {
            UIObject uiobject = UIManager.GetItem(tag);
            uiobject.Active = true;
            uiobject.Visible = true;
        }

        public void DisableAndHideAll()
        {
            foreach (UIObject uiobject in UIManager.Values)
            {
                uiobject.Active = false;
                uiobject.Visible = false;
            }
        }

        public void ActivateAndShowAll()
        {
            foreach (UIObject uiobject in UIManager.Values)
            {
                uiobject.Active = true;
                uiobject.Visible = true;
            }
        }

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            UIManager.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();

            UIManager.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            UIManager.Update(gameTime);
            UIManager.GetItem<Label>("Money UI").Text = "Money: " + UnderSiegeGameplayScreen.Session.Money;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            UIManager.Draw(spriteBatch);
        }

        public override void HandleInput()
        {
            base.HandleInput();

            UIManager.HandleInput();
        }

        #endregion
    }
}
