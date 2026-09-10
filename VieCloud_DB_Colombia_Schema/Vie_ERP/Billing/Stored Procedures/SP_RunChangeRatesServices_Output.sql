-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-05-01
-- Description:	Retarifica un listado de servicios
-- =============================================

CREATE Procedure [Billing].[SP_RunChangeRatesServices_Output]
	@RevenueControlDetailId Int,
	@CareGroupId Int,
	@OnlyRateChange Bit,
	@ListHomologationsXml Xml,
	@PatientGenus Int,
	@PatientBirth DateTime,
	@distributionToRetarificXml Xml,
	@retarificServiceOrdenDetailSurgicalXml Xml,
	@StatusResultOut Bit Output,
	@MessageResultOut Varchar(Max) Output,
	@DistributionToRetarificOut Xml Output,
	@ListNewServiceOrderDetailOut Xml Output
AS
Begin

	Set Nocount On;
	Begin Try
	
		Declare @myErrorListResult Varchar(Max) = ''

		--Listado de serviceorderdetail a devolver pues si se envia varias homologaciones se pueden retornar varios
		Declare @listNew Table (RowId Int Primary Key Identity(1, 1),
			Id Int,
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
			RateManualSalePrice Decimal(20,2),
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
			SubTotalSalesPrice Decimal(20, 2),
			ThirdPartyDiscount Decimal(20, 2),
			ThirdPartyDiscountPercentage Decimal(5, 2),
			TotalSalesPrice Decimal(20, 2),
			GrandTotalSalesPrice Decimal(20,2),
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
			ServiceOrderDetailSurgicalXml Xml Null)
		
		Declare @listHomologations Table ([Service] Xml, Homologations Xml)
		
		IF NOT EXISTS (SELECT 1 FROM Billing.RevenueControlDetail WHERE Id = @RevenueControlDetailId AND Status IN (1)) 
		BEGIN
			SELECT @MessageResultOut = CONCAT('El folio se encuentra ', CASE Status
																			WHEN 2 THEN 'Facturado'
																			WHEN 3 THEN 'Bloquado'
																			WHEN 4 THEN 'Anulado'
																			WHEN 5 THEN 'Reconocimiento Ingresos'
																			WHEN 6 THEN 'Factura Asociada'
																		END)
			FROM Billing.RevenueControlDetail
			WHERE Id = @RevenueControlDetailId

			Set @StatusResultOut = 0
			Set @MessageResultOut = ISNULL(@MessageResultOut, 'El folio no existe')
			Set @DistributionToRetarificOut = Null
			Set @ListNewServiceOrderDetailOut = Null
			RETURN
		END

		Insert Into @ListHomologations
		Select t.x.query('Service')
			, t.x.query('Homologations')
		From @ListHomologationsXml.nodes('/Homologacion') t(x)
		
		Declare @distributionToRetarific Table(Id Int Primary Key, 
				RevenueControlDetailId Int,
				ServiceOrderDetailId Int,
				Quantity Int,
				GrandTotalSalesPrice Decimal(20,2),
				GrandTotalDiscount Decimal(20,2),
				DistributionType Tinyint,
				ThirdPartySalesPrice Decimal(20,2),
				ThirdPartyPercentage Decimal(5, 2),
				ApplyRecoveryFee Tinyint,
				RecoveryFeeType Tinyint,
				SubTotalPatientSalesPrice Decimal(20,2),
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
				RateManualSalePrice Decimal(20,2),
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
				ServiceOrderDetailSurgicalXml Xml Null,
				RIASId int null,
				ContractDescriptionId int null,
				RoundService int,
				-----------------------------------------
				GrossValue					numeric(20,2),
				TaxValue					numeric(20,2),
				IvaId						int)
				
		Insert Into @distributionToRetarific
		Select t.x.value('Id[1]','Int'),
			t.x.value('RevenueControlDetailId[1]','Int'),
			t.x.value('ServiceOrderDetailId[1]','Int'),
			t.x.value('Quantity[1]','Int'),
			t.x.value('GrandTotalSalesPrice[1]','Decimal(20,2)'),
			t.x.value('GrandTotalDiscount[1]','Decimal(20,2)'),
			t.x.value('DistributionType[1]','Tinyint'),
			t.x.value('ThirdPartySalesPrice[1]','Decimal(20,2)'),
			t.x.value('ThirdPartyPercentage[1]','Decimal(5, 2)'),
			t.x.value('ApplyRecoveryFee[1]','Tinyint'),
			t.x.value('RecoveryFeeType[1]','Tinyint'),
			t.x.value('SubTotalPatientSalesPrice[1]','Decimal(20,2)'),
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
			t.x.value('RateManualSalePrice[1]','Decimal(20,2)'),
			t.x.value('CostValue[1]','Decimal(18, 2)'),
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
			Null,
			t.x.value('RIASId[1]','Int'),
			t.x.value('ContractDescriptionId[1]','Int'),
			t.x.value('RoundService[1]','Int'),
			---------------------------------------------------
			t.x.value('GrossValue[1]','numeric(20,2)'),
			t.x.value('TaxValue[1]','numeric(20,2)'),
			t.x.value('IvaId[1]','Int')
		From @distributionToRetarificXml.nodes('/DistributionToRetarific') t(x)

		Declare @RetarificServiceOrderDetailSurgical Table(
			RowId Int Primary Key Identity(1, 1),
			[Id] [int] NULL,
			ServiceOrderDetailId Int,
			[CodeNameIpsService] [varchar](320) Null,
			[IPSServiceId] [int] NOT NULL,
			[InvoicedQuantity] [int] NOT NULL,
			[LiquidationPercentage] [numeric](5, 2) NOT NULL,
			[RateManualSalePrice] [numeric](20,2) NOT NULL,
			[TotalSalesPrice] [numeric](20,2) NOT NULL,
			[PerformsHealthProfessionalCode] [char](20) NULL,
			[PerformsHealthProfessionalThirdPartyId] [int] NULL,
			[CostValue] [numeric](18, 2) NOT NULL,
			[BillingConceptId] [int] NOT NULL,
			[CostCenterId] [int] NOT NULL,
			[RateManualDetailSurgicalId] [int] NULL,
			[SurchargeApply] [bit] NOT NULL,
			[IncomeMainAccountId] [int] NOT NULL,
			RoundService int
		)
	
		Insert Into @RetarificServiceOrderDetailSurgical
		Select t.x.value('Id[1]', 'Int'),
			t.x.value('ServiceOrderDetailId[1]', 'Int'),
			t.x.value('CodeNameIpsService[1]', 'Varchar(320)'),
			t.x.value('IPSServiceId[1]', 'Int'),
			t.x.value('InvoicedQuantity[1]', 'Int'),
			t.x.value('LiquidationPercentage[1]', 'Decimal(5, 2)'),
			t.x.value('RateManualSalePrice[1]', 'Decimal(20,2)'),
			t.x.value('TotalSalesPrice[1]', 'Decimal(20,2)'),
			t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)'),
			t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'Int'),
			t.x.value('CostValue[1]', 'Decimal(18, 2)'),
			t.x.value('BillingConceptId[1]', 'Int'),
			t.x.value('CostCenterId[1]', 'Int'),
			t.x.value('RateManualDetailSurgicalId[1]', 'Int'),
			t.x.value('SurchargeApply[1]', 'Bit'),
			t.x.value('IncomeMainAccountId[1]', 'Int'),
			t.x.value('RoundService[1]', 'Int')
		From @retarificServiceOrdenDetailSurgicalXml.nodes('/RetarificServiceOrderDetailSurgical') t(x)
		
		--select '@distributionToRetarific', * from @distributionToRetarific
		--select '@listHomologations', * from @listHomologations
		--select '@RetarificServiceOrderDetailSurgical', * from @RetarificServiceOrderDetailSurgical

		Declare @_ServiceOrderDetailDistributionId Int, 
			@_RevenueControlDetailId Int,
			@_ServiceOrderDetailId Int,
			@_Quantity Int,
			@_GrandTotalSalesPrice Decimal(20, 2),
			@_GrandTotalDiscount Decimal(20, 2),
			@_DistributionType Tinyint,
			@_ThirdPartySalesPrice Decimal(20, 2),
			@_ThirdPartyPercentage Decimal(5, 2),
			@_ApplyRecoveryFee Tinyint,
			@_RecoveryFeeType Tinyint,
			@_SubTotalPatientSalesPrice Decimal(20, 2),
			@_PatientPercentage Decimal(5, 2),
			@_LastCaregroupId Int,
			@_ServiceOrderId Int,
			@_AdmissionNumber CHAR(10),
			@_CareGroupId Int,
			@_HealthAdministratorId Int,
			@_ThirdPartyId Int,
			@_ServiceType Tinyint,
			@_RecordType Tinyint,
			@_CUPSEntityId Int,
			@_IPSServiceId Int,
			@_HospitalStayId Int,
			@_HospitalStayDetailId Int,
			@_ControlExternalConsultation Tinyint,
			@_ControlExternalConsultationCode Decimal(18, 0),
			@_CUPSAssociateService Bit,
			@_CodeAssociateService Varchar(50),
			@_IsPackage Bit,
			@_Packaging Bit,
			@_PackageServiceOrderDetailId Int,
			@_LiquidationType Tinyint,
			@_Presentation Tinyint,
			@_ProductId Int,
			@_InvoicedQuantity Int,
			@_SupplyQuantity Int,
			@_DevolutionQuantity Int,
			@_RateManualSalePrice Decimal(20, 2),
			@_CostValue Decimal(18, 2),
			@_ServiceDate DateTime,
			@_AuthorizationNumber Varchar(20),
			@_PerformsFunctionalUnitId Int,
			@_PerformsHealthProfessionalCode Char(20),
			@_PerformsProfessionalSpecialty Char(3),
			@_PerformsHealthProfessionalThirdPartyId Int,
			@_BillingConceptId Int,
			@_CostCenterId Int,
			@_SettlementType Tinyint,
			@_IncludeServiceOrderDetailId Int,
			@_RecoveryRatio Decimal(5, 2),
			@_RateManualId Int,
			@_RateManualType Tinyint,
			@_RateManualDetailId Int,
			@_DefinitionRateDetailId Int,
			@_DefinitionRateDetailConditionId Int,
			@_SubTotalSalesPrice_1 Decimal(20, 2),
			@_ThirdPartyDiscount_1 Decimal(20, 2),
			@_ThirdPartyDiscountPercentage Decimal(5, 2),
			@_TotalSalesPrice Decimal(18, 2),
			@_GrandTotalSalesPrice_1 Decimal(18, 2),
			@_SurchargeApply Bit,
			@_SurgicalInterventionType Tinyint,
			@_SurgeryNumber Tinyint,
			@_IsFirstEvent Bit,
			@_IsAnnulled Bit,
			@_IsDelete Bit,
			@_IncomeMainAccountId Int,
			@_ServiceOrderDetailSurgicalId Int,		
			@_CodeNameSpeciality Varchar(300),
			@_CodeNameFunctionalUnit Varchar(300),
			@_CodeNameHealthAdministrator Varchar(300),				
			@_PreviusServiceOrderDetailId Int,				
			@_CodeNameCareGroup Varchar(320),
			@_CodeNameCostCenter Varchar(300),
			@_CodeNameCups Varchar(320),
			@_CodeNameHealthProfessional Varchar(300),
			@_CodeNameIpsService Varchar(300),
			@_CodeNameProduct Varchar(300),				
			@_IsSOAT Bit,
			@_RIASId Int,
			@_ContractDescriptionId Int,
			----------------------------------------
			@_GrossValue numeric(20,2),
			@_TaxValue	numeric(20,2),
			@_IvaId		int
		

		Declare @Rows Int, @RowId Int,
			@Rowsx Int, @RowIdx Int,
			@Rowsy Int, @RowIdy Int

		Set @Rows = 1
		Set @RowId = 1

		Declare @tmp Table(RowId INT IDENTITY(1,1) PRIMARY KEY CLUSTERED, InvoiceId Int, invoiceNumber Varchar(20))

		Insert Into @tmp
		Select Id, InvoiceNumber FROM Billing.Invoice

		While @Rows > 0
		begin

			Select Top 1 @RowId = Id, @_ServiceOrderDetailDistributionId = Id, @_RevenueControlDetailId = RevenueControlDetailId, @_ServiceOrderDetailId= ServiceOrderDetailId
				, @_Quantity = Quantity, @_GrandTotalSalesPrice = GrandTotalSalesPrice, @_GrandTotalDiscount = GrandTotalDiscount, @_DistributionType = DistributionType
				, @_ThirdPartySalesPrice = ThirdPartySalesPrice, @_ThirdPartyPercentage = ThirdPartyPercentage, @_ApplyRecoveryFee = ApplyRecoveryFee, @_RecoveryFeeType = RecoveryFeeType
				, @_SubTotalPatientSalesPrice = SubTotalPatientSalesPrice, @_PatientPercentage = PatientPercentage, @_LastCaregroupId = LastCaregroupId, @_ServiceOrderId = ServiceOrderId
				, @_CareGroupId = CareGroupId, @_HealthAdministratorId = HealthAdministratorId, @_ThirdPartyId = ThirdPartyId, @_ServiceType = ServiceType, @_RecordType = RecordType
				, @_CUPSEntityId = CUPSEntityId, @_IPSServiceId = IPSServiceId, @_HospitalStayId = HospitalStayId, @_HospitalStayDetailId = HospitalStayDetailId, @_ControlExternalConsultation =ControlExternalConsultation
				, @_ControlExternalConsultationCode = ControlExternalConsultationCode, @_CUPSAssociateService = CUPSAssociateService, @_CodeAssociateService = CodeAssociateService, @_IsPackage = IsPackage
				, @_Packaging = Packaging, @_PackageServiceOrderDetailId = PackageServiceOrderDetailId, @_LiquidationType = LiquidationType, @_Presentation = Presentation, @_ProductId = ProductId
				, @_InvoicedQuantity = InvoicedQuantity, @_SupplyQuantity = SupplyQuantity, @_DevolutionQuantity = DevolutionQuantity, @_RateManualSalePrice = RateManualSalePrice, @_CostValue = CostValue
				, @_ServiceDate = ServiceDate, @_AuthorizationNumber = AuthorizationNumber, @_PerformsFunctionalUnitId = PerformsFunctionalUnitId, @_PerformsHealthProfessionalCode = PerformsHealthProfessionalCode
				, @_PerformsProfessionalSpecialty = PerformsProfessionalSpecialty, @_PerformsHealthProfessionalThirdPartyId = PerformsHealthProfessionalThirdPartyId, @_BillingConceptId = BillingConceptId
				, @_CostCenterId = CostCenterId, @_SettlementType = SettlementType, @_IncludeServiceOrderDetailId = IncludeServiceOrderDetailId, @_RecoveryRatio = RecoveryRatio, @_RateManualId = RateManualId
				, @_RateManualType = RateManualType, @_RateManualDetailId = RateManualDetailId, @_DefinitionRateDetailId = DefinitionRateDetailId, @_DefinitionRateDetailConditionId = DefinitionRateDetailConditionId
				, @_SubTotalSalesPrice_1 = SubTotalSalesPrice_1, @_ThirdPartyDiscount_1 = ThirdPartyDiscount_1, @_ThirdPartyDiscountPercentage = ThirdPartyDiscountPercentage, @_TotalSalesPrice = TotalSalesPrice
				, @_GrandTotalSalesPrice_1 = GrandTotalSalesPrice_1, @_SurchargeApply = SurchargeApply, @_SurgicalInterventionType = SurgicalInterventionType, @_SurgeryNumber = SurgeryNumber, @_IsFirstEvent = IsFirstEvent
				, @_IsAnnulled = IsAnnulled, @_IsDelete = IsDelete, @_IncomeMainAccountId = IncomeMainAccountId, @_ServiceOrderDetailSurgicalId = ServiceOrderDetailSurgicalId, @_CodeNameSpeciality = CodeNameSpeciality
				, @_CodeNameFunctionalUnit = CodeNameFunctionalUnit, @_CodeNameHealthAdministrator = CodeNameHealthAdministrator, @_PreviusServiceOrderDetailId = PreviusServiceOrderDetailId
				, @_CodeNameCareGroup = CodeNameCareGroup, @_CodeNameCostCenter = CodeNameCostCenter, @_CodeNameCups = CodeNameCups, @_CodeNameHealthProfessional = CodeNameHealthProfessional
				, @_CodeNameIpsService = CodeNameIpsService, @_CodeNameProduct = CodeNameProduct, @_IsSOAT = IsSOAT, @_RIASId = RIASId, @_ContractDescriptionId = ContractDescriptionId
				, @_GrossValue = GrossValue,@_TaxValue=TaxValue,@_IvaId=IvaId
			From @distributionToRetarific
			Where (@OnlyRateChange = 1 OR LastCaregroupId <> @CareGroupId) And DistributionType <> 2 And DistributionType <> 5 And Id >= @RowId Order By Id
			
			Set @Rows = @@ROWCOUNT
			If @Rows = 0 
				Break

			Declare @retarificXml Xml = Null
			SELECT @_AdmissionNumber = AdmissionNumber
			FROM Billing.ServiceOrder
			WHERE Id = @_ServiceOrderId
			
			--enviar solo el retarific que se esta recorriendo			
			If @_CUPSAssociateService = 0 Begin --Servicios
				If @_RecordType = 1 Begin
					
					Set @_PreviusServiceOrderDetailId = @_ServiceOrderDetailId
					Update @distributionToRetarific Set PreviusServiceOrderDetailId = @_ServiceOrderDetailId
					Where Id = @_ServiceOrderDetailDistributionId
					
					If (Select Count(1) From @listHomologations Where [Service].query('Service/ServiceOrderDetailId').value('ServiceOrderDetailId[1]', 'Int') = @_ServiceOrderDetailId) > 0 Begin						
						Declare @IsFirst Bit = 1						
						
						If (Select Count(*)
							From @listHomologations 
							Where [Service].query('Service/ServiceOrderDetailId').value('ServiceOrderDetailId[1]', 'Int') = @_ServiceOrderDetailId 
								And Homologations.exist('Homologations/CupsHomologation/Activated[.="1"]') = 1) > 0 Begin
							
							
							Declare @HomologationServiceXml Xml = (
								Select Top 1 Homologations From @listHomologations 
								Where [Service].query('Service/ServiceOrderDetailId').value('ServiceOrderDetailId[1]', 'Int') = @_ServiceOrderDetailId
							)
																					
							Declare @HomologationService Table(
								RowId Int Identity(1,1) Primary Key
								, CupsHomologationId Int
								, IPSServiceId Int
								, CupsEntityId Int
								, GuidHomologation Varchar(50))
							Declare @CupsHomologationId Int,
								@IPSServiceIdH Int,
								@CupsEntityIdH Int,
								@GuidHomologation Varchar(50)
							
							Delete From @HomologationService
							Insert Into @HomologationService
							Select t.x.value('CupsHomologationId[1]', 'Int')
								, t.x.value('IPSServiceId[1]', 'Int')
								, t.x.value('CupsEntityId[1]', 'Int')
								, t.x.value('GuidHomologation[1]', 'Varchar(50)')
							From @HomologationServiceXml.nodes('/Homologations/CupsHomologation') t(x)
							Where t.x.value('Activated[1]', 'Bit') = 1
							
							Set @Rowsx = 1
							Set @RowIdx = 1
							
							While @Rowsx > 0
							begin								
								Select Top 1 @RowIdx = RowId, @CupsHomologationId = CupsHomologationId, @IPSServiceIdH = IPSServiceId
									, @CupsEntityIdH = CupsEntityId, @GuidHomologation = GuidHomologation 
								From @HomologationService
								Where RowId >= @RowIdx Order By RowId

								Set @Rowsx = @@ROWCOUNT
								If @Rowsx = 0 
									Break

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
									@__SubTotalSalesPrice Decimal(20, 2),
									@__RateManualSalePrice Decimal(20, 2),
									@__TotalSalesPrice Decimal(20, 2),
									@__PerformsHealthProfessionalThirdPartyId Int,
									@__GrandTotalSalesPrice Decimal(20, 2),
									@__CostValue Decimal(20, 2),
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
									--------------------------------------
									@__GrossValue	numeric(20,2),
									@__TaxValue		numeric(20,2),
									@__IvaId		int
									
								--SELECT @_AdmissionNumber = AdmissionNumber
								--FROM Billing.ServiceOrder
								--WHERE Id = @_ServiceOrderId
								
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
									@__GrossValue = GrossValue,
									@__TaxValue =TaxValue,
									@__IvaId= IvaId
								From [Contract].GetValueService(@_AdmissionNumber, NULL, @_CUPSEntityId, @IPSServiceIdH, @CareGroupId, @_PerformsFunctionalUnitId, @_PerformsProfessionalSpecialty
									, @_ServiceDate, @PatientGenus, @PatientBirth, @_Quantity, @_PerformsHealthProfessionalCode, @_PerformsHealthProfessionalThirdPartyId, @_RIASId, @_ContractDescriptionId)
								
								/***n****/
								--SELECT *
								--From [Contract].GetValueService(@_AdmissionNumber, NULL, @_CUPSEntityId, @IPSServiceIdH, @CareGroupId, @_PerformsFunctionalUnitId, @_PerformsProfessionalSpecialty
								--	, @_ServiceDate, @PatientGenus, @PatientBirth, @_Quantity, @_PerformsHealthProfessionalCode, @_PerformsHealthProfessionalThirdPartyId, @_RIASId, @_ContractDescriptionId)
								
								/***n*****/

								If @__StatusResult = 1 Begin
								
									If @IsFirst = 1 Begin
										Set @IsFirst = 0		
																										
										Declare @serviceOrderDetailResultXml Xml = (										
											Select @__StatusResult As StatusResult,
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
											@__GrossValue AS  GrossValue,
											@__TaxValue AS TaxValue,
											@__IvaId as  IvaId
											For Xml Path('ServiceOrderDetail'), Elements
										)										
										Declare @ResultServiceOrderDetailXml Xml, @ResultServiceOrderDetailSurgicalXml Xml
										Set @retarificXml = (
											Select * From @distributionToRetarific Where Id = @_ServiceOrderDetailDistributionId For Xml Path('DistributionToRetarific'), Elements										
										)

										Declare @CurrentSurgical Xml = (
											Select * From @RetarificServiceOrderDetailSurgical For Xml Path('RetarificServiceOrderDetailSurgical'), Elements
										)
										
										Select @ResultServiceOrderDetailXml = ServiceOrderDetailXml, @ResultServiceOrderDetailSurgicalXml = ServiceOrderDetailSurgicalXml 
										From [Contract].SetServiceValue(@retarificXml, @CurrentSurgical
											, @serviceOrderDetailResultXml, @__ServiceOrderDetailSurgicalXml, @GuidHomologation, @CareGroupId)
										
										Update rt Set Quantity = t.x.value('Quantity[1]', 'Int'),
											GrandTotalSalesPrice = t.x.value('GrandTotalSalesPrice[1]', 'Decimal(20,2)'),
											GrandTotalDiscount = t.x.value('GrandTotalDiscount[1]', 'Decimal(20,2)'),
											DistributionType = t.x.value('DistributionType[1]', 'Tinyint'),
											ThirdPartySalesPrice = t.x.value('ThirdPartySalesPrice[1]', 'Decimal(20,2)'),
											ThirdPartyPercentage = t.x.value('ThirdPartyPercentage[1]', 'Decimal(5,2)'),
											ApplyRecoveryFee = t.x.value('ApplyRecoveryFee[1]', 'Tinyint'),
											RecoveryFeeType = t.x.value('RecoveryFeeType[1]', 'Tinyint'),
											SubTotalPatientSalesPrice = t.x.value('SubTotalPatientSalesPrice[1]', 'Decimal(20,2)'),
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
											RateManualSalePrice = t.x.value('RateManualSalePrice[1]', 'Decimal(20,2)'),
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
											SubTotalSalesPrice_1 = t.x.value('SubTotalSalesPrice_1[1]', 'Decimal(20,2)'),
											ThirdPartyDiscount_1 = t.x.value('ThirdPartyDiscount_1[1]', 'Decimal(20,2)'),
											ThirdPartyDiscountPercentage = t.x.value('ThirdPartyDiscountPercentage[1]', 'Decimal(5,2)'),
											TotalSalesPrice = t.x.value('TotalSalesPrice[1]', 'Decimal(20,2)'),
											GrandTotalSalesPrice_1 = t.x.value('GrandTotalSalesPrice_1[1]', 'Decimal(20,2)'),
											SurchargeApply = t.x.value('SurchargeApply[1]', 'Bit'),
											SurgicalInterventionType = t.x.value('SurgicalInterventionType[1]', 'Tinyint'),
											SurgeryNumber = t.x.value('SurgeryNumber[1]', 'Tinyint'),
											IsFirstEvent = t.x.value('IsFirstEvent[1]', 'Bit'),
											IsAnnulled = t.x.value('IsAnnulled[1]', 'Bit'),
											IsDelete = t.x.value('IsDelete[1]', 'Bit'),
											IncomeMainAccountId = t.x.value('IncomeMainAccountId[1]', 'Int'),
											ServiceOrderDetailSurgicalId = t.x.value('ServiceOrderDetailSurgicalId[1]', 'Int'),
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
											GrossValue= t.x.value('GrossValue[1]', 'numeric(20,2)'),
											TaxValue =t.x.value('TaxValue[1]', 'numeric(20,2)'),
											IvaId =t.x.value('IvaId[1]', 'int')
										From @distributionToRetarific rt
										Inner Join @ResultServiceOrderDetailXml.nodes('/ServiceOrderDetail') t(x) On t.x.value('Id[1]', 'Int') = rt.Id
										Where rt.Id = @_ServiceOrderDetailDistributionId
										
										Insert Into @listNew
										Select 
											t.x.value('ServiceOrderDetailId[1]', 'Int'),
											t.x.value('ServiceOrderId[1]', 'Int'),
											t.x.value('CareGroupId[1]', 'Int'),
											t.x.value('HealthAdministratorId[1]', 'Int'),
											t.x.value('ThirdPartyId[1]', 'Int'),
											t.x.value('ServiceType[1]', 'Tinyint'),
											t.x.value('RecordType[1]', 'Tinyint'),
											t.x.value('CUPSEntityId[1]', 'Int'),
											t.x.value('IPSServiceId[1]', 'Int'),
											t.x.value('HospitalStayId[1]', 'Int'),
											t.x.value('HospitalStayDetailId[1]', 'Int'),
											t.x.value('ControlExternalConsultation[1]', 'Tinyint'),
											t.x.value('ControlExternalConsultationCode[1]', 'Decimal(18,0)'),
											t.x.value('CUPSAssociateService[1]', 'Bit'),
											t.x.value('CodeAssociateService[1]', 'Varchar(50)'),
											t.x.value('IsPackage[1]', 'Bit'),
											t.x.value('Packaging[1]', 'Bit'),
											t.x.value('PackageServiceOrderDetailId[1]', 'Int'),
											t.x.value('LiquidationType[1]', 'Tinyint'),
											t.x.value('Presentation[1]', 'Tinyint'),
											t.x.value('ProductId[1]', 'Int'),
											t.x.value('InvoicedQuantity[1]', 'Int'),
											t.x.value('SupplyQuantity[1]', 'Int'),
											t.x.value('DevolutionQuantity[1]', 'Int'),
											t.x.value('RateManualSalePrice[1]', 'Decimal(20,2)'),
											t.x.value('CostValue[1]', 'Decimal(18,2)'),
											t.x.value('ServiceDate[1]', 'DateTime'),
											t.x.value('AuthorizationNumber[1]', 'Varchar(20)'),
											t.x.value('PerformsFunctionalUnitId[1]', 'Int'),
											t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)'),
											t.x.value('PerformsProfessionalSpecialty[1]', 'Char(3)'),
											t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'Int'),
											t.x.value('BillingConceptId[1]', 'Int'),
											t.x.value('CostCenterId[1]', 'Int'),
											t.x.value('SettlementType[1]', 'Tinyint'),
											t.x.value('IncludeServiceOrderDetailId[1]', 'Int'),
											t.x.value('RecoveryRatio[1]', 'Decimal(5,2)'),
											t.x.value('RateManualId[1]', 'Int'),
											t.x.value('RateManualType[1]', 'Tinyint'),
											t.x.value('RateManualDetailId[1]', 'Int'),
											t.x.value('DefinitionRateDetailId[1]', 'Int'),
											t.x.value('DefinitionRateDetailConditionId[1]', 'Int'),
											t.x.value('SubTotalSalesPrice_1[1]', 'Decimal(20, 2)'),
											t.x.value('ThirdPartyDiscount_1[1]', 'Decimal(20, 2)'),
											t.x.value('ThirdPartyDiscountPercentage[1]', 'Decimal(5, 2)'),
											t.x.value('TotalSalesPrice[1]', 'Decimal(20, 2)'),
											t.x.value('GrandTotalSalesPrice_1[1]', 'Decimal(20, 2)'),
											t.x.value('SurchargeApply[1]', 'Bit'),
											t.x.value('SurgicalInterventionType[1]', 'Tinyint'),
											t.x.value('SurgeryNumber[1]', 'Tinyint'),
											t.x.value('IsFirstEvent[1]', 'Bit'),
											t.x.value('IsAnnulled[1]', 'Bit'),
											t.x.value('IsDelete[1]', 'Bit'),
											t.x.value('IncomeMainAccountId[1]', 'Int'),
											t.x.value('CodeNameSpeciality[1]', 'Varchar(300)'),
											t.x.value('CodeNameFunctionalUnit[1]', 'Varchar(300)'),
											t.x.value('CodeNameHealthAdministrator[1]', 'Varchar(300)'),
											t.x.value('PreviusServiceOrderDetailId[1]', 'Int'),
											t.x.value('CodeNameCareGroup[1]', 'Varchar(300)'),
											t.x.value('CodeNameCostCenter[1]', 'Varchar(100)'),
											t.x.value('CodeNameCups[1]', 'Varchar(300)'),
											t.x.value('CodeNameHealthProfessional[1]', 'Varchar(300)'),
											t.x.value('CodeNameIpsService[1]', 'Varchar(300)'),
											t.x.value('CodeNameProduct[1]', 'Varchar(300)'),
											t.x.value('IsSOAT[1]', 'Bit'),
											t.x.value('RoundService[1]', 'int'),
											@ResultServiceOrderDetailSurgicalXml
										From @ResultServiceOrderDetailXml.nodes('/ServiceOrderDetail') t(x)										

									End
									Else Begin
									
										Set @__ServiceOrderId = @_ServiceOrderId
										Set @__CodeAssociateService = @GuidHomologation

										If (Select Count(ServiceOrderDetailId) From @distributionToRetarific Where CodeAssociateService = @GuidHomologation) > 0 Begin
											Set @__AuthorizationNumber = (Select Top 1 AuthorizationNumber From @distributionToRetarific Where CodeAssociateService = @GuidHomologation)
										End
										If @__Presentation = 2 Begin --Quirurgico
																						
											Declare @serviceOrderDetailSurgicalResult Table(RowId Int,
												CodeNameIpsService Varchar(320),
												IPSServiceId Int,
												InvoicedQuantity Int,
												LiquidationPercentage Decimal(5, 2),
												TotalSalesPrice Decimal(20,2),
												ClassServiceIps Varchar(30),
												RateManualSalePrice Decimal(20,2),
												PerformsHealthProfessionalCode Char(20) Null,
												PerformsHealthProfessionalThirdPartyId Int Null,
												CostValue Decimal(18, 2),
												BillingConceptId Int,
												CostCenterId Int,
												RateManualDetailSurgicalId Int Null,
												SurchargeApply Bit,
												IncomeMainAccountId Int Null,
												RoundService int)
											Delete From @serviceOrderDetailSurgicalResult
											Insert Into @serviceOrderDetailSurgicalResult
											Select t.x.value('RowId[1]', 'Int'),
												t.x.value('CodeNameIpsService[1]', 'Varchar(320)'),
												t.x.value('IPSServiceId[1]', 'Int'),
												t.x.value('InvoicedQuantity[1]', 'Int'),
												t.x.value('LiquidationPercentage[1]', 'Decimal(5, 2)'),
												t.x.value('TotalSalesPrice[1]', 'Decimal(20,2)'),
												t.x.value('ClassServiceIps[1]', 'Varchar(30)'),
												t.x.value('RateManualSalePrice[1]', 'Decimal(20,2)'),
												t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)'),
												t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'Int'),
												t.x.value('CostValue[1]', 'Decimal(18, 2)'),
												t.x.value('BillingConceptId[1]', 'Int'),
												t.x.value('CostCenterId[1]', 'Int'),
												t.x.value('RateManualDetailSurgicalId[1]', 'Int'),
												t.x.value('SurchargeApply[1]', 'Bit'),
												t.x.value('IncomeMainAccountId[1]', 'Int'),
												t.x.value('RoundService[1]', 'Int')
											From @__ServiceOrderDetailSurgicalXml.nodes('/ServiceOrderDetailSurgical') t(x)

											Declare @IPSServiceIdRetarificSugical Varchar(30),
												@ServiceClassRetarificSurgical Tinyint
												
											Set @Rowsy = 1
											Set @RowIdy = 1

											While @Rowsy > 0
											begin
												
												Select Top 1 @RowIdy = RowId, @IPSServiceIdRetarificSugical = IPSServiceId 
												From @RetarificServiceOrderDetailSurgical 
												Where ServiceOrderDetailId = @_ServiceOrderDetailId And RowId >= @RowIdy Order By RowId

												Set @Rowsy = @@ROWCOUNT
												If @Rowsy = 0 
													Break

												Select @ServiceClassRetarificSurgical = ServiceClass From [Contract].IPSService With(Nolock) Where Id = @IPSServiceIdRetarificSugical
												If @ServiceClassRetarificSurgical = 1 
													Update @serviceOrderDetailSurgicalResult Set PerformsHealthProfessionalThirdPartyId = @__PerformsHealthProfessionalThirdPartyId
														, PerformsHealthProfessionalCode = @__PerformsHealthProfessionalCode
													Where ClassServiceIps = 'Ninguno'
												Else If @ServiceClassRetarificSurgical = 2
													Update @serviceOrderDetailSurgicalResult Set PerformsHealthProfessionalThirdPartyId = @__PerformsHealthProfessionalThirdPartyId
														, PerformsHealthProfessionalCode = @__PerformsHealthProfessionalCode
													Where ClassServiceIps = 'Cirujano'
												Else If @ServiceClassRetarificSurgical = 3
													Update @serviceOrderDetailSurgicalResult Set PerformsHealthProfessionalThirdPartyId = @__PerformsHealthProfessionalThirdPartyId
														, PerformsHealthProfessionalCode = @__PerformsHealthProfessionalCode
													Where ClassServiceIps = 'Anestesiólogo'
												Else If @ServiceClassRetarificSurgical = 4
													Update @serviceOrderDetailSurgicalResult Set PerformsHealthProfessionalThirdPartyId = @__PerformsHealthProfessionalThirdPartyId
														, PerformsHealthProfessionalCode = @__PerformsHealthProfessionalCode
													Where ClassServiceIps = 'Ayudante'
												Else If @ServiceClassRetarificSurgical = 5
													Update @serviceOrderDetailSurgicalResult Set PerformsHealthProfessionalThirdPartyId = @__PerformsHealthProfessionalThirdPartyId
														, PerformsHealthProfessionalCode = @__PerformsHealthProfessionalCode
													Where ClassServiceIps = 'Derecho Sala'
												Else If @ServiceClassRetarificSurgical = 6
													Update @serviceOrderDetailSurgicalResult Set PerformsHealthProfessionalThirdPartyId = @__PerformsHealthProfessionalThirdPartyId
														, PerformsHealthProfessionalCode = @__PerformsHealthProfessionalCode
													Where ClassServiceIps = 'Materiales Sutura'
												Else If @ServiceClassRetarificSurgical = 7
													Update @serviceOrderDetailSurgicalResult Set PerformsHealthProfessionalThirdPartyId = @__PerformsHealthProfessionalThirdPartyId
														, PerformsHealthProfessionalCode = @__PerformsHealthProfessionalCode
													Where ClassServiceIps = 'Instrumentación Quirúrgica'

												Set @RowIdy += 1
											End

											Declare @RIPSConcept VARCHAR(2) = '', @round Int = 1
											SELECT @RIPSConcept = RIPSConcept FROM Contract.CUPSEntity WHERE Id = @_CUPSEntityId
											If @_RateManualId Is Not Null And @_RateManualId > 0 Begin
												Select @round = IIF(@RIPSConcept IN ('12', '13'), 1, ISNULL(RoundService, 1))
												From [Contract].RateManual With(Nolock) Where Id = @_RateManualId
											End
											
											If @_SettlementType = 2 Begin
												--PorcentajeOtroServicio
												Declare @SubTotalSalesPriceOtroServicio Decimal(18, 2)
												Select @SubTotalSalesPriceOtroServicio = SubTotalSalesPrice From [Billing].ServiceOrderDetail With(Nolock) Where Id = @_IncludeServiceOrderDetailId
												Set @_SubTotalSalesPrice_1 = Round(@SubTotalSalesPriceOtroServicio * @_RecoveryRatio / 100, 2)
												Set @_ThirdPartyDiscount_1 = Round(@_SubTotalSalesPrice_1 * @_ThirdPartyDiscountPercentage / 100, 2)
												Set @_TotalSalesPrice = [Billing].RoundValue(@_SubTotalSalesPrice_1 * @_Quantity, @round)
												Set @_GrandTotalSalesPrice_1 = [Billing].RoundValue(@_TotalSalesPrice * @_Quantity, @round)
												Set @_GrandTotalSalesPrice = @_GrandTotalSalesPrice_1
												Set @_ThirdPartySalesPrice = @_GrandTotalSalesPrice_1
												Set @_GrandTotalDiscount = @_ThirdPartyDiscount_1 * @_Quantity
												Set @_ThirdPartyPercentage = 100
												Set @_SubTotalPatientSalesPrice = 0
												Set @_PatientPercentage = 0
											End
											Else If @_SettlementType = 4 Begin
												--PorcentajeMismoServicio
												Set @_SubTotalSalesPrice_1 = Round(@_SubTotalSalesPrice_1 * @_RecoveryRatio / 100, 2)
												Set @_ThirdPartyDiscount_1 = Round(@_SubTotalSalesPrice_1 * @_ThirdPartyDiscountPercentage / 100, 2)
												Set @_TotalSalesPrice = [Billing].RoundValue(@_SubTotalSalesPrice_1 * @_Quantity, @round)
												Set @_GrandTotalSalesPrice_1 = [Billing].RoundValue(@_TotalSalesPrice * @_Quantity, @round)
												Set @_GrandTotalSalesPrice = @_GrandTotalSalesPrice_1
												Set @_ThirdPartySalesPrice = @_GrandTotalSalesPrice_1
												Set @_GrandTotalDiscount = @_ThirdPartyDiscount_1 * @_Quantity
												Set @_ThirdPartyPercentage = 100
												Set @_SubTotalPatientSalesPrice = 0
												Set @_PatientPercentage = 0
											End
											If @_SettlementType = 3 Begin
												--IncluidoOtroServicio
												--No se recalcula
												Set @_SubTotalSalesPrice_1 = 0
												Set @_ThirdPartyDiscount_1 = 0
												Set @_ThirdPartyDiscountPercentage = 0
												Set @_TotalSalesPrice = 0
												Set @_GrandTotalSalesPrice_1 = 0
												Set @_GrandTotalSalesPrice = 0
												Set @_GrandTotalDiscount = 0
												Set @_ThirdPartySalesPrice = 0
												--Si es quirurgico en los detalles quirurgicos se establecen a cero los valores
												If (Select Count(*) From @RetarificServiceOrderDetailSurgical Where ServiceOrderDetailId = @_ServiceOrderDetailId) > 0 Begin
													Update @RetarificServiceOrderDetailSurgical Set TotalSalesPrice = 0
													Where ServiceOrderDetailId = @_ServiceOrderDetailId
												End
											End

											--Sin Probar
											Update @distributionToRetarific Set SubTotalSalesPrice_1 = @_SubTotalSalesPrice_1
												, ThirdPartyDiscount_1 = @_ThirdPartyDiscount_1
												, TotalSalesPrice = @_TotalSalesPrice
												, GrandTotalSalesPrice_1 = @_GrandTotalSalesPrice_1
												, GrandTotalSalesPrice = @_GrandTotalSalesPrice
												, ThirdPartySalesPrice = @_ThirdPartySalesPrice
												, GrandTotalDiscount = @_GrandTotalDiscount
												, ThirdPartyPercentage = @_ThirdPartyPercentage
												, SubTotalPatientSalesPrice = @_SubTotalPatientSalesPrice
												, PatientPercentage = @_PatientPercentage
												, RoundService = @round
											Where Id = @_ServiceOrderDetailDistributionId

										End

										Insert Into @listNew
										Values (@__Id
											, @__ServiceOrderId
											, @__CareGroupId
											, Null
											, Null
											, @__ServiceType
											, @__Recordtype
											, @__CUPSEntityId
											, @__IPSServiceId
											, Null
											, Null
											, Null
											, Null
											, 0
											, @__CodeAssociateService
											, 0
											, 0
											, Null
											, @__LiquidationType
											, @__Presentation
											, Null
											, @__InvoicedQuantity
											, 0
											, 0
											, @__RateManualSalePrice
											, @__CostValue
											, @__ServiceDate
											, @__AuthorizationNumber
											, @__PerformsFunctionalUnitId
											, @__PerformsHealthProfessionalCode
											, @__PerformsProfessionalSpecialty
											, @__PerformsHealthProfessionalThirdPartyId
											, @__BillingConceptId
											, @__CostCenterId
											, @__SettlementType
											, Null
											, Null
											, @__RateManualId
											, @__RateManualType
											, @__RateManualDetailId
											, @__DefinitionRateDetailId
											, @__DefinitionRateDetailConditionId
											, @__SubTotalSalesPrice
											, 0
											, 0
											, @__TotalSalesPrice
											, @__GrandTotalSalesPrice
											, @__SurchargeApply
											, @__SurgicalInterventionType
											, 0
											, 0
											, 0
											, 0
											, @__IncomeMainAccountId
											, Null
											, @__CodeNameFunctionalUnit
											, Null
											, Null
											, Null
											, @__CodeNameCostCenter
											, @__CodeNameCups
											, Null
											, @__CodeNameIpsService
											, Null
											, @__IsSOAT
											, @__RoundService
											, @__ServiceOrderDetailSurgicalXml
											)
									End
									
									Declare @StateResultIncome Bit,
										@MessageResultIncome Varchar(255),
										@IncomeMainAccountIdIncome Int,
										@ServiceOrderDetailSurgicalXmlIncome Xml
									
									Select @StateResultIncome = StateResult
										, @MessageResultIncome = MessageResult
										, @IncomeMainAccountIdIncome = IncomeMainAccountId
										, @ServiceOrderDetailSurgicalXmlIncome = ServiceOrderDetailSurgicalXml 
									From Billing.GetIncomeMainAccount(@_CareGroupId, @_RecordType, @_BillingConceptId, @_PerformsFunctionalUnitId, @_ProductId
										, @ResultServiceOrderDetailSurgicalXml)

									If @StateResultIncome = 0 Begin
										Set @StatusResultOut = 0
										Set @MessageResultOut = @MessageResultIncome
										Set @DistributionToRetarificOut = Null
										Set @ListNewServiceOrderDetailOut = Null
										--Select Convert(Bit, 0) As [StatusResult], @MessageResultIncome As [MessageResult], Null As DistributionToRetarific, Null As ListNewServiceOrderDetail
										Return
									End
									Else Begin
										
										Update @listNew Set IncomeMainAccountId = @IncomeMainAccountIdIncome 											
										, ServiceOrderDetailSurgicalXml = @ServiceOrderDetailSurgicalXmlIncome
										Where Id = @_ServiceOrderDetailId
										--Where RowId = (Select Top 1 RowId From @listNew Order By RowId Desc)
												
										Update @distributionToRetarific Set IncomeMainAccountId = @IncomeMainAccountIdIncome 
											, ServiceOrderDetailSurgicalXml = @ServiceOrderDetailSurgicalXmlIncome
										Where Id = @_ServiceOrderDetailDistributionId

										Set @_IncomeMainAccountId = @IncomeMainAccountIdIncome										
										Set @retarificServiceOrdenDetailSurgicalXml = @ServiceOrderDetailSurgicalXmlIncome
										
									End

								End
								Else Begin
									Set @myErrorListResult += Char(13) + Char(10) + @__MessageResult
								End
								Set @RowIdx += 1
							End
														
						End
					End
					Else Begin		
						Declare @CupsHomologationIdResult Int
						Declare @ActionResultCupsHomologation Table(StatusResult Bit, MessageResult Varchar(255), 
							CupsHomologationId Int Null,
							IPSServiceId Int Null,
							CupsEntityId Int Null,
							CodeNameCupsEntity Varchar(320) Null,
							CodeNameIpsService Varchar(320) Null,
							Activated Bit)
						Delete From @ActionResultCupsHomologation
						
						Insert Into @ActionResultCupsHomologation
						Select StatusResult, MessageResult, CupsHomologationId, IPSServiceId, CupsEntityId
							, CodeNameCupsEntity, CodeNameIpsService, Activated
							From [Contract].[GetHomologationCups](@CareGroupId, @_CUPSEntityId, @_PerformsFunctionalUnitId
							, @_PerformsProfessionalSpecialty, @_ServiceDate, @_IPSServiceId, Null, @_RIASId, @_ContractDescriptionId)
						
						If (Select Top 1 StatusResult From @ActionResultCupsHomologation) = 1 Begin								
							If (Select Count(1) From @ActionResultCupsHomologation) = 1 Begin
								Select @CupsHomologationIdResult = CupsHomologationId From @ActionResultCupsHomologation
							End
						End
						Else
							Set @myErrorListResult += Char(13) + Char(10) + (Select Top 1 MessageResult From @ActionResultCupsHomologation)

						Declare @ipsServiceIdx Int = 0
						If (Select Count(CupsHomologationId) From @ActionResultCupsHomologation) > 0
							Select Top 1 @ipsServiceIdx = IPSServiceId From @ActionResultCupsHomologation Where CupsEntityId = @_CUPSEntityId
						Else
							Set @ipsServiceIdx = @_IPSServiceId

						Declare @___StatusResult Bit,
							@___MessageResult Varchar(Max),
							@___Id Int,
							@___CostCenterId Int,
							@___ServiceOrderId Int,
							@___CodeAssociateService Varchar(50),
							@___AuthorizationNumber Varchar(20),
							@___ServiceType Tinyint,
							@___CodeNameIpsService Varchar(320),
							@___CodeNameCups Varchar(320),
							@___CodeNameFunctionalUnit Varchar(320),
							@___CodeNameCostCenter Varchar(100),
							@___AllowValueChange Bit,
							@___DefinitionRateDetailId Int,
							@___DefinitionRateDetailConditionId Int,
							@___LiquidationType Tinyint,
							@___Presentation Tinyint,
							@___IsSOAT Bit,
							@___RateManualType Tinyint,
							@___RateManualId Int,
							@___SubTotalSalesPrice Decimal(18, 2),
							@___RateManualSalePrice Decimal(18, 0),
							@___TotalSalesPrice Decimal(18, 2),
							@___PerformsHealthProfessionalThirdPartyId Int,
							@___GrandTotalSalesPrice Decimal(18, 0),
							@___CostValue Decimal(18, 2),
							@___Recordtype Tinyint,
							@___CareGroupId Int,
							@___CUPSEntityId Int,
							@___IPSServiceId Int,
							@___InvoicedQuantity Int,
							@___ServiceDate DateTime,
							@___PerformsFunctionalUnitId Int,
							@___PerformsHealthProfessionalCode Char(20),
							@___PerformsProfessionalSpecialty Char(3),
							@___BillingConceptId Int,
							@___SettlementType Tinyint,
							@___RateManualDetailId Int,
							@___ServiceOrderDetailSurgicalXml Xml,
							@___IncomeMainAccountId Int,
							@___SurgicalInterventionType Tinyint,
							@___SurchargeApply Bit,
							@___RoundService int,
							@___GrossValue numeric(20,2),
							@___TaxValue numeric(20,2),
							@___IvaId int
							
						Select @___StatusResult = StatusResult,
							@___MessageResult = MessageResult,
							@___Id = Id,
							@___CostCenterId = CostCenterId,
							@___ServiceType = ServiceType,
							@___CodeNameIpsService = CodeNameIpsService,
							@___CodeNameCups = CodeNameCups,
							@___CodeNameFunctionalUnit = CodeNameFunctionalUnit,
							@___CodeNameCostCenter = CodeNameCostCenter,
							@___AllowValueChange = AllowValueChange,
							@___DefinitionRateDetailId = DefinitionRateDetailId,
							@___DefinitionRateDetailConditionId = DefinitionRateDetailConditionId,
							@___LiquidationType = LiquidationType,
							@___Presentation = Presentation,
							@___IsSOAT = IsSOAT,
							@___RateManualType = RateManualType,
							@___RateManualId = RateManualId,
							@___SubTotalSalesPrice = SubTotalSalesPrice,
							@___RateManualSalePrice = RateManualSalePrice,
							@___TotalSalesPrice = TotalSalesPrice,
							@___PerformsHealthProfessionalThirdPartyId = PerformsHealthProfessionalThirdPartyId,
							@___GrandTotalSalesPrice = GrandTotalSalesPrice,
							@___CostValue = CostValue,
							@___RecordType = RecordType,
							@___CareGroupId = CareGroupId,
							@___CUPSEntityId = CUPSEntityId,
							@___IPSServiceId = IPSServiceId,
							@___InvoicedQuantity = InvoicedQuantity,
							@___ServiceDate = ServiceDate,
							@___PerformsFunctionalUnitId = PerformsFunctionalUnitId,
							@___PerformsHealthProfessionalCode = PerformsHealthProfessionalCode,
							@___PerformsProfessionalSpecialty = PerformsProfessionalSpecialty,
							@___BillingConceptId = BillingConceptId,
							@___SettlementType = SettlementType,
							@___RateManualDetailId = RateManualDetailId,
							@___ServiceOrderDetailSurgicalXml = ServiceOrderDetailSurgicalXml,
							@___IncomeMainAccountId = IncomeMainAccountId,
							@___SurgicalInterventionType = SurgicalInterventionType,
							@___SurchargeApply = SurchargeApply,
							@___RoundService = RoundService,
							@___GrossValue = GrossValue,
							@___TaxValue = TaxValue,
							@___IvaId = IvaId
						From [Contract].GetValueService(@_AdmissionNumber, NULL, @_CUPSEntityId, @ipsServiceIdx, @CareGroupId, @_PerformsFunctionalUnitId, @_PerformsProfessionalSpecialty
							, @_ServiceDate, @PatientGenus, @PatientBirth, @_Quantity, @_PerformsHealthProfessionalCode, @_PerformsHealthProfessionalThirdPartyId, @_RIASId, @_ContractDescriptionId)
						
						If @___StatusResult = 1 Begin
							Declare @__serviceOrderDetailResultXml Xml = (										
								Select @___StatusResult As StatusResult,
								@___MessageResult As MessageResult,
								@___Id As Id,
								@___CostCenterId As CostCenterId,
								@___ServiceType As ServiceType,
								@___CodeNameIpsService As CodeNameIpsService,
								@___CodeNameCups As CodeNameCups,
								@___CodeNameFunctionalUnit As CodeNameFunctionalUnit,
								@___CodeNameCostCenter As CodeNameCostCenter,
								@___AllowValueChange As AllowValueChange,
								@___DefinitionRateDetailId As DefinitionRateDetailId,
								@___DefinitionRateDetailConditionId As DefinitionRateDetailConditionId,
								@___LiquidationType As LiquidationType,
								@___Presentation As Presentation,
								@___IsSOAT As IsSOAT,
								@___RateManualType As RateManualType,
								@___RateManualId As RateManualId,
								@___SubTotalSalesPrice As SubTotalSalesPrice,
								@___RateManualSalePrice As RateManualSalePrice,
								@___TotalSalesPrice As TotalSalesPrice,
								@___PerformsHealthProfessionalThirdPartyId As PerformsHealthProfessionalThirdPartyId,
								@___GrandTotalSalesPrice As GrandTotalSalesPrice,
								@___CostValue As CostValue,
								@___RecordType As RecordType,
								@___CareGroupId As CareGroupId,
								@___CUPSEntityId As CUPSEntityId,
								@___IPSServiceId As IPSServiceId,
								@___InvoicedQuantity As InvoicedQuantity,
								@___ServiceDate As ServiceDate,
								@___PerformsFunctionalUnitId As PerformsFunctionalUnitId,
								@___PerformsHealthProfessionalCode As PerformsHealthProfessionalCode,
								@___PerformsProfessionalSpecialty As PerformsProfessionalSpecialty,
								@___BillingConceptId As BillingConceptId,
								@___SettlementType As SettlementType,
								@___RateManualDetailId As RateManualDetailId,
								Null As ServiceOrderDetailSurgicalXml,
								@___IncomeMainAccountId As IncomeMainAccountId,
								@___SurgicalInterventionType As SurgicalInterventionType,
								@___SurchargeApply As SurchargeApply, 
								@___RoundService As RoundService ,
								@___GrossValue As GrossValue,
								@___TaxValue As TaxValue,
								@___IvaId As IvaId
								For Xml Path('ServiceOrderDetail'), Elements
							)
							Set @retarificXml = (
								Select * From @distributionToRetarific Where Id = @_ServiceOrderDetailDistributionId For Xml Path('DistributionToRetarific'), Elements										
							)
							Declare @__ResultServiceOrderDetailXml Xml, @__ResultServiceOrderDetailSurgicalXml Xml
							Select @__ResultServiceOrderDetailXml = ServiceOrderDetailXml, @__ResultServiceOrderDetailSurgicalXml = ServiceOrderDetailSurgicalXml 
							From [Contract].SetServiceValue(@retarificXml, @retarificServiceOrdenDetailSurgicalXml
								, @__serviceOrderDetailResultXml, @___ServiceOrderDetailSurgicalXml, Null, @CareGroupId)

							Set @___ServiceOrderDetailSurgicalXml = @__ResultServiceOrderDetailSurgicalXml								
								
							Update rt Set Quantity = t.x.value('Quantity[1]', 'Int'),
								GrandTotalSalesPrice = t.x.value('GrandTotalSalesPrice[1]', 'Decimal(20,2)'),
								GrandTotalDiscount = t.x.value('GrandTotalDiscount[1]', 'Decimal(20,2)'),
								DistributionType = t.x.value('DistributionType[1]', 'Tinyint'),
								ThirdPartySalesPrice = t.x.value('ThirdPartySalesPrice[1]', 'Decimal(20,2)'),
								ThirdPartyPercentage = t.x.value('ThirdPartyPercentage[1]', 'Decimal(5,2)'),
								ApplyRecoveryFee = t.x.value('ApplyRecoveryFee[1]', 'Tinyint'),
								RecoveryFeeType = t.x.value('RecoveryFeeType[1]', 'Tinyint'),
								SubTotalPatientSalesPrice = t.x.value('SubTotalPatientSalesPrice[1]', 'Decimal(20,2)'),
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
								RateManualSalePrice = t.x.value('RateManualSalePrice[1]', 'Decimal(20,2)'),
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
								SubTotalSalesPrice_1 = t.x.value('SubTotalSalesPrice_1[1]', 'Decimal(20,2)'),
								ThirdPartyDiscount_1 = t.x.value('ThirdPartyDiscount_1[1]', 'Decimal(20,2)'),
								ThirdPartyDiscountPercentage = t.x.value('ThirdPartyDiscountPercentage[1]', 'Decimal(5,2)'),
								TotalSalesPrice = t.x.value('TotalSalesPrice[1]', 'Decimal(20,2)'),
								GrandTotalSalesPrice_1 = t.x.value('GrandTotalSalesPrice_1[1]', 'Decimal(20,2)'),
								SurchargeApply = t.x.value('SurchargeApply[1]', 'Bit'),
								SurgicalInterventionType = t.x.value('SurgicalInterventionType[1]', 'Tinyint'),
								SurgeryNumber = t.x.value('SurgeryNumber[1]', 'Tinyint'),
								IsFirstEvent = t.x.value('IsFirstEvent[1]', 'Bit'),
								IsAnnulled = t.x.value('IsAnnulled[1]', 'Bit'),
								IsDelete = t.x.value('IsDelete[1]', 'Bit'),
								IncomeMainAccountId = t.x.value('IncomeMainAccountId[1]', 'Int'),
								ServiceOrderDetailSurgicalId = t.x.value('ServiceOrderDetailSurgicalId[1]', 'Int'),
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
								ServiceOrderDetailSurgicalXml = @__ResultServiceOrderDetailSurgicalXml,
								GrossValue= t.x.value('GrossValue[1]', 'numeric(20,2)'),
								TaxValue =t.x.value('TaxValue[1]', 'numeric(20,2)'),
								IvaId =t.x.value('IvaId[1]', 'int')
							From @distributionToRetarific rt
							Inner Join @__ResultServiceOrderDetailXml.nodes('/ServiceOrderDetail') t(x) On t.x.value('Id[1]', 'Int') = rt.Id
							Where rt.Id = @_ServiceOrderDetailDistributionId								
						End
						Else
							Set @myErrorListResult += Char(13) + Char(10) + @___MessageResult
					End

				End
				Else If @_RecordType = 2 Begin --Medicamentos
					
					Declare @ProductRateDetailId Int
						, @ProductCodeName Varchar(320)
						, @CareGroupCodeName Varchar(320)
						, @SalesValueWithSurchargeProduct Decimal(18, 2)
						, @SalesValueProduct Decimal(18, 2)
						--Nuevos campos para calculo de la tarifa
						, @LiquidationType Tinyint
						, @RateType Tinyint
						, @PercentageBasedOn Tinyint
						, @Percentage Decimal(6,2)
						, @CupsEntityId  Int
						, @ContractDescriptionId Int
						, @AverageCost Decimal(18,2)
						, @FinalCost Decimal(18,2)						
						, @TaxPercent numeric(5,2)
						, @SalePriceIncludeTax bit
						set @SalePriceIncludeTax = (select top 1 SalePriceIncludeTax from GeneralLedger.CompanySettings)
						Declare @rateManualSalePrice Decimal(18, 2) = 0
									, @RoundProduct Int = 0

						if exists(SELECT 1
									FROM Inventory.InventoryProduct ip WITH(NOLOCK) 
									JOIN Contract.CareGroup cg WITH (NOLOCK) ON 2 = cg.Id
									JOIN Inventory.ProductRateGeneral prg WITH (NOLOCK) ON cg.ProductRateId = prg.ProductRateId 
									AND ((prg.RuleType=1 and prg.ProductTypeId=ip.ProductTypeId) or (prg.RuleType=2 and prg.ProductGroupId= ip.ProductGroupId)
									or (prg.RuleType=3 and prg.ProductSubGroupId=ip.ProductSubGroupId)) AND CAST(@_ServiceDate AS DATE) BETWEEN prg.InitialDate AND prg.EndDate
									WHERE  ip.Id = @_ProductId) BEGIN
							
									declare @_ConditionType as TINYINT, @_ProductRateGeneralId as INT

									SELECT top 1	@ProductCodeName = Concat(ip.Code, ' - ', ip.[Name]),
													@CareGroupCodeName = Concat(cg.Code, ' - ', cg.[Name]),
													@_ConditionType = prg.ConditionType, 
													@RateType =prg.RateType,
													@SalesValueProduct = iif(prg.ConditionType=2,isnull(a.SalesValue,0),isnull(prg.SalesValue,0)) ,
													@PercentageBasedOn = prg.PercentageBasedOn,
													@Percentage= iif(prg.ConditionType =2,isnull(a.Percentage,0),ISNULL(prg.Percentage,0)),
													@SalesValueWithSurchargeProduct=iif(prg.ConditionType=2,isnull(a.SalesValue,0),isnull(prg.SalesValue,0)),
													@AverageCost = sod.CostValue,
													@FinalCost = sod.FinalProductCost,
													@_IvaId = sod.IVAId,
													@TaxPercent = iif(ip.TaxedProduct=1 And ip.LiquidateSalesTaxes=1, ISNULL(iva.Percentage,0), 0),
													@ProductRateDetailId = a.Id,
													@_ProductRateGeneralId =prg.Id
										FROM  Billing.ServiceOrderDetail sod with(NOLOCK)
										join Inventory.InventoryProduct ip WITH(NOLOCK) on sod.ProductId=ip.Id
										JOIN Contract.CareGroup cg WITH (NOLOCK) ON sod.CareGroupId = cg.Id
										JOIN Inventory.ProductRateGeneral prg WITH (NOLOCK) ON cg.ProductRateId = prg.ProductRateId	
										LEFT JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) on iva.Id= [sod].IVAId
										OUTER APPLY (select top 1	prgs.Id, 
																	ISNULL(prgs.[Percentage],0) [Percentage],
																	ISNULL(prgs.SalesValue,0) SalesValue
															from Inventory.ProductRateGeneralCondition prgs WITH(NOLOCK)
															WHERE prg.Id= prgs.ProductRateGeneralId and (IIF(prg.PercentageBasedOn=1,sod.CostValue,	sod.FinalProductCost) * sod.InvoicedQuantity) BETWEEN prgs.InitialValue and prgs.EndValue) A
										WHERE  ip.Id = @_ProductId and sod.id=@_ServiceOrderDetailId AND ((prg.RuleType=1 and prg.ProductTypeId=ip.ProductTypeId) or (prg.RuleType=2 and prg.ProductGroupId= ip.ProductGroupId)
																								or (prg.RuleType=3 and prg.ProductSubGroupId=ip.ProductSubGroupId)) AND CAST(@_ServiceDate AS DATE) BETWEEN prg.InitialDate AND prg.EndDate
										Order By prg.Id

										IF @_ConditionType = 2 and @ProductRateDetailId is NULL BEGIN
									
											Set @myErrorListResult += Char(13) + Char(10) + 'El producto ' + @ProductCodeName + ' no se encuentra dentro de la tarifa para el grupo de atención ' + @CareGroupCodeName + ' y fecha ' + Cast(@_ServiceDate As Varchar)
										end
										else IF @RateType =1 begin
												set @rateManualSalePrice = @SalesValueProduct
										end
										else begin
												set @rateManualSalePrice = iif( @PercentageBasedOn =1,ROUND((@AverageCost + (@AverageCost*(@Percentage/100))),2), ROUND((@FinalCost + (@FinalCost*(@Percentage/100))),2)) 														
										END
						END
						ELSE BEGIN --Tarificacion Normal
								Select Top 1 @ProductRateDetailId = prd.Id
								, @ProductCodeName = Concat(pr.Code, ' - ', pr.[Name])
								, @CareGroupCodeName = Concat(cg.Code, ' - ', cg.[Name])
								, @SalesValueWithSurchargeProduct = prd.SalesValueWithSurcharge
								, @SalesValueProduct = prd.SalesValue
								--****Nuevas variables para calcular la Tarifa del Producto
								, @LiquidationType = prd.LiquidationType
								, @RateType = prd.RateType
								, @PercentageBasedOn = prd.PercentageBasedOn
								, @Percentage = prd.Percentage
								, @CupsEntityId = prd.CupsId
								, @ContractDescriptionId = prd.ContractDescriptionId
								, @AverageCost = sod.CostValue
								, @FinalCost = sod.FinalProductCost
								, @_IvaId = sod.IVAId
								-- WI 52842: solo liquida IVA si es gravado Y "Gravado Venta Salud" (LiquidateSalesTaxes=1); si no, 0%.
								, @TaxPercent = iif([ip].TaxedProduct=1 And [ip].LiquidateSalesTaxes=1, Isnull(iva.Percentage,0), 0)
							From [Contract].CareGroup cg With(Nolock)
							Inner Join Inventory.ProductRate pr With(Nolock) On cg.ProductRateId = pr.Id
							Inner Join Inventory.ProductRateDetail prd with(NOLOCK) On prd.ProductRateId = pr.Id
							Inner Join Billing.ServiceOrderDetail sod with(NOLOCK) on prd.ProductId = sod.ProductId
							INNER JOIN Inventory.InventoryProduct [ip] with(NOLOCK) on [ip].Id=sod.ProductId
							LEFT JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) on iva.Id= [sod].IVAId
							Where cg.Id = @CareGroupId And prd.ProductId = @_ProductId AND sod.id=@_ServiceOrderDetailId AND(prd.InitialDate <= Cast(@_ServiceDate As Date) And prd.EndDate >= Cast(@_ServiceDate As Date))
							Order By prd.Id

							If @ProductRateDetailId Is Null Or @ProductRateDetailId = 0
								Set @myErrorListResult += Char(13) + Char(10) + 'El producto ' + @ProductCodeName + ' no se encuentra dentro de la tarifa para el grupo de atención ' + @CareGroupCodeName + ' y fecha ' + Cast(@_ServiceDate As Varchar)
							Else Begin
								--****Si existe algun registro en la tabla ProductServiceDetail con el id del detalle de la orden de servcio se elimina, si es necesario posteriormente vuelve y se crea
								IF (SELECT COUNT(*) FROM Billing.ProductServiceDetail psd WHERE psd.ServiceOrderDetailId = @_ServiceOrderDetailId )>0 BEGIN
									DELETE Billing.ProductServiceDetail where ServiceOrderDetailId = @_ServiceOrderDetailId							
								END
						
								--Si el tipo tarifa es Fija establece los nuevo valores 
								If @RateType =1 BEGIN
									If @_SurchargeApply = 1 
										Set @rateManualSalePrice = @SalesValueWithSurchargeProduct
									Else 
										Set @rateManualSalePrice = @SalesValueProduct
								END						

								--Si el tipo de taria es porcentual calcula el valor dependiendo de si es con el valor promedio o el de la ultima venta del producto
								 If @RateType =2 begin
								 --Si No existen valores para el costo promedio o final del producto los consulta de la tabla Inventory para traerlos actuales
						 			IF @AverageCost IS NULL OR @AverageCost = 0 BEGIN
											SELECT  @AverageCost = ip.ProductCost FROM Inventory.InventoryProduct ip where ip.id=@_ProductId 
									END
									IF @FinalCost IS NULL OR @FinalCost =0 BEGIN
											SELECT  @FinalCost = ip.FinalProductCost FROM Inventory.InventoryProduct ip where ip.id=@_ProductId 
									END

									if @PercentageBasedOn =1 
										set @rateManualSalePrice = (@AverageCost *(@Percentage/100)) +(@AverageCost)							
									else if @PercentageBasedOn =2 
										set @rateManualSalePrice = (@FinalCost *(@Percentage/100)) +(@FinalCost)
									else
										set @rateManualSalePrice =0
								 end

								 -- si el tipo de Liquidacion es Servicio o Tarifa-Servicio
								 If @LiquidationType in (2,3) begin

									 --Si tiene Tipo Tarifa crea el detalle de los valores de la tarifa en la tabla ProductServiceDetail
										IF @RateType <> 0 
										BEGIN
											INSERT INTO Billing.ProductServiceDetail
											SELECT	pdd.Id,
													sod.Id,
													null,
													null,
													pdd.ProductId,
													@rateManualSalePrice,
													@LiquidationType,
													@RateType,
													@_ThirdPartyDiscountPercentage
											FROM Billing.ServiceOrderDetail sod WITH(NOLOCK)
											JOIN Billing.ServiceOrder so WITH(NOLOCK) ON sod.ServiceOrderId = so.Id
											JOIN Inventory.PharmaceuticalDispensing pd WITH(NOLOCK) ON so.EntityId = pd.Id
											JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH(NOLOCK) ON pd.Id = pdd.PharmaceuticalDispensingId and pdd.ProductId = sod.ProductId
											WHERE sod.Id = @_ServiceOrderDetailId
										END

										--Se declaran las variables a utlizar para el calculo de la tarifa del CUPS
										declare	  @HealthProfessionalCode			CHAR(20)
												, @ProfessionalSpecialty			CHAR(3)
												, @HealthProfessionalThirdPartyId	Int
												, @CUPSSalePrice					Decimal(18,2)
												, @DateProduct						DateTime
												, @_patientGenus					int
												, @_patientBirth					Datetime
												, @IPSServiceId						int					

											-- Se asignan los valores que estan en el detalle de la Orden de Servicio 
											SELECT TOP 1 
														 @HealthProfessionalCode = sod.PerformsHealthProfessionalCode
														,@ProfessionalSpecialty = sod.PerformsProfessionalSpecialty
														,@HealthProfessionalThirdPartyId =sod.PerformsHealthProfessionalThirdPartyId
														,@DateProduct = sod.ServiceDate												
											FROM Billing.ServiceOrderDetail sod
											WHERE sod.Id =@_ServiceOrderDetailId

											-- se obtienen del paciente el genero y fecha de nacimiento
											SELECT	 @_patientGenus = p.Gender
													,@_patientBirth =p.BirthDate	
											FROM Billing.RevenueControl rc WITH(NOLOCK)
											JOIN Common.Person p WITH(NOLOCK) ON rc.PatientCode =p.IdentificationNumber
											where rc.AdmissionNumber = @_AdmissionNumber

											--Se evalua que el CUPS solo tenga una HOMOLOGACION si no retorna un error
											IF (SELECT COUNT(*) FROM  [Contract].[GetHomologationCups](@CareGroupId,@CupsEntityId,@_PerformsFunctionalUnitId,@ProfessionalSpecialty,@DateProduct,NULL,0,0,@ContractDescriptionId))=1
											BEGIN
											DECLARE @Status as BIT, @Message as VARCHAR(255)
											--Se optienen el Id del Servicio IPS
											SELECT @IPSServiceId = ghc.IPSServiceId, @Status=ghc.StatusResult,@Message =ghc.MessageResult FROM  [Contract].[GetHomologationCups](@CareGroupId,@CupsEntityId,@_PerformsFunctionalUnitId,@ProfessionalSpecialty,@DateProduct,NULL,0,0,@ContractDescriptionId)ghc
												IF @Status =0 BEGIN
													Set @StatusResultOut = @Status
													Set @MessageResultOut = @Message
													Set @DistributionToRetarificOut = Null
													Set @ListNewServiceOrderDetailOut = Null
													RETURN
												END	

											-- Se obtiene el valor del servicio  usando la funcion GetValueService
											SELECT @CUPSSalePrice = gvs.SubTotalSalesPrice
											FROM [Contract].GetValueService(@_AdmissionNumber, NULL, @CupsEntityId, @IPSServiceId, @CareGroupId, @_PerformsFunctionalUnitId, @ProfessionalSpecialty
												, @DateProduct,  @_patientGenus, @_patientBirth, 1, @HealthProfessionalCode, @HealthProfessionalThirdPartyId, 0, @ContractDescriptionId) gvs
								
										--- *********** se establece el valor del producto + el valor del servicio
												set @rateManualSalePrice =@rateManualSalePrice + @CUPSSalePrice
										--******* Se crea el detalle del CUPS en la tabla ProductServiceDetail
												INSERT INTO Billing.ProductServiceDetail
												SELECT	pdd.Id,
														sod.Id,
														@CupsEntityId,
														@ContractDescriptionId,
														null,
														@CUPSSalePrice,
														@LiquidationType,
														0,
														@_ThirdPartyDiscountPercentage
												FROM Billing.ServiceOrderDetail sod WITH(NOLOCK)
												JOIN Billing.ServiceOrder so WITH(NOLOCK) ON sod.ServiceOrderId = so.Id
												JOIN Inventory.PharmaceuticalDispensing pd WITH(NOLOCK) ON so.EntityId = pd.Id
												JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH(NOLOCK) ON pd.Id = pdd.PharmaceuticalDispensingId and pdd.ProductId = sod.ProductId		
												WHERE sod.Id = @_ServiceOrderDetailId
											END
											ELSE
											BEGIN
											Set @StatusResultOut = 0
												Set @MessageResultOut = ISNULL(@MessageResultOut, 'El CUPS Parametrizado para la tarifa del producto tiene mas de una Homologación')
												Set @DistributionToRetarificOut = Null
												Set @ListNewServiceOrderDetailOut = Null
												RETURN
											END
									end							
							END -- FIN LOGICA TARIFA NORMAL
						End-- FIN IF ELSE TARIFAS
						Update @distributionToRetarific Set RateManualSalePrice = @rateManualSalePrice 
								, ApplyRecoveryFee = 1, RecoveryFeeType = 1
							Where Id = @_ServiceOrderDetailDistributionId

							declare @SalesPrices DECIMAL(20,2)
						

							If @_SettlementType = 1 Begin --Manual de Tarifas
								select @_GrossValue = GrossValue, @_TaxValue = TaxValue, @SalesPrices = SalesPrice
								from Billing.SetValueSalesPrice (@SalePriceIncludeTax,@rateManualSalePrice,@TaxPercent)

								Set @_SubTotalSalesPrice_1 = @SalesPrices
								Set @_ThirdPartyDiscount_1 = Round(@_SubTotalSalesPrice_1 * @_ThirdPartyDiscountPercentage / 100, 2)
								Set @_TotalSalesPrice = @SalesPrices - @_ThirdPartyDiscount_1
								Set @_GrandTotalSalesPrice_1 = @_TotalSalesPrice * @_Quantity
								Set @_GrandTotalSalesPrice = @_GrandTotalSalesPrice_1
								Set @_ThirdPartySalesPrice = @_GrandTotalSalesPrice_1
								Set @_ThirdPartyPercentage = 100
								Set @_SubTotalPatientSalesPrice = 0
								Set @_PatientPercentage = 0
							End
							Else If @_SettlementType = 2 Begin --PorcentajeOtroServicio
								Declare @SubTotalSalesPriceOtroServicioProduct Decimal(18, 2)
								Select @SubTotalSalesPriceOtroServicio = SubTotalSalesPrice 
								From [Billing].ServiceOrderDetail With(Nolock) 
								Where Id = @_IncludeServiceOrderDetailId

								select @_GrossValue = GrossValue, @_TaxValue = TaxValue, @SalesPrices = SalesPrice
								from Billing.SetValueSalesPrice (1,@SubTotalSalesPriceOtroServicioProduct,@TaxPercent)

								Set @_SubTotalSalesPrice_1 = Round(@SalesPrices * @_RecoveryRatio / 100, 2)
								Set @_ThirdPartyDiscount_1 = Round(@_SubTotalSalesPrice_1 * @_ThirdPartyDiscountPercentage / 100, 2)
								Set @_TotalSalesPrice = [Billing].RoundValue(@_SubTotalSalesPrice_1 - @_ThirdPartyDiscount_1, @RoundProduct)
								Set @_GrandTotalSalesPrice_1 = [Billing].RoundValue(@_TotalSalesPrice * @_Quantity, @RoundProduct)
								Set @_GrandTotalSalesPrice = @_GrandTotalSalesPrice_1
								Set @_ThirdPartySalesPrice = @_GrandTotalSalesPrice_1
								Set @_GrandTotalDiscount = @_ThirdPartyDiscount_1 * @_Quantity
								Set @_ThirdPartyPercentage = 100
								Set @_SubTotalPatientSalesPrice = 0
								Set @_PatientPercentage = 0
							End
							Else If @_SettlementType = 4 Begin --PorcentajeMismoServicio
							
								select @_GrossValue = GrossValue, @_TaxValue = TaxValue, @SalesPrices = SalesPrice
								from Billing.SetValueSalesPrice (1,@_SubTotalSalesPrice_1,@TaxPercent)

								Set @_SubTotalSalesPrice_1 = Round(@SalesPrices * @_RecoveryRatio / 100, 2)
								Set @_ThirdPartyDiscount_1 = Round(@_SubTotalSalesPrice_1 * @_ThirdPartyDiscountPercentage / 100, 2)
								Set @_TotalSalesPrice = [Billing].RoundValue(@_SubTotalSalesPrice_1 - @_ThirdPartyDiscount_1, @RoundProduct)
								Set @_GrandTotalSalesPrice_1 = [Billing].RoundValue(@_TotalSalesPrice * @_Quantity, @RoundProduct)
								Set @_GrandTotalSalesPrice = @_GrandTotalSalesPrice_1
								Set @_ThirdPartySalesPrice = @_GrandTotalSalesPrice_1
								Set @_GrandTotalDiscount = @_ThirdPartyDiscount_1 * @_Quantity
								Set @_ThirdPartyPercentage = 100
								Set @_SubTotalPatientSalesPrice = 0
								Set @_PatientPercentage = 0
							End
							Else If @_SettlementType = 3 Begin
								Set @_SubTotalSalesPrice_1 = 0
								Set @_ThirdPartyDiscount_1 = 0
								Set @_ThirdPartyDiscountPercentage = 0

								Set @_TotalSalesPrice = 0
								Set @_GrandTotalSalesPrice_1 = 0
								Set @_GrandTotalSalesPrice = 0
								Set @_GrandTotalDiscount = 0
								Set @_ThirdPartySalesPrice = 0

								If (Select Count(*) From @RetarificServiceOrderDetailSurgical Where ServiceOrderDetailId = @_ServiceOrderDetailId) > 0 Begin
									Update @RetarificServiceOrderDetailSurgical Set TotalSalesPrice = 0
									Where ServiceOrderDetailId = @_ServiceOrderDetailId
								End
							End
							Set @_LastCaregroupId = @CareGroupId
						

							Update @distributionToRetarific Set SubTotalSalesPrice_1 = @_SubTotalSalesPrice_1
									, ThirdPartyDiscount_1 = @_ThirdPartyDiscount_1
									, TotalSalesPrice = @_TotalSalesPrice
									, GrandTotalSalesPrice_1 = @_GrandTotalSalesPrice_1
									, GrandTotalSalesPrice = @_GrandTotalSalesPrice
									, ThirdPartySalesPrice = @_ThirdPartySalesPrice
									, ThirdPartyPercentage = @_ThirdPartyPercentage
									, SubTotalPatientSalesPrice = @_SubTotalPatientSalesPrice
									, PatientPercentage = @_PatientPercentage
									, GrandTotalDiscount = @_GrandTotalDiscount
									, ThirdPartyDiscountPercentage = @_ThirdPartyDiscountPercentage
									, LastCaregroupId = @CareGroupId
									, RoundService = @RoundProduct
									, GrossValue = @_GrossValue
									, TaxValue = @_TaxValue
									,IvaId = @_IvaId
							Where Id = @_ServiceOrderDetailDistributionId
						
							Declare @StateResultIncomeProduct Bit,
								@MessageResultIncomeProduct Varchar(255),
								@IncomeMainAccountIdIncomeProduct Int,
								@ServiceOrderDetailSurgicalXmlIncomeProduct Xml

							Select @StateResultIncomeProduct = StateResult
								, @MessageResultIncomeProduct = MessageResult
								, @IncomeMainAccountIdIncomeProduct = IncomeMainAccountId
								, @ServiceOrderDetailSurgicalXmlIncomeProduct = ServiceOrderDetailSurgicalXml 
							From Billing.GetIncomeMainAccount(@_CareGroupId, @_RecordType, @_BillingConceptId, @_PerformsFunctionalUnitId, @_ProductId, Null)

							If @StateResultIncomeProduct = 0 Begin
								Set @StatusResultOut = 0
								Set @MessageResultOut = @MessageResultIncomeProduct
								Set @DistributionToRetarificOut = Null
								Set @ListNewServiceOrderDetailOut = Null

								--Select Convert(Bit, 0) As [StatusResult], @MessageResultIncomeProduct As [MessageResult], Null As DistributionToRetarific, Null As ListNewServiceOrderDetail
								Return
							End
							Else Begin												
								Update @distributionToRetarific Set IncomeMainAccountId = @IncomeMainAccountIdIncomeProduct 
									, ServiceOrderDetailSurgicalXml = @ServiceOrderDetailSurgicalXmlIncomeProduct
								Where Id = @_ServiceOrderDetailDistributionId

								Set @_IncomeMainAccountId = @IncomeMainAccountIdIncomeProduct
								Set @retarificServiceOrdenDetailSurgicalXml = @ServiceOrderDetailSurgicalXmlIncomeProduct										
							End
				End
			End
		
			Set @RowId += 1
		End

		If @myErrorListResult <> '' Begin
			Set @StatusResultOut = 0
			Set @MessageResultOut = @myErrorListResult
			Set @DistributionToRetarificOut = Null
			Set @ListNewServiceOrderDetailOut = Null
			Return	
		End

		Update dr Set ServiceOrderDetailSurgicalXml = (
			Select * From @RetarificServiceOrderDetailSurgical rs
			Where rs.ServiceOrderDetailId = dr.ServiceOrderDetailId For Xml Path('ServiceOrderDetailSurgical'), Elements
		)		
		From @distributionToRetarific dr

		--Select Convert(Bit, 1) As [StatusResult], '' As [MessageResult], Cast((Select * From @distributionToRetarific For Xml Path('ServiceOrderDetailDistribution'), Elements) As Xml) As DistributionToRetarific
		--	, Cast((Select * From @listNew For Xml Path('ListNewServiceOrderDetail'), Elements) As Xml) As ListNewServiceOrderDetail
		Set @StatusResultOut = 1
		Set @MessageResultOut = ''
		Set @DistributionToRetarificOut = Cast((Select * From @distributionToRetarific For Xml Path('ServiceOrderDetailDistribution'), Elements) As Xml)
		Set @ListNewServiceOrderDetailOut = Cast((Select * From @listNew For Xml Path('ListNewServiceOrderDetail'), Elements) As Xml)

	End Try
	Begin Catch
		--Select Convert(Bit, 0) As [StatusResult], ERROR_MESSAGE() As [MessageResult], Null As DistributionToRetarific, Null As ListNewServiceOrderDetail
		Set @StatusResultOut = 0
		Set @MessageResultOut = (Select ERROR_MESSAGE())
		Set @DistributionToRetarificOut = Null
		Set @ListNewServiceOrderDetailOut = Null
	End Catch
	
End
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de retarificación de servicios en un folio de control de ingresos (Revenue Control Detail). Recibe un listado de homologaciones en formato XML y recalcula los precios, tarifas y descuentos de los servicios de una orden médica para un paciente dado (según sexo y fecha de nacimiento), aplicando cambios de tarifas manuales o automáticas. Valida que el folio esté en estado abierto (no facturado, bloqueado, anulado o asociado) antes de procesar; si el folio no es válido, retorna un mensaje descriptivo del estado actual. Devuelve como parámetros de salida: el resultado del proceso, los mensajes de error o éxito, la distribución de costos retarificada y el listado actualizado de detalles de orden de servicio con sus nuevos valores de venta, descuentos de terceros (aseguradoras/EPS) y precios totales, listo para ser usado en el proceso de facturación o liquidación de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_RunChangeRatesServices_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_RunChangeRatesServices_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RevenueControlDetail.Status NOT IN (1) (folio no activo) → Construye mensaje según Status (2=Facturado, 3=Bloqueado, 4=Anulado, 5=Reconocimiento Ingresos, 6=Factura Asociada), retorna StatusResultOut=0 y aborta; si @OnlyRateChange = 1 OR LastCaregroupId <> @CareGroupId AND DistributionType NOT IN (2,5) → Procesa el ítem para retarificación else Se omite el detalle de distribución; si @_CUPSAssociateService = 0 (servicio, no asociado a CUPS) y @_RecordType = 1 (servicio) → Procesa retarificación de servicio: si hay homologaciones activas las recorre, si no busca homologación CUPS vía Contract.GetHomologationCups; si @_RecordType = 2 (medicamento) → Aplica tarifación de producto: usa ProductRateGeneral si existe regla por tipo/grupo/subgrupo, si no usa ProductRateDetail (tarifa normal); si @RateType = 1 (tarifa fija) → rateManualSalePrice = SalesValueWithSurcharge si SurchargeApply=1, sino SalesValue; si @RateType = 2 (tarifa porcentual) → rateManualSalePrice = costo*(1+%/100); usa AverageCost si PercentageBasedOn=1, FinalCost si =2; si @LiquidationType IN (2,3) (Servicio o Tarifa-Servicio) y @RateType<>0 → Inserta detalle en Billing.ProductServiceDetail con la tarifa del producto y luego suma valor del CUPS asociado; si COUNT(GetHomologationCups) <> 1 para producto en liquidación Servicio/Tarifa-Servicio → Aborta con mensaje ''El CUPS Parametrizado para la tarifa del producto tiene mas de una Homologación''; si @_SettlementType = 1 (Manual de Tarifas) → Calcula precio con SetValueSalesPrice (incluye o no IVA según CompanySettings.SalePriceIncludeTax), aplica descuento de tercero, ThirdPartyPercentage=100, paciente=0; si @_SettlementType = 2 (PorcentajeOtroServicio) → SubTotalSalesPrice_1 = SubTotalSalesPrice del IncludeServiceOrderDetailId * RecoveryRatio/100; si @_SettlementType = 3 (IncluidoOtroServicio) → Pone todos los valores en 0 y, si hay quirúrgicos del detalle, también pone TotalSalesPrice=0 en @RetarificServiceOrderDetailSurgical; si @_SettlementType = 4 (PorcentajeMismoServicio) → SubTotalSalesPrice_1 = SubTotalSalesPrice_1 * RecoveryRatio/100, recalcula totales; si @__Presentation = 2 (Quirúrgico) y se procesan filas de RetarificServiceOrderDetailSurgical → Asigna profesional al detalle quirúrgico según ServiceClass del IPSService (1=Ninguno, 2=Cirujano, 3=Anestesiólogo, 4=Ayudante, 5=Derecho Sala, 6=Materiales Sutura, 7=Instrumentación Quirúrgica); si RIPSConcept del CUPSEntity IN (''12'',''13'') → Forza @round = 1 (no usa RoundService del manual tarifario) else Usa ISNULL(RateManual.RoundService,1); si @__StatusResult = 1 de GetValueService (primera homologación) → Llama Contract.SetServiceValue, actualiza @distributionToRetarific con valores retarificados e inserta en @listNew; si GetValueService falla (StatusResult=0) → Acumula MessageResult en @myErrorListResult; si GetIncomeMainAccount.StateResult = 0 → Aborta con StatusResultOut=0 y el MessageResult devuelto; si ConditionType=2 y no se halla ProductRateDetailId en condiciones por rango → Acumula error ''El producto X no se encuentra dentro de la tarifa para el grupo de atención Y y fecha Z''; si @myErrorListResult <> '''' al final del bucle → Aborta con StatusResultOut=0 y el listado de errores acumulados', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_RunChangeRatesServices_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Contract.GetValueService; Contract.SetServiceValue; Contract.GetHomologationCups; Billing.GetIncomeMainAccount; Billing.SetValueSalesPrice; Billing.RoundValue', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_RunChangeRatesServices_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControlDetail; Billing.Invoice; Billing.ServiceOrder; Contract.IPSService; Contract.CUPSEntity; Contract.RateManual; Billing.ServiceOrderDetail; GeneralLedger.CompanySettings; Inventory.InventoryProduct; Contract.CareGroup; Inventory.ProductRateGeneral; Inventory.ProductRateGeneralCondition; GeneralLedger.GeneralLedgerIVA; Inventory.ProductRate; Inventory.ProductRateDetail; Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Billing.RevenueControl; Common.Person', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_RunChangeRatesServices_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_RunChangeRatesServices_Output';
-- GO
