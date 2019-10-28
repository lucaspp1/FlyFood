using FlyFood.DAO.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlyFood.Model
{
    public class Lanche_Voo
    {
        public int Lanche { get; set; }
        public int Voo { get; set; }
        public int Quantidade { get; set; }
        public int Id { get; set; }


        public static void InserirLancheVoo()
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
                    Console.WriteLine($" {voo.Id} - {voo.Origem} até {voo.Destino} no dia {voo.Decolagem.ToString("dd/MM/yyyy")} ");
                }
                int idVoo = int.Parse(Console.ReadLine());
                while (listaVoo.FindAll(o => o.Id == idVoo).Count <= 0)
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

                if (FileHelperLanche_Voo.select().FindAll(o => o.Voo == idVoo && o.Lanche == idLanche).Count > 0)
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

    }
}
