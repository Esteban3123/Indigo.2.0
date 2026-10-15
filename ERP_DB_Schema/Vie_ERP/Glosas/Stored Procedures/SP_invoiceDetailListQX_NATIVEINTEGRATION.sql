


-- =============================================
-- Author:		Rafael Patiño
-- Create date: 16/04/2018
-- Description:	Sp que retorna los detalles de las facturas Quirurgicos ERP Genesis
--  Nativo Integracion (Este tipo corresponde a cuando hay una reparametrizacion en VIE y se requiere seguir glosando facturas que vienen desde el mismo VIE pero de Otra BD)
-- =============================================
CREATE PROCEDURE [Glosas].[SP_invoiceDetailListQX_NATIVEINTEGRATION]
	@container varchar(50),
	@numeroConsecutivo varchar(15),
	@ordenServicio varchar(15),
	@ServiceCode varchar(15),
	@consecutiveOrder varchar(15),
	@ServiceNumber as varchar(15),
	@ConsecutivoInventory as varchar(15)
AS
BEGIN

	SET NOCOUNT ON;

  /* set @container = 'VIE08'
	set @numeroConsecutivo = '0000002016'
	set @ordenServicio = '0000028820'*/
	
	Declare @tablaDetalleFactura table(
		ServiceOrderDetailSurgicalId int,
		ServiceCode varchar(20),
		ServiceName varchar(300),
		MedicalCode varchar(20),
		MedicalName varchar(200),
		ValueServiceManual money,
		UnitValue money,
		InvoicedValue money, 
		Ammount int,
		CostCenterCode varchar(14),
		CostCenterName varchar(500),
		ServiceAreaCode varchar(10),
		DescriptionServiceArea varchar(300),
		AccountantAccountIncome varchar(30)
		)


		
		
	--	declare @sql as nVarchar(max)
	
	--set @sql=  
		SELECT 
			SerQX.id  as ServiceOrderDetailSurgicalId,
			Serv.Code as ServiceCode,
			Serv.name as ServiceName,
			SerQX.PerformsHealthProfessionalCode as MedicalCode,
			SerQX.PerformsHealthProfessionalCode as MedicalName,			
			ServOrDetail.RateManualSalePrice as ValueServiceManual,
			SerQX.TotalSalesPrice as UnitValue,
			SerQX.TotalSalesPrice as InvoicedValue,	
			SerQX.InvoicedQuantity as Ammount,		
			Cost.Code as CostCenterCode, 
			Cost.Name as CostCenterName,			
			ServGroup.Code as ServiceAreaCode,
			ServGroup.Name as DescriptionServiceArea,
			Account.Number as AccountantAccountIncome --cuenta de servicio IPS		
	   FROM 
			[Billing].[ServiceOrderDetailSurgical] SerQX  INNER JOIN 
			Billing.ServiceOrderDetail ServOrDetail on ServOrDetail.id = SerQX.ServiceOrderDetailId INNER JOIN
			[Contract].IPSService Serv on Serv.Id = SerQX.IPSServiceId INNER JOIN
			Billing.BillingConcept ServGroup on ServGroup.Id = ServOrDetail.BillingConceptId INNER JOIN
			[GeneralLedger].[MainAccounts] Account on Account.Id = SerQX.IncomeMainAccountId INNER JOIN
			Payroll.CostCenter Cost on Cost.Id = ServOrDetail.CostCenterId 
		WHERE ServOrDetail.id = @consecutiveOrder
			
			
	/*print @sql
	INSERT INTO @tablaDetalleFactura
	execute sp_executesql @sql ,N'@numeroConsecutivo varchar(15), @ordenServicio varchar(15), @ServiceCode varchar(15),@consecutiveOrder varchar(15),@ServiceNumber varchar(15),@ConsecutivoInventory varchar(15)',@numeroConsecutivo,@ordenServicio,@ServiceCode,@consecutiveOrder,@ServiceNumber,@ConsecutivoInventory
	
	SELECT * FROM @tablaDetalleFactura*/


END