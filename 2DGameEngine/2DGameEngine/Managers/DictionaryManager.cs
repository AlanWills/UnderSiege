using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Managers
{
    public class DictionaryManager<T>
    {
        #region Properties and Fields

        public Dictionary<string, T> Dictionary
        {
            get;
            private set;
        }

        public List<T> Values
        {
            get { return Dictionary.Values.ToList<T>(); }
        }

        public List<string> Keys
        {
            get { return Dictionary.Keys.ToList<string>(); }
        }

        #endregion

        public DictionaryManager()
        {
            Dictionary = new Dictionary<string, T>();
        }

        #region Methods

        public bool Add(string key, T item)
        {
            if (!Dictionary.ContainsKey(key))
            {
                Dictionary.Add(key, item);
                return true;
            }

            return false;
        }

        public bool Add(KeyValuePair<string, T> pair)
        {
            return Add(pair.Key, pair.Value);
        }

        public void Remove(string key)
        {
            if (Dictionary.ContainsKey(key))
                Dictionary.Remove(key);
        }

        public void Remove(KeyValuePair<string, T> pair)
        {
            Remove(pair.Key);
        }

        public T GetItem(string key)
        {
            if (Dictionary.ContainsKey(key))
                return Dictionary[key];

            return default(T);
        }

        public void SetItem(string key, T value)
        {
            if (Dictionary.ContainsKey(key))
                Dictionary[key] = value;
        }

        public K GetItem<K>(string key) where K : T
        {
            if (Dictionary.ContainsKey(key))
                return (K)Dictionary[key];

            return default(K);
        }

        public void Clear()
        {
            Dictionary.Clear();
        }

        #endregion

        #region Virtual Methods

        #endregion
    }
}