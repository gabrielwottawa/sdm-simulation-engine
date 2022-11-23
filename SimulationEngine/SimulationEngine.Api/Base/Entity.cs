using PetriNets.Controller;

namespace SimulationEngine.Api.Base
{
    public abstract class Entity
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public double CreationTime { get; set; }
        public byte Priority { get; set; }
        public PetriNet PetriNet { get; set; }
    }
}
