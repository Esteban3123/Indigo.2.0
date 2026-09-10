

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el resumen del cierre mensual de inventario para un mes y año específicos, comparando los movimientos contables versus los movimientos de inventario. Calcula tres indicadores clave por módulo (Contabilidad e Inventario): el saldo anterior (nuevo saldo acumulado del mes previo), los débitos y créditos del mes en curso, y el nuevo saldo resultante. Consume la tabla de conciliación de cierres mensuales (ClosedMonthModulesConciliation) para detectar diferencias o cuadres entre el módulo de contabilidad y el módulo de inventario. Se utiliza en el proceso de cierre de mes para validar que los totales de ambos módulos estén conciliados y así garantizar la integridad financiera del inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GetMonthlyClosureSummary';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GetMonthlyClosureSummary';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un resumen del cierre mensual por módulo (Contabilidad e Inventario) mostrando saldo anterior, débitos, créditos y nuevo saldo para el año y mes indicados.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GetMonthlyClosureSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la tabla Inventory.ClosedMonthModulesConciliation con datos previos para calcular saldo anterior; si no existen, se asume 0.; Los parámetros de año y mes deben corresponder a un periodo válido para que se obtengan movimientos del mes actual.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GetMonthlyClosureSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El saldo anterior de un mes corresponde al nuevo saldo del último mes cerrado previo (Year/Month inmediatamente anterior con datos).; El nuevo saldo se calcula siempre como: saldo anterior + débitos del mes - créditos del mes.; Se reportan exactamente dos módulos: ''Contabilidad'' e ''Inventario'', con sus totales calculados desde columnas distintas (Accounting vs Inventory).; Si no existe ningún mes previo registrado, el saldo anterior se asume 0.; Solo se consideran movimientos del periodo exacto Year=@Year y Month=@Month para débitos y créditos del mes actual.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GetMonthlyClosureSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cierre mensual; Conciliación de módulos; Contabilidad; Inventario; Saldo anterior; Movimientos débito; Movimientos crédito; Nuevo saldo', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GetMonthlyClosureSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por módulo (''Contabilidad'' e ''Inventario'') con PreviousBalance (texto ''Sin saldo inicial'' si es 0), Debits, Credits y NewBalance calculado como PreviousBalance + Debits - Credits.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GetMonthlyClosureSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PreviousBalance = 0 para el módulo → Se muestra el literal ''Sin saldo inicial'' en lugar del valor numérico else Se muestra el saldo anterior convertido a VARCHAR', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GetMonthlyClosureSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ClosedMonthModulesConciliation', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GetMonthlyClosureSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GetMonthlyClosureSummary';
-- GO
