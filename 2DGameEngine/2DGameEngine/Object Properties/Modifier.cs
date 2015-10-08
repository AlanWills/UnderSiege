using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Object_Properties
{
    public class Modifier
    {
        #region Properties and Fields

        public object ObjectToModify { get; private set; }
        public object ObjectToModifyResetValue { get; private set; }

        #endregion

        public Modifier(object objectToModify, object objectToModifyResetValue)
        {
            ObjectToModify = objectToModify;
            ObjectToModifyResetValue = objectToModify;
        }

        #region Methods

        public void Modify()
        {
            if (ModifyEvent != null)
            {
                ModifyEvent(ObjectToModify, EventArgs.Empty);
            }
        }

        #endregion

        #region Events

        public event EventHandler ModifyEvent;

        #endregion

        #region Virtual Methods

        #endregion
    }
}
