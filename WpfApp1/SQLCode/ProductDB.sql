--1. Создать базу данных интернет магазина InternetShopDB.
/*
CREATE DATABASE ProductDB               
COLLATE Cyrillic_General_CI_AS
GO
*/
--2. Создать в базе данных таблицы
CREATE TABLE Products
(
	ProductID int NOT NULL IDENTITY,
	RNumber int NOT NULL,
	Name nvarchar(20) NULL,
	Unit nvarchar(10) NULL,
)  
GO

CREATE TABLE Warehouses
(
	WHousesID int NOT NULL IDENTITY,
	WNumber int NOT NULL,
	City nvarchar(20) NOT NULL,
	Street nvarchar(20) NOT NULL,
	Building int NOT NULL,
	NameStorekeeper nvarchar(40) NULL,
)  
GO

CREATE TABLE Stores
(                                      
	StoreID int NOT NULL IDENTITY,  
	SName nvarchar(30) NOT NULL,
	City nvarchar(20) NOT NULL,
	Street nvarchar(20) NOT NULL,
	Building int NOT NULL,
)
GO	

CREATE TABLE StoreAccounting
(
	ProductID int NOT NULL,
	StoreID int NOT NULL, 
	Quantity int DEFAULT 0,
	Cost int DEFAULT 0
)
GO

CREATE TABLE WarehouseAccounting
(
	ProductID int NOT NULL,
	WHousesID int NOT NULL,  
	Quantity int DEFAULT 0,
	Cost int DEFAULT 0	
)
GO



--3. Индексация БД.

CREATE INDEX IX_Products_ID on Products(ProductID);

CREATE INDEX IX_Warehouses_ID on Warehouses(WHousesID);

CREATE INDEX IX_Stores_ID on Stores(StoreID);

CREATE INDEX IX_StoreAccounting_ProductID on StoreAccounting(ProductID);

CREATE INDEX IX_StoreAccounting_StoreID on StoreAccounting(StoreID);

CREATE INDEX IX_WarehouseAccounting_ProductID on WarehouseAccounting(ProductID);

CREATE INDEX IX_WarehouseAccounting_WHousesID on WarehouseAccounting(WHousesID);


--4. Установить связи между таблицами.

--Products
ALTER TABLE Products ADD 
	CONSTRAINT PK_Products PRIMARY KEY(ProductID) 
GO

--Warehouses
ALTER TABLE Warehouses ADD 
	CONSTRAINT PK_Warehouses PRIMARY KEY(WHousesID) 
GO

--Stores
ALTER TABLE Stores ADD 
	CONSTRAINT PK_Stores PRIMARY KEY(StoreID)
GO


--StoreAccounting
ALTER TABLE StoreAccounting ADD CONSTRAINT
	PK_StoreAccounting PRIMARY KEY
	(ProductID,StoreID) 
GO

ALTER TABLE StoreAccounting ADD CONSTRAINT
	FK_StoreAccounting_Products FOREIGN KEY(ProductID) 
	REFERENCES Products(ProductID) 
	ON DELETE CASCADE  
GO

ALTER TABLE StoreAccounting ADD CONSTRAINT
	FK_StoreAccounting_Stores FOREIGN KEY(StoreID) 
	REFERENCES Stores(StoreID)
	ON DELETE CASCADE  
GO


--WarehouseAccounting
ALTER TABLE WarehouseAccounting ADD CONSTRAINT
	PK_WarehouseAccounting PRIMARY KEY
	(ProductID,WHousesID) 
GO

ALTER TABLE WarehouseAccounting ADD CONSTRAINT
	FK_WarehouseAccounting_Products FOREIGN KEY(ProductID) 
	REFERENCES Products(ProductID) 
	ON DELETE CASCADE  
GO

ALTER TABLE WarehouseAccounting ADD CONSTRAINT
	FK_WarehouseAccounting_Warehouses FOREIGN KEY(WHousesID) 
	REFERENCES Warehouses(WHousesID)
	ON DELETE CASCADE  
GO



--5. Установить ограничения в таблицах.

--Products
ALTER TABLE Products ADD CONSTRAINT CK_Products 
	CHECK((RNumber !='')AND(RNumber >0) AND (RNumber<=9999999999)) 
GO


--Warehouses
ALTER TABLE Warehouses ADD CONSTRAINT CK_Warehouses 
	CHECK((WNumber>0) AND (WNumber<=9999999999) AND (Building>0) AND (Building<=99999))
GO


--Stores
ALTER TABLE Stores ADD CONSTRAINT CK_Stores 
	CHECK((Building>0) AND (Building<=99999))
GO


--StoreAccounting
ALTER TABLE StoreAccounting ADD CONSTRAINT CK_StoreAccounting
	CHECK((Quantity>=0) AND (Quantity<=9999999999) AND (Cost>=0) AND (Cost<=9999999999))
GO


--WarehouseAccounting
ALTER TABLE WarehouseAccounting ADD CONSTRAINT CK_WarehouseAccounting 
	CHECK((Quantity>=0) AND (Quantity<=9999999999) AND (Cost>=0) AND (Cost<=9999999999))
GO
