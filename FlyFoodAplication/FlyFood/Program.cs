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
            if (clienteLogado.TipoCliente == TipoClienteEnum.CLIENTE)
            {
                // chamar tela do cliente
                Console.WriteLine("Tela do cliente");
            }
            else
            {
                Console.WriteLine("Tela do Administrador");
                // chamar tela do adiministrador
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
            // Realizar Lógica


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
                        Console.Write("S2 \n");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        break;
                    default:
                        Console.WriteLine("Opcao invalida, tente novamente");
                        break;
                }
                Console.WriteLine("pressione uma tecla para continuar ... ");
                Console.ReadKey();
                Console.Clear();

            }
        }
    }
}
