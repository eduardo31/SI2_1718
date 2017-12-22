USE Glampinho;

declare @nome varchar(100)
set @nome = 'ParkName'

update Atividade set parque = null where parque = @nome

declare curs cursor for select nome from Alojamento  WHERE parque = @nome
for update open curs declare @nomealoj varchar(100)fetch next from curs into @nomealoj
while(@@FETCH_STATUS=0)
begin
	delete from HistoricoAloj WHERE alojamento = @nomealoj
	delete from Tendas WHERE nomeAlojamento = @nomealoj
	delete from Bungalows WHERE nomeAlojamento = @nomealoj

	declare curs1 cursor for select id from EstAlojExtra where alojamento = @nomealoj
	for update open curs1 declare @id int fetch next from curs1 into @id
	while(@@FETCH_STATUS=0)
	begin
		update Fatura set idEstada = null where idEstada = @id
		delete from HospEstAti where id = @id

		delete from HospEst where id = @id
		delete from EstAlojExtra where alojamento = @nomealoj
		delete from Estada where id = @id
	fetch next from curs1 into @id
	end
	close curs1 
	deallocate curs1
fetch next from curs into @nomealoj
end
close curs 
deallocate curs

declare curs2 cursor for select nIdentificacao from Hospede
for update open curs2 declare @idhosp varchar(100) fetch next from curs2 into @idhosp
while(@@FETCH_STATUS=0)
begin
	IF NOT EXISTS ( SELECT nIdentificacao FROM HospEst WHERE nIdentificacao= @idhosp)
		update Hospede set exist = 'F' where nIdentificacao = @idhosp
	fetch next from curs2 into @idhosp
end
close curs2 
deallocate curs2
delete from dbo.Alojamento where parque = @nome
delete from dbo.Telefone where parque = @nome
delete from dbo.ParqueCampismo where nome = @nome