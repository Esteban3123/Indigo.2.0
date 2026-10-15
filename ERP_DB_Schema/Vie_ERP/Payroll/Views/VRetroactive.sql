




CREATE VIEW [Payroll].[VRetroactive]
AS

SELECT 
	rd.Id,G.Id as GroupId, 
	G.Code as GroupCode, 
	G.Name as GroupName, 
	TP.Id as ThirdPartyId, 
	TP.Nit as NitEmployee, 
	TP.Name as EmployeeName,
	POS.Code + ' - ' + POS.Name as Position, 
	CONT.BasicSalary as BasicSalary,
	30 as PayrollDays, 
	0 as WorkedDays, 
	0 as InabilityDays, 
	0 as VacationDays,
	CONC.Id as ConceptId,
	CONC.Code as CodeConcept, 
	CONC.Name as NameConcept, 
	CONC.ConceptType as ConceptType, 
	0 as Recharges, 
	RD.ValueConceptWithRetroactive as ValueConcept, 
	rc.InitialDateRetroactive AS RetroactiveDate, 
	rc.[Status] as RegisterStatus,
	emp.Id as EmployeeId,
	fUnit.BranchOfficeId
FROM Payroll.RetroactiveD as rd
JOIN Payroll.RetroactiveC as rc on rc.Id = rd.IdRetroactiveC
JOIN Payroll.Concept as CONC on CONC.Id = rd.IdConcept
JOIN Payroll.Employee as Emp on Emp.Id = rc.IdEmployee
JOIN Payroll.[Contract] as CONT on CONT.Id = RC.IdContract
JOIN Payroll.Position as POS on POS.Id = CONT.PositionId
JOIN Common.ThirdParty as TP on TP.Id = Emp.ThirdPartyId
JOIN Payroll.[Group] as G on G.Id = CONT.GroupId
JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = CONT.FunctionalUnitId