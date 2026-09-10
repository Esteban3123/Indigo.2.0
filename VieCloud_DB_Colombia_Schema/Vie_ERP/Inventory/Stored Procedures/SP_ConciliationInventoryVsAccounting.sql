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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que ejecuta la conciliación mensual de saldos entre el módulo de inventario y la contabilidad para un mes y año de cierre indicados. Recopila los movimientos contables llamando a SP_ConciliationAccounting y los movimientos de inventario llamando a SP_ConciliationInventory, luego agrupa y pivota los débitos y créditos por tipo de transacción y cuenta contable (cuenta mayor) para comparar ambos módulos lado a lado. Los resultados conciliados se almacenan en la tabla ClosedMonthModulesConciliation, reemplazando cualquier dato previo del mismo período, y se devuelven al llamador para su revisión o reporte. Sirve para detectar diferencias entre lo registrado en inventario y lo registrado en contabilidad al momento del cierre mensual.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ConciliationInventoryVsAccounting';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ConciliationInventoryVsAccounting';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reconstruye y devuelve la conciliación mensual de saldos débito/crédito por cuenta contable y tipo de transacción comparando los módulos de Inventario y Contabilidad.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConciliationInventoryVsAccounting';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los procedimientos auxiliares de conciliación de Inventario y Contabilidad deben devolver resultsets compatibles con la estructura temporal (mismo orden y tipos de columnas).; Debe existir información de movimientos para el año y mes solicitados en los módulos Inventory y Accounting.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConciliationInventoryVsAccounting';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La conciliación se reconstruye completamente para el período: primero se borran filas existentes del mismo año/mes y luego se reinsertan.; Los totales se agrupan por Año, Mes, Transacción, Módulo y cuenta contable principal antes de pivotar.; Los módulos pivotados son exactamente ''Accounting'' e ''Inventory''; valores ausentes se sustituyen por 0.; El emparejamiento entre débitos y créditos se realiza por Año, Mes, Transacción y MainAccountNumber.; Los resultados devueltos corresponden únicamente al período solicitado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConciliationInventoryVsAccounting';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Conciliación contable; Cierre mensual; Inventario; Contabilidad; Cuenta contable principal; Débito; Crédito', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConciliationInventoryVsAccounting';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] Inventory.ClosedMonthModulesConciliation: Antes de recalcular, elimina todas las filas cuyo Year y Month coincidan con los parámetros recibidos.; [INSERT] Inventory.ClosedMonthModulesConciliation: Inserta una fila por combinación de Año/Mes/Transacción/Cuenta principal con los totales pivotados de débito y crédito de Contabilidad e Inventario, uniendo entradas y salidas por Year+Month+Transaction+MainAccountNumber.; [RETURN_RESULT] Inventory.ClosedMonthModulesConciliation: Tras la inserción, devuelve todas las filas conciliadas del período solicitado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConciliationInventoryVsAccounting';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.SP_ConciliationInventory; Inventory.SP_ConciliationAccounting', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConciliationInventoryVsAccounting';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ClosedMonthModulesConciliation', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConciliationInventoryVsAccounting';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConciliationInventoryVsAccounting';
-- GO
