using _2DGameEngine.Cutscenes;
using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;

namespace UnderSiege.Screens
{
    public class UnderSiegeTrailer : UnderSiegeGameplayScreen
    {
        #region Properties and Fields

        #endregion

        public UnderSiegeTrailer(ScreenManager screenManager, string dataAsset = "Data\\Screens\\Trailer")
            : base(screenManager, dataAsset)
        {
            
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Initialize()
        {
            base.Initialize();

            AddScript(new RunEventScript(WaitUntilAllDefencesBuilt));
            AddScript(new WaitScript(1, false));
            AddScript(new AddDialogBoxScript("Multiple hostiles dropping out of hyperspace."));
            AddScript(new AddDialogBoxScript("There are enemies everywhere!"));
            AddScript(new AddDialogBoxScript("What do we do sir?"));
            AddScript(new WaitScript(1, false));
            AddScript(new AddDialogBoxScript("Soldier?"));
            AddScript(new WaitScript(1, false));
            AddScript(new AddDialogBoxScript("Shoot."));
        }

        #endregion

        #region Events

        private void WaitUntilAllDefencesBuilt(object sender, EventArgs e)
        {
            PlayerShip commandStation = Allies.GetItem<PlayerShip>("Command Ship");
            (sender as Script).Done = commandStation.ShipAddOns.Count == (commandStation.ShipData.OtherHardPoints.Count + commandStation.ShipData.EngineHardPoints.Count);
        }

        #endregion
    }
}
