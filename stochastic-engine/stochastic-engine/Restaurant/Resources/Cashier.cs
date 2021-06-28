using stochastic_engine.Models;
using System;

namespace stochastic_engine.Resources
{
    public class Cashier : Resource
    {
        public Cashier(string name, int quantity, Guid id) : base(name, quantity, id)
        {
        }
    }
}
