using Restaurant.Engine;
using Restaurant.Entities;
using Restaurant.Events.Cashier;
using SimulationEngine.Api.Events;

namespace Restaurant.Events.Clients
{
    public class ArrivalCustomers : ManagedEvent
    {
        protected override void Strategy()
        {
            addCashier();

            if (SimulationEngine.Api.Scheduler.Time >= EngineRestaurant.MaxTimeArrivalCustomers)
                return;

            var evArrivalCustomers = new ArrivalCustomers();
            SimulationEngine.Api.Scheduler.ScheduleIn(evArrivalCustomers, EngineRestaurant.ArrivalCustomers);
        }

        private void addCashier()
        {
            if (EngineRestaurant.QueueCashierOne.CurrentSize < EngineRestaurant.QueueCashierTwo.CurrentSize)
            {
                EngineRestaurant.QueueCashierOne.Insert(new ClientGroup());
                var evCashierOne = new StartServiceCashier(1);
                SimulationEngine.Api.Scheduler.ScheduleNow(evCashierOne);
            }
            else
            {
                EngineRestaurant.QueueCashierTwo.Insert(new ClientGroup());
                var evCashierTwo = new StartServiceCashier(2);
                SimulationEngine.Api.Scheduler.ScheduleNow(evCashierTwo);
            }
        }
    }
}
