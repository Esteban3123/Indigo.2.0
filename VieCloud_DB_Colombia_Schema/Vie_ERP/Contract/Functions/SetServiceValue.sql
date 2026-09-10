CREATE Function [Contract].[SetServiceValue]
(
	@distributionToRetarificXml Xml,
	@retarificServiceOrdenDetailSurgicalXml Xml,
	@serviceOrderDetailXml Xml,
	@serviceOrderDetailSurgicalXml Xml,
	@GuidHomologation Varchar(50),
	@CareGroupId Int
)
Returns @ServiceOrderDetail Table
(
	StatusResult Bit,
	MessageResult Varchar(Max),
	ServiceOrderDetailXml Xml,
	ServiceOrderDetailSurgicalXml Xml
) 
As
Begin
	Declare @ServiceOrderDetailId Int
	Declare @distributionToRetarific Table(Id Int Primary Key, 
		RevenueControlDetailId Int,
		ServiceOrderDetailId Int,
		Quantity Int,
		GrandTotalSalesPrice Decimal(20, 2),
		GrandTotalDiscount Decimal(20, 2),
		DistributionType Tinyint,
		ThirdPartySalesPrice Decimal(20, 2),
		ThirdPartyPercentage Decimal(5, 2),
		ApplyRecoveryFee Tinyint,
		RecoveryFeeType Tinyint,
		SubTotalPatientSalesPrice Decimal(20, 2),
		PatientPercentage Decimal(5, 2),
		LastCaregroupId Int,
		--ServiceOrderDetail
		ServiceOrderId Int,
		CareGroupId Int,
		HealthAdministratorId Int Null,
		ThirdPartyId Int Null,
		ServiceType Tinyint,
		RecordType Tinyint,
		CUPSEntityId Int Null,
		IPSServiceId Int Null,
		HospitalStayId Int Null,
		HospitalStayDetailId Int Null,
		ControlExternalConsultation Tinyint Null,
		ControlExternalConsultationCode Decimal(18, 0) Null,
		CUPSAssociateService Bit,
		CodeAssociateService Varchar(50) Null,
		IsPackage Bit,
		Packaging Bit,
		PackageServiceOrderDetailId Int Null,
		LiquidationType Tinyint,
		Presentation Tinyint Null,
		ProductId Int Null,
		InvoicedQuantity Int,
		SupplyQuantity Int,
		DevolutionQuantity Int,
		RateManualSalePrice Decimal(20, 2),
		CostValue Decimal(20, 2),
		ServiceDate DateTime,
		AuthorizationNumber Varchar(20),
		PerformsFunctionalUnitId Int,
		PerformsHealthProfessionalCode Char(20),
		PerformsProfessionalSpecialty Char(3),
		PerformsHealthProfessionalThirdPartyId Int Null,
		BillingConceptId Int Null,
		CostCenterId Int,
		SettlementType Tinyint,
		IncludeServiceOrderDetailId Int Null,
		RecoveryRatio Decimal(5, 2) Null,
		RateManualId Int Null,
		RateManualType Tinyint Null,
		RateManualDetailId Int Null,
		DefinitionRateDetailId Int Null,
		DefinitionRateDetailConditionId Int Null,
		SubTotalSalesPrice_1 Decimal(20, 2),
		ThirdPartyDiscount_1 Decimal(20, 2),
		ThirdPartyDiscountPercentage Decimal(5, 2),
		TotalSalesPrice Decimal(20, 2),
		GrandTotalSalesPrice_1 Decimal(20, 2),
		SurchargeApply Bit,
		SurgicalInterventionType Tinyint Null,
		SurgeryNumber Tinyint,
		IsFirstEvent Bit,
		IsAnnulled Bit,
		IsDelete Bit,
		IncomeMainAccountId Int,
		--Extender
		ServiceOrderDetailSurgicalId Int Null,		
		CodeNameSpeciality Varchar(300) Null,
		CodeNameFunctionalUnit Varchar(300) Null,
		CodeNameHealthAdministrator Varchar(300) Null,				
		PreviusServiceOrderDetailId Int Null,				
		CodeNameCareGroup Varchar(320) Null,
		CodeNameCostCenter Varchar(300) Null,
		CodeNameCups Varchar(320) Null,
		CodeNameHealthProfessional Varchar(300) Null,
		CodeNameIpsService Varchar(300) Null,
		CodeNameProduct Varchar(300) Null,				
		IsSOAT Bit,
		RoundService int,
		---------------------------
		GrossValue					numeric(20,2),
		TaxValue					numeric(20,2),
		IvaId						int
	)
				
	Insert Into @distributionToRetarific
		Select	t.x.value('Id[1]','Int'),
				t.x.value('RevenueControlDetailId[1]','Int'),
				t.x.value('ServiceOrderDetailId[1]','Int'),
				t.x.value('Quantity[1]','Int'),
				t.x.value('GrandTotalSalesPrice[1]','Decimal(20, 2)'),
				t.x.value('GrandTotalDiscount[1]','Decimal(20, 2)'),
				t.x.value('DistributionType[1]','Tinyint'),
				t.x.value('ThirdPartySalesPrice[1]','Decimal(20, 2)'),
				t.x.value('ThirdPartyPercentage[1]','Decimal(5, 2)'),
				t.x.value('ApplyRecoveryFee[1]','Tinyint'),
				t.x.value('RecoveryFeeType[1]','Tinyint'),
				t.x.value('SubTotalPatientSalesPrice[1]','Decimal(20, 2)'),
				t.x.value('PatientPercentage[1]','Decimal(5, 2)'),
				t.x.value('LastCaregroupId[1]','Int'),
				t.x.value('ServiceOrderId[1]','Int'),
				t.x.value('CareGroupId[1]','Int'),
				t.x.value('HealthAdministratorId[1]','Int'),
				t.x.value('ThirdPartyId[1]','Int'),
				t.x.value('ServiceType[1]','Tinyint'),
				t.x.value('RecordType[1]','Tinyint'),
				t.x.value('CUPSEntityId[1]','Int'),
				t.x.value('IPSServiceId[1]','Int'),
				t.x.value('HospitalStayId[1]','Int'),
				t.x.value('HospitalStayDetailId[1]','Int'),
				t.x.value('ControlExternalConsultation[1]','Tinyint'),
				t.x.value('ControlExternalConsultationCode[1]','Decimal(18, 0)'),
				t.x.value('CUPSAssociateService[1]','Bit'),
				t.x.value('CodeAssociateService[1]','Varchar(50)'),
				t.x.value('IsPackage[1]','Bit'),
				t.x.value('Packaging[1]','Bit'),
				t.x.value('PackageServiceOrderDetailId[1]','Int'),
				t.x.value('LiquidationType[1]','Tinyint'),
				t.x.value('Presentation[1]','Tinyint'),
				t.x.value('ProductId[1]','Int'),
				t.x.value('InvoicedQuantity[1]','Int'),
				t.x.value('SupplyQuantity[1]','Int'),
				t.x.value('DevolutionQuantity[1]','Int'),
				t.x.value('RateManualSalePrice[1]','Decimal(20, 2)'),
				t.x.value('CostValue[1]','Decimal(20, 2)'),
				t.x.value('ServiceDate[1]','DateTime'),
				t.x.value('AuthorizationNumber[1]','Varchar(20)'),
				t.x.value('PerformsFunctionalUnitId[1]','Int'),
				t.x.value('PerformsHealthProfessionalCode[1]','Char(20)'),
				t.x.value('PerformsProfessionalSpecialty[1]','Char(3)'),
				t.x.value('PerformsHealthProfessionalThirdPartyId[1]','Int'),
				t.x.value('BillingConceptId[1]','Int'),
				t.x.value('CostCenterId[1]','Int'),
				t.x.value('SettlementType[1]','Tinyint'),
				t.x.value('IncludeServiceOrderDetailId[1]','Int'),
				t.x.value('RecoveryRatio[1]','Decimal(5, 2)'),
				t.x.value('RateManualId[1]','Int'),
				t.x.value('RateManualType[1]','Tinyint'),
				t.x.value('RateManualDetailId[1]','Int'),
				t.x.value('DefinitionRateDetailId[1]','Int'),
				t.x.value('DefinitionRateDetailConditionId[1]','Int'),
				t.x.value('SubTotalSalesPrice_1[1]','Decimal(20, 2)'),
				t.x.value('ThirdPartyDiscount_1[1]','Decimal(20, 2)'),
				t.x.value('ThirdPartyDiscountPercentage[1]','Decimal(5, 2)'),
				t.x.value('TotalSalesPrice[1]','Decimal(20, 2)'),
				t.x.value('GrandTotalSalesPrice_1[1]','Decimal(20, 2)'),
				t.x.value('SurchargeApply[1]','Bit'),
				t.x.value('SurgicalInterventionType[1]','Tinyint'),
				t.x.value('SurgeryNumber[1]','Tinyint'),
				t.x.value('IsFirstEvent[1]','Bit'),
				t.x.value('IsAnnulled[1]','Bit'),
				t.x.value('IsDelete[1]','Bit'),
				t.x.value('IncomeMainAccountId[1]','Int'),
				t.x.value('ServiceOrderDetailSurgicalId[1]','Int'),
				t.x.value('CodeNameSpeciality[1]','Varchar(300)'),
				t.x.value('CodeNameFunctionalUnit[1]','Varchar(300)'),
				t.x.value('CodeNameHealthAdministrator[1]','Varchar(300)'),
				t.x.value('PreviusServiceOrderDetailId[1]','Int'),
				t.x.value('CodeNameCareGroup[1]','Varchar(300)'),
				t.x.value('CodeNameCostCenter[1]','Varchar(300)'),
				t.x.value('CodeNameCups[1]','Varchar(300)'),
				t.x.value('CodeNameHealthProfessional[1]','Varchar(300)'),
				t.x.value('CodeNameIpsService[1]','Varchar(300)'),
				t.x.value('CodeNameProduct[1]','Varchar(300)'),
				t.x.value('IsSOAT[1]','Bit'),
				t.x.value('RoundService[1]','int'),
				t.x.value('GrossValue[1]','numeric(20,2)'),
				t.x.value('TaxValue[1]','numeric(20,2)'),
				t.x.value('IvaId[1]','int')
		From @distributionToRetarificXml.nodes('/DistributionToRetarific') t(x)

	Select @ServiceOrderDetailId = ServiceOrderDetailId
	from @distributionToRetarific

	Declare @__CUPSEntityId Int,
			@__CodeNameCups Varchar(320),
			@__IPSServiceId Int,
			@__CodeNameIpsService Varchar(320),
			@__IsSOAT Bit,
			@__Presentation Tinyint,
			@__CodeNameFunctionalUnit Varchar(320),
			@__CodeNameCostCenter Varchar(100),
			@__SubTotalSalesPrice Decimal(18, 2),
			@__RateManualSalePrice Decimal(18, 2),
			------------------------------------------
			@__GrossValue	numeric(20,2),
			@__TaxValue		numeric(20,2),
			@__IvaId		int

	Select	@__CUPSEntityId = t.x.value('CUPSEntityId[1]', 'Int'),
			@__CodeNameCups = t.x.value('CodeNameCups[1]', 'Varchar(320)'),
			@__IPSServiceId = t.x.value('IPSServiceId[1]', 'Int'),
			@__CodeNameIpsService = t.x.value('CodeNameIpsService[1]', 'Varchar(320)'),
			@__IsSOAT = t.x.value('IsSOAT[1]', 'Bit'),
			@__Presentation = t.x.value('Presentation[1]', 'Tinyint'),
			@__CodeNameFunctionalUnit = t.x.value('CodeNameFunctionalUnit[1]', 'Varchar(320)'),
			@__CodeNameCostCenter = t.x.value('CodeNameCostCenter[1]', 'Varchar(100)'),
			@__SubTotalSalesPrice = t.x.value('SubTotalSalesPrice[1]', 'Decimal(20, 2)'),
			@__RateManualSalePrice = t.x.value('RateManualSalePrice[1]', 'Decimal(20, 2)'),
			@__GrossValue = t.x.value('GrossValue[1]', 'Decimal(20, 2)'),
			@__TaxValue = t.x.value('TaxValue[1]', 'Decimal(20, 2)'),
			@__IvaId = t.x.value('IvaId[1]', 'int')
	From @serviceOrderDetailXml.nodes('/ServiceOrderDetail') t(x)
	
	Declare @RIPSConcept VARCHAR(2) = '', @RateManualId Int, @RetarificPresentation Tinyint, @RetarificSettlementType Tinyint
			, @RSubTotalSalesPrice_1 Decimal(18, 2), @RThirdPartyDiscount_1 Decimal(18, 0), @RQuantity Int
			, @RIncludeServiceOrderDetailId int

	SELECT @RipsConcept = RIPSConcept
	FROM Contract.CUPSEntity
	WHERE Id = @__CUPSEntityId

	Select @RateManualId = RateManualId, @RetarificPresentation = Presentation, @RetarificSettlementType = SettlementType
			, @RSubTotalSalesPrice_1 = SubTotalSalesPrice_1, @RThirdPartyDiscount_1 = ThirdPartyDiscount_1, @RQuantity = Quantity
			, @RIncludeServiceOrderDetailId = IncludeServiceOrderDetailId
	From @distributionToRetarific

	Declare @round Int = 0
	If @RateManualId Is Not Null And @RateManualId > 0		
		Select	@round = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(RoundService, 1))
		From [Contract].RateManual With(Nolock) 
		Where Id = @RateManualId

	Update @distributionToRetarific Set CareGroupId = @CareGroupId
			, CodeNameCareGroup = ''
			, CodeNameCostCenter = @__CodeNameCostCenter
			, CodeNameCups = @__CodeNameCups
			, CodeNameFunctionalUnit = @__CodeNameFunctionalUnit
			, CodeNameHealthAdministrator = Null
			, CodeNameHealthProfessional = Null
			, CodeNameIpsService = @__CodeNameIpsService
			, CodeNameProduct = Null
			, CodeNameSpeciality = Null
			, RateManualSalePrice = @__RateManualSalePrice
			, IsSOAT = @__IsSOAT
			, CodeAssociateService = @GuidHomologation
			, ApplyRecoveryFee = 1
			, RecoveryFeeType = 1
			, LastCaregroupId = @CareGroupId
			, IPSServiceId = @__IPSServiceId
			, Presentation = @__Presentation

	If @RetarificPresentation = 2 Begin
		--Quirurgico

		Declare @ServiceOrderDetailSurgical Table(
			RowId Int Primary Key Identity(1, 1),
			[Id] [int] NULL,
			ServiceOrderDetailId Int,
			[CodeNameIpsService] [varchar](320) Null,
			[IPSServiceId] [int] NOT NULL,
			[InvoicedQuantity] [int] NOT NULL,
			[LiquidationPercentage] [numeric](5, 2) NOT NULL,
			[RateManualSalePrice] [numeric](20, 2) NOT NULL,
			[TotalSalesPrice] [numeric](20, 2) NOT NULL,
			[PerformsHealthProfessionalCode] [char](20) NULL,
			[PerformsHealthProfessionalThirdPartyId] [int] NULL,
			[CostValue] [numeric](20, 2) NOT NULL,
			[BillingConceptId] [int] NOT NULL,
			[CostCenterId] [int] NOT NULL,
			[RateManualDetailSurgicalId] [int] NULL,
			[SurchargeApply] [bit] NOT NULL,
			[IncomeMainAccountId] [int] NOT NULL,
			ClassServiceIps Varchar(30) Null,
			RoundService int
		)
	
		Insert Into @ServiceOrderDetailSurgical
			Select	t.x.value('Id[1]', 'Int'),
					t.x.value('ServiceOrderDetailId[1]', 'Int'),
					t.x.value('CodeNameIpsService[1]', 'Varchar(320)'),
					t.x.value('IPSServiceId[1]', 'Int'),
					t.x.value('InvoicedQuantity[1]', 'Int'),
					t.x.value('LiquidationPercentage[1]', 'Decimal(5, 2)'),
					t.x.value('RateManualSalePrice[1]', 'Decimal(20, 2)'),
					t.x.value('TotalSalesPrice[1]', 'Decimal(20, 2)'),
					t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)'),
					t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'Int'),
					t.x.value('CostValue[1]', 'Decimal(20, 2)'),
					t.x.value('BillingConceptId[1]', 'Int'),
					t.x.value('CostCenterId[1]', 'Int'),
					t.x.value('RateManualDetailSurgicalId[1]', 'Int'),
					t.x.value('SurchargeApply[1]', 'Bit'),
					t.x.value('IncomeMainAccountId[1]', 'Int'),
					t.x.value('ClassServiceIps[1]', 'Varchar(30)'),
					t.x.value('RoundService[1]', 'Int')
			From @retarificServiceOrdenDetailSurgicalXml.nodes('/RetarificServiceOrderDetailSurgical') t(x)

		Declare @serviceOrderDetailSurgicalResult Table(RowId Int,
			CodeNameIpsService Varchar(320),
			IPSServiceId Int,
			InvoicedQuantity Int,
			LiquidationPercentage Decimal(5, 2),
			TotalSalesPrice Decimal(20, 2),
			ClassServiceIps Varchar(30),
			RateManualSalePrice Decimal(20, 2),
			PerformsHealthProfessionalCode Char(20) Null,
			PerformsHealthProfessionalThirdPartyId Int Null,
			CostValue Decimal(20, 2),
			BillingConceptId Int,
			CostCenterId Int,
			RateManualDetailSurgicalId Int Null,
			SurchargeApply Bit,
			IncomeMainAccountId Int Null,
			RoundService int
		)

		Insert Into @serviceOrderDetailSurgicalResult
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
					t.x.value('CostValue[1]', 'Decimal(20, 2)'),
					t.x.value('BillingConceptId[1]', 'Int'),
					t.x.value('CostCenterId[1]', 'Int'),
					t.x.value('RateManualDetailSurgicalId[1]', 'Int'),
					t.x.value('SurchargeApply[1]', 'Bit'),
					t.x.value('IncomeMainAccountId[1]', 'Int'),
					t.x.value('RoundService[1]', 'Int')
			From @ServiceOrderDetailSurgicalXml.nodes('/ServiceOrderDetailSurgical') t(x)
		
		Declare @SurgicalId Int, @IPSServiceIdS Int, @InvoicedQuantityS Int, @LiquidationPercentageS Decimal(5, 2), @RateManualSalePriceS Decimal(18, 0)
			, @TotalSalesPriceS Decimal(18, 0)
			, @PerformsHealthProfessionalCodeS Char(20), @PerformsHealthProfessionalThirdPartyIdS Int, @CostValueS Decimal(18, 2)
			, @BillingConceptIdS Int, @CostCenterIdS Int
			, @RateManualDetailSurgicalIdS Int, @SurchargeApplyS Bit, @IncomeMainAccountIdS Int

		Declare @Rows Int, @RowId Int

		Set @Rows = 1
		Set @RowId = 1

		While @Rows > 0
		begin

			Select Top 1 @RowId = RowId, @SurgicalId = Id, @IPSServiceIdS = IPSServiceId, @InvoicedQuantityS = InvoicedQuantity, @LiquidationPercentageS = LiquidationPercentage
				, @RateManualSalePriceS = RateManualSalePrice, @TotalSalesPriceS = TotalSalesPrice, @PerformsHealthProfessionalCodeS = PerformsHealthProfessionalCode
				, @PerformsHealthProfessionalThirdPartyIdS = PerformsHealthProfessionalThirdPartyId, @CostValueS = CostValue, @BillingConceptIdS = BillingConceptId
				, @CostCenterIdS = CostCenterId, @RateManualDetailSurgicalIdS = RateManualDetailSurgicalId, @SurchargeApplyS = SurchargeApply, @IncomeMainAccountIdS = IncomeMainAccountId
			From @ServiceOrderDetailSurgical Where ServiceOrderDetailId = @ServiceOrderDetailId And RowId >= @RowId Order By RowId

			Set @Rows = @@ROWCOUNT
			If @Rows = 0 
				Break
			
			Declare @ServiceClassCurrent Tinyint
			Select @ServiceClassCurrent = ServiceClass From [Contract].IPSService With(Nolock) Where Id = @IPSServiceIdS
			
			If @ServiceClassCurrent = 1
				Update @serviceOrderDetailSurgicalResult Set PerformsHealthProfessionalThirdPartyId = @PerformsHealthProfessionalThirdPartyIdS, PerformsHealthProfessionalCode = @PerformsHealthProfessionalCodeS
				Where ClassServiceIps = 'Ninguno'
			Else If @ServiceClassCurrent = 2
				Update @serviceOrderDetailSurgicalResult Set PerformsHealthProfessionalThirdPartyId = @PerformsHealthProfessionalThirdPartyIdS, PerformsHealthProfessionalCode = @PerformsHealthProfessionalCodeS
				Where ClassServiceIps = 'Cirujano'
			Else If @ServiceClassCurrent = 3
				Update @serviceOrderDetailSurgicalResult Set PerformsHealthProfessionalThirdPartyId = @PerformsHealthProfessionalThirdPartyIdS, PerformsHealthProfessionalCode = @PerformsHealthProfessionalCodeS
				Where ClassServiceIps = 'Anestesiólogo'
			Else If @ServiceClassCurrent = 4
				Update @serviceOrderDetailSurgicalResult Set PerformsHealthProfessionalThirdPartyId = @PerformsHealthProfessionalThirdPartyIdS, PerformsHealthProfessionalCode = @PerformsHealthProfessionalCodeS
				Where ClassServiceIps = 'Ayudante'
			Else If @ServiceClassCurrent = 5
				Update @serviceOrderDetailSurgicalResult Set PerformsHealthProfessionalThirdPartyId = @PerformsHealthProfessionalThirdPartyIdS, PerformsHealthProfessionalCode = @PerformsHealthProfessionalCodeS
				Where ClassServiceIps = 'Derecho Sala'
			Else If @ServiceClassCurrent = 6
				Update @serviceOrderDetailSurgicalResult Set PerformsHealthProfessionalThirdPartyId = @PerformsHealthProfessionalThirdPartyIdS, PerformsHealthProfessionalCode = @PerformsHealthProfessionalCodeS
				Where ClassServiceIps = 'Materiales Sutura'
			Else If @ServiceClassCurrent = 7
				Update @serviceOrderDetailSurgicalResult Set PerformsHealthProfessionalThirdPartyId = @PerformsHealthProfessionalThirdPartyIdS, PerformsHealthProfessionalCode = @PerformsHealthProfessionalCodeS
				Where ClassServiceIps = 'Instrumentación Quirúrgica'

			Set @RowId += 1
		End

		DELETE @ServiceOrderDetailSurgical
		Insert Into @ServiceOrderDetailSurgical
			Select 0, @ServiceOrderDetailId, CodeNameIpsService, IPSServiceId, InvoicedQuantity, LiquidationPercentage, RateManualSalePrice, TotalSalesPrice
				, PerformsHealthProfessionalCode, PerformsHealthProfessionalThirdPartyId, CostValue, BillingConceptId, CostCenterId
				, RateManualDetailSurgicalId, SurchargeApply, IncomeMainAccountId, ClassServiceIps, RoundService
			From @serviceOrderDetailSurgicalResult
		
	End
	
	DECLARE @Taxpercent as numeric(5,2),
			@SalesPrices as NUMERIC(20,2)

	If @RetarificSettlementType = 1 Begin --Manual de tarifas
		
		UPDATE @distributionToRetarific set GrossValue =ISNULL(@__GrossValue,@__SubTotalSalesPrice), TaxValue=@__TaxValue,IvaId =@__IvaId

		Update @distributionToRetarific Set SubTotalSalesPrice_1 =ISNULL(@__GrossValue,@__SubTotalSalesPrice) + ISNULL(@__TaxValue,0)
			, ThirdPartyDiscount_1 = Round((ISNULL(@__GrossValue,@__SubTotalSalesPrice) + ISNULL(@__TaxValue,0)) * (ThirdPartyDiscountPercentage / 100), 2)

		If (@RSubTotalSalesPrice_1 - @RThirdPartyDiscount_1) < 100
			Update @distributionToRetarific Set TotalSalesPrice = (SubTotalSalesPrice_1 - ThirdPartyDiscount_1)
		Else
			Update @distributionToRetarific Set TotalSalesPrice = [Billing].[RoundValue](SubTotalSalesPrice_1 - ThirdPartyDiscount_1, @round)
		If ((@RSubTotalSalesPrice_1 - @RThirdPartyDiscount_1) * @RQuantity) < 100
			Update @distributionToRetarific Set GrandTotalSalesPrice_1 = [Billing].[RoundValue](TotalSalesPrice * Quantity, 0), RoundService = 1
		Else
			Update @distributionToRetarific Set GrandTotalSalesPrice_1 = [Billing].[RoundValue](TotalSalesPrice * Quantity, @round), RoundService = @round

		Update @distributionToRetarific Set GrandTotalSalesPrice = GrandTotalSalesPrice_1
			, ThirdPartySalesPrice = GrandTotalSalesPrice_1
			, GrandTotalDiscount = ThirdPartyDiscount_1 * Quantity
			, ThirdPartyPercentage = 100
			, SubTotalPatientSalesPrice = 0
			, PatientPercentage = 0
	End
	Else If @RetarificSettlementType = 2 Begin --PorcentajeOtroServicio
		Declare @IncludeSubtotalSalesPrice Decimal(18, 2)				

		Select @IncludeSubtotalSalesPrice = SubTotalSalesPrice ,
				@Taxpercent =isnull( [iva].Percentage,0)
		From [Billing].ServiceOrderDetail sod With(Nolock)
		left join GeneralLedger.GeneralLedgerIVA iva with(NOLOCK) on sod.IvaId= iva.Id
		Where sod.Id = @RIncludeServiceOrderDetailId

		select @__GrossValue = GrossValue, @__TaxValue = TaxValue, @SalesPrices = SalesPrice
		from Billing.SetValueSalesPrice (1,@IncludeSubtotalSalesPrice,@Taxpercent)

		Update @distributionToRetarific Set SubTotalSalesPrice_1 = Round(@SalesPrices * RecoveryRatio / 100, 0)
			, ThirdPartyDiscount_1 = Round((Round(@SalesPrices * RecoveryRatio / 100, 0) * (ThirdPartyDiscountPercentage / 100)), 0)
		If (@RSubTotalSalesPrice_1 - @RThirdPartyDiscount_1) < 100
			Update @distributionToRetarific Set TotalSalesPrice = (SubTotalSalesPrice_1 - ThirdPartyDiscount_1)
		Else
			Update @distributionToRetarific Set TotalSalesPrice = [Billing].[RoundValue](SubTotalSalesPrice_1 - ThirdPartyDiscount_1, @round)
		If ((@RSubTotalSalesPrice_1 - @RThirdPartyDiscount_1) * @RQuantity) < 100
			Update @distributionToRetarific Set GrandTotalSalesPrice_1 = [Billing].[RoundValue](TotalSalesPrice * Quantity, 0), RoundService = 1
		Else
			Update @distributionToRetarific Set GrandTotalSalesPrice_1 = [Billing].[RoundValue](TotalSalesPrice * Quantity, @round), RoundService = @round

		Update @distributionToRetarific Set GrandTotalSalesPrice = GrandTotalSalesPrice_1
			, ThirdPartySalesPrice = GrandTotalSalesPrice_1
			, GrandTotalDiscount = ThirdPartyDiscount_1 * Quantity
			, ThirdPartyPercentage = 100
			, SubTotalPatientSalesPrice = 0
			, PatientPercentage = 0
			, GrossValue =  @__GrossValue
			, TaxValue= @__TaxValue
	End
	Else If @RetarificSettlementType = 2 Begin --PorcentajeMismoServicio

		Update @distributionToRetarific Set SubTotalSalesPrice_1 = Round(@__SubTotalSalesPrice * RecoveryRatio, 0)
			, ThirdPartyDiscount_1 = Round((Round(@__SubTotalSalesPrice * RecoveryRatio, 0) * (ThirdPartyDiscountPercentage / 100)), 0)

		If (@RSubTotalSalesPrice_1 - @RThirdPartyDiscount_1) < 100
			Update @distributionToRetarific Set TotalSalesPrice = (SubTotalSalesPrice_1 - ThirdPartyDiscount_1)
		Else
			Update @distributionToRetarific Set TotalSalesPrice = [Billing].[RoundValue](SubTotalSalesPrice_1 - ThirdPartyDiscount_1, @round)
		If ((@RSubTotalSalesPrice_1 - @RThirdPartyDiscount_1) * @RQuantity) < 100
			Update @distributionToRetarific Set GrandTotalSalesPrice_1 = [Billing].[RoundValue](TotalSalesPrice * Quantity, 0), RoundService = 1
		Else
			Update @distributionToRetarific Set GrandTotalSalesPrice_1 = [Billing].[RoundValue](TotalSalesPrice * Quantity, @round), RoundService = @round

		Update @distributionToRetarific Set GrandTotalSalesPrice = GrandTotalSalesPrice_1
			, ThirdPartySalesPrice = GrandTotalSalesPrice_1
			, GrandTotalDiscount = ThirdPartyDiscount_1 * Quantity
			, ThirdPartyPercentage = 100
			, SubTotalPatientSalesPrice = 0
			, PatientPercentage = 0
	End 
	Else If @RetarificSettlementType = 3 Begin --IncluidoOtroServicio
		--No se recalcula
		Update @distributionToRetarific Set SubTotalSalesPrice_1 = 0
			, ThirdPartyDiscount_1 = 0
			, ThirdPartyDiscountPercentage = 0
			, TotalSalesPrice = 0
			, GrandTotalSalesPrice_1 = 0
			, GrandTotalSalesPrice = 0
			, GrandTotalDiscount = 0
			, ThirdPartySalesPrice = 0
		--Si es quirurgico en los detalles quirurgicos se establecen a cero los valores

		If (Select Count(1) From @ServiceOrderDetailSurgical) > 0
			Update @ServiceOrderDetailSurgical Set TotalSalesPrice = 0
	End

	Declare @ServOrderDetailRetarificXml Xml = (
		Select * From @distributionToRetarific For Xml Path('ServiceOrderDetail'), Elements
	)
	Declare @ServOrderDetailSurgicalRetarificXml Xml = (
		Select * From @ServiceOrderDetailSurgical For Xml Path('ServiceOrderDetailSurgical'), Elements
	)

	Insert Into @ServiceOrderDetail
	Values(1, '', @ServOrderDetailRetarificXml, @ServOrderDetailSurgicalRetarificXml)
	
	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tarificación de servicios de salud en el módulo de contratos. Recibe en formato XML los detalles de órdenes de servicio y su distribución de costos, y aplica las reglas tarifarias del manual de tarifas (ISS, SOAT, particulares, etc.) para calcular y retornar los valores actualizados de cada ítem de orden de servicio, incluyendo precio de venta, descuentos a terceros, cuotas de recuperación, impuestos y redondeo. Consulta el catálogo CUPS para identificar el concepto RIPS de cada procedimiento y el manual tarifario para obtener la regla de redondeo aplicable, componiendo así la valorización final de servicios médicos facturables según el contrato vigente. Se usa en los procesos de liquidación, retarificación y ajuste de valores en órdenes de servicio antes de la facturación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'SetServiceValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'SetServiceValue';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recalcula y retarifica los valores de venta de un detalle de orden de servicio (incluyendo desglose quirúrgico) según el tipo de liquidación, aplicando redondeos, descuentos a terceros e impuestos.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'SetServiceValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de DistributionToRetarific debe contener un único nodo del cual se obtiene ServiceOrderDetailId, RateManualId, Presentation, SettlementType, etc.; Para liquidación tipo ''PorcentajeOtroServicio'' (SettlementType=2), el IncludeServiceOrderDetailId debe corresponder a un detalle existente en Billing.ServiceOrderDetail; Si RateManualId es válido (>0) debe existir el registro en Contract.RateManual para obtener RoundService; Para presentación quirúrgica (Presentation=2), los XML de detalles quirúrgicos deben venir poblados', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'SetServiceValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @ServiceOrderDetail: Siempre retorna StatusResult=1 con los XML recalculados de ServiceOrderDetail y ServiceOrderDetailSurgical; [UPDATE] @distributionToRetarific: Cuando SettlementType=1 (Manual de tarifas): SubTotalSalesPrice_1 = GrossValue + TaxValue; ThirdPartyDiscount_1 = (GrossValue+TaxValue) * ThirdPartyDiscountPercentage/100; [UPDATE] @distributionToRetarific: Cuando SettlementType=2 (PorcentajeOtroServicio): SubTotalSalesPrice_1 = Round(SalesPrice * RecoveryRatio / 100, 0), tomando SalesPrice de Billing.SetValueSalesPrice con el subtotal y el % IVA del servicio incluido; [UPDATE] @distributionToRetarific: Cuando SettlementType=3 (IncluidoOtroServicio): se ponen en cero SubTotalSalesPrice_1, ThirdPartyDiscount_1, ThirdPartyDiscountPercentage, TotalSalesPrice, GrandTotalSalesPrice_1, GrandTotalSalesPrice, GrandTotalDiscount y ThirdPartySalesPrice; [UPDATE] @ServiceOrderDetailSurgical: Cuando SettlementType=3 y existen detalles quirúrgicos, TotalSalesPrice se establece en 0; [UPDATE] @distributionToRetarific: Si (SubTotalSalesPrice_1 - ThirdPartyDiscount_1) < 100, TotalSalesPrice se asigna sin redondear; en caso contrario se redondea con Billing.RoundValue usando @round; [UPDATE] @distributionToRetarific: Si (SubTotalSalesPrice_1 - ThirdPartyDiscount_1) * Quantity < 100, GrandTotalSalesPrice_1 se redondea con factor 0 y RoundService=1; de lo contrario se redondea con @round; [UPDATE] @distributionToRetarific: Tras el cálculo principal, ThirdPartyPercentage se fija en 100, SubTotalPatientSalesPrice=0 y PatientPercentage=0 (la totalidad se asigna al tercero pagador); [UPDATE] @distributionToRetarific: Antes del cálculo, sobreescribe CareGroupId con el parámetro recibido, fija ApplyRecoveryFee=1, RecoveryFeeType=1, LastCaregroupId=CareGroupId y CodeAssociateService con el GuidHomologation; [UPDATE] @serviceOrderDetailSurgicalResult: Por cada fila quirúrgica, según ServiceClass del IPSService (1..7) se reasigna el profesional (PerformsHealthProfessionalThirdPartyId/Code) al rol correspondiente: Ninguno, Cirujano, Anestesiólogo, Ayudante, Derecho Sala, Materiales Sutura o Instrumentación Quirúrgica', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'SetServiceValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RateManualId no nulo y > 0 → Obtiene RoundService del manual; si RIPSConcept del CUPS está en (''12'',''13'') fuerza @round=1, si no usa ISNULL(RoundService,1) else @round permanece en 0; si Presentation del XML retarificado = 2 (quirúrgico) → Procesa detalles quirúrgicos: itera por cada fila y reasigna profesional según ServiceClass del IPSService, luego reemplaza @ServiceOrderDetailSurgical con el resultado else No procesa lógica quirúrgica; si SettlementType = 1 (Manual de tarifas) → Calcula valores sumando GrossValue + TaxValue y aplica descuento de tercero por porcentaje; si SettlementType = 2 (PorcentajeOtroServicio) → Recalcula SalesPrice usando Billing.SetValueSalesPrice sobre el SubTotalSalesPrice y % IVA del servicio incluido, y aplica RecoveryRatio/100; si SettlementType = 2 (PorcentajeMismoServicio - rama inalcanzable por duplicado) → Calcula SubTotalSalesPrice_1 = Round(SubTotalSalesPrice * RecoveryRatio, 0) — esta rama nunca se ejecuta porque la condición previa SettlementType=2 ya consume el flujo; si SettlementType = 3 (IncluidoOtroServicio) → No recalcula: pone todos los valores monetarios en cero, tanto en distribución como en detalles quirúrgicos; si ServiceClass del IPSService quirúrgico → Selecciona la fila de @serviceOrderDetailSurgicalResult cuya ClassServiceIps coincide con uno de: Ninguno(1), Cirujano(2), Anestesiólogo(3), Ayudante(4), Derecho Sala(5), Materiales Sutura(6), Instrumentación Quirúrgica(7) y le asigna el profesional', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'SetServiceValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'SetServiceValue';
GO
