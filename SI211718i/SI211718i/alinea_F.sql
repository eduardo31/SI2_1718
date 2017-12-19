USE Glampinho;
---INSERT UM EXTRA PESSOAL
GO

IF OBJECT_ID('dbo.InsertExtraPessoal') IS NOT NULL
	DROP PROCEDURE dbo.InsertExtraPessoal
GO

CREATE PROC dbo.InsertExtraPessoal
	@id int, @tipo varchar(4) = NULL

AS
SET xact_abort ON 
BEGIN TRAN
	IF NOT EXISTS (SELECT * FROM ExtraPessoa WHERE id = @id)
		INSERT INTO ExtraPessoa(id,tipo) VALUES (@id, @tipo)
	ELSE raiserror('ExtraPessoa já existe!',15,1)
	COMMIT TRAN

---UPDATE UM EXTRA PESSOAL
GO
IF OBJECT_ID('dbo.UpdateExtraPessoal') IS NOT NULL
	DROP PROCEDURE dbo.UpdateExtraPessoal
GO
CREATE PROC dbo.UpdateExtraPessoal
		@id int, @tipo varchar(25)
AS
SET xact_abort on 
SET TRANSACTION ISOLATION LEVEL  REPEATABLE READ

BEGIN TRAN 
BEGIN

	IF @tipo IS NULL
			SET @tipo = (SELECT tipo FROM ExtraPessoa WHERE id = @id)

	UPDATE ExtraPessoa SET tipo = @tipo 
	WHERE id = @id
	COMMIT TRAN

END

-----------DELETE UM EXTRA PESSOAL--------------
GO

IF OBJECT_ID('dbo.DeleteExtraPessoal') IS NOT NULL
	DROP PROCEDURE dbo.DeleteExtraPessoal
GO
CREATE PROC dbo.DeleteExtraPessoal
	@id int
AS
BEGIN TRAN
	DELETE FROM ExtraPessoa WHERE id = @id
	COMMIT TRAN