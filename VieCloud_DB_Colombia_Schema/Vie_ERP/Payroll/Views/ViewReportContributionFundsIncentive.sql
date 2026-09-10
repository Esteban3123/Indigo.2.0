

CREATE view [Payroll].[ViewReportContributionFundsIncentive]
AS
SELECT
	ipd.Id
	,pc.Code AS ConceptCode
	,pc.ConceptClass
	,pg.Code AS GroupCode
	,pg.[Name] AS GroupName
	,e.Id AS EmployeeId
	,t.Nit
	,t.[Name]
	,[ip].PeriodInitialDate
	,[ip].PeriodEndDate
	,[ip].[Period]
	,[ip].BasicSalary
	,[ip].WorkingDays
	,ISNULL(IBCUnemployment,0) AS IBCUnemployment
	,ISNULL(ipd.AccruedValue,0) AS TotalAccrued
	,ISNULL(ipd.DeductedValue,0) AS TotalDeducted
	,ISNULL([ip].TotalAccrued,0) AS Total
	,CASE 
		WHEN [ip].RegisterStatus=1 THEN '' 
		WHEN [ip].RegisterStatus=2 THEN 'C' 
	END AS RegisterStatus
	,pf.ThirdPartyId AS IdThirdParty
	,pf.Code AS FundCode
	,pf.[Name] AS FundName
	,fUnit.BranchOfficeId
FROM Payroll.IncentivePaymentDetail AS ipd 
JOIN Payroll.IncentivePayment AS [ip] ON [ip].Id = ipd.IncentivePaymentId
JOIN Payroll.[Group] pg ON pg.Id = [ip].GroupId
JOIN Payroll.Concept AS pc ON pc.Id = ipd.ConceptId
JOIN Payroll.[Contract] c ON c.Id = [ip].ContractId
JOIN Payroll.Employee e ON e.Id = c.EmployeeId
JOIN Common.ThirdParty t ON t.Id = e.ThirdPartyId
LEFT JOIN Payroll.Fund AS pf ON pf.Id = [ip].UnemployementFundId
JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = c.FunctionalUnitId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reportería que consolida los aportes a fondos de cesantías e incentivos de nómina por empleado y período. Integra el detalle de conceptos liquidados (devengados y deducciones) de cada incentivo o bonificación, junto con el grupo de nómina, el contrato laboral, los datos del empleado (NIT y nombre), el fondo de cesantías asociado y la unidad funcional o sucursal. Se utiliza para generar informes de contribuciones a fondos en liquidaciones de incentivos y bonificaciones, mostrando la base de cotización de cesantías (IBC), días trabajados, salario básico, totales devengados y deducidos, y el estado del registro (activo o cancelado).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportContributionFundsIncentive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportContributionFundsIncentive';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida los pagos de incentivos de nómina por concepto, empleado, período y fondo de cesantías, exponiendo bases (IBC), valores devengados/deducidos y datos del tercero asociado al fondo.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContributionFundsIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada IncentivePaymentDetail debe estar asociado a un IncentivePayment, Concept y a un Contract con Employee y ThirdParty válidos; El contrato debe tener una FunctionalUnit asignada; El IncentivePayment debe pertenecer a un Group de nómina', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContributionFundsIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los valores monetarios IBCUnemployment, AccruedValue, DeductedValue y TotalAccrued se normalizan a 0 cuando son NULL mediante ISNULL; El fondo de cesantías (Fund) es opcional: se enlaza con LEFT JOIN sobre UnemployementFundId, por lo que puede no existir; Todas las demás relaciones (pago, concepto, grupo, contrato, empleado, tercero, unidad funcional) son obligatorias por usar INNER JOIN; El tercero expuesto como Nit/Name corresponde al empleado, mientras IdThirdParty corresponde al fondo de cesantías', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContributionFundsIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Pago de incentivos de nómina; Concepto de nómina (devengado/deducción); Grupo de nómina; IBC de cesantías (Unemployment); Fondo de cesantías; Período de nómina; Salario básico; Días trabajados; Unidad funcional; Tercero (NIT); Estado de registro (activo/cancelado)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContributionFundsIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewReportContributionFundsIncentive: Devuelve una fila por cada detalle de pago de incentivo (IncentivePaymentDetail) enriquecida con concepto, grupo, empleado, tercero, período, fondo de cesantías y unidad funcional', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContributionFundsIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ip.RegisterStatus = 1 → RegisterStatus se expone como cadena vacía '''' else Si RegisterStatus = 2 se expone como ''C'' (cancelado/anulado); cualquier otro valor queda en NULL', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContributionFundsIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.IncentivePaymentDetail; Payroll.IncentivePayment; Payroll.Group; Payroll.Concept; Payroll.Contract; Payroll.Employee; Common.ThirdParty; Payroll.Fund; Payroll.FunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContributionFundsIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContributionFundsIncentive';
GO
