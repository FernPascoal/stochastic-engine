using System;
using System.Collections.Generic;
using System.Linq;

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
        private readonly List<double> recordedTimesInSet = new List<double>();
        private bool log = false;
        private double timeGap = 0;
        private double lastLogTime = 0;
        private double MaxTimeInSet { get; set; } = 0;

        private Dictionary<double, int> Log { get; } = new Dictionary<double, int>();

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
            }

            double now = DateTime.Now.ToOADate();

            if (log && (now - lastLogTime >= timeGap))
            {
                recordedSizes.Add(Entities.Count);
                lastLogTime = now;
                Log.Add(now, Entities.Count);
            }
        }

        public Entity Remove()
        {
            Entity removedEntity = Entities.Dequeue();

            double now = DateTime.Now.ToOADate();

            if (log && (now - lastLogTime >= timeGap))
            {
                int size = Entities.Count;

                double timeSinceEntrance = removedEntity.GetTimeSinceCreation();

                recordedTimesInSet.Add(timeSinceEntrance);
                recordedSizes.Add(size);

                Log.Add(now, size);

                if (timeSinceEntrance > MaxTimeInSet)
                    MaxTimeInSet = timeSinceEntrance;
            }

            return removedEntity;
        }

        public Entity RemoveById(Guid id)
        {
            Entity entityToBeRemoved = Entities.Where(entity => entity.Id == id)?.FirstOrDefault();
            Entities = new Queue<Entity>(Entities.Where(x => entityToBeRemoved != x));
            recordedSizes.Add(Entities.Count);
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
            double timeAccumulator = recordedTimesInSet.Aggregate(0.0, (acc, x) => acc + x);

            return timeAccumulator / recordedTimesInSet.Count;
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
