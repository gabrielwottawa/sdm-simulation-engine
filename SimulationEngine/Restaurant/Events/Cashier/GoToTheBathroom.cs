using Restaurant.Engine;
using SimulationEngine.Api.Events;

namespace Restaurant.Events.Cashier
{
    public class GoToTheBathroom : ManagedEvent
    {
        protected override void Strategy()
        {
            EngineRestaurant.Bartender.ReplaceCashier.ProduceToken(1);
        }
    }
}
