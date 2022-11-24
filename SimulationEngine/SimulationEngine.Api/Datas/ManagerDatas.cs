using SimulationEngine.Api.Models;

namespace SimulationEngine.Api.Datas
{
    public static class DataManager<T> where T : Entity
    {
        private static readonly Historic<T> historic = new Historic<T>();

        public static void CreateInstance(T instance) => historic.CreateInstance(instance);

        public static void DeleteInstance(T instance) => historic.DeleteInstance(instance);

        public static List<InstanceInfo<T>> ListAlive() => historic.ListAlive();

        public static List<InstanceInfo<T>> ListDead() => historic.ListDead();
    }
}
