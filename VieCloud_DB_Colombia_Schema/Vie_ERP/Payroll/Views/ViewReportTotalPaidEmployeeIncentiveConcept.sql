CREATE view [Payroll].[ViewReportTotalPaidEmployeeIncentiveConcept]
AS 
SELECT
	ipd.Id 
	,g.Code AS GroupCode
	,g.Code + ' ' + g.[Name] AS GroupName
	,e.Id AS EmployeeId
	,t.Nit
	,t.[Name]
	,[ip].PeriodInitialDate
	,[ip].PeriodEndDate
	,[ip].[Period]
	,c.BasicSalary
	,ipd.AccruedValue TotalAccrued
	,isnull(ipd.DeductedValue,0) AS TotalDeducted
	,ipd.AccruedValue + isnull(ipd.DeductedValue,0) AS Total
	,[ip].[RegisterStatus]
	,fu.Code AS FunctionalCode
	,fu.[Name] AS FunctionalName
	,cost.Code AS CostCenterCode
	,cost.[Name] AS CostCenterName
	,con.Id AS ConceptId
	,con.Code AS ConceptCode
	,con.[Name] AS ConceptName
	,fu.BranchOfficeId
FROM Payroll.IncentivePayment [ip]
JOIN Payroll.IncentivePaymentDetail ipd ON [ip].Id = ipd.IncentivePaymentId
JOIN Payroll.Concept con ON con.Id = ipd.ConceptId
JOIN Payroll.[Group] g ON g.Id = [ip].GroupId
JOIN Payroll.[Contract] c ON c.Id = [ip].ContractId
JOIN Payroll.FunctionalUnit fu ON fu.Id = c.FunctionalUnitId
JOIN Payroll.CostCenter cost ON cost.Id = fu.CostCenterId
JOIN Payroll.Employee e ON e.Id = c.EmployeeId
JOIN Common.ThirdParty t ON t.Id = e.ThirdPartyId
WHERE con.ConceptType <> 3
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el total pagado por empleado según cada concepto de incentivo de nómina (devengados y deducciones), excluyendo los conceptos de tipo aporte (tipo 3). Integra los pagos de incentivos con su detalle por concepto, los datos del empleado y tercero (NIT, nombre), el grupo y período de liquidación, el salario básico del contrato, y la clasificación por unidad funcional y centro de costo. Sirve para reportería de nómina de incentivos, permitiendo analizar cuánto se pagó a cada empleado por concepto de bonificación o incentivo en un período determinado, desglosado por grupo de nómina, unidad funcional y centro de costo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportTotalPaidEmployeeIncentiveConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportTotalPaidEmployeeIncentiveConcept';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida, por empleado y concepto, los valores devengados y deducidos de los pagos de incentivos de nómina, excluyendo los conceptos de tipo 3.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalPaidEmployeeIncentiveConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada IncentivePayment debe tener Group, Contract asociados y al menos un IncentivePaymentDetail.; Cada Contract debe estar vinculado a un Employee y a una FunctionalUnit con CostCenter.; Cada Employee debe tener un ThirdParty asociado en Common.ThirdParty.; Cada IncentivePaymentDetail debe referenciar un Concept válido en Payroll.Concept.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalPaidEmployeeIncentiveConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye conceptos cuyo ConceptType = 3 (no se reportan en el total pagado por concepto de incentivo).; El total deducido se normaliza a 0 cuando DeductedValue es NULL (ISNULL).; El Total reportado se calcula como AccruedValue + ISNULL(DeductedValue,0) por cada detalle de concepto.; Solo se incluyen pagos de incentivo que tengan al menos un detalle de concepto asociado y cuyo contrato, empleado y tercero existan (joins internos).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalPaidEmployeeIncentiveConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'pago de incentivos; concepto de nómina; devengado; deducción; contrato laboral; empleado; grupo de nómina; unidad funcional; centro de costo; tercero; período de liquidación; salario básico; sucursal', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalPaidEmployeeIncentiveConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.IncentivePaymentDetail: Devuelve una fila por cada IncentivePaymentDetail asociado a un IncentivePayment cuyo Concept.ConceptType <> 3, enriquecida con datos de grupo, contrato, empleado, tercero, unidad funcional, centro de costo y sucursal.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalPaidEmployeeIncentiveConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.IncentivePayment; Payroll.IncentivePaymentDetail; Payroll.Concept; Payroll.Group; Payroll.Contract; Payroll.FunctionalUnit; Payroll.CostCenter; Payroll.Employee; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalPaidEmployeeIncentiveConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalPaidEmployeeIncentiveConcept';
GO
