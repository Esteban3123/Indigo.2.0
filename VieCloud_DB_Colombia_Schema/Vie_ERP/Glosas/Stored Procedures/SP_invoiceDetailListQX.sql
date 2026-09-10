
-- =============================================
-- Author:		Rafael Patiño
-- Create date: 10/12/2014
-- Description:	Sp que retorna los detalles de las facturas Quirurgicos ERP Genesis
-- =============================================
CREATE PROCEDURE [Glosas].[SP_invoiceDetailListQX]
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

	/*set @container = 'DGEMPRES12'
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que retorna el detalle quirúrgico de una factura, dado el número de ítem de orden de servicio. Cruza los servicios quirúrgicos facturados (procedimientos de cirugía) con el catálogo de servicios de la IPS, el concepto de facturación o área de servicio, la cuenta contable de ingresos, el centro de costo y el código del profesional que ejecutó el procedimiento. Se usa en el módulo de Glosas para revisar o auditar el desglose quirúrgico de una factura: código y nombre del servicio, valor manual de tarifa, valor unitario facturado, cantidad facturada, área de servicio y cuenta contable asociada.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceDetailListQX';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceDetailListQX';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Retorna el detalle de servicios quirúrgicos facturados de una orden de servicio específica, incluyendo información del servicio, profesional, valores, centro de costo y cuenta contable de ingreso.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un detalle de orden de servicio cuyo Id corresponda al identificador recibido; El detalle quirúrgico debe estar relacionado con un servicio IPS, concepto de facturación, cuenta contable de ingreso y centro de costo válidos', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan detalles quirúrgicos que tengan correspondencia (INNER JOIN) en servicio IPS, concepto de facturación, cuenta contable principal de ingreso y centro de costo; El valor unitario y el valor facturado se reportan ambos desde TotalSalesPrice del detalle quirúrgico; El código y nombre del profesional médico se reportan ambos desde PerformsHealthProfessionalCode (no se resuelve nombre real)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Detalle de factura quirúrgica; Orden de servicio; Servicio IPS; Concepto de facturación; Centro de costo; Cuenta contable de ingreso; Profesional de salud; Precio de venta manual tarifario', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ServiceOrderDetailSurgical: Devuelve el conjunto de detalles quirúrgicos cuyo ServiceOrderDetailId coincide con el consecutivo recibido, enriquecido con datos de servicio IPS, concepto de facturación, cuenta contable y centro de costo', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetailSurgical; Billing.ServiceOrderDetail; Contract.IPSService; Billing.BillingConcept; GeneralLedger.MainAccounts; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX';
-- GO
