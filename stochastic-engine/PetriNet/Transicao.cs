using PetriNet.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNet
{
    public class Transicao : IConectavel
    {
        private int _id;
        public int Id { get => _id; set => _id = IdValidator.Validate(value); }
        public string _nome;
        public string Nome { get => _nome; set => _nome = NameValidator.Validate(value); }
        public bool Ativa { get; set; } = false;
        public int Prioridade { get; set; }
        public List<Arco> ArcosDeEntrada { get; set; } = new List<Arco>();
        public List<Arco> ArcosDeSaida { get; set; } = new List<Arco>();

        public Transicao(int id, string nome, int prioridade)
        {
            Id = id;
            Nome = nome;
            Prioridade = prioridade;
        }
        public Transicao() { }

        public bool IsAtiva() => Ativa;
        public string AtivaToString() => Ativa ? "S" : "N";
        public bool VerificaAtivacao()
        {
            int contadorDeValidos = 0;

            foreach (Arco arco in ArcosDeEntrada)
            {
                Lugar entrada = (Lugar)arco.Entrada;

                if (arco.IsNormal())
                {
                    if (entrada.NumeroDeMarcas >= arco.Peso)
                        contadorDeValidos++;
                }
                else if (arco.IsReset())
                    contadorDeValidos++;
                else if (arco.IsInhibitor())
                    if (entrada.NumeroDeMarcas < arco.Peso)
                        contadorDeValidos++;
            }

            return contadorDeValidos == this.ArcosDeEntrada.Count ? true : false;
        }

        public void Ativar()
        {
            foreach (Arco arcoEntrada in ArcosDeEntrada)
            {
                Lugar entrada = (Lugar)arcoEntrada.Entrada;

                if (arcoEntrada.IsNormal())
                    entrada.SubtrairMarcas(arcoEntrada.Peso);
                else if (arcoEntrada.IsReset())
                    entrada.NumeroDeMarcas = 0;
            }

            foreach (Arco arcoSaida in ArcosDeSaida)
            {
                Lugar saida = (Lugar)arcoSaida.Saida;

                saida.AdicionarMarcas(arcoSaida.Peso);
            }
        }

        public void ConectarEntrada(Arco arco) => ArcosDeEntrada.Add(arco);

        public void ConectarSaida(Arco arco) => ArcosDeSaida.Add(arco);
    }
}
