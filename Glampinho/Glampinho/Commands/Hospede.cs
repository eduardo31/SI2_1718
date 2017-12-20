using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Text;

namespace Glampinho.Commands
{
    public class Hospede : ICmd
    {
        public string Description;
        public StringBuilder n = new StringBuilder();
        public List<string> list = new List<string>();
        public Hospede(string s)
        {
            n = convertToString(s);
            Description = s;
            list = InfoGetter(list);
        }
        private List<string> InfoGetter(List<string> list)
        {
            string s = n.ToString();

            switch (s)
            {
                case "1":
                    Console.WriteLine("Inserir um Hospede.");

                    Console.WriteLine("Introduza o nif do Hospede.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza o nome do Hospede.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza a morada do Hospede.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza o email do Hospede.");
                    list.Add(Console.ReadLine());
                    break;
                case "2":
                    Console.WriteLine("Atualizar um Hospede.");

                    Console.WriteLine("Introduza o nif do Hospede.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza o nome do Hospede.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza a morada do Hospede.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza o email do Hospede.");
                    list.Add(Console.ReadLine());
                    break;

                case "3":
                    Console.WriteLine("Remover um Hospede.");
                    break;
            }
            Console.WriteLine("Introduza o numero de Identificacao do Hospede.");
            list.Add(Console.ReadLine());
            return list;
        }
        public override void Execute(string con)
        {
            if (n.Equals("3")) ExecuteRemoveHospede(con);  
            
            else ExecuteInsertUpdateHospede(con);
        }
        private void ExecuteRemoveHospede(string con)
        {
            string nIdentificacao;
            try
            {
                nIdentificacao = list[0];
            }
            catch (FormatException)
            {
                Console.WriteLine("Alguns parametros estavam errados.");
                return;
            }
            using (SqlConnection sql = new SqlConnection(con))
            {
                sql.Open();
                SqlTransaction tran = sql.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = sql.CreateCommand())
                    {
                        cmd.Transaction = tran;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "DeleteHospede";

                        cmd.Parameters.Add("@nIdentificacao", SqlDbType.NVarChar).Value = nIdentificacao;

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Hospede removido com sucesso!");
                    }
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    Console.WriteLine(e.Message);
                    return;
                }
                tran.Commit();
                sql.Close();
            }
        }
        private void ExecuteInsertUpdateHospede(string con)
        {
            decimal nif;
            string nomeHospede, morada,mail,nIdentificacao;

            try
            {
                nif = Convert.ToDecimal(list[0]);
                nomeHospede = list[1];
                morada = list[2];
                mail = list[3];
                nIdentificacao = list[4];
            }
            catch (FormatException)
            {
                Console.WriteLine("Alguns parametros estavam errados.");
                return;
            }

            using (SqlConnection sql = new SqlConnection(con))
            {
                sql.Open();
                SqlTransaction tran = sql.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = sql.CreateCommand())
                    {
                        cmd.Transaction = tran;
                        cmd.CommandType = CommandType.StoredProcedure;

                        if(n.Equals("1")) cmd.CommandText = "InsertHospede";
                        else cmd.CommandText = "UpdateHospede";

                        cmd.Parameters.Add("@nIdentificacao", SqlDbType.NVarChar).Value = nIdentificacao;
                        cmd.Parameters.Add("@nif", SqlDbType.Decimal).Value = nif;
                        cmd.Parameters.Add("@nome", SqlDbType.NVarChar).Value = nomeHospede;
                        cmd.Parameters.Add("@morada", SqlDbType.NVarChar).Value = morada;
                        cmd.Parameters.Add("@mail", SqlDbType.NVarChar).Value = mail;
                     
                        cmd.ExecuteNonQuery();
                        if (n.Equals("1")) Console.WriteLine("Hospede inserido com sucesso!");
                        else Console.WriteLine("Hospede alterado com sucesso!");
                    }
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    Console.WriteLine(e.Message);
                    return;
                }
                tran.Commit();
                sql.Close();
            }
        }
        public override void ExecuteEnt()
        {
            if (n.Equals("1") || n.Equals("2")) ExecuteEntInsertUpdateHospede();

            else ExecuteEntRemoveHospede();
        }
        private void ExecuteEntInsertUpdateHospede()
        {
            decimal nif;
            string nomeHospede, morada, mail, nIdentificacao;

            try
            {
                nif = Convert.ToDecimal(list[0]);
                nomeHospede = list[1];
                morada = list[2];
                mail = list[3];
                nIdentificacao =list[4];

            }
            catch (FormatException)
            {
                Console.WriteLine("Alguns parametros estavam errados.");
                return;
            }
            using (var ctx = new GlampinhoEntities())
            {
                try
                {
                    if (n.Equals("1"))
                        ctx.InsertHospede(nIdentificacao, nif, nomeHospede, morada, mail);
                    if (n.Equals("2"))
                        ctx.UpdateHospede(nIdentificacao, nif, nomeHospede, morada, mail);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
            }
            if (n.Equals("1")) Console.WriteLine("Hospede {0}"+nIdentificacao+" introduzido com sucesso!");
            if (n.Equals("2")) Console.WriteLine("Hospede {0}" + nIdentificacao + "alterado com sucesso!");
        }
        private void ExecuteEntRemoveHospede()
        { 
            string nIdentificacao;

            try
            {
               nIdentificacao = list[0];
            }
            catch (FormatException)
            {
                Console.WriteLine("Alguns parametros estavam errados.");
                return;
            }
            using (var ctx = new GlampinhoEntities())
            {
                try
                {
                   ctx.DeleteHospede(nIdentificacao);
                   ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
            }
            Console.WriteLine("Hospede removido com sucesso!");  
        }
    }
}

