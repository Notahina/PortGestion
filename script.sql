CREATE TABLE Quai(
	Idquai varchar(50) PRIMARY KEY,
	Long float,
	Prof float
);
CREATE TABLE Cargaison(
	IdCarg varchar(50) PRIMARY KEY,
	Type text,
	Degagement float
);
CREATE TABLE Prevision(
	Idpred varchar(50) PRIMARY KEY,
	IdCarg varchar(50),
	Name varchar(80) ,
	Datep DateTime,
	Heure varchar(25)
	Longueur float,
	Prof float
	FOREIGN KEY(IdCarg) REFERENCES Cargaison(IdCarg)
);
CREATE TAbel test(
	dates date
);
CREATE TABLE Proposition(
	Idprop varchar(50) PRIMARY KEY,
	Idpred varchar(50),
	Idquai varchar(50)
	Name varchar(80),
	Dateentre DateTime,
	Datesortie DateTime,
	FOREIGN KEY(Idpred) REFERENCES Prevision(Idpred),
	FOREIGN KEY(Idquai) REFERENCES Quai(Idquai)
);

CREATE TABLE Evenement (
	Idev varchar(50) Primary key,
	Idpred varchar(50),
	Idquai varchar(50),
	DateA DateTime NULL,
	DateD DateTime NULL,
	FOREIGN KEY(Idpred) REFERENCES Prevision(Idpred),
	FOREIGN KEY(Idquai) REFERENCES Quai(Idquai)
);
CREATE TABLE Changes(
	Id varchar(20) PRIMARY KEY,
	Devise varchar(25),
	Dates DATETime,
	Montant float
);
CREATE TABLE Penalite(
	Idpenalite varchar(25) PRIMARY KEY,
	Minim int ,
	Maxim int,
	Unite int,
	Devise varchar(30),
	Montant decimal(20,2)
);
CREATE TABLE PrevisionDetails(
	Id varchar(50) Primary key,
	Idprev varchar(50),
	Idquai varchar(50),
	DateA DateTime NULL,
	DateD DateTime NULL,
	FOREIGN KEY(Idprev) REFERENCES Prevision(Idpred),
	FOREIGN KEY(Idquai) REFERENCES Quai(Idquai)
);

 create view Propo as select q.Idquai,p.Idpred,p.IdCarg,p.Name,p.Datep,p.Duree ,p.Prof,p.Longueur from  Prevision p,Quai q where q.Longueur>=p.Longueur and q.Prof>=p.Prof 
CREATE VIEW EvenementNull as select * from Evenement  where DateD is null;
Idev,Idpred,Idquai,DateA
Drop view EvenementNull;
CREATE SEQUENCE idquai START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE idcarg START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE idpred START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE idpro START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE idev START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE idchange START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE idprevd START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE idpen START WITH 1 INCREMENT BY 1;

INSERT INTO Penalite VALUES(CONCAT('PEN',NEXT VALUE FOR idpen),'Q2',0,60,15,'Euro',15);
INSERT INTO Penalite VALUES(CONCAT('PEN',NEXT VALUE FOR idpen),'Q2',60,120,1,'Euro',50);
INSERT INTO Penalite VALUES(CONCAT('PEN',NEXT VALUE FOR idpen),'Q2',120,-1,-1,'Euro',25);
INSERT INTO Quai VALUES (CONCAT('Q',NEXT VALUE FOR idquai),300.0,10.0);
INSERT INTO Quai VALUES (CONCAT('Q',NEXT VALUE FOR idquai),500.0,10.5);
INSERT INTO Quai VALUES (CONCAT('Q',NEXT VALUE FOR idquai),320.0,15.0);


INSERT INTO Cargaison VALUES (CONCAT('CA',NEXT VALUE FOR idcarg),'Petrole',3.0);
INSERT INTO Cargaison VALUES (CONCAT('CA',NEXT VALUE FOR idcarg),'Passager',1.5);
INSERT INTO Cargaison VALUES (CONCAT('CA',NEXT VALUE FOR idcarg),'Container',3.);
INSERT INTO Cargaison VALUES (CONCAT('CA',NEXT VALUE FOR idcarg),'Container',2.5);

INSERT INTO Prevision VALUES (CONCAT('P',NEXT VALUE FOR idpred),'CA3','Cargos Ro-Ro','14/01/2021 00:00:00','09:00:00',400.0,15.0)