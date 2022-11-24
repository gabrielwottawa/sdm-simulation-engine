namespace SimulationEngine.Api.Datas
{
    public class InstanceInfo<T>
    {
        public T Instance { get; set; }

        public bool Alive { get; set; }

        public int Priority { get; set; }

        public double CreateTime { get; set; }

        public double DestructionTime { get; set; }

        public double LifeTime => Alive ? Scheduler.Time - CreateTime : DestructionTime - CreateTime;

        public InstanceInfo(T instance)
        {
            Instance = instance;
        }

        public void ToLive()
        {
            Alive = true;
            CreateTime = Scheduler.Time;
            Priority = int.MinValue;
        }

        public void ToDie()
        {
            Alive = false;
            DestructionTime = Scheduler.Time;
        }
    }
}
