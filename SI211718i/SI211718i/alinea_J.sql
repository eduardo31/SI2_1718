/*
Proceder ao pagamento devido por uma estada, com emissão da respetiva fatura; 

-Aos preços base podem acrescer os preços de extras, os quais podem ser faturados por
alojamento ou por pessoa. 
- Qualquer alteração de preços de alojamento e de extras posterior a uma reserva
ou início de estada não irá alterar o valor a pagar pelos hóspedes. -todas as pessoas têm os mesmos extras
- Cada atividade é paga por participante, tendo estes de estar alojados no parque onde a atividade vai ter lugar.
-Após o pagamento de uma estada deve ser emitida uma fatura que contém um identificador
único em cada ano, o nome e NIF do hóspede responsável e a descrição dos alojamentos, extras e
atividades e respetivos preços. 

precisas de ? 
*/

USE Glampinho;

GO
/*pagamento*/
IF OBJECT_ID('dbo.PagarEstada') IS NOT NULL
	DROP PROCEDURE dbo.PagarEstada
GO

CREATE PROCEDURE dbo.PagarEstada
		@Id_Estada int,
		@ano int,
		@Id_Factura int output
AS
SET xact_abort ON 
BEGIN TRANSACTION
	
	DECLARE @descricao varchar(1000), @totaldias int, @datadeinicio date, @datadefim date, @precocurr money, @precototal money, @hospedes int,@nome varchar(100),@nif numeric(9)
	select @datadeinicio = dataInicio FROM Estada WHERE id=@Id_Estada;
	select @datadefim = dataFim FROM Estada WHERE id=@Id_Estada;
	select @totaldias = DATEDIFF(day, @datadeinicio, @datadefim);
	select @hospedes = COUNT(nIdentificacao) FROM HospEst WHERE @Id_Estada = id
	select @nome = nome from Hospede Where nIdentificacao=(select nIdentificacao from Estada WHERE id=@Id_Estada)
	select @nif = nif from Hospede Where nIdentificacao=(select nIdentificacao from Estada WHERE id=@Id_Estada)
	/*select * from EstAlojExtra WHERE id=@Id_Estada;*/
	declare curs cursor for select distinct alojamento from EstAlojExtra  WHERE id=@Id_Estada
	
	for update open curs declare @alojamento varchar(100)fetch next from curs into @alojamento
	while(@@FETCH_STATUS=0)
	begin

		select @precocurr =  (select precoBase from Alojamento where nome=@alojamento) * @totaldias
		select @precototal = @precototal + @precocurr
		select @descricao = @descricao + 'Alojamento' + CHAR(13)+CHAR(10)
		select @descricao = @descricao + (select descricao from Alojamento where nome=@alojamento) +' '+ @precocurr + CHAR(13)+CHAR(10)
		select @descricao = @descricao + 'Extras' + CHAR(13)+CHAR(10)
		declare curex cursor for select extra from EstAlojExtra WHERE id=@Id_Estada and alojamento = @alojamento
		for update open curex declare @extra int fetch next from curex into @extra
		while(@@FETCH_STATUS=0)
		begin
			select @precocurr =  (select precoDia from Extra where id=@extra) * @totaldias
			if ((select tipo from Extra where id=@extra)='ExPessoa')
				select @precocurr = @precocurr * @hospedes
			select @precototal = @precototal + @precocurr
			select @descricao = @descricao + (select descricao from Extra where id=@extra) +' '+ @precocurr + CHAR(13)+CHAR(10)
			fetch next from curex into @extra
		end
		close curex 
		deallocate curex
		fetch next from curs into @alojamento
	end
	close curs 
	deallocate curs
	/*calcular preço actividades */
	select @descricao = @descricao + 'Actividades' + CHAR(13)+CHAR(10)
	declare curs cursor for select num,ano from HospEstAti  WHERE id=@Id_Estada and nIdentificacao=(select nIdentificacao from HospEst where id = @Id_Estada )
	for update open curs declare @num int, @anoact int fetch next from curs into @num, @anoact
	while(@@FETCH_STATUS=0)
	begin
		select @precocurr = (select precoParticipante from Atividade where num = @num and ano=@ano)
		select @precototal = @precototal + @precocurr
		select @descricao = @descricao + (select descricao from Atividade where num = @num and ano=@ano) +' '+ @precocurr + CHAR(13)+CHAR(10)
		fetch next from curs into @num, @anoact
	end
	close curs 
	deallocate curs

	/*adicionar total*/

	 select @descricao = @descricao + 'TOTAL: ' + @precototal + CHAR(13)+CHAR(10)
	/*CALCULO DO Id_Fatura*/
	
	SELECT @Id_Factura = max(id) FROM Fatura WHERE YEAR(@ano)=YEAR(ano)
	IF @Id_Factura IS NULL
		SET @Id_Factura=0
	SET @Id_Factura=@Id_Factura+1	
	
	INSERT INTO Fatura (id,ano,idEstada,descricao,nome,nif)values(@Id_Factura,@ano,@Id_Estada,@descricao,@nome,@nif)



COMMIT
RETURN 

GO

