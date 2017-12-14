USE Glampinho;
GO
IF OBJECT_ID('dbo.MediadePagamentos') IS NOT NULL
	DROP FUNCTION dbo.MediadePagamentos
GO
CREATE FUNCTION dbo.MediadePagamentos(@ano int,@amostragem int)
RETURNS int
AS
BEGIN

	DECLARE @table Table(id int PRIMARY KEY, precos money)

	---CURSOR ATIVIDADE---
	DECLARE curAtividade CURSOR FOR
	SELECT HospEstAti.id, Atividade.precoParticipante
	FROM Atividade INNER JOIN HospEstAti ON Atividade.num = HospEstAti.num
	WHERE Atividade.ano = 2017 and HospEstAti.ano = 2017

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
	WHERE YEAR(Estada.dataInicio) = 2017 and YEAR(Estada.dataFim) = 2017

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
	WHERE YEAR(Estada.dataInicio) >= 2017 and YEAR(Estada.dataFim) <= 2017

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
	DECLARE @aux int, @total int, @current int
	SET @current = 1
		
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
	
	DECLARE @toRet int
	SET @toRet = @total/@aux 
	
	RETURN @toRet
END
GO
-------TESTE----------
/*SELECT * FROM ParqueCampismo
INSERT INTO ParqueCampismo VALUES('Lisboa','Rua Lisboeta',5,'lisboa@mail.com')
SELECT * FROM Alojamento
INSERT INTO Alojamento VALUES ('Marvila','Lisboa','Lisboa','7 colinas',450,9,'Bungalows')
SELECT * FROM Extra
INSERT INTO Extra VALUES(2,'Refeição extra',5.5,'ExPessoa')
INSERT INTO Extra VALUES(4,'Refeição extra',6.5,'ExPessoa')
INSERT INTO Extra VALUES(8,'Refeição extra',5.5,'ExPessoa')
INSERT INTO Extra VALUES(9,'Refeição extra',6.5,'ExPessoa')

select * from Estada
INSERT INTO  Atividade VALUES(2,2017,'Lisboa','Estudar','Licenciatura',3,'2017-12-08',10)--1
INSERT INTO  Atividade VALUES(3,2017,'Lisboa','Estudar','Licenciatura',2,'2017-12-15',10)--2
INSERT INTO  Atividade VALUES(24,2017,'Lisboa','Estudar','Licenciatura',4,'2017-12-28',10)--7
INSERT INTO  Atividade VALUES(21,2017,'Lisboa','Estudar','Licenciatura',6,'2017-12-21',10)--9
INSERT INTO  Atividade VALUES(20,2017,'Lisboa','Estudar','Licenciatura',10,'2017-12-28',10)--10
select * from HospEstAti
INSERT INTO  Estada VALUES(1,'2017-12-06','2017-12-12','1269007')
INSERT INTO  Estada VALUES(2,'2017-12-16','2017-12-25','1269007')
INSERT INTO Estada VALUES(7,'2017-12-26','2017-12-30','1269007')
INSERT INTO  Estada VALUES(9,'2017-12-20','2017-12-22','1269007')

INSERT INTO  Estada VALUES(10,'2017-12-22','2017-12-29','1269007')

INSERT INTO  HospEstAti VALUES(2,2017,1,'1269007')
INSERT INTO  HospEstAti VALUES(3,2017,2,'1269007')
INSERT INTO  HospEstAti VALUES(24,2017,7,'1269007')
INSERT INTO  HospEstAti VALUES(21,2017,9,'1269007')
INSERT INTO  HospEstAti VALUES(20,2017,10,'1269007')

INSERT INTO  EstAlojExtra VALUES(1,'Marvila',2)
INSERT INTO  EstAlojExtra VALUES(10,'Marvila',4)
INSERT INTO  EstAlojExtra VALUES(7,'Marvila',8)
INSERT INTO  EstAlojExtra VALUES(7,'Marvila',9)
SELECT * FROM EstAlojExtra

SELECT * FROM [dbo].[MediadePagamentos](2017,2)
DECLARE @t int 
SET @t = [dbo].[MediadePagamentos](2017,2)*/

---SELECT * FROM  ListarAtividadesDisponiveis ('2018-02-20','2018-04-04')