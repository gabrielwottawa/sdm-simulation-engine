using SimulationEngine.Api.Datas;

namespace SimulationEngine.Api.Models
{
    public abstract class Entity
    {
        private static int countId = 0;

        public static string NameType { get; } = nameof(Entity);

        public int Id { get; private set; }

        protected Entity()
        {
            Id = createId();
        }

        protected void CreateAllLevels()
        {
            var currentType = GetType();

            while (currentType != null && currentType.Name != NameType)
            {
                callTypeManager(currentType, "CreateInstance");
                currentType = currentType.BaseType;
            }
        }

        protected void DeleteAllLevels()
        {
            var currentType = GetType();

            while (currentType != null && currentType.Name != "Entity")
            {
                callTypeManager(currentType, "DeleteInstance");
                currentType = currentType.BaseType;
            }
        }

        private void callTypeManager(Type currentType, string methodName)
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
