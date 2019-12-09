using FlyFood.DAO.Service;
using FlyFood.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace FlyFood
{
    class Program
    {
        public static Usuario clienteLogado = null;

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

        public static void ChamarTela(Boolean isAdm)
        {
            TelaCliente tela = new TelaCliente();
            if (isAdm)
                tela.MostrarTelaAdm();
            else
                tela.MostrarTelaCliente();
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
                    ChamarTela(isAdm: false);
                }
                else
                {
                    if(Adm.RealizarLogin(email, senha)){
                        ChamarTela(isAdm: true);    
                    }
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
                Console.Write("Digite o seu Cpf: ");
                cliente.Cpf = Console.ReadLine();
                Console.Write("Digite a sua data de nascimento: ");
                cliente.Nascimento = DateTime.Parse(Console.ReadLine());
                Console.Write("Digite o seu gênero (M/F): ");
                cliente.Genero = char.Parse(Console.ReadLine());

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
        }

        static void Main(string[] args)
        {
            if ( new FileHelper<Adm>().select().Count == 0 )
            {
                Adm adm = new Adm("adm", "adm", 1);
                new FileHelper<Adm>().Insert(adm, out string _);
            }
            int resultado = 0;
            while(resultado != 3)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
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
