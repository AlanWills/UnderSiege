using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Game_Objects;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderSiege.UI.In_Game_UI
{
    public class Explosion : AnimatedObject<InGameImage>
    {
        #region Properties and Fields

        #endregion

        public Explosion(Vector2 localPosition, string dataAsset, int framesInX, int framesInY, float timePerFrame, bool isPlaying = false, bool continual = false, BaseObject parent = null)
            : base(localPosition, dataAsset, framesInX, framesInY, timePerFrame, isPlaying, continual, parent)
        {

        }

        public Explosion(Vector2 localPosition, Vector2 size, string dataAsset, int framesInX, int framesInY, float timePerFrame, bool isPlaying = false, bool continual = false, BaseObject parent = null)
            : base(localPosition, size, dataAsset, framesInX, framesInY, timePerFrame, isPlaying, continual, parent)
        {

        }

        #region Methods

        #endregion

        #region Virtual Methods

        #endregion
    }
}
