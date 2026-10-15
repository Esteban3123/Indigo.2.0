CREATE FUNCTION [Billing].[fnJournalVoucherIncomeInvoiceDetail]
	(
		@RevenueControlDetailId AS INT,
		@AccountingForSurgical AS TINYINT,
		@Process AS TINYINT, --(1. Liquidacion, 2. Distribucion Ingresos)
		@InvoiceDate DATE,
		@InvoiceId INT
	)
    RETURNS @JournalVourcherDetail TABLE 
	(
        IdMainAccount INT,
		IdThirdParty INT,
		IdCostCenter INT,
		DebitValue DECIMAL(21,5),
		CreditValue DECIMAL(21,5)
    )
AS
BEGIN

	DECLARE @LiquidateMasterAccount  BIT, 
			@CurrencyId INTEGER,
			@OperativeUnitId INTEGER


	SELECT TOP (1) @CurrencyId = ISNULL( i.CurrencyId,cs.OfficialCurrencyId),
				@OperativeUnitId = I.OperatingUnitId,
				@LiquidateMasterAccount = ISNULL(sb.LiquidateMasterAccount,0)
	FROM Billing.Invoice i WITH(NOLOCK) 
	JOIN GeneralLedger.CompanySettings cs WITH(NOLOCK) ON 1=1
	LEFT JOIN Billing.SettingsBilling sb WITH(NOLOCK) ON i.OperatingUnitId = sb.IdOperatingUnit
	WHERE i.RevenueControlDetailId = @RevenueControlDetailId 
		AND (
			(@InvoiceId IS NOT NULL AND i.Id = @InvoiceId)
			OR (@InvoiceId IS NULL AND i.Status = 1)
		)
	ORDER BY i.Id DESC


	---------------------------------Conversión y recálculo valores Folio --------------------------------------
		
		Declare @RecalculateFolioDetails Table(
										StatusResult			Bit,
										MessageResult			Varchar(Max),
	
										RevenueControlDetailId	INTEGER,
										SodDistributionId		INTEGER,
										SubTotalSalesPrice		NUMERIC(20,2),
										GrandTotalDiscount		NUMERIC(20,2),
										UnitGrossValue			NUMERIC(20,2),
										TaxPercentage			NUMERIC(20,2),
										NetWorth				NUMERIC(20,2),
										NetUnitValue			NUMERIC(20,2),
										GrandTotalTaxes			NUMERIC(20,2),
										GrandTotalSalesPrice	NUMERIC(20,2),
										TotalSalesPrice			NUMERIC(20,2),
										InvoicedQuantity		INTEGER,
										IvaId					INTEGER,
										InvoiceThirdPartySalesValue NUMERIC(20,2),
										SubTotalPatientSalesPrice	NUMERIC(20,2)) 
			INSERT INTO @RecalculateFolioDetails
								SELECT	StatusResult			,
										MessageResult	,
	
										RevenueControlDetailId	,
										SodDistributionId		,
										SubTotalSalesPrice		,
										GrandTotalDiscount		,
										UnitGrossValue			,
										TaxPercentage			,
										NetWorth				,
										NetUnitValue			,
										GrandTotalTaxes			,
										GrandTotalSalesPrice	,
										TotalSalesPrice			,
										InvoicedQuantity		,
										IvaId,
										InvoiceThirdPartySalesValue ,
										SubTotalPatientSalesPrice	
			FROM [Billing].[RecalculateFolioDetailsByCurrency](@RevenueControlDetailId,@CurrencyId,@InvoiceDate,@OperativeUnitId) temp
		
		IF EXISTS (SELECT 1 FROM @RecalculateFolioDetails WHERE StatusResult=0) BEGIN
			
			RETURN
		END
	--------------------------------------------------------------------------------------------------------------------------


	INSERT INTO @JournalVourcherDetail 
		(
			IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue
		)
		SELECT
			ma.Id AS IdMainAccount, 
			IIF(ma.HandlesThirdParty = 1, rcd.ThirdPartyId, NULL) AS IdThirdParty,
			IIF(ma.HandlesCostCenter = 1, sod.CostCenterId, NULL) AS IdCostCenter,
			0 AS DebitValue,
			SUM(IIF(@Process = 1,
						IIF(sodd.DistributionType = 2,
							temp.GrandTotalSalesPrice + temp.GrandTotalDiscount,
						temp.SubTotalSalesPrice),
				temp.InvoiceThirdPartySalesValue)) AS CreditValue
		FROM Billing.RevenueControlDetail rcd WITH(NOLOCK)
		JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) on sodd.RevenueControlDetailId = rcd.Id
		JOIN @RecalculateFolioDetails temp on sodd.Id = temp.SodDistributionId
		JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sodd.ServiceOrderDetailId = sod.Id
		JOIN Contract.CUPSEntity ce WITH(NOLOCK) on sod.CUPSEntityId = ce.Id
		JOIN Billing.BillingConcept bc WITH(NOLOCK) on ce.BillingConceptId = bc.Id
		JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = sod.IncomeMainAccountId
		WHERE rcd.Id = @RevenueControlDetailId 			
			AND sod.RecordType = 1 
			AND sod.Presentation <> 2 
			AND sod.IsDelete = 0
		GROUP BY ma.Id, ma.HandlesThirdParty, rcd.ThirdPartyId, ma.HandlesCostCenter, sod.CostCenterId

		UNION ALL
		-- Taxes de Servicios
		SELECT
			ma.Id AS IdMainAccount, 
			IIF(ma.HandlesThirdParty = 1, rcd.ThirdPartyId, NULL) AS IdThirdParty,
			IIF(ma.HandlesCostCenter = 1, sod.CostCenterId, NULL) AS IdCostCenter,		
			0 AS DebitValue,
			round(SUM(IIF(@Process = 1,
							IIF(sodd.DistributionType = 2,
								0,
						temp.GrandTotalTaxes),
					0)),2) AS CreditValue
		FROM Billing.RevenueControlDetail rcd WITH(NOLOCK)
		JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) on sodd.RevenueControlDetailId = rcd.Id
		JOIN @RecalculateFolioDetails temp on sodd.Id=temp.SodDistributionId
		JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sodd.ServiceOrderDetailId = sod.Id
		JOIN Contract.CUPSEntity ce WITH(NOLOCK) on sod.CUPSEntityId = ce.Id
		join Contract.IPSService ips WITH(NOLOCK) on ips.Id = sod.IPSServiceId
		join GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) on iva.Id = sod.IvaId
		JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = iva.IdAccountSale
		WHERE rcd.Id = @RevenueControlDetailId 			
			AND sod.RecordType = 1 
			AND sod.Presentation <> 2 
			AND sod.IsDelete = 0
		GROUP BY ma.Id, ma.HandlesThirdParty, rcd.ThirdPartyId, ma.HandlesCostCenter, sod.CostCenterId

		UNION ALL

		SELECT
			ma.Id as MainAccountId, 
			IIF(ma.HandlesThirdParty = 1, rcd.ThirdPartyId, NULL) AS IdThirdParty,
			IIF(ma.HandlesCostCenter = 1, sod.CostCenterId, NULL) AS IdCostCenter,		
			0 AS DebitValue,
			SUM(IIF(@Process = 1,
						IIF(sodd.DistributionType = 2,
							temp.GrandTotalSalesPrice + temp.GrandTotalDiscount,
						temp.SubTotalSalesPrice),
				temp.InvoiceThirdPartySalesValue)) as CreditValue
		FROM Billing.RevenueControlDetail rcd WITH(NOLOCK)
		JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) on sodd.RevenueControlDetailId = rcd.Id
		JOIN @RecalculateFolioDetails temp on sodd.Id=temp.SodDistributionId
		JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sodd.ServiceOrderDetailId = sod.Id
		JOIN Inventory.InventoryProduct ip WITH(NOLOCK) on sod.ProductId = ip.Id
		JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = sod.IncomeMainAccountId
		WHERE rcd.Id = @revenueControlDetailId 
			AND sod.RecordType = 2 
			AND sod.IsDelete = 0
		GROUP BY ma.Id, ma.HandlesThirdParty, rcd.ThirdPartyId, ma.HandlesCostCenter, sod.CostCenterId

		UNION ALL

		-- Taxes de productos
		SELECT
			iva.IdAccountSale as MainAccountId, 
			IIF(ma.HandlesThirdParty = 1, rcd.ThirdPartyId, NULL) AS IdThirdParty,
			IIF(ma.HandlesCostCenter = 1, sod.CostCenterId, NULL) AS IdCostCenter,		
			0 AS DebitValue,
			round(SUM(IIF(@Process = 1, 						
							IIF(sodd.DistributionType = 2, 
								0,
							temp.GrandTotalTaxes),
						0)),2) as CreditValue
		FROM Billing.RevenueControlDetail rcd WITH(NOLOCK)
		JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) on sodd.RevenueControlDetailId = rcd.Id
		JOIN @RecalculateFolioDetails temp on sodd.Id=temp.SodDistributionId
		JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sodd.ServiceOrderDetailId = sod.Id
		JOIN Inventory.InventoryProduct ip WITH(NOLOCK) on sod.ProductId = ip.Id
		JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) on iva.id = sod.IvaId
		JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = iva.IdAccountSale
		WHERE rcd.Id = @revenueControlDetailId 
			AND sod.RecordType = 2 
			AND sod.IsDelete = 0
		GROUP BY iva.IdAccountSale, ma.HandlesThirdParty, rcd.ThirdPartyId, ma.HandlesCostCenter, sod.CostCenterId

	
	IF @AccountingForSurgical = 2
	BEGIN
		DECLARE @TableDetailSurgical AS TABLE 
		(
			ServiceOrderDetailId INT, 
			ServiceOrderDetailSurgicalId INT, 
			MainAccountId INT,
			ThirdPartyId INT,
			TotalSalesPrice DECIMAL(18,2),
			Value decimal(18,2)	,
			CostCenterId INT
		)

		INSERT INTO @TableDetailSurgical
			(
				ServiceOrderDetailId, ServiceOrderDetailSurgicalId, MainAccountId, ThirdPartyId, TotalSalesPrice, Value,CostCenterId
			)
			SELECT 
				sod.Id, 
				sods.Id, 
				sods.IncomeMainAccountId, 
				rcd.ThirdPartyId,
				IIF(@Process = 1, sodd.GrandTotalSalesPrice + sodd.GrandTotalDiscount, sodd.ThirdPartySalesPrice), 
				IIF(@Process = 1, sodd.GrandTotalSalesPrice + sodd.GrandTotalDiscount, sodd.ThirdPartySalesPrice) * (sods.TotalSalesPrice / sods2.TotalSurgical),
				sods.CostCenterId
			FROM Billing.RevenueControlDetail rcd
			JOIN Billing.ServiceOrderDetailDistribution sodd ON sodd.RevenueControlDetailId = rcd.Id
			JOIN Billing.ServiceOrderDetail sod ON sodd.ServiceOrderDetailId = sod.Id
			JOIN Billing.ServiceOrderDetailSurgical sods ON sod.Id = sods.ServiceOrderDetailId
			JOIN
			(
				SELECT
					sods.ServiceOrderDetailId,
					SUM(sods.TotalSalesPrice) TotalSurgical
				FROM Billing.ServiceOrderDetailSurgical sods
				GROUP BY sods.ServiceOrderDetailId
			) sods2 ON sods.ServiceOrderDetailId = sods2.ServiceOrderDetailId
			WHERE rcd.Id = @RevenueControlDetailId 
				AND sod.RecordType = 1 
				AND sod.Presentation = 2 
				AND IIF(@Process = 1, sodd.GrandTotalSalesPrice + sodd.GrandTotalDiscount, sodd.ThirdPartySalesPrice) > 0 
				AND sod.IsDelete = 0

		UPDATE ds
			SET ds.Value = ds.Value + tds.AdjustmentValue
		FROM @TableDetailSurgical ds 
		JOIN
		(
			SELECT ds.ServiceOrderDetailId, MAX(ds.ServiceOrderDetailSurgicalId) ServiceOrderDetailSurgicalId, ds.TotalSalesPrice - SUM(ds.Value) AdjustmentValue
			FROM @TableDetailSurgical ds
			GROUP BY ds.ServiceOrderDetailId, ds.TotalSalesPrice
		) tds ON ds.ServiceOrderDetailSurgicalId = tds.ServiceOrderDetailSurgicalId

		INSERT INTO @JournalVourcherDetail 
		(
			IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue
		)
		SELECT
			ma.Id AS IdMainAccount, 
			IIF(ma.HandlesThirdParty = 1, tds.ThirdPartyId, NULL) AS IdThirdParty,
			IIF(ma.HandlesCostCenter = 1, tds.CostCenterId, NULL) AS IdCostCenter,		
			0 AS DebitValue,
			SUM(tds.Value) AS CreditValue
		FROM @TableDetailSurgical tds
		JOIN Billing.ServiceOrderDetail sod on tds.ServiceOrderDetailId = sod.Id
		JOIN Contract.CUPSEntity ce on sod.CUPSEntityId = ce.Id
		JOIN Billing.BillingConcept bc on ce.BillingConceptId = bc.Id
		JOIN GeneralLedger.MainAccounts ma on ma.Id = tds.MainAccountId
		GROUP BY ma.Id, ma.HandlesThirdParty, tds.ThirdPartyId, ma.HandlesCostCenter, tds.CostCenterId
	END
    ELSE
	BEGIN
		INSERT INTO @JournalVourcherDetail 
		(
			IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue
		)
		SELECT
			ma.Id AS IdMainAccount, 
			IIF(ma.HandlesThirdParty = 1, rcd.ThirdPartyId, NULL) AS IdThirdParty,
			IIF(ma.HandlesCostCenter = 1, sod.CostCenterId, NULL) AS IdCostCenter,		
			0 AS DebitValue,
			SUM(IIF(@Process = 1, temp.GrandTotalSalesPrice + temp.GrandTotalDiscount, temp.InvoiceThirdPartySalesValue)) AS CreditValue
		FROM Billing.RevenueControlDetail rcd WITH(NOLOCK)
		JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) on sodd.RevenueControlDetailId = rcd.Id
		JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sodd.ServiceOrderDetailId = sod.Id
		JOIN @RecalculateFolioDetails temp on sodd.Id=temp.SodDistributionId
		JOIN Contract.CUPSEntity ce WITH(NOLOCK) on sod.CUPSEntityId = ce.Id
		JOIN Billing.BillingConcept bc WITH(NOLOCK) on ce.BillingConceptId = bc.Id
		JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = sod.IncomeMainAccountId
		WHERE rcd.Id = @RevenueControlDetailId 			
			AND sod.RecordType = 1 
			AND sod.Presentation = 2 
			AND sod.IsDelete = 0
		GROUP BY ma.Id, ma.HandlesThirdParty, rcd.ThirdPartyId, ma.HandlesCostCenter, sod.CostCenterId
	END

    RETURN;
END;