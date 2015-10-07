using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderSiege.Gameplay_Objects
{
    // This class is just supposed to control squadron like movement of ships driven by a leader ship which the others are parented too
    // It should not be responsible for loading or anything like that
    public class Squadron : BaseObjectManager<Ship>
    {
        #region Properties and Fields

        public Ship LeaderShip { get; set; }

        private Vector2 padding = new Vector2(20, 0);
        private const float left = -1;
        private const float right = 1;

        #endregion

        public Squadron(Ship leaderShip)
            : base()
        {
            LeaderShip = leaderShip;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        /*public override void AddObject(Ship objectToAdd, string tag)
        {
            base.AddObject(objectToAdd, tag, false, false);

            float sideToAddTo = Values.Count % 2 == 0 ? right : left;

            objectToAdd.Parent = LeaderShip;
            
            if (sideToAddTo == right)
            {

            }
        }*/

        #endregion
    }
}
