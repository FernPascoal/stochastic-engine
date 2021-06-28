using stochastic_engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace stochastic_engine.Engine
{
    public class Scheduler
    {
        public double Time { get; set; }

        public Dictionary<int, Event> FutureEventsList = new Dictionary<int, Event>();

        public List<Event> Events;
        public List<Resource> Resources;
        public List<EntitySet> EntitiesSets;
        public List<Entity> Entities;

        public Scheduler()
        {
            Time = 0;
            Events = new List<Event>();
            Resources = new List<Resource>();
            EntitiesSets = new List<EntitySet>();
            Entities = new List<Entity>();
        }

        public void ScheduleNow(Event e)
        {
            e.ScheduledTime = Time;
        }

        public void ScheduleAt(Event e, double eventTime)
        {
            e.ScheduledTime = eventTime;
        }

        public void ScheduleIn(Event e, double time)
        {
            e.ScheduledTime = (Time + time);
        }

        public void WaitFor(double time)
        {
            Time += time;
        }

        public List<Event> GetEvents()
        {
            Events.Sort((x, y) => x.ScheduledTime.CompareTo(y.ScheduledTime));
            return Events.Where(e => !e.AlreadyExecuted).ToList();
        }

        public Event GetNextEvent()
        {
            foreach (Event e in GetEvents())
            {
                if (e.ScheduledTime >= Time || Time < 500)
                    return e;
            }
            return null;
        }

        public void Simulate()
        {
            while (GetNextEvent() != null)
            {
                Event nextEvent = GetNextEvent();
                Console.WriteLine("Running event: " + nextEvent.Name);

                ExecuteEvent(nextEvent);
            }
        }

        public void ExecuteEvent(Event e)
        {
            Time = e.ScheduledTime;
            e.Execute();
        }

        public Entity CreateEntity(Entity entity)
        {
            entity.CreationTime = Time;
            entity.Scheduler = this;
            entity.Id = Guid.NewGuid();
            Entities.Add(entity);

            Console.WriteLine("Created Entity: " + entity.Id + " - " + entity.Name);

            return entity;
        }

        public Entity GetEntity(Guid id)
        {
            return Entities.Where(e => e.Id == id)?.FirstOrDefault();
        }

        public Resource CreateResource(Resource resource)
        {
            resource.Id = Guid.NewGuid();
            Resources.Add(resource);

            Console.WriteLine("Created resource: " + resource.Id + " - " + resource.Name);

            return resource;
        }

        public Resource GetResource(Guid id)
        {
            return Resources.Where(r => r.Id == id)?.FirstOrDefault();
        }

        public Event CreateEvent(Event e)
        {
            e.EventId = (Guid.NewGuid());
            Events.Add(e);
            return e;
        }

        public Event GetEvent(Guid id)
        {
            return Events.Where(e => e.EventId == id)?.FirstOrDefault();
        }

        public EntitySet CreateEntitySet(string name, int maxPossibleSize)
        {
            EntitySet entitySet = new EntitySet(name, Mode.FIFO, maxPossibleSize, this);
            entitySet.Id = Guid.NewGuid();

            EntitiesSets.Add(entitySet);

            Console.WriteLine("Created EntitySet: " + entitySet.Id + " - " + entitySet.Name);
            return entitySet;
        }

        public EntitySet GetEntitySet(Guid id)
        {
            return EntitiesSets.Where(entity => entity.Id == id)?.FirstOrDefault();
        }
    }
}
