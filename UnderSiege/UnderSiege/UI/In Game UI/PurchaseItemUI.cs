﻿using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.Screens;
using _2DGameEngine.UI_Objects;
using _2DGameEngineData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.UI
{
    public class PurchaseItemUI : InGameUIObject
    {
        #region Properties and Fields

        public BaseData DataAssetOfObject
        {
            get;
            private set;
        }

        public string StoredObjectTypeName
        {
            get;
            set;
        }

        public bool LockedToPosition
        {
            get;
            set;
        }

        #endregion

        // Make this in game
        public PurchaseItemUI(Texture2D texture, BaseData dataAssetOfObject, string storedObjectTypeName, BaseObject parent = null)
            : base(Vector2.Zero, "", parent)
        {
            Texture = texture;
            DataAssetOfObject = dataAssetOfObject;
            StoredObjectTypeName = storedObjectTypeName;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        #endregion
    }
}
