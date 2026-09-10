

CREATE VIEW [Authorization].[ViewListAnnexes]
AS

	select a.Id TraceabilityPaperworkAnnexesId, t.Id TraceabilityPaperworkId,
	a.Consecutive, a.CreationDate, a.CreationUser, 'Autorización de Servicios' DocumentTypeName,
	t.ServiceCode, ISNULL(ce.Description, pro.Name) ServiceDescription,
	t.RequestQuantity, t.Status TraceabilityPaperworkStatus,
	case t.Status
		when 1 then 'Solicitado'
		when 2 then 'Radicado'
		when 3 then 'Radicado Pendiente de Autorizacion'
		when 4 then 'Radicado No Autorizado'
		when 5 then 'Autorizado'
		when 6 then 'Autorizado en Entrega'
		when 7 then 'Autorizado Entregado'
		when 8 then 'Agendado'
		when 9 then 'Ejecutado'
		when 10 then 'Facturado'
		when 11 then 'Cancelado'
		when 12 then 'Solicitado en Cotización'
		when 13 then 'Radicado en Cotización'
		when 14 then 'Autorizado Remitido'
		when 15 then 'Autorizado con Cotización'
	end TraceabilityPaperworkStatusName, a.Folio, a.TypeRequestServices, a.PriorityAttention, a.Justification, a.DiagnosticCode
	from [Authorization].TraceabilityPaperworkAnnexes a
	inner join [Authorization].TraceabilityPaperwork t on t.Id = a.TraceabilityPaperworkId
	left join Contract.CUPSEntity ce on ce.Code = t.ServiceCode
	left join Inventory.InventoryProduct pro on pro.Code = t.ServiceCode
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de anexos asociados a trámites de autorización de servicios de salud. Combina cada anexo del trámite (folio, consecutivo, tipo de solicitud, prioridad de atención, justificación y diagnóstico) con la información del trámite principal (código y descripción del servicio CUPS o medicamento/insumo del inventario, cantidad solicitada y estado legible del trámite). Traduce el estado numérico del trámite a su nombre descriptivo (Solicitado, Radicado, Autorizado, Facturado, Cancelado, entre otros). Es útil para reportería y seguimiento del ciclo de vida de autorizaciones, permitiendo identificar qué anexos respaldan cada solicitud de autorización de procedimiento, consulta o medicamento.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListAnnexes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListAnnexes';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los anexos de trámites de autorización de servicios junto con su trámite asociado, descripción del servicio (CUPS o producto de inventario) y el nombre legible del estado del trámite.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListAnnexes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada anexo en TraceabilityPaperworkAnnexes debe tener un TraceabilityPaperworkId que exista en TraceabilityPaperwork (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListAnnexes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'DocumentTypeName es siempre la constante ''Autorización de Servicios''.; El código de servicio (ServiceCode) se resuelve primero contra el catálogo CUPS y, en su defecto, contra el catálogo de productos de inventario.; Los estados del trámite están restringidos al rango 1..15 con nombres fijos; cualquier valor fuera de ese rango produce NULL en TraceabilityPaperworkStatusName.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListAnnexes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Autorización de servicios de salud; Trámite/trazabilidad de autorización; Anexos documentales; Servicios CUPS; Productos de inventario; Folio; Diagnóstico; Prioridad de atención; Justificación clínica; Estados del trámite (Solicitado, Radicado, Autorizado, Facturado, Cancelado, Cotización, Remitido)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListAnnexes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Authorization.ViewListAnnexes: Devuelve un registro por cada anexo en TraceabilityPaperworkAnnexes vinculado a su trámite, etiquetado siempre como ''Autorización de Servicios''.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListAnnexes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si t.Status entre 1 y 15 → Traduce el código numérico de estado a su nombre en español (Solicitado, Radicado, Autorizado, Cancelado, etc.) en TraceabilityPaperworkStatusName; si Existe coincidencia de t.ServiceCode en Contract.CUPSEntity.Code → ServiceDescription toma ce.Description (descripción CUPS) else Si no, toma pro.Name de Inventory.InventoryProduct (vía ISNULL)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListAnnexes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.TraceabilityPaperworkAnnexes; Authorization.TraceabilityPaperwork; Contract.CUPSEntity; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListAnnexes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListAnnexes';
GO
