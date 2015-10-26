using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Screens
{
    public class OptionsScreen : MenuScreen
    {
        #region Properties and Fields

        private float spacing;
        private BaseScreen previousScreen;

        #endregion

        public OptionsScreen(ScreenManager screenManager, BaseScreen previousScreen, string dataAsset = "Data\\Screens\\OptionsScreen")
            : base(screenManager, dataAsset)
        {
            previousScreen.Active = false;
            previousScreen.Visible = false;

            spacing = Viewport.Height * 0.1f;
            this.previousScreen = previousScreen;
            AddUI();
        }

        #region Methods

        private void AddUI()
        {
            Label fullScreenLabel = new Label("Full Screen", new Vector2(Viewport.Width * 0.25f, Viewport.Height * 0.2f), Color.White);
            UIManager.AddObject(fullScreenLabel, "Full Screen Label");

            Button fullScreenButton = new Button(new Vector2(Viewport.Width * 0.5f, 0), Options.IsFullScreen.ToString(), fullScreenLabel);
            fullScreenButton.OnSelect += fullScreenButton_OnSelect;
            UIManager.AddObject(fullScreenButton, "Full Screen Button");

            Label musicVolumeLabel = new Label("Music Volume", new Vector2(0, spacing), Color.White, fullScreenLabel);
            UIManager.AddObject(musicVolumeLabel, "Music Volume Label");

            ScrollBar musicVolumeBar = new ScrollBar(new Vector2(Viewport.Width * 0.5f, 0), new Vector2(600, 30), "Sprites\\UI\\Bars\\ShieldBar", 100, "Sprites\\UI\\Bars\\ShieldBar", musicVolumeLabel);
            musicVolumeBar.WhileSelected += musicVolumeBar_OnSelect;
            UIManager.AddObject(musicVolumeBar, "Music Volume Bar");

            Label sfxVolumeLabel = new Label("SFX Volume", new Vector2(0, spacing), Color.White, musicVolumeLabel);
            UIManager.AddObject(sfxVolumeLabel, "SFX Volume Label");

            ScrollBar sfxVolumeBar = new ScrollBar(new Vector2(Viewport.Width * 0.5f, 0), new Vector2(600, 30), "Sprites\\UI\\Bars\\ShipHullBar", 100, "Sprites\\UI\\Bars\\ShipHullBar", sfxVolumeLabel);
            sfxVolumeBar.WhileSelected += sfxVolumeBar_OnSelect;
            UIManager.AddObject(sfxVolumeBar, "SFX Volume Bar");
        }

        #endregion

        #region Events

        private void fullScreenButton_OnSelect(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Options.IsFullScreen = !Options.IsFullScreen;
            button.Text = Options.IsFullScreen.ToString();
            ScreenManager.GraphicsDeviceManager.IsFullScreen = Options.IsFullScreen;
            ScreenManager.GraphicsDeviceManager.ApplyChanges();
        }

        private void musicVolumeBar_OnSelect(object sender, EventArgs e)
        {
            ScrollBar musicVolumeBar = sender as ScrollBar;
            Options.MusicVolume = musicVolumeBar.CurrentValue * 0.01f;
            MediaPlayer.Volume = Options.MusicVolume;
        }

        private void sfxVolumeBar_OnSelect(object sender, EventArgs e)
        {
            ScrollBar sfxVolumeBar = sender as ScrollBar;
            Options.SFXVolume = sfxVolumeBar.CurrentValue * 0.01f;
        }

        #endregion

        #region Virtual Methods

        public override void Initialize()
        {
            base.Initialize();

            UIManager.GetObject<ScrollBar>("Music Volume Bar").UpdateValue(Options.MusicVolume * 100);
            UIManager.GetObject<ScrollBar>("SFX Volume Bar").UpdateValue(Options.SFXVolume * 100);
        }

        // Need this because we've changed the queue type we're passing in
        public override void AddMusic(QueueType queueType = QueueType.WaitForCurrent)
        {
            base.AddMusic(queueType);
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if (Active)
            {
                if (InputHandler.KeyPressed(Keys.Escape))
                {
                    ScreenManager.RemoveScreen(this);
                    previousScreen.Active = true;
                    previousScreen.Visible = true;
                }
            }
        }

        #endregion
    }
}
