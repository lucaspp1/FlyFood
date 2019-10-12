using FlyFood.DAO.Service;
using FlyFood.Model;
using System;
using System.Collections.Generic;

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
                        break;
                    case 2:
                        break;
                    case 3:
                        Console.WriteLine("Obrigado por usar o FlyFood S2");
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
