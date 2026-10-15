











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