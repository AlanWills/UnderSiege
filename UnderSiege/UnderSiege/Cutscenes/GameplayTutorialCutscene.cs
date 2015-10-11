using _2DGameEngine.Cutscenes;
using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;
using UnderSiege.Screens;
using UnderSiege.UI;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.Cutscenes
{
    public class GameplayTutorialCutscene : Cutscene
    {
        #region Properties and Fields

        private UnderSiegeGameplayScreen GameplayScreen { get; set; }

        #endregion

        public GameplayTutorialCutscene(ScreenManager screenManager, UnderSiegeGameplayScreen gameplayScreen, string dataAsset)
            : base(screenManager, dataAsset)
        {
            GameplayScreen = gameplayScreen;
            Camera.Position = new Vector2(ScreenCentre.X * 0.5f, -1000);
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Initialize()
        {
            base.Initialize();
            
            AddDialogBoxScript welcomeDialogBox = new AddDialogBoxScript("Welcome To Under Siege", ScreenCentre);
            AddScript(welcomeDialogBox);

            AddDialogBoxScript nextDialogBox = new AddDialogBoxScript("As Commander for this sector's defence systems,\nyou must prevent ANY enemy incursion into civilian space.", ScreenCentre);
            AddScript(nextDialogBox, welcomeDialogBox);

            MoveCameraScript moveCameraToRightOfStation = new MoveCameraScript(UnderSiegeGameplayScreen.Allies.GetItem("Space Station").WorldPosition + new Vector2(ScreenCentre.X * 0.5f, 0), MoveCameraStyle.LERP);
            AddScript(moveCameraToRightOfStation, nextDialogBox);

            AddDialogBoxScript stationDialogBox = new AddDialogBoxScript("This is your command station.", new Vector2(Viewport.Width * 0.75f, ScreenCentre.Y));
            AddScript(stationDialogBox, moveCameraToRightOfStation);

            AddDialogBoxScript stationDamageDialogBox = new AddDialogBoxScript("Protect it at ALL costs.\n\nIf it is destroyed, you will no longer\nbe able to coordinate your defence.", new Vector2(Viewport.Width * 0.75f, ScreenCentre.Y));
            AddScript(stationDamageDialogBox, stationDialogBox);

            MoveCameraScript moveCameraToLeftStation = new MoveCameraScript(new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y), MoveCameraStyle.LERP);
            AddScript(moveCameraToLeftStation, stationDamageDialogBox);

            AddDialogBoxScript turretsButtonDialogBox = new AddDialogBoxScript("Press the turrets button.", new Vector2(Viewport.Width * 0.25f, Viewport.Height * 0.75f));
            AddScript(turretsButtonDialogBox, moveCameraToLeftStation);

            ChangeObjectSelectionActivationAndVisiblityScript makeTurretButtonVisible = new ChangeObjectSelectionActivationAndVisiblityScript(UnderSiegeGameplayScreen.HUD.GetUIObject("Buy Turrets UI"), false, true, true);
            AddScript(makeTurretButtonVisible, moveCameraToLeftStation);

            // Now make a dialog box script that will show dialog when the turrets button has been pressed - can determine this by whether the ItemMenu is visible or not.
            AddDialogBoxScript explainTurretsDialogBox = new AddDialogBoxScript("From this menu, you can buy turrets to install on your station.\nTurrets will shoot at enemies and help keep you alive.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y));
            explainTurretsDialogBox.CanRunEvent += explainTurretsDialogBox_CanRunEvent;
            AddScript(explainTurretsDialogBox, makeTurretButtonVisible);
        }

        void explainTurretsDialogBox_CanRunEvent(object sender, EventArgs e)
        {
            AddDialogBoxScript script = sender as AddDialogBoxScript;
            BuyShipObjectUIMenu<ShipTurret, ShipTurretData> buyTurretsMenu = UnderSiegeGameplayScreen.HUD.GetUIObject("Buy Turrets UI") as BuyShipObjectUIMenu<ShipTurret, ShipTurretData>;
            script.CanRun = buyTurretsMenu.ItemMenu.Visible;
        }

        protected override void CheckIsDone()
        {
            IsDone = ScriptManager.NoMoreScripts;
        }

        #endregion
    }
}
