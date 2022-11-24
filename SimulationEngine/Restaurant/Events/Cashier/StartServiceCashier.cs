using Restaurant.Engine;
using Restaurant.Entities;
using Restaurant.Events.Bartender;
using Restaurant.Resources;
using SimulationEngine.Api.Events;
using SimulationEngine.Api.Managers;
using SimulationEngine.Api.Models;
using SimulationEngine.Api.Models.Interfaces;

namespace Restaurant.Events.Cashier
{
    public class StartServiceCashier : ManagedEvent
    {
        public int cashier;

        public StartServiceCashier(int cashier)
        {
            this.cashier = cashier;
        }

        protected override void Strategy()
        {
            if (!checkQueue())
                return;

            if (!checkAvailability(1))
                return;

            var attendant = allocatedAttendant(1);

            var clients = removeQueue();

            SimulationEngine.Api.Scheduler.ScheduleIn(new FinishServiceCashier(cashier, clients, attendant), EngineRestaurant.TimeServiceCashier);
        }

        private ClientGroup removeQueue()
        {
            if (cashier == 1)
                return EngineRestaurant.QueueCashierOne.Remove();

            return EngineRestaurant.QueueCashierTwo.Remove();
        }

        private IEnumerable<IManagedAllocation<Resource>> allocatedAttendant(int qty)
        {
            switch (cashier)
            {
                case 1:
                    return ResourceManager<CashierOne>.Allocated(qty);
                default:
                    return ResourceManager<CashierTwo>.Allocated(qty);
            }
        }

        private bool checkAvailability(int qty)
        {
            switch (cashier)
            {
                case 1:
                    return ResourceManager<CashierOne>.CheckAvailability(qty);
                default:
                    return ResourceManager<CashierTwo>.CheckAvailability(qty);
            }
        }

        private bool checkQueue()
        {
            if (cashier == 1)
                return EngineRestaurant.QueueCashierOne.CurrentSize > 0;

            return EngineRestaurant.QueueCashierTwo.CurrentSize > 0;
        }
    }
}
