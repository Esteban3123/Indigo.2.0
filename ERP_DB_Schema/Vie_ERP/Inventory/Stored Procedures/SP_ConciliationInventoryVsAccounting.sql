-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2017-10-19
-- Description:	Conciliación de los saldos del mes entre inventario y contabilidad
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ConciliationInventoryVsAccounting]
	@MonthClosed AS INT,
	@YearClosed AS INT
AS
BEGIN


 SET NOCOUNT ON;
   -- Crear tabla temporal
   IF OBJECT_ID('tempdb..#tableData') IS NOT NULL
       DROP TABLE #tableData;
   
   CREATE TABLE #tableData (
       [Year] int NOT NULL,
       [Month] [int] NOT NULL,
       [EntityName] varchar(250) not null,
       [Transaction] varchar(250) not null,
       [MainAccountId] int not null,
       [MainAccountNumber] varchar(50) not null,
       [DebitValue] decimal(20,2) not null,
       [CreditValue] decimal(20,2) not null,
       [Module] varchar(20) not null
   );
   
  -- Crear índices para mejorar rendimiento
   CREATE INDEX IX_tableData_Main ON #tableData ([Year], [Month], [Transaction], MainAccountId);
   
  -- Limpiamos datos existentes
   DELETE FROM Inventory.ClosedMonthModulesConciliation 
   WHERE Year = @YearClosed AND Month = @MonthClosed;
   


  -- Obtener datos de inventario
   INSERT INTO #tableData(Year, Month, EntityName, [Transaction], MainAccountId, MainAccountNumber, DebitValue, CreditValue, Module)
       EXEC Inventory.SP_ConciliationInventory @MonthClosed = @MonthClosed, @YearClosed = @YearClosed;
   
   -- Obtener datos de contabilidad
   INSERT INTO #tableData(Year, Month, EntityName, [Transaction], MainAccountId, MainAccountNumber, DebitValue, CreditValue, Module)
       EXEC Inventory.SP_ConciliationAccounting @MonthClosed = @MonthClosed, @YearClosed = @YearClosed;
   
   -- Usar CTEs para agrupar y pivotar datos
   ;WITH SourceData AS (
       SELECT  
           Year,
           Month,
           [Transaction],
           Module,
           MainAccountId,
           MainAccountNumber,
           SUM(DebitValue) DebitValue,
           SUM(CreditValue) CreditValue
       FROM #tableData
       GROUP BY Year, Month, [Transaction], Module, MainAccountNumber, MainAccountId
   ),
   PivotEntradas AS (
       SELECT 
           Year,
           Month,
           [Transaction],
           MainAccountId,
           MainAccountNumber,
           ISNULL(Accounting, 0) AS TotalDebitAccounting,
           ISNULL(Inventory, 0) AS TotalDebitInventory
       FROM 
           SourceData
       PIVOT (
           SUM(DebitValue) 
           FOR Module IN (Accounting, Inventory)
       ) AS pvt
   ),
   PivotSalidas AS (
       SELECT 
           Year,
           Month,
           [Transaction],
           MainAccountId,
           MainAccountNumber,
           ISNULL(Accounting, 0) AS TotalCreditAccounting,
           ISNULL(Inventory, 0) AS TotalCreditInventory
       FROM 
           SourceData
       PIVOT (
           SUM(CreditValue) 
           FOR Module IN (Accounting, Inventory)
       ) AS pvt
   )
   -- Insertar resultados conciliados
   INSERT INTO Inventory.ClosedMonthModulesConciliation
   SELECT DISTINCT
       e.Year,
       e.Month,
       e.[Transaction],
       e.MainAccountId,
       e.TotalDebitAccounting,
       s.TotalCreditAccounting,
       e.TotalDebitInventory,
       s.TotalCreditInventory
   FROM PivotEntradas e
   JOIN PivotSalidas s ON e.Year = s.Year 
       AND e.Month = s.Month 
       AND e.[Transaction] = s.[Transaction] 
       AND e.MainAccountNumber = s.MainAccountNumber;
   
   -- Devolver resultados
   SELECT * FROM Inventory.ClosedMonthModulesConciliation 
   WHERE Year = @YearClosed AND Month = @MonthClosed;
   
   -- Limpieza tabla temporal
   DROP TABLE #tableData;

END