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

        private PetriNet()
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
        public Transicao GetTransicaoById(int id) => Transicoes.FirstOrDefault(x => x.Id == id);
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

        public PetriNet CriarPetriNet()
        {
            var petri = new PetriNet();

            petri.AddLugar(1, "GarçomLivre");
            petri.AddLugar(2, "SubstituirCaixa");
            petri.AddLugar(3, "PedidoPronto");
            petri.AddLugar(4, "ClienteVaiSentar");
            petri.AddLugar(5, "GarçomNoCaixa");
            petri.AddLugar(6, "LevandoPedido");
            petri.AddLugar(7, "HigienizandoMesa");
            petri.AddLugar(8, "AtendenteVoltou");
            petri.AddLugar(9, "PedidoEntregue");
            petri.AddLugar(10, "MesaHigienizada");

            petri.AddTransicao(1, "T1");
            petri.AddTransicao(2, "T2");
            petri.AddTransicao(3, "T3");
            petri.AddTransicao(4, "T4");
            petri.AddTransicao(5, "T5");
            petri.AddTransicao(6, "T6");

            petri.AddArco(petri.GetLugarById(1), petri.GetTransicaoById(1), 1, true, "normal", "A1", 1);
            petri.AddArco(petri.GetLugarById(1), petri.GetTransicaoById(2), 1, true, "normal", "A2", 2);
            petri.AddArco(petri.GetLugarById(1), petri.GetTransicaoById(3), 1, true, "normal", "A3", 3);
            petri.AddArco(petri.GetLugarById(1), petri.GetTransicaoById(4), 1, false, "normal", "A4", 4);
            petri.AddArco(petri.GetLugarById(1), petri.GetTransicaoById(5), 1, false, "normal", "A5", 5);
            petri.AddArco(petri.GetLugarById(1), petri.GetTransicaoById(6), 1, false, "normal", "A6", 6);

            petri.AddArco(petri.GetLugarById(2), petri.GetTransicaoById(1), 1, true, "normal", "A7", 7);
            petri.AddArco(petri.GetLugarById(3), petri.GetTransicaoById(2), 1, true, "normal", "A8", 8);
            petri.AddArco(petri.GetLugarById(4), petri.GetTransicaoById(3), 1, true, "normal", "A9", 9);

            petri.AddArco(petri.GetLugarById(5), petri.GetTransicaoById(1), 1, false, "normal", "A10", 10);
            petri.AddArco(petri.GetLugarById(5), petri.GetTransicaoById(4), 1, true, "normal", "A11", 11);
            petri.AddArco(petri.GetLugarById(6), petri.GetTransicaoById(2), 1, false, "normal", "A12", 12);
            petri.AddArco(petri.GetLugarById(6), petri.GetTransicaoById(5), 1, true, "normal", "A13", 13);
            petri.AddArco(petri.GetLugarById(7), petri.GetTransicaoById(3), 1, false, "normal", "A14", 14);
            petri.AddArco(petri.GetLugarById(7), petri.GetTransicaoById(6), 1, true, "normal", "A15", 15);

            petri.AddArco(petri.GetLugarById(8), petri.GetTransicaoById(4), 1, true, "normal", "A16", 16);
            petri.AddArco(petri.GetLugarById(9), petri.GetTransicaoById(5), 1, true, "normal", "A17", 17);
            petri.AddArco(petri.GetLugarById(10), petri.GetTransicaoById(6), 1, true, "normal", "A18", 18);

            return petri;
        }
    }
}

