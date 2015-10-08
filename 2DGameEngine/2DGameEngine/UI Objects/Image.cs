using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.UI_Objects
{
    public class Image : UIObject
    {
        #region Properties and Fields

        #endregion

        public Image(string dataAsset = "", BaseObject parent = null)
            : base(dataAsset, parent)
        {

        }

        public Image(Vector2 position, string dataAsset = "", BaseObject parent = null)
            : base(position, dataAsset, parent)
        {

        }

        public Image(Vector2 position, Vector2 size, string dataAsset = "", BaseObject parent = null)
            : base(position, size, dataAsset, parent)
        {

        }

        #region Methods

        #endregion

        #region Virtual Methods

        #endregion
    }
}
