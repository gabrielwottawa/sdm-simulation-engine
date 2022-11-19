using SimulationEngine.Api.Models;

namespace SimulationEngine.Api.Datas
{
    public static class ManagerDatas<T> where T : Manager
    {
        private static readonly Historic<T> historic = new Historic<T>();

        public static void CreateInstance(T instance)
        {
            historic.CreateInstance(instance);
        }

        public static void DeleteInstance(T instance)
        {
            historic.DeleteInstance(instance);
        }

        public static List<InstanceInfo<T>> ListToAlive()
        {
           return historic.ListToAlive();
        }

        public static List<InstanceInfo<T>> ListToDie()
        {
            return historic.ListToDie();
        }
    }
}
