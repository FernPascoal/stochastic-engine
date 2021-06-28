using stochastic_engine.Engine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace stochastic_engine.Models
{
    public class Resource
    {
        public string Name { get; }
        public Guid Id { get; set; }
        public Scheduler Scheduler { get; set; }
        private int availableQuantity;

        //Coleta de Estatísticas
        private readonly List<int> recordedQuantities = new List<int>();
        private double allocatedAt = 0;
        private double timeAllocatedAccumulator;

        public Resource(string name, int quantity)
        {
            Name = name;
            this.availableQuantity = quantity;
        }

        public Resource(String name, int quantity, Scheduler scheduler)
        {
            Name = name;
            availableQuantity = quantity;
            startQuantity = quantity;
            Scheduler = scheduler;
            timeAllocatedAccumulator = 0;
        }

        public bool Allocate(int quantity)
        {
            if (this.availableQuantity >= quantity)
            {
                double now = Scheduler.Time;

                this.availableQuantity -= quantity;
                recordedQuantities.Add(quantity);

                allocatedAt = now;

                Console.WriteLine("Allocated " + quantity + " of " + Name);

                return true;
            }
            return false;
        }

        public void Release(int quantity)
        {
            double now = Scheduler.Time;

            this.availableQuantity += quantity;

            Console.WriteLine("Released " + quantity + " of " + Name);

            recordedQuantities.Add(quantity);
            timeAllocatedAccumulator += allocatedAt - now;
        }

        public double AverageAllocation()
        {
            double now = Scheduler.Time;

            int quantityAccumulator = recordedQuantities.Aggregate(0, (acc, x) => acc + x);

            Console.WriteLine("Resource " + Name + " AverageAllocation " + quantityAccumulator);

            return quantityAccumulator / now;
        }

        public double AllocationRate()
        {
            double now = Scheduler.Time;
            double allocationRate = timeAllocatedAccumulator / now;

            Console.WriteLine("Resource " + Name + " AllocationRate " + allocationRate);

            return allocationRate;
        }
    }
}
