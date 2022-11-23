using Restaurant.Engine;
using Restaurant.Entities;
using Restaurant.Events.Cashier;
using Restaurant.Events.Clients;
using Restaurant.Events.Kitchen;
using SimulationEngine.Api.Events;
using SimulationEngine.Api.Models;
using SimulationEngine.Api.Models.Interfaces;

namespace Restaurant.Events.Bartender
{
    public class FinishServiceCashier : ManagedEvent
    {
        private int cashier;
        private ClientGroup clientGroup;
        private IEnumerable<IManagedAllocation<Resource>> attendants;

        public FinishServiceCashier(int cashier, ClientGroup clientGroup, IEnumerable<IManagedAllocation<Resource>> attendants)
        {
            this.cashier = cashier;
            this.clientGroup = clientGroup;
            this.attendants = attendants;
        }

        protected override void Strategy()
        {
            SimulationEngine.Api.Engine.ScheduleNow(new SendOrderKitchen());

            clientGroup.Order = new Order(clientGroup);

            EngineRestaurant.QueueOrders.Insert(clientGroup);

            insertQueueTables();

            SimulationEngine.Api.Engine.ScheduleNow(new GoToTable(clientGroup.Qty));
            SimulationEngine.Api.Engine.ScheduleNow(new StartServiceCashier(cashier));

            attendantsDeallocate();
        }

        private void attendantsDeallocate()
        {
            foreach(var attendant in attendants)
            {
                attendant.Deallocate();
            }
        }

        private void insertQueueTables()
        {
            switch (clientGroup.Qty)
            {
                case 1:
                    EngineRestaurant.QueueCounterChair.Insert(clientGroup);
                    break;
                case 2:
                    EngineRestaurant.QueueTwoSeaterTable.Insert(clientGroup);
                    break;
                default:
                    EngineRestaurant.QueueFourSeaterTable.Insert(clientGroup);
                    break;
            }
        }
    }
}
