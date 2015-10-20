using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Extra_Components
{
    public static class OptionsData
    {
        public static bool IsFullScreen
        {
            get;
            set;
        }

        public static float MusicVolume
        {
            get;
            set;
        }

        public static float SFXVolume
        {
            get;
            set;
        }
    }

    public static class Options
    {
        #region Properties and Fields

        public static bool IsFullScreen
        {
            get;
            set;
        }

        public static float MusicVolume
        {
            get;
            set;
        }

        public static float SFXVolume
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public static void Load(ContentManager content)
        {

        }

        #endregion

        #region Virtual Methods

        #endregion
    }
}
