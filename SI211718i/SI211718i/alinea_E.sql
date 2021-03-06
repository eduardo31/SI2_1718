USE Glampinho;
---INSERT UM EXTRA DO ALOJAMENTO
GO

IF OBJECT_ID('dbo.InsertExtraAloj') IS NOT NULL
	DROP PROCEDURE dbo.InsertExtraAloj
GO
CREATE PROC InsertExtraAloj
	@id int, @tipo varchar(4) = NULL

AS
SET xact_abort ON 
BEGIN TRAN
	IF NOT EXISTS (SELECT * FROM ExtraAloj WHERE id = @id)
		INSERT INTO ExtraAloj(id,tipo) VALUES (@id, @tipo)
	ELSE raiserror('ExtraAloj já existe!',15,1)
	COMMIT TRAN

---UPDATE UM EXTRA DO ALOJAMENTO
GO

IF OBJECT_ID('dbo.UpdateExtraAloj') IS NOT NULL
	DROP PROCEDURE dbo.UpdateExtraAloj
GO
CREATE PROC UpdateExtraAloj
		@id int, @tipo varchar(4) = NULL

AS
SET xact_abort on 
SET TRANSACTION ISOLATION LEVEL  REPEATABLE READ

BEGIN TRAN 
BEGIN

	IF @tipo IS NULL
			SET @tipo = (SELECT tipo FROM ExtraAloj WHERE id = @id)

	UPDATE ExtraAloj SET tipo = @tipo 
	WHERE id = @id
	COMMIT 

END

-----------DELETE UM EXTRA DO ALOJAMENTO
GO

IF OBJECT_ID('dbo.DeleteExtraAloj') IS NOT NULL
	DROP PROCEDURE dbo.DeleteExtraAloj
GO
CREATE PROC DeleteExtraAloj	@id int
AS
BEGIN TRAN
	DELETE FROM ExtraAloj WHERE id = @id
	COMMIT 

