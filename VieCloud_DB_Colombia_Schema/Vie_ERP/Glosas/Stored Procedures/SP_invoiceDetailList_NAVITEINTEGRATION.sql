
-- =============================================
-- Author:		Rafael Patiño
-- Create date: 16/04/2018
-- Description:	Sp  Nativo Integracion (Este tipo corresponde a cuando hay una reparametrizacion en VIE y se requiere seguir glosando facturas que vienen desde el mismo VIE pero de Otra BD)
-- =============================================
CREATE PROCEDURE [Glosas].[SP_invoiceDetailList_NAVITEINTEGRATION] 
	@container varchar(50),
	@HISContainer varchar(50),
	@SecurityContainer varchar(50),
	@numerofactura varchar(15),
	@numeroConsecutivo varchar(15)
AS
BEGIN
	SET NOCOUNT ON;

   	set @container = 'VIE08'
	
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
		Ingress varchar(50),					 --dato para filtro no para persistir
		ServiceOrder varchar(50),				--dato para filtro no para persistir
		consecutiveOrder varchar(50),			--dato para filtro no para persistir
		ServiceNumber  varchar(50),				--dato para filtro no para persistir
		ConsecutivoInventory  varchar(50)		--dato para filtro no para persistir	
		,EntityValue money
		,PatientValue money 
	)

	DECLARE @sql AS NVARCHAR(MAX)	

	Set @sql = 'USE ' + @container  +'
		SELECT  
		inv.InvoiceNumber as InvoiceNumber,
		inv.InvoiceDate as ServiceDate,
		Serv.Code as ServiceCode,
		Serv.name as ServiceName,
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
	FROM billing.invoice inv INNER JOIN
		billing.InvoiceDetail invD on inv.id = invD.InvoiceId INNER JOIN 
		Billing.ServiceOrderDetail ServOrDetail on ServOrDetail.id = invD.ServiceOrderDetailId INNER JOIN
		Billing.ServiceOrder ServOr on ServOr.id = ServOrDetail.ServiceOrderId INNER JOIN
		[Contract].IPSService Serv on Serv.Id = ServOrDetail.IPSServiceId INNER JOIN
		Billing.BillingConcept ServGroup on ServGroup.Id = ServOrDetail.BillingConceptId INNER JOIN
		[Contract].CUPSEntity CUPS on CUPS.Id = ServOrDetail.CUPSEntityId INNER JOIN
		[Billing].[BillingGroup] billingGroup on billingGroup.Id = CUPS.BillingGroupId LEFT JOIN
		[GeneralLedger].[MainAccounts] Account on Account.Id = ServOrDetail.IncomeMainAccountId INNER JOIN
		Payroll.CostCenter Cost on Cost.Id = ServOrDetail.CostCenterId LEFT JOIN
		' + @HISContainer + '..INPROFSAL Prof on Prof.CODPROSAL =  ServOrDetail.PerformsHealthProfessionalCode LEFT JOIN
		' + @SecurityContainer + '.[Security].[User] userC on userC.id = ServOr.InvoicedUser LEFT JOIN
		' + @SecurityContainer + '.[Security].[Person] Person on Person.Id = userC.IdPerson 
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
	LEFT JOIN ' + @HISContainer + '..INPROFSAL Prof on Prof.CODPROSAL =  ServOrDetail.PerformsHealthProfessionalCode 
	LEFT JOIN ' + @SecurityContainer + '.[Security].[User] userC on userC.id = ServOr.InvoicedUser 
	LEFT JOIN ' + @SecurityContainer + '.[Security].[Person] Person on Person.Id = userC.IdPerson 
	WHERE ServOrDetail.RecordType = 2  AND inv.Invoicenumber = @numerofactura		'
	
	INSERT INTO @tablaDetalleFactura
	execute sp_executesql @sql,N'@numeroConsecutivo varchar(15), @numerofactura varchar(15)',@numeroConsecutivo, @numerofactura
	
	SELECT * FROM @tablaDetalleFactura
		
	--NOTA *
	--NOTA: FALTA REVISAR FILTROS CANTIDA DE FILTROS  SLS.SERCOPCTA
	--NOTA * 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado usado en el módulo de Glosas para obtener el detalle de ítems facturados (servicios y productos/medicamentos) de una factura específica, en escenarios de reparametrización donde la factura fue generada en una base de datos VIE diferente a la actual (integración nativa entre instancias). Recibe el número de factura y un número consecutivo, consulta dinámicamente la base de datos de facturación indicada por el contenedor VIE08, y retorna para cada línea de factura: el servicio o producto cobrado, código CUPS, valor unitario, valor facturado, cantidades, centro de costo, grupo de facturación, profesional que realizó el servicio, facturador responsable y los valores a cargo de la entidad aseguradora y del paciente. Combina información de facturación, órdenes de servicio, inventario de productos, historia clínica (profesionales de salud) y seguridad (usuarios), cruzando múltiples contenedores de bases de datos para soportar el proceso de revisión y generación de glosas sobre facturas migradas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceDetailList_NAVITEINTEGRATION';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceDetailList_NAVITEINTEGRATION';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el detalle consolidado (servicios y productos) de una factura específica desde una BD VIE alterna, para permitir el proceso de glosas cuando la factura proviene de una instancia VIE reparametrizada.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList_NAVITEINTEGRATION';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El contenedor (BD) destino se fuerza internamente a ''VIE08'' ignorando el parámetro recibido.; Deben existir las BD indicadas por @HISContainer (con tabla INPROFSAL) y @SecurityContainer (con esquema Security y tablas User/Person).; Debe existir una factura con el InvoiceNumber suministrado en billing.invoice de la BD VIE08.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList_NAVITEINTEGRATION';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El contenedor de datos siempre se sobreescribe a ''VIE08'', anulando el parámetro @container recibido.; Solo se retornan registros cuyo InvoiceNumber coincide exactamente con el solicitado.; Los detalles se clasifican exclusivamente como servicio (RecordType=1, Type=''1'') o producto (RecordType=2, Type=''2'').; Códigos y nombres médicos se devuelven como cadena vacía cuando no existe profesional asociado (isnull a '''').; La cuenta contable de ingreso para productos depende de la parametrización AssociateCostMainAccount (1=por unidad funcional, 2=por grupo de producto).; ConsecutivoInventory siempre se devuelve como cadena vacía.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList_NAVITEINTEGRATION';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosas; Factura; Detalle de factura; Orden de servicio; Servicio IPS; CUPS; Grupo de facturación; Centro de costo; Profesional de la salud; Producto de inventario; Unidad funcional; Cuenta contable de ingreso; Integración VIE multi-base', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList_NAVITEINTEGRATION';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @tablaDetalleFactura: Inserta filas tipo servicio cuando ServiceOrderDetail.RecordType = 1 e InvoiceNumber coincide con el parámetro, marcando TypeServiceProduct = ''1''.; [INSERT] @tablaDetalleFactura: Inserta filas tipo producto cuando ServiceOrderDetail.RecordType = 2 e InvoiceNumber coincide, marcando TypeServiceProduct = ''2'' y resolviendo cuenta contable según configuración de inventario.; [RETURN_RESULT] (resultset): Devuelve el contenido completo de @tablaDetalleFactura como conjunto de resultados al consumidor.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList_NAVITEINTEGRATION';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ServiceOrderDetail.RecordType = 1 → Se trata como servicio: se enlaza con Contract.IPSService y BillingConcept y se marca TypeServiceProduct=''1''. else Si RecordType = 2 se trata como producto: se enlaza con Inventory.InventoryProduct, FunctionalUnit y configuración de inventario, marcando TypeServiceProduct=''2''.; si Inventory.SettingInventory.AssociateCostMainAccount = 1 → La cuenta contable de ingreso se toma desde SettingInventoryFunctionalUnit.SalesAccountId asociada a la unidad funcional que ejecuta. else Si AssociateCostMainAccount = 2, la cuenta contable se toma desde ProductGroupFunctionalUnit.SalesAccountId según el grupo de producto y unidad funcional.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList_NAVITEINTEGRATION';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'billing.invoice; billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrder; Contract.IPSService; Billing.BillingConcept; Contract.CUPSEntity; Billing.BillingGroup; GeneralLedger.MainAccounts; Payroll.CostCenter; Inventory.InventoryProduct; Payroll.FunctionalUnit; Inventory.SettingInventory; Inventory.SettingInventoryFunctionalUnit; Inventory.ProductGroupFunctionalUnit; INPROFSAL; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList_NAVITEINTEGRATION';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList_NAVITEINTEGRATION';
-- GO
