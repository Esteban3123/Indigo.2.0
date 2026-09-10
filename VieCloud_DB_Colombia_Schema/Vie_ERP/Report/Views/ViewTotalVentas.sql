
CREATE view [Report].[ViewTotalVentas] as
SELECT
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
 ((sc.Year * 100) + sc.Month) [ID_TIEMPO_VENTAS],
 SUM(((sc.DebitValue - sc.CreditValue) * -1)) VENTAS 
FROM  
 GeneralLedger.GeneralLedgerBalance AS sc  
 INNER JOIN Common.ThirdParty AS t  ON t .Id = sc.IdThirdParty 
 LEFT OUTER JOIN GeneralLedger.MainAccounts AS c  ON c.Id = sc.IdMainAccount 
WHERE
 (t .PersonType IN ('1','2')) AND (c.LegalBookId = '1') AND 
 (c.Number BETWEEN '41' AND '41999999')
GROUP BY
 ((sc.Year * 100) + sc.Month)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte que consolida las ventas totales por período (año-mes) consultando saldos del libro mayor. Filtra terceros de tipo persona natural o jurídica (PersonType 1 y 2), cuentas del libro legal (LegalBookId = ''1'') en el rango 41 (ingresos según PUC colombiano), calculando ventas como la inversión del neto débito-crédito. Expone el nombre de la base de datos como identificador de compañía, orientada a consumo en reporting financiero o integración multisede.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTotalVentas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTotalVentas';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida el total de ventas por período contable (año-mes) a partir de los saldos del libro mayor de las cuentas de ingresos operacionales (clase 4) registradas a nombre de terceros persona natural o jurídica.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTotalVentas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'GeneralLedgerBalance debe contener saldos cargados con Year y Month válidos.; ThirdParty debe tener definido el campo PersonType con valores ''1'' o ''2'' para que el movimiento sea considerado.; MainAccounts debe tener LegalBookId definido y la columna Number debe permitir comparación lexicográfica/numérica en el rango 41-41999999.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTotalVentas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El período se representa como entero AAAAMM mediante (Year*100)+Month.; Las ventas se calculan invirtiendo el signo del saldo neto: (Debe - Haber) * -1, asumiendo naturaleza crédito de las cuentas de ingreso.; Solo se consideran movimientos cuyo tercero sea persona natural (''1'') o persona jurídica (''2'').; Solo se consideran cuentas del libro legal ''1'' (contabilidad oficial/fiscal).; Solo se incluyen cuentas contables del rango ''41'' a ''41999999'', correspondiente a la clase 4 ''Ingresos operacionales'' del PUC.; El identificador de compañía se toma dinámicamente del nombre de la base de datos en ejecución, truncado a 9 caracteres.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTotalVentas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ventas; Libro mayor contable; Plan de cuentas (PUC); Tercero (persona natural/jurídica); Período contable (año-mes); Cuentas de ingresos clase 4', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTotalVentas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewTotalVentas: Devuelve una fila por período (Year*100+Month) con la suma de (DebitValue-CreditValue)*-1 filtrando ThirdParty.PersonType IN (''1'',''2''), MainAccounts.LegalBookId=''1'' y MainAccounts.Number BETWEEN ''41'' AND ''41999999''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTotalVentas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.GeneralLedgerBalance; Common.ThirdParty; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTotalVentas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTotalVentas';
GO
