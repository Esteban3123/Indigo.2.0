-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Inventory].[SP_InventoryContractReport] 
	-- Add the parameters for the stored procedure here
	@InventoryReportCode Varchar(20)
AS
BEGIN
	SET NOCOUNT ON;
	
    -- Insert statements for procedure here
	select ic.Code, Concat(ou.UnitCode, ' - ' , ou.UnitName) As OperatingUnitCodeName,
		Concat(ict.Code, ' - ', ict.[Name]) As ContractTypeName, ic.InitialDate, ic.EndDate,
		Concat(sp.Code, ' - ', sp.[Name]) As SupplierName,
		Concat(dl.Code, ' - ', dl.[Name]) As DistributionLineName,
		ic.ContractNumber, ic.[Description], ic.PaymentMethod, ic.DeliveryMethod, ic.DeliveryPlace,
		Case ic.SourceOrder When 1 Then 'Orden Simple' When 2 Then 'Llamado Oferta' When 3 Then 'Licitacion Publica' Else '' End As SourceOrder,
		Case ic.PurchaseProcess When 1 Then 'Directa' When 2 Then 'Negociacion Conjunta' Else '' End As PurchaseProcess,
		Case ic.Exclusivity When 1 Then 'SI' Else 'NO' End As Exclusivity,
		Case ic.OnlyGuarantee When 1 Then 'SI' Else 'NO' End As OnlyGuarantee,
		ic.TechnicalSupervicion, ic.SupervisionExecution, ic.Clauses, ic.Attachments, ic.Availability, ic.Resolution, ic.ResolutionDate,
		ic.QuoteNumber, ic.QuoteDate, ic.RecordNumber, ic.RecordDate, ic.NegotiationType, ic.Approved, ic.Deadline, ic.ValidityDate,
		ic.Value, ic.DiscountValue, ic.IvaValue, ic.TotalValue,
		Case ic.Status When 1 Then 'Registrado' When 2 Then 'Confirmado' When 3 then 'Anulado' Else '' End as Status, 
		ic.CreationUser, ic.CreationDate,
		ipr.Code As ProductCode, ipr.Name As ProductName, icd.Quantity As Quantity, icd.Value As ValueProduct,
		icd.OutstandingQuantity As OutstandingQuantity, icd.SubTotalValue, icd.TotalValue As TotalValueProduct
	From Inventory.InventoryContract ic With(Nolock)
	inner join Common.OperatingUnit ou With(Nolock) On ic.OperatingUnitId = ou.Id
	inner join Inventory.InventoryContractType ict With(Nolock) On ic.ContractTypeId = ict.Id
	inner join Common.Supplier sp With(Nolock) On ic.SupplierId = sp.Id
	inner join Common.SuppliersDistributionLines so With(Nolock) On ic.SupplierDistributionLineId = so.Id
	inner join Common.DistributionLines dl With(Nolock) on so.IdDistributionLine = dl.Id
	left join Inventory.InventoryContractDetail icd with(nolock) on icd.InventoryContractId = ic.Id
	left outer join Inventory.InventoryProduct ipr with(nolock) on icd.ProductId = ipr.Id
	where ic.Id = @InventoryReportCode
	
END