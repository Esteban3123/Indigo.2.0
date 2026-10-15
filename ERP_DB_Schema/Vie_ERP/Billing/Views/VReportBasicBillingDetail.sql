

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