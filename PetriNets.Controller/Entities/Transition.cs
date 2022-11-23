using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNets.Controller.Entities
{
    public class Transition : Entity
    {
        public bool IsEnabled { get { return tokenIsEnabled(); } }

        public List<Connection> InputConnections = new();
        public List<Connection> OutputConnections = new();
        private Action<Transition>? actionAfterExecution;

        public Transition(string id, Action<Transition>? actionAfterExecution = null) : base(id)
        {
            this.actionAfterExecution = actionAfterExecution;
        }

        public void ExecuteTransition()
        {
            foreach(var connection in InputConnections)
                connection.ConsumeTokens();

            foreach (var connection in OutputConnections)
                connection.Place?.ProduceToken(connection.Weight);

            actionAfterExecution?.Invoke(this);
        }

        private bool tokenIsEnabled() => InputConnections.Any() && InputConnections.All(el => el.IsEnabled);
    }
}
