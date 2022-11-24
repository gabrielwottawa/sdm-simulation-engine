using Restaurant.Engine;
using Restaurant.Entities;
using Restaurant.Resources;
using SimulationEngine.Api.Events;
using SimulationEngine.Api.Managers;
using SimulationEngine.Api.Models.Interfaces;

namespace Restaurant.Events.Kitchen
{
    public class OrderPrepared : ManagedEvent
    {
        private readonly IEnumerable<IManagedAllocation<Chef>> chefs;
        private readonly ClientGroup client;

        public OrderPrepared(IEnumerable<IManagedAllocation<Chef>> chefs, ClientGroup client)
        {
            this.chefs = chefs;
            this.client = client;
        }

        protected override void Strategy()
        {
            ResourceManager<Chef>.Deallocate(chefs);

            SimulationEngine.Api.Scheduler.ScheduleNow(new SendOrderKitchen());

            client.Order.ReadyToEat = true;

            if (client.OccupiedPlace != null)
            {
                EngineRestaurant.QueueDelivery.Insert(client);
                EngineRestaurant.Bartender.OrderReady.ProduceToken(1);
            }
        }
    }
}
