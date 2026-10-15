CREATE Function [Contract].[GetIPSSutureMaterials]
(
	@surgicalProcedureServiceIPSServiceId Int,
	@surgicalProcedureServiceIPSServiceParentId Int,
	@ServiceOrderDetailXml Xml,
	@ServiceOrderDetailSurgicalXml Xml Null,
	@listSurgicalProcedureServiceXml Xml Null
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
	ListSurgicalProcedureServiceXml Xml,
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
		t.x.value('RoundService[1]', 'int'),
		Null,
		t.x.value('GrossValue[1]', 'NUMERIC(20,2)'),
		t.x.value('TaxValue[1]', 'NUMERIC(20,2)'),
		t.x.value('IvaId[1]', 'int')
	From @ServiceOrderDetailXml.nodes('/ServiceOrderDetail') t(x)


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


	Declare @listSurgicalProcedureService Table(
		IPSServiceParentId Int
		, IPSServiceId Int
		, ServiceAmount Int
		, DefaultService Bit
		, ValueItemServiceOrderDetail Decimal(18, 2) Null
		, CodeNameService Varchar(300)
		, ClassService Varchar(30)
		, PerformsHealthProfessionalCode Char(20) Null)

	Insert Into @listSurgicalProcedureService
	Select t.x.value('IPSServiceParentId[1]', 'Int'),
		t.x.value('IPSServiceId[1]', 'Int'),
		t.x.value('ServiceAmount[1]', 'Int'),
		t.x.value('DefaultService[1]', 'Bit'),
		t.x.value('ValueItemServiceOrderDetail[1]', 'Decimal(18, 2)'),
		t.x.value('CodeNameService[1]', 'Varchar(300)'),
		t.x.value('ClassService[1]', 'Varchar(30)'),
		t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)')
	From @listSurgicalProcedureServiceXml.nodes('/ListSurgicalProcedureService') t(x)

	Declare @AssociatedMaterialIPSServiceId Int

	Select @AssociatedMaterialIPSServiceId = AssociatedMaterialIPSServiceId
	From [Contract].IPSService With(Nolock) Where Id = @surgicalProcedureServiceIPSServiceId

	If @AssociatedMaterialIPSServiceId Is Not Null And @AssociatedMaterialIPSServiceId > 0 Begin
		--consulto el ips para el material de sutura
		
		Declare @MaterialIpsServiceCodeName Varchar(320)
		Select @MaterialIpsServiceCodeName = Concat(Code,  ' - ', [Name])
		From [Contract].IPSService With(Nolock) Where Id = @AssociatedMaterialIPSServiceId

		Insert Into @listSurgicalProcedureService
		Values (@surgicalProcedureServiceIPSServiceParentId, @AssociatedMaterialIPSServiceId, 1, 1, 0, @MaterialIpsServiceCodeName, 'Materiales Sutura', Null)

	End

	Update @listSurgicalProcedureService Set ValueItemServiceOrderDetail = 0, PerformsHealthProfessionalCode = Null

	Declare @listSurgicalDefault Table(
		RowId Int Primary Key Identity(1,1)
		, IPSServiceId Int
		, ServiceAmount Int
		, ClassService Varchar(30)
		, ValueItemServiceOrderDetail Decimal(18, 2)
		, PerformsHealthProfessionalCode Char(20))
	--obetengo todos los valores por defecto 
	Insert Into @listSurgicalDefault
	Select IPSServiceId, ServiceAmount, ClassService, ValueItemServiceOrderDetail, PerformsHealthProfessionalCode
	From @listSurgicalProcedureService Where DefaultService = 1

	Declare @listSurgicalDefaultXml Xml = (
		Select * From @listSurgicalDefault For Xml Path('ListSurgicalDefault'), Elements
	)
	--consulto el valor enviando como parametro el listado de los valores por defecto y el detalle que se esta trabajando
	

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
	Select * 
	From [Contract].[GetServiceValueBySurgicalProcedureService](@ServiceOrderDetailXml, @ServiceOrderDetailSurgicalXml, @listSurgicalDefaultXml)

	If (Select Top 1 StatusResult From @ServiceOrderDetailTmp) = 1 Begin
		Delete From @ServiceOrderDetail
		
		Insert Into @ServiceOrderDetail
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
					Null,
					GrossValue,
					TaxValue,
					IvaId
			From @ServiceOrderDetailTmp
	End
	Else 
		Update @ServiceOrderDetail Set StatusResult = 0, MessageResult = (Select Top 1 MessageResult From @ServiceOrderDetailTmp)
	
	Update @ServiceOrderDetail Set ListSurgicalProcedureServiceXml = (
		select *
		From @listSurgicalProcedureService
		For Xml Path('ListSurgicalProcedureService'), Elements
	)

	Return
End