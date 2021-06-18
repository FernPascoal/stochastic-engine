using System;
using System.Collections.Generic;
using System.Text;

namespace stochastic_engine.Models
{
    public class Event
    {
        private string name;
        private Guid eventId;

        public Event(string name, Guid eventId)
        {
            this.name = name;
            this.eventId = eventId;
        }
    }
}
