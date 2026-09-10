

CREATE PROCEDURE [GeneralLedger].[SP_CONTABILIDAD_VENTAS]
@Year char(4)
AS

/*
SELECT [Nit]
      ,[Cliente]
      ,[CentroCosto]
      ,[Descripcion Centro Costo]
      ,[Centro Atencion]
      ,[Agrupador]
      ,[AÑO]
      ,[ENERO]
      ,[FEBRERO]
      ,[MARZO]
      ,[ABRIL]
      ,[MAYO]
      ,[JUNIO]
      ,[JULIO]
      ,[AGOSTO]
      ,[SEPTIEMBRE]
      ,[OCTUBRE]
      ,[NOVIEMBRE]
      ,[DICIEMBRE]
      ,[Origen]
      ,[Logica]
	FROM [INDIGODWH].[GeneralLedger].[STG_CONTABILIDAD_VENTAS]
	WHERE [AÑO] = @Year 
*/

WITH CTE_AÑO_ACTUAL
	AS
	(
	SELECT ISNULL(Nit,0) [NIT], [Cliente], [CodigoCentroCosto], [CentroCosto], [CentroAtencion] ,-- Agrupador, 
	@Year [Año],
	ISNULL([1],0) AS 'Enero',
	ISNULL([2],0) AS 'Febrero',
	ISNULL([3],0) AS 'Marzo',
	ISNULL([4],0) AS 'Abril',
	ISNULL([5],0) AS 'Mayo',
	ISNULL([6],0) AS 'Junio',
	ISNULL([7],0) AS 'Julio',
	ISNULL([8],0) AS 'Agosto',
	ISNULL([9],0) AS 'Septiembre',
	ISNULL([10],0) AS 'Octubre',
	ISNULL([11],0) AS 'Noviembre',
	ISNULL([12],0) AS 'Diciembre', 
	'Contabilidad' [Origen],
	'Ventas ejecutadas' as [Logica]
	FROM
	(
	SELECT  sc.Month AS MES, t.Nit, t.Name AS Cliente, cco.Code AS [CodigoCentroCosto], cco.Name as [CentroCosto],BO.Name AS [CentroAtencion] ,--AC.DESCRIPCION AS Agrupador,
	CASE WHEN sc.Month = '1' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '2' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '3' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '4' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '5' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '6' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '7' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '8' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '9' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1  
		 WHEN sc.Month = '10' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '11' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '12' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 END ACUMULADO
		FROM   .GeneralLedger.GeneralLedgerBalance AS sc WITH (nolock) INNER JOIN
		.Common.ThirdParty AS t WITH (nolock) ON t .Id = sc.IdThirdParty LEFT OUTER JOIN
		.Payroll.CostCenter AS cco WITH (nolock) ON cco.Id = sc.IdCostCenter LEFT OUTER JOIN
		.Payroll.BranchOffice  AS bo WITH (nolock) ON cco.BranchOfficeId =BO.Id LEFT OUTER JOIN
		.GeneralLedger.MainAccounts AS c WITH (nolock) ON c.Id = sc.IdMainAccount --LEFT OUTER JOIN
		--INDIGOREP.DBO.DWH_AGRUPADORES_CONTABILIDAD AS AC WITH (nolock) ON AC.CODECC=cco.Code 
		WHERE (t .PersonType = '2') AND	(c.LegalBookId = '1')
		AND (c.Number BETWEEN '41' AND '41749999') AND YEAR = @Year
		GROUP BY t.Nit,t .Name, cco.Code, cco.name, sc.Month, sc.Year,BO.Name--,AC.DESCRIPCION

	UNION

	SELECT  sc.Month AS MES, 0  Nit, 'Personas Naturales' AS Cliente, cco.Code AS CentroCosto, cco.Name as [Descripcion Centro Costo],BO.Name AS [Centro Atencion] ,--AC.DESCRIPCION AS Agrupador,
		CASE WHEN sc.Month = '1' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '2' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '3' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '4' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '5' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '6' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '7' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '8' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '9' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1  
		 WHEN sc.Month = '10' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '11' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '12' AND sc.Year = @year THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 END ACUMULADO
		FROM   .GeneralLedger.GeneralLedgerBalance AS sc WITH (nolock) INNER JOIN
		.Common.ThirdParty AS t WITH (nolock) ON t .Id = sc.IdThirdParty LEFT OUTER JOIN
		.Payroll.CostCenter AS cco WITH (nolock) ON cco.Id = sc.IdCostCenter LEFT OUTER JOIN
		.Payroll.BranchOffice  AS bo WITH (nolock) ON cco.BranchOfficeId =BO.Id LEFT OUTER JOIN
		.GeneralLedger.MainAccounts AS c WITH (nolock) ON c.Id = sc.IdMainAccount --LEFT OUTER JOIN
		--INDIGOREP.DBO.DWH_AGRUPADORES_CONTABILIDAD AS AC WITH (nolock) ON AC.CODECC=cco.Code 
		WHERE (t .PersonType = '1') AND	(c.LegalBookId = '1')
		AND (c.Number BETWEEN '41' AND '41749999')  AND YEAR=  @Year
		GROUP BY  cco.Code, cco.name, sc.Month, sc.Year,BO.Name--,AC.DESCRIPCION
		) DET
		PIVOT
		(
		SUM(ACUMULADO)
		FOR MES IN ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12])) AS piv
		--ORDER BY 1,2,3,4,5,6
	)

	, CTE_AÑO_ANTERIOR
	AS
	(
	SELECT ISNULL(Nit,0) [NIT], [Cliente], [CodigoCentroCosto], [CentroCosto], [CentroAtencion] ,-- Agrupador,
	@Year -1 [Año],
	ISNULL([1],0) AS 'Enero',
	ISNULL([2],0) AS 'Febrero',
	ISNULL([3],0) AS 'Marzo',
	ISNULL([4],0) AS 'Abril',
	ISNULL([5],0) AS 'Mayo',
	ISNULL([6],0) AS 'Junio',
	ISNULL([7],0) AS 'Julio',
	ISNULL([8],0) AS 'Agosto',
	ISNULL([9],0) AS 'Septiembre',
	ISNULL([10],0) AS 'Octubre',
	ISNULL([11],0) AS 'Noviembre',
	ISNULL([12],0) AS 'Diciembre', 
	'Contabilidad' [Origen],
	'Ventas ejecutadas' as [Logica]
	FROM
	(
	SELECT  sc.Month AS MES, t.Nit, t.Name AS Cliente, cco.Code AS [CodigoCentroCosto], cco.Name as [CentroCosto],BO.Name AS [CentroAtencion],--AC.DESCRIPCION AS Agrupador,
	CASE WHEN sc.Month = '1' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '2' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '3' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '4' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '5' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '6' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
         WHEN sc.Month = '7' AND sc.Year = 2020 THEN 0
		 WHEN sc.Month = '7' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '8' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '9' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1  
		 WHEN sc.Month = '10' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '11' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '12' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 END ACUMULADO
		FROM  .GeneralLedger.GeneralLedgerBalance AS sc WITH (nolock) INNER JOIN
		.Common.ThirdParty AS t WITH (nolock) ON t .Id = sc.IdThirdParty LEFT OUTER JOIN
		.Payroll.CostCenter AS cco WITH (nolock) ON cco.Id = sc.IdCostCenter LEFT OUTER JOIN
		.Payroll.BranchOffice  AS bo WITH (nolock) ON cco.BranchOfficeId =BO.Id LEFT OUTER JOIN
		.GeneralLedger.MainAccounts AS c WITH (nolock) ON c.Id = sc.IdMainAccount --LEFT OUTER JOIN
		--INDIGOREP.DBO.DWH_AGRUPADORES_CONTABILIDAD AS AC WITH (nolock) ON AC.CODECC=cco.Code 
		WHERE (t .PersonType = '2') AND	(c.LegalBookId = '1')
		AND (c.Number BETWEEN '41' AND '41749999')  AND YEAR= @Year - 1
		GROUP BY t.Nit,t .Name, cco.Code, cco.name, sc.Month, sc.Year,BO.Name--,AC.DESCRIPCION

	UNION

	SELECT  sc.Month AS MES, 0  Nit, 'Personas Naturales' AS Cliente, cco.Code AS CentroCosto, cco.Name as [Descripcion Centro Costo],BO.Name AS [Centro Atencion] ,--AC.DESCRIPCION AS Agrupador,
		CASE WHEN sc.Month = '1' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '2' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '3' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '4' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '5' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '6' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '7' AND sc.Year = 2020 THEN 0
		 WHEN sc.Month = '7' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '8' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '9' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1  
		 WHEN sc.Month = '10' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '11' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 WHEN sc.Month = '12' AND sc.Year = @year - 1 THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 
		 END ACUMULADO
		FROM   .GeneralLedger.GeneralLedgerBalance AS sc WITH (nolock) INNER JOIN
		.Common.ThirdParty AS t WITH (nolock) ON t .Id = sc.IdThirdParty LEFT OUTER JOIN
		.Payroll.CostCenter AS cco WITH (nolock) ON cco.Id = sc.IdCostCenter LEFT OUTER JOIN
		.Payroll.BranchOffice  AS bo WITH (nolock) ON cco.BranchOfficeId =BO.Id LEFT OUTER JOIN
		.GeneralLedger.MainAccounts AS c WITH (nolock) ON c.Id = sc.IdMainAccount --LEFT OUTER JOIN
		--INDIGOREP.DBO.DWH_AGRUPADORES_CONTABILIDAD AS AC WITH (nolock) ON AC.CODECC=cco.Code 
		WHERE (t .PersonType = '1') AND	(c.LegalBookId = '1')
		AND (c.Number BETWEEN '41' AND '41749999')  AND YEAR= @Year - 1
		GROUP BY  cco.Code, cco.name, sc.Month, sc.Year,BO.Name--,AC.DESCRIPCION
		) DET
		PIVOT
		(
		SUM(ACUMULADO)
		FOR MES IN ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12])) AS piv
		--ORDER BY 1,2,3,4,5,6
	)

	SELECT * FROM CTE_AÑO_ANTERIOR 
	UNION ALL
	SELECT * FROM CTE_AÑO_ACTUAL --ORDER BY 1,2,3,4,5,6,7
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte contable de ventas ejecutadas por cliente, centro de costo y sucursal para un año fiscal dado (y el año anterior como comparativo). Consulta los saldos del libro mayor (débitos y créditos de cuentas de ingresos entre 41 y 41749999 del libro legal), los cruza con terceros (clientes jurídicos agrupados por NIT y personas naturales consolidadas), centros de costo y sedes, y presenta los valores mes a mes en columnas (enero a diciembre). Útil para análisis de ingresos contables, seguimiento de ventas por aseguradora o contratista, y comparación anual de facturación. Recibe como parámetro el año (@Year) y devuelve una fila por cliente-centro de costo con su facturación mensual acumulada.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_CONTABILIDAD_VENTAS';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_CONTABILIDAD_VENTAS';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte pivotado de ventas contables ejecutadas por cliente, centro de costo y centro de atención, con detalle mensual (enero-diciembre) para el año solicitado y el año inmediatamente anterior.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_CONTABILIDAD_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de año debe corresponder a un año válido con movimientos en GeneralLedgerBalance; Debe existir el LegalBookId = 1 (libro legal principal); Las cuentas de ingresos deben estar codificadas en el rango ''41''..''41749999''; Los terceros deben tener PersonType clasificado como ''1'' (natural) o ''2'' (jurídica)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_CONTABILIDAD_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen movimientos cuya cuenta principal está entre ''41'' y ''41749999'' (cuentas de ingresos operacionales); Solo se consideran cuentas con LegalBookId = 1 (libro oficial); Las personas naturales (PersonType=''1'') siempre se consolidan con NIT=0 y nombre genérico ''Personas Naturales''; El saldo de ventas se expresa como (Crédito - Débito), invirtiendo el signo natural contable mediante multiplicación por -1; Se devuelven siempre 12 columnas mensuales (Enero a Diciembre); meses sin movimiento se rellenan con 0; Cada fila del resultado se etiqueta con Origen=''Contabilidad'' y Logica=''Ventas ejecutadas''; Para julio del año 2020 (cuando se calcula como año anterior), el valor se neutraliza a 0', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_CONTABILIDAD_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Contabilidad; Ventas ejecutadas; Centro de costo; Centro de atención (BranchOffice); Tercero/Cliente; NIT; Persona natural vs jurídica; Cuentas contables (clase 4 - ingresos); Libro legal (LegalBookId); Débito y Crédito', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_CONTABILIDAD_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve UNION ALL de dos bloques (año anterior y año actual) con saldos mensuales pivotados de cuentas de ingresos (cuenta entre ''41'' y ''41749999'', LegalBookId=1), invirtiendo el signo (Debit-Credit)*-1', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_CONTABILIDAD_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ThirdParty.PersonType = ''2'' (persona jurídica) → Se reporta el saldo desagregado por NIT y nombre del cliente real; si ThirdParty.PersonType = ''1'' (persona natural) → Se agrupa bajo NIT=0 y Cliente=''Personas Naturales'' (anonimización/consolidación); si sc.Month = ''7'' AND sc.Year = 2020 (en el bloque del año anterior) → Se fuerza el acumulado a 0 (excepción puntual de julio 2020) else Se calcula (SUM(Debit) - SUM(Credit)) * -1', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_CONTABILIDAD_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.GeneralLedgerBalance; Common.ThirdParty; Payroll.CostCenter; Payroll.BranchOffice; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_CONTABILIDAD_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_CONTABILIDAD_VENTAS';
-- GO
