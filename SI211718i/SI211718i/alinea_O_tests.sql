use SoAventura
SET NOCOUNT ON 
begin tran
GO
print(' ')
print('*****************************TESTE C CLIENTES*****************************')
print(' ')
print('Inserir 4 clientes')
print('Antes de inserir clientes')
	select * from dbo.Cliente
	exec dbo.InsertCliente @Nome='Isabel', @NIF=178845691, @CC=273075692, @Morada='Av. Cidade de Praga', @email='isabel94@gmail.com', @data_nascimento='1994-06-25'/*23*/
	exec dbo.InsertCliente @Nome='Santiago', @NIF=198865691, @CC=283075692, @Morada='Av. Liverdade', @email='santiago51@gmail.com', @data_nascimento='1977-06-25'/*40*/
	exec dbo.InsertCliente @Nome='Sandra', @NIF=118845791, @CC=173075692, @Morada='R. Amália Rodrigues', @email='sandrabonita@gmail.com', @data_nascimento='1947-06-25'/*70*/
	exec dbo.InsertCliente @Nome='Sérgio', @NIF=138845691, @CC=213075692, @Morada='R. Lucilia Simões', @email='sergiomorais@gmail.com', @data_nascimento='1987-06-25'/*30*/
print('Verificar se foram inseridos clientes')
	select * from dbo.Cliente
GO
print(' ')
print('Atualizar email da Isabel para isabelsoares@gmail.com')
print('Antes da atulização') 
	select email from dbo.Cliente where NIF=178845691
print('Verificar a atulização')
	exec dbo.UpdateCliente @NIF=178845691,@email='isabelsoares@gmail.com'
	select * from dbo.Cliente where NIF=178845691
print(' ')	
print('Atualizar nome da Sandra para Sandro')
print('Antes da atulização') 
	select Nome from dbo.Cliente where NIF=118845791
print('Verificar a atulização')
	exec dbo.UpdateCliente @NIF=118845791, @Nome='Sandro'
	select * from dbo.Cliente where NIF=118845791	
print(' ')
print('Atualizar morada do Santiago para Av. Cidade de Praga')
print('Antes da atulização') 
	select Morada from dbo.Cliente where NIF=198865691
print('Verificar a atulização')
	exec dbo.UpdateCliente @NIF=198865691, @Morada='Av. Cidade de Praga'
	select * from dbo.Cliente where NIF=198865691	
print(' ')	
print('Remover cliente com nif 118845791')
print('Antes de remover') 
	select * from dbo.Cliente where existente='T'
print('Verificar se foram removidos clientes')
	exec dbo.DeleteCliente @NIF=118845791
	select * from dbo.Cliente where existente='T'
	
GO

COMMIT



















begin tran
GO
print(' ')
print('*****************************TESTE D EVENTOS*****************************')
print(' ')
print('Inserir 12 eventos')
print('Antes de inserir eventos')
	select * from dbo.Evento_Desportivo


	exec dbo.InsertEventoCanoagem @ano=2017,	@data_da_realização='2017-06-1 16:30:00',@data_limite_pagamento = '2017-05-31',@fim_data_subscrição = '2017-05-25',		@inicio_data_subscrição = '2017-05-1',	@idade_min = 8,		@idade_max = 65,	@min_participantes = 2,	@max_participantes = 4,		@descrição = 'Descer o rio tejo.',	@preço_por_participante = 24.99,	@dificuldade =1
	exec dbo.InsertEventoCanoagem @ano=2017,	@data_da_realização='2017-06-25 16:30:00',@data_limite_pagamento = '2017-06-24',@fim_data_subscrição = '2017-06-20',	@inicio_data_subscrição = '2017-05-5',	@idade_min = 8,		@idade_max = 65,	@min_participantes = 1,	@max_participantes = 2,		@descrição = 'Descer o rio douro.',	@preço_por_participante = 24.99,	@dificuldade =5
	
	exec dbo.InsertEventoEscalada @ano=2017,	@data_da_realização='2017-06-25 16:30:00',@data_limite_pagamento = '2017-06-24',@fim_data_subscrição = '2017-06-20',	@inicio_data_subscrição = '2017-06-1',	@idade_min = 8,		@idade_max = 65,	@min_participantes = 1,	@max_participantes = 2,		@descrição = 'Escalar a parede do ISEL.',	@preço_por_participante = 24.99,	@dificuldade =5

	exec dbo.InsertEventoCiclismo @ano=2017,	@data_da_realização='2017-06-25 16:30:00',@data_limite_pagamento = '2017-06-24',@fim_data_subscrição = '2017-06-20',	@inicio_data_subscrição = '2017-06-1',	@idade_min = 8,		@idade_max = 65,	@min_participantes = 1,	@max_participantes = 2,		@descrição = 'BTT #1 ISEL.',	@preço_por_participante = 14.99,	@distancia =50
	exec dbo.InsertEventoCiclismo @ano=2017,	@data_da_realização='2017-07-15 16:30:00',@data_limite_pagamento = '2017-07-10',@fim_data_subscrição = '2017-07-08',	@inicio_data_subscrição = '2017-07-1',	@idade_min = 8,		@idade_max = 65,	@min_participantes = 1,	@max_participantes = 2,		@descrição = 'BTT #2 ISEL.',	@preço_por_participante = 27.50,	@distancia =50

	exec dbo.InsertEventoEscalada @ano=2017,	@data_da_realização='2017-07-25 16:30:00',@data_limite_pagamento = '2017-06-30',@fim_data_subscrição = '2017-06-30',	@inicio_data_subscrição = '2017-06-1',	@idade_min = 10,	@idade_max = 16,	@min_participantes = 5,	@max_participantes = 10,	@descrição = 'Escalar a parede do ISEL. #2',	@preço_por_participante = 10.99,	@dificuldade =2

	exec dbo.InsertEventoTrail @ano=2017,		@data_da_realização='2017-07-25 14:30:00',@data_limite_pagamento = '2017-06-25',@fim_data_subscrição = '2017-06-20',	@inicio_data_subscrição = '2017-06-1',	@idade_min = 8,		@idade_max = 65,	@min_participantes = 1,	@max_participantes = 2,		@descrição = 'Trail',	@preço_por_participante = 24.99,	@distancia =50
	
	/* 1 mes depois */
	exec dbo.InsertEventoTrail @ano=2017,		@data_da_realização='2017-08-10 10:00:00',@data_limite_pagamento = '2017-07-30',@fim_data_subscrição = '2017-07-30',	@inicio_data_subscrição = '2017-06-1',	@idade_min = 20,	@idade_max = 45,	@min_participantes = 5,	@max_participantes = 20,	@descrição = 'Summer Trail',	@preço_por_participante = 20.99,	@distancia =30

	/* past events */  
	exec dbo.InsertEventoTrail @ano=2017,		@data_da_realização='2017-04-05 10:00:00',@data_limite_pagamento = '2017-04-03',@fim_data_subscrição = '2017-04-01',	@inicio_data_subscrição = '2017-03-10',	@idade_min = 30,	@idade_max = 50,	@min_participantes = 10,	@max_participantes = 30,	@descrição = 'Trail ISEL.',	@preço_por_participante = 35.99,	@distancia =70  --higher price
	exec dbo.InsertEventoCiclismo @ano=2016,	@data_da_realização='2016-10-10 15:00:00',@data_limite_pagamento = '2016-10-01',@fim_data_subscrição = '2016-10-05',	@inicio_data_subscrição = '2016-10-1',	@idade_min = 10,	@idade_max = 50,	@min_participantes = 10,	@max_participantes = 20,	@descrição = 'Bicicleta ISEL.',	@preço_por_participante = 9.99,	@distancia =20

	/* evento escalada que inicia com o estado cancelado */
	exec dbo.InsertEventoEscalada @ano=2017,	@data_da_realização='2017-05-10 15:00:00',@data_limite_pagamento = '2017-05-09',@fim_data_subscrição = '2017-05-15',	@inicio_data_subscrição = '2017-05-1',	@idade_min = 15,	@idade_max = 30, @estado='cancelado', @min_participantes = 15,	@max_participantes = 30,	@descrição = 'Escalada ISEL.',	@preço_por_participante = 12.99,	@dificuldade =4
	exec dbo.InsertEventoEscalada @ano=2017,	@data_da_realização='2017-09-10 10:00:00',@data_limite_pagamento = '2017-09-09',@fim_data_subscrição = '2017-09-10',	@inicio_data_subscrição = '2017-09-01',	@idade_min = 25,	@idade_max = 30, @estado='cancelado', @min_participantes = 15,	@max_participantes = 30,	@descrição = 'Escalada ISEL 2.',	@preço_por_participante = 15.99,	@dificuldade =4
		exec dbo.InsertEventoEscalada @ano=2017,	@data_da_realização='2017-09-10 10:00:00',@data_limite_pagamento = '2017-09-09',@fim_data_subscrição = '2017-09-10',	@inicio_data_subscrição = '2017-09-01',	@idade_min = 25,	@idade_max = 30, @estado='cancelado', @min_participantes = 15,	@max_participantes = 30,	@descrição = 'Escalada ISEL 2.',	@preço_por_participante = 15.99,	@dificuldade =4
			exec dbo.InsertEventoEscalada @ano=2017,	@data_da_realização='2017-09-10 10:00:00',@data_limite_pagamento = '2017-09-09',@fim_data_subscrição = '2017-09-10',	@inicio_data_subscrição = '2017-09-01',	@idade_min = 25,	@idade_max = 30, @estado='cancelado', @min_participantes = 15,	@max_participantes = 30,	@descrição = 'Escalada ISEL 2.',	@preço_por_participante = 15.99,	@dificuldade =4
				exec dbo.InsertEventoEscalada @ano=2017,	@data_da_realização='2017-09-10 10:00:00',@data_limite_pagamento = '2017-09-09',@fim_data_subscrição = '2017-09-10',	@inicio_data_subscrição = '2017-09-01',	@idade_min = 25,	@idade_max = 30, @estado='cancelado', @min_participantes = 15,	@max_participantes = 30,	@descrição = 'Escalada ISEL 2.',	@preço_por_participante = 15.99,	@dificuldade =4



print('Verificar se foram inseridos os eventos')
	select * from dbo.Evento_Desportivo
GO
print(' ')
print('Atualizar idade minima')
print('Antes da atulização') 
	select idade_min from dbo.Evento_Desportivo where Id_Evento=1 and ano=2017
print('Verificar a atulização')
	exec dbo.UpdateEventoCanoagem @Id_Evento=1,@ano=2017,@idade_min=9
	select idade_min from dbo.Evento_Desportivo where Id_Evento=1 and ano=2017
print(' ')	
print('Remover evento ')
print('Antes de remover') 
	select * from dbo.Evento_Desportivo
print('Verificar se foram removidos clientes')
	exec dbo.RemoveEvento @Id_Evento=1,@ano=2017
	select * from dbo.Evento_Desportivo
	
GO

COMMIT



print('*****************************TESTE G*****************************')
	select Id_Evento,estado from dbo.Evento_Desportivo
	exec dbo.VerificarEstados
	select Id_Evento,estado from dbo.Evento_Desportivo





begin tran
GO
print(' ')
print('*****************************TESTE E*****************************')
print(' ')
print('Subscrever clinte')
print('Antes de subscrever') 
	select * from dbo.Subscrição

	
	exec dbo.SubscreverClienteEvento @Id_Evento=2,@ano=2017,@NIF=178845691
	exec dbo.SubscreverClienteEvento @Id_Evento=3,@ano=2017,@NIF=178845691
	exec dbo.SubscreverClienteEvento @Id_Evento=4,@ano=2017,@NIF=138845691 /* client exceeds max age */
	exec dbo.SubscreverClienteEvento @Id_Evento=5,@ano=2017,@NIF=118845791
	exec dbo.SubscreverClienteEvento @Id_Evento=1,@ano=2016,@NIF=118845791 /* 2016 */
	exec dbo.SubscreverClienteEvento @Id_Evento=9,@ano=2017,@NIF=198865691 /* subscription with price over 35eur */
	
print('Depois de subscrever') 
	select * from dbo.Subscrição	
GO

COMMIT

GO





SET NOCOUNT ON
begin tran
GO

print(' ')
print('*****************************TESTE F*****************************')
print(' ')
print('Proceder ao pagamento de uma subscrição.')
print('Before: Faturas') 
	select * from dbo.Fatura

print('Pagar subscrição')


	
	
	-- testar montante insuficiente
	exec dbo.PagarSubscricao @Id_Evento=2,@ano=2017,@NIF=178845691,@Id_Factura=1,@montante=50
	exec dbo.PagarSubscricao @Id_Evento=5,@ano=2017,@NIF=118845791,@Id_Factura=2,@montante=1

	
	
		


	
print('After: Faturas') 
	select * from dbo.Fatura


	commit
GO











SET NOCOUNT ON 
begin tran
GO
print(' ')
print('*****************************TESTE I INICIO*****************************')
print('Listar a contagem dos eventos cancelados, agrupados por tipo, num dado intervalo de datas')
print('Data de  inicio 2017-01-1 e data de fim 2017-08-1')
	select * from dbo.EventosCancelados ('2015-01-1','2020-08-1');
GO
COMMIT




SET NOCOUNT ON 
begin tran
GO
print(' ')
print('*****************************TESTE J INICIO*****************************')
print('Listar todos os eventos com lugares disponíveis para um intervalo de datas especificado')
print('Data de  inicio 2017-05-05 e data de fim 2017-05-20')
	select * from dbo.EventosDisponiveis ('2017-06-20','2017-06-30');
GO
COMMIT


SET NOCOUNT ON 
begin tran
GO


print('*****************************TESTE VERIFICAR ESTADOS*****************************')
	SELECT * FROM dbo.Subscrição
	print(' ')
	print('Atualizar ESTADO')
	
	print('Antes da atulização') 
	select estado from dbo.Evento_Desportivo where Id_Evento=2 and ano=2017
	print('Verificar a atulização')
	exec dbo.UpdateEvento @Id_Evento=2,@ano=2017,@estado='concluído'


	select estado from dbo.Evento_Desportivo where Id_Evento=2 and ano=2017
	exec dbo.VerificarEstados


	print('*****************************TESTE Mails*****************************')
	select NIF,email from dbo.MailsEnviados;

GO
COMMIT




SET NOCOUNT ON 
begin tran
GO
print(' ')
print('*****************************TESTE K INICIO*****************************')
print('Listar todas as faturas por ano num dado intervalo de montantes.')
print('Ano 2017 entre 0 e 100 euros')
	select * from dbo.FaturasPorAno ('2017',0,100);
GO
COMMIT


