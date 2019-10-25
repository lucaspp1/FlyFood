using FlyFood.DAO.Service;
using FlyFood.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlyFood
{
    class TelaCliente
    {
        /* MÉTODOS DE AMD */
        private void cadastrarVoo()
        {
            Voo voo = new Voo();
            try
            {
                Console.WriteLine("Insira a data de decolagem");
                voo.Decolagem = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Insira a duração em hroas: ");
                voo.TempoVoo = int.Parse(Console.ReadLine());   
                Console.WriteLine("Insira o companhia aérea");
                voo.Aviao = Console.ReadLine();
                Console.WriteLine("Insira a origem");
                voo.Origem = Console.ReadLine();
                Console.WriteLine("Insira o destino");
                voo.Destino = Console.ReadLine();
                new FileHelper<Voo>().Insert(voo, out string _ );
                Console.WriteLine("voo Inserido com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar voo: " + ex.Message);
            }
        }

        private void inserirLancheVoo()
        {
            List<Voo> listaVoo = new FileHelper<Voo>().select();
            List<Lanche> listaLanche = new FileHelper<Lanche>().select();
            Lanche_Voo lancheVoo = new Lanche_Voo();
            try
            {
                Console.WriteLine("  Selecione um voo ");
                foreach (Voo voo in listaVoo)
                {
                    Console.WriteLine( $" {voo.Id} -`{voo.Decolagem} até ${voo.Destino} no dia ${voo.Decolagem.ToString("dd/mm/yyyy")} " );
                }
                int idVoo = int.Parse(Console.ReadLine());
                while ( listaVoo.FindAll( o => o.Id == idVoo).Count <= 0 )
                {
                    Console.WriteLine("  Selecione um voo Válido ");
                    idVoo = int.Parse(Console.ReadLine());
                }

                Console.WriteLine("  Selecione um lanche ");
                foreach (Lanche lanche in listaLanche)
                {
                    Console.WriteLine($" {lanche.Id} - {lanche.Nome} ");
                }
                int idLanche = int.Parse(Console.ReadLine());
                while (listaLanche.FindAll(o => o.Id == idLanche).Count <= 0)
                {
                    Console.WriteLine("  Selecione um lanche Válido ");
                    idVoo = int.Parse(Console.ReadLine());
                }
                lancheVoo.Lanche = idLanche;
                lancheVoo.Voo = idLanche;

                if (new FileHelper<Lanche_Voo>().select().FindAll( o => o.Voo == idVoo && o.Lanche == idLanche ).Count > 0)
                {
                    Console.WriteLine("  Lanche já inserido no voo ");
                }
                else
                {
                    Console.WriteLine(" Insira a quantidade de lanche ");
                    int quantidade = int.Parse(Console.ReadLine());
                    lancheVoo.Quantidade = quantidade;
                    new FileHelper<Lanche_Voo>().Insert(lancheVoo, out string _);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar lanche no Voo: " + ex.Message);
            }
        }

        private void cadastrarLanche()
        {
            Lanche lanche = new Lanche();
            try
            {
                Console.WriteLine("Insira o nome do produto: ");
                lanche.Nome = Console.ReadLine();
                Console.WriteLine("Insira o preço do produto: "); 
                lanche.Preco = float.Parse(Console.ReadLine());
                new FileHelper<Lanche>().Insert(lanche, out string _);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar lanche: " + ex.Message);
            }
        }

        public void MostrarTelaAdm()
        {
            string menu = @"
seleciona uma das opções:
    1) Cadastrar Um voo
    2) Cadastrar Lanche
    3) Adicionar Lanche para Voo
    3) Sair
opção: ";
            string opcao = "";
            while (opcao != "3")
            {
                Console.Clear();
                Console.WriteLine(menu);
                opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        cadastrarVoo();
                        break;
                    case "2":
                        cadastrarLanche();
                        break;
                    case "3":
                        inserirLancheVoo();
                        break;
                    case "4":
                        Console.WriteLine("Logou :)");
                        break;
                    default:
                        Console.WriteLine("Opção Inválida");
                        break;
                }

            }
        }
        
        public void ComprarPassagem()
        {
            List<Voo> listaVoo = new FileHelper<Voo>().select(); // todos os voos
            List<Passagem> passagensUsuario = new FileHelper<Passagem>().select().FindAll(o => o.Cliente == Program.clienteLogado.Id); // passagens do usuario
            List<Voo> listaVooComprados = listaVoo.FindAll( voo => passagensUsuario.Exists( passagem => passagem.Voo == voo.Id) ); // voos comprados pelo usuario
            listaVoo.RemoveAll(voo => listaVooComprados.Exists( vooComprado => vooComprado.Id == voo.Id)); // tirar voos comprados do usuario
            if (listaVoo.Count > 0)
            {
                Console.WriteLine("Selecione um Voo");
                foreach (Voo voo in listaVoo)
                {
                    Console.WriteLine($" {voo.Id} - {voo.Origem} até {voo.Destino} ");
                }
                int idVoo = int.Parse(Console.ReadLine());
                while ( !listaVoo.Exists( o => o.Id == idVoo) )
                {
                    Console.WriteLine("Digite um valor válido");
                }
                idVoo = int.Parse(Console.ReadLine());
            }
            else
            {
                Console.WriteLine("Nenhum Voo disponível");
            }
        }
        public void entrarVoo()
        {

        }

        /* MÉTODOS DO CLIENTE */
        public void MostrarTelaCliente()
        {
            string menu = @"
seleciona uma das opções:
    1) comprar Passagem um voo
    2) Entrar um Voo
    3) Sair
opção: ";
            string opcao = "";
            while (opcao != "3")
            {
                Console.Clear();
                Console.WriteLine(menu);
                opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        ComprarPassagem();
                        break;
                    case "2":
                        entrarVoo();
                        break;
                    case "3":
                        Console.WriteLine("Logou :)");
                        break;
                    default:
                        Console.WriteLine("Opção Inválida");
                        break;
                }
            }
        }

    }
}
