using Restaurant.Engine;
using SimulationEngine.Api.Events;

namespace Restaurant.Events.Cashier
{
    public class FinishGoingToTheBathroom : ManagedEvent
    {
        protected override void Strategy()
        {
            EngineRestaurant.Bartender.ReturnCashier.ProduceToken(1);

            var nextGoToBathroom = EngineRestaurant.TimeGoToBathroomCashier;

            var sumNextGoToBathroom = SimulationEngine.Api.Scheduler.Time + nextGoToBathroom;

            if (sumNextGoToBathroom <= EngineRestaurant.MaximumTimeToBathroom)
                SimulationEngine.Api.Scheduler.ScheduleIn(new GoToTheBathroom(), nextGoToBathroom);
        }
    }
}
