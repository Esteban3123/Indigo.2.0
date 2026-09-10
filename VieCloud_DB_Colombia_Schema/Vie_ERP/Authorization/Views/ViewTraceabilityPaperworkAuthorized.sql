

CREATE VIEW [Authorization].[ViewTraceabilityPaperworkAuthorized]
AS

	select tpe.Id TraceabilityPaperworkEventsId, temp.TraceabilityPaperworkId,
	tp.CareCenterCode, tp.FunctionalUnitCode, tp.PatientCode, tp.Type,
	tp.ServiceCode, ce.Description, tp.RequestDate, tpe.CreationDate, tpe.AuthorizationNumber
	from [Authorization].TraceabilityPaperwork tp
	inner join (
		select MAX(Id) Id, e.TraceabilityPaperworkId
		from [Authorization].TraceabilityPaperworkEvents e
		group by e.TraceabilityPaperworkId
	) temp on temp.TraceabilityPaperworkId = tp.Id
	inner join [Authorization].TraceabilityPaperworkEvents tpe on tpe.Id = temp.Id and tpe.Status = 2
	inner join Contract.CUPSEntity ce on ce.Code = tp.ServiceCode
	where tp.Status in (5, 6, 7)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los trámites de autorización de servicios de salud que ya fueron autorizados por la aseguradora (EPS), mostrando únicamente el evento más reciente de cada trámite cuando dicho evento tiene estado aprobado (Status = 2) y el trámite global se encuentra en estados de autorizado, parcialmente autorizado o similar (Status 5, 6 o 7). Integra la trazabilidad del trámite con sus eventos de gestión y enriquece el resultado con la descripción del procedimiento o servicio CUPS correspondiente. Sirve para reportería y seguimiento operativo de autorizaciones otorgadas, permitiendo identificar por centro de atención, unidad funcional, paciente, tipo de trámite, código de servicio CUPS, fecha de solicitud, fecha del último evento y número de autorización asignado por la aseguradora.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewTraceabilityPaperworkAuthorized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewTraceabilityPaperworkAuthorized';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la trazabilidad de trámites de autorización cuyo último evento corresponde a una autorización otorgada, enriquecida con la descripción CUPS del servicio solicitado.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewTraceabilityPaperworkAuthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en TraceabilityPaperwork con Status en (5, 6, 7).; Cada trámite debe tener al menos un evento en TraceabilityPaperworkEvents y su evento más reciente debe tener Status = 2.; El ServiceCode del trámite debe corresponder a un código existente en Contract.CUPSEntity.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewTraceabilityPaperworkAuthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen trámites cuyo Status es 5, 6 o 7 (estados que representan trámites autorizados/finalizados según su flujo).; Para cada trámite se considera únicamente su último evento registrado (MAX(Id) por TraceabilityPaperworkId).; Ese último evento debe tener Status = 2 (evento de autorización); si el evento más reciente tiene otro status, el trámite no aparece en la vista.; Cada fila debe tener un código de servicio (ServiceCode) que exista en el catálogo CUPS (Contract.CUPSEntity) — los servicios sin homólogo CUPS quedan excluidos por el INNER JOIN.; El número de autorización expuesto corresponde siempre al del último evento del trámite.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewTraceabilityPaperworkAuthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Trazabilidad de trámites de autorización; Autorización de servicios de salud; Evento de autorización; Servicio CUPS; Centro de atención; Unidad funcional; Paciente', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewTraceabilityPaperworkAuthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Authorization.ViewTraceabilityPaperworkAuthorized: Devuelve un registro por trámite cuando tp.Status IN (5,6,7) y el evento de mayor Id (último) tiene Status = 2, incluyendo datos del trámite, fecha de creación del evento, número de autorización y descripción CUPS.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewTraceabilityPaperworkAuthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.TraceabilityPaperwork; Authorization.TraceabilityPaperworkEvents; Contract.CUPSEntity', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewTraceabilityPaperworkAuthorized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewTraceabilityPaperworkAuthorized';
GO
