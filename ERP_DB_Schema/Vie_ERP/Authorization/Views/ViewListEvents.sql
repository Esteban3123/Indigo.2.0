













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