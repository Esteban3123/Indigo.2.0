CREATE view [Payroll].[ViewReportTotalPaidEmployeeIncentive]
AS 
SELECT
	[ip].Id 
	,g.Code AS GroupCode
	,g.Code + ' ' + g.[Name] AS GroupName
	,e.Id AS EmployeeId
	,t.Nit
	,t.[Name]
	,[ip].PeriodInitialDate
	,[ip].PeriodEndDate
	,[ip].[Period]
	,c.BasicSalary
	,[ip].TotalAccrued
	,isnull([ip].TotalDeducted,0) AS TotalDeducted
	,[ip].TotalAccrued - isnull([ip].TotalDeducted,0) AS Total
	,[ip].[RegisterStatus]
	,fu.Code AS FunctionalCode
	,fu.[Name] AS FunctionalName
	,cost.Code AS CostCenterCode
	,cost.[Name] AS CostCenterName
	,fu.BranchOfficeId
FROM Payroll.IncentivePayment [ip]
JOIN Payroll.[Group] g ON g.Id = [ip].GroupId
JOIN Payroll.[Contract] c ON c.Id = [ip].ContractId
JOIN Payroll.FunctionalUnit fu ON fu.Id = c.FunctionalUnitId
JOIN Payroll.CostCenter cost ON cost.Id = fu.CostCenterId
JOIN Payroll.Employee e ON e.Id = c.EmployeeId
JOIN Common.ThirdParty t ON t.Id = e.ThirdPartyId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte consolidado de pagos de incentivos y bonificaciones por empleado. Integra información del grupo de nómina, el contrato laboral, la unidad funcional, el centro de costo y los datos del tercero (nombre y NIT del trabajador) para mostrar, por cada liquidación de incentivo, el total devengado, el total deducido y el neto a pagar en el período. Permite a nómina y contabilidad consultar el resumen de incentivos pagados por área, dependencia o sucursal, filtrando por período inicial y final de liquidación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportTotalPaidEmployeeIncentive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportTotalPaidEmployeeIncentive';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información de pagos de incentivos liquidados por período a empleados, integrando datos del contrato, grupo de nómina, unidad funcional, centro de costo y tercero para reportes de totales pagados.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalPaidEmployeeIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada IncentivePayment debe tener un GroupId, ContractId válidos.; El Contract debe tener FunctionalUnitId y EmployeeId válidos.; La FunctionalUnit debe tener CostCenterId válido.; El Employee debe tener ThirdPartyId válido (para obtener Nit y Name).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalPaidEmployeeIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'TotalDeducted nunca se reporta como NULL; se normaliza a 0 mediante ISNULL.; Total neto siempre se calcula como TotalAccrued menos TotalDeducted normalizado.; GroupName siempre se construye concatenando Code + '' '' + Name del grupo.; Solo se incluyen pagos de incentivos cuyos contratos, empleados, unidades funcionales, centros de costo, grupos y terceros existen (uso exclusivo de INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalPaidEmployeeIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Pago de incentivo; Nómina; Grupo de nómina; Contrato laboral; Salario básico; Devengado; Deducción; Período de liquidación; Unidad funcional; Centro de costo; Empleado; Tercero (NIT); Sucursal', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalPaidEmployeeIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.IncentivePayment: Devuelve una fila por cada pago de incentivo, calculando Total = TotalAccrued - ISNULL(TotalDeducted,0) y normalizando TotalDeducted nulo a 0.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalPaidEmployeeIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.IncentivePayment; Payroll.Group; Payroll.Contract; Payroll.FunctionalUnit; Payroll.CostCenter; Payroll.Employee; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalPaidEmployeeIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalPaidEmployeeIncentive';
GO
