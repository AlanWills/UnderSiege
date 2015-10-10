﻿using _2DGameEngine.Cutscenes.Scripts;
using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Cutscenes
{
    public class ScriptManager
    {
        #region Properties and Fields

        private ScreenManager ScreenManager { get; set; }

        private List<Script> ScriptsToAdd { get; set; }
        private List<Script> RunningScripts { get; set; }
        private List<Script> ScriptsToRemove { get; set; }

        #endregion

        public ScriptManager(ScreenManager screenManager)
        {
            ScreenManager = screenManager;

            ScriptsToAdd = new List<Script>();
            RunningScripts = new List<Script>();
            ScriptsToRemove = new List<Script>();
        }

        #region Methods

        public void AddScript(Script script)
        {
            ScriptsToAdd.Add(script);
        }

        public void LoadAndAddScripts(ContentManager content)
        {
            foreach (Script script in ScriptsToAdd)
            {
                script.LoadAndInit(content);
                RunningScripts.Add(script);
            }

            ScriptsToAdd.Clear();
        }

        public bool UpdateScripts(GameTime gameTime)
        {
            bool updateGame = true;

            foreach (Script script in RunningScripts)
            {
                // Update the run status of the script
                script.CheckCanRun();
                if (script.CanRun)
                {
                    // If we can run it, we update the script
                    script.Update(gameTime);
                    script.CheckDone();

                    if (script.Done)
                    {
                        ScriptsToRemove.Add(script);
                    }

                    updateGame = updateGame && script.ShouldUpdateGame();
                }
            }

            foreach (Script script in ScriptsToRemove)
            {
                RunningScripts.Remove(script);
            }

            ScriptsToRemove.Clear();

            return updateGame;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Script script in RunningScripts)
            {
                if (script.CanRun)
                {
                    script.Draw(spriteBatch);
                }
            }
        }

        public void DrawUI(SpriteBatch spriteBatch)
        {
            foreach (Script script in RunningScripts)
            {
                if (script.CanRun)
                {
                    script.DrawUI(spriteBatch);
                }
            }
        }

        public void HandleInput()
        {
            foreach (Script script in RunningScripts)
            {
                if (script.CanRun)
                {
                    script.HandleInput();
                }
            }
        }

        #endregion

        #region Virtual Methods

        #endregion
    }
}
