
namespace SimulationEngine.Api.Models.Interfaces
{
    public interface IManagedAllocation<out T> where T : Resource, new()
    {
        T Resource { get; }

        void Deallocate();
    }
}
