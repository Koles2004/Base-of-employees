
IF DB_ID('BaseOfEmployees') IS NOT NULL
DROP DATABASE BaseOfEmployees
GO

CREATE DATABASE BaseOfEmployees
GO

USE BaseOfEmployees
GO

CREATE TABLE Cities
(
	Id bigint IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Name nvarchar(50) NOT NULL
);

CREATE TABLE Streets
(
	Id bigint IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Name nvarchar(50) NOT NULL
);

CREATE TABLE Addresses
(
	Id bigint IDENTITY(1,1) NOT NULL PRIMARY KEY,
	CityFk bigint NOT NULL,
	StreetFk bigint NOT NULL,
	House nvarchar(20) NOT NULL

	FOREIGN KEY (CityFK) REFERENCES Cities(Id)
	ON DELETE CASCADE
	ON UPDATE CASCADE,

	FOREIGN KEY (StreetFK) REFERENCES Streets(Id)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);

CREATE TABLE Positions
(
	Id bigint IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Name nvarchar(50) NOT NULL,
	Salary bigint NOT NULL
);

CREATE TABLE Certifications
(
	Id bigint IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Name nvarchar(50) NOT NULL,
);

CREATE TABLE Employees
(
	Id bigint IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Surname nvarchar(20) NOT NULL,
	Name nvarchar(20) NOT NULL,
	Patronymic nvarchar(20) NOT NULL,
	Sex nvarchar(20) NOT NULL,
	Phone nvarchar(20) NOT NULL,
	DateOfBirth datetime NOT NULL,
	AddressFk bigint NOT NULL,
	PositionFk bigint NOT NULL,
	Photo nvarchar(100) NOT NULL

	FOREIGN KEY (AddressFK) REFERENCES Addresses(Id)
	ON DELETE CASCADE
	ON UPDATE CASCADE,

	FOREIGN KEY (PositionFK) REFERENCES Positions(Id)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);

CREATE TABLE CertificationsOfEmployees
(
	Id bigint IDENTITY(1,1) NOT NULL PRIMARY KEY,
	EmployeeFk bigint NOT NULL,
	CertificationFk bigint NOT NULL

	FOREIGN KEY (EmployeeFK) REFERENCES Employees(Id)
	ON DELETE CASCADE
	ON UPDATE CASCADE,

	FOREIGN KEY (CertificationFK) REFERENCES Certifications(Id)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);


/*Addresses, Cities, Streets*/
BEGIN TRANSACTION;
DECLARE @CityPrimaryKey int;
DECLARE @StreetPrimaryKey int;
INSERT INTO Cities(Name) VALUES ('Odessa');
SET @CityPrimaryKey = SCOPE_IDENTITY();
INSERT INTO Streets(Name) VALUES ('Kanatnaja');
SET @StreetPrimaryKey = SCOPE_IDENTITY();
INSERT INTO Addresses(CityFk, StreetFK, House)
VALUES (@CityPrimaryKey, @StreetPrimaryKey, '53'),
       (@CityPrimaryKey, @StreetPrimaryKey, '25/1');
	   
INSERT INTO Streets(Name) VALUES ('Koroleva');
SET @StreetPrimaryKey = SCOPE_IDENTITY();
INSERT INTO Addresses(CityFk, StreetFK, House)
VALUES (@CityPrimaryKey, @StreetPrimaryKey, '17'),
       (@CityPrimaryKey, @StreetPrimaryKey, '48');

INSERT INTO Streets(Name) VALUES ('Evreiskaya');
SET @StreetPrimaryKey = SCOPE_IDENTITY();
INSERT INTO Addresses(CityFk, StreetFK, House)
VALUES (@CityPrimaryKey, @StreetPrimaryKey, '20'),
       (@CityPrimaryKey, @StreetPrimaryKey, '50/2');

INSERT INTO Cities(Name) VALUES ('Lviv');
SET @CityPrimaryKey = SCOPE_IDENTITY();
INSERT INTO Streets(Name) VALUES ('Shevchenka');
SET @StreetPrimaryKey = SCOPE_IDENTITY();
INSERT INTO Addresses(CityFk, StreetFK, House)
VALUES (@CityPrimaryKey, @StreetPrimaryKey, '10'),
       (@CityPrimaryKey, @StreetPrimaryKey, '18');
 
IF @@ERROR <> 0
	ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
GO

INSERT INTO Positions(Name, Salary) VALUES('Director', 30000);
INSERT INTO Positions(Name, Salary) VALUES('Accountant', 15000);
INSERT INTO Positions(Name, Salary) VALUES('Lawyer', 10000);
INSERT INTO Positions(Name, Salary) VALUES('Recruiter', 9000);
INSERT INTO Positions(Name, Salary) VALUES('Driver', 7000);
INSERT INTO Positions(Name, Salary) VALUES('Loader', 7500);
INSERT INTO Positions(Name, Salary) VALUES('Seller', 8000);

INSERT INTO Certifications(Name) VALUES('Driver license B');
INSERT INTO Certifications(Name) VALUES('Driver license C');
INSERT INTO Certifications(Name) VALUES('Ph.D');
INSERT INTO Certifications(Name) VALUES('Doctor of Law');

INSERT INTO Employees(Surname, Name, Patronymic, Sex, AddressFk, Phone, DateOfBirth, PositionFk, Photo)
VALUES('Kovalenko', 'Ivan', 'Vladimirovich', 'M', 1, '123-456-789', '1980-05-07', 1, 'man2.jpg');
INSERT INTO Employees(Surname, Name, Patronymic, Sex, AddressFk, Phone, DateOfBirth, PositionFk, Photo)
VALUES('Tkachenko', 'Anna', 'Aleksandrovna', 'F', 2, '555-444-333', '1975-10-10', 2, 'woman1.jpg');
INSERT INTO Employees(Surname, Name, Patronymic, Sex, AddressFk, Phone, DateOfBirth, PositionFk, Photo)
VALUES('Ivanov', 'Nikolaj', 'Viktorovich', 'M', 3, '111-222-333', '1985-11-11', 3, 'man1.jpg');

INSERT INTO CertificationsOfEmployees(EmployeeFk, CertificationFk) VALUES(1, 2);
INSERT INTO CertificationsOfEmployees(EmployeeFk, CertificationFk) VALUES(2, 3);