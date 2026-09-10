
CREATE VIEW [Payroll].[ViewReportIncentivePayment]
AS
SELECT
	ipd.Id,
	ct.Nit,
	ct.[Name] as ThirdName,
	pc.ConceptType,
	pc.Code,
	pc.[Name],
	[ip].PeriodInitialDate,
	[ip].PeriodEndDate,
	et.Code AS EmployeeTypeCode,
	[ip].[Period],
	ipd.AccruedValue,
	ipd.DeductedValue,
	c.JobBondingDate,
	c.BasicSalary,
	pe.Id AS EmployeeId,
	pg.Id AS GroupId,
	pg.Code AS GroupCode,
	pg.[Name] AS GroupName,
	pco.Id AS CostCenetrId,
	pco.Code AS CostCenetrCode, 
	pco.[Name] AS CostCenetrName,
	fu.Id AS FunctionalId,
	fu.Code AS FunctionalCode,
	fu.[Name] AS FunctiomalName,
	bOffice.Id AS BranchOfficeID,
	bOffice.Code AS BranchOfficeCode,
	bOffice.[Name] AS BranchOffice
FROM Payroll.IncentivePaymentDetail as ipd 
JOIN Payroll.IncentivePayment as [ip] on [ip].Id = ipd.IncentivePaymentId
JOIN Payroll.[Group] as pg on pg.Id = [ip].GroupId
JOIN Payroll.Concept as pc on pc.Id = ipd.ConceptId
JOIN Payroll.[Contract] as c on c.Id = [ip].ContractId
JOIN Payroll.FunctionalUnit as fu on fu.id = c.FunctionalUnitId
JOIN Payroll.Employee as pe on pe.Id = c.EmployeeId
JOIN Payroll.CostCenter as pco on pco.Id = pe.CostCenterId
JOIN Common.ThirdParty as ct on ct.Id = pe.ThirdPartyId
JOIN Payroll.EmployeeType as et on et.Id = pe.EmployeeTypeId
JOIN Payroll.BranchOffice bOffice ON bOffice.Id = fu.BranchOfficeId
WHERE pc.ConceptType <> 3
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte que consolida el detalle de pagos de incentivos y bonificaciones de nómina por empleado, excluyendo los conceptos de tipo 3. Integra información del empleado (cédula, nombre, tipo de vinculación), su contrato (salario base, fecha de ingreso), el grupo de nómina, el centro de costo, la unidad funcional y la sucursal, junto con el concepto liquidado (código, nombre, tipo) y los valores devengados y deducidos en cada período. Sirve como fuente principal para reportes de incentivos y bonificaciones del personal, permitiendo analizar los pagos por área, sede, grupo de nómina y empleado en un rango de fechas determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportIncentivePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportIncentivePayment';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida los detalles de pagos de incentivos de nómina con información del empleado, contrato, concepto, grupo, centro de costo, unidad funcional y sucursal, excluyendo conceptos de aportes patronales.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada detalle de pago de incentivo debe tener un IncentivePayment, Concept, Contract, Group, FunctionalUnit, Employee, CostCenter, ThirdParty, EmployeeType y BranchOffice asociados (uso de INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca retorna detalles de pago cuyo concepto sea ConceptType = 3.; Todos los registros expuestos tienen empleado, contrato, tercero, centro de costo, unidad funcional y sucursal vigentes (INNER JOIN obligatorio).; La sucursal expuesta proviene de la unidad funcional del contrato, no del centro de costo del empleado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'pago de incentivos; nómina; concepto de nómina; devengado; deducción; contrato laboral; salario básico; fecha de vinculación; grupo de nómina; centro de costo; unidad funcional; tipo de empleado; sucursal; tercero (NIT); período de liquidación', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewReportIncentivePayment: Devuelve un registro por cada Payroll.IncentivePaymentDetail cuyo Concept.ConceptType sea distinto de 3, enriquecido con datos del tercero, empleado, contrato, grupo, centro de costo, unidad funcional y sucursal.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pc.ConceptType <> 3 → Incluye el detalle del pago de incentivo en el resultado else Excluye el registro (filtra los conceptos tipo 3, típicamente aportes patronales)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.IncentivePaymentDetail; Payroll.IncentivePayment; Payroll.Group; Payroll.Concept; Payroll.Contract; Payroll.FunctionalUnit; Payroll.Employee; Payroll.CostCenter; Common.ThirdParty; Payroll.EmployeeType; Payroll.BranchOffice', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentivePayment';
GO
