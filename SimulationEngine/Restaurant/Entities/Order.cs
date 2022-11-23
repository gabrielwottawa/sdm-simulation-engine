using SimulationEngine.Api.Models;

namespace Restaurant.Entities
{
    public class Order : ManagedEntity
    {
        private ClientGroup clientGroup { get; set; }

        public bool ReadyToEat { get; set; }

        public Order(ClientGroup clientGroup)
        {
            this.clientGroup = clientGroup;
        }
    }
}
