using Restaurant.Engine;
using Restaurant.Resources;
using SimulationEngine.Api.Events;
using SimulationEngine.Api.Managers;

namespace Restaurant.Events.Kitchen
{
    public class SendOrderKitchen : ManagedEvent
    {
        private const int qtyChef = 1;

        protected override void Strategy()
        {
            if (!(EngineRestaurant.QueueOrders.CurrentSize > 0))
                return;

            if (!ResourceManager<Chef>.CheckAvailability(qtyChef))
                return;

            var client = EngineRestaurant.QueueOrders.Remove();
            var chefs = ResourceManager<Chef>.Allocated(qtyChef);

            SimulationEngine.Api.Scheduler.ScheduleIn(new OrderPrepared(chefs, client), EngineRestaurant.TimePreparationOrder);
        }
    }
}
