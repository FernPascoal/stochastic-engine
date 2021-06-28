using stochastic_engine.Engine;
using stochastic_engine.Models;
using System;

namespace stochastic_engine.Resources
{
    public class Cashier : Resource
    {
        public Cashier(int quantity, Scheduler scheduler, string name) : base(name, quantity, scheduler)
        {
        }
    }
}
