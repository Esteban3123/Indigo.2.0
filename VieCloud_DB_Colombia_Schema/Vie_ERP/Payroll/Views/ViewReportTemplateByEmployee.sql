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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte que consolida la programación de turnos por empleado, integrando la plantilla de horario asignada, los conceptos de nómina que aplican a cada bloque horario (recargos, horas extras, etc.) y la unidad funcional o grupo de nómina al que pertenece el trabajador. Cruza los datos del empleado (identificación NIT, nombre) con el detalle diario de su turno, las horas programadas y los conceptos de liquidación correspondientes, filtrando únicamente los conceptos cuyo tipo coincide con el aplicado en la liquidación. Sirve como base para reportes de programación laboral y verificación de turnos por empleado, mostrando qué plantilla de horario se usó, cuántas horas tiene asignadas en cada fecha y qué conceptos de nómina se derivan de esa programación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportTemplateByEmployee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportTemplateByEmployee';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, para reportes de nómina, la programación de turnos por empleado cruzada con sus horas y conceptos de liquidación, enriquecida con datos del tercero, grupo, unidad funcional y plantilla de horario.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTemplateByEmployee';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas base de programación de nómina (ScheduleDetail, ScheduleDetailHour, ScheduleDetailConcept) deben estar pobladas y relacionadas correctamente por sus llaves (ScheduleDetailId, ScheduleDetailHourId).; ScheduleDetail debe tener GroupId y FunctionalUnitId válidos, ya que el join es INNER (filas sin grupo o unidad funcional no aparecen en el reporte).; Para que una fila aparezca con horas y concepto, debe existir coincidencia entre ScheduleDetailHour.AppliedLiquidationConcept y ScheduleDetailConcept.ConceptType.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTemplateByEmployee';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan filas en las que el concepto aplicado a la liquidación de las horas (ScheduleDetailHour.AppliedLiquidationConcept) coincide con el tipo de concepto asociado al detalle (ScheduleDetailConcept.ConceptType), garantizando consistencia entre el bloque horario y el concepto liquidado.; Cada fila resultante recibe un identificador secuencial (Id) generado por ROW_NUMBER ordenado por NIT del tercero, por lo que el Id no es estable entre ejecuciones.; Los resultados se devuelven sin duplicados (DISTINCT + GROUP BY sobre las mismas columnas proyectadas).; Las relaciones con Empleado, Tercero, ScheduleTemplate, Concepto y horas/conceptos del turno son LEFT JOIN, por lo que el reporte puede incluir detalles de turno aún sin empleado, plantilla, concepto u horas asociadas, siempre que se cumpla la condición de igualdad de conceptos.; El grupo (Group) y la unidad funcional (FunctionalUnit) son obligatorios en cada fila al estar enlazados con INNER JOIN sobre ScheduleDetail.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTemplateByEmployee';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empleado; Tercero (NIT); Turno/Jornada programada; Grupo de nómina; Unidad funcional; Plantilla de horario (ScheduleTemplate); Concepto de liquidación de nómina; Horas trabajadas por turno; Concepto aplicado a liquidación', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTemplateByEmployee';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewReportTemplateByEmployee: Devuelve un conjunto de resultados con una fila por combinación distinta de NIT, nombre del empleado, fecha del turno, horas, concepto, grupo, unidad funcional y plantilla, filtrada por SDH.AppliedLiquidationConcept = SDC.ConceptType.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTemplateByEmployee';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.ScheduleDetail; Payroll.Group; Payroll.FunctionalUnit; Payroll.ScheduleDetailHour; Payroll.ScheduleDetailConcept; Payroll.Employee; Common.ThirdParty; Payroll.ScheduleTemplate; Payroll.Concept', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTemplateByEmployee';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTemplateByEmployee';
GO
