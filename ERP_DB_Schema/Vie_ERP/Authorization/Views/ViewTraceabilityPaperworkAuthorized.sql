












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