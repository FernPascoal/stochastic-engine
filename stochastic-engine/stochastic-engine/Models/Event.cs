using stochastic_engine.Engine;
using System;

namespace stochastic_engine.Models
{
    public class Event
    {
        public string Name { get; set; }
        public Guid EventId { get; set; }

        private Scheduler Scheduler { get; set; }
        private EntitySet EntitySet { get; set; }
        private Resource Resource { get; set; }
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

        public void Execute()
        {
            AlreadyExecuted = true;
        }
    }
}
