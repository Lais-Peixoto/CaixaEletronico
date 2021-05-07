using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos
{
    public class Cliente
    {
        public string Nome { get; }
        public string Senha { get; }

        public Cliente(string nome, string senha)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O argumento nome não pode ser nulo ou em branco.", nameof(nome));
            }

            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new ArgumentException("A senha precisa ter 6 dígitos.", nameof(senha));
            }

            Nome = nome;
            Senha = senha;
        }
    }
}
