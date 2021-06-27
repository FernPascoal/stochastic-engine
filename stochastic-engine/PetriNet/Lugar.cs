using PetriNet.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNet
{
    public class Lugar : IConectavel
    {
        private int _id;
        public int Id { get => _id; set => _id = IdValidator.Validate(value); }
        public string _nome;
        public string Nome { get => _nome; set => _nome = NameValidator.Validate(value); }
        public int NumeroDeMarcas { get; set; }
        public List<Arco> ArcosDeEntrada { get; set; } = new List<Arco>();
        public List<Arco> ArcosDeSaida { get; set; } = new List<Arco>();

        public Lugar(int id, string nome, int numeroDeMarcas)
        {
            Id = id;
            Nome = nome;
            NumeroDeMarcas = numeroDeMarcas;
        }

        public void ConectarEntrada(Arco arco) => ArcosDeEntrada.Add(arco);

        public void ConectarSaida(Arco arco) => ArcosDeSaida.Add(arco);
        public void SubtrairMarcas(int qtd) => NumeroDeMarcas -= qtd;
        public void SubtrairUmaMarca() => NumeroDeMarcas--;
        public void AdicionarMarcas(int qtd) => NumeroDeMarcas += qtd;
        public void AdicionarUmaMarca() => NumeroDeMarcas++;
    }
}
