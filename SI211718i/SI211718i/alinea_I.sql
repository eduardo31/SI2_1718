USE Glampinho;
GO

IF OBJECT_ID('dbo.InscreverUmHospedeNumaAtividade') IS NOT NULL
	DROP PROCEDURE dbo.InscreverUmHospedeNumaAtividade
GO
CREATE PROC InscreverUmHospedeNumaAtividade
	@nIdentificacaoHospede varchar(100) = NULL,
	@numAtividade int = NULL,
	@anoAtividade int = NULL,
	@idEstada int = NULL
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
BEGIN TRY
	BEGIN TRANSACTION

	
	IF NOT EXISTS (SELECT * FROM Estada WHERE id = @idEstada)
		raiserror('Não existe a Estada inserida.',16,1)

	IF NOT EXISTS(SELECT * FROM Atividade WHERE ano = @anoAtividade and num = @numAtividade)
		raiserror('Não existe a Atividade inserida.',16,1)

	DECLARE @data date
	SET @data = (SELECT TOP 1 dataRealizacao FROM Atividade WHERE ano = @anoAtividade and num = @numAtividade)

	DECLARE @dataInicial date
	SET @dataInicial = (SELECT dataInicio FROM Estada WHERE id = @idEstada)

	DECLARE @dataFinal date
	SET @dataFinal = (SELECT dataFim FROM Estada WHERE id = @idEstada)

	IF(@data between @dataInicial and @dataFinal)
	BEGIN 
		DECLARE @nTotalHospedes int
	
		SET @nTotalHospedes =( SELECT COUNT(nIdentificacao) AS nTotalHospedes FROM HospEstAti WHERE ano = @anoAtividade and num = @numAtividade)
	
		DECLARE @lotacaoAtividade int = (SELECT TOP 1 lotacaoMaxima FROM Atividade WHERE ano = @anoAtividade and num = @numAtividade)
		
		IF(@nTotalHospedes >= @lotacaoAtividade)	
			raiserror('A atividade em questão está lotada',16,1)
		
		IF(@nTotalHospedes < @lotacaoAtividade)
			EXEC InsertHospEstAti @nIdentificacao=@nIdentificacaoHospede, @num=@numAtividade, @ano=@anoAtividade, @id=@idEstada

	END 
	ELSE raiserror('Data de Realização não está entre a data de início e de fim da Estada',16,1)
COMMIT
END TRY
begin catch
	declare @ErrorMessage NVARCHAR(4000);  
	declare @ErrorSeverity INT;  
	declare @ErrorState INT;  
	select 
		@ErrorMessage = ERROR_MESSAGE(),  
		@ErrorSeverity = ERROR_SEVERITY(),  
		@ErrorState = ERROR_STATE()
	raiserror (@ErrorMessage, @ErrorSeverity, @ErrorState)
	rollback 
end catch


------PROC AUXILIAR----
GO
IF OBJECT_ID('dbo.InsertHospEstAti') IS NOT NULL
	DROP PROCEDURE dbo.InsertHospEstAti
GO
CREATE PROC InsertHospEstAti
	@nIdentificacao varchar(100),
	@num int = NULL, 
	@ano int = NULL,
	@id int = NULL 
	
AS
SET xact_abort ON 
BEGIN TRAN
	IF NOT EXISTS (SELECT * FROM HospEstAti WHERE id = @id and nIdentificacao=@nIdentificacao and num = @num and ano = @ano)
		INSERT INTO HospEstAti (nIdentificacao,num,ano,id) VALUES (@nIdentificacao,@num,@ano,@id)
	ELSE raiserror('Tuplo introduzido já se encontra na base de dados!',15,1)
	COMMIT TRAN
