using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Managers
{
    public class BaseObjectManager<T> : DictionaryManager<T> where T : BaseObject
    {
        #region Properties and Fields

        public bool AcceptInput
        {
            get;
            set;
        }

        private DictionaryManager<T> ObjectsToAdd
        {
            get;
            set;
        }

        private DictionaryManager<T> ObjectsToRemove
        {
            get;
            set;
        }

        #endregion

        public BaseObjectManager()
            : base()
        {
            AcceptInput = true;
            ObjectsToAdd = new DictionaryManager<T>();
            ObjectsToRemove = new DictionaryManager<T>();
        }

        #region Methods

        public void DeactivateAll()
        {

        }

        #endregion

        #region Virtual Methods

        public virtual void LoadContent()
        {
            foreach (T baseObject in ObjectsToAdd.Values)
            {
                Add(baseObject.Tag, baseObject);
            }

            ObjectsToAdd.Clear();

            foreach (T baseObject in this.Values)
            {
                baseObject.LoadContent();
            }
        }

        public virtual void Initialize()
        {
            foreach (T baseObject in this.Values)
            {
                baseObject.Initialize();
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<string, T> baseObjectToAdd in ObjectsToAdd.Dictionary)
            {
                Add(baseObjectToAdd);
            }

            ObjectsToAdd.Clear();

            foreach (KeyValuePair<string, T> baseObject in this.Dictionary)
            {   
                if (!baseObject.Value.Alive)
                {
                    RemoveObject(baseObject);
                }
                else
                {
                    baseObject.Value.Update(gameTime);
                }
            }

            foreach (KeyValuePair<string, T> baseObjectToRemove in ObjectsToRemove.Dictionary)
                Remove(baseObjectToRemove);

            ObjectsToRemove.Clear();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (T baseObject in this.Values)
                baseObject.Draw(spriteBatch);
        }

        public virtual void HandleInput()
        {
            if (!AcceptInput)
                return;

            foreach (T baseObject in this.Values)
                baseObject.HandleInput();
        }

        public virtual void AddObject(T objectToAdd, string tag, bool load = false, bool linkWithObject = true)
        {
            if (load)
            {
                objectToAdd.LoadContent();
                objectToAdd.Initialize();
            }

            objectToAdd.Tag = tag;

            if (linkWithObject)
                objectToAdd.ParentObjectManager = this as BaseObjectManager<BaseObject>;

            ObjectsToAdd.Add(tag, objectToAdd);
        }

        public virtual void RemoveObject(KeyValuePair<string, T> pair)
        {
            ObjectsToRemove.Add(pair);
        }

        public virtual void RemoveObject(T objectToRemove)
        {
            ObjectsToRemove.Add(objectToRemove.Tag, objectToRemove);
        }

        public virtual void RemoveObject(string key, T objectToRemove)
        {
            ObjectsToRemove.Add(key, objectToRemove);
        }

        public virtual void RemoveObject(string key)
        {
            ObjectsToRemove.Add(key, this.GetItem(key));
        }

        #endregion
    }
}
