using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationEngine.Api.Base
{
    public abstract class HistoricBase
    {
        public string Name { get; protected set; }

        public abstract double ShorterLifetime();

        public abstract double AverageLifetime();

        public abstract double LongerLifetime();

        public abstract double StandardDeviationOfLife();

    }
}
