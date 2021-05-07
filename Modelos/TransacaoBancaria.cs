using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos
{
    public class TransacaoBancaria
    {
        public ContaCorrente Conta { get; }
        public double Valor { get; set; }
        public string Tipo { get; set; }

        public TransacaoBancaria(ContaCorrente conta, double valor, string tipo)
        {
            Conta = conta;
            Valor = valor;
            Tipo = tipo;
        }
    }
}
