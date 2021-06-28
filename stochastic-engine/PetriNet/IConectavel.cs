using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetProject
{
    public interface IConectavel
    {
        public void ConectarEntrada(Arco arco);
        public void ConectarSaida(Arco arco);
    }
}
