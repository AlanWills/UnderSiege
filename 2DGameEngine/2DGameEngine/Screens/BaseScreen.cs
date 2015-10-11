﻿using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Cutscenes;
using _2DGameEngine.Managers;
using _2DGameEngine.UI_Objects;
using _2DGameEngineData.Screen_Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Screens
{
    public abstract class BaseScreen
    {
        #region Properties and Fields

        public BaseScreenData BaseScreenData
        {
            get;
            private set;
        }

        public string DataAsset
        {
            get;
            private set;
        }

        public BaseScreen Tag
        {
            get;
            private set;
        }

        public ScreenManager ScreenManager
        {
            get;
            private set;
        }

        public BaseObjectManager<UIObject> UIManager
        {
            get;
            protected set;
        }

        public BaseObjectManager<GameObject> GameObjectManager
        {
            get;
            protected set;
        }

        public BaseObjectManager<InGameUIObject> InGameUIManager
        {
            get;
            protected set;
        }

        protected BaseScreen TransitionTo
        {
            get;
            set;
        }

        public ScriptManager ScriptManager
        {
            get;
            private set;
        }

        public ContentManager Content
        {
            get { return ScreenManager.Content; }
        }

        public SpriteBatch SpriteBatch
        {
            get { return ScreenManager.SpriteBatch; }
        }

        public Viewport Viewport
        {
            get { return ScreenManager.Viewport; }
        }

        public Vector2 ScreenCentre
        {
            get { return ScreenManager.ScreenCentre; }
        }

        public bool Visible
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }

        protected string BackgroundAsset
        {
            get;
            set;
        }

        public PictureBox Background
        {
            get;
            protected set;
        }

        protected bool Transitioning;
        private bool Begun = false;
        protected ChangeType ChangeType;

        protected TimeSpan transitionTimer;
        protected TimeSpan transitionInterval = TimeSpan.FromSeconds(0.5f);

        #endregion

        public BaseScreen(ScreenManager screenManager, string dataAsset)
        {
            ScreenManager = screenManager;
            DataAsset = dataAsset;
            Tag = this;
            Visible = true;
            Active = true;

            ScriptManager = new ScriptManager();
            GameObjectManager = new BaseObjectManager<GameObject>();
            UIManager = new BaseObjectManager<UIObject>();
            InGameUIManager = new BaseObjectManager<InGameUIObject>();
        }

        #region Methods

        #region GameObject Addition Methods

        // Simple wrapper to avoid extra coding when adding GameObjects
        public void AddGameObject(GameObject gameObject, string tag, bool load = false, bool linkWithGameObjectManager = true)
        {
            GameObjectManager.AddObject(gameObject, tag, load, linkWithGameObjectManager);
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            GameObjectManager.RemoveObject(gameObject.Tag);
        }

        public void RemoveGameObject(string gameObjectName)
        {
            GameObjectManager.RemoveObject(gameObjectName);
        }

        #endregion

        #region UIObject Addition Methods

        protected void AddBackground()
        {
            Background = new PictureBox(new Vector2(Viewport.Width, Viewport.Height) * 0.5f, BaseScreenData.TextureAsset);
        }

        #region UI Addition Methods

        public void AddUIObject(UIObject uiobject, string tag, bool load = false, bool linkWithUIManager = true)
        {
            UIManager.AddObject(uiobject, tag, load, linkWithUIManager);
        }

        protected void AddButton(Vector2 position, string tag, EventHandler pressedFunction, bool load = false, string text = "")
        {
            Button button = new Button(position, text);
            button.OnSelect += pressedFunction;

            AddUIObject(button, tag, load);
        }

        // Short hand method for creating a quick picture box - for picture boxes with more custom attributes, use AddUIObject
        protected void AddPictureBox(Vector2 position, string tag, Vector2 size, bool load = false, string dataAsset = "")
        {
            PictureBox pictureBox = new PictureBox(position, size, dataAsset);

            AddUIObject(pictureBox, tag, load);
        }

        // Short hand method for creating a quick image - for images with more custom attributes, use AddUIObject
        protected void AddImage(Vector2 position, string tag, string dataAsset, bool load = false)
        {
            Image image = new Image(position, dataAsset);

            AddUIObject(image, tag, load);
        }

        protected void AddLabel(string text, string tag, Vector2 position)
        {
            Label label = new Label(text, position, Color.White);

            AddUIObject(label, tag, false);
        }

        public void RemoveUIObject(UIObject uiObject)
        {
            UIManager.RemoveObject(uiObject.Tag);
        }

        public void RemoveUIObject(string uiObjectName)
        {
            UIManager.RemoveObject(uiObjectName);
        }

        #endregion

        public void AddInGameUIObject(InGameUIObject inGameUIobject, string tag, bool load = true, bool linkWithInGameUIManager = true)
        {
            InGameUIManager.AddObject(inGameUIobject, tag, load, linkWithInGameUIManager);
        }

        public void RemoveInGameUIObject(InGameUIObject inGameUIObject)
        {
            InGameUIManager.RemoveObject(inGameUIObject.Tag);
        }

        public void RemoveInGameUIObject(string inGameUIObjectName)
        {
            InGameUIManager.RemoveObject(inGameUIObjectName);
        }

        #endregion

        private void HandleTransitioning(GameTime gameTime)
        {
            /*if (Transitioning)
            {
                transitionTimer += gameTime.ElapsedGameTime;

                if (transitionTimer >= transitionInterval)
                {
                    Transitioning = false;
                    switch (ChangeType)
                    {
                        case ChangeType.Change:
                            ScreenManager.AddScreen(TransitionTo, false);
                            break;
                        case ChangeType.Pop:
                            ScreenManager.PopState();
                            break;
                        case ChangeType.Push:
                            ScreenManager.PushState(TransitionTo);
                            break;
                    }
                }
            }*/
        }

        public virtual void Transition(BaseScreen transitionTo)
        {
            Transitioning = true;
            TransitionTo = transitionTo;
            transitionTimer = TimeSpan.Zero;

            ScreenManager.AddScreen(transitionTo);
            ScreenManager.RemoveScreen(this);
        }

        #endregion

        #region Virtual Methods

        public virtual void LoadContent(ContentManager content)
        {
            BaseScreenData = AssetManager.GetData<BaseScreenData>(DataAsset);

            if (!string.IsNullOrEmpty(BaseScreenData.TextureAsset))
                AddBackground();

            if (Background != null)
                Background.LoadContent();

            GameObjectManager.LoadContent();
            UIManager.LoadContent();
            InGameUIManager.LoadContent();
        }

        public virtual void Initialize()
        {
            Background.Initialize();
            GameObjectManager.Initialize();
            UIManager.Initialize();
            InGameUIManager.Initialize();
        }

        public virtual void Begin(GameTime gameTime)
        {
            Begun = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!Begun)
            {
                Begin(gameTime);
            }

            ScriptManager.LoadAndAddScripts(Content);
            ScriptManager.UpdateScripts(gameTime);

            if (ScriptManager.UpdateGame)
            {
                GameObjectManager.Update(gameTime);
                UIManager.Update(gameTime);
                InGameUIManager.Update(gameTime);
                // HandleTransitioning(gameTime);
            }
        }

        public virtual void Draw()
        {
            GameObjectManager.Draw(SpriteBatch);

            foreach (GameObject gameObject in GameObjectManager.Values)
            {
                gameObject.DrawInGameUI(SpriteBatch);
            }

            foreach (InGameUIObject inGameUIObject in InGameUIManager.Values)
            {
                inGameUIObject.Draw(SpriteBatch);
            }

            ScriptManager.Draw(SpriteBatch);
        }

        public virtual void DrawScreenUI()
        {
            UIManager.Draw(SpriteBatch);
            ScriptManager.DrawUI(SpriteBatch);
        }

        internal protected virtual void StateChange(object sender, EventArgs e)
        {
            if (ScreenManager.CurrentScreen == Tag)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public virtual void HandleInput()
        {
            if (ScriptManager.UpdateGame)
            {
                GameObjectManager.HandleInput();
                UIManager.HandleInput();
                InGameUIManager.HandleInput();
            }
        }

        public virtual void Show()
        {
            Visible = true;
            Active = true;
        }

        public virtual void Hide()
        {
            Visible = false;
            Active = false;
        }

        #endregion
    }
}
