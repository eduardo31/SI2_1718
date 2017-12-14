USE Glampinho;
GO

IF OBJECT_ID('dbo.ListarAtividadesDisponiveis') IS NOT NULL
	DROP FUNCTION dbo.ListarAtividadesDisponiveis
GO
CREATE FUNCTION dbo.ListarAtividadesDisponiveis (@dataInicio date, @dataFim date)
RETURNS @table table(numAtividade int, anoAtivdade int,lugaresDisponiveis int)
AS
BEGIN 
		DECLARE @total table (nHospedes int, ano int, num int,nMaxPessoas int)
		INSERT INTO @total
		
		SELECT COUNT(nIdentificacao),Atividade.num,Atividade.ano,Atividade.lotacaoMaxima FROM Atividade INNER JOIN HospEstAti 
		ON Atividade.num = HospEstAti.num and Atividade.ano = HospEstAti.ano WHERE(dataRealizacao>=@dataInicio and  dataRealizacao>=@dataFim)
		GROUP BY Atividade.num,Atividade.ano,Atividade.lotacaoMaxima

		--DECLARE @table table (num int, ano int, disponiveis int)
		INSERT INTO @table
		SELECT T.num,T.ano,(T.nMaxPessoas-T.nHospedes) AS disponiveis FROM @total AS T 

		RETURN 
END
GO
-----TESTE----
/*insert into ParqueCampismo values('Isel','Rua do Isel',5,'isel@mail.com')
insert into ParqueCampismo values('ISEP','Rua do ISEP',4,'iseP@mail.com')
insert into ParqueCampismo values('UM','Rua do ISEP',4,'um@mail.com')
Select * from Atividade
insert into Atividade values(2,2018,'Isel','Estudar','licenciatura',3,'2018-02-28',9.5)--

insert into Atividade values(6,2018,'UM','Estudar','licenciatura',3,'2018-05-15',9.5)--
insert into Atividade values(4,2018,'ISEP','Estudar','licenciatura',4,'2018-06-06',9.5)

insert into Hospede values ('45454545',526341,'Ana Sousa','Rua XPTO','anasousa@mail.com')
insert into Hospede values ('23232323',963852741,'Tejal Abreu','Rua DEF','tejabreu@mail.com')
insert into Hospede values ('8525224',7895412,'Eduardo Jorge','Rua ABC','eduedu@mail.com')
insert into Hospede values ('78954136',4521221,'Sofia Torres','Rua Torres','torressofia@mail.com')
insert into Hospede values ('86553214',9517534,'Carlos Fernandes','Rua FF','fernandes@mail.com')
insert into Hospede values ('59899321',645751,'João Martins','Rua ABC','jmartins@mail.com')

insert into Estada values (2,'2018-01-02','2018-03-22','45454545')
insert into Estada values (9,'2018-04-02','2018-05-22','45454545')

insert HospEstAti values (2,2018,9,'45454545')
insert HospEstAti values (2,2018,9,'23232323')
insert HospEstAti values (2,2018,9,'78954136')
select * from HospEstAti 

insert HospEstAti values (6,2018,9,'23232323')
insert HospEstAti values (6,2018,9,'45454545')

SELECT * FROM  ListarAtividadesDisponiveis ('2018-02-20','2018-04-04')*/
