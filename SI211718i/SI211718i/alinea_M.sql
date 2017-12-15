USE Glampinho;
GO
IF OBJECT_ID('dbo.MediadePagamentos') IS NOT NULL
	DROP FUNCTION dbo.MediadePagamentos
GO
CREATE FUNCTION dbo.MediadePagamentos(@ano int,@amostragem int)
RETURNS money
AS
BEGIN 

	DECLARE @table TABLE(id int PRIMARY KEY, precos money) 

	---CURSOR ATIVIDADE---
	DECLARE curAtividade CURSOR FOR
	SELECT HospEstAti.id, Atividade.precoParticipante
	FROM Atividade INNER JOIN HospEstAti ON Atividade.num = HospEstAti.num
	WHERE Atividade.ano = 2018 and HospEstAti.ano = 2018

	DECLARE @id int
	DECLARE @precos money
	
	OPEN curAtividade
	FETCH NEXT FROM curAtividade INTO @id, @precos
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		IF EXISTS(SELECT * FROM @table WHERE @id = id )
			UPDATE @table SET precos = precos + @precos WHERE id = @id
		ELSE INSERT INTO @table VALUES (@id,@precos)

		FETCH NEXT FROM curAtividade INTO @id, @precos
	END

	CLOSE curAtividade
	DEALLOCATE curAtividade

	---CURSOR EXTRA---
	DECLARE curExtra CURSOR FOR 
	SELECT Estada.id,Extra.precoDia FROM Estada 
	INNER JOIN EstAlojExtra ON Estada.id = EstAlojExtra.id
	INNER JOIN Extra ON EstAlojExtra.extra = Extra.id
	WHERE YEAR(Estada.dataInicio) = @ano and YEAR(Estada.dataFim) = @ano

	OPEN curExtra
	FETCH NEXT FROM curExtra INTO @id, @precos
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		IF EXISTS(SELECT * FROM @table WHERE @id = id )
			UPDATE @table SET precos = precos + @precos WHERE id = @id
		ELSE INSERT INTO @table VALUES (@id,@precos)

		FETCH NEXT FROM curExtra INTO @id, @precos
	END

	CLOSE curExtra
	DEALLOCATE curExtra
	
	---CURSOR ALOJAMENTO---
	DECLARE curAlojamento CURSOR FOR 
	SELECT EstAlojExtra.id,Alojamento.precoBase FROM Alojamento 
	INNER JOIN EstAlojExtra ON Alojamento.nome= EstAlojExtra.alojamento
	INNER JOIN Estada ON Estada.id = EstAlojExtra.id
	WHERE YEAR(Estada.dataInicio) >= @ano and YEAR(Estada.dataFim) <= @ano

	OPEN curAlojamento
	FETCH NEXT FROM curAlojamento INTO @id, @precos
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		IF EXISTS(SELECT * FROM @table WHERE @id = id )
			UPDATE @table SET precos = precos + @precos WHERE id = @id
		ELSE INSERT INTO @table VALUES (@id,@precos)

		FETCH NEXT FROM curAlojamento INTO @id, @precos
	END

	CLOSE curAlojamento
	DEALLOCATE curAlojamento

	
	----CALCULAR A MÉDIA COM O INTERVALO DE AMOSTRAGEM-----
	DECLARE @aux int, @total money, @current int
	SET @current = 1
	SET @total = 0
	SET @aux = 0
		
	DECLARE cur CURSOR FOR SELECT id, precos FROM @table
	OPEN cur
	FETCH NEXT FROM cur INTO @id, @precos
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		IF(@current%@amostragem = 1 OR @amostragem = 1)
		BEGIN
			SET @total = @total +@precos
			SET @aux = @aux + 1
		END
		SET @current = @current + 1
		FETCH NEXT FROM cur INTO @id, @precos
	END
	
	CLOSE cur
	DEALLOCATE cur
	
	DECLARE @toRet money
	SET @toRet = @total/@aux 
	
	RETURN @toRet
END
GO