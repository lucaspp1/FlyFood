using FlyFood.DAO.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlyFood.Model
{
    public class Passagem
    {
        public int Id { get; set; }
        public int Cliente { get; set; }
        public int Voo { get; set; }

        public static void ComprarPassagem()
        {
            FileHelper<Voo> FileHelperVoo = new FileHelper<Voo>();
            FileHelper<Passagem> FileHelperPassagem = new FileHelper<Passagem>();
            List<Voo> listaVoo = FileHelperVoo.select().FindAll(o => o.Decolagem.AddDays(1).CompareTo(DateTime.Now) >= 0); // todos os voos que ainda estão pendentes de voar
            List<Passagem> passagensUsuario = FileHelperPassagem.select().FindAll(o => o.Cliente == Program.clienteLogado.Id); // passagens do usuario
            List<Voo> listaVooComprados = listaVoo.FindAll(voo => passagensUsuario.Exists(passagem => passagem.Voo == voo.Id)); // voos comprados pelo usuario
            listaVoo.RemoveAll(voo => listaVooComprados.Exists(vooComprado => vooComprado.Id == voo.Id)); // tirar voos comprados do usuario
            if (listaVoo.Count > 0)
            {
                Console.WriteLine("lista de Voos disponíveis");
                foreach (Voo voo in listaVoo)
                {
                    Console.WriteLine(voo.detalhesVoo());
                }
                Console.Write("Selecione um Voo: ");
                int idVoo = int.Parse(Console.ReadLine());
                while (!listaVoo.Exists(o => o.Id == idVoo))
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
    }
}
