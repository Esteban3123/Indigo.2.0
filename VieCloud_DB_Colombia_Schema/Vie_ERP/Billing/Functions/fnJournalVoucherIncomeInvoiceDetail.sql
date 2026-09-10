CREATE FUNCTION [Billing].[fnJournalVoucherIncomeInvoiceDetail]
	(
		@RevenueControlDetailId AS INT,
		@AccountingForSurgical AS TINYINT,
		@Process AS TINYINT, --(1. Liquidacion, 2. Distribucion Ingresos)
		@InvoiceDate DATE = NULL
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

	SELECT top 1 @CurrencyId =isnull( i.CurrencyId,cs.OfficialCurrencyId),
				@OperativeUnitId = I.OperatingUnitId,
				@LiquidateMasterAccount = isnull(sb.LiquidateMasterAccount,0)
	from Billing.Invoice i WITH(NOLOCK) 
	JOIN GeneralLedger.CompanySettings cs WITH(NOLOCK) on 1=1
	LEFT JOIN Billing.SettingsBilling sb WITH(NOLOCK) ON i.OperatingUnitId = sb.IdOperatingUnit
	where i.RevenueControlDetailId =@RevenueControlDetailId 

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que calcula el detalle contable (asientos de comprobante de diario) para el reconocimiento de ingresos de una factura de venta, dado un detalle de control de recaudo específico. Determina las cuentas contables de ingreso y de impuestos (IVA), los terceros y los centros de costo correspondientes a cada línea de servicio o medicamento facturado, generando los valores en crédito que se registrarán en contabilidad. Internamente convierte los valores del folio a la moneda oficial de la compañía invocando RecalculateFolioDetailsByCurrency, y aplica la configuración contable del módulo de facturación (SettingsBilling) para determinar si se liquida contra cuenta maestra. Soporta dos procesos: liquidación de la factura y distribución de ingresos entre unidades, y es utilizada en la generación automática de comprobantes contables de ingresos por servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'fnJournalVoucherIncomeInvoiceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'fnJournalVoucherIncomeInvoiceDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye el detalle CRÉDITO del comprobante contable de ingresos por una factura/folio, agregando ingresos e IVA por cuenta contable, tercero y centro de costo, con tratamiento especial para servicios quirúrgicos.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnJournalVoucherIncomeInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El RevenueControlDetailId debe existir y estar asociado a una Billing.Invoice para obtener moneda y unidad operativa; La función Billing.RecalculateFolioDetailsByCurrency debe ejecutarse exitosamente (StatusResult=1) para todos los detalles; si algún StatusResult=0 se aborta y se retorna tabla vacía; @Process debe indicar 1=Liquidación o 2=Distribución de ingresos para seleccionar la base de cálculo; @AccountingForSurgical define si los servicios con Presentation=2 (quirúrgicos) se prorratean a nivel de detalle quirúrgico (=2) o se contabilizan agregados (otro valor)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnJournalVoucherIncomeInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todos los renglones generados son créditos (DebitValue siempre 0); Los detalles eliminados (IsDelete=1) nunca se contabilizan; Los servicios con Presentation=2 (quirúrgicos) se procesan en una rama separada y nunca en la rama general de servicios; El IVA sólo se contabiliza en proceso de Liquidación (@Process=1) y cuando DistributionType<>2; La moneda y unidad operativa se toman de la factura asociada al RevenueControlDetailId; si la factura no tiene CurrencyId se usa CompanySettings.OfficialCurrencyId; El prorrateo quirúrgico garantiza que la suma de Value iguale el TotalSalesPrice original mediante un ajuste de residuo en el ServiceOrderDetailSurgical de mayor Id; Las filas se agrupan por cuenta principal, tercero y centro de costo (cuando aplican)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnJournalVoucherIncomeInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @JournalVourcherDetail: Por cada cuenta de ingreso (sod.IncomeMainAccountId) de servicios con RecordType=1 y Presentation<>2 e IsDelete=0: CreditValue = SUM, en @Process=1 con DistributionType=2 usa GrandTotalSalesPrice+GrandTotalDiscount, sino SubTotalSalesPrice; en @Process=2 usa InvoiceThirdPartySalesValue.; [INSERT] @JournalVourcherDetail: IVA de servicios (cuenta iva.IdAccountSale) sólo cuando @Process=1 y DistributionType<>2: CreditValue = ROUND(SUM(GrandTotalTaxes),2); en otros casos 0.; [INSERT] @JournalVourcherDetail: Por cuenta de ingreso de productos (RecordType=2, IsDelete=0): misma lógica de CreditValue que servicios según @Process y DistributionType.; [INSERT] @JournalVourcherDetail: IVA de productos (iva.IdAccountSale) cuando @Process=1 y DistributionType<>2: ROUND(SUM(GrandTotalTaxes),2).; [INSERT] @JournalVourcherDetail: Si @AccountingForSurgical=2: para servicios con RecordType=1, Presentation=2 e IsDelete=0, prorratea el valor (GrandTotalSalesPrice+GrandTotalDiscount si @Process=1, sino ThirdPartySalesPrice) entre los ServiceOrderDetailSurgical proporcionalmente a sods.TotalSalesPrice/TotalSurgical, ajustando residuo en el último, y registra CreditValue por sods.IncomeMainAccountId.; [INSERT] @JournalVourcherDetail: Si @AccountingForSurgical<>2: para servicios con RecordType=1, Presentation=2 e IsDelete=0, registra CreditValue = SUM(GrandTotalSalesPrice+GrandTotalDiscount) si @Process=1, o SUM(InvoiceThirdPartySalesValue) si @Process=2, contra sod.IncomeMainAccountId.; [RETURN_RESULT] @JournalVourcherDetail: Cada fila lleva DebitValue=0 (sólo créditos); IdThirdParty se llena sólo si MainAccounts.HandlesThirdParty=1; IdCostCenter sólo si MainAccounts.HandlesCostCenter=1.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnJournalVoucherIncomeInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EXISTS en @RecalculateFolioDetails con StatusResult=0 → RETURN inmediato sin generar detalle contable else Continúa con la generación de asientos; si @Process = 1 (Liquidación) vs distinto (Distribución de ingresos) → Usa GrandTotalSalesPrice+GrandTotalDiscount o SubTotalSalesPrice según DistributionType, e incluye IVA else Usa InvoiceThirdPartySalesValue y NO contabiliza IVA (queda en 0); si sodd.DistributionType = 2 → Para ingreso usa GrandTotalSalesPrice+GrandTotalDiscount y el IVA se anula (0) else Usa SubTotalSalesPrice como ingreso e incluye GrandTotalTaxes como IVA; si @AccountingForSurgical = 2 → Genera asientos por cada ServiceOrderDetailSurgical prorrateando con TotalSalesPrice/TotalSurgical y ajustando diferencias en el último registro else Genera un único asiento agregado por sod.IncomeMainAccountId para servicios quirúrgicos (Presentation=2); si ma.HandlesThirdParty = 1 → Asigna rcd.ThirdPartyId al asiento else IdThirdParty = NULL; si ma.HandlesCostCenter = 1 → Asigna sod.CostCenterId (o sods.CostCenterId en quirúrgicos) al asiento else IdCostCenter = NULL', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnJournalVoucherIncomeInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.RecalculateFolioDetailsByCurrency', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnJournalVoucherIncomeInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnJournalVoucherIncomeInvoiceDetail';
GO
