

CREATE VIEW [Payroll].[ViewReportContractLiquidation] 
AS

select
cast(cld.Id as varchar(50)) as Id,
cl.Id as contractLiquidationId,
cs.[Name] as employeeCostCenterName,
p.FirstName + ' ' + p.SecondName + ' ' + p.FirstLastName + ' ' + p.SecondLastName as employeeName, 
p.IdentificationNumber as employeeIdentificationNumber, 
pos.[Name] as employeePosition, 
rr.[Name] as employeeRetirementReason,
c.JobBondingDate as contractInitialDate,
c.RetirementDate as contractEndingDate,
datediff(day, c.JobBondingDate, c.RetirementDate) as workedDays,
c.ContractInitialDate as initialBenefitsLiquidation,
c.BasicSalary as basicSalary,
CASE WHEN C.JobBondingDate <=  CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) THEN CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) ELSE C.JobBondingDate END AS incentiveLiquidationDate  ,
--cld.initialdate as incentiveLiquidationDate,
cld.endingdate as incentiveCourtDate,
datediff(day, CASE WHEN C.JobBondingDate <=  CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) THEN CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) ELSE C.JobBondingDate END, cld.endingdate) as incentiveDays,
cld.initialdate as christmasLiquidationDate,
cld.endingdate as christmasCourtDate,
datediff(day, cld.initialdate, cld.endingdate) as christmasDays,
SUM(vp.TakenDays) as vacationTakenDays,
IIF((datediff(day, cld.initialdate, cld.endingdate) - SUM(vp.TakenDays)) > 0, (datediff(day, cld.initialdate, cld.endingdate) - SUM(vp.TakenDays)), 0) as vacationPendingDays,
CASE WHEN C.JobBondingDate <=  CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) THEN CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) ELSE C.JobBondingDate END AS unemployedInitialDate,
--cld.initialdate as unemployedInitialDate,
cld.endingdate as unemployedEndingDate,
datediff(day, CASE WHEN C.JobBondingDate <=  CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) THEN CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) ELSE C.JobBondingDate END, cld.endingdate) as unemployedDays,
CASE WHEN C.JobBondingDate <=  CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) THEN CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) ELSE C.JobBondingDate END AS unemployedInterestInitialDate  ,
--cld.initialdate as unemployedInterestInitialDate,
cld.endingdate as unemployedInterestEndingDate,
datediff(day, CASE WHEN C.JobBondingDate <=  CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) THEN CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) ELSE C.JobBondingDate END, cld.endingdate) as unemployedInterestDays,
SUM(vp.PendingDays) as sumaryVacationAccured,
cld.Accrued as sumaryAccured,
cld.Deducted as sumaryDeducted,
cld.[Description] as sumaryDescription,
cl.TotalPaid as totalPaidLiquidation,
cl.TotalAccrued as totalAccured,
cl.TotalDeducted as totalDeducted,
--IIF(c.BasicSalary <= (pp.LegalSalaryMinimum * 2), pp.TransportHelpValue, 0) AS transportHelpValue
case when c.BasicSalary <= (pp.LegalSalaryMinimum * 2) then pp.TransportHelpValue else 0 end AS transportHelpValue
from Payroll.ContractLiquidation cl
inner join Payroll.Employee e on e.Id = cl.EmployeeId
inner join Common.ThirdParty tp on tp.Id = e.ThirdPartyId
inner join Common.Person p on p.Id = tp.PersonId
inner join Payroll.[Contract] c on c.Id = cl.ContractId
inner join Payroll.Position pos on pos.Id = c.PositionId
inner join Payroll.RetirementReason rr on rr.Id = cl.RetirementReasonId
left join Payroll.VacationPeriod vp on vp.EmployeeId = e.Id
inner join Payroll.CostCenter cs on cs.Id = e.CostCenterId
inner join payroll.contractliquidationdetail cld on cld.contractliquidationid = cl.id
inner join payroll.[Group] g on g.Id = c.GroupId
inner join Payroll.PayrollParameter pp on pp.Id = g.PayrollParameterId
group by cld.id, cl.Id, cs.[Name], p.FirstName, p.SecondName, p.FirstLastName, p.SecondLastName, p.IdentificationNumber, pos.[Name], rr.[Name], c.JobBondingDate, c.RetirementDate, c.ContractInitialDate,
c.JobBondingDate,c.BasicSalary,cld.InitialDate,cld.EndingDate,
cl.TotalPaid,
cld.Accrued,
cld.Deducted,
cld.[Description],
cl.TotalAccrued,
cl.TotalDeducted,
pp.TransportHelpValue,
pp.LegalSalaryMinimum
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte detallado de liquidaciones definitivas de contratos laborales. Consolida, por cada concepto de liquidación (ítem de devengado, deducción o retención), la información completa del empleado retirado: nombre y cédula, cargo, centro de costo, motivo de retiro, fechas del contrato, días trabajados, períodos y días para prima de servicios, cesantías, intereses sobre cesantías y vacaciones (tomadas y pendientes), salario básico, auxilio de transporte (si aplica según salario mínimo legal vigente), y los totales devengados, deducidos y netos pagados en la liquidación. Integra ContractLiquidation, ContractLiquidationDetail, Employee, Person, Contract, Position, RetirementReason, VacationPeriod, CostCenter y parámetros de nómina (salario mínimo y auxilio de transporte). Está diseñada para alimentar el informe de liquidación de contratos en el módulo de nómina, facilitando la revisión y auditoría del cierre económico de cada empleado retirado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportContractLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportContractLiquidation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida la liquidación final de contratos laborales por empleado, calculando días e importes para cesantías, intereses, vacaciones, prima e incentivos, junto con auxilio de transporte aplicable.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El empleado debe tener un contrato vinculado (ContractId) y una causa de retiro (RetirementReasonId) en ContractLiquidation.; El contrato debe tener un grupo de nómina con parámetros (PayrollParameter) asociados vía Group.PayrollParameterId.; Debe existir al menos un registro en ContractLiquidationDetail por cada ContractLiquidation (INNER JOIN).; El empleado debe tener tercero y persona registrados en Common.ThirdParty y Common.Person.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los días trabajados se calculan como DATEDIFF(day, JobBondingDate, RetirementDate).; El periodo base anual para incentivos, cesantías e intereses se acota al año calendario del retiro: nunca arranca antes del 1° de enero del año de RetirementDate.; El auxilio de transporte solo aplica si el salario básico no supera el doble del salario mínimo legal vigente parametrizado.; Los días pendientes de vacaciones nunca son negativos (mínimo 0).; El nombre completo del empleado se conforma concatenando FirstName + SecondName + FirstLastName + SecondLastName con espacios.; Solo se incluyen liquidaciones que tengan al menos un detalle (ContractLiquidationDetail) asociado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'liquidación de contrato; cesantías (unemployed); intereses de cesantías; prima (christmas); vacaciones (días tomados/pendientes/causados); incentivo; auxilio de transporte; salario básico; salario mínimo legal; causa de retiro; centro de costo; cargo; fecha de vinculación laboral; fecha de retiro; devengado / deducido / total pagado', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewReportContractLiquidation: Devuelve una fila por cada detalle de liquidación (ContractLiquidationDetail), agrupada por los campos identificativos del empleado, contrato y conceptos liquidados.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.JobBondingDate <= 1° de enero del año de RetirementDate → Toma como fecha inicial de liquidación de incentivo, cesantías, intereses de cesantías y unemployed el 1° de enero del año del retiro else Toma como fecha inicial la fecha de vinculación laboral (JobBondingDate); si (días entre cld.InitialDate y cld.EndingDate) - SUM(vp.TakenDays) > 0 → vacationPendingDays = diferencia de días menos días tomados else vacationPendingDays = 0; si c.BasicSalary <= pp.LegalSalaryMinimum * 2 → Aplica el valor de auxilio de transporte (pp.TransportHelpValue) else transportHelpValue = 0', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.ContractLiquidation; Payroll.Employee; Common.ThirdParty; Common.Person; Payroll.Contract; Payroll.Position; Payroll.RetirementReason; Payroll.VacationPeriod; Payroll.CostCenter; Payroll.ContractLiquidationDetail; Payroll.Group; Payroll.PayrollParameter', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidation';
GO
