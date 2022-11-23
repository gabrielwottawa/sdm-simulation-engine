using Restaurant.Engine;
using SimulationEngine.Api.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Events.Bartender
{
    public class FinalizeSanitizeTable : ManagedEvent
    {
        protected override void Strategy()
        {
            EngineRestaurant.Bartender.SanitizedTable.ProduceToken(1);
        }
    }
}
