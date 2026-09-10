-- =============================================
-- Author:		Rafael Patiño
-- Create date: 10/12/2014
-- Description:	Sp que retorna los detalles de las facturas version ERP Genesis
-- =============================================
CREATE PROCEDURE [Glosas].[SP_invoiceDetailList] 
	@container varchar(50),
	@HISContainer varchar(50),
	@SecurityContainer varchar(50),
	@numerofactura varchar(15),
	@numeroConsecutivo varchar(15)
AS
BEGIN
	SET NOCOUNT ON;

   	Declare @tablaDetalleFactura table
	(
		InvoiceNumber varchar(50),
		ServiceDate datetime,
		ServiceCode varchar(20),
		ServiceName varchar(500),
		ServiceAreaCode varchar(10),
		DescriptionServiceArea varchar(300),
		InvoiceDetailId int,
		ServiceOrderDetailId int,
		MedicalCode varchar(20),
		MedicalName varchar(500),
		BillerCode varchar(20),
		BillerName varchar(200),
		BillingGroupCode varchar(20),
		BillingGroup varchar(120),
		ValueServiceManual money,
		UnitValue money,
		InvoicedValue money, 
		Ammount int,
		CostCenterCode varchar(14),
		CostCenterName varchar(500),
		TypeServiceProduct char(1),
		TypeProcedure char(1),
		AccountantAccountIncome varchar(30),
		Ingress varchar(50),					--dato para filtro no para persistir
		ServiceOrder varchar(50),				--dato para filtro no para persistir
		consecutiveOrder varchar(50),			--dato para filtro no para persistir
		ServiceNumber  varchar(50),				--dato para filtro no para persistir
		ConsecutivoInventory  varchar(50)		--dato para filtro no para persistir	
		,EntityValue money
		,PatientValue money 
	)

	DECLARE @sql AS NVARCHAR(MAX)	

	Set @sql = '
SELECT  
	inv.InvoiceNumber as InvoiceNumber,
	inv.InvoiceDate as ServiceDate,
	CUPS.Code as ServiceCode,
	CUPS.Description as ServiceName,
	ServGroup.Code as ServiceAreaCode,
	ServGroup.Name as DescriptionServiceArea,
	invD.id as InvoiceDetailId,
	ServOrDetail.Id as ServiceOrderDetailId,
	isnull(ServOrDetail.PerformsHealthProfessionalCode,'''') as MedicalCode,
	isnull(Prof.NOMMEDICO,'''') as MedicalName,
	userC.UserCode as BillerCode,
	Person.Fullname as BillerName,
	billingGroup.Code as BillingGroupCode,
	billingGroup.Name as BillingGroup,
	ServOrDetail.RateManualSalePrice as ValueServiceManual,
	ServOrDetail.TotalSalesPrice as UnitValue,
	ServOrDetail.GrandTotalSalesPrice as InvoicedValue,
	ServOrDetail.InvoicedQuantity as Ammount,
	Cost.Code as CostCenterCode, 
	Cost.Name as CostCenterName,
	''1'' as TypeServiceProduct,  --servicio
	convert(varchar(1),ServOrDetail.Presentation) as TypeProcedure,
	Account.Number as AccountantAccountIncome, --cuenta de servicio IPS
	ServOr.AdmissionNumber as Ingress,
	ServOr.Code as ServiceOrder,
	ServOrDetail.Id as consecutiveOrder,
	ServOr.Code as ServiceNumber,
	'''' as ConsecutivoInventory
	,invD.ThirdPartySalesPrice
	,invD.SubTotalPatientSalesPrice
FROM billing.invoice inv 
INNER JOIN billing.InvoiceDetail invD on inv.id = invD.InvoiceId 
INNER JOIN Billing.ServiceOrderDetail ServOrDetail on ServOrDetail.id = invD.ServiceOrderDetailId 
INNER JOIN Billing.ServiceOrder ServOr on ServOr.id = ServOrDetail.ServiceOrderId 
INNER JOIN [Contract].IPSService Serv on Serv.Id = ServOrDetail.IPSServiceId 
INNER JOIN Billing.BillingConcept ServGroup on ServGroup.Id = ServOrDetail.BillingConceptId 
INNER JOIN [Contract].CUPSEntity CUPS on CUPS.Id = ServOrDetail.CUPSEntityId 
INNER JOIN [Billing].[BillingGroup] billingGroup on billingGroup.Id = CUPS.BillingGroupId 
LEFT JOIN [GeneralLedger].[MainAccounts] Account on Account.Id = ServOrDetail.IncomeMainAccountId 
INNER JOIN Payroll.CostCenter Cost on Cost.Id = ServOrDetail.CostCenterId 
LEFT JOIN dbo.INPROFSAL Prof on Prof.CODPROSAL =  ServOrDetail.PerformsHealthProfessionalCode 
LEFT JOIN [Security].[User] userC on userC.id = ServOr.InvoicedUser 
LEFT JOIN [Security].[Person] Person on Person.Id = userC.IdPerson 
WHERE ServOrDetail.RecordType = 1 AND inv.Invoicenumber = @numerofactura

UNION ALL

SELECT  
	inv.InvoiceNumber as InvoiceNumber,
	inv.InvoiceDate as ServiceDate,
	Product.Code as ServiceCode,
	Product.name as ServiceName,
	fu.Code as ServiceAreaCode,
	fu.Name as DescriptionServiceArea,
	invD.id as InvoiceDetailId,
	ServOrDetail.Id as ServiceOrderDetailId,
	isnull(ServOrDetail.PerformsHealthProfessionalCode,'''') as MedicalCode,
	isnull(Prof.NOMMEDICO,'''') as MedicalName,
	userC.UserCode as BillerCode,
	Person.Fullname as BillerName,
	billingGroup.Code as BillingGroupCode,
	billingGroup.Name as BillingGroup,
	ServOrDetail.RateManualSalePrice as ValueServiceManual,
	ServOrDetail.TotalSalesPrice as UnitValue,
	ServOrDetail.GrandTotalSalesPrice as InvoicedValue,
	ServOrDetail.InvoicedQuantity as Ammount,
	Cost.Code as CostCenterCode, 
	Cost.Name as CostCenterName,
	''2'' as TypeServiceProduct,  --producto
	convert(varchar(1),isnull(ServOrDetail.Presentation,'' '')) as TypeProcedure,
	isnull(Account.Number,'''') as AccountantAccountIncome, --cuenta de servicio IPS
	ServOr.AdmissionNumber as Ingress,
	ServOr.Code as ServiceOrder,
	ServOrDetail.Id as consecutiveOrder,
	ServOr.Code as ServiceNumber,
	'''' as ConsecutivoInventory
	,invD.ThirdPartySalesPrice
	,invD.SubTotalPatientSalesPrice
FROM billing.invoice inv 
INNER JOIN billing.InvoiceDetail invD on inv.id = invD.InvoiceId 
INNER JOIN Billing.ServiceOrderDetail ServOrDetail on ServOrDetail.id = invD.ServiceOrderDetailId 
INNER JOIN Billing.ServiceOrder ServOr on ServOr.id = ServOrDetail.ServiceOrderId 
INNER JOIN [Inventory].[InventoryProduct] Product on Product.Id = ServOrDetail.ProductId 
INNER JOIN Payroll.FunctionalUnit fu on fu.Id = ServOrDetail.PerformsFunctionalUnitId 
INNER JOIN [Billing].[BillingGroup] billingGroup on billingGroup.Id = Product.BillingGroupId 
INNER JOIN Payroll.CostCenter Cost on Cost.Id = ServOrDetail.CostCenterId 
INNER JOIN Inventory.SettingInventory sett on inv.OperatingUnitId = sett.OperatingUnitId
LEFT JOIN Inventory.SettingInventoryFunctionalUnit settFunc on sett.AssociateCostMainAccount = 1 AND settFunc.FunctionalUnitId = ServOrDetail.PerformsFunctionalUnitId
LEFT JOIN Inventory.ProductGroupFunctionalUnit pgFunc on sett.AssociateCostMainAccount = 2 AND Product.ProductGroupId = pgFunc.ProductGroupId AND pgFunc.FunctionalUnitId = ServOrDetail.PerformsFunctionalUnitId
LEFT JOIN [GeneralLedger].[MainAccounts] Account on Account.Id = ISNULL(settFunc.SalesAccountId, pgFunc.SalesAccountId)
LEFT JOIN dbo.INPROFSAL Prof on Prof.CODPROSAL =  ServOrDetail.PerformsHealthProfessionalCode 
LEFT JOIN [Security].[User] userC on userC.id = ServOr.InvoicedUser 
LEFT JOIN [Security].[Person] Person on Person.Id = userC.IdPerson 
WHERE ServOrDetail.RecordType = 2  AND inv.Invoicenumber = @numerofactura'
	
	INSERT INTO @tablaDetalleFactura
	execute sp_executesql @sql,N'@numeroConsecutivo varchar(15), @numerofactura varchar(15)',@numeroConsecutivo, @numerofactura
	
	SELECT * FROM @tablaDetalleFactura
		
	--NOTA *
	--NOTA: FALTA REVISAR FILTROS CANTIDAD DE FILTROS  SLS.SERCOPCTA
	--NOTA * 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que retorna el detalle completo de los ítems facturados en una factura específica, identificada por número de factura o consecutivo. Integra tanto servicios clínicos (procedimientos CUPS) como productos de inventario (medicamentos e insumos), consolidando en un único resultado información de órdenes de servicio, valores unitarios, cantidades facturadas, centros de costo, grupo de facturación, médico que ejecutó el servicio, usuario facturador y cuentas contables de ingreso. Se utiliza en el módulo de Glosas para revisar y auditar el detalle de cada línea de factura, facilitando la identificación de ítems susceptibles de glosa por parte de la aseguradora o la IPS.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceDetailList';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceDetailList';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Retorna el detalle consolidado de una factura (servicios y productos) con datos clínicos, contables y de facturación para su uso en el módulo de glosas.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una factura en billing.invoice cuyo InvoiceNumber coincida con el número recibido.; Los detalles deben tener una ServiceOrderDetail asociada con RecordType 1 (servicio) o 2 (producto).; Para productos, debe existir configuración en Inventory.SettingInventory para la OperatingUnit de la factura.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El conjunto resultado solo contiene detalles de la factura solicitada por número de factura.; Los detalles se clasifican exclusivamente como servicio (''1'') o producto (''2'') según RecordType.; El código y nombre del médico se devuelven como cadena vacía si no existe (isnull).; Para productos, la cuenta contable se selecciona priorizando la asociada a la unidad funcional sobre la del grupo de producto (ISNULL(settFunc.SalesAccountId, pgFunc.SalesAccountId)).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Detalle de factura; Orden de servicio; Procedimiento CUPS; Producto de inventario; Profesional de la salud; Centro de costo; Unidad funcional; Cuenta contable de ingreso; Grupo de facturación; Glosas; IPS; Ingreso/Admisión del paciente', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @tablaDetalleFactura: Inserta una fila por cada detalle de factura con RecordType=1 (servicio) marcándolo como TypeServiceProduct=''1'' y resolviendo CUPS, grupo facturable y cuenta contable de ingreso de la IPS.; [INSERT] @tablaDetalleFactura: Inserta una fila por cada detalle de factura con RecordType=2 (producto) marcándolo como TypeServiceProduct=''2'', tomando datos del InventoryProduct y resolviendo la cuenta de ingreso según la unidad funcional o el grupo de producto.; [RETURN_RESULT] resultset: Devuelve todas las filas acumuladas en la tabla en memoria como un único resultset combinado de servicios y productos.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ServOrDetail.RecordType = 1 → Se procesa como servicio: se une con Contract.IPSService, Contract.CUPSEntity y Billing.BillingConcept; el tipo se marca como ''1'' y la cuenta contable proviene de ServOrDetail.IncomeMainAccountId. else Si RecordType = 2, se procesa como producto.; si ServOrDetail.RecordType = 2 → Se procesa como producto: se une con Inventory.InventoryProduct y Payroll.FunctionalUnit; el tipo se marca como ''2''.; si sett.AssociateCostMainAccount = 1 (productos) → La cuenta contable de ingreso se resuelve por la unidad funcional (SettingInventoryFunctionalUnit). else Si AssociateCostMainAccount = 2, la cuenta se resuelve por grupo de producto + unidad funcional (ProductGroupFunctionalUnit).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'billing.invoice; billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrder; Contract.IPSService; Billing.BillingConcept; Contract.CUPSEntity; Billing.BillingGroup; GeneralLedger.MainAccounts; Payroll.CostCenter; dbo.INPROFSAL; Security.User; Security.Person; Inventory.InventoryProduct; Payroll.FunctionalUnit; Inventory.SettingInventory; Inventory.SettingInventoryFunctionalUnit; Inventory.ProductGroupFunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList';
-- GO
