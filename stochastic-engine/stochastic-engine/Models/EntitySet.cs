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
            Entities.Enqueue(entity);
        }

        public Entity Remove()
        {
            return Entities.Dequeue();
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
    }
}
