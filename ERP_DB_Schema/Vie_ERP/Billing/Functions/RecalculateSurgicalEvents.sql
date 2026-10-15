CREATE Function [Billing].[RecalculateSurgicalEvents]
(
	@CupsEntityId Int,
	@IPSServiceId Int,
	@CareGroupId Int,
	@PerformsFunctionalUnitId Int,
	@PerformsProfessionalSpecialty Char(3),
	@ServiceDate DateTime,
	@AllowValueChange Bit,
	@LiquidationType Tinyint,
	@ServiceOrderDetailSurgicalXml Xml,
	@SurchargeApply Bit,
	@SubTotalSalesPrice Decimal(18, 2),
	@TotalSalesPrice Decimal(18, 2),
	@GrandTotalSalesPrice Decimal(18, 0),
	@InvoicedQuantity Int
)
Returns @ServiceOrderDetail Table
(
	StatusResult Bit,
	MessageResult Varchar(Max),
	AllowValueChange Bit,
	LiquidationType Tinyint,
	SubTotalSalesPrice Decimal(18, 2),
	TotalSalesPrice Decimal(18, 2),
	GrandTotalSalesPrice Decimal(18, 0),
	ServiceOrderDetailSurgicalXml Xml
) 
As
Begin
	
	Declare @RIPSConcept char(2),
			@rateVariation Decimal(18, 2) = 0,
			@rateManualRoundService Int

	Declare @_StateResult Bit,
		@_MessageResult Varchar(Max),
		@_ConditionType Tinyint,	
		@_LogicalOperator Tinyint,
		@_ConditionType2 Tinyint,
		@_DefinitionRateDetailId Int,
		@_LiquidateionType Tinyint,
		@_ManualType Tinyint,
		@_RateManualId Int,
		@_AllowValueChange Bit,
		@_RateVariation Decimal(5, 2),
		@_SalesValue Decimal(18, 2),
		@_SalesValueWithSurcharge Decimal(18, 2),
		@_DefinitionRateDetailConditionId Int,	
		@_LiquidateionTypeAux Tinyint,
		@_ManualTypeAux Tinyint,
		@_RateManualIdAux Int,
		@_RateVariationAux Decimal(5, 2),
		@_SalesValueAux Decimal(18, 2),
		@_SalesValueWithSurchargeAux Decimal(18, 2)

	SELECT @RIPSConcept = RIPSConcept
	FROM Contract.CUPSEntity
	WHERE Id = @CupsEntityId
	
	Select Top 1 @_StateResult = StateResult, @_MessageResult = MessageResult, @_ConditionType = ConditionType
		, @_LogicalOperator = LogicalOperator, @_ConditionType2 = ConditionType2, @_DefinitionRateDetailId = DefinitionRateDetailId
		, @_LiquidateionType = LiquidateionType, @_ManualType = ManualType, @_RateManualId = RateManualId
		, @_AllowValueChange = AllowValueChange, @_RateVariation = RateVariation, @_SalesValue = SalesValue
		, @_SalesValueWithSurcharge = SalesValueWithSurcharge, @_DefinitionRateDetailConditionId = DefinitionRateDetailConditionId
		, @_LiquidateionTypeAux = LiquidateionTypeAux, @_ManualTypeAux = ManualTypeAux, @_RateManualIdAux = RateManualIdAux
		, @_RateVariationAux = RateVariationAux, @_SalesValueAux = SalesValueAux, @_SalesValueWithSurchargeAux = SalesValueWithSurchargeAux
	From [Contract].GetRateValue(@CupsEntityId, @CareGroupId, @PerformsFunctionalUnitId, @PerformsProfessionalSpecialty, @ServiceDate, 1, 0, 1, 0) 

	If @_DefinitionRateDetailConditionId Is Not Null And @_DefinitionRateDetailConditionId > 0 Begin
		--Fue por que la tarifa se saco de una condicion
		--asigno la bandera para saber si en presentacion puedo cambiar el valor del servicio o no
		Set @AllowValueChange = @_AllowValueChange
		Set @LiquidationType = @_LiquidateionTypeAux
		If @_LiquidateionTypeAux = 2 Begin --Si es estandar
			Set @rateVariation = @_RateVariationAux

			Select @rateManualRoundService = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(RoundService, 1))
			From [Contract].RateManual With(Nolock) Where Id = @_RateManualIdAux
		End
	End
	Else Begin
		Set @AllowValueChange = @_AllowValueChange
		Set @LiquidationType = @_LiquidateionType
		If @_LiquidateionType = 2 Begin --Si es estandar
			Set @rateVariation = @_RateVariation
			
			Select @rateManualRoundService = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(RoundService, 1))
			From [Contract].RateManual With(Nolock) Where Id = @_RateManualId
		End
	End

	Declare @IpsUVRNumber Int,	
		@costValueItem Decimal(18, 2)
	Select @IpsUVRNumber = UVRNumber
	From [Contract].IPSService With(Nolock) Where Id = @IPSServiceId

	Declare @ServiceOrderDetailSurgical Table
	(
		RowId Int Primary Key Identity(1, 1),
		[Id] [int] NULL,
		ServiceOrderDetailId Int,
		[CodeNameIpsService] [varchar](320) Null,
		[IPSServiceId] [int] NOT NULL,
		[InvoicedQuantity] [int] NOT NULL,
		[LiquidationPercentage] [numeric](5, 2) NOT NULL,
		[RateManualSalePrice] [numeric](18, 0) NOT NULL,
		[TotalSalesPrice] [numeric](18, 0) NOT NULL,
		[PerformsHealthProfessionalCode] [char](20) NULL,
		[PerformsHealthProfessionalThirdPartyId] [int] NULL,
		[CostValue] [numeric](18, 2) NOT NULL,
		[BillingConceptId] [int] NOT NULL,
		[CostCenterId] [int] NOT NULL,
		[RateManualDetailSurgicalId] [int] NULL,
		[SurchargeApply] [bit] NOT NULL,
		[IncomeMainAccountId] [int] NOT NULL,
		ClassServiceIps Varchar(50),
		RoundService int
	)

	Insert Into @ServiceOrderDetailSurgical
		Select t.x.value('Id[1]', 'Int'),
			t.x.value('ServiceOrderDetailId[1]', 'Int'),
			t.x.value('CodeNameIpsService[1]', 'Varchar(320)'),
			t.x.value('IPSServiceId[1]', 'Int'),
			t.x.value('InvoicedQuantity[1]', 'Int'),
			t.x.value('LiquidationPercentage[1]', 'Numeric(5,2)'),
			t.x.value('RateManualSalePrice[1]', 'Numeric(18,0)'),
			t.x.value('TotalSalesPrice[1]', 'Numeric(18,0)'),
			t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)'),
			t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'Int'),
			t.x.value('CostValue[1]', 'Numeric(18,2)'),
			t.x.value('BillingConceptId[1]', 'Int'),
			t.x.value('CostCenterId[1]', 'Int'),
			t.x.value('RateManualDetailSurgicalId[1]', 'Int'),
			t.x.value('SurchargeApply[1]', 'Bit'),
			t.x.value('IncomeMainAccountId[1]', 'Int'),
			t.x.value('ClassServiceIps[1]', 'Varchar(50)'),
			t.x.value('RoundService[1]', 'int')
		From @ServiceOrderDetailSurgicalXml.nodes('ServiceOrderDetailSurgicalXml') t(x)

	Declare @Rows Int, @RowId Int
	Set @Rows = 1
	Set @RowId = 1

	Declare @RateManualDetailSurgicalId Int,
		@SurgicalInvoicedQuantity Int,
		@SurgicalIPSServiceId Int,
		@SurgicalClassServiceIps Varchar(50)

	While @Rows > 0
	begin
		Select Top 1 @RowId = RowId 
			, @RateManualDetailSurgicalId = RateManualDetailSurgicalId
			, @SurgicalInvoicedQuantity = InvoicedQuantity
			, @SurgicalIPSServiceId = IPSServiceId
			, @SurgicalClassServiceIps = ClassServiceIps
		From @ServiceOrderDetailSurgical Where RowId >= @RowId Order By RowId
		Set @Rows = @@ROWCOUNT
		If @Rows = 0 
			Break
		Declare @totalSalesPriceItem Decimal(18, 2)

		If @RateManualDetailSurgicalId Is Not Null And @RateManualDetailSurgicalId > 0 Begin
			Declare @SalesValueWithSurcharge Decimal(18, 2),
				@SalesValue Decimal(18, 2),
				@SurgicalRoundService Int

			Select @SalesValueWithSurcharge = rmds.SalesValueWithSurcharge,
				@SalesValue = rmds.SalesValue,
				@SurgicalRoundService = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(rm.RoundService, 1))
			From [Contract].RateManualDetailSurgical rmds With(Nolock) 
			Inner Join [Contract].RateManual rm With(Nolock) On rmds.RateManualId = rm.Id
			Where rmds.Id = @RateManualDetailSurgicalId

			If @SurchargeApply = 1
				Set @costValueItem = @SalesValueWithSurcharge * @SurgicalInvoicedQuantity
			Else
				Set @costValueItem = @SalesValue * @SurgicalInvoicedQuantity

			Set @totalSalesPriceItem = @costValueItem + (@costValueItem * (@rateVariation / 100))
			Set @totalSalesPriceItem = [Billing].RoundValue(@totalSalesPriceItem, @SurgicalRoundService)

			Update @ServiceOrderDetailSurgical 
				Set RateManualSalePrice = @totalSalesPriceItem, 
					TotalSalesPrice = @totalSalesPriceItem,
					RoundService = @SurgicalRoundService
			Where RowId = @RowId
		End
		Else Begin
			Declare @NewScore Decimal(18, 2),
				@Score Decimal(18, 2),
				@ApplyChageScore Bit
			
			Select @NewScore = NewScore
				, @Score = Score
				, @ApplyChageScore = ApplyChangeScore
			From [Contract].IPSService With(Nolock) Where Id = @SurgicalIPSServiceId

			If @SurgicalClassServiceIps = 'Derecho Sala'
				Set @costValueItem = @IpsUVRNumber * @NewScore
			Else Begin
				If @IpsUVRNumber > 450 And @ApplyChageScore = 1
					Set @costValueItem = @IpsUVRNumber * @NewScore
				Else
					Set @costValueItem = @IpsUVRNumber * @Score
			End

			Set @totalSalesPriceItem = @costValueItem + (@costValueItem * (@rateVariation / 100))
			Set @totalSalesPriceItem = [Billing].RoundValue(@totalSalesPriceItem, @rateManualRoundService)

			Update @ServiceOrderDetailSurgical 
				Set RateManualSalePrice = @totalSalesPriceItem, 
					TotalSalesPrice = @totalSalesPriceItem,
					RoundService = @rateManualRoundService
			Where RowId = @RowId
		End

		Update @ServiceOrderDetailSurgical Set SurchargeApply = 1 Where RowId = @RowId

		Set @RowId += 1
	End

	Set @SubTotalSalesPrice = (Select Sum(TotalSalesPrice) From @ServiceOrderDetailSurgical)
	Set @TotalSalesPrice = @SubTotalSalesPrice
	Set @GrandTotalSalesPrice = @TotalSalesPrice * @InvoicedQuantity

	Declare @SurgicalXml Xml = (
		Select *
		From @ServiceOrderDetailSurgical
		For Xml Path('ServiceOrderDetailSurgicalXml'), Elements
	)
	Insert Into @ServiceOrderDetail
	Values (1, '', @AllowValueChange, @LiquidationType, @SubTotalSalesPrice, @TotalSalesPrice, @GrandTotalSalesPrice, @SurgicalXml)

	Return
End