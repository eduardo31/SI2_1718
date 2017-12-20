using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho.Commands
{
    public class Alojamento : ICmd
    {
        public string Description;
        public StringBuilder n = new StringBuilder();
        public List<string> list = new List<string>();

        public Alojamento (string s)
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
                case "4":
                    Console.WriteLine("Inserir um Alojamento.");

                    Console.WriteLine("Introduza o nome do parque.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza a localizacao do Alojamento.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza a descricao do Alojamento.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza o precoBase do Alojamento.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza o número máximo de pessoas no Alojamento.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza o tipo Alojamento(T - Tendas o B - Bungalows).");
                    list.Add(Console.ReadLine());
                    break;

                case "5":
                    Console.WriteLine("Atualizar um Alojamento.");

                    Console.WriteLine("Introduza o nome do parque.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza a localizacao do Alojamento.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza a descricao do Alojamento.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza o precoBase do Alojamento.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza o número máximo de pessoas no Alojamento.");
                    list.Add(Console.ReadLine());

                    Console.WriteLine("Introduza o tipo Alojamento(T - Tendas o B - Bungalows).");
                    list.Add(Console.ReadLine());
                    break;
                case "6":
                    Console.WriteLine("Remover um Alojamento.");
                    break;
            }
            Console.WriteLine("Introduza o nome Alojamento.");
            list.Add(Console.ReadLine());
            return list;
        }
        public override void Execute(string con)
        {
            if (n.Equals("6")) ExecuteRemoveAlojamento(con);
            else  ExecuteInsertUpdateAlojamento(con);            
        }
        private void ExecuteRemoveAlojamento(string con)
        {
            string nomeAlojamento;            
            try
            {
                nomeAlojamento = list[0];
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
                        cmd.CommandText = "DeleteAlojamento";

                        cmd.Parameters.Add("@nome", SqlDbType.NVarChar).Value = nomeAlojamento;

                        cmd.ExecuteNonQuery();

                        Console.WriteLine("Alojamento removido com sucesso!");
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

        private void ExecuteInsertUpdateAlojamento(string con)
        {
            decimal precoBase;
            string parque, localizacao, descricao,tipo,nomeAlojamento;
            int nMaxPessoas;

            try
            {
                parque = list[0];
                localizacao = list[1];
                descricao = list[2];
                precoBase = Convert.ToDecimal(list[3]);
                nMaxPessoas = Convert.ToInt32(list[4]);
                tipo = list[5];
                nomeAlojamento = list[6];
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

                        if(n.Equals("4")) cmd.CommandText = "InsertAlojamento";
                        else cmd.CommandText = "UpdateAlojamento";

                        cmd.Parameters.Add("@nome", SqlDbType.NVarChar).Value = nomeAlojamento;
                        cmd.Parameters.Add("@parque", SqlDbType.NVarChar).Value = parque;
                        cmd.Parameters.Add("@localizacao", SqlDbType.NVarChar).Value = localizacao;
                        cmd.Parameters.Add("@descricao", SqlDbType.NVarChar).Value = descricao;
                        cmd.Parameters.Add("@precoBase", SqlDbType.SmallMoney).Value = precoBase;
                        cmd.Parameters.Add("@nMaxPessoas", SqlDbType.Int).Value = descricao;
                        cmd.Parameters.Add("@tipo", SqlDbType.NChar).Value = precoBase;

                        cmd.ExecuteNonQuery();
                        if (n.Equals("4")) Console.WriteLine("Alojamento inserido com sucesso!");
                        else Console.WriteLine("Alojamento atualizado com sucesso!");                       
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
            if (n.Equals("6")) ExecuteEntRemoveAlojamento();
            else ExecuteInsertUpdateEntAlojamento();
        }
        private void ExecuteInsertUpdateEntAlojamento()
        {
            decimal precoBase;
            string parque, localizacao, descricao, tipo,nomeAlojamento;
            int nMaxPessoas;

            try
            {
                parque = list[0];
                localizacao = list[1];
                descricao = list[2];
                precoBase = Convert.ToDecimal(list[3]);
                nMaxPessoas = Convert.ToInt32(list[4]);
                tipo = list[5];
                nomeAlojamento = list[6];
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
                    if (n.Equals("4")) ctx.InsertAlojamento(nomeAlojamento,parque,localizacao,descricao,precoBase,nMaxPessoas,tipo);
                    else ctx.UpdateAlojamento(nomeAlojamento, parque, localizacao, descricao, precoBase, nMaxPessoas, tipo);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
            }
            if (n.Equals("4")) Console.WriteLine("Alojamento {0}"+nomeAlojamento+" inserido com sucesso!");
            else Console.WriteLine("Alojamento {0}" + nomeAlojamento + " atualizado com sucesso!");
        }
        private void ExecuteEntRemoveAlojamento()
        {
            string nome;
            try
            {
                nome = list[0];
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
                    ctx.DeleteAlojamento(nome);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
            }
            Console.WriteLine("Alojamento eliminada com sucesso!");
        }
    }
}
