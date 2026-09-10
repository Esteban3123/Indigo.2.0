

CREATE VIEW [Authorization].[ViewListEvents]
AS

	select e.Id TraceabilityPaperworkEventsId, t.Id TraceabilityPaperworkId, e.TraceabilityPaperworkAnnexesId,
	ha.Id HealthAdministratorId, ha.Code HealthAdministratorCode, ha.Name HealthAdministratorName, ha.Code + ' - ' + ha.Name HealthAdministratorCodeName,
	e.CreationUser, e.CreationDate, e.ReportType,
	case e.ReportType 
		when 1 then 'Llamada Telefónica' 
		when 2 then 'Envío Físico' 
		when 3 then 'Registro Página Web' 
		when 4 then 'Correo Electrónico'
		when 5 then 'Tramita Paciente'
	end ReportTypeName,
	a.Consecutive AnnexConsecutive, t.Status TraceabilityPaperworkStatus,
	e.Status TraceabilityPaperworkEventsStatus,
	case e.Status
		when 1 then 'Pendiente por Autorizar' 
		when 2 then 'Autorizado' 
		when 3 then 'No Autorizado' 
		when 4 then 'Autorizado con Aval'
	end TraceabilityPaperworkEventsStatusName
	from [Authorization].TraceabilityPaperworkEvents e
	inner join [Authorization].TraceabilityPaperwork t on t.Id = e.TraceabilityPaperworkId
	inner join Contract.HealthAdministrator ha on ha.Id = e.HealthAdministratorId
	left join [Authorization].TraceabilityPaperworkAnnexes a on a.Id = e.TraceabilityPaperworkAnnexesId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de eventos y gestiones realizadas sobre trámites de autorización de servicios de salud ante las administradoras (EPS/aseguradoras). Combina los eventos de trazabilidad con el trámite principal, la administradora de salud involucrada y el anexo asociado, exponiendo descripciones legibles del canal de reporte (llamada telefónica, correo electrónico, envío físico, página web o gestión del paciente) y del estado del evento (pendiente por autorizar, autorizado, no autorizado, autorizado con aval). Sirve como fuente principal para reportería y seguimiento del ciclo de vida de las autorizaciones, permitiendo identificar qué hizo la institución ante cada EPS, cuándo, a través de qué medio y con qué resultado.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListEvents';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListEvents';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida los eventos de trazabilidad de trámites de autorización con su administradora de salud y anexo asociado, traduciendo códigos de tipo de reporte y estado a etiquetas legibles.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListEvents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada evento debe tener un trámite de trazabilidad existente (inner join con TraceabilityPaperwork); Cada evento debe tener una administradora de salud válida (inner join con HealthAdministrator)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListEvents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los tipos de reporte válidos son 1..5 (Llamada Telefónica, Envío Físico, Registro Página Web, Correo Electrónico, Tramita Paciente); Los estados de evento válidos son 1..4 (Pendiente por Autorizar, Autorizado, No Autorizado, Autorizado con Aval); El identificador legible de la administradora se compone como ''Code - Name''; Un evento sin anexo asociado se incluye igualmente con AnnexConsecutive en NULL', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListEvents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Trámite de autorización; Trazabilidad; Eventos de gestión; Administradora de salud (EPS); Anexo de trámite; Tipo de reporte; Estado de autorización; Aval', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListEvents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Authorization.ViewListEvents: Devuelve un registro por cada fila en TraceabilityPaperworkEvents que tenga trámite y administradora válidos; el anexo es opcional (LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListEvents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si e.ReportType = 1 → Etiqueta ''Llamada Telefónica''; si e.ReportType = 2 → Etiqueta ''Envío Físico''; si e.ReportType = 3 → Etiqueta ''Registro Página Web''; si e.ReportType = 4 → Etiqueta ''Correo Electrónico''; si e.ReportType = 5 → Etiqueta ''Tramita Paciente''; si e.Status = 1 → Etiqueta ''Pendiente por Autorizar''; si e.Status = 2 → Etiqueta ''Autorizado''; si e.Status = 3 → Etiqueta ''No Autorizado''; si e.Status = 4 → Etiqueta ''Autorizado con Aval''', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListEvents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.TraceabilityPaperworkEvents; Authorization.TraceabilityPaperwork; Contract.HealthAdministrator; Authorization.TraceabilityPaperworkAnnexes', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListEvents';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListEvents';
GO
