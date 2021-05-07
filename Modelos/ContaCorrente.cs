using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos
{
    public class ContaCorrente
    {
        public Cliente Titular { get; set; }
        public List<TransacaoBancaria> Transacoes { get; }
        public int Agencia { get; }
        public int Numero { get; }


        private double _saldo;
        public double Saldo
        {
            get
            {
                return _saldo;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("O saldo deve ser maior que 0.");
                }
                _saldo = value;
            }
        }


        public ContaCorrente(int agencia, int numero)
        {
            if (agencia <= 0)
            {
                throw new ArgumentException("O argumento agencia deve ser maior que 0.", nameof(agencia));
            }

            if (numero <= 0)
            {
                throw new ArgumentException("O argumento numero deve ser maior que 0.", nameof(numero));
            }

            Transacoes = new List<TransacaoBancaria>();
            Agencia = agencia;
            Numero = numero;
        }


        public void Sacar(double valor)
        {
            if (valor < 0)
            {
                throw new ArgumentException("Valor inválido para o saque.", nameof(valor));
            }

            if (_saldo < valor)
            {
                throw new SaldoInsuficienteException(Saldo, valor);
            }

            _saldo -= valor;
            Transacoes.Add(new TransacaoBancaria(this, -valor, "Saque"));
        }

        public void Depositar(double valor)
        {
            _saldo += valor;
            Transacoes.Add(new TransacaoBancaria(this, valor, "Depósito"));
        }

        public void Transferir(double valor, ContaCorrente contaDestino)
        {
            if (valor < 0)
            {
                throw new ArgumentException("Valor inválido para a transferência.", nameof(valor));
            }

            if (_saldo < valor)
            {
                throw new SaldoInsuficienteException(Saldo, valor);
            }

            _saldo -= valor;
            contaDestino.Depositar(valor);
            Transacoes.Add(new TransacaoBancaria(this, -valor, "Transferência"));
        }
    }
}
