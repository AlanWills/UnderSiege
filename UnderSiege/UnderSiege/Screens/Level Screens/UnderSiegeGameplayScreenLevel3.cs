using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Cutscenes.Level3;
using UnderSiege.Gameplay_Objects;
using UnderSiege.Player_Data;

namespace UnderSiege.Screens.Level_Screens
{
    public class UnderSiegeGameplayScreenLevel3 : UnderSiegeGameplayScreen
    {
        #region Properties and Fields

        private Vector2 basePosition = new Vector2(850, 500);

        #endregion

        public UnderSiegeGameplayScreenLevel3(ScreenManager screenManager)
            : base(screenManager, "Data\\Screens\\LevelScreens\\Level3")
        {
            Session.Money = 3000;
            WaveManager.Paused = true;

            HUD.DisableAndHideUIObject("Buy Ships Button");

            GameObject shipObject = new GameObject(basePosition + new Vector2(-90, 0), "Sprites\\GameObjects\\Ships\\PlayerShips\\hii_saari");
            AddGameObject(shipObject, "Ship Object");
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void AddScripts()
        {
            base.AddScripts();

            AddScript(new AddCutsceneScript(new Level3StartCutScene(ScreenManager, "Data\\Screens\\LevelScreens\\Level3", this), this));

            TransitionToScreenScript<UnderSiegeGameOverScreen<UnderSiegeGameplayScreenLevel1>> onCommandShipDeath = new TransitionToScreenScript<UnderSiegeGameOverScreen<UnderSiegeGameplayScreenLevel1>>(this);
            onCommandShipDeath.CanRunEvent += checkCommandShipDead;
            AddScript(onCommandShipDeath);

            AddCutsceneScript endCutScene = new AddCutsceneScript(new Level3EndCutScene(ScreenManager, "Data\\Screens\\LevelScreens\\Level3", this), this);
            endCutScene.CanRunEvent += endCutScene_CanRunEvent;
            AddScript(endCutScene);

            AddScript(new TransitionToScreenScript<UnderSiegeGameplayScreenLevel4>(this));
        }

        public override void Initialize()
        {
            base.Initialize();

            CommandShip.LocalPosition = basePosition;
        }

        #endregion

        #region Events

        private void checkCommandShipDead(object sender, EventArgs e)
        {
            (sender as Script).CanRun = CommandShip.Alive == false;
        }

        void endCutScene_CanRunEvent(object sender, EventArgs e)
        {
            (sender as Script).CanRun = WaveManager.Waves.Count == 0;
        }

        #endregion
    }
}
