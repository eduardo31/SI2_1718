using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glampinho.Commands
{
    class RemoveParque : ICmd
    {
        public readonly string Description;
        public RemoveParque(string desc)
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
            string parque;

            try
            {
                parque = prms[0];
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
                        cmd.CommandText =   "USE Glampinho; "+
                                            "declare @nome varchar(100) "+
                                            "set @nome = '"+ parque + "' "+
                                            "update Atividade set parque = null where parque = @nome "+
                                            "declare curs cursor for select nome from Alojamento WHERE parque = @nome "+
                                            "for update open curs declare @nomealoj varchar(100)fetch next from curs into @nomealoj "+
                                            "while (@@FETCH_STATUS = 0) "+
                                            "begin "+
                                                "delete from HistoricoAloj WHERE alojamento = @nomealoj "+
                                                "delete from Tendas WHERE nomeAlojamento = @nomealoj "+
                                                "delete from Bungalows WHERE nomeAlojamento = @nomealoj "+
                                                "declare curs1 cursor for select id from EstAlojExtra where alojamento = @nomealoj "+
                                                "for update open curs1 declare @id int fetch next from curs1 into @id "+
                                                "while (@@FETCH_STATUS = 0) "+
                                                "begin "+
                                                    "update Fatura set idEstada = null where idEstada = @id "+
                                                    "delete from HospEstAti where id = @id "+
                                                    "delete from HospEst where id = @id "+
                                                    "delete from EstAlojExtra where alojamento = @nomealoj "+
                                                    "delete from Estada where id = @id "+
                                                "fetch next from curs1 into @id "+
                                                "end "+
                                                "close curs1 "+
                                                "deallocate curs1 "+
                                            "fetch next from curs into @nomealoj "+
                                            "end "+
                                            "close curs "+
                                            "deallocate curs "+
                                            "declare curs2 cursor for select nIdentificacao from Hospede "+
                                            "for update open curs2 declare @idhosp varchar(100) fetch next from curs2 into @idhosp "+
                                            "while (@@FETCH_STATUS = 0) "+
                                            "begin "+
                                                "IF NOT EXISTS (SELECT nIdentificacao FROM HospEst WHERE nIdentificacao = @idhosp) "+
                                                    "update Hospede set exist = 'F' where nIdentificacao = @idhosp "+
                                                "fetch next from curs2 into @idhosp "+
                                            "end "+
                                            "close curs2 "+
                                            "deallocate curs2 "+
                                            "delete from dbo.Alojamento where parque = @nome "+
                                            "delete from dbo.Telefone where parque = @nome "+
                                            "delete from dbo.ParqueCampismo where nome = @nome ";
                        cmd.Transaction = tran;
                        cmd.ExecuteNonQuery();

                        Console.WriteLine("Parque "+parque+" apagado.");
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
            throw new NotImplementedException();
        }
        private List<string> InfoGetter(List<string> list)
        {
            Console.WriteLine("Nome do Parque a ser apagado:");
            list.Add(Console.ReadLine());
            return list;
        }
    }
}
