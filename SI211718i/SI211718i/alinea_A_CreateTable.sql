USE master
GO
IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'Glampinho')
	BEGIN
		PRINT('Creating database named Glampinho...')
		CREATE DATABASE Glampinho
		PRINT('Database created.')
	END
ELSE
	PRINT('Database already created.')

GO 
USE Glampinho;
CREATE TABLE ParqueCampismo(
	nome varchar(100) PRIMARY KEY,
	morada varchar(100) NOT NULL,
	estrelas int NOT NULL,
	check(estrelas>=1 and estrelas <= 5),
	mail varchar(100) UNIQUE NOT NULL
)

CREATE TABLE Telefone(
	telefone int PRIMARY KEY,
	parque varchar(100) NOT NULL FOREIGN KEY REFERENCES ParqueCampismo(nome)
)

CREATE TABLE Alojamento(
	nome varchar(100) PRIMARY KEY,
	parque varchar(100) FOREIGN KEY REFERENCES ParqueCampismo(nome),
	localizacao varchar(30) UNIQUE NOT NULL,
	descricao varchar(100)NOT NULL,	
	precoBase money NOT NULL,
	nMaxPessoas int NOT NULL,
	tipo varchar(10) NOT NULL,
	check (tipo = 'Tendas' or tipo = 'Bungalows'),		
)
CREATE TABLE Tendas(
	nomeAlojamento varchar(100) PRIMARY KEY,
	area int,
	tipo varchar(10) NOT NULL,
	check(tipo = 'Yurt' or tipo = 'Tipi' or tipo = 'Safari'),
	FOREIGN KEY(nomeAlojamento) REFERENCES Alojamento(nome)
)
CREATE TABLE Bungalows(
	nomeAlojamento varchar(100) PRIMARY KEY,
	tipologia varchar(2) NOT NULL,
	check(tipologia = 'T0' or tipologia = 'T1' or tipologia = 'T2' or tipologia = 'T3'),
	FOREIGN KEY(nomeAlojamento) REFERENCES Alojamento(nome)
)
CREATE TABLE Hospede(
	nIdentificacao varchar(100) PRIMARY KEY,
	nif numeric(9) UNIQUE NOT NULL,
	nome varchar(100) NOT NULL,	
	morada varchar(100) NOT NULL,
	mail varchar(100) UNIQUE NOT NULL
)
CREATE TABLE Estada(
	id int PRIMARY KEY,
	dataInicio date NOT NULL,
	dataFim date NOT NULL,
	nIdentificacao varchar(100)
)
CREATE TABLE Extra(
	id int PRIMARY KEY,
	descricao varchar(100) NOT NULL,
	precoDia money NOT NULL,
	tipo varchar(10) NOT NULL,
	check(tipo = 'ExPessoa' or tipo = 'ExAloj')
)
CREATE TABLE EstAlojExtra(
	id int FOREIGN KEY REFERENCES Estada,
	alojamento varchar(100) FOREIGN KEY REFERENCES Alojamento(nome),
	extra int FOREIGN KEY REFERENCES Extra(id) 
	PRIMARY KEY(id,alojamento,extra)
)
CREATE TABLE ExtraPessoa(
	id int PRIMARY KEY,
	FOREIGN KEY(id) REFERENCES Extra,
	tipo varchar(4)  NOT NULL,
	check(tipo ='pa' or tipo = 'mp' or tipo = 'pc')
)
CREATE TABLE ExtraAloj(
	id int PRIMARY KEY,
	FOREIGN KEY(id) REFERENCES Extra,
	tipo varchar(4)  NOT NULL,
	check(tipo ='pe' or tipo = 'ac' or tipo = 'ea')
)
CREATE TABLE Fatura(
	id int,
	ano int,
	idEstada int NOT NULL FOREIGN KEY REFERENCES Estada(id),
	PRIMARY KEY(id,ano)
)
CREATE TABLE Atividade(
	num int,
	ano int,
	parque varchar(100) NOT NULL FOREIGN KEY REFERENCES ParqueCampismo(nome),
	nome varchar(100) NOT NULL,
	descricao varchar(100) NOT NULL,
	lotacaoMaxima int NOT NULL,
	dataRealizacao date NOT NULL,
	precoParticipante money NOT NULL,
	PRIMARY KEY(num,ano)
)
CREATE TABLE HospEst(
	id int REFERENCES Estada,
	nIdentificacao varchar(100) REFERENCES Hospede,
	PRIMARY KEY(id,nIdentificacao)
)
CREATE TABLE HospEstAti(
	num int, 
	ano int,
	id int NOT NULL FOREIGN KEY REFERENCES Estada,
	nIdentificacao varchar(100) REFERENCES Hospede,
	PRIMARY KEY(num,ano,id,nIdentificacao),
	FOREIGN KEY(num,ano) REFERENCES Atividade,
)
