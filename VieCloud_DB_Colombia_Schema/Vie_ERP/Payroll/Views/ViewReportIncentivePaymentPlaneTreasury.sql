

CREATE VIEW [Payroll].[ViewReportIncentivePaymentPlaneTreasury]
AS
select ip.Id
,ct.Nit
,ct.Name as ThirdName
,ip.PeriodInitialDate
,ip.PeriodEndDate
,ip.Period
,pg.Id as GroupId
,pg.Code as GroupCode
,pg.Name as GroupName
,c.BankAccountNumber as BankAccountNumber
,b.Name as BankName
, isNull(ip.TotalAccrued,0) - isNull(ip.TotalDeducted,0) as TotalPaid
from Payroll.IncentivePayment as ip
inner join Payroll.[Group] as pg on pg.Id = ip.GroupId
inner join Payroll.[Contract] as c on c.Id = ip.ContractId
inner join Payroll.Employee as pe on pe.Id = c.EmployeeId
inner join Common.ThirdParty as ct on ct.Id = pe.ThirdPartyId
left join Payroll.Bank as b on b.Id = c.BankId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista para el reporte de plano de tesorería de pagos de incentivos y bonificaciones del personal. Consolida, por cada liquidación de incentivos, los datos de identificación del empleado (NIT y nombre), el período de pago, el grupo de nómina al que pertenece, la cuenta bancaria y el banco destino del desembolso, y el valor neto a pagar (total devengado menos deducciones). Se utiliza para generar el archivo o plano de instrucciones de pago que tesorería envía al banco para dispersar los incentivos y bonificaciones liquidados en nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportIncentivePaymentPlaneTreasury';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportIncentivePaymentPlaneTreasury';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el plano de pago a tesorería de incentivos liquidados, consolidando datos del tercero, cuenta bancaria y neto a pagar (devengado menos deducido) por período y grupo de nómina.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentivePaymentPlaneTreasury';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada IncentivePayment debe estar asociado a un Group, Contract, Employee y ThirdParty existentes (joins INNER); El contrato puede o no tener banco asignado (LEFT JOIN con Payroll.Bank)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentivePaymentPlaneTreasury';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'TotalPaid nunca es NULL: se sustituyen TotalAccrued y TotalDeducted nulos por 0 antes de la resta; Solo se incluyen incentivos cuyo contrato tenga empleado y tercero asociado (joins obligatorios); Los datos bancarios (BankAccountNumber, BankName) provienen del contrato, no del empleado; Si el contrato no tiene banco asociado, BankName será NULL pero el registro se incluye igualmente', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentivePaymentPlaneTreasury';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'pago de incentivos; nómina; tesorería; tercero (NIT); cuenta bancaria; grupo de nómina; período de liquidación; devengado; deducido; neto a pagar', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentivePaymentPlaneTreasury';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewReportIncentivePaymentPlaneTreasury: Devuelve TotalPaid = isNull(TotalAccrued,0) - isNull(TotalDeducted,0) por cada pago de incentivo, junto con NIT/nombre del tercero, datos del grupo, número de cuenta y banco', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentivePaymentPlaneTreasury';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.IncentivePayment; Payroll.Group; Payroll.Contract; Payroll.Employee; Common.ThirdParty; Payroll.Bank', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentivePaymentPlaneTreasury';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncentivePaymentPlaneTreasury';
GO
