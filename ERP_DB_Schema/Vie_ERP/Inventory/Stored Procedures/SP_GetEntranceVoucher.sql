-- =============================================
-- Author:		Hector Rodriguez Rubiano
-- Create date: 11-08-2020
-- Description:	Consulta entrance voucher
-- =============================================
CREATE PROCEDURE [Inventory].[SP_GetEntranceVoucher]
	-- Add the parameters for the stored procedure here
	@EntranceVoucherId AS Int
	,@EntranceVoucherCode as varchar(20)
	,@Operacion as tinyint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if @EntranceVoucherCode is not null AND LEN(@EntranceVoucherCode) > 0
	begin
	select @EntranceVoucherId = id from Inventory.EntranceVoucher where Code = @EntranceVoucherCode
	end
	
    -- Insert statements for procedure here
	if @Operacion = 0
	begin
		select	CONCAT(s.Code, ' - ', s.Name, ' - ', dl.Code, ' - ', dl.Name) DescriptionSupplier
				, CONCAT(w.Code, ' - ', w.Name) DescriptionWarehouse
				, CONCAT(st.Code, ' - ', st.Name) DescriptionSupplierType
				, CONCAT(ba.Code, ' - ', ba.Name) DescriptionDocumentSupport
		from Inventory.EntranceVoucher ev
		join Common.Supplier s with(nolock) on ev.SupplierId = s.Id
		join Common.SuppliersDistributionLines sdl with(nolock) on ev.SupplierDistributionLineId = sdl.Id
		join Common.DistributionLines dl with(nolock) on sdl.IdDistributionLine = dl.Id
		join Inventory.Warehouse w with(nolock) on ev.WarehouseId = w.Id
		join Common.SupplierType st with(nolock) on ev.SupplierTypeId = st.Id
		LEFT JOIN Billing.BillingAuthorization ba WITH (NOLOCK) ON ev.DocumentSupportId = ba.Id
		where ev.Id = @EntranceVoucherId
	end

	if @Operacion = 1
	BEGIN
		select	evd.Id,
				evd.EntranceVoucherId,
				evd.ProductId,
				evd.Quantity,
				evd.EntranceSource,
				evd.SourceCode,
				evd.PurchaseOrderDetailId,
				evd.ContractDetailId,
				evd.RemissionEntranceDetailBatchSerialId,
				evd.UnitValue,
				evd.LastValue,
				evd.SubTotalValue,
				evd.NetoValue,
				evd.IvaPercentage,
				evd.IvaValue,
				evd.DiscountPercentage,
				evd.DiscountValue,
				evd.TotalValue,
				evd.RTFPercentage,
				evd.RTFValue,
				evd.ConsignmentInventoryRemissionDetailBatchSerialId
				,GroupCodeName = pg.Code + ' - ' + pg.Name
				,ProductCode = [ip].Code
				,ProductName = [ip].Name
				,HandlesBatch = psg.HandlesBatch
				,po.DeliveredDate
				,ISNULL(m.Name, '') ManufacturerName
				,ip.HealthRegistration
				,ip.Presentation
				,MinBase = isnull(rc.MinBase,0)
				,Rate = isnull(rc.Rate,0)
				,TypeRounding = isnull(rc.TypeRounding,1)
		from Inventory.EntranceVoucher ev with(nolock)
		join Common.Supplier s with(NOLOCK) on ev.SupplierId = s.Id  
		join Inventory.EntranceVoucherDetail evd with(nolock) on ev.Id = evd.EntranceVoucherId
		join Inventory.InventoryProduct [ip] with(NOLOCK) on evd.ProductId = [ip].Id
		join Inventory.ProductGroup pg with(nolock) on [ip].ProductGroupId = pg.Id
		join Inventory.ProductSubGroup psg with(nolock) on [ip].ProductSubGroupId = psg.Id
		join Payments.AccountPayableConcepts apc with(NOLOCK) on apc.id = case when s.Declarant = 1 then pg.DeclarantRetentionAccountPayableConceptId else pg.NotDeclarantRetentionAccountPayableConceptId end
		left join Inventory.PurchaseOrderDetail pod with(NOLOCK) on evd.PurchaseOrderDetailId = pod.Id
		left join Inventory.PurchaseOrder po with(NOLOCK) on pod.PurchaseOrderId = po.Id
		left join GeneralLedger.RetentionConcepts rc with(nolock) on rc.Id = apc.RetentionConceptId
		LEFT JOIN Inventory.Manufacturer m WITH (NOLOCK) ON ip.ManufacturerId = m.Id
		where ev.Id = @EntranceVoucherId
	END

	if @Operacion = 2
	BEGIN
	select 
	edbs.Id
	,edbs.EntranceVoucherDetailId
	,edbs.BatchSerialId
	,edbs.Quantity
	,edbs.OutstandingQuantity
	,CodeBatchSerial = bs.BatchCode
	from Inventory.EntranceVoucherDetail evd with(nolock)
	inner join Inventory.EntranceVoucherDetailBatchSerial edbs with(nolock) on evd.id = edbs.EntranceVoucherDetailId
	left join Inventory.BatchSerial bs with(nolock) on edbs.BatchSerialId = bs.Id
	where evd.EntranceVoucherId = @EntranceVoucherId
	END

	if @Operacion = 3
	BEGIN
	select 
	evc.Id
	, evc.EntranceVoucherId
	, evc.CommitmentDetailId
	, evc.Value
	,CommitmentCode = c.Code
	,CommitmentDocument = c.Document
	,CategoryCodeName = ca.Code + ' - ' + ca.Name
	,FinancialSourceCodeName = fs.Code + ' - ' + fs.Name
	,RevenueTypeCodeName = rt.Code + ' - ' + rt.Name
	,Balance = cd.Balance
	from Inventory.EntranceVoucher ev with(nolock)
	inner join Inventory.EntranceVoucherCommitment evc with(nolock) on ev.Id = evc.EntranceVoucherId 
	inner join Budget.CommitmentDetail cd with(nolock) on evc.CommitmentDetailId = cd.Id
	inner join Budget.Commitment c with(nolock) on cd.CommitmentId = c.Id
	inner join Budget.Category ca with(nolock) on cd.CategoryId = ca.Id
	inner join Budget.FinancialSource fs with(nolock) on ca.FinancialSourceId = fs.Id
	inner join Budget.RevenueType rt with(nolock) on cd.RevenueTypeId = rt.Id
	where ev.Id = @EntranceVoucherId
	END

	if @Operacion = 4
	begin
		select 
		evod.Id,
		evod.EntranceVoucherId,
		evod.OtherWithholdingDeductionId,
		evod.Type,
		evod.Value,
		evod.ValueOutstanding
		from Inventory.EntranceVoucherOtherDeduction evod
		where evod.EntranceVoucherId = @EntranceVoucherId
	end

END