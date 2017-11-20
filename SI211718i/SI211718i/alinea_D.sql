USE Glampinho;

-- insert alojamento num parque
GO

IF OBJECT_ID('dbo.InsertAlojamento') IS NOT NULL
	DROP PROCEDURE dbo.InsertAlojamento
GO
CREATE PROC InsertAlojamento
	@nome varchar(100) OUTPUT, 
	@parque varchar(100) = NULL, 
	@localizacao varchar(100)= NULL,
	@descricao varchar(100) = NULL,
	@precoBase money = NULL, 
	@nMaxPessoas int = NULL, 
	@tipo varchar(10) = NULL
	
AS
SET xact_abort ON 
BEGIN TRAN

	INSERT INTO Alojamento(nome, parque, localizacao, descricao, precoBase, nMaxPessoas, tipo)  VALUES (@nome, @parque, 
	@localizacao, @descricao, @precoBase, @nMaxPessoas, @tipo)
	
	
	COMMIT

--teste	
	INSERT INTO ParqueCampismo VALUES('Alto Lima', 'Rua Lima', 4, 'lima@mail.com')


	EXEC InsertAlojamento @nome = 'Lima', @parque = 'Alto Lima', @localizacao ='Ponte da Barca', @descricao ='Paisagem Linda',
		@precoBase = 450, @nMaxPessoas = 12, @tipo = 'Tendas'

-------------------------------------
---update Alojamento
GO
IF OBJECT_ID('dbo.UpdateAlojamento') IS NOT NULL
	DROP PROCEDURE dbo.UpdateAlojamento
GO

CREATE PROC UpdateAlojamento
	@nome varchar(100) OUTPUT, 
	@parque varchar(100) = NULL, 
	@localizacao varchar(100)= NULL,
	@descricao varchar(100) = NULL,
	@precoBase money = NULL, 
	@nMaxPessoas int = NULL, 
	@tipo varchar(10) = NULL

AS

SET xact_abort on 
SET TRANSACTION ISOLATION LEVEL  REPEATABLE READ

BEGIN TRAN 
BEGIN
	IF @parque IS NULL
		SET @parque = (SELECT parque FROM Alojamento WHERE nome = @nome)
	IF @localizacao IS NULL
		SET  @localizacao = (SELECT localizacao FROM Alojamento WHERE nome = @nome)
	IF @descricao IS NULL
		SET @descricao = (SELECT descricao FROM Alojamento WHERE nome = @nome)
	IF @precoBase IS NULL
		SET @precoBase = (SELECT precoBase FROM Alojamento WHERE nome = @nome)
	IF @nMaxPessoas IS NULL
		SET @nMaxPessoas = (SELECT nMaxPessoas FROM Alojamento WHERE nome = @nome)
	IF @tipo IS NULL
		SET @tipo = (SELECT tipo FROM Alojamento WHERE nome = @nome)
	
	UPDATE Alojamento SET localizacao = @localizacao, descricao = @descricao,precoBase = @precoBase,
	nMaxPessoas = @nMaxPessoas, tipo = @tipo,parque = @parque
	WHERE nome = @nome 
	
	COMMIT
END


----------teste

EXEC UpdateAlojamento @nome = 'Lima', @parque = 'Alto Lima', @localizacao = 'Ponte de Lima', @descricao = 'Rio Lima',
		@precoBase = 460, @nMaxPessoas = 13 , @tipo = 'Bungalows'

--------------delete alojamento
GO

IF OBJECT_ID('dbo.DeleteAlojamento') IS NOT NULL
	DROP PROCEDURE dbo.DeleteAlojamento
GO
CREATE PROC DeleteAlojamento
	@nome varchar(100)
AS
BEGIN TRAN
	DELETE FROM Alojamento WHERE nome = @nome 
	COMMIT
---------TESTE
EXEC DeleteAlojamento @nome = 'Lima'

SELECT * FROM Alojamento
--SELECT * FROM ParqueCampismo