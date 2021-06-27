using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNet.Validators
{
    public class IdValidator
    {
        public static List<int> Ids { get; set; } = new List<int>();

        public static int Validate(int id)
        {
            if (!Ids.Contains(id) && id > 0)
                Ids.Add(id);
            else
                throw new Exception("Id inválido");

            return id;
        }

    }
}
