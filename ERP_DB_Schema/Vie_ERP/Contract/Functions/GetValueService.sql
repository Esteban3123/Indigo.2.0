CREATE Function [Contract].[GetValueService]
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
	Id Int,-- Primary Key,
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
	Select StatusResult, MessageResult, Id, CostCenterId, ServiceType, CodeNameIpsService, CodeNameCups
		,CodeNameFunctionalUnit, CodeNameCostCenter, AllowValueChange, DefinitionRateDetailId, DefinitionRateDetailConditionId
		, LiquidationType, Presentation, IsSOAT, RateManualType, RateManualId, SubTotalSalesPrice, RateManualSalePrice
		, TotalSalesPrice, PerformsHealthProfessionalThirdPartyId, GrandTotalSalesPrice, CostValue
		, Recordtype, CareGroupId, CUPSEntityId, IPSServiceId, InvoicedQuantity, ServiceDate, PerformsFunctionalUnitId
		, PerformsHealthProfessionalCode, PerformsProfessionalSpecialty, BillingConceptId, SettlementType, RateManualDetailId
		, ServiceOrderDetailSurgicalXml, IncomeMainAccountId, SurgicalInterventionType, SurchargeApply, RoundService,GrossValue,TaxValue,IvaId
	From [Contract].GetServiceValue(@AdmissionNumber, @CenterAttentionCode, @CupsEntityId, @IPSServiceId, @CareGroupId, @FunctionalUnitId, @SpecialtyId, @ServiceDate, @PatientGenus, @PatientDateBirth
		, @InvoicedQuantity, @ProfessionalHealthCode, @ProfessionalHealthThirdPartyId, @RiasId, @ContractDescriptionId)
	
	If (Select Top 1 StatusResult From @ServiceOrderDetail) = 1 Begin
		
		Declare @StatusResult Bit, @MessageResult Varchar(Max)

		Declare @ServiceOrderDetailXml Xml = (
			Select * From @ServiceOrderDetail For Xml Path('ServiceOrderDetail'), Elements
		)
		Declare @ServiceOrderDetailSurgicalXml Xml = (
			Select Top 1 ServiceOrderDetailSurgicalXml From @ServiceOrderDetail-- For Xml Path('ServiceOrderDetailSurgical'), Elements
		)

		Declare @ServiceOrderDetailTmp Table (StatusResult Bit,
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
		
		Insert Into @ServiceOrderDetailTmp
			Select	StatusResult,
					MessageResult,
					Id,
					CostCenterId,
					ServiceType,
					CodeNameIpsService,
					CodeNameCups,
					CodeNameFunctionalUnit,
					CodeNameCostCenter,
					AllowValueChange,
					DefinitionRateDetailId,
					DefinitionRateDetailConditionId,
					LiquidationType,
					Presentation,
					IsSOAT,
					RateManualType,
					RateManualId,
					SubTotalSalesPrice,
					RateManualSalePrice,
					TotalSalesPrice,
					PerformsHealthProfessionalThirdPartyId,
					GrandTotalSalesPrice,
					CostValue,
					Recordtype,
					CareGroupId,
					CUPSEntityId,
					IPSServiceId,
					InvoicedQuantity,
					ServiceDate,
					PerformsFunctionalUnitId,
					PerformsHealthProfessionalCode,
					PerformsProfessionalSpecialty,
					BillingConceptId,
					SettlementType,
					RateManualDetailId,
					ServiceOrderDetailSurgicalXml,
					IncomeMainAccountId,
					SurgicalInterventionType,
					SurchargeApply,
					RoundService,
					GrossValue,
					TaxValue,
					IvaId
			From [Contract].RefactorServiceValue(@ServiceOrderDetailXml, @ServiceOrderDetailSurgicalXml)

		Delete From @ServiceOrderDetail
		Insert Into @ServiceOrderDetail
		Select * From @ServiceOrderDetailTmp

	End

	Return
End