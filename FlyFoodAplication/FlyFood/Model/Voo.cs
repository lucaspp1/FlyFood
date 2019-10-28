using FlyFood.DAO.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlyFood.Model
{
    public class Voo
    {
        public DateTime Decolagem { get; set; }
        public float TempoVoo { get; set; }
        public string Companhia { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }

        public int Id { get; set; }

        public static void EntrarVoo()
        {
            FileHelper<Voo> FileHelperVoo = new FileHelper<Voo>();
            FileHelper<Passagem> FileHelperPassagem = new FileHelper<Passagem>();
            List<Passagem> passagensDoCliente = FileHelperPassagem.select().FindAll(passagem => passagem.Cliente == Program.clienteLogado.Id);
            List<Voo> listaDeVoo = FileHelperVoo.select(); // todos os voos
            try
            {
                List<Voo> listaVooCliente = new List<Voo>();
                foreach (Voo voo in listaDeVoo)
                {
                    if (passagensDoCliente.Exists(passagem => passagem.Voo == voo.Id))
                        listaVooCliente.Add(voo);
                }
                DateTime dt = DateTime.Now;
                listaVooCliente = listaVooCliente.FindAll(o => o.validoParaDecolar());
                if (listaVooCliente.Count == 0)
                {
                    Console.WriteLine("Nenhum Voo disponivel \n digite algo para continuar");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine("Lista de voos disponíveis: ");
                foreach (Voo voo in listaVooCliente)
                {
                    Console.WriteLine(voo.detalhesVoo());
                }
                Console.Write("Escolha um voo para entrar: ");
                int idVoo = int.Parse(Console.ReadLine());
                while (!listaVooCliente.Exists(voo => voo.Id == idVoo))
                {
                    Console.Write("Esse voo não existe, deseja escolher outro voo (S/N): ");
                    if (Console.ReadLine().ToLower() == "n")
                    {
                        Console.WriteLine("Digite algo para continuar");
                        Console.ReadKey();
                        return;
                    }
                    Console.Write("Digite uma opção válida: ");
                    idVoo = int.Parse(Console.ReadLine());
                }
                TelaVoo telaVoo = new TelaVoo(listaVooCliente.Find(o => o.Id == idVoo));
                telaVoo.comecarMenu();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message + "\n\n Digite algo para continuar");
                Console.ReadKey();
            }
        }

        public static void CadastrarVoo()
        {
            Voo voo = new Voo();
            FileHelper<Voo> FileHelperVoo = new FileHelper<Voo>();
            try
            {
                Console.WriteLine("Insira a data de decolagem");
                voo.Decolagem = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Insira a duração em horas: ");
                voo.TempoVoo = int.Parse(Console.ReadLine());
                Console.WriteLine("Insira a companhia aérea");
                voo.Companhia = Console.ReadLine();
                Console.WriteLine("Insira o local de origem");
                voo.Origem = Console.ReadLine();
                Console.WriteLine("Insira o local de destino");
                voo.Destino = Console.ReadLine();
                FileHelperVoo.Insert(voo, out string _);
                Console.WriteLine("voo Inserido com sucesso \n Digite algo para continuar");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar voo: " + ex.Message + " \n Digite algo para continuar");
                Console.ReadKey();
            }
        }

        public string detalhesVoo()
        {
            string horas = TempoVoo == 1 ? "hora" : "horas";
            return $" {this.Id} - {this.Origem} até {this.Destino} ({this.Companhia}) \n \t {this.TempoVoo} {horas} de viagem aproximadamente";
        }

        public string detalhesVoo(bool adm)
        {
            string horas = TempoVoo == 1 ? "hora" : "horas";
            return adm ? $"{this.Origem} até {this.Destino} ({this.Companhia}) \n \t {this.TempoVoo} {horas} de viagem aproximadamente, no dia {this.Decolagem.ToString("dd/MM/yyyy")} " : detalhesVoo();
        }

        public bool validoParaDecolar()
        {
            DateTime now = DateTime.Now;
            return Decolagem.Day == now.Day &&
                    Decolagem.Month == now.Month &&
                    Decolagem.Year == now.Year
                ;
        }

    }
}
