using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Cutscenes.Level_2;
using UnderSiege.Gameplay_Objects;
using UnderSiege.Player_Data;
using UnderSiege.UI.HUD_Menus;

namespace UnderSiege.Screens.Level_Screens
{
    public class UnderSiegeGameplayScreenLevel2 : UnderSiegeGameplayScreen
    {
        #region Properties and Fields

        public static Vector2 destPosition = new Vector2(4000, -800);

        #endregion

        public UnderSiegeGameplayScreenLevel2(ScreenManager screenManager)
            : base(screenManager, "Data\\Screens\\LevelScreens\\Level2")
        {
            Session.Money = 2000;
            WaveManager.Paused = true;
            WaveManager.Continuous = true;

            HUD.DisableAndHideUIObject("Buy Engines UI");
            HUD.DisableAndHideUIObject("Buy Ships Button");
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadContent(ContentManager content)
        {
            GameObject hyperspaceGate = new GameObject(destPosition, "Sprites\\GameObjects\\SpaceStations\\hyperspace_gate", null, false);
            AddGameObject(hyperspaceGate, "Hyperspace Gate");

            base.LoadContent(content);
        }

        public override void AddScripts()
        {
            base.AddScripts();

            AddDialogBoxScript.defaultPosition = new Vector2(Viewport.Width * 0.75f, Viewport.Height * 0.75f);

            AddScript(new AddCutsceneScript(new Level2StartCutScene(ScreenManager, "Data\\Screens\\LevelScreens\\Level2", this), this));

            AddScript(new RunEventScript(OnAllEnginesBought));
            AddScript(new AddDialogBoxScript("All necessary engines have been constructed.", true, true));
            AddScript(new AddDialogBoxScript("The navigation marker on the overlay should\ndirect us to the nearest UNI exit gate.", true, true));
            AddScript(new RunEventScript(AddDestinationMarker));
            AddScript(new AddDialogBoxScript("Direct control of the ship is achievable\nthrough use of the W, A, S, D\nkeys on the interface input.", true, true));

            TransitionToScreenScript<UnderSiegeGameOverScreen<UnderSiegeGameplayScreenLevel1>> onCommandShipDeath = new TransitionToScreenScript<UnderSiegeGameOverScreen<UnderSiegeGameplayScreenLevel1>>(this);
            onCommandShipDeath.CanRunEvent += checkCommandShipDead;
            AddScript(onCommandShipDeath);

            AddCutsceneScript endCutScene = new AddCutsceneScript(new Level2EndCutScene(ScreenManager, "Data\\Screens\\LevelScreens\\Level2End", this), this);
            endCutScene.CanRunEvent += endOfLevelCutscene;
            AddScript(endCutScene);

            AddScript(new TransitionToScreenScript<UnderSiegeGameplayScreenLevel3>(this));
        }

        #endregion

        #region Events

        private void OnAllEnginesBought(object sender, EventArgs e)
        {
            PlayerShip playerShip = UnderSiegeGameplayScreen.Allies.GetItem<PlayerShip>("Command Ship");
            (sender as Script).Done = playerShip.ShipAddOns.ShipEngines.Count == 3;
        }

        private void AddDestinationMarker(object sender, EventArgs e)
        {
            Marker marker = new Marker(new Vector2(Viewport.Width * 0.9f, ScreenCentre.Y), UnderSiegeGameplayScreenLevel2.destPosition, "Sprites\\UI\\Markers\\DirectionMarker");
            HUD.AddUIObject(marker, "Direction Marker");

            InGameImage destMarker = new InGameImage(UnderSiegeGameplayScreenLevel2.destPosition, "Sprites\\UI\\InGameUI\\AttackMarker");
            AddInGameUIObject(destMarker, "Target Marker");

            (sender as Script).Done = true;

            // Set the camera to now follow the ship
            /*ScreenManager.Camera.FocusedObject = UnderSiegeGameplayScreen.Allies.GetItem<PlayerShip>("Command Ship");
            ScreenManager.Camera.ToggleCameraMode();*/
        }

        private void checkCommandShipDead(object sender, EventArgs e)
        {
            (sender as Script).CanRun = CommandShip.Alive == false;
        }

        private void endOfLevelCutscene(object sender, EventArgs e)
        {
            (sender as Script).CanRun = (UnderSiegeGameplayScreen.CommandShip.WorldPosition - destPosition).LengthSquared() <= 100 * 100;
        }

        #endregion
    }
}
