using Restaurant.Engine;
using Restaurant.Entities;
using SimulationEngine.Api.Events;

namespace Restaurant.Events.Clients
{
    public class StartEating : ManagedEvent
    {
        private readonly ClientGroup clientGroup;

        public StartEating(ClientGroup clientGroup)
        {
            this.clientGroup = clientGroup;
        }

        protected override void Strategy()
        {
            SimulationEngine.Api.Scheduler.ScheduleIn(new LeaveTheTable(clientGroup), EngineRestaurant.MealTime);
        }
    }
}
