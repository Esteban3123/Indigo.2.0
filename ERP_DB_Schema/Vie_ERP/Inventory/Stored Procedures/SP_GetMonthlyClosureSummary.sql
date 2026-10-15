


-- =============================================
-- Author:		Oscar Astudillo reyes
-- Create date: 20/08/2024
-- Description:	Procedimiento que se encarga de obtener el resumen del cierre mensual por mes y año
-- =============================================


CREATE PROCEDURE [Inventory].[SP_GetMonthlyClosureSummary]
    @Year INT,
    @Month INT
AS
BEGIN
    WITH PreviousBalance AS (
        -- Calcula el saldo anterior, la formula es que el saldo anterior  es el nuevo saldo del mes anterior
		-- el nuevo saldo se calcula  movimientos debito - movimientos creditos
		-- Por lo tanto se calcula el nuevo saldo del mes anterior para obtener el saldo anterior del mes ingresado
        SELECT 
            'Contabilidad' AS Module,
            COALESCE(
                (
                    SELECT TOP 1 
                        SUM(TotalDebitAccounting - TotalCreditAccounting)
                    FROM Inventory.ClosedMonthModulesConciliation C1
                    WHERE (C1.Year < @Year OR (C1.Year = @Year AND C1.Month < @Month))
                    GROUP BY C1.Year, C1.Month
                    ORDER BY C1.Year DESC, C1.Month DESC
                ), 0
            ) AS PreviousBalance
        UNION ALL
        SELECT 
            'Inventario' AS Module,
            COALESCE(
                (
                    SELECT TOP 1 
                        SUM(TotalDebitInventory - TotalCreditInventory)
                    FROM Inventory.ClosedMonthModulesConciliation C1
                    WHERE (C1.Year < @Year OR (C1.Year = @Year AND C1.Month < @Month))
                    GROUP BY C1.Year, C1.Month
                    ORDER BY C1.Year DESC, C1.Month DESC
                ), 0
            ) AS PreviousBalance
    ),
    CurrentMonthTransactions AS (
        -- Calculo de los movimientos del mes actual para cada módulo
        SELECT 
            'Contabilidad' AS Module,
            SUM(TotalDebitAccounting) AS TotalDebitEntry,
            SUM(TotalCreditAccounting) AS TotalCreditOutput
        FROM Inventory.ClosedMonthModulesConciliation 
        WHERE Year = @Year AND Month = @Month
        UNION ALL
        SELECT 
            'Inventario' AS Module,
            SUM(TotalDebitInventory) AS TotalDebitEntry,
            SUM(TotalCreditInventory) AS TotalCreditOutput
        FROM Inventory.ClosedMonthModulesConciliation 
        WHERE Year = @Year AND Month = @Month
    )
    SELECT 
        t.Module,
        CASE
            WHEN sa.PreviousBalance = 0 THEN 'Sin saldo inicial'
            ELSE CAST(sa.PreviousBalance AS VARCHAR)
        END AS PreviousBalance,
        t.TotalDebitEntry AS Debits,
        t.TotalCreditOutput AS Credits,
        CAST(sa.PreviousBalance AS DECIMAL) + t.TotalDebitEntry - t.TotalCreditOutput AS NewBalance
    FROM CurrentMonthTransactions t
    LEFT JOIN PreviousBalance sa ON t.Module = sa.Module;
END