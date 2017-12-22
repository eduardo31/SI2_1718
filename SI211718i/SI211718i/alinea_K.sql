USE Glampinho;

--Enviar emails
GO
IF OBJECT_ID('dbo.SendMail') IS NOT NULL
	DROP PROCEDURE dbo.SendMail
GO

create PROC dbo.SendMail
	@nif int, 
	@texto varchar(600)
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
		insert into Email values(@nif, @email, @texto)
	END
commit
return
go


/* procedure avisar os hospedes responsáveis por estadas que se irão iniciar dentro de um dado periodo temporal*/

IF OBJECT_ID('dbo.EnviarMailResponsaveis') IS NOT NULL
	DROP PROCEDURE dbo.EnviarMailResponsaveis
GO

create PROC dbo.EnviarMailResponsaveis
	@dias int ---nos proximos @dias 
as
set tran isolation level repeatable read
SET xact_abort ON 
BEGIN TRAN

	Declare @datapresente Date,@periodoTemporal int, @id int,  @nif numeric(9), @mail varchar(100),@datafim Date,@datainit Date
	select @datapresente = GETDATE(), @datafim= DATEADD(day,@dias,@datapresente)
	IF (@datafim > @datapresente )
begin
	DECLARE @tabs TABLE
	(
		id int,  
		dataInicio Date,
		nif numeric(9),
		mail varchar(100)
		
	)
	insert into @tabs select id, dataInicio, nif, mail from dbo.Estada INNER JOIN dbo.Hospede on (Estada.nIdentificacao = Hospede.nIdentificacao and Estada.dataInicio<=@datafim and Estada.dataInicio>=@datapresente)
	Declare curs cursor for select id, dataInicio, nif, mail from @tabs
	for update open curs
	Fetch next From curs into @id, @datainit, @nif, @mail
		while (@@FETCH_STATUS = 0)
		begin
			select @periodoTemporal = DATEDIFF(day, @datapresente ,@datainit)
			declare @mensagem varchar(600)
			set @mensagem = CONCAT('Gostariamos de informar que a sua estada ira começar em ' , @periodoTemporal,' dias.' , CHAR(13),CHAR(10));
			Exec dbo.SendMail @nif, @mensagem
			fetch next from curs into @id, @datainit, @nif, @mail
		end
		close curs
		deallocate curs	
end
commit
return
go