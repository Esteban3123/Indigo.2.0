



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