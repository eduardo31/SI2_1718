USE Glampinho;

BEGIN TRANSACTION
	PRINT('Droping tables...')
	
	IF OBJECT_ID('HistoricoExtra') IS NOT NULL
		DROP TABLE HistoricoExtra
		
	IF OBJECT_ID('HistoricoAloj') IS NOT NULL
		DROP TABLE HistoricoAloj

	IF OBJECT_ID('HospEstAti') IS NOT NULL
		DROP TABLE HospEstAti

	IF OBJECT_ID('HospEst') IS NOT NULL
		DROP TABLE HospEst

	IF OBJECT_ID('Atividade') IS NOT NULL
		DROP TABLE Atividade

	IF OBJECT_ID('Fatura') IS NOT NULL
		DROP TABLE Fatura

	IF OBJECT_ID('ExtraAloj') IS NOT NULL
		DROP TABLE ExtraAloj

	IF OBJECT_ID('ExtraPessoa') IS NOT NULL
		DROP TABLE ExtraPessoa

	IF OBJECT_ID('EstAlojExtra') IS NOT NULL
		DROP TABLE EstAlojExtra

	IF OBJECT_ID('Extra') IS NOT NULL
		DROP TABLE Extra

	IF OBJECT_ID('Estada') IS NOT NULL
		DROP TABLE Estada

	IF OBJECT_ID('Hospede') IS NOT NULL
		DROP TABLE Hospede

	IF OBJECT_ID('Bungalows') IS NOT NULL
		DROP TABLE Bungalows

	IF OBJECT_ID('Tendas') IS NOT NULL
		DROP TABLE Tendas

	IF OBJECT_ID('Alojamento') IS NOT NULL
		DROP TABLE Alojamento

	IF OBJECT_ID('Telefone') IS NOT NULL
		DROP TABLE Telefone

	IF OBJECT_ID('ParqueCampismo') IS NOT NULL
		DROP TABLE ParqueCampismo

	IF OBJECT_ID('Email') IS NOT NULL
		DROP TABLE Email

	PRINT('Tables droped.')
COMMIT
GO