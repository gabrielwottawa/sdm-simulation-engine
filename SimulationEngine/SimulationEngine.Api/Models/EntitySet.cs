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

    public sealed class EntitySet<TEntity> where TEntity : ManagedEntity
    {
        private readonly Historic<TEntity> historic;
        private IEnumerable<TEntity> internalCollections;

        public string Name { get; set; }
        public Mode Mode { get; set; }
        public int MaxSize { get; set; }
        public int CurrentSize => internalCollections.Count();
        public Historic<TEntity> Historic => historic;
        public List<HistoricEntitySet> HistoricEntitySets { get; } = new List<HistoricEntitySet>();

        public EntitySet(string name, Mode mode, int maxSize)
        {
            Name = name;
            Mode = mode;
            MaxSize = maxSize;
            internalCollections = new List<TEntity>();
            historic = new Historic<TEntity>("EntitySet " + Name);
        }

        public bool Insert(TEntity entity)
        {
            if (IsFull())
                return false;

            historic.CreateInstance(entity);

            switch (Mode)
            {
                case Mode.Fifo:
                    fifoAdd(entity);
                    break;
                case Mode.Lifo:
                    lifoAdd(entity);
                    break;
            }

            HistoricEntitySets.Add(new HistoricEntitySet(Engine.Time, CurrentSize));

            return false;
        }

        public TEntity Remove()
        {
            var entity = Mode switch
            {
                Mode.Fifo => fifoRemove(),
                Mode.Lifo => lifoRemove(),
                _ => null
            };

            historic.DeleteInstance(entity);

            HistoricEntitySets.Add(new HistoricEntitySet(Engine.Time, CurrentSize));

            return entity;
        }

        private TEntity lifoRemove()
        {
            var stack = new Stack<TEntity>(internalCollections);
            var entity = stack.Pop();
            internalCollections = new List<TEntity>(stack);
            return entity;
        }

        private TEntity fifoRemove()
        {
            var queue = new Queue<TEntity>(internalCollections);
            var entity = queue.Dequeue();
            internalCollections = new List<TEntity>(queue);
            return entity;
        }

        private void lifoAdd(TEntity entity)
        {
            var stack = new Stack<TEntity>(internalCollections);
            stack.Push(entity);
            internalCollections = new List<TEntity>(stack);
        }

        private void fifoAdd(TEntity entity)
        {
            var queue = new Queue<TEntity>(internalCollections);
            queue.Enqueue(entity);
            internalCollections = new List<TEntity>(queue);
        }

        public bool IsEmpty() => !internalCollections.Any();

        public bool IsFull() => CurrentSize == MaxSize;        
    }
}
