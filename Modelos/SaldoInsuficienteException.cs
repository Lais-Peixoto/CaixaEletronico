using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos
{
    public class SaldoInsuficienteException : OperacaoFinanceiraException
    {
        public double Saldo { get; }
        public double ValorSaque { get; }
        public SaldoInsuficienteException()
        {
        }
        public SaldoInsuficienteException(string mensagem)
            : base(mensagem)
        {
        }
        public SaldoInsuficienteException(string mensagem, Exception excecaoInterna)
            : base(mensagem, excecaoInterna)
        {
        }
        public SaldoInsuficienteException(double saldo, double valorSaque)
           : this($"Tentativa de saque no valor de {valorSaque} em uma conta com saldo de {saldo}.")
        {
            Saldo = saldo;
            ValorSaque = valorSaque;
        }
    }
}
