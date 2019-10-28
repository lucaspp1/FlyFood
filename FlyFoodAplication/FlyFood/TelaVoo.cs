using FlyFood.DAO.Service;
using FlyFood.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyFood
{
    class TelaVoo
    {
        public Voo voo { get; set; }

        public TelaVoo(Voo voo)
        {
            this.voo = voo;
        }

        private int EscolhaInicial()
        {
            string menu = @"
Escolhe uma das opções:
    1) pedir lanche
    2) sair
opção: ";
            Console.Write(menu);
            try
            {
                int decisao = int.Parse(Console.ReadLine());
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

        private void EscolherLanche()
        {
            FileHelper<Lanche> fileHelperLanche = new FileHelper<Lanche>();
            List<Lanche> listaLanche = fileHelperLanche.select(); // lista de todos os lanches
            FileHelper<Lanche_Voo> fileHelperLanche_Voo = new FileHelper<Lanche_Voo>();
            List<Lanche_Voo> listaLancheVoo = fileHelperLanche_Voo.select().FindAll(o => o.Voo == voo.Id && o.Quantidade > 0); // pega todos os lanches disponiveis do voo
            if (listaLancheVoo.Count > 0) // verificar se o voo ainda tem lanche
            {
                var lanchesDoVoo = (from x in listaLanche
                                             where listaLancheVoo.Exists( o => o.Lanche == x.Id)
                                             select new {
                                                 lanche = x,
                                                 quantidade = listaLancheVoo.Find(o => o.Lanche == x.Id).Quantidade,
                                                 id = listaLancheVoo.Find(o => o.Lanche == x.Id).Id,
                                             }).ToList();
                if (lanchesDoVoo.Count == 0)
                {
                    Console.WriteLine(" Não existe lanche para esse voo \n\n Digite algo para continuar");
                    Console.ReadKey();
                    return;
                }
                foreach (var item in lanchesDoVoo)
                    Console.WriteLine($" {item.id} - {item.lanche.Nome} \n      quantidade: {item.quantidade}");
                Console.WriteLine($" {lanchesDoVoo.Max( o => o.id) + 1} - sair");
                Console.Write("Escolha um dos lanches : ");
                try
                {
                    int resultado = int.Parse(Console.ReadLine());
                    int maxResult = lanchesDoVoo.Max(o => o.id) + 1;
                    while (maxResult != resultado && !lanchesDoVoo.Exists( o => o.id == resultado) )
                    {
                        Console.Write("Insira uma opção válida: ");
                        resultado = int.Parse(Console.ReadLine());
                    }
                    if (maxResult == resultado)
                    {
                        Console.WriteLine("\n Digite algo para continuar");
                        Console.ReadKey();
                        return;
                    }
                    int maxQtd = lanchesDoVoo.Find(o => o.id == resultado).quantidade;
                    Console.Write($"\n Insira a quantidade do lanche \n     (quantidade máxima: {maxQtd}): ");
                    int quantidade = int.Parse( Console.ReadLine() );
                    while (quantidade < 0 || quantidade > maxQtd)
                    {
                        Console.Write("Insira uma quantidade válida: ");
                        quantidade = int.Parse(Console.ReadLine());
                    }
                    Lanche_Voo lanche_Voo = listaLancheVoo.Find( obj => obj.Id == lanchesDoVoo.Find(o => o.id == resultado).id );
                    lanche_Voo.Quantidade -= quantidade;
                    fileHelperLanche_Voo.Update(lanche_Voo, lanche_Voo.Id);
                    Console.WriteLine("lanche comprado com sucesso \n Digite algo para continuar");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao pedir lanche: " + ex.Message + "\n\n Digite algo para continuar");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Esse voo nao tem lanches disponiveis. \n Digite algo para continuar");
                Console.ReadKey();
            }

        }

        public void comecarMenu()
        {
            int resultado = -1;
            while(resultado != 2)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Clear();
                resultado = EscolhaInicial();
                if (resultado == 1)
                    EscolherLanche();
                else if(resultado != 2)
                    Console.WriteLine("Escolha inválida");
            }
        }
    }
}
