using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Physics_Components.Movement_Behaviours
{
    public class MovementBehaviourManager : Dictionary<string, List<MovementBehaviour>>
    {
        #region Properties and Fields

        #endregion

        public MovementBehaviourManager()
            : base()
        {
            // Only allow 1 in for now - change it for the ones that make sense
            // Using lists to allow adding and removing during iteration through all behaviours
            this.Add("Move To Position", new List<MovementBehaviour>());
            this.Add("Rotate To Position", new List<MovementBehaviour>());
            this.Add("Point At Target", new List<MovementBehaviour>(1));
        }

        #region Methods

        public void AddBehaviour(KeyValuePair<string, MovementBehaviour> pair)
        {
            AddBehaviour(pair.Key, pair.Value);
        }

        public void AddBehaviour(string name, MovementBehaviour behaviour)
        {
            if (!this.ContainsKey(name))
            {
                this.Add(name, new List<MovementBehaviour>(1));
            }

            if (this[name].Count == this[name].Capacity && this[name].Capacity > 0)
            {
                this[name].RemoveAt(0);
            }

            this[name].Add(behaviour);
        }

        public void RemoveBehaviour(KeyValuePair<string, MovementBehaviour> pair)
        {
            RemoveBehaviour(pair.Key, pair.Value);
        }

        public void RemoveBehaviour(string name, MovementBehaviour behaviour)
        {
            behaviour.End(this);
            this[name].Remove(behaviour);
        }

        public void RemoveAllBehaviours(string name)
        {
            this[name].Clear();
        }

        public void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<string, List<MovementBehaviour>> pair in this)
            {
                if (pair.Value.Count > 0)
                {
                    MovementBehaviour behaviour = pair.Value[0];

                    if (behaviour.Completed)
                        RemoveBehaviour(pair.Key, behaviour);
                    else
                        behaviour.Execute(gameTime);
                }
                else
                {
                    continue;
                }
            }
        }

        #endregion

        #region Virtual Methods

        #endregion
    }
}
