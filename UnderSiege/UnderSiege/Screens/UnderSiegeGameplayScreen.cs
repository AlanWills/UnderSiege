using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.Maths;
using _2DGameEngine.Screens;
using _2DGameEngine.UI_Objects;
using _2DTowerDefenceEngine.Waves;
using _2DTowerDefenceLibraryData.Waves_Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;
using UnderSiege.Player_Data;
using UnderSiege.UI;
using UnderSiege.Waves;
using UnderSiegeData.Gameplay_Objects;
using UnderSiegeData.Screens;

namespace UnderSiege.Screens
{
    public class UnderSiegeGameplayScreen : GameplayScreen
    {
        #region Properties and Fields

        public UnderSiegeGameplayScreenData GameplayScreenData
        {
            get;
            private set;
        }

        /*public static Session Session
        {
            get;
            private set;
        }*/

        // Maybe use the Session list instead?  Want public set though
        public static DictionaryManager<PlayerShip> Allies
        {
            get;
            set;
        }

        public static DictionaryManager<EnemyShip> Enemies
        {
            get;
            private set;
        }

        public static PlayerShip CommandShip
        {
            get;
            private set;
        }

        public WaveManager<EnemyShip> WaveManager
        {
            get;
            private set;
        }

        public static HUD HUD
        {
            get;
            private set;
        }

        #endregion

        public UnderSiegeGameplayScreen(ScreenManager screenManager, string dataAsset)
            : base(screenManager, dataAsset)
        {
            //Session = new Session();

            Allies = new DictionaryManager<PlayerShip>();
            Enemies = new DictionaryManager<EnemyShip>();
            WaveManager = new WaveManager<EnemyShip>(this);

            HUD = new HUD(this);
            AddScreenUIObject(HUD, "HUD");
        }

        #region Methods

        public void AddAlliedShip(PlayerShip ally, string tag, bool load = false, bool linkWithGameObjectManager = true)
        {
            //ally.OnSelect += AddSelectedShipUI;
            //ally.OnDeselect += RemoveSelectedShipUI;
            AddGameObject(ally, tag, load, linkWithGameObjectManager);
            Allies.Add(tag, ally);
        }

        public void AddEnemyShip(GameObject enemy, string tag, bool load = false, bool linkWithGameObjectManager = true)
        {
            AddGameObject(enemy, tag, load, linkWithGameObjectManager);
            Enemies.Add(tag, enemy as EnemyShip);
        }

        #endregion

        #region Events

        private void AddSelectedShipUI(object sender, EventArgs e)
        {
            PlayerShip playerShip = sender as PlayerShip;

            FlashingInGameImage selectedShipUI = InGameUIManager.GetObject<FlashingInGameImage>("Selected Player Ship UI");
            if (selectedShipUI == null)
            {
                InGameUIManager.AddObject(new FlashingInGameImage(Vector2.Zero, playerShip.Size, "Sprites\\UI\\Markers\\SelectedShipMarker", playerShip), "Selected Player Ship UI", true);
            }
            else if (selectedShipUI.Parent != playerShip)
            {
                selectedShipUI.Parent = playerShip;
                selectedShipUI.Size = playerShip.Size;
                selectedShipUI.Visible = true;
            }

            InGameShipInfo inGameShipInfo = UIManager.GetObject<InGameShipInfo>("Selected Player Ship Info UI");
            if (inGameShipInfo == null)
            {
                InGameShipInfo shipInfo = new InGameShipInfo(playerShip, new Vector2(Viewport.Width - 150, ScreenCentre.Y), new Vector2(300, Viewport.Height), "Sprites\\UI\\Menus\\default", UnderSiegeGameplayScreen.SceneRoot);
                UIManager.AddObject(shipInfo, "Selected Player Ship Info UI", true);
            }
            else if (inGameShipInfo.PlayerShip != playerShip)
            {
                // This will automatically rebuild the UI - see the class
                inGameShipInfo.PlayerShip = playerShip;
            }
        }

        private void RemoveSelectedShipUI(object sender, EventArgs e)
        {
            PlayerShip playerShip = sender as PlayerShip;
            FlashingInGameImage selectedShipUI = InGameUIManager.GetObject<FlashingInGameImage>("Selected Player Ship UI");

            if (selectedShipUI != null && selectedShipUI.Parent == playerShip)
            {
                selectedShipUI.Visible = false;
                selectedShipUI.Parent = null;
            }

            InGameShipInfo inGameShipInfo = UIManager.GetObject<InGameShipInfo>("Selected Player Ship Info UI");
            if (inGameShipInfo != null && inGameShipInfo.PlayerShip == playerShip)
            {
                // This will automatically hide the UI - see the class
                inGameShipInfo.PlayerShip = null;
            }
        }

        #endregion

        #region Virtual Methods

        public override void LoadContent(ContentManager content)
        {
            // We have to do this first so that the ship is added straight away
            // Otherwise it is not added until after the first update loop has run
            GameplayScreenData = AssetManager.GetData<UnderSiegeGameplayScreenData>(DataAsset);
            CommandShip = new PlayerShip(ScreenManager.ScreenCentre, GameplayScreenData.CommandShipName, SceneRoot);
            AddAlliedShip(CommandShip, "Command Ship");

            base.LoadContent(content);

            WaveManager.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();

            WaveManager.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (ScriptManager.UpdateGame)
            {
                WaveManager.Update(gameTime);
            }
        }

        public override void AddPauseMenu()
        {
            AddMenuScript addPauseMenuScript = new AddMenuScript(new UnderSiegeGameplayScreenPauseMenu(this));
            addPauseMenuScript.CanRunEvent += addPauseMenuScript_CanRunEvent;
            AddScript(addPauseMenuScript);
        }

        void addPauseMenuScript_CanRunEvent(object sender, EventArgs e)
        {
            (sender as Script).CanRun = true;
        }

        #endregion
    }
}