using stochastic_engine.Engine;
using stochastic_engine.Models;
using System;

namespace stochastic_engine.Restaurant.EntitiesSets
{
    class CheckoutLine : EntitySet
    {
        public CheckoutLine(string name, Mode mode, int maxPossibleSize, Scheduler scheduler) : base(name, mode, maxPossibleSize, scheduler)
        {
        }
    }
}
