USE ProductDB;
GO  

--Insert
CREATE PROCEDURE ins_Products   
    @RNumber int ,
	@Name nvarchar(20) ,
	@Unit nvarchar(10)   
AS   
    SET NOCOUNT ON;  
    INSERT Products (RNumber, Name, Unit)  
    VALUES (@RNumber,@Name, @Unit)  
GO  
CREATE PROCEDURE ins_Warehouses   
    @WNumber int ,
	@City nvarchar(20) ,
	@Street nvarchar(20) ,
	@Building int ,
	@NameStorekeeper nvarchar(40)  
AS   
    SET NOCOUNT ON;  
    INSERT Warehouses(WNumber, City, Street, Building, NameStorekeeper)  
    VALUES (@WNumber, @City, @Street, @Building, @NameStorekeeper) 
GO 
CREATE PROCEDURE ins_Stores   
    @SName nvarchar(30) ,
	@City nvarchar(20) ,
	@Street nvarchar(20) ,
	@Building int  
AS   
    SET NOCOUNT ON;  
    INSERT Stores(SName, City, Street, Building) 
    VALUES (@SName, @City, @Street, @Building)  
GO 
CREATE PROCEDURE ins_WarehouseAccounting   
    @ProductID int ,
	@WHousesID int ,  
	@Quantity int,
	@Cost int 	 
AS   
    SET NOCOUNT ON;  
    INSERT WarehouseAccounting(ProductID,WHousesID, Quantity,Cost)  
    VALUES (@ProductID,@WHousesID, @Quantity,@Cost)   
GO 
CREATE PROCEDURE ins_StoreAccounting   
    @ProductID int ,
	@StoreID int , 
	@Quantity int,
	@Cost int
AS   
    SET NOCOUNT ON;  
    INSERT StoreAccounting(ProductID,StoreID, Quantity,Cost)  
    VALUES (@ProductID,@StoreID, @Quantity,@Cost)  
GO 


--Delete
CREATE PROCEDURE del_Products   
    @ProductID int  
AS   
    DELETE FROM Products 
	WHERE ProductID = @ProductID;  
GO  
CREATE PROCEDURE del_Warehouses   
    @WHousesID int  
AS   
    DELETE FROM Warehouses 
	WHERE WHousesID = @WHousesID;  
GO 
CREATE PROCEDURE del_Stores   
    @StoreID int  
AS   
    DELETE FROM Stores 
	WHERE StoreID = @StoreID;  
GO 
CREATE PROCEDURE del_WarehouseAccounting   
    @ProductID int ,
	@WHousesID int 
AS   
    DELETE FROM WarehouseAccounting 
	WHERE (ProductID = @ProductID)AND(WHousesID = @WHousesID) ;    
GO 
CREATE PROCEDURE del_StoreAccounting   
    @ProductID int ,
	@StoreID int 
AS   
    DELETE FROM StoreAccounting 
	WHERE (ProductID = @ProductID)AND(StoreID = @StoreID);  
GO 


--Update
CREATE PROCEDURE up_Products 
	@ProductID int 	,
    @RNumber int ,
	@Name nvarchar(20) ,
	@Unit nvarchar(10)   
AS   
	UPDATE Products  
	SET RNumber = @RNumber, Name = @Name,Unit=@Unit   
	WHERE ProductID = @ProductID ; 
GO  
CREATE PROCEDURE up_Warehouses  
    @WHousesID int ,
    @WNumber int ,
	@City nvarchar(20) ,
	@Street nvarchar(20) ,
	@Building int ,
	@NameStorekeeper nvarchar(40)  
AS   
    UPDATE Warehouses  
	SET WNumber = @WNumber, City = @City, Street=@Street, 
	Building = @Building, NameStorekeeper=@NameStorekeeper
	WHERE WHousesID = @WHousesID ;  
GO 
CREATE PROCEDURE up_Stores   
    @StoreID int ,
    @SName nvarchar(30) ,
	@City nvarchar(20) ,
	@Street nvarchar(20) ,
	@Building int 
AS   
    UPDATE Stores  
	SET SName = @SName, City = @City, Street=@Street, 
	Building = @Building   
	WHERE StoreID = @StoreID ;   
GO 
CREATE PROCEDURE up_WarehouseAccounting   
    @ProductID int ,
	@WHousesID int ,  
	@Quantity int,
	@Cost int 	 
AS   
    UPDATE WarehouseAccounting  
	SET Quantity = @Quantity, Cost = @Cost   
	WHERE (ProductID = @ProductID)AND(WHousesID = @WHousesID);    
GO 
CREATE PROCEDURE up_StoreAccounting   
    @ProductID int ,
	@StoreID int ,
	@Quantity int,
	@Cost int
AS   
    UPDATE StoreAccounting  
	SET Quantity = @Quantity, Cost = @Cost   
	WHERE (ProductID = @ProductID)AND(StoreID = @StoreID);
GO



--Views
USE ProductDB;
GO 
CREATE FUNCTION v_StoreAccounting 
	(@StoreID int)
    RETURNS TABLE
    AS RETURN ( SELECT Products.Name,  StoreAccounting.Quantity, StoreAccounting.Cost
	FROM StoreAccounting INNER JOIN Products ON StoreAccounting.ProductID = Products.ProductID
	WHERE StoreAccounting.StoreID= @StoreID)
	GO 
CREATE FUNCTION v_WarehouseAccounting 
	(@WHousesID int)
    RETURNS TABLE
    AS RETURN ( SELECT Products.Name,  WarehouseAccounting.Quantity, WarehouseAccounting.Cost
	FROM WarehouseAccounting INNER JOIN Products ON WarehouseAccounting.ProductID = Products.ProductID
	WHERE WarehouseAccounting.WHousesID= @WHousesID
	ORDER BY Products.Name)
	GO 
