CREATE Function [Billing].[GetProductRateDetail]
(
	@CareGroupId Int,
	@ProductId Int,
	@ServiceDate DateTime
)
Returns @ProductRateDetail Table
(
	StatusResult Bit,
	MessageResult Varchar(Max),
	
	Id Int,
	ProductRateId Int,
	ProductId Int,
	InitialDate DateTime,
	EndDate DateTime,
	SalesValue Decimal(18, 2),
	SalesValueWithSurcharge Decimal(18, 2),
	GrossValue Numeric(20,2),
	TaxValue numeric(20,2)
) 
As
Begin	
	Declare @Id Int,
		@ProductRateId Int,
		@ProductIdx Int,
		@InitialDate DateTime,
		@EndDate DateTime,
		@SalesValue Decimal(18, 2),
		@SalesValueWithSurcharge Decimal(18, 2),
		@SalePriceIncludeTax bit,
		@TaxValue numeric(20,2),
		@GrossValue numeric(20,2),
		@TaxPercentage numeric(20,2),
		@RateType Tinyint,
		@RatePercentage numeric(20,2),
		@ProductCost numeric(20,2),
		@FinalProductCost numeric(20,2),
		@PercentageBasedOn Tinyint

		Select top 1 @SalePriceIncludeTax = cs.SalePriceIncludeTax from GeneralLedger.CompanySettings cs WITH(NOLOCK)

		Select Top 1 @Id = prd.Id, @ProductRateId = prd.ProductRateId, @ProductIdx = ProductId, @InitialDate = InitialDate
			, @EndDate = EndDate, @SalesValue = SalesValue, @SalesValueWithSurcharge = SalesValueWithSurcharge,
			@TaxPercentage = iif(ip.TaxedProduct=1,isnull(giva.Percentage,0),0), @RateType=prd.RateType,
			@ProductCost=ip.ProductCost, @FinalProductCost=ip.FinalProductCost, @PercentageBasedOn=prd.PercentageBasedOn,
			@RatePercentage= prd.Percentage
		From [Contract].CareGroup cg With(Nolock)
		Inner Join Inventory.ProductRate pr With(Nolock) On cg.ProductRateId = pr.Id
		Inner Join Inventory.ProductRateDetail prd With(Nolock) On prd.ProductRateId = pr.Id
		inner join Inventory.InventoryProduct ip WITH(NOLOCK) on ip.Id = prd.ProductId
		LEFT join GeneralLedger.GeneralLedgerIVA giva WITH(NOLOCK) on ip.IVAId=giva.Id
		Where cg.Id = @CareGroupId And prd.ProductId = @ProductId And (prd.InitialDate <= @ServiceDate And prd.EndDate >= @ServiceDate)
		Order By prd.Id
	
	If @Id Is Null Begin
		Declare @ProductCodeName Varchar(320) = (Select Concat(Code, ' - ', [Name]) From Inventory.InventoryProduct With(Nolock) Where Id = @ProductId)
		Declare @CareGroupCodeName Varchar(320) = (Select Concat(Code, ' - ', [Name]) From [Contract].CareGroup With(Nolock) Where Id = @CareGroupId)

		Insert Into @ProductRateDetail
		Values (0, 'El producto ' + @ProductCodeName + ' no se encuentra dentro de la tarifa para el grupo de atención ' + @CareGroupCodeName + ' y fecha ' + Cast(@ServiceDate As Varchar)
			, 0,0,0,GetDate(), GetDate(), 0, 0,0,0)
	End
	Else Begin

		IF @RateType =2 BEGIN
			
			SET @SalesValue = (CASE @PercentageBasedOn
								WHEN 1 THEN ROUND( @ProductCost * (@RatePercentage/100) + @ProductCost,2)
								WHEN 2 THEN ROUND( @FinalProductCost * (@RatePercentage/100) + @FinalProductCost,2)
								ELSE @SalesValue
								END)

		END
		
		SELECT	@GrossValue = GrossValue,
				@TaxValue = TaxValue,
				@SalesValue = SalesPrice
		from billing.SetValueSalesPrice(@SalePriceIncludeTax,@SalesValue,@TaxPercentage)

		Insert Into @ProductRateDetail
		Values (1, '', @Id, @ProductRateId, @ProductIdx, @InitialDate, @EndDate, @SalesValue, @SalesValueWithSurcharge,@GrossValue,@TaxValue)
	End

	Return
End