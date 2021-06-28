using stochastic_engine.Models;
using System;

namespace stochastic_engine.Entities
{
    public class Customer : Entity
    {
        public Customer(string name, Guid id, double creationTime) : base(name, id, creationTime)
        {
        }
    }
}
