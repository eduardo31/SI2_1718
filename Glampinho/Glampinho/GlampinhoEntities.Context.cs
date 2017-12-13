﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Glampinho
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class GlampinhoEntities : DbContext
    {
        public GlampinhoEntities()
            : base("name=GlampinhoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Alojamento> Alojamento { get; set; }
        public virtual DbSet<Atividade> Atividade { get; set; }
        public virtual DbSet<Bungalows> Bungalows { get; set; }
        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<Estada> Estada { get; set; }
        public virtual DbSet<EstAlojExtra> EstAlojExtra { get; set; }
        public virtual DbSet<Extra> Extra { get; set; }
        public virtual DbSet<ExtraAloj> ExtraAloj { get; set; }
        public virtual DbSet<ExtraPessoa> ExtraPessoa { get; set; }
        public virtual DbSet<Fatura> Fatura { get; set; }
        public virtual DbSet<HistoricoAloj> HistoricoAloj { get; set; }
        public virtual DbSet<HistoricoExtra> HistoricoExtra { get; set; }
        public virtual DbSet<Hospede> Hospede { get; set; }
        public virtual DbSet<HospEstAti> HospEstAti { get; set; }
        public virtual DbSet<ParqueCampismo> ParqueCampismo { get; set; }
        public virtual DbSet<Telefone> Telefone { get; set; }
        public virtual DbSet<Tendas> Tendas { get; set; }
    
        [DbFunction("GlampinhoEntities", "ListarAtividadesDisponiveis")]
        public virtual IQueryable<ListarAtividadesDisponiveis_Result> ListarAtividadesDisponiveis(Nullable<System.DateTime> dataInicio, Nullable<System.DateTime> dataFim)
        {
            var dataInicioParameter = dataInicio.HasValue ?
                new ObjectParameter("dataInicio", dataInicio) :
                new ObjectParameter("dataInicio", typeof(System.DateTime));
    
            var dataFimParameter = dataFim.HasValue ?
                new ObjectParameter("dataFim", dataFim) :
                new ObjectParameter("dataFim", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ListarAtividadesDisponiveis_Result>("[GlampinhoEntities].[ListarAtividadesDisponiveis](@dataInicio, @dataFim)", dataInicioParameter, dataFimParameter);
        }
    
        public virtual int CreateEstada(Nullable<int> idEstada, Nullable<System.DateTime> dataInicio, Nullable<System.DateTime> dataFim)
        {
            var idEstadaParameter = idEstada.HasValue ?
                new ObjectParameter("idEstada", idEstada) :
                new ObjectParameter("idEstada", typeof(int));
    
            var dataInicioParameter = dataInicio.HasValue ?
                new ObjectParameter("dataInicio", dataInicio) :
                new ObjectParameter("dataInicio", typeof(System.DateTime));
    
            var dataFimParameter = dataFim.HasValue ?
                new ObjectParameter("dataFim", dataFim) :
                new ObjectParameter("dataFim", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateEstada", idEstadaParameter, dataInicioParameter, dataFimParameter);
        }
    
        public virtual int DeleteAlojamento(string nome)
        {
            var nomeParameter = nome != null ?
                new ObjectParameter("nome", nome) :
                new ObjectParameter("nome", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteAlojamento", nomeParameter);
        }
    
        public virtual int DeleteAtividade(Nullable<int> num, Nullable<int> ano)
        {
            var numParameter = num.HasValue ?
                new ObjectParameter("num", num) :
                new ObjectParameter("num", typeof(int));
    
            var anoParameter = ano.HasValue ?
                new ObjectParameter("ano", ano) :
                new ObjectParameter("ano", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteAtividade", numParameter, anoParameter);
        }
    
        public virtual int DeleteExtraAloj(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteExtraAloj", idParameter);
        }
    
        public virtual int DeleteExtraPessoal(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteExtraPessoal", idParameter);
        }
    
        public virtual int DeleteHospede(string nIdentificacao)
        {
            var nIdentificacaoParameter = nIdentificacao != null ?
                new ObjectParameter("nIdentificacao", nIdentificacao) :
                new ObjectParameter("nIdentificacao", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteHospede", nIdentificacaoParameter);
        }
    
        public virtual ObjectResult<EnviarMailResponsaveis_Result> EnviarMailResponsaveis(Nullable<System.DateTime> datainicio)
        {
            var datainicioParameter = datainicio.HasValue ?
                new ObjectParameter("datainicio", datainicio) :
                new ObjectParameter("datainicio", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EnviarMailResponsaveis_Result>("EnviarMailResponsaveis", datainicioParameter);
        }
    
        public virtual ObjectResult<InscreverUmHospedeNumaAtividade_Result> InscreverUmHospedeNumaAtividade(string nIdentificacaoHospede, Nullable<int> numAtividade, Nullable<int> anoAtividade, Nullable<int> idEstada)
        {
            var nIdentificacaoHospedeParameter = nIdentificacaoHospede != null ?
                new ObjectParameter("nIdentificacaoHospede", nIdentificacaoHospede) :
                new ObjectParameter("nIdentificacaoHospede", typeof(string));
    
            var numAtividadeParameter = numAtividade.HasValue ?
                new ObjectParameter("numAtividade", numAtividade) :
                new ObjectParameter("numAtividade", typeof(int));
    
            var anoAtividadeParameter = anoAtividade.HasValue ?
                new ObjectParameter("anoAtividade", anoAtividade) :
                new ObjectParameter("anoAtividade", typeof(int));
    
            var idEstadaParameter = idEstada.HasValue ?
                new ObjectParameter("idEstada", idEstada) :
                new ObjectParameter("idEstada", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InscreverUmHospedeNumaAtividade_Result>("InscreverUmHospedeNumaAtividade", nIdentificacaoHospedeParameter, numAtividadeParameter, anoAtividadeParameter, idEstadaParameter);
        }
    
        public virtual int InsertAlojamento(ObjectParameter nome, string parque, string localizacao, string descricao, Nullable<decimal> precoBase, Nullable<int> nMaxPessoas, string tipo)
        {
            var parqueParameter = parque != null ?
                new ObjectParameter("parque", parque) :
                new ObjectParameter("parque", typeof(string));
    
            var localizacaoParameter = localizacao != null ?
                new ObjectParameter("localizacao", localizacao) :
                new ObjectParameter("localizacao", typeof(string));
    
            var descricaoParameter = descricao != null ?
                new ObjectParameter("descricao", descricao) :
                new ObjectParameter("descricao", typeof(string));
    
            var precoBaseParameter = precoBase.HasValue ?
                new ObjectParameter("precoBase", precoBase) :
                new ObjectParameter("precoBase", typeof(decimal));
    
            var nMaxPessoasParameter = nMaxPessoas.HasValue ?
                new ObjectParameter("nMaxPessoas", nMaxPessoas) :
                new ObjectParameter("nMaxPessoas", typeof(int));
    
            var tipoParameter = tipo != null ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertAlojamento", nome, parqueParameter, localizacaoParameter, descricaoParameter, precoBaseParameter, nMaxPessoasParameter, tipoParameter);
        }
    
        public virtual int InsertAlojEstEx(Nullable<int> id, string alojamento, Nullable<int> extra)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var alojamentoParameter = alojamento != null ?
                new ObjectParameter("alojamento", alojamento) :
                new ObjectParameter("alojamento", typeof(string));
    
            var extraParameter = extra.HasValue ?
                new ObjectParameter("extra", extra) :
                new ObjectParameter("extra", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertAlojEstEx", idParameter, alojamentoParameter, extraParameter);
        }
    
        public virtual ObjectResult<InsertAtividade_Result> InsertAtividade(ObjectParameter num, ObjectParameter ano, string parque, string nome, string descricao, Nullable<int> lotacaoMaxima, Nullable<System.DateTime> dataRealizacao, Nullable<decimal> precoParticipante)
        {
            var parqueParameter = parque != null ?
                new ObjectParameter("parque", parque) :
                new ObjectParameter("parque", typeof(string));
    
            var nomeParameter = nome != null ?
                new ObjectParameter("nome", nome) :
                new ObjectParameter("nome", typeof(string));
    
            var descricaoParameter = descricao != null ?
                new ObjectParameter("descricao", descricao) :
                new ObjectParameter("descricao", typeof(string));
    
            var lotacaoMaximaParameter = lotacaoMaxima.HasValue ?
                new ObjectParameter("lotacaoMaxima", lotacaoMaxima) :
                new ObjectParameter("lotacaoMaxima", typeof(int));
    
            var dataRealizacaoParameter = dataRealizacao.HasValue ?
                new ObjectParameter("dataRealizacao", dataRealizacao) :
                new ObjectParameter("dataRealizacao", typeof(System.DateTime));
    
            var precoParticipanteParameter = precoParticipante.HasValue ?
                new ObjectParameter("precoParticipante", precoParticipante) :
                new ObjectParameter("precoParticipante", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InsertAtividade_Result>("InsertAtividade", num, ano, parqueParameter, nomeParameter, descricaoParameter, lotacaoMaximaParameter, dataRealizacaoParameter, precoParticipanteParameter);
        }
    
        public virtual int InsertEstada(ObjectParameter id, Nullable<System.DateTime> dataInicio, Nullable<System.DateTime> dataFim, string nIdentificacao)
        {
            var dataInicioParameter = dataInicio.HasValue ?
                new ObjectParameter("dataInicio", dataInicio) :
                new ObjectParameter("dataInicio", typeof(System.DateTime));
    
            var dataFimParameter = dataFim.HasValue ?
                new ObjectParameter("dataFim", dataFim) :
                new ObjectParameter("dataFim", typeof(System.DateTime));
    
            var nIdentificacaoParameter = nIdentificacao != null ?
                new ObjectParameter("nIdentificacao", nIdentificacao) :
                new ObjectParameter("nIdentificacao", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertEstada", id, dataInicioParameter, dataFimParameter, nIdentificacaoParameter);
        }
    
        public virtual int InsertExtra(Nullable<int> id, string descricao, Nullable<decimal> precoDia, string tipo)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var descricaoParameter = descricao != null ?
                new ObjectParameter("descricao", descricao) :
                new ObjectParameter("descricao", typeof(string));
    
            var precoDiaParameter = precoDia.HasValue ?
                new ObjectParameter("precoDia", precoDia) :
                new ObjectParameter("precoDia", typeof(decimal));
    
            var tipoParameter = tipo != null ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertExtra", idParameter, descricaoParameter, precoDiaParameter, tipoParameter);
        }
    
        public virtual int InsertExtraAloj(ObjectParameter id, string tipo)
        {
            var tipoParameter = tipo != null ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertExtraAloj", id, tipoParameter);
        }
    
        public virtual int InsertExtraPessoal(ObjectParameter id, string tipo)
        {
            var tipoParameter = tipo != null ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertExtraPessoal", id, tipoParameter);
        }
    
        public virtual int InsertHospede(ObjectParameter nIdentificacao, Nullable<decimal> nif, string nome, string morada, string mail)
        {
            var nifParameter = nif.HasValue ?
                new ObjectParameter("nif", nif) :
                new ObjectParameter("nif", typeof(decimal));
    
            var nomeParameter = nome != null ?
                new ObjectParameter("nome", nome) :
                new ObjectParameter("nome", typeof(string));
    
            var moradaParameter = morada != null ?
                new ObjectParameter("morada", morada) :
                new ObjectParameter("morada", typeof(string));
    
            var mailParameter = mail != null ?
                new ObjectParameter("mail", mail) :
                new ObjectParameter("mail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertHospede", nIdentificacao, nifParameter, nomeParameter, moradaParameter, mailParameter);
        }
    
        public virtual int InsertHospEst(Nullable<int> id, string nIdentificacao)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var nIdentificacaoParameter = nIdentificacao != null ?
                new ObjectParameter("nIdentificacao", nIdentificacao) :
                new ObjectParameter("nIdentificacao", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertHospEst", idParameter, nIdentificacaoParameter);
        }
    
        public virtual ObjectResult<InsertHospEstAti_Result> InsertHospEstAti(string nIdentificacao, Nullable<int> num, Nullable<int> ano, Nullable<int> id)
        {
            var nIdentificacaoParameter = nIdentificacao != null ?
                new ObjectParameter("nIdentificacao", nIdentificacao) :
                new ObjectParameter("nIdentificacao", typeof(string));
    
            var numParameter = num.HasValue ?
                new ObjectParameter("num", num) :
                new ObjectParameter("num", typeof(int));
    
            var anoParameter = ano.HasValue ?
                new ObjectParameter("ano", ano) :
                new ObjectParameter("ano", typeof(int));
    
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InsertHospEstAti_Result>("InsertHospEstAti", nIdentificacaoParameter, numParameter, anoParameter, idParameter);
        }
    
        public virtual int InsertParqueCampismo(string nome, string morada, Nullable<int> estrelas, string mail)
        {
            var nomeParameter = nome != null ?
                new ObjectParameter("nome", nome) :
                new ObjectParameter("nome", typeof(string));
    
            var moradaParameter = morada != null ?
                new ObjectParameter("morada", morada) :
                new ObjectParameter("morada", typeof(string));
    
            var estrelasParameter = estrelas.HasValue ?
                new ObjectParameter("estrelas", estrelas) :
                new ObjectParameter("estrelas", typeof(int));
    
            var mailParameter = mail != null ?
                new ObjectParameter("mail", mail) :
                new ObjectParameter("mail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertParqueCampismo", nomeParameter, moradaParameter, estrelasParameter, mailParameter);
        }
    
        public virtual int PagarEstada(Nullable<int> id_Estada, Nullable<int> ano, ObjectParameter id_Factura)
        {
            var id_EstadaParameter = id_Estada.HasValue ?
                new ObjectParameter("Id_Estada", id_Estada) :
                new ObjectParameter("Id_Estada", typeof(int));
    
            var anoParameter = ano.HasValue ?
                new ObjectParameter("ano", ano) :
                new ObjectParameter("ano", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PagarEstada", id_EstadaParameter, anoParameter, id_Factura);
        }
    
        public virtual int SendMail(Nullable<int> nif, string texto)
        {
            var nifParameter = nif.HasValue ?
                new ObjectParameter("nif", nif) :
                new ObjectParameter("nif", typeof(int));
    
            var textoParameter = texto != null ?
                new ObjectParameter("texto", texto) :
                new ObjectParameter("texto", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SendMail", nifParameter, textoParameter);
        }
    
        public virtual int UpdateAlojamento(ObjectParameter nome, string parque, string localizacao, string descricao, Nullable<decimal> precoBase, Nullable<int> nMaxPessoas, string tipo)
        {
            var parqueParameter = parque != null ?
                new ObjectParameter("parque", parque) :
                new ObjectParameter("parque", typeof(string));
    
            var localizacaoParameter = localizacao != null ?
                new ObjectParameter("localizacao", localizacao) :
                new ObjectParameter("localizacao", typeof(string));
    
            var descricaoParameter = descricao != null ?
                new ObjectParameter("descricao", descricao) :
                new ObjectParameter("descricao", typeof(string));
    
            var precoBaseParameter = precoBase.HasValue ?
                new ObjectParameter("precoBase", precoBase) :
                new ObjectParameter("precoBase", typeof(decimal));
    
            var nMaxPessoasParameter = nMaxPessoas.HasValue ?
                new ObjectParameter("nMaxPessoas", nMaxPessoas) :
                new ObjectParameter("nMaxPessoas", typeof(int));
    
            var tipoParameter = tipo != null ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateAlojamento", nome, parqueParameter, localizacaoParameter, descricaoParameter, precoBaseParameter, nMaxPessoasParameter, tipoParameter);
        }
    
        public virtual int UpdateAtividade(ObjectParameter num, ObjectParameter ano, string parque, string nome, string descricao, Nullable<int> lotacaoMaxima, Nullable<System.DateTime> dataRealizacao, Nullable<decimal> precoParticipante)
        {
            var parqueParameter = parque != null ?
                new ObjectParameter("parque", parque) :
                new ObjectParameter("parque", typeof(string));
    
            var nomeParameter = nome != null ?
                new ObjectParameter("nome", nome) :
                new ObjectParameter("nome", typeof(string));
    
            var descricaoParameter = descricao != null ?
                new ObjectParameter("descricao", descricao) :
                new ObjectParameter("descricao", typeof(string));
    
            var lotacaoMaximaParameter = lotacaoMaxima.HasValue ?
                new ObjectParameter("lotacaoMaxima", lotacaoMaxima) :
                new ObjectParameter("lotacaoMaxima", typeof(int));
    
            var dataRealizacaoParameter = dataRealizacao.HasValue ?
                new ObjectParameter("dataRealizacao", dataRealizacao) :
                new ObjectParameter("dataRealizacao", typeof(System.DateTime));
    
            var precoParticipanteParameter = precoParticipante.HasValue ?
                new ObjectParameter("precoParticipante", precoParticipante) :
                new ObjectParameter("precoParticipante", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateAtividade", num, ano, parqueParameter, nomeParameter, descricaoParameter, lotacaoMaximaParameter, dataRealizacaoParameter, precoParticipanteParameter);
        }
    
        public virtual int UpdateExtraAloj(ObjectParameter id, string tipo)
        {
            var tipoParameter = tipo != null ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateExtraAloj", id, tipoParameter);
        }
    
        public virtual int UpdateExtraPessoal(Nullable<int> id, string tipo)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var tipoParameter = tipo != null ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateExtraPessoal", idParameter, tipoParameter);
        }
    
        public virtual int UpdateHospede(ObjectParameter nIdentificacao, Nullable<decimal> nif, string nome, string morada, string mail)
        {
            var nifParameter = nif.HasValue ?
                new ObjectParameter("nif", nif) :
                new ObjectParameter("nif", typeof(decimal));
    
            var nomeParameter = nome != null ?
                new ObjectParameter("nome", nome) :
                new ObjectParameter("nome", typeof(string));
    
            var moradaParameter = morada != null ?
                new ObjectParameter("morada", morada) :
                new ObjectParameter("morada", typeof(string));
    
            var mailParameter = mail != null ?
                new ObjectParameter("mail", mail) :
                new ObjectParameter("mail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateHospede", nIdentificacao, nifParameter, nomeParameter, moradaParameter, mailParameter);
        }
    }
}
