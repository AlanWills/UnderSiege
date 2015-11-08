using _2DGameEngine.Managers;
using _2DGameEngineData.GameObject_Data;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Abstract_Object_Classes
{
    public class DamageableGameObject : GameObject
    {
        #region Properties and Fields

        public DamageableGameObjectData DamageableGameObjectData { get; set; }
        public float CurrentHealth { get; set; }

        #endregion

        public DamageableGameObject(string dataAsset = "", BaseObject parent = null)
            : base(dataAsset, parent)
        {

        }

        public DamageableGameObject(Vector2 position, string dataAsset = "", BaseObject parent = null)
            : base(position, dataAsset, parent)
        {

        }

        public DamageableGameObject(Vector2 position, Vector2 size, string dataAsset = "", BaseObject parent = null)
            : base(position, size, dataAsset, parent)
        {

        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            DamageableGameObjectData = AssetManager.GetData<DamageableGameObjectData>(DataAsset);
            Debug.Assert(DamageableGameObjectData != null);
            CurrentHealth = DamageableGameObjectData.Health;
        }

        public virtual void Damage(float damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
                Alive = false;
        }

        #endregion
    }
}
