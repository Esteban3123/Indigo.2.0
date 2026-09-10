

CREATE VIEW [Payroll].[ViewReportIncentiveAccountingWithConcepts]
as
SELECT 
	ipd.Id as IncentivePaymentDetailId,
	ip.Id as IncentivePaymentId,
	thi.Nit, 
	thi.Name, 
	ip.BasicSalary, 
	ip.RetentionValue, 
	ip.TotalAccrued, 
	ip.TotalDeducted, 
	ip.PaidValue,
	conc.Code as ConceptCode,
	conc.Name as ConceptName,
	iif(ipd.AccruedValue = 0, ipd.DeductedValue, ipd.AccruedValue) as ValConceptAccruDeduc,
	gro.Code as GroupCode,
	gro.Name as GroupName,
	ipd.AccruedValue,
	ipd.DeductedValue,
	ip.PeriodInitialDate,
	ip.PeriodEndDate,
	em.Id as EmployeeeId,
	ip.Period,
	bOffice.Id AS BranchOfficeID
FROM 
[Payroll].[IncentivePayment] as ip with (nolock) inner join
[Payroll].[IncentivePaymentDetail] as ipd with (nolock) on ip.id = ipd.IncentivePaymentId inner join
[Payroll].[Contract] as con with (nolock) on ip.ContractId = con.Id inner join
[Payroll].[Employee] as em with (nolock) on con.EmployeeId = em.Id inner join
[Common].[ThirdParty] as thi with (nolock) on em.ThirdPartyId = thi.Id inner join
[Payroll].[Group] as gro with (nolock) on ip.GroupId = gro.Id inner join
[Payroll].[Concept] as conc with (nolock) on ipd.ConceptId = conc.Id
JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = con.FunctionalUnitId
JOIN Payroll.BranchOffice bOffice ON bOffice.Id = fUnit.BranchOfficeId
Where conc.ConceptType <> 3
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle contable de los pagos de incentivos y bonificaciones de nómina por empleado y período, integrando los conceptos devengados y deducidos (excluyendo el tipo de concepto 3) junto con sus valores de liquidación. Combina los registros de pagos de incentivos, el detalle por concepto, el contrato laboral, el empleado (con su NIT y nombre como tercero), el grupo de nómina, la unidad funcional y la sucursal, permitiendo así trazabilidad completa desde el concepto hasta la sede. Está diseñada para reportería contable y de auditoría de incentivos, facilitando el análisis de devengados, deducciones, retenciones y valor neto pagado por empleado, grupo de nómina y período.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportIncentiveAccountingWithConcepts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportIncentiveAccountingWithConcepts';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle contable de pagos de incentivos de nómina por concepto, enriquecido con datos del tercero, grupo, sucursal y empleado, excluyendo conceptos de aportes patronales para reportería.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentiveAccountingWithConcepts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada IncentivePayment debe tener al menos un IncentivePaymentDetail asociado para aparecer en el reporte (INNER JOIN).; El contrato del pago debe estar vinculado a un Empleado con ThirdParty existente (INNER JOIN).; El contrato debe tener una FunctionalUnit asignada y ésta una BranchOffice válida (INNER JOIN).; El detalle debe referenciar un Concept y el pago un Group existente.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentiveAccountingWithConcepts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Un mismo concepto de detalle se interpreta como devengado o deducción de forma excluyente: si AccruedValue = 0 se toma DeductedValue, en caso contrario se toma AccruedValue.; Los conceptos con ConceptType = 3 nunca son devueltos por la vista.; Cada fila del reporte siempre se asocia a una sucursal (BranchOffice) vía la unidad funcional del contrato.; La identidad fiscal del empleado se obtiene siempre desde Common.ThirdParty (Nit, Name).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentiveAccountingWithConcepts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Pago de incentivos; Nómina; Conceptos de nómina (devengados/deducciones); Contrato laboral; Empleado; Tercero (NIT); Grupo de nómina; Unidad funcional; Sucursal; Período de liquidación; Salario básico; Retención; Total devengado; Total deducido', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentiveAccountingWithConcepts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve una fila por cada IncentivePaymentDetail cuyo Concept.ConceptType sea distinto de 3 (se excluyen explícitamente los conceptos tipo 3, típicamente aportes patronales).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentiveAccountingWithConcepts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ipd.AccruedValue = 0 → Se reporta ipd.DeductedValue como ValConceptAccruDeduc (valor del concepto corresponde a una deducción). else Se reporta ipd.AccruedValue como ValConceptAccruDeduc (valor del concepto corresponde a un devengado).; si conc.ConceptType <> 3 → El concepto se incluye en el reporte; los de tipo 3 se filtran y no aparecen.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentiveAccountingWithConcepts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.IncentivePayment; Payroll.IncentivePaymentDetail; Payroll.Contract; Payroll.Employee; Common.ThirdParty; Payroll.Group; Payroll.Concept; Payroll.FunctionalUnit; Payroll.BranchOffice', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentiveAccountingWithConcepts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentiveAccountingWithConcepts';
GO
