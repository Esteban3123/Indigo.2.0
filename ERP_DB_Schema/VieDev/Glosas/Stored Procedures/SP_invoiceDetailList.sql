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