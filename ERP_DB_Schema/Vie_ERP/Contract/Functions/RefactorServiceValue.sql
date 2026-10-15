CREATE Function [Contract].[RefactorServiceValue]
(
	@ServiceOrderDetailXml Xml,
	@ServiceOrderDetailSurgicalXml Xml Null
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
	RateManualSalePrice Decimal(20,2),
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
		t.x.value('RoundService[1]', 'int'),
		t.x.value('GrossValue[1]', 'Numeric(20,2)'),
		t.x.value('TaxValue[1]', 'numeric(20,2)'),
		t.x.value('IvaId[1]', 'int')
	From @ServiceOrderDetailXml.nodes('/ServiceOrderDetail') t(x)

	Declare @Presentation Tinyint, @IPSServiceId Int, @SurgicalInterventionType Tinyint, @RateManualId Int

	Select @Presentation = Presentation, @IPSServiceId = IPSServiceId
		, @SurgicalInterventionType = SurgicalInterventionType, @RateManualId = RateManualId
	From @ServiceOrderDetail

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

	If @Presentation = 2 Begin
		Declare @listSurgicalProcedureService Table(
			  IPSServiceParentId Int
			, IPSServiceId Int
			, ServiceAmount Int
			, DefaultService Bit
			, ValueItemServiceOrderDetail Decimal(18, 2) Null
			, CodeNameService Varchar(300)
			, ClassService Varchar(30)
			, PerformsHealthProfessionalCode Char(20) Null)

		Declare @listSurgicalDefault Table(
			RowId Int Primary Key Identity(1,1)
			, IPSServiceParentId Int
			, IPSServiceId Int
			, ServiceAmount Int
			, ClassService Varchar(30)
			, ValueItemServiceOrderDetail Decimal(18, 2)
			, PerformsHealthProfessionalCode Char(20))

		Insert Into @listSurgicalProcedureService
		Select sps.IPSServiceParentId
			, sps.IPSServiceId
			, sps.ServiceAmount
			, DefaultService
			, Null
			, Concat(ips.Code, ' - ', ips.[Name])
			, (Case ips.ServiceClass When 1 Then 'Ninguno' 
				When 2 Then 'Cirujano' 
				When 3 Then 'Anestesiólogo' 
				When 4 Then 'Ayudante' 
				When 5 Then 'Derecho Sala' 
				When 6 Then 'Materiales Sutura' 
				When 7 Then 'Instrumentación Quirúrgica' 
				Else '' End)
			, PerformsHealthProfessionalCode
		From [Contract].SurgicalProcedureService sps With(Nolock)
		Inner Join [Contract].IPSService ips With(Nolock) On sps.IPSServiceId = ips.Id
		Where sps.IPSServiceParentId = @IPSServiceId Order By ips.ServiceClass Asc

		Declare @ClassServiceS Varchar(30),
			@ServiceAmountS Int,
			@IPSServiceIdS Int,
			@IPSServiceParentIdS Int,
			@RowId Int

		Insert Into @listSurgicalDefault
		Select IPSServiceParentId, IPSServiceId, ServiceAmount
			, ClassService, ValueItemServiceOrderDetail, PerformsHealthProfessionalCode
		From @listSurgicalProcedureService Where DefaultService = 1
		
		Declare @Rows Int, @RowIdx Int
		Set @Rows = 1
		Set @RowIdx = 1

		Declare @tmp Table(RowId INT IDENTITY(1,1) PRIMARY KEY CLUSTERED, InvoiceId Int, invoiceNumber Varchar(20))

		Insert Into @tmp
		Select Id, InvoiceNumber FROM Billing.Invoice

		While @Rows > 0
		begin
			
			Select Top 1 @RowIdx = RowId, @ClassServiceS = ClassService, @ServiceAmountS = ServiceAmount
				, @IPSServiceIdS = IPSServiceId, @IPSServiceParentIdS = IPSServiceParentId, @RowId = RowId 
			From @listSurgicalDefault
			Where RowId >= @RowIdx Order By RowId

			Set @Rows = @@ROWCOUNT
			If @Rows = 0 
				Break

			--si el item es derecho a sala busco el ips para materiales de sutura
			If @ClassServiceS = 'Derecho Sala' Begin
				
				If @SurgicalInterventionType Is Null Or @SurgicalInterventionType < 9 Begin	
				
					Declare @listSurgicalProcedureServiceXml Xml = (
						Select * From @listSurgicalProcedureService For Xml Path('ListSurgicalProcedureService'), Elements
					)

					Declare @ServiceOrderDetailTmp Table (StatusResult Bit,
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
						ListSurgicalProcedureServiceXml Xml,
						GrossValue numeric(20,2),
						TaxValue numeric(20,2),
						IvaId int
					) 

					Delete From @ServiceOrderDetailTmp

					Insert Into @ServiceOrderDetailTmp
						Select StatusResult,
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
								ListSurgicalProcedureServiceXml,
								GrossValue,
								TaxValue,
								IvaId
						From [Contract].GetIPSSutureMaterials(@IPSServiceIdS, @IPSServiceParentIdS, @ServiceOrderDetailXml, @ServiceOrderDetailSurgicalXml, @listSurgicalProcedureServiceXml)
					

					Declare @listSurgicalProcedureServiceNewXml Xml = NUll
					Select @listSurgicalProcedureServiceNewXml = ListSurgicalProcedureServiceXml
					From @ServiceOrderDetailTmp

					Delete From @listSurgicalProcedureService
					Insert Into @listSurgicalProcedureService
					Select t.x.value('IPSServiceParentId[1]', 'Int'),
						t.x.value('IPSServiceId[1]', 'Int'),
						t.x.value('ServiceAmount[1]', 'Int'),
						t.x.value('DefaultService[1]', 'Bit'),
						t.x.value('ValueItemServiceOrderDetail[1]', 'Decimal(18, 2)'),
						t.x.value('CodeNameService[1]', 'Varchar(300)'),
						t.x.value('ClassService[1]', 'Varchar(30)'),
						t.x.value('PerformsHealthProfessionalCode[1]', 'Char(20)')
					From @listSurgicalProcedureServiceNewXml.nodes('ListSurgicalProcedureService') t(x)
					
					If (Select Top 1 StatusResult From @ServiceOrderDetailTmp) = 0 Begin
						Update @ServiceOrderDetail Set StatusResult = 0, MessageResult = (Select Top 1 MessageResult From @ServiceOrderDetailTmp)						
						Return
					End

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
								GrossValue,
								TaxValue,
								IvaId
						From @ServiceOrderDetailTmp

					Declare @NewSurgical Xml = (Select Top 1 ServiceOrderDetailSurgicalXml From @ServiceOrderDetail)
					
					Declare @serviceOrderDetailSurgicalTmp Table(RowId Int,
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

					Insert Into @serviceOrderDetailSurgicalTmp
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
					From @NewSurgical.nodes('/ServiceOrderDetailSurgical') t(x)
					Declare @RowIDLast Int = (Select Max(RowId) From @serviceOrderDetailSurgicalTmp)
					
					Update @serviceOrderDetailSurgicalTmp Set IncomeMainAccountId = (Select Top 1 IncomeMainAccountId From @ServiceOrderDetail)
					Where RowId = @RowIDLast

					--Delete From @serviceOrderDetailSurgical
					--Insertamos el último registro surgical que fue el que se agrego en GetServiceValueBySurgicalProcedureService
					If (Select Count(1) From @serviceOrderDetailSurgical Where RowId = @RowIDLast) = 0 Begin
						Insert Into @serviceOrderDetailSurgical
						Select * From @serviceOrderDetailSurgicalTmp Where RowId = @RowIDLast

						update @ServiceOrderDetail Set ServiceOrderDetailSurgicalXml = (
							select * From @serviceOrderDetailSurgical For Xml Path('ServiceOrderDetailSurgical'), Elements
						)
					End
				End
				Else Begin

					--si es cruento consulto el manual de tarifas para obtener el ips de materiales
					Declare @MaterialNoBloodyIPSServiceId Int,
						@ipsSutureMaterialsId Int,
						@IpsSutureMaterialsCodeName Varchar(320)

					Select Top 1 @MaterialNoBloodyIPSServiceId = MaterialNoBloodyIPSServiceId
					From [Contract].RateManual With(Nolock) Where Id = @RateManualId

					Select @ipsSutureMaterialsId = Id, @IpsSutureMaterialsCodeName = Concat(Code, ' - ', [Name])
					From [Contract].IPSService With(Nolock) Where Id = @MaterialNoBloodyIPSServiceId

					Insert Into @listSurgicalProcedureService
					Values (@IPSServiceId, @ipsSutureMaterialsId, 1, 1, 0, @IpsSutureMaterialsCodeName, 'Materiales Sutura', Null)

					Update @listSurgicalDefault Set ValueItemServiceOrderDetail = Coalesce((Select Top 1 TotalSalesPrice From @serviceOrderDetailSurgical Where IPSServiceId = @IPSServiceIdS), 0)
						, PerformsHealthProfessionalCode = (Select Top 1 PerformsHealthProfessionalCode From @serviceOrderDetailSurgical Where IPSServiceId = @IPSServiceIdS)
					Where RowId = @RowId

					--asigno valores al item materiales de sutura 
					Update @listSurgicalProcedureService Set ValueItemServiceOrderDetail = Coalesce((Select Top 1 TotalSalesPrice From @serviceOrderDetailSurgical Where IPSServiceId = @ipsSutureMaterialsId), 0)
						, PerformsHealthProfessionalCode = (Select Top 1 PerformsHealthProfessionalCode From @serviceOrderDetailSurgical Where IPSServiceId = @ipsSutureMaterialsId)
					Where ClassService = 'Materiales Sutura'
				End
			End
			Else Begin
				Update @listSurgicalDefault Set ValueItemServiceOrderDetail = Coalesce((Select Top 1 TotalSalesPrice From @serviceOrderDetailSurgical Where IPSServiceId = @IPSServiceIdS), 0)
					, PerformsHealthProfessionalCode = (Select Top 1 PerformsHealthProfessionalCode From @serviceOrderDetailSurgical Where IPSServiceId = @IPSServiceIdS)
				Where RowId = @RowId
			End

			Set @RowIdx += 1
		End

	End	
		
	Return
End