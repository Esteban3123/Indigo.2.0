

CREATE VIEW [dbo].[VIE_AD_Ledger_BalanceGeneral2017]
AS
SELECT  c.Number AS Auxiliar, cco.Code AS CentroCosto, c.Name AS [Nombre auxiliar], cl.Code AS CodClase, cl.Name AS Clase, LEFT(c.Number, 2) AS Grupo,
               (SELECT  Name
              FROM     GeneralLedger.MainAccounts
              WHERE   (Number = LEFT(c.Number, 2)) AND (LegalBookId = '1')) AS [Nombre Grupo], LEFT(c.Number, 4) AS Cuenta,
               (SELECT  Name
              FROM     GeneralLedger.MainAccounts AS MainAccounts_1
              WHERE   (Number = LEFT(c.Number, 4)) AND (LegalBookId = '1')) AS [Nombre Cuenta], LEFT(c.Number, 6) AS SubCuenta,
               (SELECT  Name
              FROM     GeneralLedger.MainAccounts AS MainAccounts_1
              WHERE   (Number = LEFT(c.Number, 6)) AND (LegalBookId = '1')) AS [Nombre SubCuenta], t .Nit, t .Name AS [Nombre tercero], CASE WHEN sc.Month = '14' AND sc.Year = '2016' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, 
           CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS [SaldosIniciales], CASE WHEN sc.Month = '1' AND sc.Year = '2017' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) 
           - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS Enero2017, CASE WHEN sc.Month = '2' AND sc.Year = '2017' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) 
           AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS Febrero2017, CASE WHEN sc.Month = '3' AND sc.Year = '2017' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, 
           CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS Marzo2017, CASE WHEN sc.Month = '4' AND 
           sc.Year = '2017' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS Abril2017, 
           CASE WHEN sc.Month = '5' AND sc.Year = '2017' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) 
           * IIF(cl.Nature = 2, - 1, 1) END AS Mayo2017, CASE WHEN sc.Month = '6' AND sc.Year = '2017' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) 
           - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS Junio2017, CASE WHEN sc.Month = '7' AND sc.Year = '2017' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) 
           AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS Julio2017, CASE WHEN sc.Month = '8' AND sc.Year = '2017' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, 
           CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS Agosto2017, CASE WHEN sc.Month = '9' AND sc.Year = '2017' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) 
           AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS Septiembre2017, CASE WHEN sc.Month = '10' AND sc.Year = '2017' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), 
           CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS Octubre2017, CASE WHEN sc.Month = '11' AND sc.Year = '2017' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) 
           - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS Noviembre2017, CASE WHEN sc.Month = '12' AND sc.Year = '2017' THEN IIF(cl.Nature <> c.Nature, 
           IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS Diciembre2017
FROM    GeneralLedger.GeneralLedgerBalance AS sc WITH (nolock) LEFT OUTER JOIN
           Common.ThirdParty AS t ON t .Id = sc.IdThirdParty LEFT OUTER JOIN
           GeneralLedger.MainAccounts AS c ON c.Id = sc.IdMainAccount INNER JOIN
           GeneralLedger.MainAccountClasses AS cl ON cl.Id = c.IdAccountClass LEFT OUTER JOIN
           Payroll.CostCenter AS cco ON cco.Id = sc.IdCostCenter
WHERE  (c.Number BETWEEN '11050501' AND '38105601') AND (c.LegalBookId = '1')
GROUP BY c.Number, t .Nit, t .Name, cco.Code, sc.Month, c.Name, cl.Code, cl.Name, sc.Year, c.Nature, cl.Nature
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting financiero que presenta el Balance General del año 2017 en formato pivotado por mes, tomando como saldo inicial el período 14 del cierre de 2016. Consolida saldos netos (débito menos crédito) por cuenta auxiliar del PUC —con su jerarquía de grupo, cuenta y subcuenta—, centro de costo, tercero (NIT) y clase contable, aplicando ajustes de signo según la naturaleza de la cuenta. Está restringida al rango de cuentas entre 11050501 y 38105601 del libro legal 1.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Ledger_BalanceGeneral2017';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Ledger_BalanceGeneral2017';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye un balance general mensual de 2017 (con saldos iniciales del periodo 14 de 2016) por auxiliar contable, tercero y centro de costo, ajustado según la naturaleza de la cuenta y de la clase.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Ledger_BalanceGeneral2017';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los saldos deben existir en GeneralLedgerBalance con LegalBookId=''1''; Las cuentas auxiliares deben tener Number entre ''11050501'' y ''38105601''; La cuenta debe tener una clase asociada (IdAccountClass) en MainAccountClasses; El periodo de saldos iniciales se identifica por Month=''14'' y Year=''2016''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Ledger_BalanceGeneral2017';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera cuentas del libro legal ''1'' (LegalBookId=''1''); Restringe el rango de cuentas auxiliares al intervalo ''11050501''..''38105601''; Los nombres de Grupo (2 dígitos), Cuenta (4 dígitos) y SubCuenta (6 dígitos) se obtienen del mismo plan de cuentas con LegalBookId=''1''; Los meses fuera del año 2017 (excepto el periodo 14/2016 usado como saldo inicial) producen NULL en las columnas mensuales; El signo del saldo siempre se ajusta combinando la naturaleza de la cuenta y la naturaleza de la clase contable', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Ledger_BalanceGeneral2017';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Balance general; Plan único de cuentas (PUC); Naturaleza contable (débito/crédito); Saldos iniciales; Tercero (NIT); Centro de costo; Clase de cuenta; Libro legal contable; Movimientos mensuales', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Ledger_BalanceGeneral2017';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] GeneralLedger.GeneralLedgerBalance: Devuelve por cada auxiliar/tercero/centro de costo el neto (Débito-Crédito o Crédito-Débito) según c.Nature, invirtiendo el signo cuando la naturaleza de la cuenta difiere de la de su clase y multiplicando por -1 cuando cl.Nature=2.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Ledger_BalanceGeneral2017';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si sc.Month=''14'' AND sc.Year=''2016'' → El valor calculado se asigna a la columna SaldosIniciales else Se evalúa cada mes de 2017 (1..12) para asignar a Enero2017..Diciembre2017; si c.Nature = 1 (cuenta de naturaleza débito) → Saldo = SUM(DebitValue) - SUM(CreditValue) else Saldo = SUM(CreditValue) - SUM(DebitValue); si cl.Nature <> c.Nature (naturaleza de la clase distinta a la de la cuenta) → Se multiplica el saldo por -1 para invertir el signo else Se conserva el signo calculado; si cl.Nature = 2 (clase de naturaleza crédito) → Se multiplica el resultado final por -1 else Se conserva el resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Ledger_BalanceGeneral2017';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.GeneralLedgerBalance; Common.ThirdParty; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Ledger_BalanceGeneral2017';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Ledger_BalanceGeneral2017';
GO
