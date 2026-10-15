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