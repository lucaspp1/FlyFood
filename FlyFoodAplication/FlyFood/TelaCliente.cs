using FlyFood.DAO.Service;
using FlyFood.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlyFood
{
    class TelaCliente
    {
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
                        Voo.CadastrarVoo();
                        break;
                    case "2":
                        Lanche.cadastrarLanche();
                        break;
                    case "3":
                        Lanche_Voo.InserirLancheVoo();
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

        /* MÉTODOS DO CLIENTE */
        public void MostrarTelaCliente()
        {
            string menu = @"
seleciona uma das opções:
    1) comprar Passagem em um voo
    2) Entrar um Voo
    3) Perfil
    4) Sair
opção: ";
            string opcao = "";
            while (opcao != "4")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Clear();
                Console.WriteLine(menu);
                opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        Passagem.ComprarPassagem();
                        break;
                    case "2":
                        Voo.EntrarVoo();
                        break;
                    case "3":
                        ((Cliente)Program.clienteLogado).MostrarPerfil();
                        break;
                    case "4":
                        Console.WriteLine("Logout");
                        break;
                    default:
                        Console.WriteLine("Opção Inválida");
                        break;
                }
                Console.WriteLine("pressione uma tecla para continuar ... ");
                Console.ReadKey();
            }
        }

    }
}
