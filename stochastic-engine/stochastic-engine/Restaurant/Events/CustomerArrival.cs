using Accord.Statistics.Distributions.Univariate;
using stochastic_engine.Engine;
using stochastic_engine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace stochastic_engine.Events
{
    public class CustomerArrival : Event
    {
        private Scheduler scheduler;
        
        private EntitySet checkoutLine1;
        private EntitySet checkoutLine2;

        private Resource cashier1;
        private Resource cashier2;

        private double timeLimit;
        public CustomerArrival(double timeLimit, Scheduler scheduler, string name = "Arrival") : base(name)
        {
            this.scheduler = scheduler;
            this.timeLimit = timeLimit;
        }

        public void Execute()
        {
            base.Execute();

            int customers = 1 + (new Random().Next(0,3));
        }
    }
}
