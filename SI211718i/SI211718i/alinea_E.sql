USE Glampinho;
---INSERT UM EXTRA DO ALOJAMENTO
GO

IF OBJECT_ID('dbo.InsertExtraAloj') IS NOT NULL
	DROP PROCEDURE dbo.InsertExtraAloj
GO
CREATE PROC InsertExtraAloj
	@id int OUTPUT, @tipo varchar(4) = NULL

AS
SET xact_abort ON 
BEGIN TRAN
	INSERT INTO ExtraAloj(id,tipo) VALUES (@id, @tipo)
	COMMIT

---teste
INSERT INTO Extra VALUES (2,'Uma Cama Extra',15,'ExAloj')
INSERT INTO Extra VALUES (3,'Uma Cama Extra',18,'ExAloj')
INSERT INTO Extra VALUES (5,'Uma Atividade Extra',16.6,'ExAloj')

EXEC InsertExtraAloj @id = 2, @tipo='Pessoa Extra'
EXEC InsertExtraAloj @id = 3, @tipo='Animal de Companhia'

---UPDATE UM EXTRA DO ALOJAMENTO
GO

IF OBJECT_ID('dbo.UpdateExtraAloj') IS NOT NULL
	DROP PROCEDURE dbo.UpdateExtraAloj
GO
CREATE PROC UpdateExtraAloj
		@id int OUTPUT, @tipo varchar(4) = NULL

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
---teste
EXEC UpdateExtraAloj @id =2, @tipo='Animal de Companhia'

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

------TESTE----

EXEC DeleteExtraAloj @id=2
-----
SELECT *FROM Extra
SELECT *FROM ExtraAloj
