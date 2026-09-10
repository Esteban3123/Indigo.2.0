
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte que muestra la programación de turnos y horarios asignados a cada empleado de nómina, cruzando información del empleado (NIT e identificación del tercero), la fecha y total de horas del turno, el grupo de nómina al que pertenece, la unidad funcional o área donde labora, y la plantilla de turno aplicada (código y nombre). Integra las tablas de detalle de programación, horas por turno, conceptos de liquidación aplicados, empleados, terceros, grupos de nómina y unidades funcionales, filtrando únicamente los bloques horarios donde el concepto liquidado coincide con el tipo de concepto del turno. Sirve como base para reportes de control de turnos por empleado, verificación de horas programadas y auditoría de la plantilla de horario aplicada por sucursal y unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportTemplateByEmployeeA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportTemplateByEmployeeA';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida la programación de turnos por empleado mostrando plantilla de horario, grupo, unidad funcional, sucursal y horas totales del día, filtrando solo registros cuyo concepto de liquidación aplicado coincide con el tipo de concepto.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTemplateByEmployeeA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las filas de ScheduleDetailHour y ScheduleDetailConcept deben estar relacionadas para evaluar la igualdad entre AppliedLiquidationConcept y ConceptType.; Los empleados deben estar asociados a un tercero (ThirdParty) para obtener Nit y nombre.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTemplateByEmployeeA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila final corresponde a una combinación única (DISTINCT) de empleado, fecha del turno, grupo, unidad funcional, plantilla de horario y sucursal.; El Id devuelto es un consecutivo generado en tiempo de consulta, ordenado por Nit; no es persistente.; Solo se reportan registros donde el concepto aplicado en la liquidación coincide con el tipo de concepto del detalle (filtro en cláusula WHERE que actúa también como INNER JOIN lógico sobre los LEFT JOIN de SDH y SDC).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTemplateByEmployeeA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empleado; Tercero (Nit); Grupo de nómina; Unidad funcional; Sucursal; Plantilla de horario/turno; Concepto de liquidación; Horas trabajadas por turno', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTemplateByEmployeeA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewReportTemplateByEmployeeA: Devuelve un identificador secuencial mediante ROW_NUMBER() OVER (ORDER BY Nit) junto con los datos consolidados de turno por empleado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTemplateByEmployeeA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SDH.AppliedLiquidationConcept = SDC.ConceptType → Solo se incluyen los detalles de hora/concepto donde el concepto aplicado en la liquidación coincide con el tipo de concepto registrado en el detalle. else Se excluyen del resultado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTemplateByEmployeeA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.ScheduleDetail; Payroll.Group; Payroll.FunctionalUnit; Payroll.ScheduleDetailHour; Payroll.ScheduleDetailConcept; Payroll.Employee; Common.ThirdParty; Payroll.ScheduleTemplate; Payroll.Concept', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTemplateByEmployeeA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTemplateByEmployeeA';
GO
