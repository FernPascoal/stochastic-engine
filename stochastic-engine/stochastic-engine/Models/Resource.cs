using System;
using System.Collections.Generic;
using System.Text;

namespace stochastic_engine.Models
{
    public class Resource
    {
        private string name;
        public Guid Id { get; set; }
        private int quantity;

        public Resource(string name, int quantity, Guid id)
        {
            this.name = name;
            this.quantity = quantity;
            Id = id;
        }

        public bool Allocate(int quantity)
        {
            if (this.quantity >= quantity)
            {
                this.quantity -= quantity;
                return true;
            }
            return false;
        }

        public void Release(int quantity)
        {
            this.quantity += quantity;
        }
    }
}
