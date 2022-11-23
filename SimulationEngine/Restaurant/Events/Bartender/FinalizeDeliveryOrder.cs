using Restaurant.Engine;
using SimulationEngine.Api.Events;

namespace Restaurant.Events.Bartender
{
    public class FinalizeDeliveryOrder : ManagedEvent
    {
        protected override void Strategy()
        {
            EngineRestaurant.Bartender.OrderOnTable.ProduceToken(1);
        }
    }
}
