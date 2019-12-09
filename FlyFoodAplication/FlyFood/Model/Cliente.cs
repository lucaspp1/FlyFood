using FlyFood.DAO.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlyFood.Model
{
    public class Cliente : Usuario
    {
        public static FileHelper<Cliente> fileHelper = new FileHelper<Cliente>();
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public char Genero { get; set; }
        public DateTime Nascimento { get; set; }
        public Cliente() : base()
        {
                
        }

        public Cliente(string nome, string email, string senha, int id, string cpf, char genero, DateTime nascimento) :
        base(email, senha, id)
        {
            this.Nome = nome;
            this.Cpf = cpf;
            this.Genero = genero;
            this.Nascimento = nascimento;
        }

        // Método para verificar se existe algum cliente com o email e a senha
        public static bool RealizarLogin(string email, string senha)
        {
            List<Cliente> listaClientes = fileHelper.select(); // Busca todos os clientes no arquivo json
            List<Cliente> ClientesEncontrados = listaClientes.FindAll(o => o.Email.Equals(email) && o.Senha.Equals(senha)); // peg todos os clientes que tem o email e a senha definidas
            bool clienteEncontrado = ClientesEncontrados.Count > 0; // variavel para guardar se encontrou algum cliente
            if (clienteEncontrado)
                Program.clienteLogado = ClientesEncontrados[0]; // pega o primeiro cliente que achou na busca
            return clienteEncontrado;

        }

        public static bool RealizarCadastro(Cliente cliente)
        {
            List<Cliente> listaUsuario = fileHelper.select(); // pego todos os usuario da arquivo

            bool existeEmail = false;
            for (int i = 0; i < listaUsuario.Count; i++) // percorrer cada usuario
            {
                if (listaUsuario[i].Email == cliente.Email) // verificar se o email do usuario existe em algum usuario da lista
                {
                    existeEmail = true;
                }
            }
            if (existeEmail) // verificar condicao acima
            {
                return false;
            }
            else
            {
                bool resultadoInsercao = fileHelper.Insert(cliente, out string _); // inserir usuario
                return resultadoInsercao;
            }

        }
        public void MostrarPerfil()
        {
            Console.WriteLine("Nome: " + this.Nome);
            Console.WriteLine("CPF: " + this.Cpf);
            Console.WriteLine("Gênero: " + this.Genero);
            Console.WriteLine("Data de Nascimento: " + this.Nascimento.ToString("dd/MM/yyyy"));

            List<Passagem> passagens = new FileHelper<Passagem>().select();
            List<Voo> voos = new FileHelper<Voo>().select();

            foreach (Passagem item in passagens)
            {
                if (item.Cliente == this.Id)
                {
                    foreach (Voo passagemVoo in voos)
                    {
                        if (passagemVoo.Id == item.Voo)
                        {
                            Console.WriteLine("Id:" + passagemVoo.Id);
                            Console.WriteLine("Origem:" + passagemVoo.Origem);
                            Console.WriteLine("Destino:" + passagemVoo.Destino);
                            Console.WriteLine("Companhia:" + passagemVoo.Companhia);
                        }
                    }
                }
                
            }
        }

    }
}
