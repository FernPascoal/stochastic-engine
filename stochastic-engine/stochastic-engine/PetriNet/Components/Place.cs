using System;
using System.Collections.Generic;
using System.Text;

namespace stochastic_engine.PetriNet.Components
{
    public class Place
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        private int TokenNumber { get; set; }

        public Place(Guid id, string name, int tokenNumber)
        {
            Id = id;
            Name = name;
            TokenNumber = tokenNumber;
        }

        public Place(Guid id, string name)
        {
            new Place(id, name, 0);
        }

        public void AddToken()
        {
            TokenNumber++;
        }

        public void AddTokens(int tokens)
        {
            TokenNumber+= tokens;
        }

        public void DeleteToken()
        {
            TokenNumber--;
        }

        public void DeleteTokens(int tokens)
        {
            TokenNumber-= tokens;
        }
    }
}
