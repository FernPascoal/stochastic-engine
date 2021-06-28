using stochastic_engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace stochastic_engine.Engine
{
    public class Scheduler
    {
        public int Time { get; set; }

        public Dictionary<int, Event> FutureEventsList = new Dictionary<int, Event>();

        public List<Resource> Resources = new List<Resource>();
        public List<EntitySet> EntitiesSets = new List<EntitySet>();
        public List<Entity> Entities = new List<Entity>();

        public void ScheduleNow(Event e)
        {
            FutureEventsList.Add(Time, e);
        }

        public void ScheduleAt(Event e, int timeToEvent)
        {
            FutureEventsList.Add(Time + timeToEvent, e);
        }

        public void ScheduleIn(Event e, int time)
        {
            FutureEventsList.Add(time, e);
        }

        public void WaitFor(int time)
        {
            Time += time;
        }

        public List<Event> GetEvents()
        {
            return FutureEventsList.Values.Where(e => !e.AlreadyExecuted).ToList();
        }

        public Event GetNextEvent()
        {
            foreach(int k in FutureEventsList.Keys)
            {
                if (k >= Time)
                    return FutureEventsList.ElementAt(k).Value;
            }
            return null;
        }

        public void Simulate()
        {
            while(GetNextEvent() != null)
            {
                Event nextEvent = GetNextEvent();
                Console.WriteLine("Running event: " + nextEvent.Name);
            }
        }

        public void ExecuteEvent(Event e)
        {
            Time = FutureEventsList.FirstOrDefault(fe => fe.Equals(e)).Key;
            e.Execute();
        }

    }
}
