using FlyFood.DAO.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlyFood.Model
{
    public class Adm : Usuario
    {
        public static FileHelper<Adm> fileHelper = new FileHelper<Adm>();

        public Adm()
        {
            
        }

        public Adm(string email, string senha, int id) :
        base(email, senha, id)
        {
            
        }

        public static bool RealizarLogin(string email, string senha)
        {
            List<Adm> listaClientes = fileHelper.select(); // Busca todos os clientes no arquivo json
            List<Adm> ClientesEncontrados = listaClientes.FindAll(o => o.Email.Equals(email) && o.Senha.Equals(senha)); // peg todos os clientes que tem o email e a senha definidas
            bool clienteEncontrado = ClientesEncontrados.Count > 0; // variavel para guardar se encontrou algum cliente
            if (clienteEncontrado)
                Program.clienteLogado = ClientesEncontrados[0]; // pega o primeiro cliente que achou na busca
            return clienteEncontrado;

        }

        public static bool RealizarCadastro(Adm cliente)
        {
            List<Adm> listaUsuario = fileHelper.select(); // pego todos os usuario da arquivo

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
    }
}