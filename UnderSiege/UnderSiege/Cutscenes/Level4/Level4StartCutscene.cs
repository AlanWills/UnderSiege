using _2DGameEngine.Cutscenes;
using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Cutscenes.Scripts;
using UnderSiege.Gameplay_Objects;
using UnderSiege.Screens;

namespace UnderSiege.Cutscenes.Level4
{
    public class Level4StartCutScene : Cutscene
    {
        #region Properties and Fields

        #endregion

        public Level4StartCutScene(ScreenManager screenManager, string dataAsset, GameplayScreen gameplayScreen)
            : base(screenManager, dataAsset, gameplayScreen)
        {

        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void AddScripts()
        {
            base.AddScripts();

            AddDialogBoxScript.defaultPosition = new Vector2(Viewport.Width * 0.75f, Viewport.Height * 0.25f);

            AddScript(new AddDialogBoxScript("Redoubt."));
            AddScript(new AddDialogBoxScript("One of the newest UNI colonies."));
            AddScript(new AddDialogBoxScript("As it's name suggests, it was originally\ndesigned to be a stronghold world, but UNI\nhas had little need for such fortifications."));
            AddScript(new AddDialogBoxScript("At least, until now."));
            AddScript(new AddDialogBoxScript("I have already communicated with UNI command on the planet's surface."));
            AddScript(new AddDialogBoxScript("They have agreed to supply us with what ships and weapons they can."));
            AddScript(new AddDialogBoxScript("Evacuation of the planet has already begun,\nwhich means our top priority is to buy as much time\nas we can for the civilian ships to escape."));
            AddScript(new WaitScript(0.75f));
            AddScript(new AddDialogBoxScript("Even if it means laying down our lives."));
            AddScript(new WaitScript(2));
            AddScript(new AddDialogBoxScript("Incoming message from the planet's surface."));
            AddScript(new WaitScript(0.75f));
            AddScript(new AddDialogBoxScript("Captain, we have been instructed by Redoubt UNI command to be\nplaced under your direct control in the event of an attack."));
            AddScript(new WaitScript(0.5f));
            AddScript(new AddAlliedShipScript(new PlayerShip(new Vector2(500, 300), "Data\\GameObjects\\Ships\\PlayerShips\\HiiFiirkan"), "Fiirkan1", (GameplayScreen as UnderSiegeGameplayScreen), false));
            AddScript(new WaitScript(0.5f));
            AddScript(new AddAlliedShipScript(new PlayerShip(new Vector2(400, 500), "Data\\GameObjects\\Ships\\PlayerShips\\HiiFiirkan"), "Fiirkan2", (GameplayScreen as UnderSiegeGameplayScreen), false));
            AddScript(new WaitScript(0.5f));
            AddScript(new AddAlliedShipScript(new PlayerShip(new Vector2(400, 700), "Data\\GameObjects\\Ships\\PlayerShips\\HiiFiirkan"), "Fiirkan3", (GameplayScreen as UnderSiegeGameplayScreen), false));
            AddScript(new WaitScript(0.5f));
            AddScript(new AddAlliedShipScript(new PlayerShip(new Vector2(500, 900), "Data\\GameObjects\\Ships\\PlayerShips\\HiiFiirkan"), "Fiirkan4", (GameplayScreen as UnderSiegeGameplayScreen), false));
            AddScript(new WaitScript(0.5f));
            AddScript(new AddAlliedShipScript(new PlayerShip(new Vector2(1400, 400), "Data\\GameObjects\\Ships\\PlayerShips\\HiiVanaar"), "Vanaar1", (GameplayScreen as UnderSiegeGameplayScreen), false));
            AddScript(new WaitScript(0.5f));
            AddScript(new AddAlliedShipScript(new PlayerShip(new Vector2(1400, 800), "Data\\GameObjects\\Ships\\PlayerShips\\HiiVanaar"), "Vanaar2", (GameplayScreen as UnderSiegeGameplayScreen), false));
            AddScript(new WaitScript(0.5f));
            AddScript(new RunEventScript(ActivateWaveManager));
            AddScript(new WaitScript(20f));
            AddScript(new AddDialogBoxScript("Here they come!", false, true, true, null, 4.0f));
        }

        protected override void CheckIsDone()
        {
            IsDone = ScriptManager.NoMoreScripts;
        }

        #endregion

        #region Events

        private void ActivateWaveManager(object sender, EventArgs e)
        {
            (GameplayScreen as UnderSiegeGameplayScreen).WaveManager.Paused = false;
            (sender as Script).Done = true;
        }

        #endregion
    }
}
