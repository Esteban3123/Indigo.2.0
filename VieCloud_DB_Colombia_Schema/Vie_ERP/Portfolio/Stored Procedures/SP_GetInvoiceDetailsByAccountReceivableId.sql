-- ===============================================================================================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-12-13
-- Description:	Procedimiento que se encarga de obtener los detalles de una factura
-- ==============================================================================================================
CREATE PROCEDURE [Portfolio].[SP_GetInvoiceDetailsByAccountReceivableId]
	@AccountReceivableId INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @InvoiceId INT,
			@DocumentType TINYINT

	DECLARE @Table_Result AS TABLE
	(		
		EntityId INT, EntityName VARCHAR(20),
		----------------------------------
		BillingGroupCode VARCHAR(20),
		BillingGroupName VARCHAR(500),
		----------------------------------
		Code VARCHAR(20), 
		Name VARCHAR(500),
		----------------------------------
		MainAccountId INT,
		MainAccountNumber VARCHAR(20), 
		MainAccountName VARCHAR(500),
		----------------------------------
		CostCenterId INT,
		CostCenterCode VARCHAR(20), 
		CostCenterName VARCHAR(500),
		----------------------------------
		Value DECIMAL(18,2)
	)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@InvoiceId = i.Id,
				@DocumentType = i.DocumentType
		FROM Portfolio.AccountReceivable ar WITH(NOLOCK)
		JOIN Billing.Invoice i WITH(NOLOCK) ON ar.InvoiceId = i.Id
		WHERE ar.Id = @AccountReceivableId

		/********************************** OBTENCION DE DATOS **********************************/

		IF @DocumentType = 1 OR @DocumentType = 2 OR @DocumentType = 3 --OR @DocumentType = 5
		BEGIN
			INSERT INTO @Table_Result
				SELECT	id.Id, 'InvoiceDetail',
						----------------------------------
						bg.Code BillingGroupCode, bg.Name BillingGroupName,
						ips.Code, ips.Name,
						----------------------------------
						ma.Id, ma.Number, ma.Name,
						cc.Id, cc.Code, cc.Name,
						----------------------------------
						id.ThirdPartySalesPrice Value
				FROM Billing.InvoiceDetail id WITH(NOLOCK)
				JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON id.ServiceOrderDetailId = sod.Id
				JOIN Contract.IPSService ips WITH(NOLOCK) ON sod.IPSServiceId = ips.Id
				JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON sod.CUPSEntityId = ce.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON sod.IncomeMainAccountId = ma.Id
				JOIN Billing.BillingGroup bg WITH(NOLOCK) ON ce.BillingGroupId = bg.Id
				LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON IIF(ma.HandlesCostCenter = 1, sod.CostCenterId, NULL) = cc.Id
				WHERE id.InvoiceId = @InvoiceId
					AND sod.SettlementType <> 3
					AND sod.IsDelete = 0
					AND id.GrandTotalSalesPrice > 0
			UNION ALL
				SELECT	id.Id, 'InvoiceDetail',
						----------------------------------
						bg.Code BillingGroupCode, bg.Name BillingGroupName,
						ip.Code, ip.Name,
						----------------------------------
						ma.Id, ma.Number, ma.Name,
						cc.Id, cc.Code, cc.Name,
						----------------------------------
						id.ThirdPartySalesPrice Value
				FROM Billing.InvoiceDetail id WITH(NOLOCK)
				JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON id.ServiceOrderDetailId = sod.Id
				JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON sod.ProductId = ip.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON sod.IncomeMainAccountId = ma.Id
				LEFT JOIN Billing.BillingGroup bg WITH(NOLOCK) ON ip.BillingGroupId = bg.Id
				LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON IIF(ma.HandlesCostCenter = 1, sod.CostCenterId, NULL) = cc.Id
				WHERE id.InvoiceId = @InvoiceId
					AND sod.SettlementType <> 3
					AND sod.IsDelete = 0
					AND id.GrandTotalSalesPrice > 0
		END
		ELSE IF @DocumentType = 4
		BEGIN
			INSERT INTO @Table_Result
				SELECT	i.Id, 'Invoice',
						----------------------------------
						NULL BillingGroupCode, NULL BillingGroupName,
						NULL Code, 'Factura Monto Fijo' Name,
						----------------------------------
						ma.Id, ma.Number, ma.Name,
						cc.Id, cc.Code, cc.Name,
						----------------------------------
						i.InvoiceValue Value
				FROM Billing.Invoice i WITH(NOLOCK)
				JOIN Billing.InvoiceEntityCapitated iec WITH(NOLOCK) ON i.Id = iec.InvoiceId
				JOIN Contract.CareGroup cg WITH(NOLOCK) ON iec.CareGroupId = cg.Id
				JOIN Billing.SettingsBilling sb WITH(NOLOCK) ON i.OperatingUnitId = sb.IdOperatingUnit
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON sb.CapitationRevenueMainAccountId = ma.Id
				LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON IIF(ma.HandlesCostCenter = 1, cg.CostCenterId, NULL) = cc.Id
				WHERE i.Id = @InvoiceId
		END
	END TRY
	BEGIN CATCH	
		PRINT 'Error: ' + CAST(ERROR_MESSAGE() AS VARCHAR(MAX))
		PRINT 'Error Line: ' + CAST(ERROR_LINE() AS VARCHAR(MAX))

		DELETE FROM @Table_Result		
	END CATCH

	SELECT	tr.EntityId, tr.EntityName,
			----------------------------------
			tr.BillingGroupCode, tr.BillingGroupName,
			tr.Code, tr.Name,
			----------------------------------
			tr.MainAccountId, tr.MainAccountNumber, tr.MainAccountName,
			tr.CostCenterId, tr.CostCenterCode, tr.CostCenterName,
			----------------------------------
			tr.Value
	FROM @Table_Result tr
	ORDER BY tr.BillingGroupCode, tr.Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el detalle de los servicios y productos facturados asociados a una cuenta por cobrar específica de cartera. Dado el ID de la cuenta por cobrar, recupera la factura vinculada y, según el tipo de documento (factura de servicios, factura de monto fijo o capitación), devuelve cada línea facturada con su grupo de facturación, código y nombre del servicio o producto, cuenta contable de ingreso y centro de costo. Compone información de las tablas de detalle de factura, órdenes de servicio, catálogo de servicios de la IPS (CUPS), inventario de productos, cuentas del plan contable y centros de costo, permitiendo conocer el desglose económico de lo que se cobra a un tercero pagador (EPS, aseguradora, empresa) o paciente dentro del módulo de cartera y facturación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GetInvoiceDetailsByAccountReceivableId';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GetInvoiceDetailsByAccountReceivableId';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el desglose contable y de facturación (servicios, productos o monto fijo capitado) asociado a una cuenta por cobrar, junto con su grupo de facturación, cuenta contable de ingreso y centro de costo.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceDetailsByAccountReceivableId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cuenta por cobrar debe existir en Portfolio.AccountReceivable y estar enlazada a una factura en Billing.Invoice (de lo contrario @InvoiceId y @DocumentType quedan NULL y no se inserta nada).; La factura debe tener un DocumentType en (1,2,3,4) para producir resultados; otros tipos no generan filas.; Para DocumentType 1-3: deben existir InvoiceDetail con ServiceOrderDetail no eliminado, SettlementType<>3 y GrandTotalSalesPrice>0.; Para DocumentType 4: la factura debe tener registro en Billing.InvoiceEntityCapitated y configuración en Billing.SettingsBilling para la unidad operativa con CapitationRevenueMainAccountId definido.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceDetailsByAccountReceivableId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen líneas de orden de servicio con SettlementType distinto de 3 y no marcadas como eliminadas (IsDelete=0).; Solo se incluyen líneas de factura con GrandTotalSalesPrice mayor que 0.; El centro de costo se asigna únicamente cuando la cuenta contable principal tiene HandlesCostCenter=1; en caso contrario queda nulo.; Para tipos de documento 1-3 el valor reportado es ThirdPartySalesPrice (porción a cargo del tercero), no el total bruto.; Ante cualquier excepción no se devuelven filas (la tabla resultado se vacía y solo se imprime el error).; Los tipos de documento distintos de 1, 2, 3 y 4 no producen ningún detalle.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceDetailsByAccountReceivableId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por cobrar; Factura; Detalle de factura; Orden de servicio; Servicio IPS / CUPS; Producto de inventario; Cuenta contable de ingreso; Centro de costo; Grupo de facturación; Factura de capitación / monto fijo; Grupo de atención (CareGroup); Precio de venta a tercero (ThirdPartySalesPrice)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceDetailsByAccountReceivableId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Table_Result: Cuando DocumentType IN (1,2,3): inserta una fila por cada InvoiceDetail asociado a servicios (vía IPSService/CUPSEntity) con sod.SettlementType<>3, sod.IsDelete=0 y id.GrandTotalSalesPrice>0, usando ThirdPartySalesPrice como valor.; [INSERT] @Table_Result: Cuando DocumentType IN (1,2,3): además inserta una fila por cada InvoiceDetail asociado a productos de inventario (Inventory.InventoryProduct) bajo las mismas condiciones de filtro.; [INSERT] @Table_Result: Cuando DocumentType=4 (factura de monto fijo/capitación): inserta una sola fila con Name=''Factura Monto Fijo'', valor = i.InvoiceValue y cuenta contable tomada de SettingsBilling.CapitationRevenueMainAccountId.; [DELETE] @Table_Result: En caso de error capturado en el TRY/CATCH se vacía la tabla resultado para no devolver datos parciales.; [RETURN_RESULT] RESULT_SET: Devuelve el contenido de @Table_Result ordenado por BillingGroupCode y Code.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceDetailsByAccountReceivableId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @DocumentType IN (1,2,3) → Construye el detalle contable a partir de InvoiceDetail+ServiceOrderDetail, separando servicios (IPSService/CUPSEntity/BillingGroup) y productos (InventoryProduct), usando ThirdPartySalesPrice y la cuenta de ingreso del SOD. else Si DocumentType=4 se procesa como factura capitada de monto fijo; cualquier otro valor no genera filas.; si @DocumentType = 4 → Toma valor de Invoice.InvoiceValue y cuenta contable desde SettingsBilling.CapitationRevenueMainAccountId, asociando el centro de costo al CareGroup vía InvoiceEntityCapitated.; si ma.HandlesCostCenter = 1 → Resuelve el centro de costo usando sod.CostCenterId (o cg.CostCenterId en capitación); si la cuenta no maneja centro de costo, se fuerza NULL en el JOIN a Payroll.CostCenter.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceDetailsByAccountReceivableId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Contract.IPSService; Contract.CUPSEntity; GeneralLedger.MainAccounts; Billing.BillingGroup; Payroll.CostCenter; Inventory.InventoryProduct; Billing.InvoiceEntityCapitated; Contract.CareGroup; Billing.SettingsBilling', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceDetailsByAccountReceivableId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceDetailsByAccountReceivableId';
-- GO
