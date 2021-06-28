using stochastic_engine.Engine;
using stochastic_engine.Events;
using stochastic_engine.Models;
using stochastic_engine.Resources;
using System;

namespace stochastic_engine
{
    class Program
    {
        public static void Main(string[] args)
        {
            Scheduler schedulerEngine = new Scheduler();

            EntitySet checkoutQueue1 = schedulerEngine.CreateEntitySet("CheckoutQueue1", 750);
            checkoutQueue1.Mode = Mode.FIFO;

            EntitySet checkoutQueue2 = schedulerEngine.CreateEntitySet("CheckoutQueue2", 750);
            checkoutQueue2.Mode = Mode.FIFO;

            Resource cashier1 = schedulerEngine.CreateResource(new Cashier(1, schedulerEngine, "João"));
            Resource cashier2 = schedulerEngine.CreateResource(new Cashier(1, schedulerEngine, "Julia"));

            Event customersArrival = schedulerEngine.CreateEvent(
                new CustomerArrival(schedulerEngine, checkoutQueue1, checkoutQueue2, cashier1, cashier2)
                );

            schedulerEngine.ScheduleNow(customersArrival);

            schedulerEngine.Simulate();
        }
    }
}
