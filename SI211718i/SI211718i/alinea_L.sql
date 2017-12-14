USE Glampinho;
GO

IF OBJECT_ID('dbo.ListarAtividadesDisponiveis') IS NOT NULL
	DROP FUNCTION dbo.ListarAtividadesDisponiveis
GO
CREATE FUNCTION dbo.ListarAtividadesDisponiveis (@dataInicio date, @dataFim date)
RETURNS @table table(numAtividade int, anoAtivdade int,lugaresDisponiveis int)
AS
BEGIN 
		DECLARE @total table (nHospedes int, ano int, num int,nMaxPessoas int)
		INSERT INTO @total
		
		SELECT COUNT(nIdentificacao),Atividade.num,Atividade.ano,Atividade.lotacaoMaxima FROM Atividade INNER JOIN HospEstAti 
		ON Atividade.num = HospEstAti.num and Atividade.ano = HospEstAti.ano WHERE(dataRealizacao>=@dataInicio and  dataRealizacao>=@dataFim)
		GROUP BY Atividade.num,Atividade.ano,Atividade.lotacaoMaxima

		--DECLARE @table table (num int, ano int, disponiveis int)
		INSERT INTO @table
		SELECT T.num,T.ano,(T.nMaxPessoas-T.nHospedes) AS disponiveis FROM @total AS T 

		RETURN 
END
GO