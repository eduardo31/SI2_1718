USE Glampinho;

SET NOCOUNT ON 

GO
BEGIN TRAN
	PRINT('---------------TESTES--------------------------------TESTES--------------------------------TESTES-----------------')
	PRINT(CONCAT(CHAR(13),CHAR(10)))
	PRINT('OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO')
	PRINT('---TESTE DA AL�NEA C---')

	PRINT('INSERIR 3 HOSPEDES')

		SELECT * FROM dbo.Hospede

		EXEC InsertHospede @nIdentificacao ='12352',@nif = 222, @nome ='Pedro Fonseca' , @morada='Rua ABC', @mail ='pfonseca@mail.com'

		EXEC dbo.InsertHospede @nIdentificacao = '1234564', @nif = 333, @nome = 'Maria Carlota', @morada = 'Rua DEF',@mail = 'mcarlota@mail.com'

		EXEC dbo.InsertHospede @nIdentificacao ='4561233',@nif = 444, @nome ='Jo�o Pereira' , @morada='Rua ABC', @mail ='jpereira@mail.com'

	PRINT ('VERIFICA��O DOS HOSPEDES INSERIDOS')
	
		SELECT * FROM Hospede


	PRINT(' ')
	PRINT('ATUALIZA��O DA MORADA DO HOSPEDE "MARIA CARLOTA" - "RUA DEF" PARA "RUA ABC"')
	PRINT('ANTES DA ATUALIZA��O')
	
		SELECT morada FROM dbo.Hospede WHERE nIdentificacao = '1234564'

	PRINT('AP�S A ATUALIZA��O')
		EXEC UpdateHospede @nIdentificacao = '1234564', @nif = 333, @nome = 'Maria Carlota', @morada = 'Rua ABC',@mail = 'mcarlota@mail.com'

		SELECT morada FROM dbo.Hospede WHERE nIdentificacao ='1234564'

	PRINT(' ')

	PRINT('ATUALIZA��O DO NOME DO HOSPEDE - "JO�O PEREIRA" PARA "JOAQUIM PEREIRA"')
	PRINT('ANTES DA ATUALIZA��O')
	
		SELECT nome FROM dbo.Hospede WHERE nIdentificacao = '1234564'

	PRINT('AP�S A ATUALIZA��O')
		EXEC UpdateHospede @nIdentificacao ='4561233',@nif = 444, @nome ='Joaquim Pereira' , @morada='Rua ABC', @mail ='jpereira@mail.com'

		SELECT nome FROM dbo.Hospede WHERE nIdentificacao = '4561233'

	PRINT(' ')
	PRINT('REMO��O DO H�SPEDE COM O N�MERO DE IDENTIFICA��O, @nIdentificacao = "1234564"')
		SELECT * FROM dbo.Hospede
		EXEC DeleteHospede @nIdentificacao ='1234564'
	PRINT('VERIFICA��O DA REMO��O')
		SELECT * FROM dbo.Hospede
COMMIT
GO

BEGIN TRAN
	PRINT(CONCAT(CHAR(13),CHAR(10)))
	PRINT('OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO')
	PRINT('---TESTE DA AL�NEA D---') 

	PRINT(' ')

	PRINT('INSERIR 3 ALOJAMENTOS')

		SELECT * FROM dbo.Alojamento

		EXEC InsertParqueCampismo @nome = 'Alto Lima',	@morada = 'Rua Lima',@estrelas = 4,	@mail = 'lima@mail.com'
		go
		SELECT * FROM dbo.ParqueCampismo

		
		EXEC InsertAlojamento @nome = 'Lima', @parque = 'Alto Lima', @localizacao ='3AC', @descricao ='Paisagem Linda',
			@precoBase = 500, @nMaxPessoas = 12, @tipo = 'Tendas'
		
		
		go
		SELECT * FROM dbo.Alojamento

		EXEC InsertAlojamento @nome = 'Lindoso', @parque = 'Alto Lima', @localizacao ='2AC', @descricao ='Barragem',
			@precoBase = 450, @nMaxPessoas = 15, @tipo = 'Bungalows'

		EXEC InsertAlojamento @nome = 'Serra Amarela', @parque = 'Alto Lima', @localizacao ='6AC', @descricao ='Neve',
			@precoBase = 400, @nMaxPessoas = 14, @tipo = 'Tendas'

	PRINT ('VERIFICA��O DOS ALOJAMENTOS INSERIDOS')
	
		SELECT * FROM dbo.Alojamento


	PRINT(' ')
	PRINT('ATUALIZA��O DA LOCALIZA��O DO ALOJAMENTO "LIMA" - "3AC" PARA "4AC"')
	PRINT('ANTES DA ATUALIZA��O')
	
		SELECT localizacao FROM dbo.Alojamento WHERE nome = 'Lima'

	PRINT('AP�S A ATUALIZA��O')
	EXEC UpdateAlojamento @nome = 'Lima', @parque = 'Alto Lima', @localizacao ='4AC', @descricao ='Paisagem Linda',
			@precoBase = 500, @nMaxPessoas = 12, @tipo = 'Tendas'

		SELECT * FROM dbo.Alojamento WHERE nome = 'Lima'

	PRINT(' ')

	PRINT('ATUALIZA��O DO TIPO DO ALOJAMENTO "LINDOSO" - "BUNGALOWS" PARA "TENDAS"')
	PRINT('ANTES DA ATUALIZA��O')
	
		SELECT tipo FROM dbo.Alojamento WHERE nome = 'Lindoso'

	PRINT('AP�S A ATUALIZA��O')
			EXEC UpdateAlojamento @nome = 'Lindoso', @parque = 'Alto Lima', @localizacao ='2AC', @descricao ='Barragem',
			@precoBase = 450, @nMaxPessoas = 15, @tipo = 'Tendas'

		SELECT tipo FROM dbo.Alojamento WHERE nome = 'Lindoso'

	PRINT(' ')
	PRINT('REMO��O DO ALOJAMENTO "LINDOSO"')
		SELECT * FROM dbo.Alojamento

		EXEC DeleteAlojamento @nome = 'Lima'
	PRINT('VERIFICA��O DA REMO��O')
		SELECT * FROM dbo.Alojamento
		
COMMIT
GO

BEGIN TRAN



	PRINT(CONCAT(CHAR(13),CHAR(10)))
	PRINT('OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO')
	PRINT('---TESTE DA AL�NEA E---') 

	PRINT(' ')

	PRINT('INSERIR 3 EXTRA DE ALOJAMENTOS')

		SELECT * FROM dbo.ExtraAloj

		INSERT INTO Extra VALUES (2,'Uma Cama Extra',15,'ExAloj')
	
		INSERT INTO Extra VALUES (3,'Uma Cama Extra',18,'ExAloj')

		INSERT INTO Extra VALUES (5,'Uma Atividade Extra',16.6,'ExAloj')

		EXEC InsertExtraAloj @id = 2, @tipo='pe'
		EXEC InsertExtraAloj @id = 3, @tipo='ac'
		EXEC InsertExtraAloj @id = 5, @tipo='ac'

	PRINT ('VERIFICA��O DOS EXTRA DE ALOJAMENTOS INSERIDOS')
	
		SELECT * FROM dbo.ExtraAloj


	PRINT(' ')
	PRINT('ATUALIZA��O DO TIPO DE EXTRA DE ALOJAMENTO "2" - "PE" PARA "AC"')
	PRINT('ANTES DA ATUALIZA��O')
	
		SELECT tipo FROM dbo.ExtraAloj WHERE id = 2

	PRINT('AP�S A ATUALIZA��O')
	EXEC UpdateExtraAloj @id = 2,@tipo='ac'

		SELECT * FROM dbo.ExtraAloj WHERE id = 2

	PRINT(' ')

	PRINT('ATUALIZA��O DO TIPO DE EXTRA DE ALOJAMENTO "3" - "AC" PARA "PE"')
	PRINT('ANTES DA ATUALIZA��O')
	
		SELECT tipo FROM dbo.ExtraAloj WHERE id = 3

	PRINT('AP�S A ATUALIZA��O')
	EXEC UpdateExtraAloj @id = 3,@tipo='pe'

		SELECT * FROM dbo.ExtraAloj WHERE id = 3

	PRINT(' ')

	PRINT('REMO��O DO EXTRA DO ALOJAMENTO "2"')

		SELECT * FROM dbo.ExtraAloj
		EXEC DeleteExtraAloj @id =2

	PRINT('VERIFICA��O DA REMO��O')
		SELECT * FROM dbo.ExtraAloj
	


COMMIT

GO
---------------------------------------------------------
BEGIN TRAN



	PRINT(CONCAT(CHAR(13),CHAR(10)))
	PRINT('OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO')
	PRINT('---TESTE DA AL�NEA F---') 

	PRINT(' ')

	PRINT('INSERIR 3 EXTRA PESSOAL')

		SELECT * FROM dbo.ExtraPessoa

	
		INSERT INTO Extra VALUES (1,'Um almo�o Extra',15,'ExPessoa')
		INSERT INTO Extra VALUES (4,'Um pequeno-almo�o Extra',15,'ExPessoa')
		INSERT INTO Extra VALUES (6,'Um pequeno-almo�o Extra',15,'ExPessoa')

		EXEC InsertExtraPessoal @id = 1, @tipo='pa'
		EXEC InsertExtraPessoal @id = 4, @tipo='pc'
		EXEC InsertExtraPessoal @id = 6, @tipo='mp'
	

	PRINT ('VERIFICA��O DOS EXTRA PESSOAIS INSERIDOS')
	
		SELECT * FROM dbo.ExtraPessoa


	PRINT(' ')
	PRINT('ATUALIZA��O DO TIPO DE EXTRA PESSOAL "1" - "PA" PARA "PC"')
	PRINT('ANTES DA ATUALIZA��O')
	
		SELECT tipo FROM dbo.ExtraPessoa WHERE id = 1

	PRINT('AP�S A ATUALIZA��O')
	EXEC UpdateExtraPessoal @id = 1,@tipo='pc'

		SELECT * FROM dbo.ExtraPessoa WHERE id = 1

	PRINT(' ')

	PRINT('ATUALIZA��O DO TIPO DE EXTRA PESSOAL "4" - "PC" PARA "PA"')
	PRINT('ANTES DA ATUALIZA��O')
	
		SELECT tipo FROM dbo.ExtraPessoa WHERE id = 4

	PRINT('AP�S A ATUALIZA��O')
	EXEC UpdateExtraPessoal @id = 4,@tipo='pa'

		SELECT * FROM dbo.ExtraPessoa WHERE id = 4

	PRINT(' ')

	PRINT('REMO��O DO EXTRA PESSOAL "4"')

		SELECT * FROM dbo.ExtraPessoa
		EXEC DeleteExtraPessoal @id = 4

	PRINT('VERIFICA��O DA REMO��O')
		SELECT * FROM dbo.ExtraPessoa
	


COMMIT
GO

BEGIN TRAN



	PRINT(CONCAT(CHAR(13),CHAR(10)))
	PRINT('OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO')
	PRINT('---TESTE DA AL�NEA G---') 

	PRINT(' ')

	PRINT('INSERIR 3 ATIVIDADES')

		SELECT * FROM dbo.Atividade

	EXEC InsertAtividade @num =2, @ano=2017, @nome='Surf',@parque ='Alto Lima' ,@descricao='Praia e Mar', 
						@lotacaoMaxima = 22, @dataRealizacao='2018-08-07', @precoParticipante =7.5

	EXEC InsertAtividade @num = 22, @ano=2018, @nome='Ondas',@parque ='Alto Lima' ,@descricao='Praia e Mar', 
						@lotacaoMaxima = 9, @dataRealizacao='2018-08-22', @precoParticipante =8.43

	EXEC InsertAtividade @num = 7, @ano=2018, @nome='Escalada',@parque ='Alto Lima' ,@descricao='Serras e Montanhas', 
						@lotacaoMaxima = 9, @dataRealizacao='2018-09-01', @precoParticipante =9.5
	

	PRINT ('VERIFICA��O DAS ATIVIDADES INSERIDOS')
	
	SELECT * FROM dbo.Atividade


	PRINT(' ')
	PRINT('ATUALIZA��O DO PRE�O POR PARTICIPANTE NA ATIVDADE "2 DE 2017" - "7.5" PARA "10.49"')
	PRINT('ANTES DA ATUALIZA��O')
	
		SELECT precoParticipante FROM dbo.Atividade WHERE  num = 2 AND ano = 2017

	PRINT('AP�S A ATUALIZA��O')
	EXEC UpdateAtividade @num = 2, @ano = 2017, @nome = 'Surf',@parque ='Alto Lima',@descricao = 'Ondas Gigantes',@lotacaoMaxima = 4, 
		@dataRealizacao = '2017-08-07', @precoParticipante = 10.49

		SELECT * FROM dbo.Atividade WHERE  num = 2 AND ano = 2017

	PRINT(' ')

	PRINT('ATUALIZA��O DO NOME NA ATIVDADE "7 DE 2018" - "ESCALADA" PARA "RUNNING"')
	PRINT('ANTES DA ATUALIZA��O')
	
		SELECT nome FROM dbo.Atividade WHERE  num = 7 AND ano = 2018

	
	PRINT('AP�S A ATUALIZA��O')

		EXEC UpdateAtividade @num = 7, @ano = 2018, @nome = 'Running',@parque = 'Alto Lima' ,@descricao = 'Serras e Montanhas', 
						@lotacaoMaxima = 9, @dataRealizacao = '2018-09-01', @precoParticipante = 9.5

		SELECT nome FROM dbo.Atividade WHERE  num = 7 AND ano = 2018

	PRINT(' ')

	PRINT('REMO��O DA ATIVIDADE "7 DE 2018"')

		SELECT * FROM dbo.Atividade
		EXEC DeleteAtividade @num = 2, @ano =2017

	PRINT('VERIFICA��O DA REMO��O')
		SELECT * FROM dbo.Atividade


COMMIT

GO

BEGIN TRAN



	PRINT(CONCAT(CHAR(13),CHAR(10)))
	PRINT('OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO')

	PRINT('---TESTE DA AL�NEA H---') 

	PRINT('INSERIR UMA ESTADA...')

		EXEC CreateEstada @idEstada = 3,@dataInicio='2018-08-02',@dataFim = '2018-09-02'

	PRINT('VERIFICA��O DA INSER��O DA ESTADA')
		SELECT *  FROM dbo.Estada WHERE id= 3
	
COMMIT
---------------
GO

BEGIN TRAN
	PRINT(CONCAT(CHAR(13),CHAR(10)))
	PRINT('OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO')

	PRINT('---TESTE DA AL�NEA I---') 

	PRINT('INSERIR 2 H�SPEDES NUMA ATIVIDADE')
		EXEC InscreverUmHospedeNumaAtividade @nIdentificacaoHospede='12352',@numAtividade = 7, @anoAtividade =2018,	@idEstada = 3
		EXEC InscreverUmHospedeNumaAtividade @nIdentificacaoHospede='4561233',@numAtividade = 7, @anoAtividade =2018,	@idEstada = 3

	PRINT('VERIFICA��O DA INSER��O DA ESTADA')
		SELECT *  FROM dbo.HospEstAti WHERE id = 3 and ano = 2018 and num = 7

COMMIT

GO
BEGIN TRAN
	PRINT(CONCAT(CHAR(13),CHAR(10)))
	PRINT('OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO')
	PRINT('----------------------------------------------------------------TESTES ALINEA J----------------------------------------------------------------')

	PRINT('FATURAS')
	SELECT * FROM Fatura
	Declare @fat int
	set @fat = 0
	EXEC dbo.PagarEstada @Id_Estada = 3, @ano = 2018, @Id_Factura = @fat
	PRINT('Novo id da fatura inserida:')
	PRINT(CONVERT(varchar(max), @fat))
	PRINT('FATURA inserida descricao?')
	SELECT * FROM Fatura
	delete from Fatura where id = 1 and ano = 2018
COMMIT
GO
/*teste k*/
BEGIN TRAN
	PRINT(CONCAT(CHAR(13),CHAR(10)))
	PRINT('OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO')
	PRINT('----------------------------------------------------------------TESTES ALINEA K----------------------------------------------------------------')

	PRINT('Emails')
	SELECT * FROM Email
	select * from Estada
	select * from Hospede
	EXEC dbo.EnviarMailResponsaveis @dias = 30000;
	
	SELECT * FROM Email
	/* @fat int
	set @fat = 0
	EXEC dbo.PagarEstada @Id_Estada = 3, @ano = 2018, @Id_Factura = @fat
	PRINT('Novo id da fatura inserida:')
	PRINT(CONVERT(varchar(max), @fat))
	PRINT('FATURA inserida descricao?')
	SELECT * FROM Fatura
	delete from Fatura where id = 1 and ano = 2018*/
COMMIT

GO
BEGIN TRAN



	PRINT(CONCAT(CHAR(13),CHAR(10)))
	PRINT('OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO')

	PRINT('---TESTE DA AL�NEA L---') 

	PRINT('LISTAGEM DE TODAS AS ATIVIDADES COM LUGARES DISPON�VEIS NUM DADO INTERVALO')
	PRINT('INSER��O 6 H�SPEDES')
		EXEC InsertHospede @nIdentificacao ='222222',@nif = 2220, @nome ='Jos� Duarte' , @morada='Rua ABC', @mail ='jduarte@mail.com'

		EXEC dbo.InsertHospede @nIdentificacao ='333333', @nif = 3333, @nome = 'Maria Leoner', @morada = 'Rua DEF',@mail = 'mleoner@mail.com'

		EXEC dbo.InsertHospede @nIdentificacao ='4444444',@nif = 4444, @nome ='Jo�o Nunes' , @morada='Rua ABC', @mail ='jnunes@mail.com'

		EXEC dbo.InsertHospede @nIdentificacao ='5555555',@nif = 2922, @nome ='Francisco Fonseca' , @morada='Rua ABC', @mail ='ffoca@mail.com'

		EXEC dbo.InsertHospede @nIdentificacao = '6666666', @nif = 3393, @nome = 'Maria Rita', @morada = 'Rua DEF',@mail = 'mrita@mail.com'

		EXEC dbo.InsertHospede @nIdentificacao ='777777',@nif = 4544, @nome ='Rodolfo Pereira' , @morada='Rua ABC', @mail ='rodolfopereira@mail.com'

	PRINT('VERIFICA��O')
		SELECT * FROM dbo.Hospede

	PRINT('INSER��O DOS H�SPEDES � ATIVIDADE "22 DE 2018"')

		EXEC InscreverUmHospedeNumaAtividade @nIdentificacaoHospede='222222',@numAtividade = 22, @anoAtividade =2018,	@idEstada = 3
		EXEC InscreverUmHospedeNumaAtividade @nIdentificacaoHospede='333333',@numAtividade = 22, @anoAtividade =2018,	@idEstada = 3
		EXEC InscreverUmHospedeNumaAtividade @nIdentificacaoHospede='4444444',@numAtividade = 22, @anoAtividade =2018,	@idEstada = 3

		EXEC InscreverUmHospedeNumaAtividade @nIdentificacaoHospede='5555555',@numAtividade = 22, @anoAtividade =2018,	@idEstada = 3
		EXEC InscreverUmHospedeNumaAtividade @nIdentificacaoHospede='6666666',@numAtividade = 22, @anoAtividade =2018,	@idEstada = 3
		EXEC InscreverUmHospedeNumaAtividade @nIdentificacaoHospede='777777',@numAtividade = 22, @anoAtividade =2018,	@idEstada = 3

	PRINT('VERIFICA��O')
		SELECT * FROM dbo.HospEstAti WHERE ano = 2018 and num = 22 and id= 3
	PRINT('LUGARES DISPON�VEIS')
		SELECT * FROM  ListarAtividadesDisponiveis ('2018-07-29','2018-09-04')
	
COMMIT
----------------
GO
BEGIN TRAN

	PRINT(CONCAT(CHAR(13),CHAR(10)))
	PRINT('OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO')
	PRINT('---TESTE DA AL�NEA M---') 

	PRINT('OBTEN��O DA M�DIA DE PAGAMENTOS REALIZADOS NUM DADO ANO SENDO CALCAULADA COM UM INTERVALO DE AMOSTRAGEM ESPECIFICADO')
	PRINT('INSER��O DE UMA NOVA ESTADA')
		EXEC InsertEstada @id = 4, @dataInicio ='2018-08-02', @dataFim = '2018-09-02', @nIdentificacao  = 777777 /*PROCEDIMENTO CRIADO NA AL�NEA H*/

	PRINT('INSERIR H�SPEDES NUMA ATIVIDADE')
		EXEC InscreverUmHospedeNumaAtividade @nIdentificacaoHospede='222222',@numAtividade = 22, @anoAtividade =2018,	@idEstada = 4
		EXEC InscreverUmHospedeNumaAtividade @nIdentificacaoHospede='333333',@numAtividade = 22, @anoAtividade =2018,	@idEstada = 4
		EXEC InscreverUmHospedeNumaAtividade @nIdentificacaoHospede='4444444',@numAtividade = 22, @anoAtividade =2018,	@idEstada = 4

		EXEC InscreverUmHospedeNumaAtividade @nIdentificacaoHospede='5555555',@numAtividade = 7, @anoAtividade =2018,	@idEstada = 4
		EXEC InscreverUmHospedeNumaAtividade @nIdentificacaoHospede='6666666',@numAtividade = 7, @anoAtividade =2018,	@idEstada = 4
		EXEC InscreverUmHospedeNumaAtividade @nIdentificacaoHospede='777777',@numAtividade = 7, @anoAtividade =2018,	@idEstada = 4
	PRINT('VERIFICA��O')
		SELECT * FROM dbo.HospEstAti
	PRINT('OBTEN��O DA M�DIA DE PAGAMENTOS REALIZADOS NO ANO ="2018" COM UM INTERVALO DE AMOSTRAGEM = 1')
		SELECT dbo.MediadePagamentos (2018, 1)

	PRINT('OBTEN��O DA M�DIA DE PAGAMENTOS REALIZADOS NO ANO ="2018" COM UM INTERVALO DE AMOSTRAGEM = 2')
		SELECT dbo.MediadePagamentos (2018, 2)
	
COMMIT
