CREATE Function [Contract].[GetServiceValueBySurgicalProcedureService]
(
	@ServiceOrderDetailXml Xml,
	@ServiceOrderDetailSurgicalXml Xml Null,
	@listSurgicalProcedureServiceDefaultXml Xml Null
)
Returns @ServiceOrderDetail Table
(
	StatusResult Bit,
	MessageResult Varchar(Max),
	Id Int Primary Key,
	CostCenterId Int,
	ServiceType Tinyint,
	CodeNameIpsService Varchar(320),
	CodeNameCups Varchar(320),
	CodeNameFunctionalUnit Varchar(320),
	CodeNameCostCenter Varchar(100),
	AllowValueChange Bit,
	DefinitionRateDetailId Int Null,
	DefinitionRateDetailConditionId Int Null,
	LiquidationType Tinyint,
	Presentation Tinyint Null,
	IsSOAT Bit,
	RateManualType Tinyint Null,
	RateManualId Int Null,
	SubTotalSalesPrice Decimal(20, 2),
	RateManualSalePrice Decimal(20, 2),
	TotalSalesPrice Decimal(20, 2),
	PerformsHealthProfessionalThirdPartyId Int Null,
	GrandTotalSalesPrice Decimal(20, 2),
	CostValue Decimal(18, 2),
	Recordtype Tinyint,
	CareGroupId Int,
	CUPSEntityId Int Null,
	IPSServiceId Int Null,
	InvoicedQuantity Int,
	ServiceDate DateTime,
	PerformsFunctionalUnitId Int,
	PerformsHealthProfessionalCode Char(20),
	PerformsProfessionalSpecialty Char(3),
	BillingConceptId Int Null,
	SettlementType Tinyint,
	RateManualDetailId Int Null,
	ServiceOrderDetailSurgicalXml Xml Null,
	IncomeMainAccountId Int Null,
	SurgicalInterventionType Tinyint Null,
	SurchargeApply Bit,
	RoundService int,
	GrossValue numeric(20,2),
	TaxValue numeric(20,2),
	IvaId int
) 
As
Begin

	Insert Into @ServiceOrderDetail
	Select t.x.value('StatusResult[1]', 'Bit'),
		t.x.value('MessageResult[1]', 'Varchar(Max)'),
		t.x.value('Id[1]', 'Int'),
		t.x.value('CostCenterId[1]', 'Int'),
		t.x.value('ServiceType[1]', 'Tinyint'),
		t.x.value('CodeNameIpsService[1]', 'Varchar(320)'),
		t.x.value('CodeNameCups[1]', 'Varchar(320)'),
		t.x.value('CodeNameFunctionalUnit[1]', 'Varchar(320)'),
		t.x.value('CodeNameCostCenter[1]', 'Varchar(100)'),
		t.x.value('AllowValueChange[1]', 'Bit'),
		t.x.value('DefinitionRateDetailId[1]', 'Int'),
		t.x.value('DefinitionRateDetailConditionId[1]', 'Int'),
		t.x.value('LiquidationType[1]', 'Tinyint'),
		t.x.value('Presentation[1]', 'Tinyint'),
		t.x.value('IsSOAT[1]', 'Bit'),
		t.x.value('RateManualType[1]', 'Tinyint'),
		t.x.value('RateManualId[1]', 'Int'),
		t.x.value('SubTotalSalesPrice[1]', 'Decimal(20, 2)'),
		t.x.value('RateManualSalePrice[1]', 'Decimal(20, 2)'),
		t.x.value('TotalSalesPrice[1]', 'Decimal(20, 2)'),
		t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'Int'),
		t.x.value('GrandTotalSalesPrice[1]', 'Decimal(20, 2)'),
		t.x.value('CostValue[1]', 'Decimal(18, 2)'),
		t.x.value('Recordtype[1]', 'Tinyint'),
		t.x.value('CareGroupId[1]', 'Int'),
		t.x.value('CUPSEntityId[1]', 'Int'),
		t.x.value('IPSServiceId[1]', 'Int'),
		t.x.value('InvoicedQuantity[1]', 'Int'),
		t.x.value('ServiceDate[1]', 'DateTime'),
		t.x.value('PerformsFunctionalUnitId[1]', 'Int'),
		t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)'),
		t.x.value('PerformsProfessionalSpecialty[1]', 'Char(3)'),
		t.x.value('BillingConceptId[1]', 'Int'),
		t.x.value('SettlementType[1]', 'Tinyint'),
		t.x.value('RateManualDetailId[1]', 'Int'),
		Null,
		t.x.value('IncomeMainAccountId[1]', 'Int'),
		t.x.value('SurgicalInterventionType[1]', 'Tinyint'),
		t.x.value('SurchargeApply[1]', 'Bit'),
		t.x.value('RoundService[1]', 'Int'),
		t.x.value('GrossValue[1]', 'NUMERIC(20,2)'),
		t.x.value('TaxValue[1]', 'NUMERIC(20,2)'),
		t.x.value('IvaId[1]', 'Int')
	From @ServiceOrderDetailXml.nodes('/ServiceOrderDetail') t(x)
	

	Declare @CUPSEntityId Int, @CareGroupId Int, @PerformsFunctionalUnitId Int, @RIPSConcept char(2) = ''
		, @PerformsProfessionalSpecialty Char(3), @ServiceDate DateTime, @IPSServiceId Int
		, @SurgicalInterventionType Tinyint, @SurchargeApply Bit, @CostValueSod Decimal(18, 2)
		, @CostCenterSOD Int, @PerformsHealthProfessionalCodeSOD Char(20), @PerformsHealthProfessionalThirdPartyIdSOD Int		

	Select @CUPSEntityId = CUPSEntityId, @CareGroupId = CareGroupId, @PerformsFunctionalUnitId = PerformsFunctionalUnitId
		, @PerformsProfessionalSpecialty = PerformsProfessionalSpecialty, @ServiceDate = ServiceDate, @IPSServiceId = IPSServiceId
		, @SurgicalInterventionType = SurgicalInterventionType, @SurchargeApply = SurchargeApply, @CostValueSod = CostValue
		, @CostCenterSOD = CostCenterId, @PerformsHealthProfessionalCodeSOD = PerformsHealthProfessionalCode
		, @PerformsHealthProfessionalThirdPartyIdSOD = PerformsHealthProfessionalThirdPartyId
	From @ServiceOrderDetail

	SELECT @RIPSConcept = RIPSConcept
	FROM Contract.CUPSEntity
	WHERE Id = @CUPSEntityId
	
	Declare @serviceOrderDetailSurgical Table(RowId Int,
		CodeNameIpsService Varchar(320),
		IPSServiceId Int,
		InvoicedQuantity Int,
		LiquidationPercentage Decimal(5, 2),
		TotalSalesPrice Decimal(20, 2),
		ClassServiceIps Varchar(30),
		RateManualSalePrice Decimal(20, 2),
		PerformsHealthProfessionalCode Char(20) Null,
		PerformsHealthProfessionalThirdPartyId Int Null,
		CostValue Decimal(18, 2),
		BillingConceptId Int,
		CostCenterId Int,
		RateManualDetailSurgicalId Int Null,
		SurchargeApply Bit,
		IncomeMainAccountId Int Null,
		RoundService int)

	Insert Into @serviceOrderDetailSurgical
	Select t.x.value('RowId[1]', 'Int'),
		t.x.value('CodeNameIpsService[1]', 'Varchar(320)'),
		t.x.value('IPSServiceId[1]', 'Int'),
		t.x.value('InvoicedQuantity[1]', 'Int'),
		t.x.value('LiquidationPercentage[1]', 'Decimal(5, 2)'),
		t.x.value('TotalSalesPrice[1]', 'Decimal(20, 2)'),
		t.x.value('ClassServiceIps[1]', 'Varchar(30)'),
		t.x.value('RateManualSalePrice[1]', 'Decimal(20, 2)'),
		t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)'),
		t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'Int'),
		t.x.value('CostValue[1]', 'Decimal(18, 2)'),
		t.x.value('BillingConceptId[1]', 'Int'),
		t.x.value('CostCenterId[1]', 'Int'),
		t.x.value('RateManualDetailSurgicalId[1]', 'Int'),
		t.x.value('SurchargeApply[1]', 'Bit'),
		t.x.value('IncomeMainAccountId[1]', 'Int'),
		t.x.value('RoundService[1]', 'Int')
	From @ServiceOrderDetailSurgicalXml.nodes('/ServiceOrderDetailSurgical') t(x)

	Declare @listSurgicalProcedureServiceDefault Table(
		RowId Int Primary Key
		, IPSServiceId Int
		, ServiceAmount Int
		, ClassService Varchar(30)
		, ValueItemServiceOrderDetail Decimal(18, 2)
		, PerformsHealthProfessionalCode Char(20))

	Insert Into @listSurgicalProcedureServiceDefault
	Select t.x.value('RowId[1]', 'Int'),
		t.x.value('IPSServiceId[1]', 'Int'),
		t.x.value('ServiceAmount[1]', 'Int'),
		t.x.value('ClassService[1]', 'Varchar(30)'),		
		t.x.value('ValueItemServiceOrderDetail[1]', 'Decimal(18, 2)'),		
		t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)')
	From @listSurgicalProcedureServiceDefaultXml.nodes('/ListSurgicalDefault') t(x)

	Declare @rateVariation Decimal(18, 2) = 0,
		@rateManualValidityId Int,
		@rateManualValidityCodeName VARCHAR(500),
		@rateManualId Int,
		@rateManualType Tinyint,
		@rateManualRoundService Int

	Declare @StateResultx Bit,
	@MessageResultx Varchar(Max),
	@ConditionTypex Tinyint,	
	@LogicalOperatorx Tinyint,
	@ConditionType2x Tinyint,
	@DefinitionRateDetailIdx Int,
	@LiquidateionTypex Tinyint,
	@ManualTypex Tinyint,
	@RateManualValidityIdx Int,
	@RateManualIdx Int,
	@AllowValueChangex Bit,
	@RateVariationx Decimal(5, 2),
	@SalesValuex Decimal(18, 2),
	@SalesValueWithSurchargex Decimal(18, 2),
	@DefinitionRateDetailConditionIdx Int,	
	@LiquidateionTypeAuxx Tinyint,
	@ManualTypeAuxx Tinyint,
	@RateManualValidityIdAuxx Int,
	@RateManualIdAuxx Int,
	@RateVariationAuxx Decimal(5, 2),
	@SalesValueAuxx Decimal(18, 2),
	@SalesValueWithSurchargeAuxx Decimal(18, 2)
	
	Select @StateResultx = StateResult
		, @MessageResultx = MessageResult
		, @ConditionTypex = ConditionType
		, @LogicalOperatorx = LogicalOperator
		, @ConditionType2x = ConditionType2
		, @DefinitionRateDetailIdx = DefinitionRateDetailId
		, @LiquidateionTypex = LiquidateionType
		, @ManualTypex = ManualType
		, @RateManualValidityIdx = RateManualValidityId
		, @RateManualIdx = RateManualId
		, @AllowValueChangex = AllowValueChange
		, @RateVariationx = RateVariation
		, @SalesValuex = SalesValue
		, @SalesValueWithSurchargex = SalesValueWithSurcharge
		, @DefinitionRateDetailConditionIdx = DefinitionRateDetailConditionId
		, @LiquidateionTypeAuxx = LiquidateionTypeAux
		, @ManualTypeAuxx = ManualTypeAux
		, @RateManualValidityIdAuxx = RateManualValidityIdAux
		, @RateManualIdAuxx = RateManualIdAux
		, @RateVariationAuxx = RateVariationAux
		, @SalesValueAuxx = SalesValueAux
		, @SalesValueWithSurchargeAuxx = SalesValueWithSurchargeAux
	From [Contract].GetRateValue(@CUPSEntityId, @CareGroupId, @PerformsFunctionalUnitId, @PerformsProfessionalSpecialty, @ServiceDate, 0, 0, 0,@IPSServiceId)

	If @DefinitionRateDetailConditionIdx Is Not Null And @DefinitionRateDetailConditionIdx > 0 Begin --Fue por que la tarifa se saco de una condicion
		If @LiquidateionTypeAuxx = 2 Begin --Si es estandar
			Set @rateVariation = @RateVariationAuxx
			Select @rateManualId = Id, @rateManualType = [Type], @rateManualRoundService = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(RoundService, 1))
			From [Contract].RateManual With(Nolock)
			Where Id = @RateManualIdAuxx
		END
		ELSE If @LiquidateionTypeAuxx = 3 Begin --Si es por vigencia
			Set @rateVariation = @RateVariationAuxx
			Select @rateManualId = rm.Id, @ratemanualType = rm.[Type]
				, @rateManualRoundService = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(rm.RoundService, 1))
				, @rateManualValidityCodeName = Concat(rmv.Code, ' - ', rmv.[Name])
			From [Contract].RateManualValidity rmv With(Nolock) 
			LEFT JOIN Contract.RateManualValidityDetail rmvd WITH (NOLOCK) ON rmv.Id = rmvd.RateManualValidityId AND @ServiceDate BETWEEN rmvd.InitialDate AND rmvd.EndDate
			LEFT JOIN Contract.RateManual rm WITH (NOLOCK) ON rmvd.RateManualId = rm.Id
			Where rmv.Id = @RateManualValidityIdAuxx

			If @rateManualId IS NULL Begin					
				Update @ServiceOrderDetail Set StatusResult = 0, MessageResult = 'El servicio IPS ' + Concat(CodeNameIpsService, ' no tiene parametrizado un manual de tarifas para la vigencia ', @rateManualValidityCodeName, ' para la fecha del servicio ', @ServiceDate)
				Return
			End
		End
	End
	Else Begin
		If @LiquidateionTypex = 2 begin --Si es estandar
			Set @rateVariation = @RateVariationx
			Select @rateManualId = Id, @rateManualType = [Type], @rateManualRoundService = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(RoundService, 1))
			From [Contract].RateManual With(Nolock)
			Where Id = @RateManualIdx
		END
		ELSE If @LiquidateionTypex = 3 Begin --Si es por vigencia
			Set @rateVariation = @RateVariationAuxx
			Select @rateManualId = rm.Id, @ratemanualType = rm.[Type]
				, @rateManualRoundService = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(rm.RoundService, 1))
				, @rateManualValidityCodeName = Concat(rmv.Code, ' - ', rmv.[Name])
			From [Contract].RateManualValidity rmv With(Nolock) 
			LEFT JOIN Contract.RateManualValidityDetail rmvd WITH (NOLOCK) ON rmv.Id = rmvd.RateManualValidityId AND @ServiceDate BETWEEN rmvd.InitialDate AND rmvd.EndDate
			LEFT JOIN Contract.RateManual rm WITH (NOLOCK) ON rmvd.RateManualId = rm.Id
			Where rmv.Id = @RateManualValidityIdx

			If @rateManualId IS NULL Begin					
				Update @ServiceOrderDetail Set StatusResult = 0, MessageResult = 'El servicio IPS ' + Concat(CodeNameIpsService, ' no tiene parametrizado un manual de tarifas para la vigencia ', @rateManualValidityCodeName, ' para la fecha del servicio ', @ServiceDate)
				Return
			End
		End
		ELSE IF @LiquidateionTypex = 1 BEGIN
				SET @rateManualType=@ManualTypex
				SET	@rateManualId=@RateManualIdx
				
		END
	End
	--consulto el ips y el cups

	Declare @UVRNumber Int,
			@ServiceManual Tinyint,
			@SurgicalGroupId Int,
			@SalePriceIncludeTax bit,
			@GrossValue Numeric(20,2),
			@IVAPercent NUMERIC(5,2),
			@IVAId int

		set @SalePriceIncludeTax =(SELECT top 1 SalePriceIncludeTax from GeneralLedger.CompanySettings)

	Select	@UVRNumber = ips.UVRNumber,
			@ServiceManual = ips.ServiceManual,
			@SurgicalGroupId = ips.SurgicalGroupId,
			@IVAId = iif(ips.TaxedProduct=1,iva.Id,null),
			@IVAPercent = iif(ips.TaxedProduct=1,isnull(iva.Percentage,0),0)
	From [Contract].IPSService ips With(Nolock)
	LEFT JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) on ips.IVAId= iva.Id
	Where ips.Id = @IPSServiceId

	Update @ServiceOrderDetail Set SubTotalSalesPrice = 0, RateManualSalePrice = 0, TotalSalesPrice = 0, GrossValue =0, TaxValue=0
	
	Declare @errorsSurgical Varchar(Max) = '',
		@ClassServiceD Varchar(30),
		@ServiceAmountD Int,
		@IPSServiceIdD Int,
		@RowIdD Int
	
	Declare @Rows Int, @RowId Int
	Set @Rows = 1
	Set @RowId = 1
	
	While @Rows > 0
	begin

		Select Top 1 @RowId = RowId, @ClassServiceD = ClassService, @ServiceAmountD = ServiceAmount, @IPSServiceIdD = IPSServiceId, @RowIdD = RowId 
		From @listSurgicalProcedureServiceDefault Where RowId >= @RowId Order By RowId

		Set @Rows = @@ROWCOUNT
		If @Rows = 0 
			Break

			--esta validacion la hago por si el servicio es no cruento no busque los materiales de sutura en las tablas
			If @ClassServiceD = 'Materiales Sutura' And @SurgicalInterventionType = 9 Begin
				Set @RowId += 1				
				Continue
			End

			--consulto el manual de tarifas 
			Declare @rateManualDetailSurgicalId Int,
				@SalesValueWithSurcharge Decimal(18, 2),
				@SalesValue Decimal(18, 2),
				@costValueItem Decimal(18, 2),
				@ServiceClass Tinyint,
				@Score Decimal(18, 2),
				@NewScore Decimal(18, 2),
				@ApplyChangeScore Bit,
				@IpsCodeName varchar(320),
				@IpsBillingConceptId Int

			Select @ServiceClass = ServiceClass, @Score = Score, @NewScore = NewScore
				, @ApplyChangeScore = ApplyChangeScore, @IpsCodeName = Concat(Code, ' - ', [Name])
				, @IpsBillingConceptId = BillingConceptId
			From [Contract].IPSService With(Nolock)
			Where Id = @IPSServiceIdD

			--valido el tipo del manual de tarifas - Tipo del manual tarifario 1 - ISS 2001 , 2 - ISS 2004,  3 - SOAT
			If @rateManualType < 3 Begin
				If @ServiceClass = 2 Or @ServiceClass = 3 Or @ServiceClass = 4 Begin --2 - Cirujano,  3 - Anesteciologo, 4 - Ayudante
					If @UVRNumber > 450 And @ApplyChangeScore = 1
						Set @costValueItem = @UVRNumber * @NewScore * @ServiceAmountD
					Else
						Set @costValueItem = @UVRNumber * @Score * @ServiceAmountD
				End
				Else Begin
					If @ServiceClass = 5 And @UVRNumber > 450 And @ApplyChangeScore = 1 Begin --5 - Derecho Sala
						Set @costValueItem = @UVRNumber * @NewScore * @ServiceAmountD
					End
					Else Begin

						If @ServiceManual <= 2 
							Select Top 1 @rateManualDetailSurgicalId = rms.Id, @SalesValue = rms.SalesValue, @SalesValueWithSurcharge = rms.SalesValueWithSurcharge
							From [Contract].RateManualDetailSurgical rms With(Nolock)
							Left Join [Contract].UVRRange uvr With(Nolock) On uvr.Id = rms.UVRRangeId
							Where RateManualId = @rateManualId And IPSServiceId = @IPSServiceIdD And @UVRNumber >= uvr.InitialUVR And @UVRNumber <= uvr.EndUVR
						Else
							Select Top 1 @rateManualDetailSurgicalId = rms.Id, @SalesValue = rms.SalesValue, @SalesValueWithSurcharge = rms.SalesValueWithSurcharge
							From [Contract].RateManualDetailSurgical rms With(Nolock)
							Left Join [Contract].UVRRange uvr With(Nolock) On uvr.Id = rms.UVRRangeId
							Where RateManualId = @rateManualId And IPSServiceId = @IPSServiceIdD And SurgicalGroupId = @SurgicalGroupId

						If @rateManualDetailSurgicalId Is Null Or @rateManualDetailSurgicalId = 0 Begin
							Set @errorsSurgical += Char(13) + Char(10) + 'El servicio IPS ' + @IpsCodeName + ' no esta parametrizado en el manual de tarifas quirúrgico'
							Set @RowId += 1
							Continue
						End
						If @SurchargeApply = 1
							Set @costValueItem = @SalesValueWithSurcharge * @ServiceAmountD
						Else
							Set @costValueItem = @SalesValue * @ServiceAmountD
					End
				End
			End
			Else Begin
				If @ServiceManual <= 2 
					Select Top 1 @rateManualDetailSurgicalId = rms.Id, @SalesValue = rms.SalesValue, @SalesValueWithSurcharge = rms.SalesValueWithSurcharge
					From [Contract].RateManualDetailSurgical rms With(Nolock)
					Left Join [Contract].UVRRange uvr With(Nolock) On uvr.Id = rms.UVRRangeId
					Where RateManualId = @rateManualId And IPSServiceId = @IPSServiceIdD And @UVRNumber >= uvr.InitialUVR And @UVRNumber <= uvr.EndUVR
				Else
					Select Top 1 @rateManualDetailSurgicalId = rms.Id, @SalesValue = rms.SalesValue, @SalesValueWithSurcharge = rms.SalesValueWithSurcharge
					From [Contract].RateManualDetailSurgical rms With(Nolock)
					Left Join [Contract].UVRRange uvr With(Nolock) On uvr.Id = rms.UVRRangeId
					Where RateManualId = @rateManualId And IPSServiceId = @IPSServiceIdD And SurgicalGroupId = @SurgicalGroupId

				If @rateManualDetailSurgicalId Is Null Or @rateManualDetailSurgicalId = 0 Begin
					Set @errorsSurgical += Char(13) + Char(10) + 'El servicio IPS ' + @IpsCodeName + ' no esta parametrizado en el manual de tarifas quirúrgico'
					Set @RowId += 1
					Continue
				End
				If @SurchargeApply = 1
					Set @costValueItem = @SalesValueWithSurcharge * @ServiceAmountD
				Else
					Set @costValueItem = @SalesValue * @ServiceAmountD
			End

			Declare @totalSalesPriceItem Decimal(18, 2) = @costValueItem + (@costValueItem * (@rateVariation / 100))
			SELECT	@totalSalesPriceItem = [Billing].[RoundValue](@totalSalesPriceItem, @rateManualRoundService)

			Update @ServiceOrderDetail 
				Set RateManualSalePrice += @totalSalesPriceItem,
					RoundService = @rateManualRoundService
			
			If (Select Count(RowId)
				From @serviceOrderDetailSurgical Where IPSServiceId = @IPSServiceIdD) = 0 Begin
				
				Declare @newId Int = (Select Max(RowId) + 1 From @serviceOrderDetailSurgical)

				Insert Into @serviceOrderDetailSurgical
				Values (@newId, @IpsCodeName, @IPSServiceIdD, @ServiceAmountD
					, 0, isnull(@totalSalesPriceItem,0), @ClassServiceD, isnull(@totalSalesPriceItem,0), Null, Null, @CostValueSod, @IpsBillingConceptId, @CostCenterSOD, Null,@SurchargeApply, Null, @rateManualRoundService)

				If @ClassServiceD = 'Cirujano' Begin
					Update @serviceOrderDetailSurgical Set PerformsHealthProfessionalCode = @PerformsHealthProfessionalCodeSOD, PerformsHealthProfessionalThirdPartyId = @PerformsHealthProfessionalThirdPartyIdSOD
					Where RowId = @newId
				End
				If @IpsBillingConceptId Is Null Begin
					Set @errorsSurgical += 'El servicio IPS ' + @IpsCodeName + ' no tiene asignado concepto de facturación'
					Set @RowId += 1
					Continue
				End
				If @rateManualDetailSurgicalId Is Not Null And @rateManualDetailSurgicalId > 0 Begin
					Update @serviceOrderDetailSurgical Set RateManualDetailSurgicalId = @rateManualDetailSurgicalId
					Where RowId = @newId
				End
			End

		Set @RowId += 1
	End

	-- Eliminamos los servicios de service order detail que no se encuentran en los enviados
	DELETE FROM @serviceOrderDetailSurgical WHERE IPSServiceId NOT IN (
		SELECT IPSServiceId FROM @listSurgicalProcedureServiceDefault
	)

	DECLARE @_valueSalesPrice as NUMERIC(20,2) = (Select Sum(TotalSalesPrice) From @serviceOrderDetailSurgical)

	IF @SalePriceIncludeTax = 1 BEGIN
		set @GrossValue = @_valueSalesPrice/((@IVAPercent/100)+1)
	END
	ELSE BEGIN
		set  @GrossValue = @_valueSalesPrice				
		set	 @_valueSalesPrice = @GrossValue+(@GrossValue *(@IVAPercent/100))
	END

	If @errorsSurgical <> '' Begin
		Update @ServiceOrderDetail Set StatusResult = 0, MessageResult = @errorsSurgical
		Return
	End

	Update @ServiceOrderDetail Set	GrossValue = @GrossValue,
									TaxValue = (@GrossValue *(@IVAPercent/100)),
									IvaId = @IVAId,
									TotalSalesPrice = @_valueSalesPrice,
									SubTotalSalesPrice = @_valueSalesPrice,
									GrandTotalSalesPrice = (@GrossValue + (@GrossValue *(@IVAPercent/100))) * InvoicedQuantity,
									StatusResult = 1,
									ServiceOrderDetailSurgicalXml = (
																		Select * From @serviceOrderDetailSurgical For Xml Path('ServiceOrderDetailSurgical'), Elements
																	)			
	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el valor tarifario de los servicios asociados a un procedimiento quirúrgico, recibiendo como entrada los detalles de la orden de servicio en formato XML (incluyendo servicios quirúrgicos adicionales y procedimientos predeterminados). Consulta el catálogo de servicios CUPS para obtener el concepto RIPS del procedimiento y, a través de la función Contract.GetRateValue, determina la tarifa aplicable según el contrato, el tipo de liquidación, la especialidad del profesional que ejecuta y el tipo de intervención quirúrgica (principal, ayudantía, anestesia, etc.). Retorna una tabla con el desglose económico completo de cada ítem: subtotal, total de venta, valor de costo, valor bruto, impuestos (IVA), recargos y la información del profesional ejecutor y unidad funcional, sirviendo como motor de tarifación en el proceso de facturación y liquidación de cirugías.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetServiceValueBySurgicalProcedureService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetServiceValueBySurgicalProcedureService';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el valor de venta de un servicio quirúrgico (cirujano, anestesiólogo, ayudante, derecho de sala, materiales de sutura) aplicando el manual tarifario vigente, UVR, recargos e IVA, y devuelve el detalle quirúrgico para la orden de servicio.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetServiceValueBySurgicalProcedureService';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener un nodo /ServiceOrderDetail con los datos básicos del servicio.; El CUPSEntityId del detalle debe existir en Contract.CUPSEntity para obtener el RIPSConcept.; El IPSServiceId debe existir en Contract.IPSService con UVRNumber, ServiceManual, SurgicalGroupId y configuración de IVA.; Debe existir una tarifa retornada por Contract.GetRateValue para la combinación CUPS/grupo/UF/especialidad/fecha.; Si el tipo de liquidación es por vigencia (3), debe existir RateManualValidityDetail con la fecha del servicio dentro de InitialDate-EndDate.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetServiceValueBySurgicalProcedureService';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] @ServiceOrderDetail: Cuando LiquidationType=3 (vigencia) y no se encuentra RateManual asociado a la vigencia para la fecha del servicio, marca StatusResult=0 con mensaje indicando que el servicio IPS no tiene manual de tarifas parametrizado para esa vigencia y fecha.; [UPDATE] @ServiceOrderDetail: Inicializa SubTotalSalesPrice, RateManualSalePrice, TotalSalesPrice, GrossValue y TaxValue en 0 antes de recalcular.; [UPDATE] @ServiceOrderDetail: Por cada ítem quirúrgico procesado acumula RateManualSalePrice += totalSalesPriceItem y asigna RoundService con el redondeo del manual.; [UPDATE] @ServiceOrderDetail: Si @errorsSurgical no está vacío (faltó parametrización en manual quirúrgico o concepto de facturación), marca StatusResult=0 y MessageResult con los errores acumulados, y retorna.; [UPDATE] @ServiceOrderDetail: Al final, fija GrossValue, TaxValue=GrossValue*(IVAPercent/100), IvaId, TotalSalesPrice=SubTotalSalesPrice=valor con IVA, GrandTotalSalesPrice=(Gross+IVA)*InvoicedQuantity, StatusResult=1 y serializa @serviceOrderDetailSurgical en ServiceOrderDetailSurgicalXml.; [INSERT] @serviceOrderDetailSurgical: Si un IPSService de la lista por defecto no existe aún en el detalle quirúrgico recibido, lo inserta con el valor calculado, costo, concepto de facturación, centro de costo y recargo.; [UPDATE] @serviceOrderDetailSurgical: Cuando el ítem insertado tiene ClassService=''Cirujano'', copia PerformsHealthProfessionalCode y PerformsHealthProfessionalThirdPartyId desde el detalle principal.; [UPDATE] @serviceOrderDetailSurgical: Si el ítem insertado tiene rateManualDetailSurgicalId válido, lo asigna en RateManualDetailSurgicalId.; [DELETE] @serviceOrderDetailSurgical: Elimina del detalle quirúrgico cualquier IPSServiceId que no esté presente en la lista de servicios quirúrgicos por defecto recibida.; [RETURN_RESULT] @ServiceOrderDetail: Devuelve la tabla @ServiceOrderDetail con los valores recalculados y el XML del detalle quirúrgico.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetServiceValueBySurgicalProcedureService';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetServiceValueBySurgicalProcedureService';
GO
