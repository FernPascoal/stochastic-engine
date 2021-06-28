using stochastic_engine.Engine;
using stochastic_engine.Models;
using System;

namespace stochastic_engine.Entities
{
    public class Customer : Entity
    {
        private int Quantity { get; set; }
        public Customer(string name, int quantity, Scheduler scheduler) : base(name, scheduler)
        {
            Quantity = quantity;
        }
    }
}
