using Accord.Statistics.Distributions.Univariate;
using stochastic_engine.Engine;
using stochastic_engine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace stochastic_engine.Restaurant.Events
{
    public class CashierService : Event
    {
        public CashierService(string name, Resource resource, Scheduler scheduler, EntitySet queue) : base(name)
        {
            Resource = resource;
            EntitySet = queue;
            Scheduler = scheduler;
        }

        public override void Execute()
        {
            base.Execute();

            if (EntitySet.Entities.Count != 0)
            {
                if (Resource.Allocate(1))
                {
                    Scheduler.ScheduleIn(Scheduler.CreateEvent(
                        new Event("Checkout completed")
                        ), new NormalDistribution(8, 2).Generate());
                }
            } else
            {
                Resource.Release(1);
            }
        }
    }
}
