-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-06-01
-- Description:	Excluye de un servicio
-- =============================================

CREATE Procedure [Billing].[SP_ExcludeOutService]
	@ServiceOrderDetailToExcludeXml Xml,
	@FolioId Int,
	@CareGroupId Int,
	@PatientGenus Int,
	@PatientBirth DateTime
AS
Begin
	Set Nocount On;
	Begin Try
		
		Declare @validateErrors Varchar(Max) = '',
			@Cero Int = 0, @Uno Tinyint = 1, @Dos Int = 2, @Tres Int = 3
		Declare @UpdateFolio Table(StatusResult Bit, MessageResult varchar(100))		
		Declare @ServiceOrderDetailToExclude Table(RowId Int Identity(1, 1) Primary Key, ServiceOrderDetailId Int)
		Declare @ListServiceorderDetail Table(
			Id Int Primary Key,
			RevenueControlDetailId Int,
			ServiceOrderDetailId Int,
			Quantity Int,
			GrandTotalSalesPrice Decimal(18, 2),
			GrandTotalDiscount Decimal(18, 2),
			DistributionType Tinyint,
			ThirdPartySalesPrice Decimal(18, 2),
			ThirdPartyPercentage Decimal(5, 2),
			ApplyRecoveryFee Tinyint,
			RecoveryFeeType Tinyint,
			SubTotalPatientSalesPrice Decimal(18, 2),
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
			RateManualSalePrice Decimal(18, 0),
			CostValue Decimal(18, 2),
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
			SubTotalSalesPrice_1 Decimal(18, 2),
			ThirdPartyDiscount_1 Decimal(18, 2),
			ThirdPartyDiscountPercentage Decimal(5, 2),
			TotalSalesPrice Decimal(18, 2),
			GrandTotalSalesPrice_1 Decimal(18, 2),
			SurchargeApply Bit,
			SurgicalInterventionType Tinyint Null,
			SurgeryNumber Tinyint,
			IsFirstEvent Bit,
			IsAnnulled Bit,
			IsDelete Bit,
			IncomeMainAccountId Int,
			--Extender
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
			ServiceOrderDetailSurgicalXml Xml Null,
			GrossValue NUMERIC(20,2),
			TaxValue NUMERIC(20,2),
			IvaId INT,
			ContractDescriptionId INT NULL
		)
		
		Insert Into @ServiceOrderDetailToExclude
		Select t.x.value('ServiceOrderDetailId[1]', 'Int')
		From @ServiceOrderDetailToExcludeXml.nodes('ServiceOrderDetailToExclude') t(x)

		--Select *
		--From Billing.RevenueControlDetail rcd With(Nolock)
		--Where Id = @FolioId
		
		Insert Into @ListServiceorderDetail
		Select Distinct sodd.Id
			, sodd.RevenueControlDetailId
			, sodd.ServiceOrderDetailId
			, sodd.Quantity
			, sodd.GrandTotalSalesPrice
			, sodd.GrandTotalDiscount
			, sodd.DistributionType
			, sodd.ThirdPartySalesPrice
			, sodd.ThirdPartyPercentage
			, sodd.ApplyRecoveryFee
			, sodd.RecoveryFeeType
			, sodd.SubTotalPatientSalesPrice
			, sodd.PatientPercentage
			, sodd.LastCaregroupId
			, sod.ServiceOrderId
			, sod.CareGroupId
			, sod.HealthAdministratorId
			, sod.ThirdPartyId
			, sod.ServiceType
			, sod.RecordType
			, sod.CUPSEntityId
			, sod.IPSServiceId
			, sod.HospitalStayId
			, sod.HospitalStayDetailId
			, sod.ControlExternalConsultation
			, sod.ControlExternalConsultationCode
			, sod.CUPSAssociateService
			, sod.CodeAssociateService
			, sod.IsPackage
			, sod.Packaging
			, sod.PackageServiceOrderDetailId
			, sod.LiquidationType
			, sod.Presentation
			, sod.ProductId
			, sod.InvoicedQuantity
			, sod.SupplyQuantity
			, sod.DevolutionQuantity
			, sod.RateManualSalePrice
			, sod.CostValue
			, sod.ServiceDate
			, sod.AuthorizationNumber
			, sod.PerformsFunctionalUnitId
			, sod.PerformsHealthProfessionalCode
			, sod.PerformsProfessionalSpecialty
			, sod.PerformsHealthProfessionalThirdPartyId
			, sod.BillingConceptId
			, sod.CostCenterId
			, 1 --sod.SettlementType
			, Null--sod.IncludeServiceOrderDetailId
			, sod.RecoveryRatio
			, sod.RateManualId
			, sod.RateManualType
			, sod.RateManualDetailId
			, sod.DefinitionRateDetailId
			, sod.DefinitionRateDetailConditionId
			, sod.SubTotalSalesPrice
			, sod.ThirdPartyDiscount
			, sod.ThirdPartyDiscountPercentage
			, sod.TotalSalesPrice
			, sod.GrandTotalSalesPrice
			, sod.SurchargeApply
			, sod.SurgicalInterventionType
			, sod.SurgeryNumber
			, sod.IsFirstEvent
			, sod.IsAnnulled
			, sod.IsDelete
			, sod.IncomeMainAccountId
			, Null
			, Null
			, Null
			, Null
			, Null
			, Null
			, Null
			, Null
			, Null
			, Null
			, 0
			, sod.RoundService
			, Null
			, sod.GrossValue
			, sod.TaxValue
			, sod.IvaId
			, cecd.ContractDescriptionId
		From Billing.ServiceOrderDetailDistribution sodd With(Nolock)
		Inner Join Billing.ServiceOrderDetail sod With(Nolock) On sodd.ServiceOrderDetailId = sod.Id		
		Inner Join @ServiceOrderDetailToExclude sode On sode.ServiceOrderDetailId = sod.Id
		left join Contract.CUPSEntityContractDescriptions cecd WITH(NOLOCK) on sod.CUPSEntityContractDescriptionId =cecd.Id 

		Update lsod Set ServiceOrderDetailSurgicalXml = (
			Select Id
				, ServiceOrderDetailId
				, CodeNameIpsService
				, IPSServiceId
				, InvoicedQuantity
				, LiquidationPercentage
				, RateManualSalePrice
				, TotalSalesPrice
				, PerformsHealthProfessionalCode
				, PerformsHealthProfessionalThirdPartyId
				, CostValue
				, BillingConceptId
				, CostCenterId
				, RateManualDetailSurgicalId
				, SurchargeApply
				, IncomeMainAccountId
				, RoundService
			From Billing.ServiceOrderDetailSurgical sods
			Where sods.ServiceOrderDetailId = lsod.ServiceOrderDetailId
			For Xml Path('ExcludeServiceOrderDetailSurgical'), Elements
		)
		From @ListServiceorderDetail lsod

		Declare @Rows Int, @RowId Int
		Declare @RecordType Tinyint,
			@ServiceOrderDetailSurgicalXml Xml,			
			@CUPSEntityId Int,
			@IPSServiceId Int,
			@PerformsFunctionalUnitId Int,
			@PerformsProfessionalSpecialty Char(3),
			@ServiceDate DateTime,
			@InvoicedQuantity Int,
			@PerformsHealthProfessionalCode Char(20),
			@PerformsHealthProfessionalThirdPartyId Int,
			@CodeAssociateService Varchar(50),
			@ServiceOrderId Int,
			@AdmissionNumber CHAR(10),
			@ServiceOrderDetailId Int,
			@SurchargeApply Bit,
			@RateManualSalePrice Decimal(18, 2),
			@SubTotalSalesPrice_1 Decimal(18, 2),
			@ThirdPartyDiscount_1 Decimal(18, 2),
			@TotalSalesPrice Decimal(18, 2),
			@GrandTotalSalesPrice_1 Decimal(18, 2),
			@GrandTotalSalesPrice Decimal(18, 2),
			@ThirdPartySalesPrice Decimal(18, 2),
			@ThirdPartyPercentage Decimal(5, 2),
			@ApplyRecoveryFee Tinyint,
			@RecoveryFeeType Tinyint,
			@SubTotalPatientSalesPrice Decimal(18, 2),
			@PatientPercentage Decimal(5, 2),
			@LastCaregroupId Int,
			@ProductId Int,
			@ThirdPartyDiscountPercentage Decimal(5, 2),
			@Quantity Int,
			@GrossValue NUMERIC(20,2),
			@TaxValue NUMERIC(20,2),
			@IvaId INT,
			@ContractDescriptionId INT = NULL

		Set @Rows = 1
		Set @RowId = 1

		While @Rows > 0
		begin

			Select Top 1 @RowId = Id, @RecordType = RecordType
				, @ServiceOrderDetailSurgicalXml = ServiceOrderDetailSurgicalXml
				, @CUPSEntityId = CUPSEntityId
				, @IPSServiceId = IPSServiceId
				, @PerformsFunctionalUnitId = PerformsFunctionalUnitId
				, @PerformsProfessionalSpecialty = PerformsProfessionalSpecialty
				, @ServiceDate = ServiceDate
				, @InvoicedQuantity = InvoicedQuantity
				, @PerformsHealthProfessionalCode = PerformsHealthProfessionalCode
				, @PerformsHealthProfessionalThirdPartyId = PerformsHealthProfessionalThirdPartyId
				, @CodeAssociateService = CodeAssociateService
				, @ServiceOrderId = ServiceOrderId
				, @ServiceOrderDetailId = ServiceOrderDetailId
				, @SurchargeApply = SurchargeApply
				, @RateManualSalePrice = RateManualSalePrice
				, @SubTotalSalesPrice_1 = SubTotalSalesPrice_1
				, @ThirdPartyDiscount_1 = ThirdPartyDiscount_1
				, @TotalSalesPrice = TotalSalesPrice
				, @GrandTotalSalesPrice_1 = GrandTotalSalesPrice_1
				, @GrandTotalSalesPrice = GrandTotalSalesPrice
				, @ThirdPartySalesPrice = ThirdPartySalesPrice
				, @ThirdPartyPercentage = ThirdPartyPercentage
				, @ApplyRecoveryFee = ApplyRecoveryFee
				, @RecoveryFeeType = RecoveryFeeType
				, @SubTotalPatientSalesPrice = SubTotalPatientSalesPrice
				, @PatientPercentage = PatientPercentage
				, @LastCaregroupId = LastCaregroupId
				, @ProductId = ProductId
				, @ThirdPartyDiscountPercentage = ThirdPartyDiscountPercentage
				, @Quantity = Quantity
				, @GrossValue = GrossValue
				, @TaxValue= TaxValue
				, @IvaId = IvaId
				, @ContractDescriptionId = ContractDescriptionId
			From @ListServiceorderDetail Where Id >= @RowId Order By Id

			Set @Rows = @@ROWCOUNT
			If @Rows = 0 
				Break

			If @RecordType = 1 Begin

				If @ServiceOrderDetailSurgicalXml Is Null Begin
					
					Declare @__StatusResult Bit,
						@__MessageResult Varchar(Max),
						@__Id Int,
						@__CostCenterId Int,
						@__ServiceOrderId Int,
						@__CodeAssociateService Varchar(50),
						@__AuthorizationNumber Varchar(20),
						@__ServiceType Tinyint,
						@__CodeNameIpsService Varchar(320),
						@__CodeNameCups Varchar(320),
						@__CodeNameFunctionalUnit Varchar(320),
						@__CodeNameCostCenter Varchar(100),
						@__AllowValueChange Bit,
						@__DefinitionRateDetailId Int,
						@__DefinitionRateDetailConditionId Int,
						@__LiquidationType Tinyint,
						@__Presentation Tinyint,
						@__IsSOAT Bit,
						@__RateManualType Tinyint,
						@__RateManualId Int,
						@__SubTotalSalesPrice Decimal(18, 2),
						@__RateManualSalePrice Decimal(18, 2),
						@__TotalSalesPrice Decimal(18, 2),
						@__PerformsHealthProfessionalThirdPartyId Int,
						@__GrandTotalSalesPrice Decimal(18, 2),
						@__CostValue Decimal(18, 2),
						@__Recordtype Tinyint,
						@__CareGroupId Int,
						@__CUPSEntityId Int,
						@__IPSServiceId Int,
						@__InvoicedQuantity Int,
						@__ServiceDate DateTime,
						@__PerformsFunctionalUnitId Int,
						@__PerformsHealthProfessionalCode Char(20),
						@__PerformsProfessionalSpecialty Char(3),
						@__BillingConceptId Int,
						@__SettlementType Tinyint,
						@__RateManualDetailId Int,
						@__ServiceOrderDetailSurgicalXml Xml,
						@__IncomeMainAccountId Int,
						@__SurgicalInterventionType Tinyint,
						@__SurchargeApply Bit,
						@__RoundService int,
						@__GrossValue NUMERIC(20,2),
						@__TaxValue NUMERIC(20,2),
						@__IvaId INT

					SELECT @AdmissionNumber = AdmissionNumber
					FROM Billing.ServiceOrder
					WHERE Id = @ServiceOrderId
						
					Select @__StatusResult = StatusResult,
						@__MessageResult = MessageResult,
						@__Id = Id,
						@__CostCenterId = CostCenterId,
						@__ServiceType = ServiceType,
						@__CodeNameIpsService = CodeNameIpsService,
						@__CodeNameCups = CodeNameCups,
						@__CodeNameFunctionalUnit = CodeNameFunctionalUnit,
						@__CodeNameCostCenter = CodeNameCostCenter,
						@__AllowValueChange = AllowValueChange,
						@__DefinitionRateDetailId = DefinitionRateDetailId,
						@__DefinitionRateDetailConditionId = DefinitionRateDetailConditionId,
						@__LiquidationType = LiquidationType,
						@__Presentation = Presentation,
						@__IsSOAT = IsSOAT,
						@__RateManualType = RateManualType,
						@__RateManualId = RateManualId,
						@__SubTotalSalesPrice = SubTotalSalesPrice,
						@__RateManualSalePrice = RateManualSalePrice,
						@__TotalSalesPrice = TotalSalesPrice,
						@__PerformsHealthProfessionalThirdPartyId = PerformsHealthProfessionalThirdPartyId,
						@__GrandTotalSalesPrice = GrandTotalSalesPrice,
						@__CostValue = CostValue,
						@__RecordType = RecordType,
						@__CareGroupId = CareGroupId,
						@__CUPSEntityId = CUPSEntityId,
						@__IPSServiceId = IPSServiceId,
						@__InvoicedQuantity = InvoicedQuantity,
						@__ServiceDate = ServiceDate,
						@__PerformsFunctionalUnitId = PerformsFunctionalUnitId,
						@__PerformsHealthProfessionalCode = PerformsHealthProfessionalCode,
						@__PerformsProfessionalSpecialty = PerformsProfessionalSpecialty,
						@__BillingConceptId = BillingConceptId,
						@__SettlementType = SettlementType,
						@__RateManualDetailId = RateManualDetailId,
						@__ServiceOrderDetailSurgicalXml = ServiceOrderDetailSurgicalXml,
						@__IncomeMainAccountId = IncomeMainAccountId,
						@__SurgicalInterventionType = SurgicalInterventionType,
						@__SurchargeApply = SurchargeApply,
						@__RoundService = RoundService,
						@__GrossValue= GrossValue,
						@__TaxValue = TaxValue,
						@__IvaId= IvaId
					From [Contract].GetValueService(@AdmissionNumber, NULL, @CUPSEntityId, @IPSServiceId, @CareGroupId, @PerformsFunctionalUnitId,
						@PerformsProfessionalSpecialty, @ServiceDate, @PatientGenus, @PatientBirth, @InvoicedQuantity,
						@PerformsHealthProfessionalCode, @PerformsHealthProfessionalThirdPartyId,0,COALESCE(@ContractDescriptionId,0))
						
					If @__StatusResult = 1 Begin

						Declare @serviceOrderDetailResultXml Xml = (										
							Select	@__StatusResult As StatusResult,
									@__MessageResult As MessageResult,
									@__Id As Id,
									@__CostCenterId As CostCenterId,
									@__ServiceType As ServiceType,
									@__CodeNameIpsService As CodeNameIpsService,
									@__CodeNameCups As CodeNameCups,
									@__CodeNameFunctionalUnit As CodeNameFunctionalUnit,
									@__CodeNameCostCenter As CodeNameCostCenter,
									@__AllowValueChange As AllowValueChange,
									@__DefinitionRateDetailId As DefinitionRateDetailId,
									@__DefinitionRateDetailConditionId As DefinitionRateDetailConditionId,
									@__LiquidationType As LiquidationType,
									@__Presentation As Presentation,
									@__IsSOAT As IsSOAT,
									@__RateManualType As RateManualType,
									@__RateManualId As RateManualId,
									@__SubTotalSalesPrice As SubTotalSalesPrice,
									@__RateManualSalePrice As RateManualSalePrice,
									@__TotalSalesPrice As TotalSalesPrice,
									@__PerformsHealthProfessionalThirdPartyId As PerformsHealthProfessionalThirdPartyId,
									@__GrandTotalSalesPrice As GrandTotalSalesPrice,
									@__CostValue As CostValue,
									@__RecordType As RecordType,
									@__CareGroupId As CareGroupId,
									@__CUPSEntityId As CUPSEntityId,
									@__IPSServiceId As IPSServiceId,
									@__InvoicedQuantity As InvoicedQuantity,
									@__ServiceDate As ServiceDate,
									@__PerformsFunctionalUnitId As PerformsFunctionalUnitId,
									@__PerformsHealthProfessionalCode As PerformsHealthProfessionalCode,
									@__PerformsProfessionalSpecialty As PerformsProfessionalSpecialty,
									@__BillingConceptId As BillingConceptId,
									@__SettlementType As SettlementType,
									@__RateManualDetailId As RateManualDetailId,
									Null As ServiceOrderDetailSurgicalXml,
									@__IncomeMainAccountId As IncomeMainAccountId,
									@__SurgicalInterventionType As SurgicalInterventionType,
									@__SurchargeApply As SurchargeApply,
									@__RoundService As RoundService,
									@__GrossValue AS GrossValue,
									@__TaxValue AS TaxValue,
									@__IvaId AS IvaId
								For Xml Path('ServiceOrderDetail'), Elements
						)

						Declare @ResultServiceOrderDetailXml Xml, @ResultServiceOrderDetailSurgicalXml Xml

						Declare @ExcludeServiceOrderDetailXml Xml = (
							Select * From @ListServiceorderDetail Where ServiceOrderDetailId = @ServiceOrderDetailId For Xml Path('DistributionToRetarific'), Elements
						)
						Declare @CurrentSurgical Xml = (
							Select Replace(Replace(Cast(ServiceOrderDetailSurgicalXml As Varchar(Max)),'</ExcludeServiceOrderDetailSurgical>','</RetarificServiceOrderDetailSurgical>'),'<ExcludeServiceOrderDetailSurgical>','<RetarificServiceOrderDetailSurgical>')
							From @ListServiceorderDetail Where ServiceOrderDetailId = @ServiceOrderDetailId
						)
						Select @ResultServiceOrderDetailXml = ServiceOrderDetailXml, @ResultServiceOrderDetailSurgicalXml = ServiceOrderDetailSurgicalXml 
						From [Contract].SetServiceValue(@ExcludeServiceOrderDetailXml, @CurrentSurgical
							, @serviceOrderDetailResultXml, @__ServiceOrderDetailSurgicalXml, @CodeAssociateService, @CareGroupId)

						--Actualizar los nuevos datos de la tabla @ListServiceorderDetail
						Update rt Set Quantity = t.x.value('Quantity[1]', 'Int'),
							GrandTotalSalesPrice = t.x.value('GrandTotalSalesPrice[1]', 'Decimal(18,2)'),
							GrandTotalDiscount = t.x.value('GrandTotalDiscount[1]', 'Decimal(18,2)'),
							DistributionType = t.x.value('DistributionType[1]', 'Tinyint'),
							ThirdPartySalesPrice = t.x.value('ThirdPartySalesPrice[1]', 'Decimal(18,2)'),
							ThirdPartyPercentage = t.x.value('ThirdPartyPercentage[1]', 'Decimal(5,2)'),
							ApplyRecoveryFee = t.x.value('ApplyRecoveryFee[1]', 'Tinyint'),
							RecoveryFeeType = t.x.value('RecoveryFeeType[1]', 'Tinyint'),
							SubTotalPatientSalesPrice = t.x.value('SubTotalPatientSalesPrice[1]', 'Decimal(18,2)'),
							PatientPercentage = t.x.value('PatientPercentage[1]', 'Decimal(5,2)'),
							LastCaregroupId = t.x.value('LastCaregroupId[1]', 'Int'),
							ServiceOrderId = t.x.value('ServiceOrderId[1]', 'Int'),
							CareGroupId = t.x.value('CareGroupId[1]', 'Int'),
							HealthAdministratorId = t.x.value('HealthAdministratorId[1]', 'Int'),
							ThirdPartyId = t.x.value('ThirdPartyId[1]', 'Int'),
							ServiceType = t.x.value('ServiceType[1]', 'Tinyint'),
							RecordType = t.x.value('RecordType[1]', 'Tinyint'),
							CUPSEntityId = t.x.value('CUPSEntityId[1]', 'Int'),
							IPSServiceId = t.x.value('IPSServiceId[1]', 'Int'),
							HospitalStayId = t.x.value('HospitalStayId[1]', 'Int'),
							HospitalStayDetailId = t.x.value('HospitalStayDetailId[1]', 'Int'),
							ControlExternalConsultation = t.x.value('ControlExternalConsultation[1]', 'Tinyint'),
							ControlExternalConsultationCode = t.x.value('ControlExternalConsultationCode[1]', 'Decimal(18,0)'),
							CUPSAssociateService = t.x.value('CUPSAssociateService[1]', 'Bit'),
							CodeAssociateService = t.x.value('CodeAssociateService[1]', 'Varchar(50)'),
							IsPackage = t.x.value('IsPackage[1]', 'Bit'),
							Packaging = t.x.value('Packaging[1]', 'Bit'),
							PackageServiceOrderDetailId = t.x.value('PackageServiceOrderDetailId[1]', 'Int'),
							LiquidationType = t.x.value('LiquidationType[1]', 'Tinyint'),
							Presentation = t.x.value('Presentation[1]', 'Tinyint'),
							ProductId = t.x.value('ProductId[1]', 'Int'),
							InvoicedQuantity = t.x.value('InvoicedQuantity[1]', 'Int'),
							SupplyQuantity = t.x.value('SupplyQuantity[1]', 'Int'),
							DevolutionQuantity = t.x.value('DevolutionQuantity[1]', 'Int'),
							RateManualSalePrice = t.x.value('RateManualSalePrice[1]', 'Decimal(18,0)'),
							CostValue = t.x.value('CostValue[1]', 'Decimal(18,2)'),
							ServiceDate = t.x.value('ServiceDate[1]', 'DateTime'),
							AuthorizationNumber = t.x.value('AuthorizationNumber[1]', 'Varchar(20)'),
							PerformsFunctionalUnitId = t.x.value('PerformsFunctionalUnitId[1]', 'Int'),
							PerformsHealthProfessionalCode = t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)'),
							PerformsProfessionalSpecialty = t.x.value('PerformsProfessionalSpecialty[1]', 'Char(3)'),
							PerformsHealthProfessionalThirdPartyId = t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'Int'),
							BillingConceptId = t.x.value('BillingConceptId[1]', 'Int'),
							CostCenterId = t.x.value('CostCenterId[1]', 'Int'),
							SettlementType = t.x.value('SettlementType[1]', 'Tinyint'),
							IncludeServiceOrderDetailId = t.x.value('IncludeServiceOrderDetailId[1]', 'Int'),
							RecoveryRatio = t.x.value('RecoveryRatio[1]', 'Decimal(5,2)'),
							RateManualId = t.x.value('RateManualId[1]', 'Int'),
							RateManualType = t.x.value('RateManualType[1]', 'Tinyint'),
							RateManualDetailId = t.x.value('RateManualDetailId[1]', 'Int'),
							DefinitionRateDetailId = t.x.value('DefinitionRateDetailId[1]', 'Int'),
							DefinitionRateDetailConditionId = t.x.value('DefinitionRateDetailConditionId[1]', 'Int'),
							SubTotalSalesPrice_1 = t.x.value('SubTotalSalesPrice_1[1]', 'Decimal(18,2)'),
							ThirdPartyDiscount_1 = t.x.value('ThirdPartyDiscount_1[1]', 'Decimal(18,2)'),
							ThirdPartyDiscountPercentage = t.x.value('ThirdPartyDiscountPercentage[1]', 'Decimal(5,2)'),
							TotalSalesPrice = t.x.value('TotalSalesPrice[1]', 'Decimal(18,2)'),
							GrandTotalSalesPrice_1 = t.x.value('GrandTotalSalesPrice_1[1]', 'Decimal(18,2)'),
							SurchargeApply = t.x.value('SurchargeApply[1]', 'Bit'),
							SurgicalInterventionType = t.x.value('SurgicalInterventionType[1]', 'Tinyint'),
							SurgeryNumber = t.x.value('SurgeryNumber[1]', 'Tinyint'),
							IsFirstEvent = t.x.value('IsFirstEvent[1]', 'Bit'),
							IsAnnulled = t.x.value('IsAnnulled[1]', 'Bit'),
							IsDelete = t.x.value('IsDelete[1]', 'Bit'),
							IncomeMainAccountId = t.x.value('IncomeMainAccountId[1]', 'Int'),
							CodeNameSpeciality = t.x.value('CodeNameSpeciality[1]', 'Varchar(300)'),
							CodeNameFunctionalUnit = t.x.value('CodeNameFunctionalUnit[1]', 'Varchar(300)'),
							CodeNameHealthAdministrator = t.x.value('CodeNameHealthAdministrator[1]', 'Varchar(300)'),
							PreviusServiceOrderDetailId = t.x.value('PreviusServiceOrderDetailId[1]', 'Int'),
							CodeNameCareGroup = t.x.value('CodeNameCareGroup[1]', 'Varchar(320)'),
							CodeNameCostCenter = t.x.value('CodeNameCostCenter[1]', 'Varchar(300)'),
							CodeNameCups = t.x.value('CodeNameCups[1]', 'Varchar(320)'),
							CodeNameHealthProfessional = t.x.value('CodeNameHealthProfessional[1]', 'Varchar(300)'),
							CodeNameIpsService = t.x.value('CodeNameIpsService[1]', 'Varchar(300)'),
							CodeNameProduct = t.x.value('CodeNameProduct[1]', 'Varchar(300)'),
							IsSOAT = t.x.value('IsSOAT[1]', 'Bit'),
							RoundService = t.x.value('RoundService[1]', 'int'),
							ServiceOrderDetailSurgicalXml = @ResultServiceOrderDetailSurgicalXml,
							GrossValue = t.x.value('GrossValue[1]', 'NUMERIC(20,2)'),
							TaxValue = t.x.value('TaxValue[1]', 'NUMERIC(20,2)'),
							IvaId = t.x.value('IvaId[1]', 'int')
						From @ListServiceorderDetail rt
						Inner Join @ResultServiceOrderDetailXml.nodes('/ServiceOrderDetail') t(x) On t.x.value('Id[1]', 'Int') = rt.Id
						Where rt.Id = @RowId

					End
					Else Begin
						Declare @IpsCodeName Varchar(320)
						Select @IpsCodeName = Concat(Code, ' - ', [Name])
						From [Contract].IPSService With(Nolock)
						Where Id = @IPSServiceId

						Set @validateErrors += Char(13) + Char(10) + 'No se ha encontrado valor para el servicio ' + @IpsCodeName
					End
				End
				Else Begin
					Declare @errorList Varchar(Max) = ''
					Declare @listServiceOrderDetails Table(
						Id Int Primary Key,
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
						RateManualSalePrice Decimal(18, 2),
						CostValue Decimal(18, 2),
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
						SubTotalSalesPrice_1 Decimal(18, 2),
						ThirdPartyDiscount_1 Decimal(18, 2),
						ThirdPartyDiscountPercentage Decimal(5, 2),
						TotalSalesPrice Decimal(18, 2),
						GrandTotalSalesPrice_1 Decimal(18, 2),
						SurchargeApply Bit,
						SurgicalInterventionType Tinyint Null,
						SurgeryNumber Tinyint,
						IsFirstEvent Bit,
						IsAnnulled Bit,
						IsDelete Bit,
						IncomeMainAccountId Int,						
						--Extender
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
						AllowValueChange Bit,
						RoundService int,
						ServiceOrderDetailSurgicalXml Xml Null						
					)
					Delete From @listServiceOrderDetails

					Insert Into @listServiceOrderDetails
						Select 
							sod.Id
							, sod.ServiceOrderId
							, sod.CareGroupId
							, sod.HealthAdministratorId
							, sod.ThirdPartyId
							, sod.ServiceType
							, sod.RecordType
							, sod.CUPSEntityId
							, sod.IPSServiceId
							, sod.HospitalStayId
							, sod.HospitalStayDetailId
							, sod.ControlExternalConsultation
							, sod.ControlExternalConsultationCode
							, sod.CUPSAssociateService
							, sod.CodeAssociateService
							, sod.IsPackage
							, sod.Packaging
							, sod.PackageServiceOrderDetailId
							, sod.LiquidationType
							, sod.Presentation
							, sod.ProductId
							, sod.InvoicedQuantity
							, sod.SupplyQuantity
							, sod.DevolutionQuantity
							, sod.RateManualSalePrice
							, sod.CostValue
							, sod.ServiceDate
							, sod.AuthorizationNumber
							, sod.PerformsFunctionalUnitId
							, sod.PerformsHealthProfessionalCode
							, sod.PerformsProfessionalSpecialty
							, sod.PerformsHealthProfessionalThirdPartyId
							, sod.BillingConceptId
							, sod.CostCenterId
							, sod.SettlementType --1--sod.SettlementType
							, sod.IncludeServiceOrderDetailId --Null--sod.IncludeServiceOrderDetailId
							, sod.RecoveryRatio
							, sod.RateManualId
							, sod.RateManualType
							, sod.RateManualDetailId
							, sod.DefinitionRateDetailId
							, sod.DefinitionRateDetailConditionId
							, sod.SubTotalSalesPrice
							, sod.ThirdPartyDiscount
							, sod.ThirdPartyDiscountPercentage
							, sod.TotalSalesPrice
							, sod.GrandTotalSalesPrice
							, sod.SurchargeApply
							, sod.SurgicalInterventionType
							, sod.SurgeryNumber
							, sod.IsFirstEvent
							, sod.IsAnnulled
							, sod.IsDelete
							, sod.IncomeMainAccountId
							, Null
							, Concat(fu.Code, ' - ', fu.[Name])
							, Case When sod.HealthAdministratorId Is Null Then Null Else Concat(ha.Code, ' - ', ha.[Name]) End
							, Null
							, Concat(cg.Code, ' - ', cg.[Name])
							, Concat(cc.Code, ' - ', cc.[Name])
							, Case When sod.CUPSEntityId Is Null Then Null Else Concat(ce.Code, ' - ', ce.[Description]) End
							, Null
							, Case When sod.IPSServiceId Is Null Then Null Else Concat(ips.Code, ' - ', ips.[Name]) End
							, Case When sod.ProductId Is Null Then Null Else Concat(ipr.Code, ' - ', ipr.[Name]) End
							, 0
							, 0
							, sod.RoundService
							, Null						
						From Billing.ServiceOrderDetail sod With(Nolock)					
						Inner Join [Contract].CareGroup cg With(Nolock) On sod.CareGroupId = cg.Id
						Left Join [Contract].IPSService ips With(Nolock) On ips.Id = sod.IPSServiceId
						Left Join [Contract].CUPSEntity ce With(Nolock) On sod.CUPSEntityId = ce.Id
						Left Outer Join Payroll.FunctionalUnit fu With(Nolock) On sod.PerformsFunctionalUnitId = fu.Id
						Left Outer Join [Payroll].CostCenter cc With(Nolock) On sod.CostCenterId = cc.Id
						Left Join [Common].ThirdParty tp With(Nolock) On sod.ThirdPartyId = tp.Id
						Left Join Inventory.InventoryProduct ipr With(Nolock) On sod.ProductId = ipr.Id
						Left Join [Contract].HealthAdministrator ha With(Nolock) On sod.HealthAdministratorId = ha.Id
						Where sod.ServiceOrderId = @ServiceOrderId And sod.IsDelete = @Cero
					
					Update ld Set SettlementType = 1, IncludeServiceOrderDetailId = Null
					From @listServiceOrderDetails ld
					Inner Join @ListServiceorderDetail ld2 On ld2.ServiceOrderDetailId = ld.Id

					Update lsod 
						Set ServiceOrderDetailSurgicalXml = 
							(
								Select sods.Id
									, ServiceOrderDetailId
									, IPSServiceId
									, InvoicedQuantity
									, LiquidationPercentage
									, RateManualSalePrice
									, TotalSalesPrice
									, PerformsHealthProfessionalCode
									, PerformsHealthProfessionalThirdPartyId
									, CostValue
									, sods.BillingConceptId
									, CostCenterId
									, RateManualDetailSurgicalId
									, SurchargeApply
									, IncomeMainAccountId
									, (
										Case ips.ServiceClass
											When 1 Then
												'Ninguno'
											When 2 Then
												'Cirujano'
											When 3 Then
												'Anestesiólogo'
											When 4 Then
												'Ayudante'
											When 5 Then
												'Derecho Sala'
											When 6 Then
												'Materiales Sutura'
											When 7 Then
												'Instrumentación Quirúrgica'
											Else ''
										End
									) As ClassServiceIps
									, Concat(ips.Code, ' - ', ips.[Name]) As CodeNameIpsService
									, sods.RoundService
								From Billing.ServiceOrderDetailSurgical sods
								Inner Join [Contract].IPSService ips With(Nolock) On sods.IPSServiceId = ips.Id
								Where sods.ServiceOrderDetailId = lsod.Id
								For Xml Path('ExcludeServiceOrderDetailSurgical'), Elements
							)
					From @listServiceOrderDetails lsod
					
					Declare @serviceOrderDatailFirstEventNewCodeName As Varchar(320),
						@serviceOrderDatailFirstEventNewSurgeryNumber Tinyint

					Select @serviceOrderDatailFirstEventNewCodeName = CodeNameIpsService
						, @serviceOrderDatailFirstEventNewSurgeryNumber = SurgeryNumber
					From @listServiceOrderDetails
					Where IsFirstEvent = 1

					Declare @listEventsTmp Table(
						Id Int Primary Key,
						IsPackage Bit,
						CodeNameIpsService Varchar(320)
					)
					Delete From @listEventsTmp

					Insert Into @listEventsTmp
					Select Id, IsPackage, CodeNameIpsService
					From @listServiceOrderDetails
					Where SurgeryNumber = @serviceOrderDatailFirstEventNewSurgeryNumber And Presentation = @Dos And Id > @Cero

					Declare @Rowsx Int, @RowIdx Int
						, @CodeNameIpsServicex Varchar(320)
						, @IsPackagex Bit

					Set @Rowsx = 1
					Set @RowIdx = 1

					While @Rowsx > 0
					begin

						Select Top 1 @RowIdx = Id, @CodeNameIpsServicex = CodeNameIpsService
							, @IsPackagex = IsPackage
						From @listEventsTmp Where Id >= @RowIdx Order By Id

						Set @Rowsx = @@ROWCOUNT
						If @Rowsx = 0 
							Break

						If @IsPackagex = 1 Begin
							Set @errorList += Char(13) + Char(10) + 'El servicio ' + @serviceOrderDatailFirstEventNewCodeName + 
								' no se puede modificar porque el servicio ' + @CodeNameIpsServicex + ' esta empaquetado.'

							Select Convert(Bit, 0) As [StatusResult], @errorList As [MessageResult]
							Return
						End

						--valido que los items no esten distribuidos
						If (Select Count(Id) From Billing.ServiceOrderDetailDistribution With(Nolock) Where ServiceOrderDetailId = @RowIdx) > 1 Begin
							Set @errorList += Char(13) + Char(10) + 'El servicio ' + @serviceOrderDatailFirstEventNewCodeName + 
								' no se puede modificar porque el servicio ' + @CodeNameIpsServicex + ' esta distribuido.'

							Select Convert(Bit, 0) As [StatusResult], @errorList As [MessageResult]
							Return
						End
						Declare @FolioStatus Tinyint, @FolioOrder Tinyint

						Select @FolioStatus = rcd.[Status], @FolioOrder = rcd.FolioOrder
						From Billing.ServiceOrderDetailDistribution sodd With(Nolock)
						Inner Join Billing.RevenueControlDetail rcd With(Nolock) On sodd.RevenueControlDetailId = rcd.Id
						Where sodd.ServiceOrderDetailId = @RowIdx

						If @FolioStatus > 1 Begin
							If @FolioStatus = 2
								Set @errorList += Char(13) + Char(10) + 'El servicio ' + @serviceOrderDatailFirstEventNewCodeName + 
									' no se puede modificar porque el folio #' + Cast(@FolioOrder As Varchar) + ' esta facturado.'
							Else If @FolioStatus = 5
								Set @errorList += Char(13) + Char(10) + 'El servicio ' + @serviceOrderDatailFirstEventNewCodeName + 
									' no se puede modificar porque el folio #' + Cast(@FolioOrder As Varchar) + ' esta en estado de Reconocimiento de Ingresos.'
							Else
								Set @errorList += Char(13) + Char(10) + 'El servicio ' + @serviceOrderDatailFirstEventNewCodeName + 
									' no se puede modificar porque el folio #' + Cast(@FolioOrder As Varchar) + ' esta bloqueado.'

							Select Convert(Bit, 0) As [StatusResult], @errorList As [MessageResult]
							Return
						End

						Set @RowIdx += 1
					End
					
					Declare @__Rows Int, @__RowId Int
					Set @__Rows = 1
					Set @__RowId = 1

					Declare @___IncludeServiceOrderDetailId Int,
						@___SurgicalInterventionType Tinyint,
						@___CupsEntityId Int,
						@___IPSServiceId Int,
						@___CareGroupId Int,
						@___PerformsFunctionalUnitId Int,
						@___PerformsProfessionalSpecialty Char(3),
						@___ServiceDate DateTime,
						@___AllowValueChange Bit,
						@___LiquidationType Tinyint,
						@___SurchargeApply Bit,
						@___SubTotalSalesPrice Decimal(18, 2),
						@___TotalSalesPrice Decimal(18, 2),
						@___GrandTotalSalesPrice Decimal(18, 2),
						@___InvoicedQuantity Int,
						@___RateManualId Int,
						@___IsFirstEvent Bit,
						@___ServiceOrderDetailSurgicalXml Xml,
						@___ThirdPartyDiscount Decimal(18, 2),
						@___SurgeryNumber Tinyint

					While @__Rows > 0
					begin

						Select Top 1 @__RowId = Id
							, @___IncludeServiceOrderDetailId = IncludeServiceOrderDetailId
							, @___SurgicalInterventionType = SurgicalInterventionType
							, @___CupsEntityId = CUPSEntityId
							, @___IPSServiceId = IPSServiceId
							, @___CareGroupId = CareGroupId
							, @___PerformsFunctionalUnitId = PerformsFunctionalUnitId
							, @___PerformsProfessionalSpecialty = PerformsProfessionalSpecialty
							, @___ServiceDate = ServiceDate
							, @___AllowValueChange = AllowValueChange
							, @___LiquidationType = LiquidationType
							, @___SurchargeApply = SurchargeApply
							, @___SubTotalSalesPrice = SubTotalSalesPrice_1
							, @___TotalSalesPrice = TotalSalesPrice
							, @___GrandTotalSalesPrice = GrandTotalSalesPrice_1
							, @___InvoicedQuantity = InvoicedQuantity
							, @___RateManualId = RateManualId
							, @___IsFirstEvent = IsFirstEvent
							, @___ServiceOrderDetailSurgicalXml = ServiceOrderDetailSurgicalXml
							, @___ThirdPartyDiscount = ThirdPartyDiscount_1
						From @listServiceOrderDetails 
						Where Presentation = 2 And Id >= @__RowId Order By Id

						Set @__Rows = @@ROWCOUNT
						If @__Rows = 0 
							Break

						If @___IncludeServiceOrderDetailId Is Not Null And @___IncludeServiceOrderDetailId > 0 And @__RowId <> @ServiceOrderDetailId Begin
							Set @__RowId += 1
							Continue
						End							

						--si el item no es basico ni cruento se recalcula
						If @___SurgicalInterventionType > 1 And @___SurgicalInterventionType < 9 Begin														
						
							Declare @SurgicalExclude Xml = (
								Select t.x.value('RowId[1]', 'Int') As RowId,
									t.x.value('Id[1]', 'Int') As Id,
									t.x.value('ServiceOrderDetailId[1]', 'Int') As ServiceOrderDetailId,
									t.x.value('CodeNameIpsService[1]', 'Varchar(320)') As CodeNameIpsService,
									t.x.value('IPSServiceId[1]', 'Int') As IPSServiceId,
									t.x.value('InvoicedQuantity[1]', 'Int') As InvoicedQuantity,
									t.x.value('LiquidationPercentage[1]', 'Numeric(5,2)') As LiquidationPercentage,
									t.x.value('RateManualSalePrice[1]', 'Numeric(18,2)') As RateManualSalePrice,
									t.x.value('TotalSalesPrice[1]', 'Numeric(18,2)') As TotalSalesPrice,
									t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)') As PerformsHealthProfessionalCode,
									t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'Int') As PerformsHealthProfessionalThirdPartyId,
									t.x.value('CostValue[1]', 'Numeric(18,2)') As CostValue,
									t.x.value('BillingConceptId[1]', 'Int') As BillingConceptId,
									t.x.value('CostCenterId[1]', 'Int') As CostCenterId,
									t.x.value('RateManualDetailSurgicalId[1]', 'Int') As RateManualDetailSurgicalId,
									t.x.value('SurchargeApply[1]', 'Bit') As SurchargeApply,
									t.x.value('IncomeMainAccountId[1]', 'Int') As IncomeMainAccountId,
									t.x.value('ClassServiceIps[1]', 'Varchar(50)') As ClassServiceIps,
									t.x.value('RoundService[1]', 'int') As RoundService
								From @___ServiceOrderDetailSurgicalXml.nodes('ExcludeServiceOrderDetailSurgical') t(x)
								For Xml Path('ServiceOrderDetailSurgicalXml'), Elements
							)

							Select @___AllowValueChange = AllowValueChange
								, @___LiquidationType = LiquidationType
								, @___SubTotalSalesPrice = SubTotalSalesPrice
								, @___TotalSalesPrice = TotalSalesPrice
								, @___GrandTotalSalesPrice = GrandTotalSalesPrice
								, @___ServiceOrderDetailSurgicalXml = ServiceOrderDetailSurgicalXml
							From Billing.RecalculateSurgicalEvents(@___CupsEntityId, @___IPSServiceId, @___CareGroupId
								, @___PerformsFunctionalUnitId, @___PerformsProfessionalSpecialty, @___ServiceDate, @___AllowValueChange
								, @___LiquidationType, @SurgicalExclude, @___SurchargeApply, @___SubTotalSalesPrice, @___TotalSalesPrice
								, @___GrandTotalSalesPrice, @___InvoicedQuantity)
							
							Update @listServiceOrderDetails	Set AllowValueChange = @___AllowValueChange, LiquidationType = @___LiquidationType
								, SubTotalSalesPrice_1 = @___SubTotalSalesPrice, TotalSalesPrice = @___TotalSalesPrice
								, GrandTotalSalesPrice_1 = @___GrandTotalSalesPrice, ServiceOrderDetailSurgicalXml = @___ServiceOrderDetailSurgicalXml
							Where Id = @__RowId

							Declare @MainHundredPercent Bit, @SurgeonPercentage Decimal(5, 2)
								, @AnesthesiologistPercentage Decimal(5, 2)
								, @AssistantPercentage Decimal(5, 2)
								, @RoomPercentage Decimal(5, 2)
								, @MaterialsPercentage Decimal(5, 2)
								, @SurgeriesRoundService Int

							Select Top 1 @MainHundredPercent = spm.MainHundredPercent
								, @SurgeonPercentage = spm.SurgeonPercentage
								, @AnesthesiologistPercentage = spm.AnesthesiologistPercentage
								, @AssistantPercentage = spm.AssistantPercentage
								, @RoomPercentage = spm.RoomPercentage
								, @MaterialsPercentage = spm.MaterialsPercentage
								, @SurgeriesRoundService = rm.RoundService
							From [Contract].SurgeriesPercentageManual spm With(Nolock)
							Inner Join [Contract].RateManual rm With(Nolock) On spm.RateManualId = rm.Id
							Where spm.RateManualId = @___RateManualId And spm.InterventionType = @___SurgicalInterventionType

							Declare @ServiceOrderDetailSurgicalResult Table (
								RowId Int Primary Key,
								[Id] [int] NULL,
								[ServiceOrderDetailId] Int,
								[CodeNameIpsService] [varchar](320) Null,
								[IPSServiceId] [int] NOT NULL,
								[InvoicedQuantity] [int] NOT NULL,
								[LiquidationPercentage] [numeric](5, 2) NOT NULL,
								[RateManualSalePrice] [numeric](18, 2) NOT NULL,
								[TotalSalesPrice] [numeric](18, 2) NOT NULL,
								[PerformsHealthProfessionalCode] [char](20) NULL,
								[PerformsHealthProfessionalThirdPartyId] [int] NULL,
								[CostValue] [numeric](18, 2) NOT NULL,
								[BillingConceptId] [int] NOT NULL,
								[CostCenterId] [int] NOT NULL,
								[RateManualDetailSurgicalId] [int] NULL,
								[SurchargeApply] [bit] NOT NULL,
								[IncomeMainAccountId] [int] NOT NULL,
								[ClassServiceIps] Varchar(50),
								[RoundService] int
							)

							Delete From @ServiceOrderDetailSurgicalResult

							Insert Into @ServiceOrderDetailSurgicalResult
								Select t.x.value('RowId[1]', 'Int'),
									t.x.value('Id[1]', 'Int'),
									t.x.value('ServiceOrderDetailId[1]', 'Int'),
									t.x.value('CodeNameIpsService[1]', 'Varchar(320)'),
									t.x.value('IPSServiceId[1]', 'Int'),
									t.x.value('InvoicedQuantity[1]', 'Int'),
									t.x.value('LiquidationPercentage[1]', 'Numeric(5,2)'),
									t.x.value('RateManualSalePrice[1]', 'Numeric(18,2)'),
									t.x.value('TotalSalesPrice[1]', 'Numeric(18,2)'),
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
								From @___ServiceOrderDetailSurgicalXml.nodes('ServiceOrderDetailSurgicalXml') t(x)

							If (@___IsFirstEvent = 1 And @MainHundredPercent = 0) Or (@___IsFirstEvent = 0) Begin
								
								Declare @Rowss Int, @RowIds Int
								Set @Rowss = 1
								Set @RowIds = 1

								Declare @ClassServiceIpsx Varchar(50),
									@TotalSalesPricex Numeric(18,0)

								While @Rowss > 0
								begin
									Select Top 1 @RowIds = RowId,
										@ClassServiceIpsx = ClassServiceIps,
										@TotalSalesPricex = TotalSalesPrice
									From @ServiceOrderDetailSurgicalResult Where RowId >= @RowIds Order By RowId
									Set @Rowss = @@ROWCOUNT
									If @Rowss = 0 
										Break

									If @ClassServiceIpsx = 'Cirujano'
										Set @TotalSalesPricex = Billing.RoundValue(@TotalSalesPricex * @SurgeonPercentage / 100, @SurgeriesRoundService)
									Else If @ClassServiceIpsx = 'Anestesiólogo'
										Set @TotalSalesPricex = Billing.RoundValue(@TotalSalesPricex * @AnesthesiologistPercentage / 100, @SurgeriesRoundService)
									Else If @ClassServiceIpsx = 'Ayudante'
										Set @TotalSalesPricex = Billing.RoundValue(@TotalSalesPricex * @AssistantPercentage / 100, @SurgeriesRoundService)
									Else If @ClassServiceIpsx = 'Derecho Sala'
										Set @TotalSalesPricex = Billing.RoundValue(@TotalSalesPricex * @RoomPercentage / 100, @SurgeriesRoundService)
									Else If @ClassServiceIpsx = 'Materiales Sutura'
										Set @TotalSalesPricex = Billing.RoundValue(@TotalSalesPricex * @MaterialsPercentage / 100, @SurgeriesRoundService)
										
									Update @ServiceOrderDetailSurgicalResult Set TotalSalesPrice = @TotalSalesPricex Where RowId = @RowIds

									Set @RowIds += 1
								End
							End

							Set @___TotalSalesPrice = (Select Sum(TotalSalesPrice) From @ServiceOrderDetailSurgicalResult) - @___ThirdPartyDiscount
							Set @___SubTotalSalesPrice = @___TotalSalesPrice
							Set @___GrandTotalSalesPrice = @___TotalSalesPrice * @___InvoicedQuantity
							
							Update @listServiceOrderDetails	
								Set TotalSalesPrice = @___TotalSalesPrice
									, SubTotalSalesPrice_1 = @___SubTotalSalesPrice
									, GrandTotalSalesPrice_1 = @___GrandTotalSalesPrice
									, ServiceOrderDetailSurgicalXml = (
										Select *
										From @ServiceOrderDetailSurgicalResult
										For Xml Path('ExcludeServiceOrderDetailSurgical'), Elements
									)
									, RoundService = @SurgeriesRoundService
							Where Id = @__RowId
						End
						
						Set @__RowId += 1
					End
								
					Set @__Rows = 1
					Set @__RowId = 1

					While @__Rows > 0
					Begin
						Select Top 1 @__RowId = Id
							, @___IncludeServiceOrderDetailId = IncludeServiceOrderDetailId
							, @___SurgicalInterventionType = SurgicalInterventionType
							, @___CupsEntityId = CUPSEntityId
							, @___IPSServiceId = IPSServiceId
							, @___CareGroupId = CareGroupId
							, @___PerformsFunctionalUnitId = PerformsFunctionalUnitId
							, @___PerformsProfessionalSpecialty = PerformsProfessionalSpecialty
							, @___ServiceDate = ServiceDate
							, @___AllowValueChange = AllowValueChange
							, @___LiquidationType = LiquidationType
							, @___SurchargeApply = SurchargeApply
							, @___SubTotalSalesPrice = SubTotalSalesPrice_1
							, @___TotalSalesPrice = TotalSalesPrice
							, @___GrandTotalSalesPrice = GrandTotalSalesPrice_1
							, @___InvoicedQuantity = InvoicedQuantity
							, @___RateManualId = RateManualId
							, @___IsFirstEvent = IsFirstEvent
							, @___ServiceOrderDetailSurgicalXml = ServiceOrderDetailSurgicalXml
							, @___ThirdPartyDiscount = ThirdPartyDiscount_1
							, @___SurgeryNumber = SurgeryNumber							
						From @listServiceOrderDetails 
						Where SettlementType = @Uno And Id >= @__RowId Order By Id

						Set @__Rows = @@ROWCOUNT
						If @__Rows = 0 
							Break

						Declare @Index Int = 2

						If (Select Count(Id) From @listServiceOrderDetails 
							Where SurgeryNumber = @___SurgeryNumber And RateManualType < @Tres
								And SurgicalInterventionType Is Not Null And SurgicalInterventionType = @Tres) > 2 Begin

							If Exists (Select IsFirstEvent From @listServiceOrderDetails 
								Where SurgeryNumber = @___SurgeryNumber And RateManualType < @Tres
									And SurgicalInterventionType Is Not Null And SurgicalInterventionType = @Tres
									And IsFirstEvent = @Uno
								) Begin

								Delete From @listServiceOrderDetails Where IsFirstEvent = 1 And SurgeryNumber = @___SurgeryNumber
								Set @Index = 1
							End
							
							Declare @_Rows Int, @_RowId Int
							Set @_Rows = 1
							Set @_RowId = 1
							Declare @IncludeServiceOrderDetailIdz Int,
								@SubTotalSalesPricez Decimal(18, 2),
								@TotalSalesPricez Decimal(18, 2),
								@GrandTotalSalesPricez Decimal(18, 2),
								@ServiceOrderDetailSurgicalXmlz Xml

							While @_Rows > 0
							begin
								Select Top 1 @_RowId = Id
									, @IncludeServiceOrderDetailIdz = IncludeServiceOrderDetailId
									, @SubTotalSalesPricez = SubTotalSalesPrice_1
									, @TotalSalesPricez = TotalSalesPrice
									, @GrandTotalSalesPricez = GrandTotalSalesPrice_1
									, @ServiceOrderDetailSurgicalXmlz = ServiceOrderDetailSurgicalXml
								From @listServiceOrderDetails 
								Where SurgeryNumber = @___SurgeryNumber And RateManualType < @Tres
									And SurgicalInterventionType Is Not Null And SurgicalInterventionType = @Tres
									And Id >= @_RowId
								Order By Id

								Set @_Rows = @@ROWCOUNT
								If @_Rows = 0 
									Break

								If @IncludeServiceOrderDetailIdz Is Not Null And @IncludeServiceOrderDetailIdz > 0 And @_RowId <> @ServiceOrderDetailId
									Continue

								Set @SubTotalSalesPricez = 0
								Set @TotalSalesPricez = 0
								Set @GrandTotalSalesPricez = 0

								Declare @SurgicalRefresh Xml = (
									Select t.x.value('RowId[1]', 'Int') As RowId,
										t.x.value('Id[1]', 'Int') As Id,
										t.x.value('ServiceOrderDetailId[1]', 'Int') As ServiceOrderDetailId,
										t.x.value('CodeNameIpsService[1]', 'Varchar(320)') As CodeNameIpsService,
										t.x.value('IPSServiceId[1]', 'Int') As IPSServiceId,
										t.x.value('InvoicedQuantity[1]', 'Int') As InvoicedQuantity,
										t.x.value('LiquidationPercentage[1]', 'Numeric(5,2)') As LiquidationPercentage,
										t.x.value('RateManualSalePrice[1]', 'Numeric(18,2)') As RateManualSalePrice,
										0 As TotalSalesPrice,
										t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)') As PerformsHealthProfessionalCode,
										t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'Int') As PerformsHealthProfessionalThirdPartyId,
										t.x.value('CostValue[1]', 'Numeric(18,2)') As CostValue,
										t.x.value('BillingConceptId[1]', 'Int') As BillingConceptId,
										t.x.value('CostCenterId[1]', 'Int') As CostCenterId,
										t.x.value('RateManualDetailSurgicalId[1]', 'Int') As RateManualDetailSurgicalId,
										t.x.value('SurchargeApply[1]', 'Bit') As SurchargeApply,
										t.x.value('IncomeMainAccountId[1]', 'Int') As IncomeMainAccountId,
										t.x.value('ClassServiceIps[1]', 'Varchar(50)') As ClassServiceIps,
										t.x.value('RoundService[1]', 'int') As RoundService
									From @ServiceOrderDetailSurgicalXmlz.nodes('ServiceOrderDetailSurgicalXml') t(x)
									For Xml Path('ExcludeServiceOrderDetailSurgical'), Elements
								)
								Update @listServiceOrderDetails Set SubTotalSalesPrice_1 = @SubTotalSalesPricez
									, TotalSalesPrice = @TotalSalesPricez
									, GrandTotalSalesPrice_1 = @GrandTotalSalesPricez
									, ServiceOrderDetailSurgicalXml = @SurgicalRefresh
								Where Id = @_RowId

								Set @_RowId += 1
							End

						End

						Set @__RowId += 1
					End
					
					--Actualizamos Sods
					--Update sods Set GrandTotalSalesPrice = 0
					--From Billing.ServiceOrderDetailSurgical sods
					--Inner Join @listServiceOrderDetails sod On sods.ServiceOrderDetailId = sod.Id

					Declare @SurgicalAllToUpdate Table (
						RowId Int Identity(1,1) Primary Key,
						[Id] [int] NULL,
						ServiceOrderDetailId Int,
						[CodeNameIpsService] [varchar](320) Null,
						[IPSServiceId] [int] NOT NULL,
						[InvoicedQuantity] [int] NOT NULL,
						[LiquidationPercentage] [numeric](5, 2) NOT NULL,
						[RateManualSalePrice] [numeric](18, 2) NOT NULL,
						[TotalSalesPrice] [numeric](18, 2) NOT NULL,
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

					Set @__Rows = 1
					Set @__RowId = 1
					Declare @SodSurgicalXml Xml
					Delete From @SurgicalAllToUpdate

					While @__Rows > 0
					Begin
						Select Top 1 @__RowId = Id, @SodSurgicalXml = ServiceOrderDetailSurgicalXml 
						From @listServiceOrderDetails Where Id >= @__RowId Order By Id
						Set @__Rows = @@ROWCOUNT
						If @__Rows = 0 
							Break
			
						Insert Into @SurgicalAllToUpdate
							Select	t.x.value('Id[1]', 'Int'),
									t.x.value('ServiceOrderDetailId[1]', 'Int'),
									t.x.value('CodeNameIpsService[1]', 'Varchar(320)'),
									t.x.value('IPSServiceId[1]', 'Int'),
									t.x.value('InvoicedQuantity[1]', 'Int'),
									t.x.value('LiquidationPercentage[1]', 'Numeric(5,2)'),
									t.x.value('RateManualSalePrice[1]', 'Numeric(18,2)'),
									t.x.value('TotalSalesPrice[1]', 'Numeric(18,2)'),
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
							From @SodSurgicalXml.nodes('ExcludeServiceOrderDetailSurgical') t(x)

						Set @__RowId += 1
					End
					
					Update sods 
						Set TotalSalesPrice = sup.TotalSalesPrice,
							RoundService = sup.RoundService
					From Billing.ServiceOrderDetailSurgical sods
					Inner Join @SurgicalAllToUpdate sup On sods.Id = sup.Id
					
					Update sod 
						SET [ServiceOrderId] = dr.[ServiceOrderId]
						  ,[CareGroupId] = dr.[CareGroupId]
						  ,[HealthAdministratorId] = dr.[HealthAdministratorId]
						  ,[ThirdPartyId] = dr.[ThirdPartyId]
						  ,[ServiceType] = dr.[ServiceType]
						  ,[RecordType] = dr.[RecordType]
						  ,[CUPSEntityId] = dr.[CUPSEntityId]
						  ,[IPSServiceId] = dr.[IPSServiceId]
						  ,[HospitalStayId] = dr.[HospitalStayId]
						  ,[HospitalStayDetailId] = dr.[HospitalStayDetailId]
						  ,[ControlExternalConsultation] = dr.[ControlExternalConsultation]
						  ,[ControlExternalConsultationCode] = dr.[ControlExternalConsultationCode]
						  ,[CUPSAssociateService] = dr.[CUPSAssociateService]
						  ,[CodeAssociateService] = dr.[CodeAssociateService]
						  ,[IsPackage] = dr.[IsPackage]
						  ,[Packaging] = dr.[Packaging]
						  ,[PackageServiceOrderDetailId] = dr.[PackageServiceOrderDetailId]
						  ,[LiquidationType] = dr.[LiquidationType]
						  ,[Presentation] = dr.[Presentation]
						  ,[ProductId] = dr.[ProductId]
						  ,[InvoicedQuantity] = dr.[InvoicedQuantity]
						  ,[SupplyQuantity] = dr.[SupplyQuantity]
						  ,[DevolutionQuantity] = dr.[DevolutionQuantity]
						  ,[RateManualSalePrice] = dr.[RateManualSalePrice]
						  ,[CostValue] = dr.[CostValue]
						  ,[ServiceDate] = dr.[ServiceDate]
						  ,[AuthorizationNumber] = dr.[AuthorizationNumber]
						  ,[PerformsFunctionalUnitId] = dr.[PerformsFunctionalUnitId]
						  ,[PerformsHealthProfessionalCode] = dr.[PerformsHealthProfessionalCode]
						  ,[PerformsProfessionalSpecialty] = dr.[PerformsProfessionalSpecialty]
						  ,[PerformsHealthProfessionalThirdPartyId] = dr.[PerformsHealthProfessionalThirdPartyId]
						  ,[BillingConceptId] = dr.[BillingConceptId]
						  ,[CostCenterId] = dr.[CostCenterId]
						  ,[SettlementType] = dr.[SettlementType]
						  ,[IncludeServiceOrderDetailId] = dr.[IncludeServiceOrderDetailId]
						  ,[RecoveryRatio] = dr.[RecoveryRatio]
						  ,[RateManualId] = dr.[RateManualId]
						  ,[RateManualType] = dr.[RateManualType]
						  ,[RateManualDetailId] = dr.[RateManualDetailId]
						  ,[DefinitionRateDetailId] = dr.[DefinitionRateDetailId]
						  ,[DefinitionRateDetailConditionId] = dr.[DefinitionRateDetailConditionId]
						  ,[SubTotalSalesPrice] = dr.SubTotalSalesPrice_1
						  ,[ThirdPartyDiscount] = dr.ThirdPartyDiscount_1
						  ,[ThirdPartyDiscountPercentage] = dr.[ThirdPartyDiscountPercentage]
						  ,[TotalSalesPrice] = dr.[TotalSalesPrice]
						  ,[GrandTotalSalesPrice] = dr.GrandTotalSalesPrice_1
						  ,[SurchargeApply] = dr.[SurchargeApply]
						  ,[SurgicalInterventionType] = dr.[SurgicalInterventionType]
						  ,[SurgeryNumber] = dr.[SurgeryNumber]
						  ,[IsFirstEvent] = dr.[IsFirstEvent]
						  ,[IsAnnulled] = dr.[IsAnnulled]
						  ,[IsDelete] = dr.[IsDelete]
						  ,[IncomeMainAccountId] = dr.[IncomeMainAccountId]
						  ,[RoundService] = dr.[RoundService]
					From Billing.ServiceOrderDetail sod
					Inner Join @listServiceOrderDetails dr On sod.Id = dr.Id

					Update sodd Set GrandTotalSalesPrice = lsod.GrandTotalSalesPrice_1
						, ThirdPartySalesPrice = lsod.GrandTotalSalesPrice_1
						, SubTotalPatientSalesPrice = 0
						, PatientPercentage = 0
						, ThirdPartyPercentage = (Case When lsod.GrandTotalSalesPrice_1 = 0 Then 0 Else 100 End)
						, LastCaregroupId = lsod.CareGroupId
					From Billing.ServiceOrderDetailDistribution sodd
					Inner Join @listServiceOrderDetails lsod On sodd.ServiceOrderDetailId = lsod.Id
					Where sodd.GrandTotalSalesPrice <> lsod.GrandTotalSalesPrice_1
					
					Delete From @listServiceOrderDetail Where ServiceOrderDetailId In (Select Id From @listServiceOrderDetails Where Presentation = 2)
					Declare @listFoliosServiceOrder Table(RowId Int Identity(1,1) Primary Key, RevenueControlDetailId Int)
					Delete From @listFoliosServiceOrder

					Insert Into @listFoliosServiceOrder
					Select Distinct sodd.RevenueControlDetailId From Billing.ServiceOrderDetailDistribution sodd With(Nolock)
					Inner Join @listServiceOrderDetails lsod On sodd.ServiceOrderDetailId = lsod.Id

					----Actualizar La tabla ServiceOrderDetail
					--/********************************************/
					--Update sod Set GrandTotalSalesPrice = 0
					--From Billing.ServiceOrderDetail sod
					--Inner Join @listServiceOrderDetails lsod On sod.Id = lsod.Id
					--/********************************************/

					Declare @RevenueControlDetailId Int
					Set @__Rows = 1
					Set @__RowId = 1		
					While @__Rows > 0
					Begin
						Select Top 1 @__RowId = RowId, @RevenueControlDetailId = RevenueControlDetailId From @listFoliosServiceOrder Where RowId >= @__RowId Order By RowId
						Set @__Rows = @@ROWCOUNT
						If @__Rows = 0 
							Break

						Insert Into @UpdateFolio
						Exec Billing.SP_UpdateRevenueControlDetailValues @RevenueControlDetailId, NULL
						Set @__RowId += 1
					End
				End				
			End
			Else Begin
				
			Declare @Pr_StatusResult Bit, 
					@Pr_MessageResult Varchar(Max),
					@ProductRateDetailId Int,
					@ProductRateId Int,
					@ProductIdx Int,
					@InitialDate DateTime,
					@EndDate DateTime,
					@SalesValue Decimal(18, 2),
					@SalesValueWithSurcharge Decimal(18, 2),
					@ProductGrossValue NUMERIC(20,2),
					@ProductTaxValue numeric(20,2),
					@ProductIvaId INT = NULL

				Select @Pr_StatusResult = StatusResult
					, @Pr_MessageResult = MessageResult
					, @ProductRateDetailId = Id, @ProductRateId = ProductRateId
					, @ProductIdx = ProductId, @InitialDate = InitialDate
					, @EndDate = EndDate, @SalesValue = SalesValue
					, @SalesValueWithSurcharge = SalesValueWithSurcharge
					, @ProductGrossValue = GrossValue
					, @ProductTaxValue= TaxValue
				From [Billing].[GetProductRateDetail](@CareGroupId, @ProductId, @ServiceDate)

			If @Pr_StatusResult = 1 Begin
				Declare @Pr_rateManualSalePrice Decimal(18, 2) = 0
				If @SurchargeApply = 1 
					Set @Pr_rateManualSalePrice = @SalesValueWithSurcharge
				Else 
					Set @Pr_rateManualSalePrice = @SalesValue

				Select @ProductIvaId      = IIF(ip.TaxedProduct = 1 AND ip.LiquidateSalesTaxes = 1, ip.IVAId, NULL),
					   @ProductTaxValue   = IIF(ip.TaxedProduct = 1 AND ip.LiquidateSalesTaxes = 1, @ProductTaxValue, 0),
					   @ProductGrossValue = IIF(ip.TaxedProduct = 1 AND ip.LiquidateSalesTaxes = 1, @ProductGrossValue, @Pr_rateManualSalePrice)
				From Inventory.InventoryProduct ip WITH(NOLOCK)
				Where ip.Id = @ProductId

				Set @RateManualSalePrice = @Pr_rateManualSalePrice
					Set @SubTotalSalesPrice_1 = @Pr_rateManualSalePrice
					Set @ThirdPartyDiscount_1 = Round(@SubTotalSalesPrice_1 * (@ThirdPartyDiscountPercentage / 100), 0)
					Set @TotalSalesPrice = (@RateManualSalePrice - @ThirdPartyDiscount_1)
					Set @GrandTotalSalesPrice_1 = (@TotalSalesPrice * @Quantity)
					Set @GrandTotalSalesPrice = @GrandTotalSalesPrice_1
					Set @ThirdPartySalesPrice = @GrandTotalSalesPrice_1
					Set @ThirdPartyPercentage = 100
					Set @ApplyRecoveryFee = 1
					Set @RecoveryFeeType = 1
					Set @SubTotalPatientSalesPrice = 0
					Set @PatientPercentage = 0
					Set @LastCaregroupId = @CareGroupId

					Update @ListServiceorderDetail 
						Set	RateManualSalePrice = @RateManualSalePrice,
							SubTotalSalesPrice_1 = @SubTotalSalesPrice_1,
							ThirdPartyDiscount_1 = @ThirdPartyDiscount_1,
							TotalSalesPrice = @TotalSalesPrice,
							GrandTotalSalesPrice_1 = @GrandTotalSalesPrice_1,
							GrandTotalSalesPrice = @GrandTotalSalesPrice,
							ThirdPartySalesPrice = @ThirdPartySalesPrice,
							ThirdPartyPercentage = @ThirdPartyPercentage,
							ApplyRecoveryFee = @ApplyRecoveryFee,
							RecoveryFeeType = @RecoveryFeeType,
							SubTotalPatientSalesPrice = @SubTotalPatientSalesPrice,
							PatientPercentage = @PatientPercentage,
							LastCaregroupId = @LastCaregroupId,
							GrossValue =@ProductGrossValue,
							TaxValue = @ProductTaxValue,
							IvaId = @ProductIvaId
				Where Id = @RowId
				End
				Else
					Set @validateErrors += Char(13) + Char(10) + @Pr_MessageResult

			End

			Set @RowId += 1
		End

		Update sodd Set GrandTotalSalesPrice = lsod.GrandTotalSalesPrice_1
			, ThirdPartySalesPrice = lsod.GrandTotalSalesPrice_1
			, SubTotalPatientSalesPrice = 0
			, PatientPercentage = 0
			, ThirdPartyPercentage = (Case When lsod.GrandTotalSalesPrice_1 = 0 Then 0 Else 100 End)
			, LastCaregroupId = lsod.CareGroupId
		From Billing.ServiceOrderDetailDistribution sodd
		Inner Join @listServiceOrderDetails lsod On sodd.ServiceOrderDetailId = lsod.Id
		Where sodd.GrandTotalSalesPrice <> lsod.GrandTotalSalesPrice_1

		--Actualizar La tabla ServiceOrderDetail
		--/********************************************/
		--Update sod Set GrandTotalSalesPrice = 0
		--From Billing.ServiceOrderDetail sod
		--Inner Join @listServiceOrderDetails lsod On sod.Id = lsod.Id
		--/********************************************/
				
		--select sod.InvoicedQuantity, sod.TotalSalesPrice, ROUND(sod.InvoicedQuantity * sod.TotalSalesPrice,0), i.GrandTotalSalesPrice 
		--	, ROUND(sod.InvoicedQuantity * sod.TotalSalesPrice,-2), i.GrandTotalSalesPrice
		--from @listServiceOrderDetail i inner join Billing.ServiceOrderDetail sod on i.ServiceOrderDetailId = sod.Id where i.DistributionType = 1 and (ROUND(sod.InvoicedQuantity * sod.TotalSalesPrice,0) <> i.GrandTotalSalesPrice and ROUND(sod.InvoicedQuantity * sod.TotalSalesPrice,-2) <> i.GrandTotalSalesPrice)
		--select * from @listServiceOrderDetail
		--select * from @listServiceOrderDetails

		--actualizar los surgical que tienen la tabla
								--select * from @listServiceOrderDetails where Id = @__RowId

		Update sod 
			SET [ServiceOrderId] = dr.[ServiceOrderId]
			  ,[CareGroupId] = dr.[CareGroupId]
			  ,[HealthAdministratorId] = dr.[HealthAdministratorId]
			  ,[ThirdPartyId] = dr.[ThirdPartyId]
			  ,[ServiceType] = dr.[ServiceType]
			  ,[RecordType] = dr.[RecordType]
			  ,[CUPSEntityId] = dr.[CUPSEntityId]
			  ,[IPSServiceId] = dr.[IPSServiceId]
			  ,[HospitalStayId] = dr.[HospitalStayId]
			  ,[HospitalStayDetailId] = dr.[HospitalStayDetailId]
			  ,[ControlExternalConsultation] = dr.[ControlExternalConsultation]
			  ,[ControlExternalConsultationCode] = dr.[ControlExternalConsultationCode]
			  ,[CUPSAssociateService] = dr.[CUPSAssociateService]
			  ,[CodeAssociateService] = dr.[CodeAssociateService]
			  ,[IsPackage] = dr.[IsPackage]
			  ,[Packaging] = dr.[Packaging]
			  ,[PackageServiceOrderDetailId] = dr.[PackageServiceOrderDetailId]
			  ,[LiquidationType] = dr.[LiquidationType]
			  ,[Presentation] = dr.[Presentation]
			  ,[ProductId] = dr.[ProductId]
			  ,[InvoicedQuantity] = dr.[InvoicedQuantity]
			  ,[SupplyQuantity] = dr.[SupplyQuantity]
			  ,[DevolutionQuantity] = dr.[DevolutionQuantity]
			  ,[RateManualSalePrice] = dr.[RateManualSalePrice]
			  ,[CostValue] = dr.[CostValue]
			  ,[ServiceDate] = dr.[ServiceDate]
			  ,[AuthorizationNumber] = dr.[AuthorizationNumber]
			  ,[PerformsFunctionalUnitId] = dr.[PerformsFunctionalUnitId]
			  ,[PerformsHealthProfessionalCode] = dr.[PerformsHealthProfessionalCode]
			  ,[PerformsProfessionalSpecialty] = dr.[PerformsProfessionalSpecialty]
			  ,[PerformsHealthProfessionalThirdPartyId] = dr.[PerformsHealthProfessionalThirdPartyId]
			  ,[BillingConceptId] = dr.[BillingConceptId]
			  ,[CostCenterId] = dr.[CostCenterId]
			  ,[SettlementType] = dr.[SettlementType]
			  ,[IncludeServiceOrderDetailId] = dr.[IncludeServiceOrderDetailId]
			  ,[RecoveryRatio] = dr.[RecoveryRatio]
			  ,[RateManualId] = dr.[RateManualId]
			  ,[RateManualType] = dr.[RateManualType]
			  ,[RateManualDetailId] = dr.[RateManualDetailId]
			  ,[DefinitionRateDetailId] = dr.[DefinitionRateDetailId]
			  ,[DefinitionRateDetailConditionId] = dr.[DefinitionRateDetailConditionId]
			  ,[SubTotalSalesPrice] = dr.SubTotalSalesPrice_1
			  ,[ThirdPartyDiscount] = dr.ThirdPartyDiscount_1
			  ,[ThirdPartyDiscountPercentage] = dr.[ThirdPartyDiscountPercentage]
			  ,[TotalSalesPrice] = dr.[TotalSalesPrice]
			  ,[GrandTotalSalesPrice] = dr.GrandTotalSalesPrice_1
			  ,[SurchargeApply] = dr.[SurchargeApply]
			  ,[SurgicalInterventionType] = dr.[SurgicalInterventionType]
			  ,[SurgeryNumber] = dr.[SurgeryNumber]
			  ,[IsFirstEvent] = dr.[IsFirstEvent]
			  ,[IsAnnulled] = dr.[IsAnnulled]
			  ,[IsDelete] = dr.[IsDelete]
			  ,[IncomeMainAccountId] = dr.[IncomeMainAccountId]
			  ,[RoundService] = dr.[RoundService]
			  ,[GrossValue] = dr.[GrossValue]
			  ,[TaxValue] = dr.[TaxValue]
			  ,[IvaId] = dr.[IvaId]
		From Billing.ServiceOrderDetail sod
		Inner Join @listServiceOrderDetail dr On sod.Id = dr.ServiceOrderDetailId

		Update sod
			Set sod.GrossValue = sod.GrossValue + sod.TaxValue
			  , sod.TaxValue   = 0
			  , sod.IvaId      = Null
		From Billing.ServiceOrderDetail sod
		Inner Join @listServiceOrderDetail dr On sod.Id = dr.ServiceOrderDetailId
		Inner Join Inventory.InventoryProduct ip With(Nolock) On ip.Id = dr.ProductId
		Where dr.ProductId Is Not Null
		  And Not (ip.TaxedProduct = 1 And ip.LiquidateSalesTaxes = 1)
		  And (sod.TaxValue > 0 Or sod.IvaId Is Not Null)
		
		Update sodd Set sodd.RevenueControlDetailId = dr.RevenueControlDetailId
			, sodd.ServiceOrderDetailId = dr.ServiceOrderDetailId
			, sodd.Quantity = dr.Quantity
			, sodd.GrandTotalSalesPrice = dr.GrandTotalSalesPrice
			, sodd.GrandTotalDiscount = dr.GrandTotalDiscount
			, sodd.DistributionType = dr.DistributionType
			, sodd.ThirdPartySalesPrice = dr.ThirdPartySalesPrice
			, sodd.ThirdPartyPercentage = dr.ThirdPartyPercentage
			, sodd.ApplyRecoveryFee = dr.ApplyRecoveryFee
			, sodd.RecoveryFeeType = dr.RecoveryFeeType
			, sodd.SubTotalPatientSalesPrice = dr.SubTotalPatientSalesPrice
			, sodd.PatientPercentage = dr.PatientPercentage
			, sodd.LastCaregroupId = dr.LastCaregroupId
		From Billing.ServiceOrderDetailDistribution sodd
		Inner Join @listServiceOrderDetail dr On sodd.Id = dr.Id

		Insert Into @UpdateFolio
		Exec Billing.SP_UpdateRevenueControlDetailValues @FolioId, NULL

		Select Convert(Bit, 1) As [StatusResult], '' As [MessageResult]

	End Try
	Begin Catch
		Select Convert(Bit, 0) As [StatusResult], ERROR_MESSAGE() As [MessageResult]
	End Catch
	
End
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de facturación que excluye uno o varios ítems de servicio (procedimientos, medicamentos, insumos o estancias) de un folio o cuenta de cobro activo. Recibe como parámetro un XML con los identificadores de los detalles de orden de servicio a excluir, junto con datos del paciente y del grupo de atención, y opera sobre las tablas de detalle de orden de servicio (ServiceOrderDetail) y su distribución financiera (ServiceOrderDetailDistribution), revirtiendo o desvinculando los registros correspondientes del folio indicado. Es utilizado en el proceso de ajuste de facturación cuando un servicio facturado debe retirarse de la cuenta antes de su cierre o envío al tercero pagador (EPS, aseguradora o particular), garantizando que la distribución de valores entre el tercero y el paciente quede correctamente actualizada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ExcludeOutService';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ExcludeOutService';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Excluye/retarifica un conjunto de detalles de orden de servicio recalculando sus valores (servicios o productos), recomponiendo los eventos quirúrgicos relacionados y actualizando la distribución y los folios afectados.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ExcludeOutService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los ServiceOrderDetailId enviados en el XML deben existir y tener registro en Billing.ServiceOrderDetailDistribution; El folio asociado (RevenueControlDetail) no debe estar facturado (Status=2), en Reconocimiento de Ingresos (Status=5) ni bloqueado (Status>1); Los servicios relacionados al mismo SurgeryNumber no deben estar empaquetados (IsPackage=1) ni distribuidos en más de un detalle de distribución; Para servicios (RecordType=1) se requiere AdmissionNumber válido en Billing.ServiceOrder y que Contract.GetValueService retorne StatusResult=1; Para productos (RecordType<>1) Billing.GetProductRateDetail debe retornar StatusResult=1 con tarifa vigente para el CareGroup, Producto y ServiceDate; Debe existir configuración en Contract.SurgeriesPercentageManual para el RateManualId e InterventionType cuando se recalculan eventos quirúrgicos no básicos ni cruentos', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ExcludeOutService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de servicio; Detalle de orden de servicio; Distribución de servicio; Folio (RevenueControlDetail); Servicio quirúrgico; Evento quirúrgico (cirujano, anestesiólogo, ayudante, derecho de sala, materiales sutura, instrumentación); Tarifa manual; Servicio IPS / CUPS; Producto / medicamento; Liquidación / retarificación; Empaquetado de servicios; Estado del folio (facturado, reconocimiento de ingresos, bloqueado); Recargo (SurchargeApply); Porcentajes quirúrgicos por tipo de intervención', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ExcludeOutService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Billing.ServiceOrderDetail: Tras retarificar/recalcular cada detalle, se sobrescriben todas las columnas operativas (valores, cantidades, tarifas, IVA, GrossValue, TaxValue, RoundService, etc.) desde la tabla en memoria @listServiceOrderDetail/@listServiceOrderDetails uniendo por Id; [UPDATE] Billing.ServiceOrderDetailDistribution: Cuando GrandTotalSalesPrice difiere del nuevo GrandTotalSalesPrice_1 calculado, se actualiza GrandTotalSalesPrice y ThirdPartySalesPrice al nuevo valor, SubTotalPatientSalesPrice=0, PatientPercentage=0, ThirdPartyPercentage=100 (o 0 si el total es 0) y LastCaregroupId=CareGroupId del detalle; [UPDATE] Billing.ServiceOrderDetailDistribution: Al final del flujo se reescriben todas las columnas de distribución (Quantity, descuentos, tipos, porcentajes, etc.) desde @listServiceOrderDetail por Id de la distribución; [UPDATE] Billing.ServiceOrderDetailSurgical: Para cada evento quirúrgico recalculado se actualizan TotalSalesPrice y RoundService desde el resultado de Billing.RecalculateSurgicalEvents y la aplicación de los porcentajes de Contract.SurgeriesPercentageManual; [EXECUTE] Billing.RevenueControlDetail: Por cada RevenueControlDetailId afectado por servicios quirúrgicos modificados se ejecuta Billing.SP_UpdateRevenueControlDetailValues; al final se ejecuta también para @FolioId; [RETURN_RESULT] ResultSet: Devuelve StatusResult=0 con mensaje cuando un servicio relacionado está empaquetado, está distribuido en más de un detalle, o el folio está facturado/en reconocimiento de ingresos/bloqueado; StatusResult=1 con mensaje vacío cuando todo el proceso termina; StatusResult=0 con ERROR_MESSAGE() ante excepción; [RETURN_RESULT] ResultSet: Si Contract.GetValueService o Billing.GetProductRateDetail retornan StatusResult<>1, no se aborta: se acumula en @validateErrors el mensaje (incluyendo ''No se ha encontrado valor para el servicio <Code - Name>'' para IPS) y se continúa con el siguiente registro', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ExcludeOutService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_UpdateRevenueControlDetailValues; Contract.GetValueService; Contract.SetServiceValue; Billing.RecalculateSurgicalEvents; Billing.GetProductRateDetail; Billing.RoundValue', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ExcludeOutService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail; Contract.CUPSEntityContractDescriptions; Billing.ServiceOrderDetailSurgical; Billing.ServiceOrder; Contract.IPSService; Contract.CareGroup; Contract.CUPSEntity; Payroll.FunctionalUnit; Payroll.CostCenter; Common.ThirdParty; Inventory.InventoryProduct; Contract.HealthAdministrator; Contract.SurgeriesPercentageManual; Contract.RateManual; Billing.RevenueControlDetail', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ExcludeOutService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ExcludeOutService';
-- GO
