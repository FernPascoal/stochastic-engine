using System;
using System.Collections.Generic;
using System.Linq;

namespace stochastic_engine.Models
{
    public class Resource
    {
        private readonly string name;
        public Guid Id { get; set; }
        private int quantity;

        //Coleta de Estatísticas
        private readonly List<int> recordedQuantities = new List<int>();
        private double allocatedAt = 0;
        private double timeAllocatedAccumulator = 0;

        public Resource(string name, int quantity, Guid id)
        {
            this.name = name;
            this.quantity = quantity;
            Id = id;
        }

        public bool Allocate(int quantity)
        {
            if (this.quantity >= quantity)
            {
                double now = DateTime.Now.ToOADate();

                this.quantity -= quantity;
                recordedQuantities.Add(quantity);

                allocatedAt = now;

                return true;
            }
            return false;
        }

        public void Release(int quantity)
        {
            double now = DateTime.Now.ToOADate();

            this.quantity += quantity;

            recordedQuantities.Add(quantity);
            timeAllocatedAccumulator += allocatedAt - now;
        }

        public double AverageAllocation()
        {
            int quantityAccumulator = recordedQuantities.Aggregate(0, (acc, x) => acc + x);

            return quantityAccumulator / recordedQuantities.Count;
        }

        public double AllocationRate()
        {
            double now = DateTime.Now.ToOADate();
            return timeAllocatedAccumulator / now;
        }
    }
}
