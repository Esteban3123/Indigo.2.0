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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista orientada a reporting/cube que consolida información del personal activo con contrato vigente, combinando datos de usuario del sistema, terceros (NIT y nombre), cargo y unidad funcional. Filtra empleados con estado activo (`state = 1`) y contratos activos (`status = 1`). Incluye identificador de compañía desde el nombre de la base de datos y marca temporal de última actualización ajustada a la zona horaria de Pakistán.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingPersonnelList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingPersonnelList';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el listado de personal activo (usuarios con empleado y contrato vigentes) con su cargo y unidad funcional, para alimentar un cubo de información financiera/contable.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingPersonnelList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir correspondencia entre security.userINT.UserCode y common.thirdparty.Nit para que el usuario aparezca; El tercero debe estar registrado como empleado en payroll.employee con state=1; El empleado debe tener al menos un contrato en Payroll.contract con status=1', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingPersonnelList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen empleados con estado activo (employee.state = 1); Solo se incluyen contratos vigentes (contract.status = 1); El usuario del sistema se vincula con el tercero por coincidencia entre UserCode y Nit; El identificador de compañía se deriva del nombre de la base de datos en ejecución (DB_NAME()); La marca de última actualización se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingPersonnelList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'empleado; contrato; cargo; unidad funcional; tercero; usuario; nómina', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingPersonnelList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieFinanceAccountingPersonnelList: Devuelve una fila por cada usuario interno cuyo Nit coincide con un tercero que es empleado activo (state=1) con contrato vigente (status=1), incluyendo cargo y unidad funcional si existen', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingPersonnelList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'security.userINT; common.thirdparty; payroll.employee; Payroll.contract; Payroll.position; payroll.functionalunit', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingPersonnelList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingPersonnelList';
GO
