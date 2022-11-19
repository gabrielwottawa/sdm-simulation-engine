using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationEngine.Api.Managers
{
    public class ManagerRandom
    {
        private const long m = 4294967296;
        private const long a = 1664525;
        private const long c = 1013904223;
        private long lastManager;
        private double lastButOneManager_0a1 = -999.99;
        private double lastManager_0a1;

        public ManagerRandom()
        {
            lastManager = DateTime.Now.Ticks % m;
        }

        public ManagerRandom(long seed)
        {
            lastManager = seed;
        }

        public long Next()
        {
            return lastManager = ((a * lastManager) + c) % m;
        }

        public long Next(long maxValue)
        {
            return Next() % maxValue;
        }

        public double NextScale_0a1(long maxValue = 1000)
        {
            lastButOneManager_0a1 = lastManager_0a1;
            lastManager_0a1 = (Next() % maxValue) / (maxValue - 1.0);
            return lastManager_0a1;
        }

        public double Exponential(double average)
        {
            var x = NextScale_0a1();
            var exponential = -(Math.Log(1.0 - x)) / average;

            if (exponential > double.MaxValue)
                return Exponential(average);

            return exponential;
        }

        public double Normal(double average, double deviation, double minValue = double.MinValue, double maxValue = double.MaxValue)
        {
            if (lastButOneManager_0a1 == -999.99)
                NextScale_0a1();

            NextScale_0a1();

            var vi1 = 2.00 * lastButOneManager_0a1 - 1.00;
            var vi2 = 2.00 * lastManager_0a1 - 1.00;
            var w = vi1 * vi1 + vi2 * vi2;

            if (w < 1.00)
            {
                var y = Math.Sqrt((-2 * Math.Log(w)) / w);
                var x1 = y * vi2;
                var normal = average + deviation * x1;

                if (normal > maxValue)
                    return maxValue;
                else if (normal < minValue)
                    return minValue;
                else 
                    return normal;
            }
            else
                return Normal(average, deviation, minValue, maxValue);
        }
    }
}
