

CREATE VIEW [Authorization].[ViewListRequest]
AS

with temp_ADINGRESO as (
	select a.UFUCODIGO, a.NUMINGRES
	from .ADINGRESO a with(nolock)
	where IESTADOIN <> 'A'
),
temp_HCHISPACA as (
	select h.FECHISPAC, h.CODPROSAL, h.INDICAMED, h.CODDIAGNO, h.NUMINGRES, h.NUMEFOLIO, h.IPCODPACI, h.CODCENATE, h.UFUCODIGO
	from .HCHISPACA h with(nolock)
	inner join temp_ADINGRESO t with(nolock) on t.NUMINGRES = h.NUMINGRES
	where ESTAFOLIO <> 0
),
temp_HCORDIMAG as (
	select h.AUTO, h.CODCENATE, h.UFUCODIGO, h.NUMINGRES, h.NUMEFOLIO, h.IPCODPACI, h.FECORDMED, h.CODPROSAL, h.CANSERIPS,
	h.CODSERIPS, h.TraceabilityPaperworkId, h.TraceabilityPaperworkEventsId, h.OBSSERIPS, h.CODDIAGNO, h.IDDESCRIPCIONRELACIONADA
	from .HCORDIMAG h with(nolock)
	inner join temp_HCHISPACA t with(nolock) on t.NUMINGRES = h.NUMINGRES and t.NUMEFOLIO = h.NUMEFOLIO and t.IPCODPACI = h.IPCODPACI
	where h.MANEXTPRO = 1
),
temp_HCORDLABO as (
	select h.AUTO, h.CODCENATE, h.UFUCODIGO, h.NUMINGRES, h.NUMEFOLIO, h.IPCODPACI, h.FECORDMED, h.CODPROSAL, h.CANSERIPS,
	h.CODSERIPS, h.TraceabilityPaperworkId, h.TraceabilityPaperworkEventsId, h.OBSSERIPS, h.CODDIAGNO, h.IDDESCRIPCIONRELACIONADA
	from .HCORDLABO h with(nolock)
	inner join temp_HCHISPACA t with(nolock) on t.NUMINGRES = h.NUMINGRES and t.NUMEFOLIO = h.NUMEFOLIO and t.IPCODPACI = h.IPCODPACI
	where h.MANEXTPRO = 1
),
temp_HCORDPATO as (
	select h.AUTO, h.CODCENATE, h.UFUCODIGO, h.NUMINGRES, h.NUMEFOLIO, h.IPCODPACI, h.FECORDMED, h.CODPROSAL, h.CANSERIPS,
	h.CODSERIPS, h.TraceabilityPaperworkId, h.TraceabilityPaperworkEventsId, h.OBSSERIPS, h.CODDIAGNO, h.IDDESCRIPCIONRELACIONADA
	from .HCORDPATO h with(nolock)
	inner join temp_HCHISPACA t with(nolock) on t.NUMINGRES = h.NUMINGRES and t.NUMEFOLIO = h.NUMEFOLIO and t.IPCODPACI = h.IPCODPACI
	where h.MANEXTPRO = 1
),
temp_HCORDINTE as (
	select h.AUTO, h.CODCENATE, h.UFUCODIGO, h.NUMINGRES, h.NUMEFOLIO, h.IPCODPACI, h.FECORDMED, h.CODPROSAL, h.CANSERIPS,
	h.CODSERIPS, h.TraceabilityPaperworkId, h.TraceabilityPaperworkEventsId, h.OBSSERIPS, h.CODDIAGNO, h.IDDESCRIPCIONRELACIONADA
	from .HCORDINTE h with(nolock)
	inner join temp_HCHISPACA t with(nolock) on t.NUMINGRES = h.NUMINGRES and t.NUMEFOLIO = h.NUMEFOLIO and t.IPCODPACI = h.IPCODPACI
	where h.MANEXTPRO = 1
),
temp_HCORDPRON as (
	select h.AUTO, h.CODCENATE, h.UFUCODIGO, h.NUMINGRES, h.NUMEFOLIO, h.IPCODPACI, h.FECORDMED, h.CODPROSAL, h.CANSERIPS,
	h.CODSERIPS, h.TraceabilityPaperworkId, h.TraceabilityPaperworkEventsId, h.OBSSERIPS, h.CODDIAGNO, h.IDDESCRIPCIONRELACIONADA
	from .HCORDPRON h with(nolock)
	inner join temp_HCHISPACA t with(nolock) on t.NUMINGRES = h.NUMINGRES and t.NUMEFOLIO = h.NUMEFOLIO and t.IPCODPACI = h.IPCODPACI
	where h.MANEXTPRO = 1
),
temp_HCORDPROQ as (
	select h.AUTO, h.CODCENATE, h.UFUCODIGO, h.NUMINGRES, h.NUMEFOLIO, h.IPCODPACI, h.FECORDMED, h.CODPROSAL, h.CANSERIPS,
	h.CODSERIPS, h.TraceabilityPaperworkId, h.TraceabilityPaperworkEventsId, h.OBSSERIPS, h.CODDIAGNO, h.IDDESCRIPCIONRELACIONADA
	from .HCORDPROQ h with(nolock)
	inner join temp_HCHISPACA t with(nolock) on t.NUMINGRES = h.NUMINGRES and t.NUMEFOLIO = h.NUMEFOLIO and t.IPCODPACI = h.IPCODPACI
	where h.MANEXTPRO = 1
),
temp_HCPRESCRD as (
	select h.ID, h.CODCENATE, h.UFUCODIGO, h.NUMINGRES, h.NUMEFOLIO, h.IPCODPACI, h.FECINIDOS, h.CODPROSAL, h.CANPEDPRO,
	h.CODPRODUC, h.TraceabilityPaperworkId, h.TraceabilityPaperworkEventsId
	from .HCPRESCRD h with(nolock)
	inner join temp_HCHISPACA t with(nolock) on t.NUMINGRES = h.NUMINGRES and t.NUMEFOLIO = h.NUMEFOLIO and t.IPCODPACI = h.IPCODPACI
	where h.MANEXTPRO = 1 and h.IDHCORDQUIMIO is null
),
temp_HCORHEMCO as (
	select h.ID, h.CODCENATE, h.NUMINGRES, h.NUMEFOLIO, h.IPCODPACI, h.FECORDMED, h.CODDIAGNO, t.UFUCODIGO
	from .HCORHEMCO h with(nolock)
	inner join temp_HCHISPACA t with(nolock) on t.NUMINGRES = h.NUMINGRES and t.NUMEFOLIO = h.NUMEFOLIO and t.IPCODPACI = h.IPCODPACI
	where h.MANEXTPRO = 1
),
temp_CUPSEntity as (
	select ce.Id, ce.Code, ce.Description, 
	cecd.Id CUPSEntityContractDescriptionId,
	cd.Id ContractDescriptionId, cd.Code ContractDescriptionCode, cd.Name ContractDescriptionName
	from Contract.CUPSEntity ce with(nolock) 
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd with(nolock) ON ce.Id = cecd.CUPSEntityId AND cecd.IsDelete = 0
	LEFT JOIN Contract.ContractDescriptions cd with(nolock) ON cecd.ContractDescriptionId = cd.Id
	where ce.Status = 1
)

SELECT	h.EntityName,
		h.EntityId,
		h.CareCenterCode,
		h.FunctionalUnitCode,
		h.AdmissionNumber,
		h.Folio,
		h.PatientCode,
		h.RequestDate, 
		h.ProfessionalCode,
		h.Quantity,
		h.Type,
		h.ItemId,
		h.ItemCode,
		h.ItemCodeOriginal,
		h.ItemName,
		h.CUPSEntityContractDescriptionId,
		h.ContractDescriptionId,
		h.DescriptionCodeName,
		h.TraceabilityPaperworkId,
		h.TraceabilityPaperworkEventsId,
		h.Observations,
		h.DiagnosticCode
FROM
(
	-- Ordenes de imagenes ambulatorias
	SELECT	'HCORDIMAG' EntityName, 
			h.AUTO EntityId,		
			h.CODCENATE CareCenterCode, 
			h.UFUCODIGO FunctionalUnitCode,
			h.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			h.IPCODPACI PatientCode,
			h.FECORDMED RequestDate, 
			h.CODPROSAL ProfessionalCode,
			h.CANSERIPS Quantity,
			1 Type,			
			ce.Id ItemId,
			ce.Code ItemCode,
			h.CODSERIPS ItemCodeOriginal,
			ce.Description ItemName,
			ce.CUPSEntityContractDescriptionId,
			ce.ContractDescriptionId,
			CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName,
			h.TraceabilityPaperworkId,
			h.TraceabilityPaperworkEventsId,
			h.OBSSERIPS Observations,
			h.CODDIAGNO DiagnosticCode
	FROM temp_HCORDIMAG h with(nolock)
	join temp_CUPSEntity ce with(nolock) on ce.Code = h.CODSERIPS and ce.CUPSEntityContractDescriptionId = h.IDDESCRIPCIONRELACIONADA
UNION ALL
	-- Ordenes de laboratorios ambulatorios
	SELECT	'HCORDLABO' EntityName, 
			h.AUTO EntityId,		
			h.CODCENATE CareCenterCode, 
			h.UFUCODIGO FunctionalUnitCode,
			h.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			h.IPCODPACI PatientCode,
			h.FECORDMED RequestDate, 
			h.CODPROSAL ProfessionalCode,
			h.CANSERIPS Quantity,
			1 Type,
			ce.Id ItemId,
			ce.Code ItemCode,
			h.CODSERIPS ItemCodeOriginal,
			ce.Description ItemName,
			ce.CUPSEntityContractDescriptionId,
			ce.ContractDescriptionId,
			CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName,
			h.TraceabilityPaperworkId,
			h.TraceabilityPaperworkEventsId,
			h.OBSSERIPS Observations,
			h.CODDIAGNO DiagnosticCode
	FROM temp_HCORDLABO h with(nolock)
	join temp_CUPSEntity ce with(nolock) on ce.Code = h.CODSERIPS and ce.CUPSEntityContractDescriptionId = h.IDDESCRIPCIONRELACIONADA
UNION ALL
	-- Ordenes de patologias ambulatorias
	SELECT	'HCORDPATO' EntityName, 
			h.AUTO EntityId,		
			h.CODCENATE CareCenterCode, 
			h.UFUCODIGO FunctionalUnitCode,
			h.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			h.IPCODPACI PatientCode,
			h.FECORDMED RequestDate, 
			h.CODPROSAL ProfessionalCode,
			h.CANSERIPS Quantity,
			1 Type,
			ce.Id ItemId,
			ce.Code ItemCode,
			h.CODSERIPS ItemCodeOriginal,
			ce.Description ItemName,
			ce.CUPSEntityContractDescriptionId,
			ce.ContractDescriptionId,
			CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName,
			h.TraceabilityPaperworkId,
			h.TraceabilityPaperworkEventsId,
			h.OBSSERIPS Observations,
			h.CODDIAGNO DiagnosticCode
	FROM temp_HCORDPATO h with(nolock)
	join temp_CUPSEntity ce with(nolock) on ce.Code = h.CODSERIPS and ce.CUPSEntityContractDescriptionId = h.IDDESCRIPCIONRELACIONADA
UNION ALL
	-- Ordenes de interconsultas ambulatorias
	SELECT	'HCORDINTE' EntityName, 
			h.AUTO EntityId,		
			h.CODCENATE CareCenterCode, 
			h.UFUCODIGO FunctionalUnitCode,
			h.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			h.IPCODPACI PatientCode,
			h.FECORDMED RequestDate, 
			h.CODPROSAL ProfessionalCode,
			h.CANSERIPS Quantity,
			1 Type,
			ce.Id ItemId,
			ce.Code ItemCode,
			h.CODSERIPS ItemCodeOriginal,
			ce.Description ItemName,
			ce.CUPSEntityContractDescriptionId,
			ce.ContractDescriptionId,
			CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName,
			h.TraceabilityPaperworkId,
			h.TraceabilityPaperworkEventsId,
			h.OBSSERIPS Observations,
			h.CODDIAGNO DiagnosticCode
	FROM temp_HCORDINTE h with(nolock)
	join temp_CUPSEntity ce with(nolock) on ce.Code = h.CODSERIPS and ce.CUPSEntityContractDescriptionId = h.IDDESCRIPCIONRELACIONADA
UNION ALL
	-- Ordenes de procedimientos no Qx ambulatorias
	SELECT	'HCORDPRON' EntityName, 
			h.AUTO EntityId,		
			h.CODCENATE CareCenterCode, 
			h.UFUCODIGO FunctionalUnitCode,
			h.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			h.IPCODPACI PatientCode,
			h.FECORDMED RequestDate, 
			h.CODPROSAL ProfessionalCode,
			h.CANSERIPS Quantity,
			1 Type,
			ce.Id ItemId,
			ce.Code ItemCode,
			h.CODSERIPS ItemCodeOriginal,
			ce.Description ItemName,
			ce.CUPSEntityContractDescriptionId,
			ce.ContractDescriptionId,
			CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName,
			h.TraceabilityPaperworkId,
			h.TraceabilityPaperworkEventsId,
			h.OBSSERIPS Observations,
			h.CODDIAGNO DiagnosticCode
	FROM temp_HCORDPRON h with(nolock)
	join temp_CUPSEntity ce with(nolock) on ce.Code = h.CODSERIPS and ce.CUPSEntityContractDescriptionId = h.IDDESCRIPCIONRELACIONADA
UNION ALL
	-- Ordenes de procedimientos Qx ambulatorias
	SELECT	'HCORDPROQ' EntityName, 
			h.AUTO EntityId,		
			h.CODCENATE CareCenterCode, 
			h.UFUCODIGO FunctionalUnitCode,
			h.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			h.IPCODPACI PatientCode,
			h.FECORDMED RequestDate, 
			h.CODPROSAL ProfessionalCode,
			h.CANSERIPS Quantity,
			1 Type,
			ce.Id ItemId,
			ce.Code ItemCode,
			h.CODSERIPS ItemCodeOriginal,
			ce.Description ItemName,
			ce.CUPSEntityContractDescriptionId,
			ce.ContractDescriptionId,
			CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName,
			h.TraceabilityPaperworkId,
			h.TraceabilityPaperworkEventsId,
			h.OBSSERIPS Observations,
			h.CODDIAGNO DiagnosticCode
	FROM temp_HCORDPROQ h with(nolock)
	join temp_CUPSEntity ce with(nolock) on ce.Code = h.CODSERIPS and ce.CUPSEntityContractDescriptionId = h.IDDESCRIPCIONRELACIONADA
UNION ALL
	-- Hemocomponentes
	SELECT 'HCORHEMCO' EntityName, 
			h.ID EntityId,		
			h.CODCENATE CareCenterCode, 
			h.UFUCODIGO FunctionalUnitCode,
			h.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			h.IPCODPACI PatientCode,
			h.FECORDMED RequestDate, 
			'' ProfessionalCode,
			hd.Quantity Quantity,
			1 Type,
			ce.Id ItemId,
			ce.Code ItemCode,
			hd.CODSERIPS ItemCodeOriginal,
			ce.Description ItemName,
			ce.CUPSEntityContractDescriptionId,
			ce.ContractDescriptionId,
			CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName,
			hd.TraceabilityPaperworkId,
			hd.TraceabilityPaperworkEventsId,
			'' Observations,
			h.CODDIAGNO DiagnosticCode
	FROM temp_HCORHEMCO h with(nolock) 
	JOIN 
	(
		SELECT	hd.HCORHEMCOID, 
				hd.CODSERIPS, 
				hd.TraceabilityPaperworkId, 
				hd.TraceabilityPaperworkEventsId, 
				hd.IDDESCRIPCIONRELACIONADA,
				COUNT(1) Quantity
		FROM .HCORHEMSER hd with(nolock)
		GROUP BY hd.HCORHEMCOID, hd.CODSERIPS, hd.TraceabilityPaperworkId, hd.TraceabilityPaperworkEventsId, hd.IDDESCRIPCIONRELACIONADA
	) hd ON h.ID = hd.HCORHEMCOID
	join temp_CUPSEntity ce with(nolock) on ce.Code = hd.CODSERIPS and ce.CUPSEntityContractDescriptionId = hd.IDDESCRIPCIONRELACIONADA
UNION ALL
	-- Ordenes de control por la especialidad que atendió al paciente
	SELECT	'HCDESCOEX' EntityName, 
			h.AUTO EntityId,		
			h.CODCENATE CareCenterCode, 
			h.UFUCODIGO FunctionalUnitCode,
			h.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			h.IPCODPACI PatientCode,
			hc.FECHISPAC RequestDate, 
			hc.CODPROSAL ProfessionalCode,
			1 Quantity,
			1 Type,			
			ce.Id ItemId,
			ce.Code ItemCode,
			h.CODSERIPS ItemCodeOriginal,
			ce.Description ItemName,
			ce.CUPSEntityContractDescriptionId,
			ce.ContractDescriptionId,
			CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName,
			h.TraceabilityPaperworkId,
			h.TraceabilityPaperworkEventsId,
			hc.INDICAMED Observations,
			hc.CODDIAGNO DiagnosticCode
	FROM temp_HCHISPACA hc with(nolock)
	JOIN .HCDESCOEX h with(nolock) ON hc.NUMINGRES = h.NUMINGRES AND hc.NUMEFOLIO = h.NUMEFOLIO
	join temp_CUPSEntity ce with(nolock) on ce.Code = h.CODSERIPS and ce.CUPSEntityContractDescriptionId = h.IDDESCRIPCIONRELACIONADA
UNION ALL
	-- Medicamentos extramurales que no sean de quimio
	SELECT	'HCPRESCRA' EntityName, 
			h.ID EntityId,		
			h.CODCENATE CareCenterCode, 
			h.UFUCODIGO FunctionalUnitCode,
			h.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			h.IPCODPACI PatientCode,
			h.FECINIDOS RequestDate, 
			h.CODPROSAL ProfessionalCode,
			h.CANPEDPRO Quantity,
			2 Type,
			ip.Id ItemId,
			ip.Code ItemCode,
			h.CODPRODUC ItemCodeOriginal,
			ip.Name ItemName,
			NULL CUPSEntityContractDescriptionId,
			NULL ContractDescriptionId,
			NULL DescriptionCodeName,
			h.TraceabilityPaperworkId,
			h.TraceabilityPaperworkEventsId,
			'' Observations,
			pres.CODDIAGNO DiagnosticCode
	FROM temp_HCPRESCRD h with(nolock)
	join 
	(
		SELECT a.NUMINGRES, a.NUMEFOLIO, a.CODPRODUC, MIN(a.CODDIAGNO) CODDIAGNO
		FROM .HCPRESCRA a with(nolock)
		GROUP BY a.NUMINGRES, a.NUMEFOLIO, a.CODPRODUC
	) pres on pres.NUMINGRES = h.NUMINGRES and pres.NUMEFOLIO = h.NUMEFOLIO and pres.CODPRODUC = h.CODPRODUC
	JOIN Inventory.ATC atc with(nolock) ON h.CODPRODUC = atc.Code
	JOIN Inventory.InventoryProduct ip with(nolock) ON atc.Id = ip.ATCId
UNION ALL
	-- Medicamentos extramurales que si esten en quimio
	SELECT	'HCORMEDICAMESQUEMA' EntityName, 
			h.ID EntityId,		
			his.CODCENATE CareCenterCode, 
			o.UFUCODIGO FunctionalUnitCode,
			his.NUMINGRES AdmissionNumber,
			h.NUMEFOLIO Folio,
			o.IPCODPACI PatientCode,
			his.FECHISPAC RequestDate, 
			o.CODPROSAL ProfessionalCode,
			temp.Quantity Quantity,
			2 Type,
			ip.Id ItemId,
			ip.Code ItemCode,
			h.CODPRODUC ItemCodeOriginal,
			ip.Name ItemName,
			NULL CUPSEntityContractDescriptionId,
			NULL ContractDescriptionId,
			NULL DescriptionCodeName,
			h.TraceabilityPaperworkId,
			h.TraceabilityPaperworkEventsId,
			'' Observations,
			o.CODDIAGNO DiagnosticCode
	FROM EHR.HCORMEDICAMESQUEMA h with(nolock)
	inner join EHR.HCORDQUIMIO o with(nolock) on o.ID = h.IDHCORDQUIMIO
	inner join temp_HCHISPACA his with(nolock) on his.IPCODPACI = o.IPCODPACI and his.NUMEFOLIO = h.NUMEFOLIO
	JOIN Inventory.ATC atc with(nolock) ON h.CODPRODUC = atc.Code
	JOIN Inventory.InventoryProduct ip with(nolock) ON atc.Id = ip.ATCId
    inner join (
        select hes.IDHCORDQUIMIO, hes.CICLO, hes.CODPRODUC, SUM(hes.CANTIDAD) Quantity
        from EHR.HCORDMEDICAM hes with(nolock)
        group by hes.IDHCORDQUIMIO, hes.CICLO, hes.CODPRODUC
    ) temp on h.IDHCORDQUIMIO = temp.IDHCORDQUIMIO and h.CICLO = temp.CICLO and h.CODPRODUC = temp.CODPRODUC
	LEFT JOIN [Authorization].TraceabilityPaperwork tp with(nolock) ON h.TraceabilityPaperworkId = tp.Id
UNION ALL
	-- Solicitudes Manuales
	SELECT	'TraceabilityPaperwork' EntityName, 
			h.Id EntityId,		
			h.CareCenterCode, 
			h.FunctionalUnitCode,
			h.AdmissionNumber,
			h.Folio,
			h.PatientCode,
			h.RequestDate, 
			h.ProfessionalCode,
			h.RequestQuantity Quantity,
			h.Type,
			ISNULL(ip.Id, ce.Id) ItemId,
			ISNULL(ip.Code, ce.Code) ItemCode,
			ISNULL(ip.Code, ce.Code) ItemCodeOriginal,
			ISNULL(ip.Name, ce.Description) ItemName,
			cecd.Id CUPSEntityContractDescriptionId,
			cd.Id ContractDescriptionId,
			CONCAT(cd.Code, ' - ', cd.Name) DescriptionCodeName,
			h.Id TraceabilityPaperworkId,
			tpem.Id TraceabilityPaperworkEventsId,
			'' Observations,
			'' DiagnosticCode
	FROM [Authorization].TraceabilityPaperwork h with(nolock)
	LEFT JOIN Inventory.InventoryProduct ip with(nolock) ON h.Type = 2 AND h.ServiceCode = ip.Code
	LEFT JOIN Contract.CUPSEntity ce with(nolock) ON h.Type = 1 AND h.ServiceCode = ce.Code	
	LEFT JOIN Contract.ContractDescriptions cd with(nolock) ON h.ContractDescriptionId = cd.Id
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd with(nolock) ON ce.Id = cecd.CUPSEntityId AND h.ContractDescriptionId = cecd.ContractDescriptionId
	LEFT JOIN
	(
		SELECT tpe.TraceabilityPaperworkId, MAX(tpe.Id) Id
		FROM [Authorization].TraceabilityPaperworkEvents tpe with(nolock)
		GROUP BY tpe.TraceabilityPaperworkId
	) tpem ON h.Id = tpem.TraceabilityPaperworkId
	WHERE (ISNULL(h.EntityName, '') = '' AND ISNULL(h.EntityId, 0) = 0) or h.EntityName = 'TraceabilityPaperwork'
) h
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista unificada de solicitudes de autorización de servicios médicos pendientes de aprobación por proveedor externo, consolidando todos los tipos de órdenes clínicas generadas en historia clínica: imágenes diagnósticas (radiología, ecografías, tomografías), laboratorios, patología, interconsultas, procedimientos quirúrgicos, pronósticos, prescripciones de medicamentos y hemoderivados. Integra ingresos activos (ADINGRESO), folios vigentes de historia clínica (HCHISPACA) y cada tipo de orden marcada como de manejo externo (MANEXTPRO=1), cruzando los servicios solicitados contra el catálogo de CUPS contratados (Contract.CUPSEntity) para obtener el código, nombre y descripción contractual del ítem. Sirve como fuente principal del módulo de autorizaciones para que el equipo de auditoría y contratación identifique qué procedimientos, exámenes, imágenes o medicamentos requieren aprobación externa, con información del paciente, profesional solicitante, ingreso, folio, cantidad, diagnóstico y trazabilidad del trámite.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListRequest';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado todas las solicitudes de servicios y medicamentos susceptibles de autorización (órdenes ambulatorias, hemocomponentes, controles, medicamentos extramurales, esquemas de quimioterapia y trámites manuales), unificando su identificación, ítem CUPS/producto y trazabilidad.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequest';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso (ADINGRESO) debe estar en estado distinto de ''A'' (anulado).; El folio en HCHISPACA debe tener ESTAFOLIO distinto de 0 (folio activo/no anulado).; Para órdenes clínicas (HCORDIMAG/LABO/PATO/INTE/PRON/PROQ/HCPRESCRD/HCORHEMCO) se requiere MANEXTPRO = 1, indicando manejo extramural/externo del procedimiento.; Para HCPRESCRD (medicamentos) se requiere IDHCORDQUIMIO IS NULL para excluir medicamentos pertenecientes a esquemas de quimioterapia.; Para emparejar con CUPS se requiere coincidencia de Code y CUPSEntityContractDescriptionId (descripción de contrato relacionada).; Las entidades CUPS deben tener Status = 1 (activas).; Las relaciones CUPSEntityContractDescriptions deben tener IsDelete = 0.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequest';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen solicitudes pertenecientes a ingresos no anulados (IESTADOIN <> ''A'') y folios no anulados (ESTAFOLIO <> 0).; Las órdenes clínicas mostradas son siempre de manejo extramural (MANEXTPRO = 1), es decir, candidatas a autorización externa.; Type = 1 representa servicios/procedimientos (CUPS); Type = 2 representa medicamentos/insumos.; Los medicamentos de esquemas de quimioterapia se reportan exclusivamente vía HCORMEDICAMESQUEMA y se excluyen explícitamente del listado de HCPRESCRD.; Para servicios CUPS, el match exige tanto el código de servicio como la descripción de contrato relacionada, asegurando vínculo contractual de facturación.; Las solicitudes manuales (TraceabilityPaperwork) que ya están vinculadas a una entidad clínica concreta (EntityName/EntityId con valor) no se duplican en este listado.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequest';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Authorization.ViewListRequest: Devuelve un dataset unificado vía UNION ALL de 11 orígenes distintos (HCORDIMAG, HCORDLABO, HCORDPATO, HCORDINTE, HCORDPRON, HCORDPROQ, HCORHEMCO, HCDESCOEX, HCPRESCRA, HCORMEDICAMESQUEMA, TraceabilityPaperwork) discriminados por la columna EntityName.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequest';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen tipo orden clínica ambulatoria (HCORDIMAG/LABO/PATO/INTE/PRON/PROQ) → Type = 1 (servicio CUPS); ItemId/ItemCode/ItemName se toman de Contract.CUPSEntity matcheando por CODSERIPS + IDDESCRIPCIONRELACIONADA.; si Origen HCORHEMCO (hemocomponentes) → Quantity = COUNT(*) de filas en HCORHEMSER agrupadas por HCORHEMCOID/CODSERIPS/Trazabilidad/IDDESCRIPCIONRELACIONADA; ProfessionalCode y Observations quedan en cadena vacía.; si Origen HCDESCOEX (control por especialidad) → Quantity fija = 1; RequestDate, ProfessionalCode, Observations y DiagnosticCode se toman del HCHISPACA asociado, no del propio HCDESCOEX.; si Origen HCPRESCRA con IDHCORDQUIMIO IS NULL (medicamento extramural NO de quimio) → Type = 2 (medicamento); ítem se resuelve por ATC (CODPRODUC→ATC.Code→InventoryProduct); CUPSEntityContractDescriptionId/ContractDescriptionId/DescriptionCodeName = NULL; CODDIAGNO = MIN por ingreso/folio/producto.; si Origen HCORMEDICAMESQUEMA (medicamento dentro de quimio) → Type = 2; Quantity = SUM(CANTIDAD) de HCORDMEDICAM agrupado por IDHCORDQUIMIO/CICLO/CODPRODUC; RequestDate desde HCHISPACA.FECHISPAC.; si Origen TraceabilityPaperwork → Solo se incluyen trámites con (EntityName vacío Y EntityId=0) o EntityName=''TraceabilityPaperwork'' (solicitudes manuales sin entidad clínica vinculada); ítem se resuelve a InventoryProduct si Type=2 o a CUPSEntity si Type=1; se toma el último evento (MAX(Id)) de TraceabilityPaperworkEvents.; si h.Type en TraceabilityPaperwork → Si Type=2 join contra Inventory.InventoryProduct por ServiceCode; si Type=1 join contra Contract.CUPSEntity por ServiceCode.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequest';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'ADINGRESO; HCHISPACA; HCORDIMAG; HCORDLABO; HCORDPATO; HCORDINTE; HCORDPRON; HCORDPROQ; HCPRESCRD; HCORHEMCO; HCORHEMSER; HCDESCOEX; HCPRESCRA; Contract.CUPSEntity; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Inventory.ATC; Inventory.InventoryProduct; EHR.HCORMEDICAMESQUEMA; EHR.HCORDQUIMIO; EHR.HCORDMEDICAM; Authorization.TraceabilityPaperwork; Authorization.TraceabilityPaperworkEvents', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequest';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequest';
GO
