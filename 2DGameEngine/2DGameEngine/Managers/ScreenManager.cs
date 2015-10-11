using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Cutscenes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Screens;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Managers
{
    public enum ChangeType { Change, Pop, Push }

    public class ScreenManager : DrawableGameComponent
    {
        #region Events

        public event EventHandler OnScreenChange;

        #endregion

        #region Properties and Fields

        private List<BaseScreen> Screens
        {
            get;
            set;
        }

        private List<BaseScreen> ScreensToAdd
        {
            get;
            set;
        }

        private List<BaseScreen> ScreensToRemove
        {
            get;
            set;
        }

        public static Viewport Viewport
        {
            get;
            private set;
        }

        public static SpriteBatch SpriteBatch
        {
            get;
            set;
        }

        public static Camera Camera
        {
            get;
            private set;
        }

        public static InputHandler Input
        {
            get;
            private set;
        }

        public static ContentManager Content
        {
            get;
            private set;
        }

        public static GraphicsDevice Graphics
        {
            get;
            private set;
        }

        public static Vector2 ScreenCentre
        {
            get { return new Vector2(Viewport.Width, Viewport.Height) / 2; }
        }

        public BaseScreen CurrentScreen
        {
            get 
            { 
                if (Screens.Count > 0)
                    return Screens.ElementAt(0);

                return null;
            }
        }

        #endregion

        public ScreenManager(Game game)
            : base(game)
        {
            Screens = new List<BaseScreen>();
            ScreensToAdd = new List<BaseScreen>();
            ScreensToRemove = new List<BaseScreen>();

            Content = Game.Content;
            Graphics = Game.GraphicsDevice;
            Viewport = Game.GraphicsDevice.Viewport;

            Camera = new Camera();
            Input = new InputHandler(game);
            GameMouse.SetUpMouse();

            Game.Components.Add(this);
            Game.Components.Add(Input);
        }

        #region Methods

        public void AddScreen(BaseScreen screen)
        {
            screen.LoadContent(Content);
            screen.Initialize();

            ScreensToAdd.Add(screen);

            if (OnScreenChange != null)
                OnScreenChange(this, EventArgs.Empty);
        }

        public void RemoveScreen(BaseScreen screen)
        {
            ScreensToRemove.Add(screen);
        }

        public T GetFirstInstanceOf<T>() where T : BaseScreen
        {
            foreach (BaseScreen screen in Screens)
            {
                T instance = screen as T;
                if (instance != null)
                    return instance;
            }

            return null;
        }
        
        public void DrawBackground()
        {
            // Draw the first visible screen's background and return
            foreach (BaseScreen screen in Screens)
            {
                if (screen.Visible)
                {
                    screen.Background.Draw(SpriteBatch);
                    return;
                }
            }
        }

        public void DrawScreenUI()
        {
            foreach (BaseScreen screen in Screens)
            {
                if (screen.Visible)
                {
                    screen.DrawScreenUI();
                }
            }

            GameMouse.Draw(SpriteBatch);
        }

        public void HandleInput()
        {
            foreach (BaseScreen screen in Screens)
            {
                // Update before handling input - otherwise if we select something the WhileSelected method will be called straightaway, which is usually undesired
                if (screen.Active)
                {
                    screen.HandleInput();
                }
            }
        }

        #endregion

        #region Virtual Methods

        protected override void LoadContent()
        {
            base.LoadContent();

            AssetManager.LoadAssets(Content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            GameMouse.Update(gameTime);
            Camera.Update(gameTime);

            foreach (BaseScreen screen in ScreensToAdd)
            {
                Screens.Add(screen);
            }

            ScreensToAdd.Clear();

            foreach (BaseScreen screen in Screens)
            {
                // Update before handling input - otherwise if we select something the WhileSelected method will be called straightaway, which is usually undesired
                if (screen.Active)
                {
                    screen.Update(gameTime);
                }
            }

            foreach (BaseScreen screen in ScreensToRemove)
            {
                Screens.Remove(screen);
            }

            ScreensToRemove.Clear();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            // Have this so that when we transition we overlap between one screen dying and another screen being added and so available to be drawn
            // But obviously, if we have another active screen anyway we want that to be drawn instead of having the dead screen's stuff as the top layer
            /*foreach (BaseScreen screen in ScreensToRemove)
            {
                if (screen.Visible)
                {
                    screen.Draw();
                }
            }*/

            foreach (BaseScreen screen in Screens)
            {
                if (screen.Visible)
                {
                    screen.Draw();
                }
            }
        }

        #endregion

    }
}