using Glampinho.Exceptions;
using Glampinho.Commands;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Glampinho
{
    public class Program
    {
        private static List<ICmd> SetupCommands()
        {
            List<ICmd> cmds = new List<ICmd>();

            cmds.Add(new InsertHospede("1. Inserir um Hóspede."));
            cmds.Add(new UpdateHospede("2. Atualizar a informação de um Hóspede."));
            cmds.Add(new RemoveHospede("3. Remover um Hóspede."));

            cmds.Add(new InsertAlojamento("4. Inserir um Alojamento."));
            cmds.Add(new UpdateAlojamento("5. Atualizar a informação de um Alojamento."));
            cmds.Add(new RemoveAlojamento("6. Remover um Alojamento."));

            /* cmds.Add(new Commands.ExtraAloj("7. Inserir um Extra de Alojamento."));
             cmds.Add(new Commands.ExtraAloj("8. Atualizar a informação de um Extra de Alojamento."));
             cmds.Add(new Commands.ExtraAloj("9. Remover um Extra de Alojamento."));

             cmds.Add(new Commands.ExtraPessoal("10. Inserir um Extra Pessoal."));
             cmds.Add(new Commands.ExtraPessoal("11. Atualizar a informação de um Extra Pessoal."));
             cmds.Add(new Commands.ExtraPessoal("12. Remover um Extra Pessoal."));

             cmds.Add(new Commands.Atividade("13. Inserir uma Atividade."));
             cmds.Add(new Commands.Atividade("14. Atualizar a informação de uma Atividade."));
             cmds.Add(new Commands.Atividade("14. Remover uma Atividade."));

             cmds.Add(new Commands.Estada("15.Criar uma Estada."));
             cmds.Add(new Commands.HospedeNaAtividade("16. Inserção de um Hospede numa Atividade."));*/
            //cmds.Add(new Commands.Fatura("17. Emissão de uma fatura."));
            //  cmds.Add(new Commands.EnviarEmail("18. Enviar email a todos os hospedes responsaveis."));
            //  cmds.Add(new Commands.ListaLugaresDisponiveis("19. Listagem de Atividades com lugares disponiveis."));
            int last = cmds.Count + 1;
            cmds.Add(new ExitCmd(last + ". Fechar a aplicaçao."));

            return cmds;
        }

        public static void Main(string[] args)
        {
            bool ado;

            Console.WriteLine("Bem-vindo à Empresa Glampinho!");

            Console.WriteLine("Insira 'A' se deseja ADO.NET ou 'EF' se deseja Entity Framework.");

            string s = Console.ReadLine();

            if (s.Equals("A")) ado = true;
            else if (s.Equals("EF")) ado = false;
            else
            {
                Console.WriteLine("Escolha invalida e omissao será usado ADO.NET");
                ado = true;
            }
            List<ICmd> cmds = SetupCommands();
            ICmd cmd;

            while (true)
            {
                PrintCommands(cmds);

                int value = 0;

                try
                {
                    value = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Insira um numero do commando...");
                    continue;
                }
                if (value > cmds.Count || value <= 0)
                {
                    try
                    {
                        throw new MismatchedCommand("Commando invalido tente novamente....");
                    }
                    catch (MismatchedCommand e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }
                cmd = cmds[value - 1];

                string connectionString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;

                Console.Clear();

                if (ado) cmd.Execute(connectionString);

                else cmd.ExecuteEnt();


                Console.WriteLine("\nContinue.");

                Console.ReadKey();

                Console.Clear();    
            }
        }
       
        private static void PrintCommands(List<ICmd> cmds)
        {
            Console.WriteLine("Pick a command:");
            cmds.ForEach(Console.WriteLine);
        }
   } 
}

