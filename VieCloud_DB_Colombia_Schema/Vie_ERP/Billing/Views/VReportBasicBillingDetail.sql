

-- =============================================
-- Author:		Cristian Camilo Bahamon Castaño
-- Create date: 2023-06-30
-- Description:	Vista para obtener la informacion de los detalles de la factura basica para enviar a facturacion electronica
-- =============================================

CREATE VIEW [Billing].[VReportBasicBillingDetail]
AS

	select 
		bb.Id BasicBillingId
		,bbd.Id BasicBillingDetailId
		,i.Id InvoiceId
		,i.InvoiceDate
		----PRODUCT
		,ip.Code ProductCode
		,ip.Name ProductName
		,ip.CodeAlternative
		,ip.CodeAlternativeTwo
		,ip.CodeCUM

		------SERVICE
		,bc.Code BillingConceptCode
		,bc.Name BillingConceptName

		-----ACTIVO FIJO
		,fai.Code FixedAssetCode
		,fai.Description FixedAssetName

		,bbd.DetailType

		,bbd.Price
		,bbd.Quantity InvoicedQuantity

		,bbd.Value --price*quantity 
		,bbd.ValueDiscount
		,bbd.PercentageIVA
		,((bbd.Value - bbd.ValueDiscount) * bbd.PercentageIVA)/100 ValueIva

		,bbd.Value - bbd.ValueDiscount TotalSalesPrice --subtotal - discount

		,(((bbd.Value - bbd.ValueDiscount) * bbd.PercentageIVA)/100 ) + bbd.Value - bbd.ValueDiscount GrandTotalSalesPrice 

from Billing.BasicBilling bb
join Billing.BasicBillingDetail bbd on bb.id = bbd.BasicBillingId
join Billing.Invoice i on i.Id = bb.InvoiceId
left join Inventory.InventoryProduct ip on bbd.ProductId = ip.Id
left join Billing.BillingConcept bc on bbd.BillingConceptId = bc.Id
left join FixedAsset.FixedAssetPhysicalAsset fapa on bbd.PhysicalAssetId = fapa.Id
left join [FixedAsset].[FixedAssetItem] fai on fapa.ItemId = fai.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de líneas de facturación básica asociadas a facturas electrónicas. Integra el encabezado de la factura básica y su factura de venta con cada ítem cobrado, que puede ser un producto del inventario (medicamento o insumo), un concepto de facturación (servicio o cargo) o un activo fijo físico. Para cada línea expone precio unitario, cantidad facturada, valor bruto, descuento, porcentaje de IVA, valor del IVA calculado, subtotal con descuento y total con impuestos incluidos. Se usa principalmente para alimentar el proceso de facturación electrónica y para reportería de cobros detallados por ítem.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VReportBasicBillingDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VReportBasicBillingDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los detalles de facturación básica con sus productos, conceptos o activos fijos asociados, calculando IVA, subtotal y total con impuestos para alimentar el reporte de facturación electrónica.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportBasicBillingDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe un BasicBilling vinculado a un Invoice (INNER JOIN sobre Billing.Invoice por InvoiceId).; Cada BasicBillingDetail debe estar asociado a un BasicBilling existente (INNER JOIN sobre BasicBilling).; El detalle puede o no tener producto, concepto de facturación o activo físico asignado (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportBasicBillingDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El IVA se calcula sobre la base ya descontada: (Value - ValueDiscount) * PercentageIVA / 100.; El subtotal de venta (TotalSalesPrice) es siempre Value menos ValueDiscount, sin impuestos.; El total general (GrandTotalSalesPrice) suma el subtotal con descuento más el IVA calculado.; Un detalle puede representar producto, concepto de facturación o activo fijo, pero los tres son opcionales (LEFT JOIN), determinándose la naturaleza por DetailType.; El activo fijo se resuelve en dos pasos: PhysicalAssetId → FixedAssetPhysicalAsset → ItemId → FixedAssetItem.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportBasicBillingDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura electrónica; Factura básica; Detalle de factura; Producto de inventario; Concepto de facturación; Activo fijo; IVA; Descuento; Código CUM; Códigos alternativos de producto', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportBasicBillingDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.VReportBasicBillingDetail: Devuelve una fila por cada BasicBillingDetail con sus cálculos: ValueIva = ((Value - ValueDiscount) * PercentageIVA)/100; TotalSalesPrice = Value - ValueDiscount; GrandTotalSalesPrice = TotalSalesPrice + ValueIva.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportBasicBillingDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.BasicBilling; Billing.BasicBillingDetail; Billing.Invoice; Inventory.InventoryProduct; Billing.BillingConcept; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportBasicBillingDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportBasicBillingDetail';
GO
