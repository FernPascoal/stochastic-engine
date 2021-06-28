using stochastic_engine.Models;
using System;

namespace stochastic_engine.Restaurant.EntitiesSets
{
    class CheckoutLine : EntitySet
    {
        public CheckoutLine(string name, Mode mode, int maxPossibleSize, Guid id) : base(name, mode, maxPossibleSize, id)
        {
        }
    }
}
