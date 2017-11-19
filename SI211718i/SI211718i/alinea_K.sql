USE Glampinho;

--Enviar emails
GO
IF OBJECT_ID('dbo.SendMail') IS NOT NULL
	DROP PROCEDURE dbo.SendMail
GO

create PROC dbo.SendMail
	@nif int, 
	@texto varchar(1000)
as

SET xact_abort ON 
BEGIN TRAN
	IF NOT EXISTS ( SELECT nif from dbo.Hospede where nif = @nif)
		RAISERROR ('Hospede INVÁLIDO!', 15, 1)
	ELSE BEGIN
		Declare @email varchar(200)
		Select @email = mail From dbo.Hospede Where nif = @nif
		IF @email is null
		RAISERROR('email INVÁLIDO!',15,1)
		ELSE 
		insert into Email values(@nif, @texto)
	END
commit
return
go


/* procedure avisar os hospedes responsáveis por estadas que se irão iniciar dentro de um dado periodo temporal*/

IF OBJECT_ID('dbo.EnviarMailResponsaveis') IS NOT NULL
	DROP PROCEDURE dbo.EnviarMailResponsaveis
GO

create PROC dbo.EnviarMailResponsaveis
	@datainicio Date  --datainicio
as
set tran isolation level repeatable read
SET xact_abort ON 
BEGIN TRAN

	Declare @datapresente Date,@periodoTemporal int, @id int,  @nif numeric(9), @mail varchar(100)
	--obter periodo temporal
	select @datapresente = GETDATE(), @periodoTemporal = DATEDIFF(day, @datapresente ,@datainicio)
	
	IF (@datainicio > @datapresente )
	--cursor--
begin
	select @id = id from Estada where @datainicio=dataInicio
	select @mail = mail from Hospede where nIdentificacao = (select nIdentificacao from Estada where id= @id)
	select @nif = nif from Hospede where nIdentificacao = (select nIdentificacao from Estada where id= @id)


	Declare curs cursor for select id from HospEst where id = @id
			
	for update open curs Fetch next From curs into @id, @mail, @nif
		while (@@FETCH_STATUS = 0)
		begin

			select id, mail, nif
			from dbo.Estada INNER JOIN dbo.Hospede
			on Estada.nIdentificacao = Hospede.nIdentificacao
			Where Estada.dataInicio = @datainicio

			--criar mensagem
			declare @mensagem varchar(5000)
			set @mensagem = 'Gostariamos de informar que a sua estada ira começar em' + @periodoTemporal
	
			--exec proc sendmail.. enviar email
			--Declare @nif numeric (9)
			--Select @nif = (Select @nif from hospResEstada Where @datainicio )
			Exec dbo.SendMail @nif, @mensagem

		end
		close curs
		deallocate curs
		
end
commit
return
go


---Teste--



