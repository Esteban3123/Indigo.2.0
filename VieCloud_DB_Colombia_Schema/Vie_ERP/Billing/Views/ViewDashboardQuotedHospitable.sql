

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Panel de control de servicios cotizados para pacientes hospitalizados. Consolida todas las órdenes médicas generadas durante un ingreso hospitalario (imágenes diagnósticas, laboratorios, patología, interconsultas, procedimientos de enfermería, procedimientos quirúrgicos y los insumos/medicamentos asociados a cirugías), cruzándolas con el catálogo CUPS, la información del paciente, la unidad funcional, el contrato, la EPS o administradora de salud, y el estado de cotización. Permite visualizar en el dashboard de facturación si cada servicio ordenado está sin confirmar, confirmado o no cotizado, y si tiene una cotización formal asociada, facilitando el seguimiento de la cartera de servicios pendientes de facturar en hospitalización.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewDashboardQuotedHospitable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewDashboardQuotedHospitable';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado los servicios y productos solicitados a pacientes hospitalizados/ambulatorios que están cotizados según contrato o tarifa, para alimentar el dashboard de cotización en facturación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDashboardQuotedHospitable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener un ingreso registrado en ADINGRESO con grupo de atención (GENCAREGROUP) y administradora de salud (GENCONENTITY) válidos.; Los servicios CUPS deben existir en Contract.CUPSEntity con código coincidente a CODSERIPS de las órdenes.; Los productos/insumos deben estar mapeados en Inventory.InventoryProduct vía ATC o InventorySupplie.; La unidad funcional (UFUCODIGO) debe existir en Payroll.FunctionalUnit.; Para servicios tipo 1 (CUPS) debe existir registro en Contract.ProcedureCups con la plantilla del CareGroup; para tipo 2 (productos) debe existir tarifa vigente en Inventory.ProductRateDetail.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDashboardQuotedHospitable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila se identifica unívocamente por la concatenación EntityName-EntityId-ServiceCode-Extramural.; Las órdenes intramurales (HCORD*) se marcan Extramural según MANEXTPRO; las órdenes ambulatorias (AMBORD*) y trámites de autorización siempre se marcan Extramural = 1.; Solo se exponen servicios marcados como cotizados (Quoted = 1) en el contrato (ProcedureCups) o en la tarifa de producto (ProductRateDetail).; Se excluyen registros cuyo DashboardQuoted ya está en estado distinto de 0 (sin gestión) o 1 (sin confirmar), descartando los confirmados y los marcados como no cotizados.; Para tarifas de productos solo aplican aquellas vigentes a la fecha de solicitud (RequestDate entre InitialDate y EndDate).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDashboardQuotedHospitable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cotización de servicios; Órdenes médicas (imágenes, laboratorio, patología, interconsulta, pronóstico, procedimiento quirúrgico); Prescripción de medicamentos; Materiales de procedimientos quirúrgicos; Trámites de autorización; Ingreso/Admisión hospitalaria; Paciente; Unidad funcional; Grupo de atención (CareGroup); Administradora de salud (EPS); Contrato y descripciones contractuales; CUPS (Clasificación Única de Procedimientos en Salud); Tarifas de productos; Servicios intramurales vs extramurales/ambulatorios; Dashboard de cotización; Tercero (NIT)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDashboardQuotedHospitable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewDashboardQuotedHospitable: Devuelve solo registros donde ISNULL(pc.Quoted, prd.Quoted) = 1 y ISNULL(dq.Status, 0) IN (0,1), es decir, servicios cotizables aún sin confirmar o sin gestión.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDashboardQuotedHospitable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si info.ServiceType = 1 (servicio CUPS) → Se cruza con Contract.ProcedureCups por plantilla de procedimientos del CareGroup, CupsId y ContractDescriptionId, tomando Contracted/Quoted desde pc. else Si ServiceType = 2 (producto/insumo) se cruza con Inventory.ProductRateDetail por ProductRateId del CareGroup y ProductId, validando que la fecha de solicitud esté entre InitialDate y EndDate, tomando Contracted/Quoted desde prd.; si h.MANEXTPRO = 0 en órdenes hospitalarias → ItemType = ''I'' (intramural). else ItemType = ''A'' (ambulatorio/extramural).; si Origen HCORDPROQD: h.SOLICITAMATOST = 2 AND d.MATESTADO = 2 → Se incluyen los materiales/insumos de procedimientos quirúrgicos como ServiceType = 2.; si TraceabilityPaperwork.Status IN (12, 13) → Se incluyen únicamente trámites de autorización en esos estados como fuente de servicios cotizables.; si Mapeo de producto: a.id (ATC) is null → Se enlaza InventoryProduct por SupplieId con InventorySupplie. else Se enlaza InventoryProduct por ATCId con ATC.; si dq.Status → Se traduce a etiqueta: 1=''Sin Confirmar'', 2=''Confirmado'', 3=''No Cotizado''.; si q.Status → Se traduce a etiqueta: 1=''Registrado'', 2=''Confirmado'', 3=''Anulado''.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDashboardQuotedHospitable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCORDIMAG; HCORDLABO; HCORDPATO; HCORDINTE; HCORDPRON; HCORDPROQ; HCORDPROQD; IHLISTPRO; HCPRESCRA; AMBORDLAB; AMBORDIMA; AMBORDPAT; ADINGRESO; INPACIENT; Contract.CUPSEntity; Contract.CareGroup; Contract.HealthAdministrator; Contract.ProcedureCups; Contract.ContractDescriptions; Inventory.ATC; Inventory.InventorySupplie; Inventory.InventoryProduct; Inventory.ProductRateDetail; Payroll.FunctionalUnit; Authorization.TraceabilityPaperwork; Billing.DashboardQuoted; Billing.Quotation; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDashboardQuotedHospitable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDashboardQuotedHospitable';
GO
