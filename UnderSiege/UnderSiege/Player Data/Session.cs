using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;

namespace UnderSiege.Player_Data
{
    public class Session
    {
        #region Properties and Fields

        public int Money
        {
            get;
            set;
        }

        public List<PlayerShip> PlayerShips
        {
            get;
            private set;
        }

        #endregion

        public Session()
        {
            Money = 15000000;
            PlayerShips = new List<PlayerShip>();
        }

        #region Methods

        #endregion

        #region Virtual Methods

        #endregion
    }
}
