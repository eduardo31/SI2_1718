USE Glampinho;

SET NOCOUNT ON 


SELECT * FROM ParqueCampismo
INSERT INTO ParqueCampismo VALUES('Lisboa','Rua Lisboeta',5,'lisboa@mail.com')
SELECT * FROM Alojamento
INSERT INTO Alojamento VALUES ('Marvila','Lisboa','Lisboa','7 colinas',450,9,'Bungalows')
SELECT * FROM Extra
INSERT INTO Extra VALUES(2,'Refeição extra',5.5,'ExPessoa')
INSERT INTO Extra VALUES(4,'Refeição extra',6.5,'ExPessoa')
INSERT INTO Extra VALUES(8,'Refeição extra',5.5,'ExPessoa')
INSERT INTO Extra VALUES(9,'Refeição extra',6.5,'ExPessoa')

select * from Atividade
INSERT INTO  Atividade VALUES(2,2017,'Lisboa','Estudar','Licenciatura',3,'2017-12-08',10)--1
INSERT INTO  Atividade VALUES(3,2017,'Lisboa','Estudar','Licenciatura',2,'2017-12-15',10)--2
INSERT INTO  Atividade VALUES(24,2017,'Lisboa','Estudar','Licenciatura',4,'2017-12-28',10)--7
INSERT INTO  Atividade VALUES(21,2017,'Lisboa','Estudar','Licenciatura',6,'2017-12-08',10)
INSERT INTO  Atividade VALUES(20,2017,'Lisboa','Estudar','Licenciatura',10,'2017-12-08',10)
select * from HospEstAti
INSERT INTO  Estada VALUES(1,'2017-12-06','2017-12-12','1269007')
INSERT INTO  Estada VALUES(2,'2017-12-16','2017-12-25','1269007')
INSERT INTO Estada VALUES(7,'2017-12-26','2017-12-30','1269007')
INSERT INTO  Estada VALUES(9,'2017-12-20','2017-12-22','1269007')
INSERT INTO  Estada VALUES(10,'2017-12-22','2017-12-29','1269007')
select * from Estada
INSERT INTO  EstAlojExtra VALUES(1,'Marvila',2)
INSERT INTO  EstAlojExtra VALUES(10,'Marvila',4)
INSERT INTO  EstAlojExtra VALUES(7,'Marvila',8)
INSERT INTO  EstAlojExtra VALUES(7,'Marvila',9)
select * from EstAlojExtra

Declare @Id_Fat int 
select @Id_Fat = 0
EXEC PagarEstada @Id_Estada=1, @ano=2017, @Id_Factura=@Id_Fat
go
select * from Fatura