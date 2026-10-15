



CREATE VIEW [Payroll].[ViewDistributionOfScheduledExpenses]
AS
SELECT 
	sd.EmployeeId, sd.DateDetail, sdc.ConceptId, cas.AccruedAccount, cas.DeductedAccount, fu.CostCenterId, SUM(sdh.TotalNumberHours) TotalHours, sd.GroupId
FROM Payroll.ScheduleDetail sd
JOIN Payroll.ScheduleDetailHour sdh ON sd.Id = sdh.ScheduleDetailId
JOIN Payroll.ScheduleDetailConcept sdc ON sdh.Id = sdc.ScheduleDetailHourId
JOIN Payroll.FunctionalUnit fu ON sd.ScheduleFunctionalUnitId = fu.Id
JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
JOIN Payroll.ConceptAccountingStructure cas ON acs.Id = cas.AccountingStructureId AND sdc.ConceptId = cas.ConceptId
WHERE sd.TotalNumberHours > 0
GROUP BY sd.EmployeeId, sd.DateDetail, sdc.ConceptId, cas.AccruedAccount, cas.DeductedAccount, fu.CostCenterId, sd.groupID