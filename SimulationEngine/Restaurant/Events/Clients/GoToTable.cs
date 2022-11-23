using Restaurant.Engine;
using Restaurant.Entities;
using Restaurant.Resources;
using SimulationEngine.Api.Events;
using SimulationEngine.Api.Managers;
using SimulationEngine.Api.Models;
using SimulationEngine.Api.Models.Interfaces;

namespace Restaurant.Events.Clients
{
    public class GoToTable : ManagedEvent
    {
        public int qtyTables;

        public GoToTable(int qtyTables)
        {
            this.qtyTables = qtyTables;
        }

        protected override void Strategy()
        {
            if (!checkQueue())
                return;

            if (!checkAvailability(1))
                return;

            var clients = removeQueue();

            clients.OccupiedPlace = allocatedTable(1);

            //EngineRestaurant.Bartender.

            if (clients.Order.ReadyToEat)
            {
                EngineRestaurant.QueueDelivery.Insert(clients);
                //EngineRestaurant.Bartender.
            }
        }

        private IEnumerable<IManagedAllocation<Resource>> allocatedTable(int qty)
        {
            switch (qtyTables)
            {
                case 1:
                    return ResourceManager<CounterChair>.Allocated(qty);
                case 2:
                    return ResourceManager<TwoSeaterTable>.Allocated(qty);
                default:
                    return ResourceManager<FourSeaterTable>.Allocated(qty);
            }
        }

        private ClientGroup removeQueue()
        {
            switch (qtyTables)
            {
                case 1:
                    return EngineRestaurant.QueueCounterChair.Remove();
                case 2:
                    return EngineRestaurant.QueueTwoSeaterTable.Remove();
                default:
                    return EngineRestaurant.QueueFourSeaterTable.Remove();
            }
        }

        private bool checkAvailability(int qty)
        {
            switch (qtyTables)
            {
                case 1:
                    return ResourceManager<CounterChair>.CheckAvailability(qty);
                case 2:
                    return ResourceManager<TwoSeaterTable>.CheckAvailability(qty);
                default:
                    return ResourceManager<FourSeaterTable>.CheckAvailability(qty);
            }
        }

        private bool checkQueue()
        {
            switch (qtyTables)
            {
                case 1:
                    return EngineRestaurant.QueueCounterChair.CurrentSize > 0;
                case 2:
                    return EngineRestaurant.QueueTwoSeaterTable.CurrentSize > 0;
                default:
                    return EngineRestaurant.QueueFourSeaterTable.CurrentSize > 0;
            }
        }
    }
}
