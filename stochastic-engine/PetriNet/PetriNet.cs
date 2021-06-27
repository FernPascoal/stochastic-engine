using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNet
{
    public class PetriNet
    {
        public List<Lugar> Lugares { get; set; } = new List<Lugar>();
        public List<Transicao> Transicoes { get; set; } = new List<Transicao>();
        public int Ciclo { get; set; } = 0;
        public bool HasCabecalho { get; set; } = false;
        public string DataString { get; set; } = "";

        public PetriNet(string filename)
        {
            PNM
        }
    }
}

