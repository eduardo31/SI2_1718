using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho.Commands
{
    class HospedeNaAtividade : ICmd
    {
        public readonly string Description;
        public HospedeNaAtividade(string desc)
        {
            Description = desc;
        }
        public override string ToString()
        {
            return Description;
        }
        public List<string> prms = new List<string>();
        public void Execute(string con)
        {
            prms = InfoGetter(prms);
            int idEstada, numAtividade, anoAtividade;
            string nIdentificacao;
            try
            {
                nIdentificacao = prms[0];
                numAtividade = Convert.ToInt32(prms[1]);
                anoAtividade = Convert.ToInt32(prms[2]);
                idEstada = Convert.ToInt32(prms[3]);
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
                        cmd.CommandText = "CreateEstada";

                        cmd.Parameters.Add("@nIdentificacao", SqlDbType.NVarChar).Value = nIdentificacao;
                        cmd.Parameters.Add("@numAtividade", SqlDbType.Int).Value = numAtividade;
                        cmd.Parameters.Add("@anoAtividade", SqlDbType.Int).Value = anoAtividade;
                        cmd.Parameters.Add("@idEstada", SqlDbType.Int).Value = idEstada;

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Inscricao do Hospede na Atividade com sucesso!");
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

        public void ExecuteEnt()
        {
            prms = InfoGetter(prms);
            using (var ctx = new GlampinhoEntities())
            {
                int idEstada, numAtividade, anoAtividade;
                string nIdentificacao;
                try
                {
                    nIdentificacao = prms[0];
                    numAtividade = Convert.ToInt32(prms[1]);
                    anoAtividade = Convert.ToInt32(prms[2]);
                    idEstada = Convert.ToInt32(prms[3]);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Alguns parametros estavam errados.");
                    return;
                }
                try
                {
                    ctx.InscreverUmHospedeNumaAtividade(nIdentificacao, numAtividade, anoAtividade, idEstada);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
                Console.WriteLine("Inscricao do Hospede na Atividade com sucesso!");
            }
        }
        private List<string> InfoGetter(List<string> list)
        {
            Console.WriteLine("Inscrever um Hospede numa Atividade");

            Console.WriteLine("Introduza o número de Identificacao do Hospede.");
            list.Add(Console.ReadLine());

            Console.WriteLine("Insira o número da Atividade.");
            list.Add(Console.ReadLine());

            Console.WriteLine("Insira o ano da Atividade.");
            list.Add(Console.ReadLine());

            Console.WriteLine("Introduza o id da Estada.");
            list.Add(Console.ReadLine());

            return list;
        }

    }
}
