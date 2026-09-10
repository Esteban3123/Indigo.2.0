CREATE VIEW [Payroll].[ViewReportTotalIncentiveConcept]
AS
SELECT 
	ipd.Id,
	pc.ConceptType,
	pc.Code,
	pc.[Name],
	[ip].PeriodInitialDate,
	[ip].PeriodEndDate,
	et.Code AS EmployeeTypeCode,
	[ip].[Period],
	ipd.AccruedValue,
	ipd.DeductedValue,
	pe.Id AS EmployeeId,
	pg.Id AS GroupId,
	pg.Code AS GroupCode,
	pg.[Name] AS GroupName,
	fu.BranchOfficeId
FROM Payroll.IncentivePaymentDetail AS ipd 
JOIN Payroll.IncentivePayment AS [ip] ON [ip].Id = ipd.IncentivePaymentId
JOIN Payroll.[Group] AS pg ON pg.Id = [ip].GroupId
JOIN Payroll.Concept AS pc ON pc.Id = ipd.ConceptId
JOIN Payroll.[Contract] AS c ON c.Id = [ip].ContractId
JOIN Payroll.FunctionalUnit AS fu ON fu.id = c.FunctionalUnitId
JOIN Payroll.Employee AS pe ON pe.Id = c.EmployeeId
JOIN Payroll.CostCenter AS pco ON pco.Id = pe.CostCenterId
JOIN Common.ThirdParty AS ct ON ct.Id = pe.ThirdPartyId
JOIN Payroll.EmployeeType AS et ON et.Id = pe.EmployeeTypeId
WHERE pc.ConceptType <> 3
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte que consolida el total de conceptos de incentivos y bonificaciones liquidados en nómina por período, agrupando los valores devengados y deducidos de cada concepto (excluye el tipo de concepto 3). Integra el detalle de pago de incentivos con el catálogo de conceptos, el grupo de nómina, el contrato laboral, la unidad funcional, el empleado, el centro de costo y el tipo de empleado, permitiendo analizar cuánto se pagó por cada concepto de incentivo a cada trabajador según su grupo, sucursal y período de liquidación. Se usa para reportería de nómina de incentivos y bonificaciones, facilitando el análisis por tipo de empleado, grupo de pago y unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportTotalIncentiveConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportTotalIncentiveConcept';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, para reportes, el detalle consolidado de conceptos liquidados en pagos de incentivos de nómina (devengados y deducidos) junto con período, grupo, empleado, tipo de empleado y sucursal, excluyendo los conceptos de tipo 3.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalIncentiveConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los detalles de pago de incentivo deben estar asociados a un IncentivePayment, Concept y Contract válidos.; El contrato debe tener una FunctionalUnit asignada y el empleado un CostCenter, ThirdParty y EmployeeType válidos.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalIncentiveConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen detalles de incentivos cuyo concepto NO sea de tipo 3 (ConceptType <> 3), excluyendo esa categoría del reporte.; Cada fila del reporte requiere existencia obligatoria de incentivo, grupo de nómina, concepto, contrato, unidad funcional, empleado, centro de costo, tercero y tipo de empleado (todos los JOIN son INNER), por lo que registros con cualquiera de esas referencias faltantes no aparecen.; La sucursal reportada (BranchOfficeId) proviene de la unidad funcional asociada al contrato, no del empleado ni del centro de costo.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalIncentiveConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'incentivos de nómina; conceptos de nómina (devengados/deducciones); período de liquidación; grupo de nómina; contrato laboral; unidad funcional; tipo de empleado; sucursal (BranchOffice)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalIncentiveConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.IncentivePaymentDetail: Devuelve una fila por cada detalle de incentivo cuyo concepto cumpla pc.ConceptType <> 3, enriquecido con datos del pago, grupo, concepto, contrato, unidad funcional, empleado y tipo de empleado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalIncentiveConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.IncentivePaymentDetail; Payroll.IncentivePayment; Payroll.Group; Payroll.Concept; Payroll.Contract; Payroll.FunctionalUnit; Payroll.Employee; Payroll.CostCenter; Common.ThirdParty; Payroll.EmployeeType', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalIncentiveConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalIncentiveConcept';
GO
