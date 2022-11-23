using SimulationEngine.Api.Models;

namespace SimulationEngine.Api.Events
{
    public abstract class ManagedEvent : Entity
    {
        protected ManagedEvent()
        {
            CreateAllLevels();
        }

        public void Execute()
        {
            Strategy();
            DeleteAllLevels();
        }

        protected abstract void Strategy();
    }
}
