CREATE VIEW [Billing].[ViewElectronicsRIPSTraceability]
AS

	---Facturas RIPS
	SELECT 
			er.Id,
			i.OperatingUnitId OperativeUnitId,
			ep.EntityId DocumentTypeId,
			CASE i.DocumentType
				WHEN 1 THEN 'EAPB con Contrato'
				WHEN 2 THEN 'EAPB Sin Contrato'
				WHEN 4 THEN 'Capitada'
				ELSE 'Factura'
			END AS DocumentTypeName,
			i.DocumentType DocumentType,
			cp.LiquidationType LiquidationType,
			i.InvoiceNumber DocumentNumber,
			i.InvoiceDate DocumentDate,
			i.ThirdPartyId ThirdPartyId,
			tp.Nit ThirdPartyNit,
			tp.Name ThirdPartyName,
			i.PatientCode PatientCode,
			paciente.IPNOMCOMP PatientName,
			ep.CUV,
			ep.StatusRIPS,
			er.sendDate,
			ep.EntityId,
			ep.EntityName,
			er.CosmoDBId,
			i.AdmissionNumber,
			cp.Code AS CareGroupCode,
			cp.Name AS CareGroupName,
			rid.RadicatedConsecutive
	FROM Billing.ElectronicsProperties ep 
	JOIN Billing.ElectronicsRIPS  er  on ep.Id = er.ElectronicsPropertiesId
	JOIN Billing.Invoice i  on ep.EntityId = i.Id and i.Status = 1 and i.DocumentType <> 3 and i.DocumentType <> 5
	JOIN Common.ThirdParty tp  on i.ThirdPartyId = tp.Id
	LEFT JOIN INPACIENT paciente  on i.PatientCode = paciente.IPCODPACI
	LEFT JOIN Contract.CareGroup AS cp  ON i.CareGroupId = cp.Id
	OUTER APPLY (SELECT top 1
					rc.RadicatedConsecutive,
					rd.InvoiceNumber
				FROM Portfolio.RadicateInvoiceD rd 
				JOIN Portfolio.RadicateInvoiceC rc  
					ON rc.Id = rd.RadicateInvoiceCId AND rc.[State] <> 4 -- Excluimos unicamente radicados anulados
				WHERE rd.InvoiceNumber =i.InvoiceNumber) rid
	WHERE ep.EntityName = 'Invoice' and cast( i.InvoiceDate as DATE) >= '2025-02-01'

	union all

	--Notas RIPS
	SELECT 
		er.Id,
		i.OperatingUnitId OperativeUnitId,
		ep.EntityId DocumentTypeId,
		CASE bn.Nature
			WHEN 1 THEN 'Debito'
			ELSE 'Credito'
		END AS DocumentTypeName,
		i.DocumentType DocumentType,
		cp.LiquidationType LiquidationType,
		bn.Code DocumentNumber,
		bn.NoteDate DocumentDate,
		i.ThirdPartyId ThirdPartyId,
		tp.Nit ThirdPartyNit,
		tp.Name ThirdPartyName,
		i.PatientCode PatientCode, 
		paciente.IPNOMCOMP PatientName,
		ep.CUV,
		ep.StatusRIPS,
		er.sendDate,	
		ep.EntityId,
		ep.EntityName,
		er.CosmoDBId,
		i.AdmissionNumber,
		cp.Code AS CareGroupCode,
		cp.Name AS CareGroupName,
		NULL RadicatedConsecutive
	FROM Billing.ElectronicsProperties ep 
	JOIN Billing.ElectronicsRIPS er  on ep.Id = er.ElectronicsPropertiesId
	JOIN Billing.BillingNote bn  on ep.EntityId = bn.Id
	JOIN Billing.BillingNoteDetail bnd  on bnd.BillingNoteId = bn.Id
	JOIN Billing.Invoice i  on bnd.InvoiceId = i.Id
	JOIN Billing.ElectronicsProperties ep2  on i.Id =ep2.EntityId AND ep2.EntityName ='Invoice' and ep2.StatusRIPS=2
	JOIN Billing.ElectronicsRIPS er2  on er2.ElectronicsPropertiesId = ep2.Id
	JOIN Common.ThirdParty tp  on i.ThirdPartyId = tp.Id
	LEFT JOIN INPACIENT paciente  on i.PatientCode = paciente.IPCODPACI
	LEFT JOIN Contract.CareGroup AS cp   ON i.CareGroupId = cp.Id
	WHERE ep.EntityName = 'BillingNote'
		AND (cp.Id IS NULL OR cp.CareGroupType <> 3) -- Mostrar las que no son de tipo particular y conservar notas sin grupo de atencion

	union all 

	---Notas Ajuste RIPS
	SELECT
		er.Id,
		ou.OperativeUnitId,
		ep.EntityId DocumentTypeId,
		'Ajuste' DocumentTypeName,
		CAST(NULL AS TINYINT) DocumentType,
		CAST(NULL AS TINYINT) LiquidationType,
		ep.EntityCode DocumentNumber,
		null DocumentDate,
		null ThirdPartyId,
		null ThirdPartyNit,
		null ThirdPartyName,
		null PatientCode, 
		null PatientName,
		ep.CUV,
		ep.StatusRIPS,
		er.sendDate,
		ep.EntityId,
		ep.EntityName,
		er.CosmoDBId,
		NULL AdmissionNumber,
		NULL CareGroupCode,
		NULL CareGroupName,
		NULL RadicatedConsecutive
	FROM Billing.ElectronicsProperties ep 
	JOIN Billing.ElectronicsRIPS er  on ep.Id = er.ElectronicsPropertiesId
	CROSS JOIN (
		SELECT MIN(Id) AS OperativeUnitId
		FROM Common.OperatingUnit
	) ou
	WHERE ep.EntityName = 'BillingNoteAdjustment'

	UNION ALL

	SELECT 
			er.Id,
			iec.OperatingUnitId OperativeUnitId,
			ep.EntityId DocumentTypeId,
			'Capitada' AS DocumentTypeName,
			CAST(4 AS TINYINT) DocumentType,
			cg.LiquidationType LiquidationType,
			iec.Code DocumentNumber,
			iec.DocumentDate DocumentDate,
			NULL ThirdPartyId,
			NULL ThirdPartyNit,
			CONCAT(cg.Code,' - ',cg.Name) ThirdPartyName,
			NULL PatientCode,
			NULL PatientName,
			ep.CUV,
			ep.StatusRIPS,
			er.sendDate,
			ep.EntityId,
			ep.EntityName,
			er.CosmoDBId,
			NULL AdmissionNumber,
			cg.Code AS CareGroupCode,
			cg.Name AS CareGroupName,
			0 RadicatedConsecutive
	FROM Billing.ElectronicsProperties ep 
	JOIN Billing.ElectronicsRIPS  er  on ep.Id = er.ElectronicsPropertiesId
	JOIN Billing.InvoiceEntityCapitated iec  on ep.EntityId = iec.Id and ep.EntityName ='InvoiceEntityCapitated'
	JOIN Contract.CareGroup cg  on cg.Id = iec.CareGroupId
	WHERE iec.Status =5 and iec.InvoicePeriod = 3
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Trazabilidad de documentos enviados electrónicamente como RIPS (Registros Individuales de Prestación de Servicios de Salud). Consolida en una sola consulta cuatro tipos de documentos electrónicos: facturas de cobro (EAPB con contrato, sin contrato, capitadas), notas débito y crédito, notas de ajuste y facturas de entidades capitadas, todos relacionados con sus propiedades de configuración electrónica (CUV, estado RIPS, fecha de envío) y su historial de radicación ante la cartera. Para cada documento expone datos del tercero pagador (NIT, nombre), del paciente (código, cédula, nombre completo), el grupo de atención del contrato, el número de radicado de cartera y el identificador en CosmoDB, permitiendo hacer seguimiento integral del ciclo de facturación electrónica y radicación de RIPS ante EPS y aseguradoras.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewElectronicsRIPSTraceability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewElectronicsRIPSTraceability';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista la trazabilidad de envíos electrónicos de RIPS para facturas, notas crédito/débito, notas de ajuste y facturas de capitación, exponiendo CUV, estado RIPS y datos del documento origen.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicsRIPSTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en Billing.ElectronicsProperties con EntityName en {''Invoice'',''BillingNote'',''BillingNoteAdjustment'',''InvoiceEntityCapitated''} ligados a Billing.ElectronicsRIPS por ElectronicsPropertiesId.; Para facturas: Invoice.Status = 1 y DocumentType <> 3.; Para notas: la factura asociada (vía BillingNoteDetail) debe tener un registro en ElectronicsProperties con EntityName=''Invoice'' y StatusRIPS=2.; Para capitadas: InvoiceEntityCapitated.Status = 5 y InvoicePeriod = 3.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicsRIPSTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las facturas anteriores al 2025-02-01 nunca aparecen en el segmento de facturas RIPS.; Las facturas con Status<>1 o DocumentType=3 nunca se incluyen.; Las notas crédito/débito solo se exponen si su factura asociada tiene RIPS en estado 2.; Las notas asociadas a CareGroup de tipo particular (CareGroupType=3) se excluyen; las notas sin CareGroup asociado se conservan.; Para notas de ajuste, OperativeUnitId siempre es 1 y los datos de tercero/paciente son NULL.; Para facturas capitadas filtradas, RadicatedConsecutive siempre es 0 y ThirdPartyId/Nit son NULL.; El RadicatedConsecutive se obtiene como TOP 1 desde Portfolio.RadicateInvoiceC/D por InvoiceNumber, exclusivamente en el segmento de facturas.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicsRIPSTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIPS electrónicos; CUV (Código Único de Validación); Factura; Nota crédito; Nota débito; Nota de ajuste; Factura capitada; EAPB con/sin contrato; Radicación de factura; Grupo de atención (CareGroup); Paciente; Tercero pagador', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicsRIPSTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewElectronicsRIPSTraceability: Devuelve la unión (UNION ALL) de cuatro conjuntos: facturas RIPS (EntityName=''Invoice'' con InvoiceDate >= ''2025-02-01''), notas RIPS (EntityName=''BillingNote''), notas de ajuste (EntityName=''BillingNoteAdjustment'') y facturas capitadas (EntityName=''InvoiceEntityCapitated'').', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicsRIPSTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ep.EntityName = ''Invoice'' AND CAST(i.InvoiceDate AS DATE) >= ''2025-02-01'' → Incluye la factura como documento RIPS y mapea DocumentTypeName según Invoice.DocumentType (1=''EAPB con Contrato'', 2=''EAPB Sin Contrato'', 4=''Capitada'', otro=''Factura'').; si ep.EntityName = ''BillingNote'' AND (cp.Id IS NULL OR cp.CareGroupType <> 3) → Incluye la nota; clasifica DocumentTypeName como ''Debito'' si BillingNote.Nature=1, de lo contrario ''Credito''. Excluye solo notas cuyo grupo de atención asociado es de tipo particular (CareGroupType=3), conservando notas sin grupo asociado.; si ep.EntityName = ''BillingNoteAdjustment'' → Incluye con DocumentTypeName=''Ajuste'' y OperativeUnitId fijo en 1, sin datos de tercero ni paciente.; si ep.EntityName=''InvoiceEntityCapitated'' AND iec.Status=5 AND iec.InvoicePeriod=3 → Incluye la factura capitada con DocumentTypeName=''Capitada'' y ThirdPartyName construido como ''Code - Name'' del CareGroup.; si Para notas RIPS: existe ElectronicsProperties.StatusRIPS=2 sobre la factura asociada → Solo entonces la nota crédito/débito aparece en la vista (la factura origen debe tener RIPS aceptado/estado 2).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicsRIPSTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ElectronicsProperties; Billing.ElectronicsRIPS; Billing.Invoice; Common.ThirdParty; INPACIENT; Contract.CareGroup; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC; Billing.BillingNote; Billing.BillingNoteDetail; Billing.InvoiceEntityCapitated', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicsRIPSTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicsRIPSTraceability';
GO
