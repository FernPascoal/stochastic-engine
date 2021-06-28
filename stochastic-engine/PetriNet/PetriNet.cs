using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetProject
{
    public class PetriNet
    {
        public List<Lugar> Lugares { get; set; } = new List<Lugar>();
        public List<Transicao> Transicoes { get; set; } = new List<Transicao>();
        public List<Arco> Arcos { get; set; } = new List<Arco>();
        public int Ciclo { get; set; } = 0;
        public bool HasCabecalho { get; set; } = false;
        public string DataString { get; set; } = "";
        public List<List<string>> Log { get; set; } = new List<List<string>>();

        public PetriNet(string filename)
        {
            
        }

        public void AtualizarTransicoes()
        {
            foreach (var transicao in Transicoes)
                foreach (var conexao in transicao.ArcosDeEntrada)
                {
                    if ((!conexao.IsInhibitor() && conexao.GetLugar().NumeroDeMarcas > conexao.Peso)
                        || (conexao.IsInhibitor() && conexao.GetLugar().NumeroDeMarcas > conexao.Peso))
                        conexao.GetTransicao().Ativa = true;
                    else
                    {
                        conexao.GetTransicao().Ativa = false;
                        break;
                    }                                           
                }
        }

        public void AddLugar(int id, string nome) => Lugares.Add(new Lugar(id, nome, 0));
        public Lugar GetLugarById(int id) => Lugares.FirstOrDefault(x => x.Id == id);
        public Lugar GetLugarByName(string nome) => Lugares.FirstOrDefault(x => x.Nome == nome);
        public void RemoverLugar(int id) => Lugares.RemoveAll(x => x.Id == id);
        public void AddTransicao(int id, string nome) => Transicoes.Add(new Transicao(id, nome, 0));
        public Transicao GetTransicao(int id) => Transicoes.FirstOrDefault(x => x.Id == id);
        public Transicao GetTransicaoByName(string nome) => Transicoes.FirstOrDefault(x => x.Nome == nome);
        public void RemoverTransicao(int id) => Transicoes.RemoveAll(x => x.Id == id);
        public bool GetTransicaoIsAtiva(int id) => Transicoes.FirstOrDefault(x => x.Id == id).IsAtiva();
        public void InativarTransicao(int id) => Transicoes.FirstOrDefault(x => x.Id == id).Ativa = false;
        public void AtivarTransicao(int id) => Transicoes.FirstOrDefault(x => x.Id == id).Ativa = true;
        public void AddArco(Lugar lugar, Transicao transicao, int peso, bool isEntrada, string tipo, string nome, int id)
        {
            var arco = new Arco(id, nome, peso, tipo);

            if (isEntrada)
                arco.Conectar(lugar, transicao);
            else
                arco.Conectar(transicao, lugar);

            Arcos.Add(arco);
        }

        public bool Run()
        {
            if (HasActiveTransitions())
            {
                RealizarCiclo();
                return true;
            }

            ShowData();
            return false;
        }

        public void RealizarCiclo() 
        {
            foreach (Transicao transicao in Transicoes)
            {
                if (transicao.VerificaAtivacao())
                    transicao.Ativa = true;
                else
                    transicao.Ativa = false;
            }

            ShowData();

            List<Transicao> transicoesAtivas = new List<Transicao>();

            foreach (var transicao in Transicoes)
                if (transicao.IsAtiva())
                    transicoesAtivas.Add(transicao);

            var array = transicoesAtivas.ToArray();
            Array.Sort(array, Transicao.PrioritySorter());
            transicoesAtivas = array.ToList();

            foreach (var transicao in transicoesAtivas)
                if (transicao.VerificaAtivacao())
                    transicao.Ativar();

            Ciclo++;

            foreach (var transicao in Transicoes)
                transicao.Ativa = false;
        }

        public bool HasActiveTransitions()
        {
            int counter = 0;

            foreach (var transicao in Transicoes)
                if (transicao.VerificaAtivacao())
                    counter++;

            return counter > 0;
        }

        public void ShowData()
        {
            if (!HasCabecalho)
            {
                AppendCabecalho();
                AppendData();
            }
            else
                AppendData();

            Console.WriteLine(DataString);
        }

        public void AppendCabecalho()
        {
            string cabecalho = "|Número do ciclo";

            foreach (Lugar lugar in Lugares)
            {
                if (lugar.Nome.Length >= 3)
                    cabecalho = cabecalho + "|" + lugar.Nome + " ";
                else
                    cabecalho = cabecalho + "| " + lugar.Nome + " ";
            }

            int index = 0;

            foreach (Transicao transicao in Transicoes)
            {
                if(index == Transicoes.Count - 1)
                    cabecalho = cabecalho + "| " + transicao.Nome + " |";
                else
                    cabecalho = cabecalho + "| " + transicao.Nome + " ";

                index++;
            }

            DataString = DataString + cabecalho + "\n";
            HasCabecalho = true;
        }

        public void AppendData()
        {
            string dados = "";

            if(Ciclo == 0)
                dados = dados + "|      " + Ciclo + " (Início) ";
            else if (Ciclo >= 10)
                dados = dados + "|     " + Ciclo + "          ";            
            else
                dados = dados + "|      " + Ciclo + "          ";

            foreach (Lugar lugar in Lugares)
            {
                if (lugar.NumeroDeMarcas >= 10)
                    dados = dados + "| " + lugar.NumeroDeMarcas + " ";
                else
                    dados = dados + "|  " + lugar.NumeroDeMarcas + " ";
            }

            int index = 0;

            foreach (Transicao transicao in Transicoes)
            {
                if(index == Transicoes.Count - 1)
                    dados = dados + "|  " + transicao.AtivaToString() + " |";            

                else
                    dados = dados + "|  " + transicao.AtivaToString() + " ";
            

            index++;
            }

            DataString = DataString + dados + "\n";
        }
    }
}

