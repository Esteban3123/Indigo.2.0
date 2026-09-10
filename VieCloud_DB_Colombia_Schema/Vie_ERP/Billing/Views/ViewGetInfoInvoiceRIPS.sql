

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información necesaria para generar y enviar los RIPS electrónicos ante la DIAN, integrando facturas de venta, facturas de capitación (monto fijo), notas de facturación (crédito/débito) y facturas de control de capitación. Para cada documento obtiene: el número de factura, la ruta del archivo XML electrónico, el nombre del archivo AD (formato RIPS), el CUV, el estado de envío de RIPS, los reintentos de envío, el tipo de liquidación del grupo de atención, el período de capitación y el identificador en CosmosDB. Sirve como fuente principal para el proceso de facturación electrónica RIPS, permitiendo identificar qué documentos deben reportarse, en qué estado están y cuáles son sus datos de configuración electrónica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewGetInfoInvoiceRIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewGetInfoInvoiceRIPS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola estructura la información de cabecera necesaria para generar RIPS electrónicos a partir de facturas, facturas de capitación, notas crédito/débito y documentos de control de capitación, incluyendo nombre de archivo AD, CUV, estado RIPS y reintentos.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoInvoiceRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas (Invoice) deben tener un ElectronicDocument asociado con EntityName=''Invoice'' para entrar al CTE base.; La unidad operativa del documento electrónico debe tener configuración en GeneralLedger.GeneralLedgerSettings con un IdDian (tercero) válido para construir el AdFileName.; Para capitación filtrada por CTE_Capited, los registros de InvoiceEntityCapitated deben tener Status=2.; Para el bloque de control de capitación (DocumentType=5), debe existir la factura en Billing.Invoice.; Para el bloque final de InvoiceEntityCapitated, debe existir un PreviousRIPSInvoice referenciado y ElectronicsProperties/ElectronicsRIPS asociados.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoInvoiceRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El AdFileName se construye con prefijo ''ad'' + Nit del tercero DIAN rellenado a 10 dígitos + ''000'' + dos dígitos finales del año + consecutivo rellenado a 8 dígitos + ''.xml''.; El tercero usado para el Nit del AdFileName proviene de GeneralLedgerSettings.IdDian asociado a la unidad operativa del documento electrónico.; Las facturas de capitación (DocumentType=4) siempre traen EntityName=''InvoiceFixedAmount'' independientemente del valor original en ElectronicDocument.; Las filas del bloque DocumentType<>4 fijan DocumentType de salida en 1 (constante), perdiendo distinción entre tipos 1 y 2.; Solo se consideran InvoiceEntityCapitated con Status=2 para enlazar como capitación referenciada (Cte_Capited).; Para notas, IsTotalNote solo puede ser verdadero si la naturaleza es 2 y el ajuste iguala el total de la factura, o si la entidad relacionada es la propia factura.; CareGroupLiquidationType y CapitedInvoicePeriod solo se exponen para los bloques con CareGroup vinculado (capitación y control); en el bloque de notas y el bloque general son NULL.; StatusRIPS y CUV provienen siempre de Billing.ElectronicsProperties asociado a la entidad correspondiente.; RetryRIPS y CosmoDBId provienen siempre de Billing.ElectronicsRIPS ligado a las ElectronicsProperties.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoInvoiceRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIPS electrónicos; Factura; Factura de capitación; Nota crédito/débito; CUV (Código Único de Validación); CUFE / documento electrónico DIAN; Grupo de atención (CareGroup); Categoría de facturación; Periodo de facturación capitada; Control de capitación; NIT del emisor DIAN; Reintento de envío RIPS', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoInvoiceRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultado vista): Devuelve filas unificadas (UNION ALL) de cinco orígenes: facturas no capitadas (DocumentType in (1,2) tras filtrar i.DocumentType<>4), facturas de capitación (DocumentType=4), notas de facturación (BillingNote), control de capitación (DocumentType=5) y facturas de entidad capitada con InvoicePeriod=3 y Status=5.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoInvoiceRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si i.DocumentType IN (1,2,4) en CTE base → Solo facturas con tipos de documento 1, 2 o 4 son consideradas para los dos primeros bloques de la vista else Las demás se excluyen del CTE Cte_Invoice; si i.DocumentType <> 4 (primer SELECT) → Se emite fila con DocumentType=1 fijo, sin CareGroupCode ni CategoryInvoiceCode ni periodos de capitación; si i.DocumentType = 4 (segundo SELECT) → Se emite fila con datos de capitación: CareGroupCode, CategoryInvoiceCode, EntityName=''InvoiceFixedAmount'' y se hace LEFT JOIN al periodo previo (icdTemp) solo cuando icd.InvoicePeriod IN (2,3); si ed.EntityName = ''BillingNote'' → Se emite la fila como nota; IsTotalNote=1 cuando bn.EntityName=''Invoice'' o cuando bnd.AdjusmentValue = i.TotalInvoice y bn.Nature=2; en otro caso IsTotalNote=0 else NoteNature toma el valor de bn.Nature; si i.DocumentType = 5 → Se construye FilePathXML sintético con ruta ''C:\ProgramData\Indigo Technologies\ElectronicDocuments\<DB>\<año>\<mes>\Control Capitacion\<InvoiceNumber>'' y AdFileName vacío; si iec.InvoicePeriod = 3 AND iec.Status = 5 → Se incluye la InvoiceEntityCapitated con ruta sintética ''FacturaCapitacion'' y se toman fechas del periodo previo vía PreviousRIPSInvoice else Se excluye del último bloque; si CASE IsTotalNote: bn.EntityName=''Invoice'' OR (bnd.AdjusmentValue = i.TotalInvoice AND bn.Nature=2) → IsTotalNote=1 (nota total) else IsTotalNote=0', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoInvoiceRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.ElectronicDocument; GeneralLedger.GeneralLedgerSettings; Common.ThirdParty; Billing.InvoiceEntityCapitated; Contract.CareGroup; Billing.ElectronicsProperties; Billing.ElectronicsRIPS; Billing.InvoiceCategories; Billing.BillingNote; Billing.BillingNoteDetail', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoInvoiceRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoInvoiceRIPS';
GO
