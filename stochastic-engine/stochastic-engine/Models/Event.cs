using stochastic_engine.Engine;
using System;

namespace stochastic_engine.Models
{
    public class Event
    {
        public string Name { get; }
        public Guid EventId { get; set; }

        public double ScheduledTime { get; set; }

        public Scheduler Scheduler { get; set; }
        public EntitySet EntitySet { get; set; }
        public Resource Resource { get; set; }
        public bool AlreadyExecuted { get; set; } = false;

        public Event(string name)
        {
            Name = name;
        }

        public Event(string name, Resource resource, Scheduler scheduler)
        {
            Name = name;
            Resource = resource;
            Scheduler = scheduler;
        }

        public virtual void Execute()
        {
            AlreadyExecuted = true;
        }
    }
}
