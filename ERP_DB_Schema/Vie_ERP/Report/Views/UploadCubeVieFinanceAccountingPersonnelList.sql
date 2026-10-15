CREATE view [Report].[UploadCubeVieFinanceAccountingPersonnelList] as

	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		 tp.Nit AS 'NRO DOCUMENTO',--[NroDocumento]
		 RTRIM(tp.Name) AS 'NOMBRE',--[Nombre]
		 p.Name AS 'CARGO',--[Cargo]
		 fu.name AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
		 cast(GETDATE() as date) 'FECHA BUSQUEDA',
		 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM security.[userINT] AS u
	INNER JOIN common.thirdparty AS tp ON u.UserCode = tp.nit
	INNER JOIN payroll.employee AS e ON tp.id = e.ThirdPartyId AND e.state = 1
	INNER JOIN Payroll.contract AS c ON e.id = c.EmployeeId AND c.status = 1
	LEFT JOIN Payroll.position AS p ON c.PositionId = p.Id
	LEFT JOIN payroll.functionalunit AS fu ON c.functionalunitId = fu.id