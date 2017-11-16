USE Glampinho;
---INSERT UM EXTRA PESSOAL
GO

IF OBJECT_ID('dbo.InsertExtraPessoal') IS NOT NULL
	DROP PROCEDURE dbo.InsertExtraPessoal
GO

CREATE PROC InsertExtraPessoal
	@id int OUTPUT, @tipo varchar(4) = NULL

AS
SET xact_abort ON 
BEGIN TRAN
	INSERT INTO ExtraPessoa(id,tipo) VALUES (@id, @tipo)
	COMMIT

--------TESTE-----

INSERT INTO Extra VALUES (2,'Uma Cama Extra',15,'ExAloj')
INSERT INTO Extra VALUES (3,'Uma Cama Extra',18,'ExAloj')

INSERT INTO Extra VALUES (4,'Um Pequeno Almoço Extra',8.5,'ExPessoa')
INSERT INTO Extra VALUES (5,'Uma Atividade Extra',16.6,'ExAloj')

INSERT INTO Extra VALUES (1,'Um Jantar Extra',16,'ExPessoa')

EXEC InsertExtraPessoal @id = 4, @tipo='Pequeno Almoço'
EXEC InsertExtraPessoal @id = 1, @tipo='Pequeno Almoço'

---UPDATE UM EXTRA PESSOAL
GO
IF OBJECT_ID('dbo.UpdateExtraPessoal') IS NOT NULL
	DROP PROCEDURE dbo.UpdateExtraPessoal
GO
CREATE PROC UpdateExtraPessoal
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
	COMMIT 

END
---teste
EXEC UpdateExtraPessoal @id =4, @tipo='Meia Pensão'

-----------DELETE UM EXTRA PESSOAL--------------
GO

IF OBJECT_ID('dbo.DeleteExtraPessoal') IS NOT NULL
	DROP PROCEDURE dbo.DeleteExtraPessoal
GO
CREATE PROC DeleteExtraPessoal
	@id int
AS
BEGIN TRAN
	DELETE FROM ExtraPessoa WHERE id = @id
	COMMIT 

-------TESTE----
EXEC DeleteExtraPessoal @id=1

SELECT *FROM Extra
SELECT *FROM ExtraPessoa
