using SimulationEngine.Api.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationEngine.Api.Models
{
    public class ManagedAllocation<T> : Entity, IManagedAllocation<T> where T : Resource, new()
    {
        public T Resource { get; }

        public ManagedAllocation(T resource)
        {
            Resource = resource;
            CreateAllLevels();
            Resource.Allocated = true;
        }

        public void Deallocate()
        {
            DeleteAllLevels();
            Resource.Allocated = false;
        }
    }
}
