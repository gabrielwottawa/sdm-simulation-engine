using SimulationEngine.Api.Models;

namespace SimulationEngine.Api.Events
{
    public abstract class ManagerEvent : Manager
    {
        protected ManagerEvent()
        {
            this.CreateAllLevels();
        }

        public void Execute()
        {
            Strategy();
            this.DeleteAllLevels();
        }

        protected abstract void Strategy();
    }
}
