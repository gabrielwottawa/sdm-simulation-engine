using SimulationEngine.Api.Datas;

namespace SimulationEngine.Api.Models
{
    public abstract class Manager
    {
        private static int countId = 0;

        public static string NameType { get; } = nameof(Manager);

        public int Id { get; private set; }

        protected Manager()
        {
            Id = createId();
        }

        protected void CreateAllLevels()
        {
            var currentType = GetType();

            while (currentType != null && currentType.Name != NameType)
            {
                ManagerType(currentType, "nascimento");
                currentType = currentType.BaseType;
            }
        }

        protected void DeleteAllLevels()
        {
            var currentType = GetType();

            while (currentType != null && currentType.Name != "Manager")
            {
                ManagerType(currentType, "morte");
                currentType = currentType.BaseType;
            }
        }

        private void ManagerType(Type currentType, string methodName)
        {
            var managerType = typeof(ManagerDatas<>).MakeGenericType(currentType);
            var methodInfo = managerType.GetMethod(methodName);
            object[] args = { this };

            if (methodInfo != null)
                methodInfo.Invoke(null, args);
        }

        private static int createId() => countId++;
    }
}
