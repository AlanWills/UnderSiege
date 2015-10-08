using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CutSceneScriptingEngine.Scripts
{
    public abstract class Script
    {
        #region Properties and Fields

        #endregion

        #region Methods

        #endregion

        #region Virtual Methods

        public virtual bool ShouldUpdateGame()
        {
            return true;
        }

        public abstract void Run()
        {

        }

        public virtual bool Done()
        {
            return true;
        }

        #endregion
    }
}
