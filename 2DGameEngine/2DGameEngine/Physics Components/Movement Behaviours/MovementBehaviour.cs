using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Physics_Components.Movement_Behaviours
{
    public class MovementBehaviourArgs
    {
        public Dictionary<string, object> Args { get; set; }

        public MovementBehaviourArgs()
        {
            Args = new Dictionary<string, object>();
        }
    }

    public class MovementBehaviour
    {
        private bool DependentBehavioursAdded { get; set; }
        public bool Completed { get; protected set; }
        protected List<KeyValuePair<string, MovementBehaviour>> DependentBehaviours { get; set; }

        public virtual void AddDependentBehaviours()
        {
            DependentBehaviours = new List<KeyValuePair<string, MovementBehaviour>>();
        }

        public virtual void Execute(GameTime gameTime)
        {
            if (!DependentBehavioursAdded)
            {
                AddDependentBehaviours();
                DependentBehavioursAdded = true;
            }
        }

        public virtual void End(MovementBehaviourManager behaviourManager)
        {
            foreach (KeyValuePair<string, MovementBehaviour> pair in DependentBehaviours)
            {
                pair.Value.Completed = true;
                behaviourManager.RemoveBehaviour(pair);
            }
        }
    }
}
