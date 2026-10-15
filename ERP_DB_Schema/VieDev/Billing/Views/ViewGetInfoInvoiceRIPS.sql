



-- =============================================
-- Author:		Cristian Camilo Bahamon Castaño
-- Create date: 2024-03-10
-- Description:	Vista encargada de obetner la informacion de la seccion ususiaio para RIPS electronicos
-- =============================================

CREATE VIEW [Billing].[ViewGetInfoInvoiceRIPS]
AS


	WITH Cte_Invoice as (	select	i.Id EntityId,
									i.InvoiceNumber EntityCode,
									ed.EntityName,
									ed.FilePath FilePathXML,
									CONCAT(	'ad',
											RIGHT(CONCAT('0000000000',th.Nit), 10),
											'000',
											SUBSTRING(cast(ed.Year as varchar),3,2),
											RIGHT(CONCAT('00000000',ed.Consecutive), 8),'.xml') AdFileName,
									i.CareGroupId,
									i.DocumentType,
									i.InvoiceCategoryId
							FROM Billing.Invoice i WITH(NOLOCK)
							JOIN Billing.ElectronicDocument ed on ed.EntityId = i.Id and ed.EntityName ='Invoice'
							JOIN GeneralLedger.GeneralLedgerSettings gs WITH(NOLOCK) on gs.IdOperatingUnit = ed.OperatingUnitId
							JOIN Common.ThirdParty th WITH(NOLOCK) on th.Id = gs.IdDian
							WHERE i.DocumentType in (1,2,4)),
	
	Cte_Capited AS (		SELECT iec.* 
							FROM Billing.InvoiceEntityCapitated iec WITH(NOLOCK)
							JOIN Cte_Invoice cte WITH(NOLOCK) ON cte.EntityId = iec.InvoiceId
							WHERE iec.Status=2)

	SELECT 
			i.EntityId,
			i.EntityCode,
			i.EntityName,
			i.FilePathXML,
			er.Retry RetryRIPS,
			CAST(1 AS TINYINT) DocumentType,
			null CareGroupCode,
			null CategoryInvoiceCode,
			NULL InitialDateCapitated,
			NULL EndDateCapitated,
			i.AdFileName,
			CAST(NULL AS TINYINT) AS NoteNature,
			er.CosmoDBId as InvoiceCosmoDBId,
			cast(0 as bit) IsTotalNote,
			ep.CUV InvoiceCUV,
			cast(cg.LiquidationType as TINYINT) CareGroupLiquidationType,
			CAST(NULL AS TINYINT) CapitedInvoicePeriod,
			CAST(ep.StatusRIPS AS TINYINT) StatusRIPS
	FROM Cte_Invoice i with(nolock) 
	LEFT JOIN Contract.CareGroup cg WITH(NOLOCK) on i.CareGroupId=cg.Id
	LEFT join Billing.ElectronicsProperties ep on i.EntityId = ep.EntityId and i.EntityName = ep.EntityName
	LEFT join Billing.ElectronicsRIPS er on er.ElectronicsPropertiesId = ep.Id
	where  i.DocumentType <> 4

	UNION ALL

	SELECT 
			i.EntityId,
			i.EntityCode,
			'InvoiceFixedAmount' AS EntityName,
			i.FilePathXML,
			er.Retry RetryRIPS,
			CAST(i.DocumentType AS TINYINT) AS DocumentType,
			cg.code CareGroupCode,
			ic.code CategoryInvoiceCode,
			icdTemp.InitialDate InitialDateCapitated,
			icdTemp.EndDate EndDateCapitated,
			i.AdFileName,
			CAST(NULL AS TINYINT) AS NoteNature,
			er.CosmoDBId as InvoiceCosmoDBId,
			cast(0 as bit) IsTotalNote,
			ep.CUV InvoiceCUV,
			CAST(cg.LiquidationType AS TINYINT) CareGroupLiquidationType,
			CAST(icd.InvoicePeriod AS TINYINT) AS CapitedInvoicePeriod,
			CAST(ep.StatusRIPS AS TINYINT) AS  StatusRIPS
	FROM Cte_Invoice i with(nolock)
	JOIN Contract.CareGroup cg WITH(NOLOCK) on i.CareGroupId=cg.Id
	JOIN Billing.InvoiceCategories ic WITH(NOLOCK) on i.InvoiceCategoryId = ic.Id
	join Cte_Capited icd WITH(NOLOCK) on icd.InvoiceId = i.EntityId
	LEFT JOIN Cte_Capited icdTemp WITH(NOLOCK) ON icd.PreviousRIPSInvoice = icdTemp.Id AND icd.InvoicePeriod in (2,3)
	LEFT join Billing.ElectronicsProperties ep  with(nolock) on i.EntityId = ep.EntityId and i.EntityName = ep.EntityName
	LEFT join Billing.ElectronicsRIPS er  with(nolock) on er.ElectronicsPropertiesId = ep.Id
	where  i.DocumentType=4

	union all

	SELECT 
			bn.Id EntityId,
			bn.Code EntityCode,
			ed.EntityName,
			ed.FilePath FilePathXML,
			er.Retry RetryRIPS,
			CAST(NULL AS TINYINT) DocumentType,
			NULL CareGroupCode,
			NULL CategoryInvoiceCode,
			NULL InitialDateCapitated,
			NULL EndDateCapitated,
			CONCAT(	'ad',
					RIGHT(CONCAT('0000000000',th.Nit), 10),
					'000',
					SUBSTRING(cast(ed.Year as varchar),3,2),
					RIGHT(CONCAT('00000000',ed.Consecutive), 8),'.xml') AdFileName,
			CAST(bn.Nature AS TINYINT) AS NoteNature,
			eri.CosmoDBId InvoiceCosmoDBId,
			cast(	case
					when bn.EntityName ='Invoice' then 1
					when bnd.AdjusmentValue = i.TotalInvoice and bn.Nature =2 then 1 
					else 0
				end as bit) IsTotalNote,
			epi.CUV InvoiceCUV,
			CAST(NULL AS TINYINT) AS CareGroupLiquidationType,
			CAST(NULL AS TINYINT) AS CapitedInvoicePeriod,
			CAST(ep.StatusRIPS AS TINYINT) AS  StatusRIPS
	FROM Billing.BillingNote bn WITH(NOLOCK)
	JOIN Billing.BillingNoteDetail bnd WITH(NOLOCK) on bn.Id = bnd.BillingNoteId
	JOIN Billing.Invoice i WITH(NOLOCK) on bnd.InvoiceId = i.Id
	JOIN Billing.ElectronicsProperties epi WITH(NOLOCK) on i.Id = epi.EntityId and epi.EntityName ='Invoice'
	JOIN Billing.ElectronicsRIPS eri WITH(NOLOCK) on epi.Id = eri.ElectronicsPropertiesId
	join Billing.ElectronicDocument ed on ed.EntityId = bn.Id
	JOIN GeneralLedger.GeneralLedgerSettings gs WITH(NOLOCK) on gs.IdOperatingUnit = ed.OperatingUnitId
	JOIN Common.ThirdParty th WITH(NOLOCK) on th.Id = gs.IdDian
	left join Billing.ElectronicsProperties ep on ed.EntityId = ep.EntityId and ed.EntityName = ep.EntityName
	left join Billing.ElectronicsRIPS er on er.ElectronicsPropertiesId = ep.Id
	where ed.EntityName = 'BillingNote'

	UNION ALL

		SELECT 
			i.Id as EntityId,
			i.InvoiceNumber as EntityCode,
			'Invoice' EntityName,
			CONCAT('C:\ProgramData\Indigo Technologies\ElectronicDocuments\',DB_NAME(),'\',YEAR(i.InvoiceDate),'\',MONTH(i.InvoiceDate),'\','Control Capitacion','\',i.InvoiceNumber) AS FilePathXML,
			er.Retry RetryRIPS,
			CAST(i.DocumentType AS TINYINT) AS DocumentType,
			cg.Code CareGroupCode,
			NULL CategoryInvoiceCode,
			NULL InitialDateCapitated,
			NULL EndDateCapitated,
			'' AS AdFileName,
			CAST(NULL AS TINYINT) AS NoteNature,
			er.CosmoDBId as InvoiceCosmoDBId,
			cast(0 as bit) IsTotalNote,
			ep.CUV InvoiceCUV,
			cast(cg.LiquidationType as TINYINT) CareGroupLiquidationType,
			CAST(NULL AS TINYINT) CapitedInvoicePeriod,
			CAST(ep.StatusRIPS AS TINYINT) StatusRIPS
	FROM Billing.Invoice i with(nolock)
	JOIN Contract.CareGroup cg WITH(NOLOCK) on i.CareGroupId=cg.Id
	LEFT join Billing.ElectronicsProperties ep  with(nolock) on i.Id = ep.EntityId and ep.EntityName ='Invoice'
	LEFT join Billing.ElectronicsRIPS er  with(nolock) on er.ElectronicsPropertiesId = ep.Id
	where i.DocumentType=5


	UNION ALL

		SELECT 
			iec.Id as EntityId,
			iec.code as EntityCode,
			'InvoiceEntityCapitated' EntityName,
			CONCAT('C:\ProgramData\Indigo Technologies\ElectronicDocuments\',DB_NAME(),'\',YEAR(iec.DocumentDate),'\',MONTH(iec.DocumentDate),'\','FacturaCapitacion','\',iec.Code) AS FilePathXML,
			er.Retry RetryRIPS,
			CAST(4 AS TINYINT) AS DocumentType,
			cg.Code CareGroupCode,
			ic.Code CategoryInvoiceCode,
			iecPr.InitialDate InitialDateCapitated,
			iecPr.EndDate EndDateCapitated,
			'' AS AdFileName,
			CAST(NULL AS TINYINT) AS NoteNature,
			er.CosmoDBId as InvoiceCosmoDBId,
			cast(0 as bit) IsTotalNote,
			ep.CUV InvoiceCUV,
			cast(cg.LiquidationType as TINYINT) CareGroupLiquidationType,
			iec.InvoicePeriod CapitedInvoicePeriod,
			CAST(ep.StatusRIPS AS TINYINT) StatusRIPS
	FROM Billing.InvoiceEntityCapitated iec with(nolock)
	JOIN Contract.CareGroup cg WITH(NOLOCK) on iec.CareGroupId=cg.Id
	JOIN Billing.InvoiceCategories ic WITH(NOLOCK) on iec.InvoiceCategoryId = ic.Id
	JOIN Cte_Capited iecPr WITH(NOLOCK) on iecPr.Id = iec.PreviousRIPSInvoice
	JOIN Billing.ElectronicsProperties ep  with(nolock) on iec.Id = ep.EntityId and ep.EntityName ='InvoiceEntityCapitated'
	JOIN Billing.ElectronicsRIPS er  with(nolock) on er.ElectronicsPropertiesId = ep.Id
	where iec.InvoicePeriod =3 and iec.Status=5