using Modelos;
using System;
using System.Collections.Generic;
using System.IO;

namespace CaixaEletronico
{
    class Program
    {
        static void Main(string[] args)
        {
            Caixa();
        }


        static void Caixa()
        {
            Console.WriteLine("\n\n\t\t\tBem-vindo(a) ao ByteBank!\n");
            Console.WriteLine("Digite o seu número da conta e senha para ter acesso ao Menu de Opções. \n");

            Console.WriteLine("Número da conta:");
            var numeroDaConta = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Senha:");
            var senha = Console.ReadLine();

            Console.Clear();

            SistemaInterno caixa = new SistemaInterno();
            var conta = caixa.AcessarConta(numeroDaConta, senha);

            CaixaContaAcessada(conta, caixa);
        }
        static void Menu ()
        {
            Console.WriteLine("\t{0,-20}\n", " Menu:");
            Console.WriteLine("\t - {0,-20}", Opcoes.Sacar);
            Console.WriteLine("\t - {0,-20}", Opcoes.Depositar);
            Console.WriteLine("\t - {0,-20}", Opcoes.Transferir);
            Console.WriteLine("\t - {0,-20}", Opcoes.Extrato);
            Console.WriteLine("\t - {0,-20}", Opcoes.Sair);
        }
        public enum Opcoes
        {
            Sacar = 1,
            Depositar = 2,
            Transferir = 3,
            Extrato = 4,
            Sair= 5
        }
        static void CaixaContaAcessada(ContaCorrente conta, SistemaInterno caixa)
        {
            if (conta != null)
            {
                Console.WriteLine($"\n\n\t\t Olá, {conta.Titular.Nome}!");
                var dataCorrente = DateTime.Now;
                Console.WriteLine($"\n\t\t {dataCorrente} \n");
                Console.WriteLine("---------------------------------------------- \n");

                Console.WriteLine($"\t Saldo: R$ {conta.Saldo},00 \n");
                Console.WriteLine("---------------------------------------------- \n");

                Menu();

                Console.WriteLine("\n\t Informe uma opção: \n");
                Opcoes opcaoEscolhidaPeloUsuario = Enum.Parse<Opcoes>(Console.ReadLine());

                Console.Clear();

                OpcaoEscolhida(opcaoEscolhidaPeloUsuario, conta, caixa);
            }
        }
        static void OpcaoEscolhida(Opcoes opcao, ContaCorrente conta, SistemaInterno caixa)
        {
            switch (opcao)
            {
                case Opcoes.Sacar:
                    Console.WriteLine("\t\n\n Valor:");
                    var valor = double.Parse(Console.ReadLine());
                    conta.Sacar(valor);

                    Console.Clear();
                    Console.WriteLine("\t\n\n Operação concluída com sucesso");
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();

                    CaixaContaAcessada(conta, caixa);
                    break;

                case Opcoes.Depositar:
                    Console.WriteLine("\t\n\n Valor:");
                    valor = double.Parse(Console.ReadLine());
                    conta.Depositar(valor);

                    Console.Clear();
                    Console.WriteLine("\t\n\n Operação concluída com sucesso");
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();

                    CaixaContaAcessada(conta, caixa);
                    break;

                case Opcoes.Transferir:
                    Console.WriteLine("\t\n Valor:");
                    valor = double.Parse(Console.ReadLine());

                    Console.WriteLine("\t\n Agência:");
                    var agencia = int.Parse(Console.ReadLine());
                    Console.WriteLine("\t\n Número da conta:");
                    var numero = int.Parse(Console.ReadLine());

                    var contaDestino = caixa.EncontrarConta(agencia, numero);

                    Console.WriteLine($"\n Deseja transferir R$ {valor},00 para {contaDestino.Titular.Nome}? S/N");
                    var transfere = Console.ReadLine();

                    if (transfere == "S")
                    {
                        conta.Transferir(valor, contaDestino);

                        Console.Clear();
                        Console.WriteLine("\t\n\n Operação concluída com sucesso");
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();

                        CaixaContaAcessada(conta, caixa);
                    }
                    else
                    {
                        Console.WriteLine("Operação cancelada pelo usuário.");
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        CaixaContaAcessada(conta, caixa);
                    }
                    break;

                case Opcoes.Extrato:

                    var transacoesBancariasDoUsuario = conta.Transacoes;

                    Console.WriteLine("\n\t{0,-20} {1,20}\n", "Transação", "");
                    foreach (var transacao in transacoesBancariasDoUsuario)
                    {
                        Console.WriteLine("\t{0,-20} ------------ {1,20}", transacao.Tipo, $"R$ {transacao.Valor},00");
                    }
                    Console.WriteLine("\t{0,-20} ------------ {1,20}\n", "Saldo Atual", $"R$ {conta.Saldo},00");
                    Console.WriteLine("Voltar ao Menu de Opções? S/N");
                    var voltarAoMenu = Console.ReadLine();
                    if (voltarAoMenu == "S")
                    {
                        Console.Clear();
                        CaixaContaAcessada(conta, caixa);
                    }
                    else
                    {
                        OpcaoEscolhida(Opcoes.Sair, conta, caixa);
                    }
                    break;

                case Opcoes.Sair:
                    Console.Clear();
                    Console.WriteLine("\n\n\t\tOperação finalizada.\n\n\t\tVolte sempre!");
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    Caixa();
                    break;

                default:
                    break;
            }

        }
    }
}
