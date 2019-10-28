using FlyFood.DAO.Service;
using FlyFood.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlyFood
{
    class TelaCliente
    {
        /* adm actions */
        private void cadastrarVoo()
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
                FileHelperVoo.Insert(voo, out string _ );
                Console.WriteLine("voo Inserido com sucesso \n Digite algo para continuar");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar voo: " + ex.Message + " \n Digite algo para continuar");
                Console.ReadKey();
            }
        }

        private void inserirLancheVoo()
        {
            FileHelper<Voo> FileHelperVoo = new FileHelper<Voo>();
            FileHelper<Lanche> FileHelperLanche = new FileHelper<Lanche>();
            FileHelper<Lanche_Voo> FileHelperLanche_Voo = new FileHelper<Lanche_Voo>();
            List<Voo> listaVoo = FileHelperVoo.select();
            List<Lanche> listaLanche = FileHelperLanche.select();
            Lanche_Voo lancheVoo = new Lanche_Voo();
            if (listaLanche.Count == 0)
            {
                Console.WriteLine("Não existe nenhum lanche, favor cadastrar um \n Digite algo para continuar");
                Console.ReadKey();
                return;
            }
            if (listaVoo.Count == 0)
            {
                Console.WriteLine("Não existe nenhum Voo, favor cadastrar um \n Digite algo para continuar");
                Console.ReadKey();
                return;
            }

            try
            {
                Console.WriteLine("  Selecione um voo ");
                foreach (Voo voo in listaVoo)
                {
                    Console.WriteLine( $" {voo.Id} - {voo.Origem} até {voo.Destino} no dia {voo.Decolagem.ToString("dd/MM/yyyy")} " );
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
                lancheVoo.Voo = idVoo;

                if (FileHelperLanche_Voo.select().FindAll( o => o.Voo == idVoo && o.Lanche == idLanche ).Count > 0)
                {
                    Console.WriteLine("  Lanche já inserido no voo \n Digite algo para continuar");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(" Insira a quantidade de lanche ");
                    int quantidade = int.Parse(Console.ReadLine());
                    lancheVoo.Quantidade = quantidade;
                    FileHelperLanche_Voo.Insert(lancheVoo, out string _);
                    Console.WriteLine("Lanche inserido com sucesso  \n Digite algo para continuar");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar lanche no Voo: " + ex.Message + " \n Digite algo para continuar");
                Console.ReadKey();
            }
        }

        private void cadastrarLanche()
        {
            Lanche lanche = new Lanche();
            FileHelper<Lanche> FileHelperLanche = new FileHelper<Lanche>();
            try
            {
                Console.WriteLine("Insira o nome do produto: ");
                lanche.Nome = Console.ReadLine();
                Console.WriteLine("Insira o preço do produto: "); 
                lanche.Preco = float.Parse(Console.ReadLine());
                FileHelperLanche.Insert(lanche, out string _);
                Console.WriteLine("lanche cadastrado com sucesso  \n Digite algo para continuar");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar lanche: " + ex.Message + "\n\n Digite algo para continuar");
                Console.ReadKey();
            }
        }

        public void MostrarTelaAdm()
        {
            string menu = @"
seleciona uma das opções:
    1) Cadastrar Um voo
    2) Cadastrar Lanche
    3) Adicionar Lanche para Voo
    4) mostrar todos os Voos
    5) Sair
opção: ";
            string opcao = "";
            while (opcao != "5")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
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
                        foreach (Voo item in new FileHelper<Voo>().select())
                            Console.WriteLine(item.detalhesVoo(adm: true));
                        Console.WriteLine("\n Digite algo para continuar");
                        Console.ReadKey();
                        break;
                    case "5":
                        Console.WriteLine("Logout :)");
                        break;
                    default:
                        Console.WriteLine("Opção Inválida");
                        break;
                }

            }
        }
        

        /* client actions */
        public void ComprarPassagem()
        {
            FileHelper<Voo> FileHelperVoo = new FileHelper<Voo>();
            FileHelper<Passagem> FileHelperPassagem = new FileHelper<Passagem>();
            List<Voo> listaVoo = FileHelperVoo.select().FindAll( o => o.Decolagem.AddDays(1).CompareTo(DateTime.Now) >= 0 ) ; // todos os voos que ainda estão pendentes de voar
            List<Passagem> passagensUsuario = FileHelperPassagem.select().FindAll(o => o.Cliente == Program.clienteLogado.Id); // passagens do usuario
            List<Voo> listaVooComprados = listaVoo.FindAll( voo => passagensUsuario.Exists( passagem => passagem.Voo == voo.Id) ); // voos comprados pelo usuario
            listaVoo.RemoveAll(voo => listaVooComprados.Exists( vooComprado => vooComprado.Id == voo.Id)); // tirar voos comprados do usuario
            if (listaVoo.Count > 0)
            {
                Console.WriteLine("lista de Voos disponíveis");
                foreach (Voo voo in listaVoo)
                {
                    Console.WriteLine(voo.detalhesVoo());
                }
                Console.Write("Selecione um Voo: ");
                int idVoo = int.Parse(Console.ReadLine());
                while ( !listaVoo.Exists( o => o.Id == idVoo) )
                {
                    Console.WriteLine("Digite um valor válido: ");
                    idVoo = int.Parse(Console.ReadLine());
                }
                Passagem passagem = new Passagem();
                passagem.Cliente = Program.clienteLogado.Id;
                passagem.Voo = idVoo;
                FileHelperPassagem.Insert(passagem, out string _);
            }
            else
            {
                Console.WriteLine("Nenhum Voo disponível \n digite algo para continuar");
                Console.ReadKey();
            }
        }
        public void entrarVoo()
        {
            FileHelper<Voo> FileHelperVoo = new FileHelper<Voo>();
            FileHelper<Passagem> FileHelperPassagem = new FileHelper<Passagem>();
            List<Passagem> passagensDoCliente = FileHelperPassagem.select().FindAll( passagem => passagem.Cliente == Program.clienteLogado.Id );
            List<Voo> listaDeVoo = FileHelperVoo.select(); // todos os voos
            try
            {
                List<Voo> listaVooCliente = new List<Voo>();
                foreach (Voo voo in listaDeVoo)
                {
                    if (passagensDoCliente.Exists( passagem => passagem.Voo == voo.Id))
                        listaVooCliente.Add(voo);
                }
                DateTime dt = DateTime.Now;
                listaVooCliente = listaVooCliente.FindAll( o => o.validoParaDecolar() );
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
                while (!listaVooCliente.Exists( voo => voo.Id == idVoo))
                {
                    Console.Write("Esse voo não existe, deseja escolher outro voo (S/N): ");
                    if ( Console.ReadLine().ToLower() == "n" )
                    {
                        Console.WriteLine("Digite algo para continuar");
                        Console.ReadKey();
                        return;
                    }
                    Console.Write("Digite uma opção válida: ");
                    idVoo = int.Parse(Console.ReadLine());
                }
                TelaVoo telaVoo = new TelaVoo( listaVooCliente.Find( o => o.Id == idVoo) );
                telaVoo.comecarMenu();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message + "\n\n Digite algo para continuar");
                Console.ReadKey();
            }
        }

        /* MÉTODOS DO CLIENTE */
        public void MostrarTelaCliente()
        {
            string menu = @"
seleciona uma das opções:
    1) comprar Passagem em um voo
    2) Entrar um Voo
    3) Sair
opção: ";
            string opcao = "";
            while (opcao != "3")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
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
                        Console.WriteLine("Logout :)");
                        break;
                    default:
                        Console.WriteLine("Opção Inválida");
                        break;
                }
            }
        }

    }
}
