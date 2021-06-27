using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNet.Validators
{
    public class NameValidator
    {
        public static string Validate(string name)
        {
            if (name.Length < 1)
                throw new Exception("Nome menor que 1 caractere");

            if (name.Length > 3)
                throw new Exception("Nome maior que 3 caracteres");

            return name;
        }
    }
}
