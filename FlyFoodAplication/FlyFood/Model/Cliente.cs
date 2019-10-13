using FlyFood.DAO.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlyFood.Model
{
    public enum TipoClienteEnum
    {
        ADMINISTRADOR = 0,
        CLIENTE = 1
    }

    public class Cliente
    {
        public static FileHelper<Cliente> fileHelper = new FileHelper<Cliente>();
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int Id { get; set; }
        public TipoClienteEnum TipoCliente { get; set; }
        public Cliente()
        {
            this.TipoCliente = TipoClienteEnum.CLIENTE;
        }

        // Método para verificar se existe algum cliente com o email e a senha
        public static bool RealizarLogin(string email, string senha)
        {
            List<Cliente> listaClientes = fileHelper.select(); // Busca todos os clientes no arquivo json
            List<Cliente> ClientesEncontraods = listaClientes.FindAll(o => o.Email.Equals(email) && o.Senha.Equals(senha)); // peg todos os clientes que tem o email e a senha definidas
            bool clienteEncontrado = ClientesEncontraods.Count > 0; // variavel para guardar se encontrou algum cliente
            if (clienteEncontrado)
                Program.clienteLogado = ClientesEncontraods[0]; // pega o primeiro cliente que achou na busca
            return clienteEncontrado;
        }

        public static bool RealizarCadastro(Cliente cliente)
        {
            // Realizar a lógica ainda
            return false;
        }
    }
}
