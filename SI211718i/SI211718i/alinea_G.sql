USE Glampinho;

--INSERT ATIVIDADE
GO

IF OBJECT_ID('dbo.InsertAtividade') IS NOT NULL
	DROP PROCEDURE dbo.InsertAtividade
GO

CREATE PROC InsertAtividade
	@num int, 
	@ano int, 
	@parque varchar(100),
	@nome varchar(100) = NULL,
	@descricao varchar(100) = NULL,
	@lotacaoMaxima int = NULL, 
	@dataRealizacao date = NULL,
	@precoParticipante money = NULL

AS
SET xact_abort ON 
BEGIN TRAN
	IF NOT EXISTS (SELECT * FROM Atividade WHERE num = @num and ano = @ano)
			INSERT INTO Atividade(num, ano,parque, nome, descricao, lotacaoMaxima, dataRealizacao, precoParticipante) 
	VALUES (@num, @ano,@parque, @nome, @descricao, @lotacaoMaxima, @dataRealizacao, @precoParticipante)
	ELSE raiserror('Atividade já existente!',15,1)
	COMMIT TRAN

----UPDATE
GO
IF OBJECT_ID('dbo.UpdateAtividade') IS NOT NULL
	DROP PROCEDURE dbo.UpdateAtividade
GO
CREATE PROC UpdateAtividade
	@num int, 
	@ano int, 
	@parque varchar(100),
	@nome varchar(100) = NULL,
	@descricao varchar(100) = NULL,
	@lotacaoMaxima int = NULL, 
	@dataRealizacao date = NULL,
	@precoParticipante money = NULL
AS
SET xact_abort on 
SET TRANSACTION ISOLATION LEVEL  REPEATABLE READ

BEGIN TRAN
BEGIN
	
	IF @parque IS NULL
			SET @parque = (SELECT parque FROM Atividade WHERE num = @num and ano = @ano)
	IF @nome IS NULL
			SET @nome = (SELECT nome FROM Atividade WHERE num = @num and ano = @ano)
	IF @descricao IS NULL
			SET @descricao = (SELECT descricao FROM Atividade WHERE num = @num and ano = @ano)
	IF @lotacaoMaxima IS NULL
			SET @lotacaoMaxima = (SELECT lotacaoMaxima FROM Atividade WHERE num = @num and ano = @ano)
	IF @dataRealizacao IS NULL
			SET @dataRealizacao = (SELECT dataRealizacao FROM Atividade WHERE num = @num and ano = @ano)
	IF @precoParticipante IS NULL
			SET @precoParticipante = (SELECT precoParticipante FROM Atividade WHERE num = @num and ano = @ano)


	UPDATE Atividade SET nome = @nome,descricao = @descricao, lotacaoMaxima = @lotacaoMaxima, 
	dataRealizacao = @dataRealizacao, precoParticipante = @precoParticipante
		WHERE num = @num and ano = @ano

	COMMIT
END

--------DELETE ATIVIDADE
GO

IF OBJECT_ID('DeleteAtividade') IS NOT NULL
	DROP PROCEDURE DeleteAtividade
GO
CREATE PROC DeleteAtividade
	@num int, @ano int

AS 
BEGIN TRAN
	IF EXISTS (SELECT * FROM HospEstAti WHERE num = @num and ano = @ano)
		DELETE HospEstAti WHERE num = @num and ano = @ano
	
	DELETE Atividade WHERE num = @num and ano = @ano

	COMMIT TRAN