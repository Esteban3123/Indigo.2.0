

CREATE view [Report].[UploadCubeVieFinanceSalesAccounting]
AS

WITH CTE_AÑO_ACTUAL
	AS
	(
	SELECT ID_COMPANY,ISNULL(Nit,0) [NIT], [CLIENTE], [CODIGO CENTRO COSTO], [CENTRO COSTO], [CENTRO ATENCION] ,[AÑO],
	ISNULL([1],0) AS 'ENERO',
	ISNULL([2],0) AS 'FEBRERO',
	ISNULL([3],0) AS 'MARZO',
	ISNULL([4],0) AS 'ABRIL',
	ISNULL([5],0) AS 'MAYO',
	ISNULL([6],0) AS 'JUNIO',
	ISNULL([7],0) AS 'JULIO',
	ISNULL([8],0) AS 'AGOSTO',
	ISNULL([9],0) AS 'SEPTIEMBRE',
	ISNULL([10],0) AS 'OCTUBRE',
	ISNULL([11],0) AS 'NOVIEMBRE',
	ISNULL([12],0) AS 'DICIEMBRE', 
	'Contabilidad' [ORIGEN],
	'Ventas ejecutadas' as [LOGICA],
	CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM
	(
	SELECT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
	sc.Month AS MES, t.Nit, t.Name AS 'CLIENTE', cco.Code AS [CODIGO CENTRO COSTO], cco.Name as [CENTRO COSTO],BO.Name AS [CENTRO ATENCION] ,sc.year as AÑO,
	CASE WHEN sc.Month = '1' /*AND sc.Year = @year */ THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC) * - 1 
		 WHEN sc.Month = '2' /*AND sc.Year = @year */ THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC)* - 1 
		 WHEN sc.Month = '3' /*AND sc.Year = @year */ THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC)* - 1 
		 WHEN sc.Month = '4' /*AND sc.Year = @year */ THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC)* - 1 
		 WHEN sc.Month = '5' /*AND sc.Year = @year */ THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC)* - 1 
		 WHEN sc.Month = '6' /*AND sc.Year = @year */ THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC)* - 1 
		 WHEN sc.Month = '7' /*AND sc.Year = @year */ THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC)* - 1 
		 WHEN sc.Month = '8' /*AND sc.Year = @year */ THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC)* - 1 
		 WHEN sc.Month = '9' /*AND sc.Year = @year */ THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC)* - 1  
		 WHEN sc.Month = '10' /*AND sc.Year = @year */ THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC)* - 1 
		 WHEN sc.Month = '11' /*AND sc.Year = @year */THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC)* - 1 
		 WHEN sc.Month = '12' /*AND sc.Year = @year */THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC)* - 1 
		 END ACUMULADO
		 FROM   GeneralLedger.GeneralLedgerBalance AS sc WITH (nolock) 
		INNER JOIN Common.ThirdParty AS t WITH (nolock) ON t .Id = sc.IdThirdParty 
		LEFT OUTER JOIN Payroll.CostCenter AS cco WITH (nolock) ON cco.Id = sc.IdCostCenter 
		LEFT OUTER JOIN Payroll.BranchOffice  AS bo WITH (nolock) ON cco.BranchOfficeId =BO.Id 
		LEFT OUTER JOIN GeneralLedger.MainAccounts AS c WITH (nolock) ON c.Id = sc.IdMainAccount 
		--INDIGOREP.DBO.DWH_AGRUPADORES_CONTABILIDAD AS AC WITH (nolock) ON AC.CODECC=cco.Code 
		WHERE (t .PersonType = '2') AND	(c.LegalBookId = '1')
		AND (c.Number BETWEEN '41' AND '41749999') -- AND YEAR = @Year
		GROUP BY t.Nit,t .Name, cco.Code, cco.name, sc.Month, sc.Year,BO.Name

	UNION

	SELECT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
	sc.Month AS MES, 0  Nit, 'Personas Naturales' AS 'CLIENTE', cco.Code AS 'CODIGO CENTRO COSTO', cco.Name as [CENTRO COSTO],BO.Name AS [CENTRO ATENCION] ,sc.year AS AÑO,
		CASE WHEN sc.Month = '1' /*AND sc.Year = @year */ THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC) * - 1 
		 WHEN sc.Month = '2' /*AND sc.Year = @year */THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC)* - 1 
		 WHEN sc.Month = '3' /*AND sc.Year = @year */THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC) * - 1 
		 WHEN sc.Month = '4' /*AND sc.Year = @year */THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC) * - 1 
		 WHEN sc.Month = '5' /*AND sc.Year = @year */THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC) * - 1 
		 WHEN sc.Month = '6' /*AND sc.Year = @year */THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC) * - 1 
		 WHEN sc.Month = '7' /*AND sc.Year = @year */THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC) * - 1 
		 WHEN sc.Month = '8' /*AND sc.Year = @year */THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC) * - 1 
		 WHEN sc.Month = '9' /*AND sc.Year = @year */THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC) * - 1  
		 WHEN sc.Month = '10' /*AND sc.Year = @year */THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC)* - 1 
		 WHEN sc.Month = '11' /*AND sc.Year = @year */THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC)* - 1 
		 WHEN sc.Month = '12' /*AND sc.Year = @year */THEN CAST(((SUM(sc.DebitValue) - SUM(sc.CreditValue))) AS NUMERIC)* - 1 
		 END ACUMULADO
		FROM   GeneralLedger.GeneralLedgerBalance AS sc WITH (nolock) 
		INNER JOIN Common.ThirdParty AS t WITH (nolock) ON t .Id = sc.IdThirdParty 
		LEFT OUTER JOIN Payroll.CostCenter AS cco WITH (nolock) ON cco.Id = sc.IdCostCenter 
		LEFT OUTER JOIN Payroll.BranchOffice  AS bo WITH (nolock) ON cco.BranchOfficeId =BO.Id 
		LEFT OUTER JOIN GeneralLedger.MainAccounts AS c WITH (nolock) ON c.Id = sc.IdMainAccount --LEFT OUTER JOIN
		--INDIGOREP.DBO.DWH_AGRUPADORES_CONTABILIDAD AS AC WITH (nolock) ON AC.CODECC=cco.Code 
		WHERE (t .PersonType = '1') AND	(c.LegalBookId = '1')
		AND (c.Number BETWEEN '41' AND '41749999') -- AND YEAR=  @Year
		GROUP BY  cco.Code, cco.name, sc.Month, sc.Year,BO.Name
		) DET
		PIVOT
		(
		SUM(ACUMULADO)
		FOR MES IN ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12])) AS piv
		--ORDER BY 1,2,3,4,5,6
	)

	SELECT * FROM CTE_AÑO_ACTUAL --where  [AÑO]='2024'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a alimentar un cubo analítico (UploadCube) con las ventas ejecutadas según contabilidad. Consolida los saldos del libro mayor de cuentas de ingresos (PUC entre 41 y 41749999, libro legal 1) pivotando los valores mensuales (débito menos crédito, invertido de signo) en columnas de enero a diciembre, agrupados por tercero (diferenciando personas jurídicas con NIT de personas naturales), centro de costo y sede/centro de atención.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceSalesAccounting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceSalesAccounting';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida las ventas ejecutadas mensuales (ingresos contables) por cliente, centro de costo y sede, pivotadas por mes del año, separando personas jurídicas (con NIT) y personas naturales (agrupadas).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceSalesAccounting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las cuentas contables deben pertenecer al libro legal LegalBookId = ''1''.; El número de cuenta debe estar entre ''41'' y ''41749999'' (cuentas de ingresos del PUC colombiano).; Cada saldo en GeneralLedgerBalance debe tener un IdThirdParty válido (INNER JOIN con Common.ThirdParty).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceSalesAccounting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El signo del acumulado mensual siempre se invierte multiplicando por -1, convirtiendo saldos crédito (típicos de cuentas de ingreso) en valores positivos.; Solo se incluyen movimientos de cuentas del grupo 41 (ingresos operacionales) del libro legal principal.; Los meses sin movimiento aparecen con valor 0 gracias al ISNULL aplicado tras el PIVOT.; Las personas naturales nunca exponen su identidad individual en el reporte; siempre se agregan como un único cliente sintético.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceSalesAccounting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ventas ejecutadas; Ingresos contables; Plan Único de Cuentas (PUC) - grupo 41; Libro legal contable; Centro de costo; Centro de atención / Sucursal; Tercero (persona natural / persona jurídica); NIT; Saldo débito y crédito; Cubo financiero / reporte de carga', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceSalesAccounting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieFinanceSalesAccounting: Devuelve el saldo mensual como (DebitValue - CreditValue) * -1 por cada mes (1..12), pivotado en columnas ENERO..DICIEMBRE, etiquetado ORIGEN=''Contabilidad'' y LOGICA=''Ventas ejecutadas''.; [RETURN_RESULT] Report.UploadCubeVieFinanceSalesAccounting: ULT_ACTUAL se calcula como GETDATE() convertido a la zona horaria ''Pakistan Standard Time''.; [RETURN_RESULT] Report.UploadCubeVieFinanceSalesAccounting: ID_COMPANY se obtiene de DB_NAME() truncado a VARCHAR(9), identificando la base de datos/compañía origen.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceSalesAccounting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ThirdParty.PersonType = ''2'' (persona jurídica) → Se reporta cada tercero individualmente con su NIT y razón social como CLIENTE, agrupando por Nit, Name, CostCenter, Month, Year y BranchOffice.; si ThirdParty.PersonType = ''1'' (persona natural) → Se agrupa toda la información bajo NIT=0 y CLIENTE=''Personas Naturales'', sin discriminar por tercero individual. else Otros tipos de persona quedan excluidos del resultado.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceSalesAccounting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.GeneralLedgerBalance; Common.ThirdParty; Payroll.CostCenter; Payroll.BranchOffice; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceSalesAccounting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceSalesAccounting';
GO
