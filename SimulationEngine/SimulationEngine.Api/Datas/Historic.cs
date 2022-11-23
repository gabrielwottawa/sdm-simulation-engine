using SimulationEngine.Api.Base;
using SimulationEngine.Api.Models;

namespace SimulationEngine.Api.Datas
{
    public class Historic<T> : HistoricBase where T : notnull, Entity
    {
        public Historic()
        {
            initialize(createNameHistoric());
        }

        public Historic(string name)
        {
            initialize(name);
        }

        public List<InstanceInfo<T>> ListInstanceInfos { get; private set; } = new List<InstanceInfo<T>>();

        public void CreateInstance(T instance)
        {
            var instanceInfo = new InstanceInfo<T>(instance);
            instanceInfo.ToLive();
            ListInstanceInfos.Add(instanceInfo);
        }

        public void DeleteInstance(T instance)
        {
            var instanceInfo = ListInstanceInfos.Find(f => f.Instance.Id == instance.Id);
            instanceInfo.ToDie();
        }

        public List<InstanceInfo<T>> ListAlive() => ListInstanceInfos.FindAll(f => f.Alive);

        public List<InstanceInfo<T>> ListDead() => ListInstanceInfos.FindAll(f => !f.Alive);

        public override double AverageLifetime()
        {
            if (ListInstanceInfos.Count == 0)
                return 0;

            var sum = 0d;

            foreach (var info in ListInstanceInfos)
                sum += info.LifeTime;

            return sum / ListInstanceInfos.Count();
        }

        public override double LongerLifetime()
        {
            if (ListInstanceInfos.Count == 0)
                return 0;

            var longer = ListInstanceInfos[0].LifeTime;

            foreach (var info in ListInstanceInfos)
            {
                if (info.LifeTime > longer)
                    longer = info.LifeTime;
            }

            return longer;
        }

        public override double ShorterLifetime()
        {
            if (ListInstanceInfos.Count == 0)
                return 0;

            var shorter = ListInstanceInfos[0].LifeTime;

            foreach (var info in ListInstanceInfos)
            {
                if (info.LifeTime < shorter)
                    shorter = info.LifeTime;
            }

            return shorter;
        }

        public override double StandardDeviationOfLife()
        {
            if (ListInstanceInfos.Count == 0)
                return 0;

            var lifeTimes = new List<double>();

            for (int i = 0; i < ListInstanceInfos.Count; i++)
                lifeTimes.Add(ListInstanceInfos[i].LifeTime);

            if (lifeTimes.Any())
            {
                var average = lifeTimes.Average();
                var sum = lifeTimes.Sum(d => Math.Pow(d - average, 2));

                return Math.Sqrt(sum / (lifeTimes.Count() - 1));
            }

            return 0;
        }

        private void initialize(string name)
        {
            Name = "Historic " + name;
            DataCollect.AddListHistorics(this);
        }

        private string nameNotGenerics()
        {
            var type = typeof(T);
            var typeBase = type.BaseType;

            var nameClass = type.Name;
            var nameClassBase = typeBase.Name;

            if (nameClassBase != "Object" && nameClassBase != Models.Entity.NameType)
                return nameClassBase + " " + nameClass;
            else
                return nameClass;
        }

        private string typeGenerics()
        {
            var type = typeof(T);
            var argsGenerics = type.GetGenericArguments();

            if (argsGenerics.Length == 0)
                return "";

            var genericNames = "< ";

            for (int i = 0; i < argsGenerics.Length; i++)
            {
                var typeArg = (Type)argsGenerics.GetValue(i);
                genericNames += typeArg.Name;
            }

            return genericNames += " >";
        }

        private string createNameHistoric() => nameNotGenerics() + typeGenerics();
    }
}
