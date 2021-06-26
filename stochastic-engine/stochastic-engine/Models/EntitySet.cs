using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace stochastic_engine.Models
{
    public enum Mode
    {
        NONE = 0,
        FIFO = 1,
        LIFO = 2,
        PRIORITY_BASED = 3
    }
    public class EntitySet
    {
        public string Name { get; }
        public Guid Id { get; }
        public Mode Mode { get; set; }
        public int Size { get; }
        public int MaxPossibleSize { get; set; }
        public Queue<Entity> Entities { get; set; }

        //Coleta de Estatísticas

        private readonly List<int> recordedSizes = new List<int>();
        private readonly Dictionary<Guid, double> recordedTimesInSet = new Dictionary<Guid, double>();
        private bool log = false;
        private double timeGap = 0;

        public EntitySet(string name, Mode mode, int maxPossibleSize, Guid id)
        {
            Name = name;
            Mode = mode;
            this.MaxPossibleSize = maxPossibleSize;
            Id = id;
            Entities = new Queue<Entity>();
        }

        public void Insert(Entity entity)
        {
            if (!IsFull())
            {
                Entities.Enqueue(entity);
                recordedSizes.Add(Entities.Count);
                recordedTimesInSet.Add(Id, 5.4);
            }
        }

        public Entity Remove()
        {
            Entity removedEntity = Entities.Dequeue();
            recordedSizes.Add(Entities.Count);

            return removedEntity;
        }

        public Entity RemoveById(Guid id)
        {
            Entity entityToBeRemoved = Entities.Where(entity => entity.Id == id)?.FirstOrDefault();
            Entities = new Queue<Entity>(Entities.Where(x => entityToBeRemoved != x));
            return entityToBeRemoved;
        }

        public bool IsEmpty()
        {
            return Entities.Count == 0;
        }

        public bool IsFull()
        {
            return Entities.Count == MaxPossibleSize;
        }

        public Entity FindEntity(Guid id)
        {
            return Entities.Where(entity => entity.Id == id)?.FirstOrDefault();
        }

        //Métodos de Coletas de Estatísticas

        public double AverageSize()
        {
            int sizeAccumulator = recordedSizes.Aggregate(0, (acc, x) => acc + x);

            return sizeAccumulator / recordedSizes.Count;
        }

        public double AverageTimeInSet()
        {
            double timeAccumulator = 0;

            foreach(Entity entity in  Entities)
            {
                timeAccumulator += entity.GetTimeSinceCreation();
            }

            double averageTimeInSet = timeAccumulator / Entities.Count;

            return averageTimeInSet;
        }

        public void StartLog(double timeGap)
        {
            this.timeGap = timeGap;
            log = true;
        }

        public void StopLog()
        {
            log = false;
        }
    }
}
