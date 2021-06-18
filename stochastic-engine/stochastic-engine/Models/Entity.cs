using System;

namespace stochastic_engine.Models
{
    public class Entity
    {
        public string Name { get; }
        public Guid Id { get; }
        public double CreationTime { get; set; }
        public int Priority { get; set; }
        public PetriNet.PetriNet PetriNet { get; set; }

        public Entity(string name, Guid id, double creationTime)
        {
            Name = name;
            Id = id;
            CreationTime = creationTime;
            Priority = -1;
        }

        public Entity(string name, Guid id, double creationTime, PetriNet.PetriNet petriNet)
        {
            Name = name;
            Id = id;
            CreationTime = creationTime;
            Priority = -1;
            PetriNet = petriNet;
        }

        public double GetTimeSinceCreation()
        {
            double now = DateTime.Now.ToOADate();
            double timeSinceCreation = now - CreationTime;
            return timeSinceCreation;
        }
    }
}
