using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho.Commands
{
    class InsertHospede : ICmd
    {
        public readonly string Description;
        public InsertHospede(string desc)
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
            decimal nif;
            string nomeHospede, morada, mail, nIdentificacao;

            try
            {
                nif = Convert.ToDecimal(prms[0]);
                nomeHospede = prms[1];
                morada = prms[2];
                mail = prms[3];
                nIdentificacao = prms[4];
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

                        cmd.CommandText = "InsertHospede";

                        cmd.Parameters.Add("@nIdentificacao", SqlDbType.NVarChar).Value = nIdentificacao;
                        cmd.Parameters.Add("@nif", SqlDbType.Decimal).Value = nif;
                        cmd.Parameters.Add("@nome", SqlDbType.NVarChar).Value = nomeHospede;
                        cmd.Parameters.Add("@morada", SqlDbType.NVarChar).Value = morada;
                        cmd.Parameters.Add("@mail", SqlDbType.NVarChar).Value = mail;

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Hospede inserido com sucesso!");
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
            decimal nif;
            string nomeHospede, morada, mail, nIdentificacao;

            try
            {
                nif = Convert.ToDecimal(prms[0]);
                nomeHospede = prms[1];
                morada = prms[2];
                mail = prms[3];
                nIdentificacao = prms[4];

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
                    ctx.InsertHospede(nIdentificacao, nif, nomeHospede, morada, mail);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return;
                }
            }
            Console.WriteLine("Hospede {0}" + nIdentificacao + " introduzido com sucesso!");
        }
        private List<string> InfoGetter(List<string> prms)
        {
            Console.WriteLine("Inserir um hospede da basedados.");

            Console.WriteLine("Seleccionar nif do hospede:");
            prms.Add(Console.ReadLine());
            Console.WriteLine("Seleccionar nome do hospede:");
            prms.Add(Console.ReadLine());
            Console.WriteLine("Seleccionar morada do hospede:");
            prms.Add(Console.ReadLine());
            Console.WriteLine("Seleccionar mail do hospede:");
            prms.Add(Console.ReadLine());
            Console.WriteLine("Seleccionar nIdentificacao do hospede:");
            prms.Add(Console.ReadLine());

            return prms;
        }
    }
}
