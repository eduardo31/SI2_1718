using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho.Commands
{
    class InsertAlojamento : ICmd
    {
        public readonly string Description;
        public InsertAlojamento(string desc)
        {
            Description = desc;
        }
        public override string ToString()
        {
            return Description;
        }

        public List<string> prms = new List<string>();
        public void ExecuteEnt()
        {
            prms = InfoGetter(prms);
            decimal precoBase;
            string parque, localizacao, descricao, tipo, nomeAlojamento;
            int nMaxPessoas;

            try
            {
                parque = prms[0];
                localizacao = prms[1];
                descricao = prms[2];
                precoBase = Convert.ToDecimal(prms[3]);
                nMaxPessoas = Convert.ToInt32(prms[4]);
                tipo = prms[5];
                nomeAlojamento = prms[6];
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
                    ctx.InsertAlojamento(nomeAlojamento, parque, localizacao, descricao, precoBase, nMaxPessoas, tipo);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
            }
            Console.WriteLine("Alojamento {0}" + nomeAlojamento + " inserido com sucesso!");
        }

        public void Execute(string con)
        {
            prms = InfoGetter(prms);
            decimal precoBase;
            string parque, localizacao, descricao, tipo, nomeAlojamento;
            int nMaxPessoas;

            try
            {
                parque = prms[0];
                localizacao = prms[1];
                descricao = prms[2];
                precoBase = Convert.ToDecimal(prms[3]);
                nMaxPessoas = Convert.ToInt32(prms[4]);
                tipo = prms[5];
                nomeAlojamento = prms[6];
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

                        cmd.CommandText = "InsertAlojamento";

                        cmd.Parameters.Add("@nome", SqlDbType.NVarChar).Value = nomeAlojamento;
                        cmd.Parameters.Add("@parque", SqlDbType.NVarChar).Value = parque;
                        cmd.Parameters.Add("@localizacao", SqlDbType.NVarChar).Value = localizacao;
                        cmd.Parameters.Add("@descricao", SqlDbType.NVarChar).Value = descricao;
                        cmd.Parameters.Add("@precoBase", SqlDbType.SmallMoney).Value = precoBase;
                        cmd.Parameters.Add("@nMaxPessoas", SqlDbType.Int).Value = descricao;
                        cmd.Parameters.Add("@tipo", SqlDbType.NChar).Value = precoBase;

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Alojamento inserido com sucesso!");
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

        private List<string> InfoGetter(List<string> prms)
        {
            Console.WriteLine("Inserir um Alojamento.");

            Console.WriteLine("Introduza o nome do parque.");
            prms.Add(Console.ReadLine());

            Console.WriteLine("Introduza a localizacao do Alojamento.");
            prms.Add(Console.ReadLine());

            Console.WriteLine("Introduza a descricao do Alojamento.");
            prms.Add(Console.ReadLine());

            Console.WriteLine("Introduza o precoBase do Alojamento.");
            prms.Add(Console.ReadLine());

            Console.WriteLine("Introduza o número máximo de pessoas no Alojamento.");
            prms.Add(Console.ReadLine());

            Console.WriteLine("Introduza o tipo Alojamento(T - Tendas o B - Bungalows).");
            prms.Add(Console.ReadLine());

            Console.WriteLine("Introduza o nome Alojamento.");
            prms.Add(Console.ReadLine());
            return prms;
        }
    }
}
