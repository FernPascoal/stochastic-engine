using PetriNetProject.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetProject
{
    public class Arco
    {
        public IConectavel Entrada { get; set; }
        public IConectavel Saida { get; set; }
        private int _id;
        public int Id { get => _id; set => _id = IdValidator.Validate(value); }
        public string _nome;
        public string Nome { get => _nome; set => _nome = NameValidator.Validate(value); }
        public int Peso { get; set; }
        public string _tipo { get; set; }
        public string Tipo { get => _tipo; set=> _tipo = TypeValidator.Validate(value); }

        public Arco(int id, string nome, int peso, string tipo)
        {
            Id = id;
            Nome = nome;
            Peso = peso;
            Tipo = tipo;
        }

        public Arco(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Arco(IConectavel entrada, IConectavel saida, int id, string nome, int peso, string tipo)
        {
            Entrada = entrada;
            Saida = saida;
            Id = id;
            Nome = nome;
            Peso = peso;
            Tipo = tipo;
        }

        public Arco(){ }

        public bool IsNormal() => _tipo.Equals("normal");
        public bool IsInhibitor() => _tipo.Equals("inhibitor");
        public bool IsReset() => _tipo.Equals("reset");
        public Lugar GetLugar() => Entrada.GetType().Equals(typeof(Lugar)) ? (Lugar)Entrada : (Lugar)Saida;
        public Transicao GetTransicao() => Entrada.GetType().Equals(typeof(Transicao)) ? (Transicao)Entrada : (Transicao)Saida;

        public Arco Conectar(IConectavel c1, IConectavel c2)
        {
            try
            {
                bool isOk = false;

                IConectavel con1 = null;
                IConectavel con2 = null;

                if (c1.GetType().Equals(typeof(Lugar)))
                {
                    Lugar lugar = (Lugar)c1;
                    Transicao transicao = (Transicao)c2;

                    con1 = lugar;
                    con2 = transicao;
                    isOk = true;
                }
                else if (c1.GetType().Equals(typeof(Transicao)))
                {
                    Transicao transicao = (Transicao)c1;
                    Lugar lugar = (Lugar)c2;

                    con1 = transicao;
                    con2 = lugar;
                    isOk = true;
                }

                if (isOk)
                {
                    con1.ConectarSaida(this);
                    con2.ConectarEntrada(this);

                    this.Entrada = con1;
                    this.Saida = con2;

                    return this;
                }

                return null;
            }
            catch (InvalidCastException ex)
            {
                throw new Exception("Conexão inválida");
            }
        }
    }
}

