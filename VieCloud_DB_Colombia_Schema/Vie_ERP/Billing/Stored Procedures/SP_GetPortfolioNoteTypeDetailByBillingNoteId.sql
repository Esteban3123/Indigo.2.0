-- ===============================================================================================================
-- Author:		Cesar Collazos
-- Create date: 2025-01-20
-- Description:	Procedimiento que se encarga de traer detalles de notas de tipo Glosas
-- ==============================================================================================================

CREATE PROCEDURE [Billing].[SP_GetPortfolioNoteTypeDetailByBillingNoteId]
	@BillingNoteId INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
			id.Id,
			id.Id as InvoiceDetailId,
			i.Id as InvoiceId,
			bnd.Id as BillingNoteDetailId, 
			p.Code, 
			p.Name, 
			p.CodeAlternative,
			p.CodeAlternativeTwo,
			bg.Code + ' - ' + bg.Name BillingGroup,
			1 AS InvoicedQuantity, 
			pnarad.BaseValue AS UnitValue, 
			pnarad.BaseValue, 
			pnarad.Value TotalAdjustmentValue,
			COALESCE(pnarad.TaxValue,0) TaxValue, 
			COALESCE(pnarad.TaxPercentage,0) TaxPercentage, 
			tax.Code as TaxCode,
			NULL NameAlternative,
			NULL NameAlternativeTwo,
			tax.TaxClassificationType
		FROM Billing.BillingNote bn WITH (NOLOCK)
			JOIN Billing.BillingNoteDetail bnd WITH (NOLOCK) ON bn.Id = bnd.BillingNoteId
			JOIN Portfolio.PortfolioNote pn WITH (NOLOCK) ON pn.Id = bn.EntityId AND 'PortfolioNote' = bn.EntityName
			JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH(NOLOCK) on pn.Id = pnara.PortfolioNoteId
			JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad WITH(NOLOCK) ON pnara.Id = pnarad.PortfolioNoteAccountReceivableId
			JOIN Billing.InvoiceDetail id WITH(NOLOCK) on pnarad.EntityName ='InvoiceDetail' and pnarad.EntityId = id.Id
			JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sod.Id=id.ServiceOrderDetailId
			JOIN Inventory.InventoryProduct p WITH(NOLOCK) ON sod.ProductId = p.Id
			JOIN Billing.Invoice i WITH(NOLOCK) on i.Id =id.InvoiceId
			LEFT JOIN Billing.ProductServiceDetail psd WITH(NOLOCK) ON sod.Id = psd.ServiceOrderDetailId
			LEFT JOIN Contract.CUPSEntity cups WITH(NOLOCK) ON psd.CUPSEntityId=cups.Id
			LEFT JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = IIF(psd.CUPSEntityId IS NULL, p.BillingGroupId, cups.BillingGroupId)
			LEFT JOIN GeneralLedger.GeneralLedgerIVA tax WITH(NOLOCK) ON tax.Id = pnarad.TaxId
		WHERE (pn.NoteType = 6 or ISNULL(pn.EntityName,'')='Glosas') and sod.RecordType =2 and bn.Id =@BillingNoteId

		UNION ALL

		SELECT 
			id.Id,
			id.Id as InvoiceDetailId,
			i.Id as InvoiceId,
			bnd.Id as BillingNoteDetailId,  
			ips.Code, 
			ips.Name, 
			ce.Code CodeAlternative,
			ce.RIPSCode CodeAlternativeTwo,
			bg.Code + ' - ' + bg.Name BillingGroup,
			1 AS InvoicedQuantity,
			pnarad.BaseValue AS UnitValue, 
			pnarad.BaseValue, 
			pnarad.Value TotalAdjustmentValue,
			COALESCE(pnarad.TaxValue,0) TaxValue, 
			COALESCE(pnarad.TaxPercentage,0) TaxPercentage, 
			tax.Code TaxCode,
			ce.Description NameAlternative,
			ce.RIPSDescription NameAlternativeTwo,
			tax.TaxClassificationType
		FROM Billing.BillingNote bn WITH (NOLOCK)
			JOIN Billing.BillingNoteDetail bnd WITH (NOLOCK) ON bn.Id = bnd.BillingNoteId
			JOIN Portfolio.PortfolioNote pn WITH (NOLOCK) ON pn.Id = bn.EntityId AND 'PortfolioNote' = bn.EntityName
			JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH(NOLOCK) on pn.Id = pnara.PortfolioNoteId
			JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad WITH(NOLOCK) ON pnara.Id = pnarad.PortfolioNoteAccountReceivableId
			JOIN Billing.InvoiceDetail id WITH(NOLOCK) on pnarad.EntityName ='InvoiceDetail' and pnarad.EntityId = id.Id
			JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sod.Id=id.ServiceOrderDetailId
			JOIN Billing.Invoice i WITH(NOLOCK) on i.Id =id.InvoiceId
			JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = sod.IPSServiceId
			JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON ce.Id = sod.CUPSEntityId 
			JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = ce.BillingGroupId
			LEFT JOIN GeneralLedger.GeneralLedgerIVA tax WITH(NOLOCK) ON tax.Id = pnarad.TaxId
		WHERE (pn.NoteType = 6 or ISNULL(pn.EntityName,'')='Glosas') and sod.RecordType =1 and  bn.Id =@BillingNoteId

                 UNION ALL
                 
                SELECT 
			ids.Id,
			id.Id as InvoiceDetailId,
			i.Id as InvoiceId,
			bnd.Id as BillingNoteDetailId,  
			ips.Code, 
			ips.Name, 
			NULL CodeAlternative,
			NULL CodeAlternativeTwo,
			NULL BillingGroup,
			1 AS InvoicedQuantity,
			pnarad.BaseValue AS UnitValue, 
			pnarad.BaseValue, 
			pnarad.Value TotalAdjustmentValue,
			COALESCE(pnarad.TaxValue,0) TaxValue, 
			COALESCE(pnarad.TaxPercentage,0) TaxPercentage, 
			tax.Code TaxCode,
			NULL NameAlternative,
			NULL NameAlternativeTwo,
			tax.TaxClassificationType
		FROM Billing.BillingNote bn WITH (NOLOCK)
			JOIN Billing.BillingNoteDetail bnd WITH (NOLOCK) ON bn.Id = bnd.BillingNoteId
			JOIN Portfolio.PortfolioNote pn WITH (NOLOCK) ON pn.Id = bn.EntityId AND 'PortfolioNote' = bn.EntityName
			JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH(NOLOCK) on pn.Id = pnara.PortfolioNoteId
			JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad WITH(NOLOCK) ON pnara.Id = pnarad.PortfolioNoteAccountReceivableId
			JOIN Billing.InvoiceDetailSurgical ids WITH(NOLOCK) on pnarad.EntityName ='InvoiceDetailSurgical' and pnarad.EntityId = ids.Id
			JOIN Billing.InvoiceDetail id WITH(NOLOCK) on ids.InvoiceDetailId = id.Id
			JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on id.ServiceOrderDetailId=sod.Id
			JOIN Billing.Invoice i WITH(NOLOCK) on i.Id =id.InvoiceId
			JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = ids.IPSServiceId
			LEFT JOIN GeneralLedger.GeneralLedgerIVA tax WITH(NOLOCK) ON tax.Id = pnarad.TaxId
		WHERE pn.NoteType = 6 and sod.RecordType =1 and bn.Id =@BillingNoteId
         
                 UNION ALL

		SELECT 
			ids.Id,
			id.Id as InvoiceDetailId,
			i.Id as InvoiceId,
			bnd.Id as BillingNoteDetailId,  
			ips.Code, 
			ips.Name, 
			NULL CodeAlternative,
			NULL CodeAlternativeTwo,
			NULL BillingGroup,
			1 AS InvoicedQuantity,
			pnarad.BaseValue AS UnitValue, 
			pnarad.BaseValue, 
			pnarad.Value TotalAdjustmentValue,
			COALESCE(pnarad.TaxValue,0) TaxValue, 
			COALESCE(pnarad.TaxPercentage,0) TaxPercentage, 
			tax.Code TaxCode,
			NULL NameAlternative,
			NULL NameAlternativeTwo,
			tax.TaxClassificationType
		FROM Billing.BillingNote bn WITH (NOLOCK)
			JOIN Billing.BillingNoteDetail bnd WITH (NOLOCK) ON bn.Id = bnd.BillingNoteId
			JOIN Portfolio.PortfolioNote pn WITH (NOLOCK) ON pn.Id = bn.EntityId AND 'PortfolioNote' = bn.EntityName
			JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH(NOLOCK) on pn.Id = pnara.PortfolioNoteId
			JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad WITH(NOLOCK) ON pnara.Id = pnarad.PortfolioNoteAccountReceivableId
			JOIN Billing.ServiceOrderDetailSurgical ids WITH(NOLOCK) on pnarad.EntityName ='ServiceOrderDetailSurgical' and pnarad.EntityId = ids.Id
			JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on ids.ServiceOrderDetailId=sod.Id
			JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) on ar.Id = pnara.AccountReceivableId
			JOIN Billing.Invoice i WITH(NOLOCK) on i.Id =ar.InvoiceId
			JOIN Billing.InvoiceDetail id WITH(NOLOCK) on i.Id=id.InvoiceId and id.ServiceOrderDetailId=sod.Id
			JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = ids.IPSServiceId
			LEFT JOIN GeneralLedger.GeneralLedgerIVA tax WITH(NOLOCK) ON tax.Id = pnarad.TaxId
		WHERE (pn.NoteType = 6 or ISNULL(pn.EntityName,'')='Glosas') and sod.RecordType =1 and bn.Id =@BillingNoteId

		UNION ALL

		SELECT
			ibid.Id,
			ibid.Id AS InvoiceDetailId,
			i.Id AS InvoiceId,
			bnd.Id AS BillingNoteDetailId,
			ibid.ServiceCode AS Code,
			COALESCE(ce.Description, p.Name, ibid.ServiceCode) AS Name,
			ce.Code AS CodeAlternative,
			ce.RIPSCode AS CodeAlternativeTwo,
			NULL AS BillingGroup,
			ibid.Quantity AS InvoicedQuantity,
			ibid.UnitValue,
			pnarad.BaseValue,
			pnarad.Value AS TotalAdjustmentValue,
			COALESCE(pnarad.TaxValue, 0) AS TaxValue,
			COALESCE(pnarad.TaxPercentage, 0) AS TaxPercentage,
			tax.Code AS TaxCode,
			ce.Description AS NameAlternative,
			ce.RIPSDescription AS NameAlternativeTwo,
			tax.TaxClassificationType
		FROM Billing.BillingNote bn WITH (NOLOCK)
			JOIN Billing.BillingNoteDetail bnd WITH (NOLOCK) ON bn.Id = bnd.BillingNoteId
			JOIN Portfolio.PortfolioNote pn WITH (NOLOCK) ON pn.Id = bn.EntityId AND bn.EntityName = 'PortfolioNote'
			JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH (NOLOCK) ON pn.Id = pnara.PortfolioNoteId
			JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad WITH (NOLOCK) ON pnara.Id = pnarad.PortfolioNoteAccountReceivableId
			JOIN Portfolio.InitialBalanceInvoiceDetail ibid WITH (NOLOCK)
				ON pnarad.EntityName = 'InitialBalanceInvoiceDetail' AND pnarad.EntityId = ibid.Id
			JOIN Portfolio.InitialBalanceInvoice ibi WITH (NOLOCK) ON ibid.InitialBalanceInvoiceId = ibi.Id
			JOIN Billing.Invoice i WITH (NOLOCK) ON i.Id = ibi.InvoiceId
			LEFT JOIN Contract.CUPSEntity ce WITH (NOLOCK) ON ce.Code = ibid.ServiceCode
			LEFT JOIN Inventory.InventoryProduct p WITH (NOLOCK) ON p.Code = ibid.ServiceCode
			LEFT JOIN GeneralLedger.GeneralLedgerIVA tax WITH (NOLOCK) ON tax.Id = pnarad.TaxId
		WHERE pn.NoteType = 6 AND ISNULL(pn.EntityName, '') = 'InitialBalance' AND bn.Id = @BillingNoteId

		UNION ALL

		SELECT
			bbd.Id Id,
			bbd.Id  InvoiceDetailId,
			BB.InvoiceId InvoiceId,
			bnd.Id  BillingNoteDetailId,
			isnull(ip.Code, isnull(bc.Code, fai.Code)) Code,
			isnull(ip.Name, isnull(bc.Name, fai.Description)) Name,
			NULL CodeAlternative,
			NULL CodeAlternativeTwo,
			NULL BillingGroup,
			bbd.Quantity InvoicedQuantity,
			bbd.Price As UnitValue,
			bbd.value As BaseValue,
			((bbd.Value) + (IIF(bbd.PercentageIVA > 0, bbd.Value * bbd.PercentageIVA / 100, 0))) TotalAdjustmentValue,
			IIF(bbd.PercentageIVA > 0, bbd.Value * bbd.PercentageIVA / 100, 0) As TaxValue,
			bbd.PercentageIVA As TaxPercentage,
			IVA.Code TaxCode,
			NULL NameAlternative,
			NULL NameAlternativeTwo,
			IVA.TaxClassificationType
		FROM Billing.BillingNote bn
		INNER JOIN Billing.BillingNoteDetail bnd on bnd.BillingNoteId = bn.id
		INNER JOIN Billing.BasicBilling BB ON BB.InvoiceId = bnd.InvoiceId
		INNER JOIN Billing.BasicBillingDetail bbd on bbd.BasicBillingId = bb.id
		INNER JOIN Portfolio.PortfolioNote pn on pn.Id = bn.EntityId and bn.EntityName = 'PortfolioNote'
		LEFT JOIN Inventory.InventoryProduct ip on ip.Id = bbd.ProductId
		LEFT JOIN Billing.BillingConcept bc on bc.Id = bbd.BillingConceptId
		LEFT JOIN FixedAsset.FixedAssetPhysicalAsset fapa on fapa.Id = bbd.PhysicalAssetId
		LEFT JOIN FixedAsset.FixedAssetItem fai on fai.Id = fapa.ItemId
		LEFT JOIN GeneralLedger.GeneralLedgerIVA IVA on IVA.Id in (ip.IVAId, bc.IVAId, fai.IVAId)
		WHERE bn.Id =@BillingNoteId AND pn.EntityName = 'ReverseBasicBilling'
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el detalle de los ítems afectados por una nota de facturación (crédito o débito) asociada a una nota de cartera de tipo Glosa, dado el identificador de la nota de facturación. Combina información de la nota de facturación, la nota de cartera, los anticipos y el detalle de cuentas por cobrar con las líneas de facturación (servicios, procedimientos, medicamentos e insumos quirúrgicos), incluyendo valores base, ajustes, impuestos y códigos CUPS/RIPS de cada ítem glosado. Sirve para visualizar el desglose completo de las glosas aplicadas sobre facturas, mostrando qué servicios o productos fueron objetados, a qué factura pertenecen y cuáles son los valores ajustados, lo que apoya los procesos de auditoría de cuentas médicas, conciliación de cartera y respuesta a glosas de EPS u otras entidades pagadoras.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetPortfolioNoteTypeDetailByBillingNoteId';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetPortfolioNoteTypeDetailByBillingNoteId';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el detalle de ítems facturados (productos, servicios IPS y procedimientos quirúrgicos) afectados por una nota de cartera de tipo Glosa, con sus valores de ajuste e impuestos.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortfolioNoteTypeDetailByBillingNoteId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La BillingNote debe existir y estar vinculada a una PortfolioNote vía bn.EntityId con bn.EntityName = ''PortfolioNote''.; La PortfolioNote debe ser de tipo Glosa: pn.NoteType = 6 o ISNULL(pn.EntityName,'''') = ''Glosas'' (en el tercer bloque solo se exige pn.NoteType = 6).; Deben existir registros en PortfolioNoteAccountReceivableAdvance y PortfolioNoteAccountReceivableDetail asociados a la nota.; El PortfolioNoteAccountReceivableDetail debe referenciar entidades válidas según EntityName (''InvoiceDetail'', ''InvoiceDetailSurgical'' o ''ServiceOrderDetailSurgical'').', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortfolioNoteTypeDetailByBillingNoteId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las consultas filtran por la BillingNote recibida (bn.Id = @BillingNoteId).; Solo se consideran notas de cartera de tipo Glosa (NoteType = 6 o EntityName = ''Glosas''); el bloque de InvoiceDetailSurgical exige estrictamente NoteType = 6.; InvoicedQuantity siempre se devuelve como 1 (cantidad fija por línea de ajuste).; TaxValue y TaxPercentage nunca son NULL: se sustituyen por 0 mediante COALESCE.; UnitValue y BaseValue siempre se reportan con el mismo valor (pnarad.BaseValue).; Todas las lecturas usan WITH (NOLOCK) — lecturas sucias permitidas.; El BillingNote debe estar enlazado a una PortfolioNote (bn.EntityName = ''PortfolioNote'').', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortfolioNoteTypeDetailByBillingNoteId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Nota de cartera; Nota de facturación; Cuenta por cobrar; Anticipo; Detalle de factura; Orden de servicio; Procedimiento quirúrgico; CUPS; Servicio IPS; Grupo de facturación; IVA; RIPS', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortfolioNoteTypeDetailByBillingNoteId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna un conjunto unificado (UNION ALL de 4 bloques) con Id, InvoiceDetailId, InvoiceId, BillingNoteDetailId, código/nombre del producto o servicio, grupo de facturación, valor unitario/base, valor total de ajuste, IVA y código de impuesto, para la BillingNote indicada.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortfolioNoteTypeDetailByBillingNoteId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pnarad.EntityName = ''InvoiceDetail'' y sod.RecordType = 2 (producto de inventario) → Trae datos desde Inventory.InventoryProduct y resuelve BillingGroup vía CUPSEntity si existe, en caso contrario vía p.BillingGroupId; NameAlternative y NameAlternativeTwo se devuelven NULL.; si pnarad.EntityName = ''InvoiceDetail'' y sod.RecordType = 1 (servicio IPS) → Trae datos desde Contract.IPSService y Contract.CUPSEntity, usando ce.BillingGroupId como grupo de facturación e incluye Description y RIPSDescription como nombres alternativos.; si pnarad.EntityName = ''InvoiceDetailSurgical'' y sod.RecordType = 1 y pn.NoteType = 6 → Trae datos desde Billing.InvoiceDetailSurgical e IPSService asociado al detalle quirúrgico de la factura; CodeAlternative, CodeAlternativeTwo, BillingGroup y nombres alternativos se devuelven NULL.; si pnarad.EntityName = ''ServiceOrderDetailSurgical'' y sod.RecordType = 1 → Trae datos desde Billing.ServiceOrderDetailSurgical resolviendo la factura mediante Portfolio.AccountReceivable.InvoiceId, y enlaza el InvoiceDetail correspondiente al ServiceOrderDetail.; si psd.CUPSEntityId IS NULL (en el bloque de productos) → BillingGroup se toma de InventoryProduct.BillingGroupId. else BillingGroup se toma de CUPSEntity.BillingGroupId.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortfolioNoteTypeDetailByBillingNoteId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.BillingNote; Billing.BillingNoteDetail; Portfolio.PortfolioNote; Portfolio.PortfolioNoteAccountReceivableAdvance; Portfolio.PortfolioNoteAccountReceivableDetail; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Inventory.InventoryProduct; Billing.Invoice; Billing.ProductServiceDetail; Contract.CUPSEntity; Billing.BillingGroup; GeneralLedger.GeneralLedgerIVA; Contract.IPSService; Billing.InvoiceDetailSurgical; Billing.ServiceOrderDetailSurgical; Portfolio.AccountReceivable', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortfolioNoteTypeDetailByBillingNoteId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortfolioNoteTypeDetailByBillingNoteId';
-- GO
