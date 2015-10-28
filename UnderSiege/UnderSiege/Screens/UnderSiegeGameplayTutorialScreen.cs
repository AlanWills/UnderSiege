using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;
using UnderSiege.UI;

namespace UnderSiege.Screens
{
    public class UnderSiegeGameplayTutorialScreen : UnderSiegeGameplayScreen
    {
        #region Properties and Fields

        #endregion

        public UnderSiegeGameplayTutorialScreen(ScreenManager screenManager, string dataAsset = "Data\\Screens\\GameplayTutorialScreen")
            : base(screenManager, dataAsset)
        {
            WaveManager.Paused = true;
            ScreenManager.Camera.CameraMode = CameraMode.Fixed;
            Camera.Position = new Vector2(ScreenCentre.X * 0.5f, -1000);

            HUD.DisableAndHideAll();
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void AddScripts()
        {
            base.AddScripts();

            AddScript(new AddDialogBoxScript("Welcome, sir.", ScreenCentre));
            AddScript(new AddDialogBoxScript("I am Lieutenant Drake and am in charge of your briefing.", ScreenCentre));
            AddScript(new AddDialogBoxScript("As Commander for this sector's defence systems,\nyou must prevent ANY enemy incursion into civilian space.", ScreenCentre));
            AddScript(new AddDialogBoxScript("I am bringing your sector map online now.", ScreenCentre));
            AddScript(new AddDialogBoxScript("This is an interactive image of nearby space.", ScreenCentre));
            AddScript(new AddDialogBoxScript("Use it to coordinate your defences\nand issue orders to your troops.", ScreenCentre));

            AddScript(new MoveCameraScript(UnderSiegeGameplayScreen.Allies.GetItem("Command Station").WorldPosition + new Vector2(ScreenCentre.X * 0.5f, 0), MoveCameraStyle.LERP));
            AddScript(new AddDialogBoxScript("This is the command station\nwe are currently on.", new Vector2(Viewport.Width * 0.75f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("Protect it at ALL costs.\nDefeat here is not an option.", new Vector2(Viewport.Width * 0.75f, ScreenCentre.Y)));

            AddScript(new MoveCameraScript(new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y), MoveCameraStyle.LERP));
            AddScript(new AddDialogBoxScript("Powering up your tactical overlay.", new Vector2(Viewport.Width * 0.25f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("This will allow you to issue\ncertain instructions quickly and easily.", new Vector2(Viewport.Width * 0.25f, ScreenCentre.Y)));
            AddScript(new WaitScript(2f, false));
            AddScript(new AddDialogBoxScript("Overlay command stack 90% complete.", new Vector2(Viewport.Width * 0.25f, ScreenCentre.Y), false, false, false, null, 3));
            AddScript(new WaitScript(2f, false));
            AddScript(new AddDialogBoxScript("Overlay fully operational.\nInitialising basic interface.", new Vector2(Viewport.Width * 0.25f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("Click the 'Turrets' button when ready.", new Vector2(Viewport.Width * 0.25f, ScreenCentre.Y)));

            AddScript(new ShowAndActivateObjectScript(UnderSiegeGameplayScreen.HUD.GetUIObject("Buy Turrets UI")));
            AddScript(new WaitForObjectSelectionScript(UnderSiegeGameplayScreen.HUD.GetUIObject("Buy Turrets UI")));
            AddScript(new AddDialogBoxScript("Here, you can issue turret construction orders.\nTurrets will help fend off attackers.", new Vector2(ScreenCentre.X * 0.75f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("Turrets come in three types:\nKINETIC, MISSILE and BEAM.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("Kinetic turrets fire laser bolts\nor other projectiles.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("They are low damage, but fast firing\nand are reliable, cheap firepower.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("Missile turrets fire a powerful payload.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("They take a while to fire,\nbut their damage is devastating.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("The missiles also track their target.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("Beam turrets focus concentrated\nenergy on an enemy.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("They take a short time to charge,\nbut can tear through most ships.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new WaitScript(1.5f, false));

            AddScript(new AddDialogBoxScript("Commander, a message.\nOur sensors are reporting enemy movement.", new Vector2(ScreenCentre.X * 0.7f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("We must be ready for them.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("Click any turret icon on your tactical overlay.", new Vector2(ScreenCentre.X * 0.75f, ScreenCentre.Y)));
            AddScript(new RunEventScript(CheckTurretSelected));
            AddScript(new AddDialogBoxScript("When you purchase a ship add on,\nall available hardpoints you can\nbuild on will appear on your overlay.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("Engines can ONLY be built on yellow hardpoints.", new Vector2(ScreenCentre.X * 0.75f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("Other add ons can ONLY be built on green hardpoints.", new Vector2(ScreenCentre.X * 0.7f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("With the turret selected,\nclick on an available hardpoint.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("Click on empty space to clear the selection.", new Vector2(ScreenCentre.X * 0.75f, ScreenCentre.Y)));
            AddScript(new RunEventScript(CheckFirstTurretBought));
            AddScript(new DisableAndHideObjectScript(UnderSiegeGameplayScreen.HUD.GetUIObject("Buy Turrets UI"), false));
            AddScript(new AddDialogBoxScript("A construction crew has been notified.", new Vector2(ScreenCentre.X * 0.75f, ScreenCentre.Y)));
            AddScript(new MoveCameraScript(ScreenCentre, MoveCameraStyle.Linear, 250));

            AddScript(new RunEventScript(CheckEnemyScoutWaveDefeated));
            AddScript(new MoveCameraScript(new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y), MoveCameraStyle.LERP));
            AddScript(new AddDialogBoxScript("A scout!", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("That means more enemies will soon be upon us.", new Vector2(ScreenCentre.X * 0.55f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("I have more to brief you on sir,\nbut this is an emergency situation,\nso the rest will have to wait.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("We should have just enough time\nto build one more turret.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));

            AddScript(new ShowAndActivateObjectScript(UnderSiegeGameplayScreen.HUD.GetUIObject("Buy Turrets UI")));
            AddScript(new RunEventScript(CheckSecondTurretBought));
            AddScript(new RunEventScript(CheckFirstWaveDefeated));
            AddScript(new RunEventScript(DamageAddOnsForRepair));
            AddScript(new MoveCameraScript(new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y), MoveCameraStyle.LERP));
            AddScript(new AddDialogBoxScript("We've sustained minor system damage\nto our command station's defences.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("On the tactical overlay, right click on an a turret.", new Vector2(ScreenCentre.X * 0.75f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("This will bring up the commands that\ncan be performed on the add on.", new Vector2(ScreenCentre.X * 0.75f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("Use the Repair command on all your turrets.", new Vector2(ScreenCentre.X * 0.75f, ScreenCentre.Y)));
            AddScript(new RunEventScript(CheckTurretsRepaired));
            AddScript(new AddDialogBoxScript("Turret crews reporting all repairs completed.", new Vector2(ScreenCentre.X * 0.75f, ScreenCentre.Y)));
            AddScript(new WaitScript(1.5f, false));

            AddScript(new AddDialogBoxScript("Sir, I have news from deck 13.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("Several weeks ago we began testing\na prototype shield generator.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("Apparently, the researchers have managed\nto finish the final integration tests\nand it is ready for construction on our station.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("I have been told that construction orders can now be issued.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("Updating your overlay now.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new WaitScript(1.0f, false));
            AddScript(new ShowAndActivateObjectScript(UnderSiegeGameplayScreen.HUD.GetUIObject("Buy Shields UI")));
            AddScript(new WaitScript(1.0f, false));
            AddScript(new AddDialogBoxScript("Construct a shield generator - it will help protect the station by absorbing damage.", new Vector2(ScreenCentre.X * 0.75f, ScreenCentre.Y)));
            AddScript(new RunEventScript(CheckShieldConstructed));
            AddScript(new WaitScript(1.0f, false));
            AddScript(new AddDialogBoxScript("Sir, you need to see this.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("I am getting readings on long range sensors.", new Vector2(ScreenCentre.X * 0.75f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("Well... ONE reading.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("It appears to be a cruiser class strike vessel.", new Vector2(ScreenCentre.X * 0.75f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("You orders sir?", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new RunEventScript(CheckForFinalWaveSpawn));
            AddScript(new RunEventScript(CheckFinalWaveDefeated));
            AddScript(new WaitScript(1.0f, false));
            AddScript(new AddDialogBoxScript("Enemy cruiser destroyed.", new Vector2(ScreenCentre.X * 0.5f, ScreenCentre.Y)));
            AddScript(new AddDialogBoxScript("Well done sir, I'm seeing no more enemy activity on long range sensors.", new Vector2(ScreenCentre.X * 0.75f, ScreenCentre.Y)));
            AddScript(new RunEventScript(BackToMainMenu));
        }

        #endregion

        #region Events

        private void CheckTurretSelected(object sender, EventArgs e)
        {
            Script script = sender as Script;
            script.Done = InGameUIManager.GetObject<PurchaseItemUI>("Purchase Object UI") != null;
        }

        private void CheckFirstTurretBought(object sender, EventArgs e)
        {
            Script script = sender as Script;
            script.Done = UnderSiegeGameplayScreen.Allies.GetItem("Command Station").ShipAddOns.ShipTurrets.Count >= 1;
        }

        private void CheckSecondTurretBought(object sender, EventArgs e)
        {
            Script script = sender as Script;
            script.Done = UnderSiegeGameplayScreen.Allies.GetItem("Command Station").ShipAddOns.ShipTurrets.Count >= 2;
        }

        private void CheckEnemyScoutWaveDefeated(object sender, EventArgs e)
        {
            Script script = sender as Script;

            if (WaveManager.Waves.Count == GameplayScreenData.WaveNames.Count)
            {
                WaveManager.NewWave();
            }

            script.Done = Enemies.Values.Count == 0;
        }

        private void CheckFirstWaveDefeated(object sender, EventArgs e)
        {
            Script script = sender as Script;

            if (WaveManager.Waves.Count == GameplayScreenData.WaveNames.Count - 1)
            {
                WaveManager.NewWave();
            }
            
            script.Done = Enemies.Values.Count == 0;
        }

        private void DamageAddOnsForRepair(object sender, EventArgs e)
        {
            Script script = sender as Script;
            PlayerShip commandStation = UnderSiegeGameplayScreen.Allies.GetItem("Command Station");
            foreach (ShipTurret turret in commandStation.ShipAddOns.ShipTurrets)
            {
                turret.CurrentHealth = turret.CurrentHealth == turret.ShipAddOnData.Health ? turret.ShipAddOnData.Health * 0.5f : turret.CurrentHealth;
            }

            script.Done = true;
        }

        private void CheckTurretsRepaired(object sender, EventArgs e)
        {
            Script script = sender as Script;
            PlayerShip commandStation = UnderSiegeGameplayScreen.Allies.GetItem("Command Station");

            bool turretRepaired = true;
            foreach (ShipTurret turret in commandStation.ShipAddOns.ShipTurrets)
            {
                turretRepaired = turret.CurrentHealth == turret.ShipAddOnData.Health && turretRepaired;
            }

            script.Done = turretRepaired;
        }

        private void CheckShieldConstructed(object sender, EventArgs e)
        {
            Script script = sender as Script;
            PlayerShip commandStation = UnderSiegeGameplayScreen.Allies.GetItem("Command Station");

            script.Done = commandStation.ShipAddOns.ShipShields.Count == 1;
        }

        private void CheckForFinalWaveSpawn(object sender, EventArgs e)
        {
            Script script = sender as Script;
            PlayerShip commandStation = UnderSiegeGameplayScreen.Allies.GetItem("Command Station");

            script.Done = commandStation.ShipAddOns.ShipTurrets.Count == 3 || commandStation.ShipAddOns.ShipShields.Count == 2 || script.TimeRunFor >= 20f;
        }

        private void CheckFinalWaveDefeated(object sender, EventArgs e)
        {
            Script script = sender as Script;

            if (WaveManager.Waves.Count == GameplayScreenData.WaveNames.Count - 2)
            {
                WaveManager.NewWave();
            }

            script.Done = Enemies.Values.Count == 0;
        }

        private void BackToMainMenu(object sender, EventArgs e)
        {
            Transition(new UnderSiegeMainMenuScreen(ScreenManager, true));
        }

        #endregion
    }
}