

CREATE PROCEDURE [Report].[SP_CONTABILIDAD_VENTAS]
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
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento de reporte que genera un resumen mensual pivotado de ventas contables para el año indicado y el año inmediatamente anterior, filtrando cuentas del rango 41 (ingresos) del libro legal. Distingue entre terceros jurídicos (personas jurídicas con NIT) y personas naturales, agrupando los valores netos (débitos menos créditos, invertidos de signo) por centro de costo y centro de atención. Retorna ambos períodos en un único resultado para comparación interanual.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_CONTABILIDAD_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_CONTABILIDAD_VENTAS';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte comparativo mensual (enero–diciembre) de ventas contables ejecutadas para el año solicitado y el año inmediatamente anterior, segmentando por terceros jurídicos identificados y agrupando a personas naturales en un único bloque.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_CONTABILIDAD_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de año debe ser un valor numérico válido de 4 caracteres usable en comparaciones contra GeneralLedgerBalance.Year y aritmética (@Year - 1).; Deben existir registros en GeneralLedger.GeneralLedgerBalance para el año solicitado y/o el anterior.; Las cuentas contables consultadas deben pertenecer al libro legal con LegalBookId = ''1'' y tener Number entre ''41'' y ''41749999'' (cuentas de ingresos/ventas).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_CONTABILIDAD_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen movimientos cuya cuenta principal pertenezca al libro legal ''1'' y al rango de cuentas ''41''..''41749999'' (clase 4 - ingresos).; El valor mensual presentado siempre es (Débito - Crédito) * -1, es decir, los ingresos se muestran como positivos.; Los meses sin movimiento se materializan como 0 mediante ISNULL tras el PIVOT.; El bloque del año anterior nunca reporta valor para julio de 2020 (siempre 0).; Las personas naturales nunca aparecen identificadas individualmente: se agregan bajo NIT 0 con etiqueta ''Personas Naturales''.; Todos los registros de salida llevan Origen=''Contabilidad'' y Logica=''Ventas ejecutadas''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_CONTABILIDAD_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ventas ejecutadas; Contabilidad general; Cuentas de ingresos (clase 4); Libro legal (LegalBookId); Tercero / Cliente (NIT); Persona jurídica vs persona natural (PersonType); Centro de costo; Centro de atención (sucursal); Débito y Crédito contable; Comparativo año actual vs año anterior', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_CONTABILIDAD_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve UNION ALL de dos bloques: año anterior (@Year-1) y año actual (@Year), cada uno con columnas pivote por mes (Enero..Diciembre), NIT, Cliente, CodigoCentroCosto, CentroCosto, CentroAtencion, Origen=''Contabilidad'' y Logica=''Ventas ejecutadas''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_CONTABILIDAD_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ThirdParty.PersonType = ''2'' (persona jurídica) → Se agrupa el saldo por NIT y nombre del tercero, mostrando cada cliente individualmente.; si ThirdParty.PersonType = ''1'' (persona natural) → Se consolida bajo NIT=0 y Cliente=''Personas Naturales'', sin desagregar por tercero.; si sc.Month = ''7'' AND sc.Year = 2020 (solo en bloque año anterior) → El acumulado del mes se fuerza a 0, excluyendo julio de 2020 del comparativo histórico. else Para los demás meses con sc.Year = @Year - 1 se calcula (SUM(DebitValue) - SUM(CreditValue)) * -1.; si Mes coincide con sc.Year = @Year (bloque año actual) → El acumulado mensual se calcula como (SUM(DebitValue) - SUM(CreditValue)) * -1, invirtiendo el signo contable para presentar las ventas en positivo.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_CONTABILIDAD_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.GeneralLedgerBalance; Common.ThirdParty; Payroll.CostCenter; Payroll.BranchOffice; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_CONTABILIDAD_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_CONTABILIDAD_VENTAS';
-- GO
