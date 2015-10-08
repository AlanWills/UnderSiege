using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Game_Objects;
using _2DGameEngine.Managers;
using _2DGameEngine.Physics_Components.Movement_Behaviours;
using _2DGameEngine.Screens;
using _2DGameEngine.UI_Objects;
using _2DTowerDefenceEngine.Turrets;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngineTestGame
{
    public class TestMenuScreen : MainMenuScreen
    {
        public TestMenuScreen(ScreenManager screenManager, bool addDefaultUI)
            : base(screenManager, addDefaultUI)
        {
            GameObject target = new GameObject(new Vector2(100, 300), "Sprites\\GameObjects\\test");
            target.MovementBehaviours.AddBehaviour("Move To Position", new MoveToPosition(new MoveToPositionArgs(target, new Vector2(1800, 300))));
            AddGameObject(target);

            Turret testTurret = new Turret(new Vector2(900, 1000), 0, "Data\\GameObjects\\Turrets\\TestTurret");
            testTurret.MovementBehaviours.AddBehaviour("Point At Target", new PointAtTarget(new PointAtTargetArgs(testTurret, target)));
            AddGameObject(testTurret);
        }
    }
}
