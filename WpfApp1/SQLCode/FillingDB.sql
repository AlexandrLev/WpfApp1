USE ProductDB;
GO 
EXECUTE ins_Products 44, 'Fish', 'kg';
GO
EXECUTE ins_Products 55, 'Dogg', 'kg';
GO 
EXECUTE ins_Stores 'Sezon', 'Irkutsk', 'Gogolya', 30;
GO
EXECUTE ins_Stores 'Rinok', 'Angarsk', 'Pushkina', 20;

USE ProductDB;
GO 
EXECUTE ins_StoreAccounting  1,1,100,150;
GO
EXECUTE ins_StoreAccounting 3,1,10,250;
GO 
EXECUTE ins_StoreAccounting  1,2,200,120;

USE ProductDB;
GO 
  SELECT * FROM v_StoreAccounting(1);
