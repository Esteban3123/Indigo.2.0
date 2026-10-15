
-- =============================================
-- Author:		Cristian Camilo Bahamon Castaño
-- Create date: 2024-03-01
-- Description:	Vista encargada de obtener la informacion de las facturas, notas debito/ credito y notas de ajustes en rips electronicos
-- LastModification: Jose Reyes Paez
-- ModificationDate: 2025-02-21
-- =============================================

CREATE VIEW [Billing].[ViewElectronicsRIPSTraceability]
AS


	---Facturas RIPS
	select 
			er.Id,
			i.OperatingUnitId OperativeUnitId,
			ep.EntityId DocumentTypeId,
			CASE i.DocumentType
				WHEN 1 THEN 'EAPB con Contrato'
				WHEN 2 THEN 'EAPB Sin Contrato'
				WHEN 4 THEN 'Capitada'
				ELSE 'Factura'
			END AS DocumentTypeName,
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
	from Billing.ElectronicsProperties ep WITH (NOLOCK)
	JOIN Billing.ElectronicsRIPS  er WITH (NOLOCK) on ep.Id = er.ElectronicsPropertiesId
	JOIN Billing.Invoice i WITH (NOLOCK) on ep.EntityId = i.Id and i.Status = 1 and i.DocumentType <> 3 
	JOIN Common.ThirdParty tp WITH (NOLOCK) on i.ThirdPartyId = tp.Id
	LEFT JOIN INPACIENT paciente WITH (NOLOCK) on i.PatientCode = paciente.IPCODPACI
	LEFT JOIN Contract.CareGroup AS cp WITH (NOLOCK) ON i.CareGroupId = cp.Id
	OUTER APPLY (SELECT top 1
					rc.RadicatedConsecutive,
					rd.InvoiceNumber
				FROM Portfolio.RadicateInvoiceD rd WITH(NOLOCK)
				JOIN Portfolio.RadicateInvoiceC rc WITH(NOLOCK) 
					on rc.Id =rd.RadicateInvoiceCId
				where rd.InvoiceNumber =i.InvoiceNumber) rid
	where ep.EntityName = 'Invoice' and cast( i.InvoiceDate as DATE) >= '2025-02-01'

	union all

	--Notas RIPS
	select 
		er.Id,
		i.OperatingUnitId OperativeUnitId,
		ep.EntityId DocumentTypeId,
		CASE bn.Nature
			WHEN 1 THEN 'Debito'
			ELSE 'Credito'
		END AS DocumentTypeName,
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
	from Billing.ElectronicsProperties ep WITH (NOLOCK)
	JOIN Billing.ElectronicsRIPS er WITH (NOLOCK) on ep.Id = er.ElectronicsPropertiesId
	JOIN Billing.BillingNote bn WITH (NOLOCK) on ep.EntityId = bn.Id
	JOIN Billing.BillingNoteDetail bnd WITH (NOLOCK) on bnd.BillingNoteId = bn.Id
	JOIN Billing.Invoice i WITH (NOLOCK) on bnd.InvoiceId = i.Id
	JOIN Billing.ElectronicsProperties ep2 WITH(NOLOCK) on i.Id =ep2.EntityId AND ep2.EntityName ='Invoice' and ep2.StatusRIPS=2
	JOIN Billing.ElectronicsRIPS er2 WITH(NOLOCK) on er2.ElectronicsPropertiesId = ep2.Id
	JOIN Common.ThirdParty tp WITH (NOLOCK) on i.ThirdPartyId = tp.Id
	LEFT JOIN INPACIENT paciente WITH (NOLOCK) on i.PatientCode = paciente.IPCODPACI
	LEFT JOIN Contract.CareGroup AS cp WITH (NOLOCK)  ON i.CareGroupId = cp.Id
	where ep.EntityName = 'BillingNote' and cp.CareGroupType <> 3 -- Mostrar las que no son de tipo de particular

	union all 

	---Notas Ajuste RIPS
	select
		er.Id,
		ou.OperativeUnitId,
		ep.EntityId DocumentTypeId,
		'Ajuste' DocumentTypeName,
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
	from Billing.ElectronicsProperties ep WITH (NOLOCK)
	JOIN Billing.ElectronicsRIPS er WITH (NOLOCK) on ep.Id = er.ElectronicsPropertiesId
	CROSS JOIN (
		SELECT MIN(Id) AS OperativeUnitId
		FROM Common.OperatingUnit
	) ou
	where ep.EntityName = 'BillingNoteAdjustment'

	UNION ALL

	select 
			er.Id,
			iec.OperatingUnitId OperativeUnitId,
			ep.EntityId DocumentTypeId,
			'Capitada' AS DocumentTypeName,
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
			NULL  dmissionNumber,
			cg.Code AS CareGroupCode,
			cg.Name AS CareGroupName,
			0 RadicatedConsecutive
	from Billing.ElectronicsProperties ep WITH (NOLOCK)
	JOIN Billing.ElectronicsRIPS  er WITH (NOLOCK) on ep.Id = er.ElectronicsPropertiesId
	JOIN Billing.InvoiceEntityCapitated iec WITH(NOLOCK) on ep.EntityId = iec.Id and ep.EntityName ='InvoiceEntityCapitated'
	JOIN Contract.CareGroup cg WITH(NOLOCK) on cg.Id = iec.CareGroupId
	where iec.Status =5 and iec.InvoicePeriod = 3