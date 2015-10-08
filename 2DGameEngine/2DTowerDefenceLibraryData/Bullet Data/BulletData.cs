using _2DGameEngineData;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DTowerDefenceLibraryData.Bullet_Data
{
    public class BulletData : BaseData
    {
        // The Y component of the max linear velocity of the bullet will be set to this
        public float MaxSpeed
        {
            get;
            set;
        }

        // The acceleration of the bullet - to have a constant velocity bullet upon firing, set this equal to 0
        // and the rigidbody will deal with the rest
        public float LinearAcceleration
        {
            get;
            set;
        }

        // Amount of time the bullet will stay alive for
        public float BulletLifeTime
        {
            get;
            set;
        }
    }
}
