
CREATE VIEW [Payroll].[ViewReportTemplateByEmployeeA]
AS

WITH cte AS (
	SELECT DISTINCT
		thi.Nit AS Nit, 
		thi.[Name] AS NameEmployee, 
		sd.DateDetail,
		sd.TotalNumberHours,
		em.Id AS EmployeeId,	  
		g.Id AS GroupId,
		g.[Name] AS NameGroup,
		fu.Id AS FunctionalUnitId,
		fu.[Name] AS FunctionalUnitName,
		st.Code AS ScheduleTemplateCode,
		st.[Name] AS ScheduleTemplateName,
		fu.BranchOfficeId
	FROM [Payroll].[ScheduleDetail] as sd
	JOIN [Payroll].[Group] as g on g.Id = sd.GroupId
	JOIN [Payroll].[FunctionalUnit] as fu on fu.Id = sd.FunctionalUnitId
	LEFT JOIN [Payroll].[ScheduleDetailHour] as sdh on sd.Id = sdh.ScheduleDetailId
	LEFT JOIN [Payroll].[ScheduleDetailConcept] as sdc on sdh.Id = sdc.ScheduleDetailHourId
	LEFT JOIN [Payroll].[Employee] as em on sd.EmployeeId = em.Id
	LEFT JOIN [Common].[ThirdParty] as thi on thi.Id = em.ThirdPartyId
	LEFT JOIN [Payroll].[ScheduleTemplate] as st on st.Id = sd.ScheduleTemplateId
	LEFT JOIN [Payroll].[Concept] as c on c.Id = sdc.ConceptId
	WHERE SDH.AppliedLiquidationConcept = SDC.ConceptType 
	GROUP BY 
		thi.Nit, 
		thi.[Name], 
		st.[Name], 
		sd.DateDetail,
		em.Id, 
		sdh.TotalNumberHours, 
		g.Id, 
		fu.Id, 
		g.[Name], 
		fu.[Name], 
		st.Code, 
		st.[Name], 
		sd.TotalNumberHours,
		fu.BranchOfficeId
) 

SELECT 
	ROW_NUMBER() OVER (ORDER BY Nit) AS Id,
	cte.Nit,
	cte.NameEmployee,
	cte.DateDetail,
	cte.TotalNumberHours,
	cte.EmployeeId,
	cte.GroupId,
	cte.NameGroup,
	cte.FunctionalUnitId,
	cte.FunctionalUnitName,
	cte.ScheduleTemplateCode,
	cte.ScheduleTemplateName,
	cte.BranchOfficeId
FROM cte