USE Glampinho;

--views
GO

if object_id('BungalowView') is not null
	drop view BungalowView
go

--View

create view BungalowView (nomeCamp, morada, estrelas, mail, 
	nomeAloj, localizacao, descricao, precoBase, nMaxPessoas)
as 
Select ParqueCampismo.nome, morada, estrelas, mail, 
	Alojamento.nome, localizacao, descricao, precoBase, nMaxPessoas
	from dbo.ParqueCampismo INNER JOIN dbo.Alojamento
	on ParqueCampismo.nome = Alojamento.parque
	where Alojamento.tipo ='Bungalows'
			
go

/* trigger de delete bungalow*/
create trigger deleteAlojBungalow
on BungalowView
instead of delete
as
	declare @nome varchar(100) 
	select * from deleted
	--select @nome = nomeAloj from deleted

	where nomeAloj = @nome
go



create trigger updateAlojBungalow
on BungalowView
instead of update
as
	declare @nome varchar(100) 
	if exists(Select * from inserted)
	begin
		declare @nomeAloj varchar(30)
		declare @nomeCamp varchar(30)
		declare @localizacao varchar(30)
		declare @descricao varchar(30)
		declare @precoBase money
		declare @nMaxPessoas int
		declare @tipo varchar(10)
		select @nomeAloj=nomeAloj,@nomeCamp=nomeCamp,@localizacao=localizacao, @descricao=descricao, 
				@precoBase=precoBase, @nMaxPessoas=nMaxPessoas
		from inserted
		update dbo.Alojamento
		set  
			@localizacao=localizacao, 
			@descricao = descricao, 
			@precoBase = precoBase, 
			@nMaxPessoas = nMaxPessoas
		where nome = @nome
	end
go
	

create trigger insertAlojBungalow 
on BungalowView
instead of insert
as
--declare @nome varchar(100)
--if not exists(Select nome=@nome from inserted)
begin
	declare @nomeAloj varchar(30)
	declare @nomeCamp varchar(30)
	declare @localizacao varchar(30)
	declare @descricao varchar(30)
	declare @precoBase money
	declare @nMaxPessoas int
	declare @tipo varchar(10)
		SELECT @nomeAloj=nomeAloj,@nomeCamp=nomeCamp,@localizacao=localizacao, @descricao=descricao, 
				@precoBase=precoBase, @nMaxPessoas=nMaxPessoas
			FROM inserted
		insert into dbo.Alojamento values (@nomeAloj, @nomeCamp, @localizacao, @descricao, @precoBase, @nMaxPessoas, 'Bungalows')
	end
go


--------testes---

SELECT * FROM ParqueCampismo
INSERT INTO ParqueCampismo VALUES('Lisboa','Rua Lisboeta',5,'lisboa@mail.com')
SELECT * FROM Alojamento
INSERT INTO Alojamento VALUES ('Marvila','Lisboa','Lisboa','7 colinas',450,9,'Bungalows')
INSERT INTO Alojamento VALUES ('Tribo','Lisboa','Lisb','boa vista',250,4,'Tendas')
select * from BungalowView
insert into BungalowView VALUES ('Lisboa','Rua Lisboeta', 5,'lisboa@mail.com', 'Alj', 'Lisbo', 'view', 250, 5)
--update BungalowView set precoBase = 230 where nomeCamp = 'Lisboa', nomeAloj = 'Alj'

