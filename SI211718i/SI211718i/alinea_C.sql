USE Glampinho;
-- insert hospede
GO

IF OBJECT_ID('dbo.InsertHospede') IS NOT NULL
	DROP PROCEDURE dbo.InsertHospede
GO

CREATE PROC InsertHospede
	 @nIdentificacao varchar(100) OUTPUT ,
	 @nif numeric = NULL, 
	 @nome varchar(100) = NULL, 
	 @morada varchar(100) = NULL,
	  @mail varchar(100) = NULL
AS
SET xact_abort ON 
BEGIN TRAN
	IF NOT EXISTS (SELECT * FROM Hospede WHERE nIdentificacao = @nIdentificacao)
		INSERT INTO Hospede(nIdentificacao,nif,nome,morada,mail,exist) VALUES (@nIdentificacao,@nif,@nome,@morada,@mail,'T')
	ELSE raiserror('Hospede já registado!',15,1)
	COMMIT TRAN

----update hospede
GO
IF OBJECT_ID('dbo.UpdateHospede') IS NOT NULL
	DROP PROCEDURE dbo.UpdateHospede
GO
CREATE PROC UpdateHospede
		@nIdentificacao varchar(100) OUTPUT ,
		@nif numeric = NULL, 
		@nome varchar(100) = NULL, 
		@morada varchar(100) = NULL,
		@mail varchar(100) = NULL
AS
SET xact_abort on 
SET TRANSACTION ISOLATION LEVEL  REPEATABLE READ
BEGIN TRAN
BEGIN
	IF @nif IS NULL
		SET @nif = (SELECT nif FROM Hospede WHERE nIdentificacao = @nIdentificacao)
	IF @nome IS NULL
		SET @nome = (SELECT nome FROM Hospede WHERE nIdentificacao = @nIdentificacao)
	IF @morada IS NULL
		SET @morada  = (SELECT morada  FROM Hospede WHERE nIdentificacao = @nIdentificacao)
	IF @mail IS NULL
		SET @mail = (SELECT mail FROM Hospede WHERE nIdentificacao = @nIdentificacao)

	UPDATE Hospede SET nif = @nif , morada = @morada, nome = @nome, mail = @mail
		WHERE  nIdentificacao = @nIdentificacao

	COMMIT
END

--------DeleteHospede-----
GO

IF OBJECT_ID('dbo.DeleteHospede') IS NOT NULL
	DROP PROCEDURE dbo.DeleteHospede
GO
CREATE PROC DeleteHospede
	 @nIdentificacao varchar(100)
AS
SET xact_abort ON
BEGIN TRAN
	
	IF EXISTS (SELECT * FROM HospEst WHERE nIdentificacao = @nIdentificacao)
		 DELETE FROM HospEst WHERE nIdentificacao = @nIdentificacao 
	
	IF EXISTS (SELECT * FROM HospEstAti WHERE nIdentificacao = @nIdentificacao)
		 DELETE FROM HospEstAti WHERE nIdentificacao = @nIdentificacao 

	IF EXISTS (SELECT * FROM Estada WHERE nIdentificacao = @nIdentificacao)
		 DELETE FROM Estada WHERE nIdentificacao = @nIdentificacao

	UPDATE Hospede SET exist = 'F' WHERE  nIdentificacao = @nIdentificacao

	COMMIT



