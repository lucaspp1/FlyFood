using FlyFood.DAO.Service;
using FlyFood.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace FlyFood
{
    class Program
    {
        public static Cliente clienteLogado = null;

        public static int EscolhaInicial()
        {
            string menu = @"
seleciona uma das opções:
    1) Login no sistema
    2) Cadastro no sistema
    3) Sair
opção: ";
            Console.Write(menu);
            try
            {
                int decisao = int.Parse( Console.ReadLine() );
                return decisao;
            }
            catch (FormatException)
            {
                return -1;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static void ChamarTelaCliente()
        {
            TelaCliente tela = new TelaCliente();
            if (clienteLogado.TipoCliente == TipoClienteEnum.CLIENTE)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                tela.MostrarTelaCliente();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.Cyan;
                tela.MostrarTelaAdm();
            }
        }

        public static void Login()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Insira seu email: ");
                string email = Console.ReadLine();
                Console.Write("Insira sua senha: ");
                string senha = Console.ReadLine();
                bool loginSucesso = Cliente.RealizarLogin(email, senha);
                if (loginSucesso)
                {
                    ChamarTelaCliente();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Login Incorreto");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Impossivel realizar login: \nerro: "+e.Message);
            }
        }

        public static void Cadastro()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Cliente cliente = new Cliente();
            try
            {
                Console.Write("Digite o seu nome: ");
                cliente.Nome = Console.ReadLine();
                Console.Write("Digite o seu email: ");
                cliente.Email = Console.ReadLine();
                Console.Write("Digite a sua senha: ");
                cliente.Senha = Console.ReadLine();
                bool resultado = Cliente.RealizarCadastro(cliente);
                if (resultado)
                {
                    Console.WriteLine("Vc foi cadastrado com sucesso. ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Deu ruin man");
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Erro na obtenção de dados");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            cliente.TipoCliente = TipoClienteEnum.CLIENTE;
        }

        static void Main(string[] args)
        {
            int resultado = 0;
            while(resultado != 3)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                resultado = EscolhaInicial();
                switch (resultado)
                {
                    case 1:
                        Login();
                        break;
                    case 2:
                        Cadastro();
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("Obrigado por usar o ");
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write("FlyFood ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(":) \n");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opcao inválida, tente novamente");
                        break;
                }
                Console.WriteLine("pressione uma tecla para continuar ... ");
                Console.ReadKey();
                Console.Clear();

            }
        }
    }
}
