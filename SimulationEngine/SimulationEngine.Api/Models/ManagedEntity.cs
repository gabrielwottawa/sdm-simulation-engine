using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationEngine.Api.Models
{
    public class ManagedEntity : Entity
    {
        protected ManagedEntity()
        {
            this.CreateAllLevels();
        }

        public void Delete()
        {
            this.DeleteAllLevels();
        }
    }
}
