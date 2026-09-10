
CREATE VIEW [Authorization].[ViewListRequest_CTE]
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
	from .HCORDPRON  h with(nolock)
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
	join temp_CUPSEntity ce with(nolock) on ce.Code = h.CODSERIPS and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.IDDESCRIPCIONRELACIONADA,0)
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
	join temp_CUPSEntity ce with(nolock) on ce.Code = h.CODSERIPS and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.IDDESCRIPCIONRELACIONADA,0)
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
	join temp_CUPSEntity ce with(nolock) on ce.Code = h.CODSERIPS and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.IDDESCRIPCIONRELACIONADA,0)
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
	join temp_CUPSEntity ce with(nolock) on ce.Code = h.CODSERIPS and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.IDDESCRIPCIONRELACIONADA,0)
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
	join temp_CUPSEntity ce with(nolock) on ce.Code = h.CODSERIPS and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.IDDESCRIPCIONRELACIONADA,0)
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
	join temp_CUPSEntity ce with(nolock) on ce.Code = h.CODSERIPS and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.IDDESCRIPCIONRELACIONADA,0)
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
	join temp_CUPSEntity ce with(nolock) on ce.Code = h.CODSERIPS and ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.IDDESCRIPCIONRELACIONADA,0)
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de solicitudes de autorización de servicios externos pendientes o en proceso, generadas desde la historia clínica de pacientes en ingreso activo. Integra todos los tipos de órdenes médicas que requieren gestión externa (imágenes diagnósticas, laboratorios, patología, interconsultas, procedimientos quirúrgicos y no quirúrgicos, medicamentos en prescripción y hemoderivados), filtrando únicamente los ítems marcados como manejo externo (MANEXTPRO=1) y excluyendo ingresos anulados. Combina datos de admisión, folio de historia clínica, código del paciente (cédula), profesional solicitante, servicio CUPS, cantidad, diagnóstico CIE-10 y la descripción contractual asociada al servicio, para alimentar el módulo de autorizaciones y trazabilidad de trámites con aseguradoras o prestadores externos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListRequest_CTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListRequest_CTE';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola lista las solicitudes susceptibles de autorización (órdenes ambulatorias de imágenes, laboratorio, patología, interconsultas, procedimientos Qx/no Qx, hemocomponentes, controles, medicamentos extramurales —incluidos los de quimioterapia— y trámites manuales) unificando su mapeo a CUPS/inventario y a descripciones de contrato.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequest_CTE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso (ADINGRESO) debe estar en un estado distinto de ''A'' (IESTADOIN <> ''A'') para que sus folios sean considerados.; El folio clínico (HCHISPACA.ESTAFOLIO) debe ser distinto de 0.; Las órdenes clínicas (HCORDIMAG/LABO/PATO/INTE/PRON/PROQ/HEMCO y HCPRESCRD) deben tener MANEXTPRO = 1, es decir, marcadas como manejo extramural/profesional para entrar al flujo de autorización.; Las prescripciones de HCPRESCRD deben tener IDHCORDQUIMIO IS NULL (no pertenecer a un esquema de quimioterapia) para incluirse como medicamento extramural simple.; Los items CUPS solo se incluyen si Contract.CUPSEntity.Status = 1 (activos) y la relación CUPSEntityContractDescriptions tiene IsDelete = 0.; Los medicamentos requieren existir tanto en Inventory.ATC como en Inventory.InventoryProduct vinculados por ATCId.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequest_CTE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Por cada orden de imagen/laboratorio/patología/interconsulta/procedimiento Qx/no Qx ambulatoria con MANEXTPRO=1 cuyo folio e ingreso estén activos, retorna una fila Type=1 con EntityName del tipo de orden (HCORDIMAG, HCORDLABO, etc.) y datos del CUPS asociado.; [RETURN_RESULT] ResultSet: Para hemocomponentes (HCORHEMCO con MANEXTPRO=1), agrupa los servicios HCORHEMSER por (HCORHEMCOID, CODSERIPS, TraceabilityPaperworkId, TraceabilityPaperworkEventsId, IDDESCRIPCIONRELACIONADA) y emite una fila por grupo con Quantity = COUNT(1) y EntityName=''HCORHEMCO''.; [RETURN_RESULT] ResultSet: Para órdenes de control de la especialidad (HCDESCOEX) emite EntityName=''HCDESCOEX'' con Quantity fija en 1, RequestDate=HCHISPACA.FECHISPAC y Observations=HCHISPACA.INDICAMED.; [RETURN_RESULT] ResultSet: Medicamentos extramurales no oncológicos (HCPRESCRD con IDHCORDQUIMIO IS NULL): retorna EntityName=''HCPRESCRA'', Type=2, tomando el diagnóstico mínimo de HCPRESCRA agrupado por (NUMINGRES, NUMEFOLIO, CODPRODUC) y resolviendo el producto vía ATC→InventoryProduct.; [RETURN_RESULT] ResultSet: Medicamentos de esquemas de quimioterapia (HCORMEDICAMESQUEMA + HCORDQUIMIO): retorna EntityName=''HCORMEDICAMESQUEMA'', Type=2 y Quantity = SUM(HCORDMEDICAM.CANTIDAD) agrupado por (IDHCORDQUIMIO, CICLO, CODPRODUC).; [RETURN_RESULT] ResultSet: Solicitudes manuales: incluye filas de Authorization.TraceabilityPaperwork solo cuando (EntityName IS NULL o vacío) Y (EntityId IS NULL o 0), o cuando EntityName=''TraceabilityPaperwork''; resuelve el item por Type=2→InventoryProduct vía ServiceCode o Type=1→CUPSEntity vía ServiceCode, y selecciona el último evento (MAX(Id)) por TraceabilityPaperworkId.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequest_CTE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EntityName=''HCPRESCRA'' (medicamento extramural no oncológico): h.IDHCORDQUIMIO IS NULL en HCPRESCRD → Se modela como medicamento simple resuelto por ATC→InventoryProduct y diagnóstico mínimo de HCPRESCRA; si EntityName=''HCORMEDICAMESQUEMA'' (medicamento dentro de esquema de quimioterapia) → Se vincula HCORMEDICAMESQUEMA → HCORDQUIMIO y la cantidad se calcula como SUM(HCORDMEDICAM.CANTIDAD) por ciclo y producto; si Type del item: 1 = servicio CUPS, 2 = medicamento/producto de inventario → Determina si el ItemId/ItemCode/ItemName se resuelve contra Contract.CUPSEntity (Type=1) o contra Inventory.InventoryProduct (Type=2); si En solicitudes manuales: (EntityName='''' o NULL) AND (EntityId=0 o NULL) → Se incluye la trazabilidad como solicitud manual aún sin entidad clínica origen else Solo se incluye si EntityName=''TraceabilityPaperwork''; si Match CUPS contra orden: ce.Code = h.CODSERIPS AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(h.IDDESCRIPCIONRELACIONADA,0) → El servicio CUPS se asocia respetando la descripción de contrato relacionada (o ambos NULL tratados como 0)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequest_CTE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequest_CTE';
GO
