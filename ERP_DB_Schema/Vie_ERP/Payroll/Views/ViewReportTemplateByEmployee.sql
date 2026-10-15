CREATE VIEW [Payroll].[ViewReportTemplateByEmployee]
AS
select ROW_NUMBER() OVER (ORDER BY Nit) AS Id, * from 
(select 
distinct
thi.Nit AS Nit, 
thi.Name AS NameEmployee, 
sd.DateDetail,
sd.TotalNumberHours,
em.Id AS EmployeeId,	 
c.Name AS NameConcept, 
sdh.TotalNumberHours AS ScheduleDetailHour,
g.Id AS GroupId,
g.Name AS NameGroup,
fu.Id AS FunctionalUnitId,
fu.Name AS FunctionalUnitName,
st.Code AS ScheduleTemplateCode,
st.Name AS ScheduleTemplateName 
from 
[Payroll].[ScheduleDetail] as sd
INNER JOIN [Payroll].[Group] as g on g.Id = sd.GroupId
INNER JOIN [Payroll].[FunctionalUnit] as fu on fu.Id = sd.FunctionalUnitId
LEFT JOIN [Payroll].[ScheduleDetailHour] as sdh on sd.Id = sdh.ScheduleDetailId
LEFT JOIN [Payroll].[ScheduleDetailConcept] as sdc on sdh.Id = sdc.ScheduleDetailHourId
LEFT JOIN [Payroll].[Employee] as em on sd.EmployeeId = em.Id
left join [Common].[ThirdParty] as thi on thi.Id = em.ThirdPartyId
left join [Payroll].[ScheduleTemplate] as st on st.Id = sd.ScheduleTemplateId
left join [Payroll].[Concept] as c on c.Id = sdc.ConceptId
where SDH.AppliedLiquidationConcept = SDC.ConceptType 
group by thi.Nit, thi.Name, st.Name, c.Name,sd.DateDetail,em.Id, sdh.TotalNumberHours, g.Id, fu.Id, g.Name, fu.Name, st.Code, st.Name, sd.TotalNumberHours) as Datos