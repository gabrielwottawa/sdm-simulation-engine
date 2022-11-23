using SimulationEngine.Api.Models;
using SimulationEngine.Api.Models.Interfaces;

namespace Restaurant.Entities
{
    public class ClientGroup : ManagedEntity
    {
        public int Qty { get; }

        public Order Order { get; set; }

        public IEnumerable<IManagedAllocation<Resource>> OccupiedPlace;

        private readonly Random random = new Random();

        public ClientGroup()
        {
            Qty = random.Next(1, 5);
        }
    }
}
