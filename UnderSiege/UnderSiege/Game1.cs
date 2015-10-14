
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using UnderSiege.Screens;
using _2DGameEngine;

namespace UnderSiege
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager screenManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            if (GlobalVariables.DEBUG)
            {
                graphics.PreferredBackBufferWidth = 1600;
                graphics.PreferredBackBufferHeight = 1024;
                graphics.IsFullScreen = false;
            }
            else
            {
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                graphics.IsFullScreen = true;
            }
        }

        protected override void Initialize()
        {
            screenManager = new ScreenManager(this);
            MainMenuScreen.NextButtonPosition = new Vector2(ScreenManager.Viewport.Width * 0.25f, ScreenManager.Viewport.Height * 0.25f);

            base.Initialize();

            screenManager.AddScreen(new UnderSiegeMainMenuScreen(screenManager, true));
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ScreenManager.SpriteBatch = spriteBatch;
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            screenManager.HandleInput();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw background of current screen
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Matrix.Identity);
            screenManager.DrawBackground();
            spriteBatch.End();

            // Draw the screen camera dependent objects
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, ScreenManager.Camera.Transformation);
            base.Draw(gameTime);
            spriteBatch.End();

            // Draw the camera independent UI
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Matrix.Identity);
            screenManager.DrawScreenUI();
            spriteBatch.End();
        }
    }
}
