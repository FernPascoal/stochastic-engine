using Accord.Statistics.Distributions.Univariate;
using stochastic_engine.Engine;
using stochastic_engine.Entities;
using stochastic_engine.Models;
using stochastic_engine.Resources;
using stochastic_engine.Restaurant.Events;
using System;

namespace stochastic_engine.Events
{
    public class CustomerArrival : Event
    {
        private readonly Scheduler scheduler;
        private readonly EntitySet checkoutQueue1;
        private readonly EntitySet checkoutQueue2;

        private readonly Resource cashier1;
        private readonly Resource cashier2;

        public CustomerArrival(Scheduler scheduler, EntitySet queue1, EntitySet queue2, 
            Resource cashier1, Resource cashier2, string name = "Arrival") :
            base(name)
        {
            this.scheduler = scheduler;

            checkoutQueue1 = queue1;
            checkoutQueue2 = queue2;

            this.cashier1 = cashier1;
            this.cashier2 = cashier2;
        }

        public override void Execute()
        {
            base.Execute();

            int quantityOfCustomers = 1 + (new Random().Next(0, 3));

            Entity Customers = scheduler.CreateEntity(new Customer(
                quantityOfCustomers + " customers",
                quantityOfCustomers,
                scheduler
                ));

            bool queue1Greater = checkoutQueue1.Entities.Count > checkoutQueue2.Entities.Count;

            if (queue1Greater)
            {
                checkoutQueue2.Insert(Customers);
                scheduler.ScheduleNow(
                    scheduler.CreateEvent(new CashierService("Cashier Service 2", cashier2, scheduler, checkoutQueue2))
                    );
            }
            else
            {
                checkoutQueue1.Insert(Customers);
                scheduler.ScheduleNow(
                    scheduler.CreateEvent(new CashierService("Cashier Service 1", cashier1, scheduler, checkoutQueue1))
                    );
            }

            if (scheduler.Time < 500)
            {
                double eventTime = (new ExponentialDistribution(3).Generate(1)[0]);

                scheduler.ScheduleIn(
                    scheduler.CreateEvent(
                        new CustomerArrival(scheduler, checkoutQueue1, checkoutQueue2, cashier1, cashier2)),
                    eventTime
                    );
            }

        }
    }
}
