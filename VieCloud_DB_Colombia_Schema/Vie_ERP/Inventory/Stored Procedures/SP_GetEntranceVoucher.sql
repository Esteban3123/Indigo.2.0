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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta el comprobante de entrada de mercancía (vale de entrada) al inventario, identificado por su ID o código. Según el modo de operación (@Operacion), retorna diferentes niveles de detalle: (0) encabezado del vale con datos del proveedor, línea de distribución, bodega, tipo de proveedor y documento soporte DIAN; (1) detalle de los productos recibidos, incluyendo cantidades, valores, IVA, descuentos, retenciones, grupo de producto, lote, orden de compra, fabricante y registro sanitario; (2) información de lotes y seriales asociados a cada ítem del vale; (3) compromisos presupuestales vinculados al vale, con fuente financiera, categoría y tipo de ingreso; (4) otras deducciones o retenciones adicionales aplicadas al comprobante de entrada. Este procedimiento es el punto central para visualizar una recepción de compra en bodega desde cualquier perspectiva contable, presupuestal o de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GetEntranceVoucher';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GetEntranceVoucher';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta multi-vista de un comprobante de entrada de inventario, devolviendo encabezado, detalle de productos, lotes/seriales, compromisos presupuestales u otras deducciones según la operación solicitada.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GetEntranceVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe suministrarse el Id del vale o un código no vacío que permita resolverlo desde Inventory.EntranceVoucher.; El parámetro de operación debe corresponder a uno de los modos soportados (0..4); de lo contrario no se retorna ningún resultado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GetEntranceVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El código de vale, si se provee, tiene prioridad sobre el Id recibido como parámetro.; El concepto de retención aplicado al detalle depende de la condición de declarante del proveedor.; Los valores de retención (MinBase, Rate, TypeRounding) se devuelven con valores por defecto (0,0,1) cuando no existe concepto de retención asociado.; El nombre del fabricante se devuelve como cadena vacía cuando el producto no tiene fabricante asociado.; Todas las consultas son de solo lectura y usan WITH(NOLOCK) en las uniones, salvo en la operación 4.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GetEntranceVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante/vale de entrada de inventario; Proveedor declarante / no declarante; Línea de distribución; Autorización de facturación (DIAN); Orden de compra; Lote y serial de producto; Registro sanitario; Retención en la fuente (RTF); IVA y descuentos; Compromiso presupuestal; Fuente financiera y tipo de ingreso; Otras deducciones / retenciones; Cuenta por pagar', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GetEntranceVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.EntranceVoucher: Cuando @Operacion=0, retorna descripciones concatenadas de proveedor+línea de distribución, bodega, tipo de proveedor y autorización de facturación de soporte para el vale identificado.; [RETURN_RESULT] Inventory.EntranceVoucherDetail: Cuando @Operacion=1, retorna el detalle de productos del vale con valores, IVA, descuentos, RTF, grupo/subgrupo, fabricante, registro sanitario, fecha de entrega de la OC y parámetros de retención (MinBase, Rate, TypeRounding) según si el proveedor es declarante o no.; [RETURN_RESULT] Inventory.EntranceVoucherDetailBatchSerial: Cuando @Operacion=2, retorna los lotes/seriales asociados a cada detalle del vale, con cantidad y cantidad pendiente.; [RETURN_RESULT] Inventory.EntranceVoucherCommitment: Cuando @Operacion=3, retorna los compromisos presupuestales vinculados al vale con categoría, fuente financiera, tipo de ingreso y saldo del detalle de compromiso.; [RETURN_RESULT] Inventory.EntranceVoucherOtherDeduction: Cuando @Operacion=4, retorna las otras deducciones/retenciones adicionales registradas para el vale.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GetEntranceVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @EntranceVoucherCode no nulo y con longitud > 0 → Resuelve @EntranceVoucherId buscando el Id en Inventory.EntranceVoucher por Code, ignorando el Id recibido.; si @Operacion = 0 → Devuelve cabecera descriptiva del vale (proveedor, bodega, tipo de proveedor, soporte de documento).; si @Operacion = 1 → Devuelve el detalle de productos del vale con datos comerciales, tributarios y de OC.; si @Operacion = 2 → Devuelve los lotes/seriales por cada detalle del vale.; si @Operacion = 3 → Devuelve los compromisos presupuestales asociados al vale.; si @Operacion = 4 → Devuelve las otras deducciones del vale.; si Common.Supplier.Declarant = 1 (en operación 1) → Usa ProductGroup.DeclarantRetentionAccountPayableConceptId para localizar el concepto de cuenta por pagar. else Usa ProductGroup.NotDeclarantRetentionAccountPayableConceptId.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GetEntranceVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail; Inventory.EntranceVoucherDetailBatchSerial; Inventory.EntranceVoucherCommitment; Inventory.EntranceVoucherOtherDeduction; Inventory.Warehouse; Inventory.InventoryProduct; Inventory.ProductGroup; Inventory.ProductSubGroup; Inventory.PurchaseOrderDetail; Inventory.PurchaseOrder; Inventory.Manufacturer; Inventory.BatchSerial; Common.Supplier; Common.SuppliersDistributionLines; Common.DistributionLines; Common.SupplierType; Billing.BillingAuthorization; Payments.AccountPayableConcepts; GeneralLedger.RetentionConcepts; Budget.CommitmentDetail; Budget.Commitment; Budget.Category; Budget.FinancialSource; Budget.RevenueType', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GetEntranceVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GetEntranceVoucher';
-- GO
