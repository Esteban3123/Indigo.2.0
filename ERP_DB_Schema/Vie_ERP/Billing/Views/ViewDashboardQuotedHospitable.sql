

CREATE VIEW [Billing].[ViewDashboardQuotedHospitable]
AS

	select CONCAT(info.EntityName, '-', info.EntityId, '-', info.ServiceCode, '-', info.Extramural) Id, info.AdmissionNumber,	 
	ing.CODDIAING DiagnosticCode, ing.CODCENATE CareCenterCode,
	info.PatientCode, pa.IPNOMCOMP PatientName, rtrim(ltrim(info.PatientCode)) + ' - ' + rtrim(ltrim(pa.IPNOMCOMP)) PatientCodeName,
	info.ServiceId, info.ServiceCode, info.ServiceName, info.ServiceCodeName, info.ServiceType,
	cd.Id ContractDescriptionId, cd.Code ContractDescriptionCode, cd.Name ContractDescriptionName, cd.Code + ' - ' + cd.Name ContractDescriptionCodeName,
	info.Quantity,
	fu.Id FunctionalUnitId, fu.Code FunctionalUnitCode, fu.Name FunctionalUnitName, fu.Code + ' - ' + fu.Name FunctionalUnitCodeName,
	info.ProfessionalCode,
	cg.Id CareGroupId, cg.Code CareGroupCode, cg.Name CareGroupName, cg.Code + ' - ' + cg.Name CareGroupCodeName,
	ha.Id HealthAdministratorId, ha.Code HealthAdministratorCode, ha.Name HealthAdministratorName, ha.Code + ' - ' + ha.Name HealthAdministratorCodeName,
	ISNULL(pc.Contracted, prd.Contracted) Contracted, ISNULL(pc.Quoted, prd.Quoted) Quoted, info.EntityId, info.EntityName, info.Extramural, info.RequestDate, info.ItemType,
	dq.Id DashboardQuotedId, dq.Status DashboardQuotedStatus, case dq.Status when 1 then 'Sin Confirmar' when 2 then 'Confirmado' when 3 then 'No Cotizado' end DashboardQuotedStatusName,
	q.Id QuotationId, q.Status QuotationStatus, case q.Status when 1 then 'Registrado' when 2 then 'Confirmado' when 3 then 'Anulado' end QuotationStatusName, q.Code QuotationCode,th.Id ThirdPartyId
	from (
		select h.AUTO EntityId, 'HCORDIMAG' EntityName, h.NUMINGRES AdmissionNumber, 
		h.IDDESCRIPCIONRELACIONADA ContractDescriptionId, h.MANEXTPRO Extramural, h.FECORDMED RequestDate, IIF(h.MANEXTPRO = 0, 'I', 'A') ItemType,
		h.IPCODPACI PatientCode, h.UFUCODIGO FunctionalUnitCode, 1 ServiceType, h.CANSERIPS Quantity, h.CODPROSAL ProfessionalCode,
		ce.Id ServiceId, ce.Code ServiceCode, ce.Description ServiceName, ce.Code + ' - ' + ce.Description ServiceCodeName
		from .HCORDIMAG h WITH(NOLOCK)
		inner join Contract.CUPSEntity ce on ce.Code = h.CODSERIPS

		union all

		select h.AUTO EntityId, 'HCORDLABO' EntityName, h.NUMINGRES AdmissionNumber, 
		h.IDDESCRIPCIONRELACIONADA ContractDescriptionId, h.MANEXTPRO Extramural, h.FECORDMED RequestDate, IIF(h.MANEXTPRO = 0, 'I', 'A') ItemType,
		h.IPCODPACI PatientCode, h.UFUCODIGO FunctionalUnitCode, 1 ServiceType, h.CANSERIPS Quantity, h.CODPROSAL ProfessionalCode,
		ce.Id ServiceId, ce.Code ServiceCode, ce.Description ServiceName, ce.Code + ' - ' + ce.Description ServiceCodeName
		from .HCORDLABO h WITH(NOLOCK)
		inner join Contract.CUPSEntity ce WITH(NOLOCK) on ce.Code = h.CODSERIPS

		union all

		select h.AUTO EntityId, 'HCORDPATO' EntityName, h.NUMINGRES AdmissionNumber, 
		h.IDDESCRIPCIONRELACIONADA ContractDescriptionId, h.MANEXTPRO Extramural, h.FECORDMED RequestDate, IIF(h.MANEXTPRO = 0, 'I', 'A') ItemType,
		h.IPCODPACI PatientCode, h.UFUCODIGO FunctionalUnitCode, 1 ServiceType, h.CANSERIPS Quantity, h.CODPROSAL ProfessionalCode,
		ce.Id ServiceId, ce.Code ServiceCode, ce.Description ServiceName, ce.Code + ' - ' + ce.Description ServiceCodeName
		from .HCORDPATO h WITH(NOLOCK)
		inner join Contract.CUPSEntity ce WITH(NOLOCK) on ce.Code = h.CODSERIPS

		union all

		select h.AUTO EntityId, 'HCORDINTE' EntityName, h.NUMINGRES AdmissionNumber, 
		h.IDDESCRIPCIONRELACIONADA ContractDescriptionId, h.MANEXTPRO Extramural, h.FECORDMED RequestDate, IIF(h.MANEXTPRO = 0, 'I', 'A') ItemType,
		h.IPCODPACI PatientCode, h.UFUCODIGO FunctionalUnitCode, 1 ServiceType, h.CANSERIPS Quantity, h.CODPROSAL ProfessionalCode,
		ce.Id ServiceId, ce.Code ServiceCode, ce.Description ServiceName, ce.Code + ' - ' + ce.Description ServiceCodeName
		from .HCORDINTE h WITH(NOLOCK)
		inner join Contract.CUPSEntity ce WITH(NOLOCK) on ce.Code = h.CODSERIPS

		union all

		select h.AUTO EntityId, 'HCORDPRON' EntityName, h.NUMINGRES AdmissionNumber, 
		h.IDDESCRIPCIONRELACIONADA ContractDescriptionId, h.MANEXTPRO Extramural, h.FECORDMED RequestDate, IIF(h.MANEXTPRO = 0, 'I', 'A') ItemType,
		h.IPCODPACI PatientCode, h.UFUCODIGO FunctionalUnitCode, 1 ServiceType, h.CANSERIPS Quantity, h.CODPROSAL ProfessionalCode,
		ce.Id ServiceId, ce.Code ServiceCode, ce.Description ServiceName, ce.Code + ' - ' + ce.Description ServiceCodeName
		from .HCORDPRON h WITH(NOLOCK)
		inner join Contract.CUPSEntity ce WITH(NOLOCK) on ce.Code = h.CODSERIPS

		union all

		select h.AUTO EntityId, 'HCORDPROQ' EntityName, h.NUMINGRES AdmissionNumber, 
		h.IDDESCRIPCIONRELACIONADA ContractDescriptionId, h.MANEXTPRO Extramural, h.FECORDMED RequestDate, IIF(h.MANEXTPRO = 0, 'I', 'A') ItemType,
		h.IPCODPACI PatientCode, h.UFUCODIGO FunctionalUnitCode, 1 ServiceType, h.CANSERIPS Quantity, h.CODPROSAL ProfessionalCode,
		ce.Id ServiceId, ce.Code ServiceCode, ce.Description ServiceName, ce.Code + ' - ' + ce.Description ServiceCodeName
		from .HCORDPROQ h WITH(NOLOCK)
		inner join Contract.CUPSEntity ce WITH(NOLOCK) on ce.Code = h.CODSERIPS

		UNION ALL

		select d.ID EntityId, 'HCORDPROQD' EntityName, h.NUMINGRES AdmissionNumber, 
		h.IDDESCRIPCIONRELACIONADA ContractDescriptionId, h.MANEXTPRO Extramural, h.FECORDMED RequestDate, IIF(h.MANEXTPRO = 0, 'I', 'A') ItemType,
		h.IPCODPACI PatientCode, h.UFUCODIGO FunctionalUnitCode, 2 ServiceType, d.CANTENTREG Quantity, h.CODPROSAL ProfessionalCode,
		ip.Id ServiceId, ip.Code ServiceCode,ip.Name ServiceName, ip.Code + ' - ' + ip.Name ServiceCodeName
		from .HCORDPROQ h
		INNER JOIN HCORDPROQD d WITH(NOLOCK) on h.AUTO=d.AUTOPROCED
		INNER JOIN IHLISTPRO i WITH(NOLOCK) on d.CODPRODUC=i.CODPRODUC
		LEFT JOIN Inventory.ATC a WITH(NOLOCK) on a.Code =i.CODPRODUC
		LEFT JOIN Inventory.InventorySupplie isp WITH(NOLOCK) on isp.Code=i.CODPRODUC
		INNER join Inventory.InventoryProduct ip WITH(NOLOCK) on iif(a.id is null, ip.SupplieId,ip.ATCId)=isnull(a.Id,isp.Id)
		WHERE h.SOLICITAMATOST =2 AND d.MATESTADO=2

		UNION ALL 

		select h.ID EntityId, 'HCPRESCRA' EntityName, h.NUMINGRES AdmissionNumber, 
		0 ContractDescriptionId, h.MANEXTPRO Extramural, h.FECINIDOS RequestDate, IIF(h.MANEXTPRO = 0, 'I', 'A') ItemType,
		h.IPCODPACI PatientCode, h.UFUCODIGO FunctionalUnitCode, 2 ServiceType, h.CANPEDPRO Quantity, h.CODPROSAL ProfessionalCode,
		ip.Id ServiceId, ip.Code ServiceCode, ip.Name ServiceName, ip.Code + ' - ' + ip.Name ServiceCodeName
		from .HCPRESCRA h WITH(NOLOCK)
		inner join Inventory.ATC atc WITH(NOLOCK) ON h.CODPRODUC = atc.Code
		inner join Inventory.InventoryProduct ip WITH(NOLOCK) ON atc.Id = ip.ATCId

		union all

		select tp.Id EntityId, 'TraceabilityPaperwork' EntityName, tp.AdmissionNumber AdmissionNumber, 
		tp.ContractDescriptionId ContractDescriptionId, 1 Extramural, tp.RequestDate RequestDate, 'A' ItemType,
		tp.PatientCode PatientCode, tp.FunctionalUnitCode FunctionalUnitCode, tp.Type ServiceType, tp.RequestQuantity Quantity, tp.ProfessionalCode ProfessionalCode,
		tp.ServiceId ServiceId, tp.ServiceCode ServiceCode, temp.Name ServiceName, tp.ServiceCode + ' - ' + temp.Name ServiceCodeName
		from [Authorization].TraceabilityPaperwork tp WITH(NOLOCK)
		inner join (
			select ce.Id, ce.Code, ce.Description Name, 1 Type
			from Contract.CUPSEntity ce WITH(NOLOCK)

			union all

			select ip.Id, ip.Code, ip.Name, 2 Type
			from Inventory.ATC atc WITH(NOLOCK)
			inner join Inventory.InventoryProduct ip WITH(NOLOCK) ON atc.Id = ip.ATCId
		) temp on temp.Id = tp.ServiceId and temp.Type = tp.Type
		where tp.Status in (12, 13)

		union all

		select h.AUTO EntityId, 'AMBORDLAB' EntityName, h.NUMINGRES AdmissionNumber, 
		h.IDDESCRIPCIONRELACIONADA ContractDescriptionId, 1 Extramural, h.FECORDMED RequestDate, 'A' ItemType,
		h.IPCODPACI PatientCode, h.UFUCODIGO FunctionalUnitCode, 1 ServiceType, h.CANSERIPS Quantity, h.CODPROSAL ProfessionalCode,
		ce.Id ServiceId, ce.Code ServiceCode, ce.Description ServiceName, ce.Code + ' - ' + ce.Description ServiceCodeName
		from .AMBORDLAB h WITH(NOLOCK)
		inner join Contract.CUPSEntity ce WITH(NOLOCK) on ce.Code = h.CODSERIPS

		union all

		select h.AUTO EntityId, 'AMBORDIMA' EntityName, h.NUMINGRES AdmissionNumber, 
		h.IDDESCRIPCIONRELACIONADA ContractDescriptionId, 1 Extramural, h.FECORDMED RequestDate, 'A' ItemType,
		h.IPCODPACI PatientCode, h.UFUCODIGO FunctionalUnitCode, 1 ServiceType, h.CANSERIPS Quantity, h.CODPROSAL ProfessionalCode,
		ce.Id ServiceId, ce.Code ServiceCode, ce.Description ServiceName, ce.Code + ' - ' + ce.Description ServiceCodeName
		from .AMBORDIMA h WITH(NOLOCK)
		inner join Contract.CUPSEntity ce WITH(NOLOCK) on ce.Code = h.CODSERIPS

		union all

		select h.AUTO EntityId, 'AMBORDPAT' EntityName, h.NUMINGRES AdmissionNumber, 
		h.IDDESCRIPCIONRELACIONADA ContractDescriptionId, 1 Extramural, h.FECORDMED RequestDate, 'A' ItemType,
		h.IPCODPACI PatientCode, h.UFUCODIGO FunctionalUnitCode, 1 ServiceType, h.CANSERIPS Quantity, h.CODPROSAL ProfessionalCode,
		ce.Id ServiceId, ce.Code ServiceCode, ce.Description ServiceName, ce.Code + ' - ' + ce.Description ServiceCodeName
		from .AMBORDPAT h WITH(NOLOCK)
		inner join Contract.CUPSEntity ce WITH(NOLOCK) on ce.Code = h.CODSERIPS
	) info
	inner join .ADINGRESO ing WITH(NOLOCK) on ing.NUMINGRES = info.AdmissionNumber
	inner join Contract.CareGroup cg WITH(NOLOCK) on cg.Id = ing.GENCAREGROUP
	inner join .INPACIENT pa WITH(NOLOCK) on pa.IPCODPACI = info.PatientCode
	inner join Payroll.FunctionalUnit fu WITH(NOLOCK) on fu.Code = info.FunctionalUnitCode
	inner join Contract.HealthAdministrator ha WITH(NOLOCK) on ha.Id = ing.GENCONENTITY
	INNER join Contract.ProcedureCups pc WITH(NOLOCK) on info.ServiceType = 1 and pc.ProceduresTemplateId = cg.ProcedureTemplateId and pc.CupsId = info.ServiceId and (ISNULL(pc.CUPSEntityContractDescriptionId, 0) = ISNULL(info.ContractDescriptionId, 0)) 
	LEFT JOIN 
	(
		SELECT	prd.ProductRateId, prd.ProductId, 
				MAX(prd.Id) Id, MAX(IIF(prd.Contracted = 1, 1, 0)) Contracted, MAX(IIF(prd.Quoted = 1, 1, 0)) Quoted,
				MIN(prd.InitialDate) InitialDate, MAX(prd.EndDate) EndDate
		FROM Inventory.ProductRateDetail prd
		GROUP BY prd.ProductRateId, prd.ProductId
	) prd on info.ServiceType = 2 and prd.ProductRateId = cg.ProductRateId and prd.ProductId = info.ServiceId and CAST(info.RequestDate AS DATE) between prd.InitialDate and prd.EndDate
	left join Billing.DashboardQuoted dq WITH(NOLOCK) on dq.EntityId = info.EntityId and dq.EntityName = info.EntityName
	left join Billing.Quotation q WITH(NOLOCK) on q.Id = dq.QuotationId
	left join Contract.ContractDescriptions cd WITH(NOLOCK) on cd.Id = pc.ContractDescriptionId
	LEFT join Common.ThirdParty th WITH(NOLOCK) on pa.IPCODPACI =th.Nit
	where ISNULL(pc.Quoted, prd.Quoted) = 1 and ISNULL(dq.Status, 0) in (0, 1)