
CREATE Function [Contract].[GetServiceValue_Institucional]
(
	@AdmissionNumber CHAR(10),
	@CenterAttentionCode VARCHAR(20),
	@CupsEntityId Int,
	@IPSServiceId Int,
	@CareGroupId Int,
	@FunctionalUnitId Int,
	@SpecialtyId Char(3),
	@ServiceDate DateTime,
	@PatientGenus Int,
	@PatientDateBirth DateTime,
	@InvoicedQuantity Int,
	@ProfessionalHealthCode Varchar(20),
	@ProfessionalHealthThirdPartyId Int Null,
	@RiasId int = 0,
	@ContractDescriptionId int = 0
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
	CostValue Decimal(20, 2),
	RecordType Tinyint,
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
	
	--variables de la entidad
	Insert Into @ServiceOrderDetail
	Values (1,'',0,0,0,'','','','',0,Null,Null,0,Null,0,Null,Null,0,0,0,Null,0,0,0,0,Null,Null,0,Common.GETDATE(),0,'','',Null,0,Null,Null,Null,Null,0,1,0,0,null)

	
	Declare @BillingConceptId INT,
			@ObtainCostCenter Tinyint,
			@costCenterFunctionalunit Int = 0,
			@idCostCenter Int = 0,
			@billingConceptCostCenter Int,
			@CodeNameFunctionalUnit Varchar(100) = Null,
			@CodeNameCostCenter Varchar(20),
			@ServiceType Tinyint,
			@CupsEntityCodeName Varchar(300),
			@CupsEntityBillingConceptId Int,
			@RIPSConcept char(2)

	Declare @MinimunAgeUnit Tinyint,
			@InMale Bit,
			@InFemale Bit,
			@IpsServiceCode Varchar(20),
			@MinimunAge Int,
			@MaximumAgeUnit Tinyint,
			@MaximumAge Int,
			@IPSServiceName Varchar(300),
			@IPSPresentation Tinyint,
			@UVRNumber Int,
			@SurgicalGroupId Int,
			@ServiceManual Tinyint,
			@errorIps Varchar(255) = '',
			@TaxedProduct BIT,
			@IVAPercent NUMERIC(5,2),
			@IVAId INT

	Select	@BillingConceptId = bc.Id
			, @ObtainCostCenter = bc.ObtainCostCenter 
			, @billingConceptCostCenter = bc.CostCenterId
			, @CupsEntityCodeName = Concat(ce.Code, ' - ', ce.[Description])
			, @CupsEntityBillingConceptId = ce.BillingConceptId
			, @RIPSConcept = ce.RIPSConcept
	From [Contract].CUPSEntity ce With(Nolock)
	LEFT join Contract.CUPSEntityContractDescriptions cecd WITH(NOLOCK) on ce.Id =cecd.CUPSEntityId and cecd.ContractDescriptionId = @ContractDescriptionId
	Inner Join [Billing].BillingConcept bc With(Nolock) On bc.Id =ISNULL(cecd.BillingConceptId,ce.BillingConceptId) 
	Where ce.Id = @CupsEntityId

	If @FunctionalUnitId Is Not Null And @FunctionalUnitId > 0 Begin		
		Select @CodeNameFunctionalUnit = Concat(Code, ' - ', [Name])
			, @costCenterFunctionalunit = CostCenterId
		From Payroll.FunctionalUnit With(Nolock) Where Id = @FunctionalUnitId		
		Update @ServiceOrderDetail Set CodeNameFunctionalUnit = @CodeNameFunctionalUnit
	End

	IF @CenterAttentionCode <> (SELECT ing.CODCENATE FROM dbo.ADINGRESO ing WHERE ing.NUMINGRES = @AdmissionNumber)
		BEGIN
			SET @errorIps = 'El ingreso ' + ISNULL(@AdmissionNumber, '') +' se realizo desde otra unidad funcional'
			Update @ServiceOrderDetail Set StatusResult = 0, MessageResult = @errorIps
			Return
	END

	--Define parametro de compañia para saber si es iva incluido o no
	DECLARE @SalePriceIncludeTax BIT
	SET @SalePriceIncludeTax =(SELECT TOP 1 SalePriceIncludeTax FROM GeneralLedger.CompanySettings)

	IF @ObtainCostCenter = 1
	BEGIN
		SET @idCostCenter = @costCenterFunctionalunit
	END
	ELSE IF @ObtainCostCenter = 2
	BEGIN
		SET @idCostCenter = @billingConceptCostCenter
	END
	ELSE IF @ObtainCostCenter = 3
	BEGIN
		SELECT @CenterAttentionCode = ing.CODCENATE
		FROM dbo.ADINGRESO ing
		WHERE ing.NUMINGRES = @AdmissionNumber

		IF NOT EXISTS
		(
			SELECT 1
			FROM Billing.BillingConceptCostCenter bccc
			JOIN Payroll.BranchOffice bo ON bccc.BranchOfficeId = bo.Id
			WHERE bccc.BillingConceptId = @BillingConceptId
				AND bo.Code = ISNULL(@CenterAttentionCode, '') AND bccc.FunctionalUnitId = ISNULL(@FunctionalUnitId, 0)
		)
		BEGIN
			SET @errorIps = 'No se encontró un centro de costo asociado a la sucursal ' + ISNULL(@CenterAttentionCode, '') + ' y Unidad Funcional ' + ISNULL(@CodeNameFunctionalUnit, '')+ ' - CUPS : ' + ISNULL(@CupsEntityCodeName,'')
		END
		ELSE
		BEGIN
			SELECT @idCostCenter = bccc.CostCenterId
			FROM Billing.BillingConceptCostCenter bccc
			JOIN Payroll.BranchOffice bo ON bccc.BranchOfficeId = bo.Id
			WHERE bccc.BillingConceptId = @BillingConceptId
				AND bo.Code = ISNULL(@CenterAttentionCode, '') AND bccc.FunctionalUnitId = ISNULL(@FunctionalUnitId, 0)
		END
	END

	Select @CodeNameCostCenter = Concat(Code, ' - ', [Name]) 
	From Payroll.CostCenter With(Nolock) Where Id = @idCostCenter
	
	Select @ServiceType = ips.ServiceType
			, @MinimunAgeUnit = ips.MinimunAgeUnit
			, @InMale = ips.InMale
			, @InFemale = ips.InFemale
			, @IpsServiceCode = ips.Code
			, @MinimunAge = ips.MinimunAge
			, @MaximumAgeUnit = ips.MaximumAgeUnit
			, @MaximumAge = ips.MaximumAge
			, @IPSServiceName = ips.[Name]
			, @IPSPresentation = ips.Presentation
			, @UVRNumber = ips.UVRNumber
			, @SurgicalGroupId = ips.SurgicalGroupId
			, @ServiceManual = ips.ServiceManual
			, @IVAPercent = iif(ips.TaxedProduct=1,ISNULL(iva.Percentage,0),0)
			, @IVAId = iif(ips.TaxedProduct=1,iva.Id,null)
	From [Contract].IPSService ips With(Nolock) 
	LEFT JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) on ips.IVAId= iva.Id
	Where ips.Id = @IPSServiceId
	
	If @PatientGenus = 1 And @InMale = 0
		Set @errorIps += 'El servicio IPS ' + @IpsServiceCode + ' es solo para el genero femenino'

	If @PatientGenus = 2 And @InFemale = 0
		Set @errorIps += Char(13) + Char(10) + 'El servicio IPS ' + @IpsServiceCode + ' es solo para el genero masculino'

	Declare @agePatient Int,
		@minimunAgeIPS Int = 0,
		@maximunAgeIPS Int = 0,
		@PatientDateBirthReference DateTime = @PatientDateBirth

	--Se hace para ajustar el error de que al sacar la diferencia en dias no toma la hora para saber si es dia cumplido o no
	If (Select Cast(@PatientDateBirthReference As Time)) > (Select Cast(Common.GETDATE() As Time))
		Set @PatientDateBirthReference = DATEADD(Day, 1, @PatientDateBirthReference)
		
	Select @agePatient = DateDiff(Day, @PatientDateBirthReference, Common.GETDATE())

	If @MinimunAgeUnit = 1 --Años
		Set @minimunAgeIPS = @MinimunAge * 360
	Else If @MinimunAgeUnit = 2 --Meses
		Set @minimunAgeIPS = @MinimunAge * 30
	Else If @MinimunAgeUnit = 3 --Dias
		Set @minimunAgeIPS = @MinimunAge

	If @MaximumAgeUnit = 1 --Años
		Set @maximunAgeIPS = @MaximumAge * 360
	Else If @MaximumAgeUnit = 2 --Meses
		Set @maximunAgeIPS = @MaximumAge * 30
	Else If @MaximumAgeUnit = 3 --Dias
		Set @maximunAgeIPS = @MaximumAge

	If @agePatient < @minimunAgeIPS
		Set @errorIps += Char(13) + Char(10) + 'El paciente tiene una edad menor a la edad mínima para el servicio IPS ' + @IpsServiceCode
	If @agePatient > @maximunAgeIPS
		Set @errorIps += Char(13) + Char(10) + 'El paciente tiene una edad mayor a la edad máxima para el servicio IPS ' + @IpsServiceCode
	
	Update @ServiceOrderDetail Set CostCenterId = @idCostCenter
		, CodeNameCostCenter = @CodeNameCostCenter
		, ServiceType = @ServiceType
		, CodeNameIpsService = Concat(@IpsServiceCode, ' - ', @IPSServiceName)
		, CodeNameCups = @CupsEntityCodeName

	If @errorIps Is Not Null And @errorIps <>  '' Begin
		Update @ServiceOrderDetail Set StatusResult = 0, MessageResult = @errorIps
		Return
	End

	Declare @rateVariation Decimal(18, 2) = 0 --Solo se llena si la tarifa es estandar
		, @ratemanualType Tinyint --Solo se llena si la tarifa es fija		
		, @rateManualValidityId INT
		, @rateManualValidityCodeName Varchar(200)
		, @rateManualId INT
		, @rateManualCodeName Varchar(200)
		, @rateManualRoundService Int
		, @liquidationType Tinyint = 0
		, @manualType Int = 0 --Solo se llena si la tarifa es fija
		, @salesValue Decimal(20, 2) = 0 --Solo se llena si la tarifa es fija
		, @salesValueWithSurcharge Decimal(20, 2) --Solo se llena si la tarifa es fija
		, @GrossValue NUMERIC(20,2) --valor bruto antes de Iva
		, @TaxValue NUMERIC(20,2) -- valor del IVA
			
	Declare @_StateResult Bit,
		@_MessageResult Varchar(255),
		@_ConditionType Tinyint,	
		@_LogicalOperator Tinyint,
		@_ConditionType2 Tinyint,
		@_DefinitionRateDetailId Int,
		@_LiquidationType Tinyint,
		@_ManualType Tinyint,
		@_RateManualValidityId Int,
		@_RateManualId Int,
		@_AllowValueChange Bit,
		@_RateVariation Decimal(5, 2),
		@_SalesValue Decimal(18, 2),
		@_SalesValueWithSurcharge Decimal(18, 2),
		@_DefinitionRateDetailConditionId Int,	
		@_LiquidateionTypeAux Tinyint,
		@_ManualTypeAux Tinyint,
		@_RateManualValidityIdAux Int,
		@_RateManualIdAux Int,
		@_RateVariationAux Decimal(5, 2),
		@_SalesValueAux Decimal(18, 2),
		@_SalesValueWithSurchargeAux Decimal(18, 2)
	
	Select @_StateResult = StateResult
		, @_MessageResult = MessageResult
		, @_ConditionType = ConditionType
		, @_LogicalOperator = LogicalOperator
		, @_ConditionType2 = ConditionType2
		, @_DefinitionRateDetailId = DefinitionRateDetailId
		, @_LiquidationType = LiquidateionType
		, @_ManualType = ManualType
		, @_RateManualValidityId = RateManualValidityId
		, @_RateManualId = RateManualId
		, @_AllowValueChange = AllowValueChange
		, @_RateVariation = RateVariation
		, @_SalesValue = SalesValue
		, @_SalesValueWithSurcharge = SalesValueWithSurcharge
		, @_DefinitionRateDetailConditionId = DefinitionRateDetailConditionId
		, @_LiquidateionTypeAux = LiquidateionTypeAux
		, @_ManualTypeAux = ManualTypeAux
		, @_RateManualValidityIdAux = RateManualValidityIdAux
		, @_RateManualIdAux = RateManualIdAux
		, @_RateVariationAux = RateVariationAux
		, @_SalesValueAux = SalesValueAux
		, @_SalesValueWithSurchargeAux = SalesValueWithSurchargeAux
	From [Contract].GetRateValue(@CupsEntityId, @CareGroupId, @FunctionalUnitId, @SpecialtyId, @ServiceDate, 0, @RiasId, @ContractDescriptionId, @IPSServiceId)
	
	declare @InitialLiquidationType tinyint = @_LiquidationType
	declare @InitialRateManual tinyint = @_RateManualId

	If @_StateResult = 1 Begin	
		If @_DefinitionRateDetailConditionId Is Not Null And @_DefinitionRateDetailConditionId > 0 Begin
			--Fue por que la tarifa se saco de una condicion
			Update @ServiceOrderDetail Set AllowValueChange = @_AllowValueChange, DefinitionRateDetailConditionId = @_DefinitionRateDetailConditionId			

			If @_ConditionType = 2 Or @_ConditionType2 = 2 --Especialidad
				Update @ServiceOrderDetail Set LiquidationType = 2
			Else
				Update @ServiceOrderDetail Set LiquidationType = @_ConditionType

			Set @liquidationType = @_LiquidateionTypeAux
			IF @liquidationType = 1 Begin --Si es Fija
				Set @manualType = @_ManualTypeAux
				Set @salesValue = @_SalesValueAux
				Set @salesValueWithSurcharge = @_SalesValueWithSurchargeAux

				Select @ratemanualType = COALESCE(rm.[Type], @_ManualTypeAux, 1)
				From Contract.DefinitionRateDetailCondition drdc WITH (NOLOCK)
				LEFT JOIN [Contract].RateManual rm With(Nolock) ON drdc.RateManualId = rm.Id
				Where drdc.Id = @_DefinitionRateDetailConditionId
			END
			ELSE If @liquidationType = 2 Begin --Si es estandar
				Set @rateVariation = @_RateVariationAux
				Select @rateManualId = Id, @ratemanualType = [Type]
					, @rateManualRoundService = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(RoundService, 1))
					, @rateManualCodeName = Concat(Code, ' - ', [Name])
				From [Contract].RateManual With(Nolock) 
				Where Id = @_RateManualIdAux
			END
			ELSE If @liquidationType = 3 Begin --Si es por vigencia
				Set @rateVariation = @_RateVariationAux
				Select @rateManualId = rm.Id, @ratemanualType = rm.[Type]
					, @rateManualRoundService = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(rm.RoundService, 1))
					, @rateManualCodeName = Concat(rm.Code, ' - ', rm.[Name])
					, @rateManualValidityCodeName = Concat(rmv.Code, ' - ', rmv.[Name])
				From [Contract].RateManualValidity rmv With(Nolock) 
				LEFT JOIN Contract.RateManualValidityDetail rmvd WITH (NOLOCK) ON rmv.Id = rmvd.RateManualValidityId AND @ServiceDate BETWEEN rmvd.InitialDate AND rmvd.EndDate
				LEFT JOIN Contract.RateManual rm WITH (NOLOCK) ON rmvd.RateManualId = rm.Id
				Where rmv.Id = @_RateManualValidityIdAux

				If @rateManualId IS NULL Begin					
					Update @ServiceOrderDetail Set StatusResult = 0, MessageResult = 'El servicio IPS ' + Concat(@IpsServiceCode, ' - ', @IpsServiceName, ' no tiene parametrizado un manual de tarifas para la vigencia ', @rateManualValidityCodeName, ' en la fecha del servicio ', @ServiceDate)
					Return
				End
			End
		End
		Else Begin
			--Es por que la condicion fue ninguna y la tarifa esta en la cabecera
			--asigno la bandera para saber si en presentacion puedo cambiar el valor del servicio o no
			Update @ServiceOrderDetail Set AllowValueChange = @_AllowValueChange
				, DefinitionRateDetailId = @_DefinitionRateDetailId
				, LiquidationType = @_ConditionType
			Set @liquidationType = @_LiquidationType

			IF @liquidationType = 1 Begin --Si es Fija
				Set @manualType = @_ManualType
				Set @salesValue = @_SalesValue
				Set @salesValueWithSurcharge = @_SalesValueWithSurcharge

				Select @ratemanualType = COALESCE(rm.[Type], @_ManualTypeAux, 1)
				From Contract.DefinitionRateDetailCondition drdc WITH (NOLOCK)
				LEFT JOIN [Contract].RateManual rm With(Nolock) ON drdc.RateManualId = rm.Id
				Where drdc.Id = @_DefinitionRateDetailConditionId
			END
			else If @liquidationType = 2 Begin --Si es estandar
				Set @rateVariation = @_RateVariation
				Select @rateManualId = Id, @ratemanualType = [Type]
					, @rateManualRoundService = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(RoundService, 1))
					, @rateManualCodeName = Concat(Code, ' - ', [Name])
				From [Contract].RateManual With(Nolock) 
				Where Id = @_RateManualId
			END
			else If @liquidationType = 3 Begin --Si es por vigencia
				Set @rateVariation = @_RateVariation
				Select @rateManualId = rm.Id, @ratemanualType = rm.[Type]
					, @rateManualRoundService = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(rm.RoundService, 1))
					, @rateManualCodeName = Concat(rm.Code, ' - ', rm.[Name])
					, @rateManualValidityCodeName = Concat(rmv.Code, ' - ', rmv.[Name])
				From [Contract].RateManualValidity rmv With(Nolock) 
				LEFT JOIN Contract.RateManualValidityDetail rmvd WITH (NOLOCK) ON rmv.Id = rmvd.RateManualValidityId AND @ServiceDate BETWEEN rmvd.InitialDate AND rmvd.EndDate
				LEFT JOIN Contract.RateManual rm WITH (NOLOCK) ON rmvd.RateManualId = rm.Id
				Where rmv.Id = @_RateManualValidityId

				If @rateManualId IS NULL Begin					
					Update @ServiceOrderDetail Set StatusResult = 0, MessageResult = 'El servicio IPS ' + Concat(@IpsServiceCode, ' - ', @IpsServiceName, ' no tiene parametrizado un manual de tarifas para la vigencia ', @rateManualValidityCodeName, ' en la fecha del servicio ', @ServiceDate)
					Return
				End
			End
		End
	End
	Else Begin
		Update @ServiceOrderDetail Set StatusResult = 0, MessageResult = @_MessageResult
		Return
	End

	If @liquidationType = 1 and @IPSPresentation <> 2
	BEGIN	
			
		Update @ServiceOrderDetail Set 
				  RateManualSalePrice =  @salesValue
				, GrossValue = vp.GrossValue
				, TaxValue = vp.TaxValue
				, IvaId = @IVAId
				, TotalSalesPrice = vp.SalesPrice
				, SubTotalSalesPrice = vp.SalesPrice
				, PerformsHealthProfessionalThirdPartyId = @ProfessionalHealthThirdPartyId
				, PerformsHealthProfessionalCode = @ProfessionalHealthCode
				, RateManualType = @manualType
				, GrandTotalSalesPrice = (vp.GrossValue + (vp.GrossValue *(@IVAPercent/100))) * @InvoicedQuantity
			from [Billing].[SetValueSalesPrice] (@SalePriceIncludeTax,@salesValue,@IVAPercent) vp
	end
	else begin
		--si la liquidacion es Estandar o por vigencia
		Update @ServiceOrderDetail Set Presentation = @IPSPresentation
		If @IPSPresentation = 2 Begin
			--si el servicio ips es quirurgico
			--obtengo los valores por defecto del ips quirurgico

			Declare @listDetailSurgicalDefault Table(IPSServiceId Int
				, ServiceAmount Int
				, ClassService Varchar(20))

			Declare @errorSurgical Varchar(Max) = ''
				, @errorTotalSurgical Varchar(Max) = ''
				, @IPSServiceIdSurgical Int
				, @ServiceAmountSurgical Int
				, @ClassServiceSurgical Varchar(30)
				
			Insert Into @listDetailSurgicalDefault
			Select sps.IPSServiceId
				, sps.ServiceAmount
				, (Case ips.ServiceClass When 1 Then 'Ninguno' When 2 Then 'Cirujano' When 3 Then 'Anestesiólogo' When 4 Then 'Ayudante' When 5 Then 'Derecho Sala' When 6 Then 'Materiales Sutura' When 7 Then 'Instrumentación Quirúrgica' Else '' End)
			From [Contract].SurgicalProcedureService sps With(Nolock)
			Inner Join [Contract].IPSService ips With(Nolock) On sps.IPSServiceId = ips.Id
			Where sps.IPSServiceParentId = @IPSServiceId And DefaultService = 1 Order By ips.ServiceClass Asc

			--Campos para serviceOrderDetailSurgical a insertar
			Declare @serviceOrderDetailSurgical Table(RowId Int Primary Key IDENTITY(1,1) NOT NULL,
				CodeNameIpsService Varchar(320),
				IPSServiceId Int,
				InvoicedQuantity Int,
				LiquidationPercentage Decimal(5, 2),
				TotalSalesPrice Decimal(18, 2),
				ClassServiceIps Varchar(30),
				RateManualSalePrice Decimal(18, 2),
				PerformsHealthProfessionalCode Char(20) Null,
				PerformsHealthProfessionalThirdPartyId Int Null,
				CostValue Decimal(18, 2),
				BillingConceptId Int,
				CostCenterId Int,
				RateManualDetailSurgicalId Int Null,
				SurchargeApply Bit,
				IncomeMainAccountId Int Null,
				RoundService int,
				AllowValueChange Bit)

			Delete From @serviceOrderDetailSurgical

			--Se obtiene el id de la definción de tarifa
			declare @DefinitionRateId int = (select DefinitionRateId from Contract.DefinitionRateDetail with(nolock) where Id = @_DefinitionRateDetailId)

			Declare cursor_detailSurgical Cursor For
			Select IPSServiceId, ServiceAmount, ClassService
			From @listDetailSurgicalDefault
			Open cursor_detailSurgical  
  
			Fetch Next From cursor_detailSurgical Into @IPSServiceIdSurgical, @ServiceAmountSurgical, @ClassServiceSurgical
			While @@Fetch_Status = 0  
			Begin  
				--consulto el manual de tarifas quirurgico
				Declare @rateManualDetailSurgicalId Int
					, @rateManualDetailSurgicalSalesValue Decimal(18, 2)
					, @ServiceClassTmp Tinyint
					, @NewScoreTmp Decimal(18, 2)
					, @ApplyChangeScoreTmp Bit
					, @ScoreTmp Decimal(18, 2)
					, @CodeNameTmp Varchar(320)
					, @BillingConceptIdTmp Int
					, @costValueItem Int = 0
					, @totalSalesPriceItem Decimal(18, 2)
					, @LiquidateionTypeAux tinyint
					, @SalesValueAux decimal(18, 2)
					, @SalesValueWithSurchargeAux decimal(18, 2)
					, @ratemanualTypeSurgical tinyint = @ratemanualType
					, @rateManualSurgicalId int = @rateManualId
					, @currentRateVariationSurgical decimal(18, 2) = @rateVariation
					, @rateManualRoundServiceSurgical int = @rateManualRoundService
					, @applyRateManual Bit = 1
				
				Select @ServiceClassTmp = ServiceClass, @NewScoreTmp = NewScore, @ApplyChangeScoreTmp = ApplyChangeScore
					, @ScoreTmp = Score, @CodeNameTmp = Concat(Code, ' - ', [Name]), @BillingConceptIdTmp = BillingConceptId
				From [Contract].IPSService With(Nolock) Where Id = @IPSServiceIdSurgical
				
				Declare @_StateResult_S Bit,
					@_MessageResult_S Varchar(255),
					@_LiquidationType_S Tinyint,
					@_ManualType_S Tinyint,
					@_RateManualValidityId_S Int,
					@_RateManualId_S Int,
					@_RateVariation_S Decimal(5, 2),
					@_SalesValue_S Decimal(18, 2),
					@_SalesValueWithSurcharge_S Decimal(18, 2),
					@_DefinitionRateDetailConditionId_S Int,	
					@_LiquidateionTypeAux_S Tinyint,
					@_ManualTypeAux_S Tinyint,
					@_RateManualValidityIdAux_S Int,
					@_RateManualIdAux_S Int,
					@_RateVariationAux_S Decimal(5, 2),
					@_SalesValueAux_S Decimal(18, 2),
					@_SalesValueWithSurchargeAux_S Decimal(18, 2),
					@_CostCenterIdQX INT =NULL,
					@_ObtainCostCenterQX INT=NULL,
					@_costCenterFunctionalunitQX INT =NULL,
					@_BillingConceptIdQx INT=NULL
	
				Select @_StateResult_S = StateResult
					, @_MessageResult_S = MessageResult
					, @_LiquidationType_S = LiquidateionType
					, @_ManualType_S = ManualType
					, @_RateManualValidityId_S = RateManualValidityId
					, @_RateManualId_S = RateManualId
					, @_RateVariation_S = RateVariation
					, @_SalesValue_S = SalesValue
					, @_SalesValueWithSurcharge_S = SalesValueWithSurcharge
					, @_DefinitionRateDetailConditionId_S = DefinitionRateDetailConditionId
					, @_LiquidateionTypeAux_S = LiquidateionTypeAux
					, @_ManualTypeAux_S = ManualTypeAux
					, @_RateManualValidityIdAux_S = RateManualValidityIdAux
					, @_RateManualIdAux_S = RateManualIdAux
					, @_RateVariationAux_S = RateVariationAux
					, @_SalesValueAux_S = SalesValueAux
					, @_SalesValueWithSurchargeAux_S = SalesValueWithSurchargeAux
					, @_AllowValueChange = AllowValueChange
				From [Contract].GetRateValue(@CupsEntityId, @CareGroupId, @FunctionalUnitId, @SpecialtyId, @ServiceDate, 0, @RiasId, @ContractDescriptionId, @IPSServiceIdSurgical)	

				/**************** CENTRO DE COSTO POR DETALLE QX ***************************/
				
					SELECT  @_ObtainCostCenterQX =bc.ObtainCostCenter,
							@_CostCenterIdQX= bc.CostCenterId,
							@_BillingConceptIdQx = bc.Id
					From [Contract].IPSService i  With(Nolock)
					Inner Join [Billing].BillingConcept bc With(Nolock) On bc.Id =I.BillingConceptId 
					Where i.Id = @IPSServiceIdSurgical

					If @FunctionalUnitId Is Not Null And @FunctionalUnitId > 0 Begin		
						SELECT @_costCenterFunctionalunitQX = CostCenterId
						From Payroll.FunctionalUnit With(Nolock) Where Id = @FunctionalUnitId		
					End

					IF @_ObtainCostCenterQX = 1
					BEGIN
						SET @_CostCenterIdQX = @_costCenterFunctionalunitQX
					END
					ELSE IF @_ObtainCostCenterQX = 3
					BEGIN
						IF NOT EXISTS
						(
							SELECT 1
							FROM Billing.BillingConceptCostCenter bccc  WITH(NOLOCK)
							JOIN Payroll.BranchOffice bo  WITH(NOLOCK) ON bccc.BranchOfficeId = bo.Id
							WHERE bccc.BillingConceptId = @_BillingConceptIdQx
								AND bo.Code = ISNULL(@CenterAttentionCode, '') AND bccc.FunctionalUnitId = ISNULL(@FunctionalUnitId, 0)
						)
						BEGIN
							Set @errorSurgical += Char(13) + Char(10) + CONCAT('No se encontró un centro de costo asociado a la sucursal ', ISNULL(@CenterAttentionCode, ''), ' y Unidad Funcional ', ISNULL(@CodeNameFunctionalUnit, ''), ' - IPS : ' , ISNULL(@CodeNameTmp,''))
							Fetch Next From cursor_detailSurgical Into @IPSServiceIdSurgical, @ServiceAmountSurgical, @ClassServiceSurgical
							Continue						
						END
						ELSE
						BEGIN
							SELECT @_CostCenterIdQX = bccc.CostCenterId
							FROM Billing.BillingConceptCostCenter bccc WITH(NOLOCK)
							JOIN Payroll.BranchOffice bo  WITH(NOLOCK) ON bccc.BranchOfficeId = bo.Id
							WHERE bccc.BillingConceptId = @_BillingConceptIdQx
								AND bo.Code = ISNULL(@CenterAttentionCode, '') AND bccc.FunctionalUnitId = ISNULL(@FunctionalUnitId, 0)
						END
					END
				/****************************************************************************************************/

				if @IPSServiceIdSurgical = 0 Or @_StateResult_S = 0 
				begin
					--Se valida si el servicio ips qx que se recorre esta parametrizado dentro de la tabla definitionRateDetailSurgicalProcedures
					declare @DefinitionRateDetailIdSurgical int = 0, @LiquidationTypeSurgical tinyint, @RateManualValidityIdSurgical int, @RateManualIdSurgical int, 
					@RateVariationSurgical decimal(5, 2), @SalesValueSurgical decimal(18, 2), @SalesValueWithSurchargeSurgical decimal(18, 2)
				
					select top 1	@DefinitionRateDetailIdSurgical = drd.Id, @LiquidationTypeSurgical = drd.LiquidationType, 
									@RateManualValidityIdSurgical = drd.RateManualValidityId, @RateManualIdSurgical = drd.RateManualId, 
									@RateVariationSurgical = drd.RateVariation, @SalesValueSurgical = drd.SalesValue, 
									@SalesValueWithSurchargeSurgical = drd.SalesValueWithSurcharge, @_AllowValueChange = drd.AllowValueChange
					from Contract.DefinitionRateDetail drd with(nolock)
					inner join Contract.DefinitionRateDetailSurgicalProcedures drdsp with(nolock) on drdsp.DefinitionRateDetailId = drd.Id
					where drd.DefinitionRateId = @DefinitionRateId and drd.IPSServiceId = @IPSServiceId and drd.CUPSEntityId = @CupsEntityId and drdsp.IPSServiceId = @IPSServiceIdSurgical
				
					--Se valida si el servicio ips qx que se recorre esta parametrizado dentro de la tabla definitionRateDetailSurgicalProcedures
					if @DefinitionRateDetailIdSurgical is not null and @DefinitionRateDetailIdSurgical > 0
					begin
						set @applyRateManual = 0
						declare @StateResultSurgical bit, @ManualTypeAux tinyint, @RateManualValidityIdAux int, @RateManualIdAux int, 
						@RateVariationAux decimal(5, 2)

						--Se obtiene la tarifa correspondiente al servicio qx que se va recorriendo
						select	@StateResultSurgical = StateResult, @LiquidateionTypeAux = LiquidateionTypeAux, @ManualTypeAux = ManualTypeAux, 
								@RateManualValidityIdAux = RateManualValidityIdAux, @RateManualIdAux = RateManualIdAux, 
								@RateVariationAux = RateVariationAux, @SalesValueAux = SalesValueAux, @SalesValueWithSurchargeAux = SalesValueWithSurchargeAux 
						from [Contract].[GetRateValueConditionSurgicalProcedures](@DefinitionRateDetailIdSurgical, @FunctionalUnitId, @SpecialtyId, @ServiceDate, @RiasId, @ContractDescriptionId)

						if ISNULL(@StateResultSurgical, 0) = 0 --Es por que la condicion fue ninguna y la tarifa esta en la cabecera
						begin
							set @LiquidateionTypeAux = @LiquidationTypeSurgical
							IF @LiquidateionTypeAux = 1 begin --Si es fija
								set @SalesValueAux = @SalesValueSurgical
								set @SalesValueWithSurchargeAux = @SalesValueWithSurchargeSurgical
							END
							ELSE if @LiquidateionTypeAux = 2 --Si es estandar
							begin
								set @RateManualIdAux = @RateManualIdSurgical
								set @RateVariationAux = @RateVariationSurgical
							END
							ELSE if @LiquidateionTypeAux = 3 --Si es por vigencia
							begin
								set @RateManualValidityIdAux = @RateManualValidityIdSurgical
								set @RateVariationAux = @RateVariationSurgical
							end
						end

						if @LiquidateionTypeAux = 1 begin --Si es fija
							set @totalSalesPriceItem = @SalesValueAux
							set @rateManualId = COALESCE(@rateManualId, @RateManualIdAux, @RateManualIdSurgical)
						end
						ELSE if @LiquidateionTypeAux IN (2, 3) --Si es estandar o por vigencia
						BEGIN
							if @LiquidateionTypeAux = 3 BEGIN --Si es por vigencia
								Set @rateVariation = @_RateVariationAux
								Select	@RateManualIdAux = rm.Id, @rateManualValidityCodeName = Concat(rmv.Code, ' - ', rmv.[Name])
								From [Contract].RateManualValidity rmv With(Nolock) 
								LEFT JOIN Contract.RateManualValidityDetail rmvd WITH (NOLOCK) ON rmv.Id = rmvd.RateManualValidityId AND @ServiceDate BETWEEN rmvd.InitialDate AND rmvd.EndDate
								LEFT JOIN Contract.RateManual rm WITH (NOLOCK) ON rmvd.RateManualId = rm.Id
								Where rmv.Id = @RateManualValidityIdAux

								If @RateManualIdAux IS NULL Begin					
									Set @errorSurgical += Char(13) + Char(10) + CONCAT('El servicio IPS ', @CodeNameTmp, ' no tiene parametrizado un manual de tarifas para la vigencia ', @rateManualValidityCodeName, ' en la fecha del servicio ', @ServiceDate)
									Fetch Next From cursor_detailSurgical Into @IPSServiceIdSurgical, @ServiceAmountSurgical, @ClassServiceSurgical
									Continue
								End
							end

							DECLARE @parametersXml as XMl = null
							Declare @resultTableISSLogic as table (StatusResult Bit,
																	MessageResult Varchar(Max),
																	costValueItem Decimal(20,2),
																	RateManualDetailSurgicalId INT)
							delete from @resultTableISSLogic

							If @ServiceManual <= 2 
							BEGIN
							
								SET @parametersXml = CONVERT(xml, (	SELECT *
																	FROM (	SELECT @RateManualIdAux AS RateManualId,
																			 @IPSServiceId	AS IPSServiceParentId,
																			 @IPSServiceIdSurgical AS IPSServiceChildId,
																			 @ServiceAmountSurgical AS ServiceAmount) AS LogicUVRRateManualISS
																	For xml AUTO,TYPE, ELEMENTS))

								INSERT INTO @resultTableISSLogic
								SELECT *
								FROM  [Contract].[LogicUVRRateManualISS] (@parametersXml)

								IF NOT EXISTS(SELECT 1 FROM @resultTableISSLogic) OR EXISTS(SELECT 1 FROM @resultTableISSLogic WHERE StatusResult =0) BEGIN
									Set @errorSurgical += Char(13) + Char(10) + 'El servicio IPS ' + @CodeNameTmp + ' no esta parametrizado en el manual de tarifas quirúrgico'
									Fetch Next From cursor_detailSurgical Into @IPSServiceIdSurgical, @ServiceAmountSurgical, @ClassServiceSurgical
									Continue
								END

								Select Top 1  @rateManualDetailSurgicalSalesValue = costValueItem
								From @resultTableISSLogic
							END
							ELSE 
							BEGIN
								Select Top 1 @rateManualDetailSurgicalId = rms.Id, @rateManualDetailSurgicalSalesValue = rms.SalesValue
								From [Contract].RateManualDetailSurgical rms With(Nolock)
								Left Join [Contract].UVRRange uvr With(Nolock) On uvr.Id = rms.UVRRangeId
								Where RateManualId = @RateManualIdAux 
									And IPSServiceId = @IPSServiceIdSurgical 
									And (@ServiceManual = 4 OR SurgicalGroupId = @SurgicalGroupId)
							END
							
							If (@rateManualDetailSurgicalId Is Null Or @rateManualDetailSurgicalId = 0 ) AND @ServiceManual > 2 Begin
								Set @errorSurgical += Char(13) + Char(10) + 'El servicio IPS ' + @CodeNameTmp + ' no esta parametrizado en el manual de tarifas quirúrgico'
								Fetch Next From cursor_detailSurgical Into @IPSServiceIdSurgical, @ServiceAmountSurgical, @ClassServiceSurgical
								Continue
							End

							set @totalSalesPriceItem = @rateManualDetailSurgicalSalesValue + (@rateManualDetailSurgicalSalesValue * (@RateVariationAux / 100))
							If @totalSalesPriceItem > @rateManualRoundService Begin
								SELECT	@totalSalesPriceItem = [Billing].[RoundValue](@totalSalesPriceItem, @rateManualRoundService)
							End
						END
					end
					else 
					begin
						--Se obtiene la tarifa con el papa para poder saber el tipo de manual y el id de la definición de tarifas

						Select @_StateResult = StateResult
							, @_MessageResult = MessageResult
							, @_LiquidationType = LiquidateionType
							, @_RateManualValidityId = RateManualValidityId
							, @_RateManualId = RateManualId
							, @_RateVariation = RateVariation
							, @_DefinitionRateDetailConditionId = DefinitionRateDetailConditionId
							, @_LiquidateionTypeAux = LiquidateionTypeAux
							, @_RateManualValidityIdAux = RateManualValidityIdAux
							, @_RateManualIdAux = RateManualIdAux
							, @_RateVariationAux = RateVariationAux
							, @_AllowValueChange = AllowValueChange
						From [Contract].GetRateValue(@CupsEntityId, @CareGroupId, @FunctionalUnitId, @SpecialtyId, @ServiceDate, 1, @RiasId, @ContractDescriptionId,@IPSServiceId) --0)
								   					 				  
						If @_StateResult = 1 Begin	
							
							if @_LiquidationType = 1 --Si el tipo de liquidación es fija
							begin
								Update @ServiceOrderDetail Set StatusResult = 0, MessageResult = 'El servicio IPS ' + Concat(@IpsServiceCode, ' - ', @IPSServiceName) + ' es quirurgico y no se puede liquidar con tarifa fija'
								Return
							END

							set @LiquidateionTypeAux = @_LiquidationType
							
							If @_DefinitionRateDetailConditionId Is Not Null And @_DefinitionRateDetailConditionId > 0 Begin	
								If @_LiquidateionTypeAux = 2 Begin --Si es estandar
									Set @rateVariation = @_RateVariationAux
									Set @currentRateVariationSurgical = @_RateVariationAux

									Select	@rateManualId = Id, 
											@rateManualSurgicalId = @rateManualId,
											@ratemanualTypeSurgical = [Type]
											,@rateManualRoundService = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(RoundService, 1))
											, @rateManualRoundServiceSurgical = @rateManualRoundService
											,@rateManualCodeName = Concat(Code, ' - ', [Name])
									From [Contract].RateManual With(Nolock) 
									Where Id = @_RateManualIdAux
								End
								else IF @_LiquidateionTypeAux = 3 BEGIN --Si es por vigencia
									Set @rateVariation = @_RateVariationAux
									Set @currentRateVariationSurgical = @_RateVariationAux

									Select @rateManualId = rm.Id, @ratemanualTypeSurgical = rm.[Type]
											, @rateManualSurgicalId = @rateManualId
											, @rateManualRoundService = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(rm.RoundService, 1))
											, @rateManualRoundServiceSurgical = @rateManualRoundService
											, @rateManualCodeName = Concat(rm.Code, ' - ', rm.[Name])
											, @rateManualValidityCodeName = Concat(rmv.Code, ' - ', rmv.[Name])
									From [Contract].RateManualValidity rmv With(Nolock) 
									LEFT JOIN Contract.RateManualValidityDetail rmvd WITH (NOLOCK) ON rmv.Id = rmvd.RateManualValidityId AND @ServiceDate BETWEEN rmvd.InitialDate AND rmvd.EndDate
									LEFT JOIN Contract.RateManual rm WITH (NOLOCK) ON rmvd.RateManualId = rm.Id
									Where rmv.Id = @_RateManualValidityIdAux

									If @rateManualId IS NULL Begin					
										Update @ServiceOrderDetail Set StatusResult = 0, MessageResult = 'El servicio IPS ' + Concat(@IpsServiceCode, ' - ', @IpsServiceName, ' no tiene parametrizado un manual de tarifas para la vigencia ', @rateManualValidityCodeName, ' para la fecha del servicio ', @ServiceDate)
										Return
									End
								end
							END
							ELSE BEGIN
								If @_LiquidationType = 2 Begin --Si es estandar

									Set @rateVariation = @_RateVariation
									Set @currentRateVariationSurgical = @_RateVariation

									Select @rateManualId = Id, 
											@ratemanualTypeSurgical = [Type]
										, @rateManualSurgicalId = @rateManualId
										, @rateManualRoundService = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(RoundService, 1))
										, @rateManualRoundServiceSurgical = @rateManualRoundService
										, @rateManualCodeName = Concat(Code, ' - ', [Name])
									From [Contract].RateManual With(Nolock) 
									Where Id = @_RateManualId

								end
								else If @_LiquidationType = 3 Begin --Si es por vigencia
								
									Set @rateVariation = @_RateVariation
									Set @currentRateVariationSurgical = @_RateVariation

									Select @rateManualId = rm.Id, @ratemanualTypeSurgical = rm.[Type]
										, @rateManualSurgicalId = @rateManualId
										, @rateManualRoundService = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(rm.RoundService, 1))
										, @rateManualRoundServiceSurgical = @rateManualRoundService
										, @rateManualCodeName = Concat(rm.Code, ' - ', rm.[Name])
										, @rateManualValidityCodeName = Concat(rmv.Code, ' - ', rmv.[Name])
									From [Contract].RateManualValidity rmv With(Nolock) 
									LEFT JOIN Contract.RateManualValidityDetail rmvd WITH (NOLOCK) ON rmv.Id = rmvd.RateManualValidityId AND @ServiceDate BETWEEN rmvd.InitialDate AND rmvd.EndDate
									LEFT JOIN Contract.RateManual rm WITH (NOLOCK) ON rmvd.RateManualId = rm.Id
									Where rmv.Id = @_RateManualValidityId

									If @rateManualId IS NULL Begin					
										Update @ServiceOrderDetail Set StatusResult = 0, MessageResult = 'El servicio IPS ' + Concat(@IpsServiceCode, ' - ', @IpsServiceName, ' no tiene parametrizado un manual de tarifas para la vigencia ', @rateManualValidityCodeName, ' en la fecha del servicio ', @ServiceDate)
										Return
									End
								END
							end
						End
						Else Begin
							Update @ServiceOrderDetail Set StatusResult = 0, MessageResult = @_MessageResult
							Return
						End
					end
				end
				else 
				begin
					If @_DefinitionRateDetailConditionId_S Is Not Null And @_DefinitionRateDetailConditionId_S > 0 
					Begin
						----Fue por que la tarifa se saco de una condicion	
						set @LiquidateionTypeAux = @_LiquidateionTypeAux_S					
						IF @_LiquidateionTypeAux_S = 1 Begin --Si es Fija
							Set @manualType = @_ManualTypeAux_S
							Set @salesValue = @_SalesValueAux_S
							Set @salesValueWithSurcharge = @_SalesValueWithSurchargeAux_S

							SET @SalesValueAux = @_SalesValueAux_S
							SET @SalesValueWithSurchargeAux = @_SalesValueWithSurchargeAux_S
						End
						ELSE If @_LiquidateionTypeAux_S = 2 Begin --Si es estandar
							Set @currentRateVariationSurgical = @_RateVariationAux_S
							Select @rateManualSurgicalId = Id, @ratemanualTypeSurgical = [Type]
								, @rateManualRoundServiceSurgical = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(RoundService, 1))
								--, @rateManualCodeName = Concat(Code, ' - ', [Name])
							From [Contract].RateManual With(Nolock) 
							Where Id = @_RateManualIdAux_S
						END
						ELSE If @_LiquidateionTypeAux_S = 3 Begin --Si es por vigencia
							Set @currentRateVariationSurgical = @_RateVariationAux_S
							Select @rateManualSurgicalId = rm.Id, @ratemanualTypeSurgical = rm.[Type]
								, @rateManualRoundServiceSurgical = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(rm.RoundService, 1))
								--, @rateManualCodeName = Concat(rm.Code, ' - ', rm.[Name])
								, @rateManualValidityCodeName = Concat(rmv.Code, ' - ', rmv.[Name])
							From [Contract].RateManualValidity rmv With(Nolock) 
							LEFT JOIN Contract.RateManualValidityDetail rmvd WITH (NOLOCK) ON rmv.Id = rmvd.RateManualValidityId AND @ServiceDate BETWEEN rmvd.InitialDate AND rmvd.EndDate
							LEFT JOIN Contract.RateManual rm WITH (NOLOCK) ON rmvd.RateManualId = rm.Id
							Where rmv.Id = @_RateManualValidityIdAux_S

							If @rateManualSurgicalId IS NULL Begin					
								Update @ServiceOrderDetail Set StatusResult = 0, MessageResult = 'El servicio IPS ' + Concat(@IpsServiceCode, ' - ', @IpsServiceName, ' no tiene parametrizado un manual de tarifas para la vigencia ', @rateManualValidityCodeName, ' en la fecha del servicio ', @ServiceDate)
								Return
							End
						End
					End
					Else 
					Begin
						--Es por que la condicion fue ninguna y la tarifa esta en la cabecera
						--asigno la bandera para saber si en presentacion puedo cambiar el valor del servicio o no						
						set @LiquidateionTypeAux = @_LiquidationType_S
						IF @_LiquidationType_S = 1 Begin --Si es Fija
							Set @manualType = @_ManualType_S
							Set @salesValue = @_SalesValue_S
							Set @salesValueWithSurcharge = @_SalesValueWithSurcharge_S

							SET @SalesValueAux = @salesValue
							SET @SalesValueWithSurchargeAux = @salesValueWithSurcharge
						END
						else If @_LiquidationType_S = 2 Begin --Si es estandar
							Set @currentRateVariationSurgical = @_RateVariation_S
							Select @rateManualSurgicalId = Id, @ratemanualTypeSurgical = [Type]
								, @rateManualRoundServiceSurgical = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(RoundService, 1))
								--, @rateManualCodeName = Concat(Code, ' - ', [Name])
							From [Contract].RateManual With(Nolock) 
							Where Id = @_RateManualId_S
						END
						else If @_LiquidationType_S = 3 Begin --Si es por vigencia
							Set @currentRateVariationSurgical = @_RateVariation_S
							Select @rateManualSurgicalId = rm.Id, @ratemanualTypeSurgical = rm.[Type]
								, @rateManualRoundServiceSurgical = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(rm.RoundService, 1))
								--, @rateManualCodeName = Concat(rm.Code, ' - ', rm.[Name])
								, @rateManualValidityCodeName = Concat(rmv.Code, ' - ', rmv.[Name])
							From [Contract].RateManualValidity rmv With(Nolock) 
							LEFT JOIN Contract.RateManualValidityDetail rmvd WITH (NOLOCK) ON rmv.Id = rmvd.RateManualValidityId AND @ServiceDate BETWEEN rmvd.InitialDate AND rmvd.EndDate
							LEFT JOIN Contract.RateManual rm WITH (NOLOCK) ON rmvd.RateManualId = rm.Id
							Where rmv.Id = @_RateManualValidityId_S

							If @rateManualSurgicalId IS NULL Begin					
								Update @ServiceOrderDetail Set StatusResult = 0, MessageResult = 'El servicio IPS ' + Concat(@IpsServiceCode, ' - ', @IpsServiceName, ' no tiene parametrizado un manual de tarifas para la vigencia ', @rateManualValidityCodeName, ' en la fecha del servicio ', @ServiceDate)
								Return
							End
						End
					End
				end

				if @applyRateManual = 1 begin

					IF @LiquidateionTypeAux = 1
					BEGIN
						SET @totalSalesPriceItem = @SalesValueAux
					END
					ELSE
					BEGIN
						--valido el tipo del manual de tarifas - Tipo del manual tarifario 1 - ISS 2001 , 2 - ISS 2004,  3 - SOAT
						If @ratemanualTypeSurgical < 3 Begin
							If @ServiceClassTmp = 2 Or @ServiceClassTmp = 3 Or @ServiceClassTmp = 4 Begin -- 2 - Cirujano,  3 - Anesteciologo, 4 - Ayudante
								If @UVRNumber > 450 And @ApplyChangeScoreTmp = 1
									Set @costValueItem = @UVRNumber * @NewScoreTmp * @ServiceAmountSurgical
								Else
									Set @costValueItem = @UVRNumber * @ScoreTmp * @ServiceAmountSurgical
							End
							Else Begin
								If @ServiceClassTmp = 5 And @UVRNumber > 450 And @ApplyChangeScoreTmp = 1 --5 - Derecho Sala
									Set @costValueItem = @UVRNumber * @NewScoreTmp * @ServiceAmountSurgical
								Else Begin
															
									If @ServiceManual <= 2 
										Select Top 1 @rateManualDetailSurgicalId = rms.Id, @rateManualDetailSurgicalSalesValue = rms.SalesValue
										From [Contract].RateManualDetailSurgical rms With(Nolock)
										Left Join [Contract].UVRRange uvr With(Nolock) On uvr.Id = rms.UVRRangeId
										Where RateManualId = @rateManualSurgicalId And IPSServiceId = @IPSServiceIdSurgical And @UVRNumber >= uvr.InitialUVR And @UVRNumber <= uvr.EndUVR
									Else
										Select Top 1 @rateManualDetailSurgicalId = rms.Id, @rateManualDetailSurgicalSalesValue = rms.SalesValue
										From [Contract].RateManualDetailSurgical rms With(Nolock)
										Left Join [Contract].UVRRange uvr With(Nolock) On uvr.Id = rms.UVRRangeId
										Where RateManualId = @rateManualSurgicalId 
											And IPSServiceId = @IPSServiceIdSurgical 
											And (@ServiceManual = 4 OR SurgicalGroupId = @SurgicalGroupId)

									If @rateManualDetailSurgicalId Is Null Or @rateManualDetailSurgicalId = 0 Begin
										Set @errorSurgical += Char(13) + Char(10) + 'El servicio IPS ' + @CodeNameTmp + ' no esta parametrizado en el manual de tarifas quirúrgico'
										Fetch Next From cursor_detailSurgical Into @IPSServiceIdSurgical, @ServiceAmountSurgical, @ClassServiceSurgical
										Continue
									End
									Set @costValueItem = @rateManualDetailSurgicalSalesValue * @ServiceAmountSurgical
								End
							End
						End
						Else Begin
						
							If @ServiceManual <= 2 
								Select Top 1 @rateManualDetailSurgicalId = rms.Id, @rateManualDetailSurgicalSalesValue = rms.SalesValue
								From [Contract].RateManualDetailSurgical rms With(Nolock)
								Left Join [Contract].UVRRange uvr With(Nolock) On uvr.Id = rms.UVRRangeId
								Where RateManualId = @rateManualSurgicalId And IPSServiceId = @IPSServiceIdSurgical And @UVRNumber >= uvr.InitialUVR And @UVRNumber <= uvr.EndUVR
							Else
								Select Top 1 @rateManualDetailSurgicalId = rms.Id, @rateManualDetailSurgicalSalesValue = rms.SalesValue
								From [Contract].RateManualDetailSurgical rms With(Nolock)
								Left Join [Contract].UVRRange uvr With(Nolock) On uvr.Id = rms.UVRRangeId
								Where RateManualId = @rateManualSurgicalId And IPSServiceId = @IPSServiceIdSurgical And SurgicalGroupId = @SurgicalGroupId

							If @rateManualDetailSurgicalId Is Null Or @rateManualDetailSurgicalId = 0 Begin
								Set @errorSurgical += Char(13) + Char(10) + 'El servicio IPS ' + @CodeNameTmp + ' no esta parametrizado en el manual de tarifas quirúrgico'
								Fetch Next From cursor_detailSurgical Into @IPSServiceIdSurgical, @ServiceAmountSurgical, @ClassServiceSurgical
								Continue
							End
							Set @costValueItem = @rateManualDetailSurgicalSalesValue * @ServiceAmountSurgical
							Update @ServiceOrderDetail Set IsSOAT = 1					
						End

						set @totalSalesPriceItem = @costValueItem + (@costValueItem * (@currentRateVariationSurgical / 100))
						If @totalSalesPriceItem > @rateManualRoundServiceSurgical Begin
							SELECT	@totalSalesPriceItem = [Billing].[RoundValue](@totalSalesPriceItem, @rateManualRoundServiceSurgical)
						End
					END
				end
						
				Update @ServiceOrderDetail Set RateManualType = @ratemanualType, RateManualId = @rateManualId, SubTotalSalesPrice += @totalSalesPriceItem
					, RateManualSalePrice += @totalSalesPriceItem, TotalSalesPrice += @totalSalesPriceItem, PerformsHealthProfessionalThirdPartyId = @ProfessionalHealthThirdPartyId
					, GrandTotalSalesPrice = ((TotalSalesPrice + @totalSalesPriceItem) * @InvoicedQuantity)
				
				
				--si el valor total es mayor al redondeo entonces se aplica
				If 	(Select Top 1 GrandTotalSalesPrice From @ServiceOrderDetail) > @rateManualRoundService Begin
					UPDATE @ServiceOrderDetail
						SET GrandTotalSalesPrice = [Billing].[RoundValue](GrandTotalSalesPrice, @rateManualRoundService),
							RoundService = @rateManualRoundService
				End
				
				If @BillingConceptIdTmp Is Null Or @BillingConceptIdTmp = 0 Begin
					Set @errorSurgical += Char(13) + Char(10) + 'El servicio IPS ' + @CodeNameTmp + ' no tiene asignado concepto de facturación'
					Fetch Next From cursor_detailSurgical Into @IPSServiceIdSurgical, @ServiceAmountSurgical, @ClassServiceSurgical
					Continue
				End
				
				Declare @PerformsHealthProfessionalCodeSurgical Char(20) = Null,
					@PerformsHealthProfessionalThirdPartyIdSurgical Int = Null,
					@RateManualDetailSurgicalIdSurgical Int = Null
				If @ClassServiceSurgical = 'Cirujano' Begin
					Set @PerformsHealthProfessionalCodeSurgical = @ProfessionalHealthCode
					Set @PerformsHealthProfessionalThirdPartyIdSurgical = @ProfessionalHealthThirdPartyId
				End
				
				If @rateManualDetailSurgicalId Is Not Null And @rateManualDetailSurgicalId > 0
					Set @RateManualDetailSurgicalIdSurgical = @rateManualDetailSurgicalId

				Insert Into @serviceOrderDetailSurgical
				Values (@CodeNameTmp, @IPSServiceIdSurgical, @ServiceAmountSurgical, 0, @totalSalesPriceItem, @ClassServiceSurgical
					, @totalSalesPriceItem, @PerformsHealthProfessionalCodeSurgical, @PerformsHealthProfessionalThirdPartyIdSurgical
					, (Select Top 1 CostValue From @ServiceOrderDetail), @BillingConceptIdTmp
					, @_CostCenterIdQX, @RateManualDetailSurgicalIdSurgical, 0, Null
					, IIF(@totalSalesPriceItem > @rateManualRoundService, @rateManualRoundService, 1)
					, ISNULL(@_AllowValueChange, 0))
									
				Fetch Next From cursor_detailSurgical Into @IPSServiceIdSurgical, @ServiceAmountSurgical, @ClassServiceSurgical
			End   
			Close cursor_detailSurgical
			Deallocate cursor_detailSurgical
			
			If (Select Count(1) From @serviceOrderDetailSurgical) > 0 Begin
				Update @ServiceOrderDetail Set ServiceOrderDetailSurgicalXml = (
					Select *
					From @serviceOrderDetailSurgical For Xml Path('ServiceOrderDetailSurgical'), Elements
				)
			End
			
			If @errorSurgical <>  ''
				Set @errorTotalSurgical += Char(13) + Char(10) + 'El item ' + (Select Top 1 CodeNameIpsService From @ServiceOrderDetail) + ' no se pudo homologar por:' 
					+ Char(13) + Char(10) + @errorSurgical
			Else
				Set @errorTotalSurgical = @errorSurgical
				
			If @errorTotalSurgical <> '' Begin
				Update @ServiceOrderDetail Set StatusResult = 0, MessageResult = @errorTotalSurgical
				Return
			End			
		End
		Else Begin
			--si el ips es no quirurgico o paquete
			Declare @SalesValue1 Decimal(18, 2)
				, @RoundService1 Int
				, @RateManualId1 Int
				, @RateManualDetailId Int

			Select Top 1
					@RateManualDetailId = rmd.Id, 
					@SalesValue1 = rmd.SalesValue, 
					@RoundService1 = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(rm.RoundService, 1)), 
					@RateManualId1 = rmd.RateManualId
			From [Contract].RateManualDetail rmd With(Nolock)
			Inner Join [Contract].RateManual rm With(Nolock) On rmd.RateManualId = rm.Id
			Where rmd.RateManualId = @rateManualId And rmd.IPSServiceId = @IPSServiceId

			If @RateManualDetailId Is Null Or @RateManualDetailId = 0 Begin				
				--valido que el ips este parametrizado
				Update @ServiceOrderDetail Set StatusResult = 0, MessageResult = 'El servicio IPS ' + Concat(@IpsServiceCode, ' - ', @IpsServiceName) + ' no esta parametrizado en el manual de tarifas ' + @rateManualCodeName
				Return
			End
			--asigno los valores a la entidad
			Declare @totalSalesPriceItem1 Decimal(18, 2) = @SalesValue1 + (@SalesValue1 * (@rateVariation / 100))
			If @totalSalesPriceItem1 > @RoundService1 Begin
				SELECT	@totalSalesPriceItem1 = [Billing].[RoundValue](@totalSalesPriceItem1, @RoundService1)
			End
			Update @ServiceOrderDetail Set RateManualId = @rateManualId, RateManualType = @ratemanualType
				, RateManualDetailId = @RateManualDetailId, RateManualSalePrice = @totalSalesPriceItem1
				, SubTotalSalesPrice = @totalSalesPriceItem1, TotalSalesPrice = @totalSalesPriceItem1
				, PerformsHealthProfessionalThirdPartyId = @ProfessionalHealthThirdPartyId
				, PerformsHealthProfessionalCode = @ProfessionalHealthCode
				, GrandTotalSalesPrice = @totalSalesPriceItem1 * @InvoicedQuantity

			If (@totalSalesPriceItem1 * @InvoicedQuantity) > @RoundService1 Begin
				UPDATE @ServiceOrderDetail
						SET GrandTotalSalesPrice = [Billing].[RoundValue](GrandTotalSalesPrice, @RoundService1),
							RoundService = @rateManualRoundService
			End
		End

		set @SalesValue = (SELECT SubTotalSalesPrice from @ServiceOrderDetail)

		 UPDATE sod
				Set 
				  GrossValue = svsp.GrossValue
				, TaxValue = svsp.TaxValue
				, IvaId = @IVAId
				, TotalSalesPrice = svsp.SalesPrice
				, SubTotalSalesPrice = svsp.SalesPrice
				, GrandTotalSalesPrice = (svsp.GrossValue + (svsp.GrossValue *(@IVAPercent/100))) * @InvoicedQuantity 
		from  @ServiceOrderDetail sod
		CROSS APPLY [Billing].[SetValueSalesPrice] (@SalePriceIncludeTax,sod.SubTotalSalesPrice,@IVAPercent) svsp 
	end

	If (Select Top 1 Presentation From @ServiceOrderDetail) = 2 Begin
		Set @InvoicedQuantity = 1
	End

	IF @InitialLiquidationType = 1 Begin
		SELECT @ratemanualType = rm.Type 
		from Contract.RateManual rm WITH(NOLOCK)
		WHERE rm.Id=@InitialRateManual
		update @ServiceOrderDetail set RateManualId = @InitialRateManual, RateManualType = @ratemanualType
	end
	
	Update @ServiceOrderDetail Set Recordtype = 1, CareGroupId = @CareGroupId, CUPSEntityId = @CupsEntityId
		, IPSServiceId = @IPSServiceId, InvoicedQuantity = @InvoicedQuantity, Presentation = @IPSPresentation
		, ServiceDate = @ServiceDate, PerformsFunctionalUnitId = @FunctionalUnitId
		, PerformsHealthProfessionalCode = @ProfessionalHealthCode
		, PerformsProfessionalSpecialty = @SpecialtyId
		, BillingConceptId = @CupsEntityBillingConceptId
		, SettlementType = 1
	
	Declare @StateResultIncome Bit,
		@MessageResultIncome Varchar(255),
		@IncomeMainAccountIdIncome Int,
		@ServiceOrderDetailSurgicalXmlIncome Xml
			
	Select @StateResultIncome = StateResult
		, @MessageResultIncome = MessageResult
		, @IncomeMainAccountIdIncome = IncomeMainAccountId
		, @ServiceOrderDetailSurgicalXmlIncome = ServiceOrderDetailSurgicalXml 
	From Billing.GetIncomeMainAccount(@CareGroupId, 1,  @CupsEntityBillingConceptId, @FunctionalUnitId, Null, (Select Top 1 ServiceOrderDetailSurgicalXml From @ServiceOrderDetail))

	If @StateResultIncome = 0 Begin
		Update @ServiceOrderDetail Set StatusResult = 0, MessageResult = 'No se logro encontran la cuenta contable de ingresos para el servicio: ' + Char(13) + Char(10) + @MessageResultIncome
	End
	Else Begin
		Update @ServiceOrderDetail Set IncomeMainAccountId = @IncomeMainAccountIdIncome, ServiceOrderDetailSurgicalXml = @ServiceOrderDetailSurgicalXmlIncome
	End
	
	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que calcula el valor de venta de un servicio institucional (procedimiento, examen o consulta identificado por su código CUPS) dentro del contexto de un contrato y un ingreso hospitalario. Recibe datos del encuentro clínico —número de ingreso, centro de atención, unidad funcional, profesional de salud, fecha del servicio, edad y sexo del paciente— y consulta las tarifas contractuales definidas en [Contract].[DefinitionRateDetail], el catálogo CUPS ([Contract].[CUPSEntity]) y las descripciones de contrato ([Contract].[CUPSEntityContractDescriptions]) para determinar el precio de venta, el tipo de liquidación, el recargo aplicable y el concepto de facturación ([Billing].[BillingConcept]). También resuelve el centro de costo correcto según las reglas del concepto de facturación: puede tomarlo de la unidad funcional, del concepto o de la sucursal-unidad funcional asociada al ingreso. Devuelve una fila con el detalle completo de liquidación del ítem —subtotal, total, valor bruto, IVA, tipo de tarifa, identificadores de la orden— listo para ser consumido por el motor de facturación o registro de órdenes de servicio institucionales.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetServiceValue_Institucional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetServiceValue_Institucional';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula y devuelve el detalle de valor de un servicio institucional (CUPS/IPS) para una admisión, resolviendo concepto de facturación, centro de costo, validaciones clínicas (género/edad/sucursal), tarifa aplicable (fija, estándar o por vigencia), liquidación de servicios quirúrgicos asociados y cuenta contable de ingresos.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetServiceValue_Institucional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El @AdmissionNumber debe existir en dbo.ADINGRESO y su CODCENATE debe coincidir con @CenterAttentionCode (salvo en modo ObtainCostCenter=3, donde se reasigna desde el ingreso).; El @CupsEntityId debe estar registrado en Contract.CUPSEntity con un BillingConcept asociado (directo o vía CUPSEntityContractDescriptions para el contrato).; El @IPSServiceId debe existir en Contract.IPSService.; Si ObtainCostCenter=1, la unidad funcional (@FunctionalUnitId) debe tener CostCenterId definido.; Si ObtainCostCenter=3, debe existir registro en Billing.BillingConceptCostCenter para la combinación (BillingConcept, BranchOffice.Code, FunctionalUnit).; El paciente debe cumplir con el género permitido (InMale/InFemale) y rango de edad (mínimo/máximo) configurados para el servicio IPS.; Para liquidación tipo 3 (vigencia) debe existir RateManualValidityDetail vigente para @ServiceDate y un RateManual asociado.; Para servicios quirúrgicos no fijos debe existir parametrización en RateManualDetailSurgical (o LogicUVRRateManualISS para manuales tipo ISS) que permita derivar el valor.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetServiceValue_Institucional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetServiceValue_Institucional';
GO
