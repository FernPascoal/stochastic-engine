using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNet.Validators
{
    public class TypeValidator
    {
        public static List<string> Tipos { get; set; } = new List<string>() { "normal", "reset", "inhibitor" };
        public static string Validate(string tipo)
        {
            if (!Tipos.Contains(tipo))
                throw new Exception("Tipo inválido");

            return tipo;
        }
    }
}
