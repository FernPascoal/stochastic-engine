using PetriNetProject;
using stochastic_engine.Engine;
using System;
using System.Collections.Generic;

namespace stochastic_engine.Models
{
    public class Entity
    {
        public string Name { get; }
        public Guid Id { get; set; }
        public double CreationTime { get; set; }
        public int Priority { get; set; }
        public PetriNet PetriNet { get; set; }
        public Scheduler Scheduler { get; set; }
        public List<EntitySet> InsertedSets { get; set; }

        public Entity(string name, Scheduler scheduler)
        {
            Name = name;
            Scheduler = scheduler;
            CreationTime = scheduler.Time;
            Priority = -1;
            InsertedSets = new List<EntitySet>();
        }

        public Entity(string name, Scheduler scheduler, PetriNet petriNet)
        {
            Name = name;
            Scheduler = scheduler;
            CreationTime = scheduler.Time;
            Priority = -1;
            InsertedSets = new List<EntitySet>();
            PetriNet = petriNet;
        }

        public double GetTimeSinceCreation()
        {
            double now = Scheduler.Time;
            double timeSinceCreation = now - CreationTime;
            return timeSinceCreation;
        }
    }
}
