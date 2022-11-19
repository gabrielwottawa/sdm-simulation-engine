using SimulationEngine.Api.Datas;
using SimulationEngine.Api.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationEngine.Api.Models
{
    public sealed record HistoricEntitySet(double time, int qty);

    public sealed class EntitySet<TEntity> where TEntity : EntityManager
    {
        private readonly Historic<TEntity> historic;
        private IEnumerable<TEntity> internalCollections;

        public string Name { get; set; }
        public Mode Mode { get; set; }
        public int SizeMax { get; set; } = int.MaxValue;
        public int SizeCurrent => internalCollections.Count();
        public Historic<TEntity> Historic => historic;
        public List<HistoricEntitySet> HistoricEntitySets { get; } = new List<HistoricEntitySet>();

        public EntitySet(string name, Mode mode = Mode.Fifo)
        {
            Name = name;
            Mode = mode;
            internalCollections = new List<TEntity>();
            historic = new Historic<TEntity>("EntitySet " + Name);
        }

        public bool Add(TEntity entity)
        {
            if (SizeCurrent == SizeMax)
                return false;

            historic.CreateInstance(entity);

            switch (Mode)
            {
                case Mode.Fifo:
                    FifoAdd(entity);
                    break;
                case Mode.Lifo:
                    LifoAdd(entity);
                    break;
            }

            HistoricEntitySets.Add(new HistoricEntitySet(Engine.Time, SizeCurrent));

            return false;
        }

        public TEntity Remove()
        {
            var entity = Mode switch
            {
                Mode.Fifo => FifoRemove(),
                Mode.Lifo => LifoRemove(),
                _ => null
            };

            historic.DeleteInstance(entity);

            HistoricEntitySets.Add(new HistoricEntitySet(Engine.Time, SizeCurrent));

            return entity;
        }

        private TEntity LifoRemove()
        {
            var stack = new Stack<TEntity>(internalCollections);
            TEntity entity = stack.Pop();
            internalCollections = new List<TEntity>(stack);
            return entity;
        }

        private TEntity FifoRemove()
        {
            Queue<TEntity> queue = new Queue<TEntity>(internalCollections);
            TEntity entity = queue.Dequeue();
            internalCollections = new List<TEntity>(queue);
            return entity;
        }

        private void LifoAdd(TEntity entity)
        {
            var stack = new Stack<TEntity>(internalCollections);
            stack.Push(entity);
            internalCollections = new List<TEntity>(stack);
        }

        private void FifoAdd(TEntity entity)
        {
            var queue = new Queue<TEntity>(internalCollections);
            queue.Enqueue(entity);
            internalCollections = new List<TEntity>(queue);
        }
    }
}
