using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationEngine.Api.Datas
{
    public class InstanceInfo<T>
    {
        public T Instance { get; set; }

        public bool Alive { get; set; }

        public int Priority { get; set; }

        public double CreateTime { get; set; }

        public double DestructionTime { get; set; }

        public double LifeTime => Alive ? Engine.Time - CreateTime : DestructionTime - CreateTime;

        public InstanceInfo(T instance)
        {
            Instance = instance;
        }

        public void ToLive()
        {
            Alive = true;
            CreateTime = Engine.Time;
            Priority = int.MinValue;
        }

        public void ToDie()
        {
            Alive = false;
            DestructionTime = Engine.Time;
        }
    }
}
