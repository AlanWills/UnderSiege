using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.Object_Attributes;
using _2DGameEngine.Object_Properties;
using _2DGameEngine.Physics_Components;
using _2DGameEngine.Physics_Components.Colliders;
using _2DGameEngine.Screens;
using _2DGameEngineData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Abstract_Object_Classes
{
    public abstract class BaseObject
    {
        #region Events

        // ObjectState events - do I need all these extra ones?  Not used for now
        public event EventHandler OnCreation;
        public event EventHandler OnHide;
        public event EventHandler OnShow;
        public event EventHandler OnDeactivate;
        public event EventHandler OnActivate;
        public event EventHandler OnDeath;

        // UI events
        public event EventHandler OnSelect;
        public event EventHandler OnDeselect;
        public event EventHandler WhileSelected;
        public event EventHandler WhileVisible;
        public event EventHandler MouseOverObject;

        #endregion

        #region Properties and Fields

        /*public ObjectPropertyManager Properties
        {
            get;
            private set;
        }*/

        public string DataAsset
        {
            get;
            set;
        }

        public BaseData BaseData
        {
            get;
            private set;
        }

        public virtual BaseObject Parent
        {
            get;
            set;
        }

        public string Tag
        {
            get;
            set;
        }

        private Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
            set
            {
                texture = value;

                if (Size == Vector2.Zero)
                {
                    Debug.Assert(Texture != null);
                    Size = new Vector2(Texture.Width, Texture.Height);
                }
            }
        }

        public Vector2 Size
        {
            get;
            set;
        }

        public virtual Vector2 Scale
        {
            get
            {
                Debug.Assert(Texture != null);
                return Vector2.Divide(Size, new Vector2(Texture.Width, Texture.Height));
            }
        }

        public Vector2 AspectRatio
        {
            get
            {
                Debug.Assert(Texture != null);
                return new Vector2((float)Texture.Height / (float)Texture.Width);
            }
        }

        public virtual Vector2 LocalPosition
        {
            get;
            set;
        }

        public Vector2 WorldPosition
        {
            get
            {
                if (Parent != null && Parent != BaseScreen.SceneRoot)
                {
                    // Good performance here
                    return Vector2.Add(Parent.WorldPosition, Vector2.Transform(LocalPosition, Matrix.CreateRotationZ(Parent.WorldRotation)));
                }

                return LocalPosition;
            }
        }

        private float localRotation;
        public virtual float LocalRotation
        {
            get
            {
                return localRotation;
            }
            set
            {
                localRotation = MathHelper.WrapAngle(value);
            }
        }

        public float WorldRotation
        {
            get
            {
                if (Parent != null)
                    return MathHelper.WrapAngle(Parent.WorldRotation + LocalRotation);

                return LocalRotation;
            }
        }

        public Vector2 Centre
        {
            get 
            {
                Debug.Assert(Texture != null);
                return Vector2.Multiply(new Vector2(Texture.Width, Texture.Height), 0.5f);
            }
        }

        public Collider Collider
        {
            get;
            protected set;
        }

        private Rectangle sourceRectangle;
        public Rectangle SourceRectangle
        {
            get
            {
                if (sourceRectangle == Rectangle.Empty && Texture != null)
                    sourceRectangle = new Rectangle(0, 0, (int)Texture.Width, (int)Texture.Height); ;

                return sourceRectangle;
            }
            set { sourceRectangle = value; }
        }

        public Rectangle DestinationRectangle
        {
            get
            {
                return new Rectangle((int)WorldPosition.X, (int)WorldPosition.Y, (int)Size.X, (int)Size.Y); ;
            }
        }

        public bool MouseOver
        {
            get;
            set;
        }

        public InGameUIObject InGameHoverUI
        {
            get;
            set;
        }

        public ScreenUIObject ScreenHoverUI
        {
            get;
            set;
        }

        public Color Colour
        {
            get;
            set;
        }

        public float Opacity
        {
            get;
            set;
        }

        #region Object States

        private bool alive;
        public bool Alive
        {
            get { return alive; }
            set 
            {
                alive = value;

                if (alive)
                    Create();
                else
                    Die();
            }
        }

        private bool active;
        public bool Active
        {
            get { return active; }
            set 
            {
                active = value;

                if (active)
                    Activate();
                else
                    Deactivate();
            }
        }

        private bool visible;
        public bool Visible
        {
            get { return visible; }
            set
            {
                visible = value;

                if (visible)
                    Show();
                else
                    Hide();
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;

                if (isSelected)
                    Select();
                else
                    DeSelect();
            }
        }

        public BaseObjectManager<BaseObject> ParentObjectManager
        {
            get;
            set;
        }

        #endregion

        protected TimeSpan clickResetTime = TimeSpan.FromSeconds(0.2f);

        #endregion

        public BaseObject(string dataAsset = "", BaseObject parent = null)
        {
            Opacity = 1f;
            Colour = Color.White;
            Alive = true;
            Parent = parent;
            DataAsset = dataAsset;

            // SetUpProperties();
        }

        public BaseObject(Vector2 localPosition, string dataAsset = "", BaseObject parent = null)
            : this(dataAsset, parent)
        {
            LocalPosition = localPosition;
        }

        public BaseObject(Vector2 localPosition, Vector2 size, string dataAsset = "", BaseObject parent = null)
            : this(localPosition, dataAsset, parent)
        {
            Size = size;
        }

        #region Methods

        #region Methods for BaseObject Properties

        /*public void AddProperty<T>(string key)
        {
            AddProperty<T>(key, default(T));
        }

        public void AddProperty<T>(string key, T value)
        {
            Properties.AddProperty<T>(key, value);
        }

        public void SetProperty<T>(string key, T value)
        {
            Properties.SetProperty<T>(key, value);
        }

        public void AddModifier<T>(string propertyToModify, T value)
        {
            Properties.AddModifier<T>(propertyToModify, value);
        }*/

        #endregion

        #endregion

        #region Abstract Methods

        #endregion

        #region Virtual Methods

        /*public virtual void SetUpProperties()
        {
            Properties = new ObjectPropertyManager();

            AddProperty<Vector2>("Position");
            AddProperty<Vector2>("Size");
            AddProperty<float>("Rotation");
            AddProperty<float>("Opacity", 1f);
        }*/

        public virtual void LoadContent()
        {
            BaseData = AssetManager.GetData<BaseData>(DataAsset);

            if (BaseData != null)
            {
                Texture = AssetManager.GetTexture(BaseData.TextureAsset);
            }
            else
            {
                Texture = AssetManager.GetTexture(DataAsset);
            }

            Debug.Assert(Texture != null);
        }

        public virtual void Initialize()
        {
            AddCollider();
        }

        // Default collider is a rectangular one
        public virtual void AddCollider()
        {
            Collider = new RectangleCollider(this, Size);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Active)
            {
                // Properties.Update(gameTime);
                clickResetTime += gameTime.ElapsedGameTime;

                if (MouseOver)
                {
                    IfMouseOver();
                }

                if (IsSelected)
                {
                    IfSelected();
                }
            }

            // If the parent dies, we kill all children too
            if (Parent != null && !Parent.Alive)
            {
                Alive = false;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                Debug.Assert(Texture != null);
                spriteBatch.Draw(Texture, DestinationRectangle, SourceRectangle, Colour * Opacity, (float)WorldRotation, Centre, SpriteEffects.None, 0);

                IfVisible();
            }
        }

        public virtual void DrawInGameUI(SpriteBatch spriteBatch)
        {
            if (MouseOver && Visible)
            {
                if (InGameHoverUI != null)
                {
                    InGameHoverUI.Draw(spriteBatch);
                }
            }
        }

        public virtual void DrawScreenUI(SpriteBatch spriteBatch)
        {
            if (MouseOver && Visible)
            {
                if (ScreenHoverUI != null)
                {
                    ScreenHoverUI.Draw(spriteBatch);
                }
            }
        }

        public virtual void HandleInput()
        {
            
        }

        #region Events

        #region LifeState Events

        public virtual void Create()
        {
            Active = true;
            Visible = true;

            if (OnCreation != null)
            {
                OnCreation(this, EventArgs.Empty);
            }
        }

        public virtual void Die()
        {
            Active = false;
            Visible = false;

            if (OnDeath != null)
            {
                OnDeath(this, EventArgs.Empty);
            }

            if (ParentObjectManager != null)
            {
                ParentObjectManager.RemoveObject(this);
            }
        }

        #endregion

        #region UpdateState Events

        public virtual void Deactivate()
        {
            if (OnDeactivate != null)
            {
                OnDeactivate(this, EventArgs.Empty);
            }
        }

        public virtual void Activate()
        {
            if (OnActivate != null)
            {
                OnActivate(this, EventArgs.Empty);
            }
        }

        #endregion

        #region DrawState Events

        public virtual void Hide()
        {
            if (OnHide != null)
            {
                OnHide(this, EventArgs.Empty);
            }
        }

        public virtual void Show()
        {
            if (OnShow != null)
            {
                OnShow(this, EventArgs.Empty);
            }
        }

        #endregion

        #endregion

        protected virtual void Select()
        {
            // We have this reset time to stop multiple selects happening in quick 
            clickResetTime = TimeSpan.FromSeconds(0);

            if (OnSelect != null)
            {
                OnSelect(this, EventArgs.Empty);
            }
        }

        protected virtual void DeSelect()
        {
            if (OnDeselect != null)
            {
                OnDeselect(this, EventArgs.Empty);
            }
        }

        protected virtual void IfSelected()
        {
            if (WhileSelected != null)
            {
                WhileSelected(this, EventArgs.Empty);
            }
        }

        protected virtual void IfVisible()
        {
            if (WhileVisible != null)
            {
                WhileVisible(this, EventArgs.Empty);
            }
        }

        protected virtual void IfMouseOver()
        {
            if (MouseOverObject != null)
            {
                MouseOverObject(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}
