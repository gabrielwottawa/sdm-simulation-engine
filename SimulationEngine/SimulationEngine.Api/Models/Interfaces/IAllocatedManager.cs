
namespace SimulationEngine.Api.Models.Interfaces
{
    public interface IAllocatedManager<out T> where T : Resource, new()
    {
        T Resource { get; }

        void Deallocate();
    }
}
