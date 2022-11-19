using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationEngine.Api.Models
{
    public class Resource : Manager
    {
        public bool Allocated { get; set; } = false;

        public Resource()
        {
            this.CreateAllLevels();
        }
    }
}
