using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Modelos
{
    public class SistemaInterno
    {
        public ContaCorrente EncontrarConta(int agencia, int numero)
        {
            var contas = ContasDoBanco();
            var contaProcurada = contas.Where(conta => conta.Numero == numero && conta.Agencia == agencia).Single();
            return contaProcurada;
        }

        public ContaCorrente AcessarConta(int numero, string senha)
        {
            var contas = ContasDoBanco();
            var contaAcessada = contas.Where(conta => conta.Numero == numero && conta.Titular.Senha == senha).Single();
            return contaAcessada;
        }

        protected internal List<ContaCorrente> ContasDoBanco()
        {
            string enderecoDoArquivo = "contas.txt";
            List<ContaCorrente> contas = new List<ContaCorrente>();

            using (FileStream fs = new FileStream(enderecoDoArquivo, FileMode.Open) )
            using (var leitor = new StreamReader(fs))
            {
                while (!leitor.EndOfStream)
                {
                    var linha = leitor.ReadLine();
                    string[] campos = linha.Split(",");

                    var nome = campos[0];
                    var agencia = campos[1];
                    var numero = campos[2];
                    var senha = campos[3];
                    var saldo = campos[4].Replace(".",",");

                    var agenciaInt = int.Parse(agencia);
                    var numeroInt = int.Parse(numero);
                    var saldoDouble = double.Parse(saldo);

                    var titular = new Cliente(nome, senha);
                    var conta = new ContaCorrente(agenciaInt, numeroInt)
                    {
                        Titular = titular,
                        Saldo = saldoDouble
                    };
                    contas.Add(conta);
                }
            }
            return contas;
        }
    }
}
