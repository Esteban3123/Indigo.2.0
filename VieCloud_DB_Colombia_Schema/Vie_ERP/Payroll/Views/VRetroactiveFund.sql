

CREATE VIEW [Payroll].[VRetroactiveFund]
AS
SELECT 
	rd.Id,
	G.Id AS GroupId, 
	G.Code AS GroupCode, 
	G.[Name] AS GroupName, 
	TP.Id AS ThirdPartyId, 
	TP.Nit AS NitEmployee, 
	TP.[Name] AS EmployeeName, 
	CONT.BasicSalary AS BasicSalary,
	30 AS PayrollDays, 
	RD.ValueConcept AS IBC,
	emp.Id AS IdEmployee,
	RD.ValueConceptWithRetroactive AS FundValue,
	rc.InitialDateRetroactive AS RetroactiveDate,
	CASE rc.[Status] 
		WHEN 1 THEN '' WHEN 2 THEN 'C' 
	END AS RegisterStatus,
	pc.ConceptClass,
	(SELECT ISNULL(RD.ValueConceptWithRetroactive,0) FROM Payroll.RetroactiveC RC1, Payroll.RetroactiveD RD1, Payroll.Concept CONC WHERE RC1.Id = RC.Id AND RD1.IdRetroactiveC = RC1.Id AND CONC.Id = RD1.IdConcept AND CONC.ConceptClass = '014') as FundValuePension,
	(SELECT TOP 1 F.[Name] FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.FundId = F.Id AND FC.ContractId = RC.IdContract AND FC.FundType = 1 AND VoluntaryContribution = 0) AS HealthFund,
	(SELECT TOP 1 F.[Name] FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.FundId = F.Id AND FC.ContractId = RC.IdContract AND FC.FundType = 2 AND VoluntaryContribution = 0) AS PensionFund,
	(SELECT TOP 1 F.[Name] FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.FundId = F.Id AND FC.ContractId = RC.IdContract AND FC.FundType = 1 AND VoluntaryContribution = 1) AS VoluntaryHealthFund,
	(SELECT TOP 1 F.[Name] FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.FundId = F.Id AND FC.ContractId = RC.IdContract AND FC.FundType = 2 AND VoluntaryContribution = 1) AS VoluntaryPensionFund,
	(SELECT TOP 1 F.[Name] FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.FundId = F.Id AND FC.ContractId = RC.IdContract AND FC.FundType = 4 AND VoluntaryContribution = 0) AS RiskFund,
	(SELECT TOP 1 F.[Name] FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.FundId = F.Id AND FC.ContractId = RC.IdContract AND FC.FundType = 3 AND VoluntaryContribution = 0) AS UnemploymentFund,
	fUnit.BranchOfficeId
FROM Payroll.RetroactiveD AS rd
JOIN Payroll.RetroactiveC AS rc ON rc.Id = rd.IdRetroactiveC
JOIN Payroll.Concept AS pc ON pc.Id = rd.IdConcept
JOIN Payroll.Employee AS Emp ON Emp.Id = rc.IdEmployee
JOIN Payroll.[Contract] AS CONT ON CONT.Id = RC.IdContract
JOIN Common.ThirdParty AS TP ON TP.Id = Emp.ThirdPartyId
JOIN Payroll.[Group] AS G ON G.Id = CONT.GroupId
JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = CONT.FunctionalUnitId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de nómina que consolida los conceptos retroactivos de nómina por empleado, integrando el detalle de reliquidaciones (RetroactiveD) con el encabezado del proceso retroactivo (RetroactiveC), el contrato laboral, el grupo de nómina y los datos de identificación del trabajador. Para cada concepto retroactivo expone el valor original (IBC), el valor ajustado con el retroactivo (FundValue), el salario básico, los días de nómina, la fecha de inicio del período retroactivo y el estado del registro. Además, resuelve dinámicamente el nombre de las entidades afiliadas del empleado: EPS (salud obligatoria y voluntaria), fondo de pensiones (obligatorio y voluntario), administradora de riesgos laborales (ARL) y fondo de cesantías. Se usa principalmente para reportes de aportes a seguridad social y parafiscales derivados de reliquidaciones retroactivas, así como para la generación de archivos de fondos y auditoría de diferencias salariales.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'VRetroactiveFund';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'VRetroactiveFund';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de conceptos de liquidaciones retroactivas de nómina junto con los datos del empleado, contrato, grupo y los fondos (salud, pensión, ARL, cesantías y voluntarios) asociados al contrato, para reportes de aportes a fondos.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRetroactiveFund';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada RetroactiveD debe tener un RetroactiveC asociado (rc.Id = rd.IdRetroactiveC); Cada RetroactiveC debe referenciar un Empleado, un Contrato y un Concepto válidos; El Empleado debe tener un ThirdParty asociado; El Contrato debe tener un Group y una FunctionalUnit asociados', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRetroactiveFund';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'PayrollDays siempre es 30 (se asume mes comercial de 30 días para el cálculo del retroactivo); IBC se toma del ValueConcept del detalle retroactivo y FundValue del ValueConceptWithRetroactive; Solo se considera un único fondo por tipo y modalidad (TOP 1) por contrato; Los fondos voluntarios solo se identifican con VoluntaryContribution = 1; los obligatorios con VoluntaryContribution = 0; FundType usa convención: 1=Salud, 2=Pensión, 3=Cesantías, 4=Riesgos (ARL); ConceptClass ''014'' identifica el concepto de pensión para el cálculo de FundValuePension; El uso de INNER JOIN garantiza que solo se exponen detalles con cabecera, empleado, contrato, tercero, grupo y unidad funcional existentes', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRetroactiveFund';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retroactivo de nómina; IBC (Ingreso Base de Cotización); Fondo de salud (EPS); Fondo de pensión; Fondo de cesantías; Fondo de riesgos laborales (ARL); Aportes voluntarios; Empleado; Contrato laboral; Grupo de nómina; Unidad funcional; Salario básico; Concepto de nómina', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRetroactiveFund';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.VRetroactiveFund: Devuelve una fila por cada detalle de retroactivo (RetroactiveD) cruzado con su cabecera, empleado, tercero, contrato, grupo y unidad funcional', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRetroactiveFund';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rc.Status = 1 → RegisterStatus se expone como '''' (activo/sin marca) else Si rc.Status = 2 → RegisterStatus = ''C'' (anulado/cancelado); otros valores → NULL; si FundContract.FundType = 1 AND VoluntaryContribution = 0 → Se expone como HealthFund (fondo de salud obligatorio) else FundType=1 y VoluntaryContribution=1 → VoluntaryHealthFund; si FundContract.FundType = 2 AND VoluntaryContribution = 0 → Se expone como PensionFund (fondo de pensión obligatorio) else FundType=2 y VoluntaryContribution=1 → VoluntaryPensionFund; si FundContract.FundType = 4 AND VoluntaryContribution = 0 → Se expone como RiskFund (ARL/riesgos laborales); si FundContract.FundType = 3 AND VoluntaryContribution = 0 → Se expone como UnemploymentFund (cesantías); si Concept.ConceptClass = ''014'' dentro del mismo RetroactiveC → Su ValueConceptWithRetroactive se acumula como FundValuePension (valor del retroactivo correspondiente al concepto de pensión)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRetroactiveFund';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.RetroactiveD; Payroll.RetroactiveC; Payroll.Concept; Payroll.Employee; Payroll.Contract; Common.ThirdParty; Payroll.Group; Payroll.FunctionalUnit; Payroll.FundContract; Payroll.Fund', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRetroactiveFund';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRetroactiveFund';
GO
