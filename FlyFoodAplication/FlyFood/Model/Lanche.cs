using FlyFood.DAO.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlyFood.Model
{
    public class Lanche
    {
        public string Nome { get; set; }
        public float Preco { get; set; }
        public int Id { get; set; }


        public static void cadastrarLanche()
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
    }
}
