
CREATE Function [Billing].[RecalculateFolioDetailsByCurrency]
(
	@revenueControlDetailId INTEGER,
	@currencyId				INTEGER,
	@dateTRM				DATE,
	@OperativeUnitId		INTEGER
)
Returns @RecalculateFolioDetails Table
(
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
	IvaId						INTEGER,
	InvoiceThirdPartySalesValue NUMERIC(20,2),
	SubTotalPatientSalesPrice	NUMERIC(20,2),
	CurrencyId					INTEGER,
	CurrencyAbbreviation		Varchar(4),
	CurrencyName				Varchar(20)
) 
As
Begin	
	Declare @OfficialCurrencyId  integer,
			@LiquidateMasterAccount BIT
	/***********Tabla variable para recalcular los valores segun moneda********************/
	DECLARE @TempFolioDetailConverted as TABLE(	RevenueControlDetailId		INTEGER NOT NULL,
												SodDistributionId			INTEGER NOT NULL,
												SubTotalSalesPrice			NUMERIC(20,2) ,
												GrandTotalDiscount			NUMERIC(20,2) ,
												TaxPercentage				NUMERIC(20,2),
												IvaId						INTEGER,
												SubTotalPatientSalesPrice	NUMERIC(20,2),
												GrandTotalTaxes				NUMERIC(20,2),
												CurrencyId					INTEGER)
	
		If @revenueControlDetailId is null begin
			insert into @RecalculateFolioDetails (	StatusResult,
													MessageResult)
			values (0, 'Id del folio vacío')
		end

		set @OfficialCurrencyId = (SELECT top 1 OfficialCurrencyId from GeneralLedger.CompanySettings WITH(NOLOCK))
		SET @LiquidateMasterAccount =(	SELECT top 1 ISNULL(LiquidateMasterAccount,0) 
										from Billing.SettingsBilling WITH(NOLOCK) 
										where @OperativeUnitId is null OR IdOperatingUnit=@OperativeUnitId
										order by LiquidateMasterAccount DESC);


		WITH cte_ValuesCol as (	SELECT (ssp.GrossValue + sodd.GrandTotalDiscount) as GrossValue,
										ssp.GrossValue as NetWorth,
										ssp.TaxValue,
										ssp.SalesPrice as GrandTotalSalesPrice,
										sodd.Id ServicerOrderDId
								FROM Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK)
								JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sodd.ServiceOrderDetailId=sod.Id
								LEFT JOIN GeneralLedger.GeneralLedgerIVA gli WITH(NOLOCK) on sod.IvaId=gli.Id
								CROSS APPLY Billing.SetValueSalesPrice(1,sodd.GrandTotalSalesPrice,ISNULL(gli.Percentage,0)) ssp
								where sodd.RevenueControlDetailId = @revenueControlDetailId and @LiquidateMasterAccount=0
								)

		INSERT INTO @TempFolioDetailConverted
		SELECT	rcd.Id as RevenueControlDetailId,
				sodd.Id as SodDistributionId,
				ROUND([Common].[CurrencyConverterByModule](ISNULL(cte.GrossValue,sodd.SubTotalSalesPrice),@OfficialCurrencyId, ISNULL(@CurrencyId,@OfficialCurrencyId),@OperativeUnitId,'Invoice',@dateTRM),2),
				ROUND([Common].[CurrencyConverterByModule](sodd.GrandTotalDiscount,@OfficialCurrencyId, ISNULL(@CurrencyId,@OfficialCurrencyId),@OperativeUnitId,'Invoice',@dateTRM),2),				
				IIF(ISNULL(cte.TaxValue,sodd.GrandTotalTaxes)>0,ISNULL(gli.Percentage,0),0) as TaxPercentage,
				IIF(ISNULL(cte.TaxValue,sodd.GrandTotalTaxes)>0,gli.Id,NULL) as IvaId,
				[Common].[CurrencyConverterByModule](iif(@LiquidateMasterAccount = 1,0,sodd.SubTotalPatientSalesPrice),@OfficialCurrencyId, ISNULL(@CurrencyId,@OfficialCurrencyId),@OperativeUnitId,'Invoice',@dateTRM),
				ROUND([Common].[CurrencyConverterByModule](ISNULL(cte.TaxValue,sodd.GrandTotalTaxes),@OfficialCurrencyId, ISNULL(@CurrencyId,@OfficialCurrencyId),@OperativeUnitId,'Invoice',@dateTRM),2),
				ISNULL(@CurrencyId,@OfficialCurrencyId)
		FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
		JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sod.Id = sodd.ServiceOrderDetailId
		JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) on sodd.RevenueControlDetailId=rcd.Id
		LEFT JOIN GeneralLedger.GeneralLedgerIVA gli with(NOLOCK) on sod.IvaId = gli.Id
		LEFT JOIN cte_ValuesCol cte on cte.ServicerOrderDId=sodd.Id
		WHERE RevenueControlDetailId = @revenueControlDetailId AND sod.IsDelete = 0


		INSERT INTO @RecalculateFolioDetails
		SELECT	iif(temp.SubTotalSalesPrice is null,0,1),
				iif(temp.SubTotalSalesPrice is null,'Valores no recalculados','Valores recalculados'),
				temp.RevenueControlDetailId,
				temp.SodDistributionId,
				temp.SubTotalSalesPrice,
				temp.GrandTotalDiscount,
				ROUND(temp.SubTotalSalesPrice/sodd.Quantity,2) AS UnitGrossValue,
				temp.TaxPercentage,
				(temp.SubTotalSalesPrice - temp.GrandTotalDiscount) AS NetWorth,
				ROUND((temp.SubTotalSalesPrice - temp.GrandTotalDiscount)/sodd.Quantity,2) AS NetUnitValue,
				-------------------------------------------------------------------------------------
				temp.GrandTotalTaxes AS GrandTotalTaxes,
				-------------------------------------------------------------------------------------
				(temp.SubTotalSalesPrice - temp.GrandTotalDiscount) + temp.GrandTotalTaxes AS GrandTotalSalesPrice,
				-------------------------------------------------------------------------------------
				((temp.SubTotalSalesPrice - temp.GrandTotalDiscount) + temp.GrandTotalTaxes) / sodd.Quantity AS TotalSalesPrice,
				-------------------------------------------------------------------------------------
				sodd.Quantity AS InvoicedQuantity,
				temp.IvaId,
				-------------------------------------------------------------------------------------
				(temp.SubTotalSalesPrice - temp.GrandTotalDiscount) + temp.GrandTotalTaxes - temp.SubTotalPatientSalesPrice AS InvoiceThirdPartySalesValue,
				-------------------------------------------------------------------------------------
				temp.SubTotalPatientSalesPrice,
				temp.CurrencyId,
				iso.CodeAbbreviation,
				iso.CurrencyName
		FROM @TempFolioDetailConverted temp
		INNER JOIN Billing.ServiceOrderDetailDistribution sodd with(NOLOCK) on  sodd.Id=temp.SodDistributionId
		INNER JOIN Common.Currency c with(NOLOCK) on temp.CurrencyId = c.Id
		INNER JOIN Common.ISO4217 iso WITH(NOLOCK) on c.ISO4217Id=iso.Id
		Return

End