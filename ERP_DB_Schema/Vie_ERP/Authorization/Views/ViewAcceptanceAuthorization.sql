














CREATE VIEW [Authorization].[ViewAcceptanceAuthorization]
AS

	select CONCAT(t.Id, '') Id, t.PatientCode, p.IPNOMCOMP PatientName, RTRIM(LTRIM(t.PatientCode)) + ' - ' + RTRIM(LTRIM(p.IPNOMCOMP)) PatientCodeName, t.AdmissionNumber,
	t.ServiceCode, temp.CodeName ServiceCodeName,
	fu.Id FunctionalUnitId, fu.Code FunctionalUnitCode, fu.Name FunctionalUnitName, fu.Code + ' - ' + fu.Name FunctionalUnitCodeName,
	fut.Id FunctionalUnitTargetId, fut.Code FunctionalUnitTargetCode, fut.Name FunctionalUnitTargetName, fut.Code + ' - ' + fut.Name FunctionalUnitTargetCodeName,
	t.Id TraceabilityPaperworkId, t.CareCenterCode, t.CareCenterTargetCode
	from [Authorization].TraceabilityPaperwork t
	inner join Payroll.FunctionalUnit fu on fu.Code = t.FunctionalUnitCode
	inner join Payroll.FunctionalUnit fut on fut.Id = t.FunctionalUnitTargetId
	inner join .INPACIENT p on p.IPCODPACI = t.PatientCode
	inner join (
		select ce.Code, 1 Type, ce.Code + ' - ' + ce.Description CodeName
		from Contract.CUPSEntity ce

		union all

		select p.Code, 2 Type, p.Code + ' - ' + p.Name CodeName
		from Inventory.InventoryProduct p
	) temp on temp.Code = t.ServiceCode and temp.Type = t.Type
	where t.AcceptanceStatus is null or (t.Status = 6 and t.AcceptanceStatus is not null)