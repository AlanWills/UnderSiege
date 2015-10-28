using _2DGameEngine.Cutscenes;
using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Screens;
using UnderSiege.UI;

namespace UnderSiege.Cutscenes.Level3
{
    public class Level3StartCutScene : Cutscene
    {
        #region Properties and Fields

        #endregion

        public Level3StartCutScene(ScreenManager screenManager, string dataAsset, GameplayScreen gameplayScreen)
            : base(screenManager, dataAsset, gameplayScreen)
        {
            
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void AddScripts()
        {
            base.AddScripts();

            AddDialogBoxScript.defaultPosition = new Vector2(Viewport.Width * 0.75f, Viewport.Height * 0.75f);

            AddScript(new AddDialogBoxScript("Captain, we have successfully docked with UNI Shipyard Kantus."));
            AddScript(new AddDialogBoxScript("Excellent, let us commence repairs immediately."));
            AddScript(new AddDialogBoxScript("In the mean time, I must attend a debriefing on these latest attacks."));
            AddScript(new WaitScript(2));
            AddScript(new AddDialogBoxScript("Ah, Captain, I'm glad you came - I am Admiral Utheck.\nWith you here, I can begin the discussion."));
            AddScript(new AddDialogBoxScript("As you are all no doubt aware, attacks on \nouter-sector stations have been steadily increasing.", false));
            AddScript(new AddDialogBoxScript("UNI have been unable to retrieve much information about our attackers,", false));
            AddScript(new AddDialogBoxScript("but we understand they are calling themselves the 'Bushi'.", false));
            AddScript(new AddDialogBoxScript("From analysing both wreckage and flight data,\nwe have calculated that they are more advanced\nin both technology and strategy.", false));
            AddScript(new AddDialogBoxScript("Communication with this alien species has so far been unsuccessful,\nand their ambitions or interests in this area of space unknown.", false));
            AddScript(new AddDialogBoxScript("WARNING"));
            AddScript(new AddDialogBoxScript("PROXIMITY ALERT"));
            AddScript(new AddDialogBoxScript("What?"));
            AddScript(new AddDialogBoxScript("Admiral, multiple quantum waveforms are collapsing around this station."));
            AddScript(new AddDialogBoxScript("These are exactly the same readings we\nrecorded moments before the last attack."));
            AddScript(new AddDialogBoxScript("Captain, you have dealt with the Bushi more than me,\nI am placing this shipyard's defence systems under your control."));
            AddScript(new AddDialogBoxScript("Admiral, what about you?"));
            AddScript(new AddDialogBoxScript("Wait and see."));
            AddScript(new RunEventScript(ActivateWaveManager));
            AddScript(new WaitScript(10));
            AddScript(new AddDialogBoxScript("Captain, this is Utheck.", false, true, true));
            AddScript(new AddDialogBoxScript("This shipyard's engineering decks are fully operational.", false, true, true));
            AddScript(new AddDialogBoxScript("Additional strike craft can be constructed\nby issuing orders from your overlay.", false, true, true));
            AddScript(new AddDialogBoxScript("Updating now...", false, true, true));
            AddScript(new ShowAndActivateObjectScript(UnderSiegeGameplayScreen.HUD.GetUIObject("Buy Ships Button")));
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
