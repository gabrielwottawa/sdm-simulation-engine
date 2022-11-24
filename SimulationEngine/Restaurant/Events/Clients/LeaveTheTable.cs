using Restaurant.Entities;
using SimulationEngine.Api.Events;

namespace Restaurant.Events.Clients
{
    public class LeaveTheTable : ManagedEvent
    {
        private ClientGroup clientGroup;

        public LeaveTheTable(ClientGroup clientGroup)
        {
            this.clientGroup = clientGroup;
        }

        protected override void Strategy()
        {
            foreach(var chair in clientGroup.OccupiedPlace)
                chair.Deallocate();

            SimulationEngine.Api.Scheduler.ScheduleNow(new GoToTable(clientGroup.Qty));
        }
    }
}
