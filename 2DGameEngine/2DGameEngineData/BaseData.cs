using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngineData
{
    // Base data class shared by all other data classes - this is to allow inheritance and what not
    public class BaseData
    {
        // Everything that will be loaded in via a data file should have a display name
        public string DisplayName
        {
            get;
            set;
        }

        // Most objects will have a texture and those that don't can just have "" passed in
        public string TextureAsset
        {
            get;
            set;
        }
    }
}
