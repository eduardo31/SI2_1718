USE Glampinho;
GO

IF OBJECT_ID('dbo.CreateEstada') IS NOT NULL
	DROP PROCEDURE dbo.CreateEstada
GO
CREATE PROC CreateEstada
	@idEstada int, @dataInicio date, @dataFim date
AS
BEGIN TRY
	BEGIN TRAN

	----1.---
	/*Criar uma estada dado o NIF do hóspede responsável e o período temporal pretendido;*/

	---inserir hospede responsavel
	EXEC InsertHospede @nif = 2992222,@nIdentificacao = '1269007', @morada ='Rua do ISEL', @nome='Francisco Fernandes', @mail='ff@mail.com'
	

	--insercao na estada
	 EXEC InsertEstada @id = @idEstada,@dataInicio=@dataInicio,@dataFim=@dataFim,@nIdentificacao = '1269007'
	 

	---associacao do hosp responsavel à estada
	EXEC InsertHospEst @id=@idEstada, @nIdentificacao = '1269007'
	

	 --select * from HospEst
	---2.--
	/*Adicionar um alojamento de um dado tipo com uma determinada lotação a uma estada;*/

	--inserir um parque
	EXEC InsertParqueCampismo @nome='Alto Lima',@morada = 'Rua Lima', @estrelas=4, @mail='lima@mail.com'
	

	--INSERIR UM ALOJAMENTO----
	EXEC InsertAlojamento @nome = 'Lima', @parque = 'Alto Lima', @localizacao ='Ponte da Barca', @descricao ='Paisagem Linda',
							@precoBase = 450, @nMaxPessoas = 12, @tipo = 'Bungalows'
	
	-----EXTRA----
	EXEC InsertExtra @id = 10,@descricao = 'Uma Cama Extra',@precoDia =9.5,@tipo='ExAloj'
	--delete Extra where id=10
	
	------INSERT ALOJAMENTO ADICIONADO À ESTADA-----
	EXEC InsertAlojEstEx @id=@idEstada, @alojamento='Lima',@extra =10

	----3.----
	/*Adicionar um hospede numa Estada*/
	----INSERT HOSPEDE

	EXEC InsertHospede @nif = 2227872,@nIdentificacao = '1278967', @morada ='Rua do ISEL', @nome='Joana Fonseca', @mail='joaninhafonseca@mail.com'
	
	----ADICIONAR O HOSPEDE À ESTADA---
	EXEC InsertHospEst @id=@idEstada, @nIdentificacao = '1278967'

	-------4.----
	/*Adicionar um extra a um alojamento de uma estada*/
	----ADICIONAR EXTRA ALOJAMENTO------

	EXEC InsertExtraAloj @id=10, @tipo='pe'
	
	----5.---
	/*Adicionar um extra pessoal de uma estada*/

	-------ADICIONAR EXTRA-----

	EXEC InsertExtra @id =23, @descricao ='Pequeno Almoço Extra',@precoDia=10,@tipo='ExPessoa'
	
	-----ADICIONAR EXTRA PESSOAL---
	EXEC InsertExtraPessoal @id=23, @tipo='pa'
	EXEC InsertAlojEstEx @id=@idEstada, @alojamento='Lima',@extra =23



	COMMIT TRAN
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
--------------------------PROCEDIMENTO AUXILIARES--------------
--USE Glampinho;

----------INSERÇÃO DA ESTADA------------
GO

IF OBJECT_ID('dbo.InsertEstada') IS NOT NULL
	DROP PROCEDURE dbo.InsertEstada
GO

CREATE PROC InsertEstada
	@id int OUTPUT,
	@dataInicio date = NULL,
	@dataFim date = NULL,
	@nIdentificacao varchar(100) = NULL
AS
SET xact_abort ON 
BEGIN TRANSACTION
	IF(@dataFim<@dataInicio)
		raiserror('Data de Inicio é superior à Data Fim!',16,1)
	IF NOT EXISTS (SELECT * FROM Estada WHERE id = @id)
		INSERT INTO Estada(id,dataInicio,dataFim,nIdentificacao) VALUES (@id,@dataInicio,@dataFim,@nIdentificacao)
	ELSE raiserror('Estada já existente!',15,1)
	COMMIT TRAN

---------INSERÇÃO DO HOSPEDE À ESTADA-------
GO

IF OBJECT_ID('dbo.InsertHospEst') IS NOT NULL
	DROP PROCEDURE dbo.InsertHospEst
GO

CREATE PROC InsertHospEst
	@id int = NULL,
	@nIdentificacao varchar(100) = NULL

AS
SET xact_abort ON 
BEGIN TRANSACTION
	IF NOT EXISTS (SELECT * FROM HospEst WHERE id = @id and nIdentificacao= @nIdentificacao)
		INSERT INTO HospEst(id,nIdentificacao) VALUES (@id,@nIdentificacao)
	ELSE raiserror('O Hospede já se encontra inserido na Estada!',15,1)
	COMMIT TRAN

-------------INSERÇÃO AO PARQUE DE CAMPISMO--------
GO

IF OBJECT_ID('dbo.InsertParqueCampismo') IS NOT NULL
	DROP PROCEDURE dbo.InsertParqueCampismo
GO

CREATE PROC InsertParqueCampismo
	@nome varchar(100),
	@morada varchar(100) = NULL,
	@estrelas int = NULL,
	@mail varchar(100) = NULL

AS
SET xact_abort ON 
BEGIN TRANSACTION
	IF NOT EXISTS (SELECT * FROM ParqueCampismo WHERE nome = @nome)
		INSERT INTO ParqueCampismo(nome,morada,estrelas,mail) VALUES (@nome,@morada,@estrelas,@mail)
	ELSE raiserror('O Parque de Campismo já se encontra inserido!',15,1)
	COMMIT TRAN

-------------INSERÇÃO AO EXTRA--------
GO

IF OBJECT_ID('dbo.InsertExtra') IS NOT NULL
	DROP PROCEDURE dbo.InsertExtra
GO

CREATE PROC InsertExtra
	@id int,
	@descricao varchar(100) = NULL,
	@precoDia money = NULL,
	@tipo varchar(10) = NULL

AS
SET xact_abort ON 
BEGIN TRANSACTION
	IF NOT EXISTS (SELECT * FROM Extra WHERE id = @id)
			INSERT INTO Extra(id,descricao,precoDia,tipo) VALUES (@id,@descricao,@precoDia,@tipo)
	ELSE raiserror('Extra já se encontra inserido!',15,1)
	COMMIT TRAN
---------INSERÇÃO ALOJAMENTO ADICIONADO À ESTADA-----------
GO

IF OBJECT_ID('dbo.InsertAlojEstEx') IS NOT NULL
	DROP PROCEDURE dbo.InsertAlojEstEx
GO

CREATE PROC InsertAlojEstEx
	@id int,
	@alojamento varchar(100) = NULL,
	@extra int = NULL

AS
SET xact_abort ON 
BEGIN TRANSACTION
	IF NOT EXISTS (SELECT * FROM EstAlojExtra WHERE id = @id and alojamento = @alojamento and extra = @extra)
		INSERT INTO EstAlojExtra(id,alojamento,extra) VALUES (@id,@alojamento,@extra)
	ELSE raiserror('Tuplo inserido já se encontra na base de dados!',15,1)
COMMIT TRAN

--------TESTE---
EXEC CreateEstada @idEstada = 3, @dataInicio='2018-08-02',@dataFim = '2018-09-02'
--select * from Estada