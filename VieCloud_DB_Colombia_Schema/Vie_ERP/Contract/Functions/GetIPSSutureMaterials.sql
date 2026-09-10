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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que determina y valida los materiales de sutura asociados a un procedimiento quirúrgico dentro de una orden de servicio. Recibe como entrada el identificador del servicio IPS quirúrgico, su servicio padre, y tres estructuras XML con el detalle de la orden, los servicios quirúrgicos adicionales y la lista de procedimientos del acto quirúrgico. Consulta el catálogo maestro de servicios de la IPS (Contract.IPSService) para identificar los materiales de sutura vinculados al procedimiento, y utiliza la función Contract.GetServiceValueBySurgicalProcedureService para calcular el valor tarifario de cada material según el contrato aplicable. Retorna una tabla con el detalle completo de cada ítem de la orden —incluyendo precios, cantidades facturadas, tipo de liquidación, centro de costo, profesional ejecutante, código CUPS y valores de IVA— lista para ser incorporada en el proceso de facturación y liquidación de cirugías.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetIPSSutureMaterials';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetIPSSutureMaterials';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Agrega al detalle quirúrgico el material de sutura asociado al servicio IPS y recalcula los valores del servicio incorporando dicho material.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetIPSSutureMaterials';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El servicio IPS quirúrgico debe existir en Contract.IPSService; Los XML de entrada deben respetar el esquema esperado (ServiceOrderDetail, ServiceOrderDetailSurgical, ListSurgicalProcedureService)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetIPSSutureMaterials';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todos los renglones de la lista de servicios quirúrgicos quedan con ValueItemServiceOrderDetail=0 y PerformsHealthProfessionalCode=NULL antes del recálculo; El material de sutura asociado se agrega siempre como servicio por defecto (DefaultService=1) y con cantidad 1; El detalle final siempre incluye la serialización XML de la lista de servicios quirúrgicos; Solo se consideran como insumo del recálculo los servicios marcados como DefaultService=1', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetIPSSutureMaterials';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Servicio IPS; Procedimiento quirúrgico; Material de sutura; Orden de servicio; Manual tarifario; Liquidación de servicios; Centro de costo; Unidad funcional; Profesional de la salud', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetIPSSutureMaterials';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @ServiceOrderDetail: Carga inicial del detalle de la orden de servicio a partir del XML de entrada (ServiceOrderDetailXml); [INSERT] @listSurgicalProcedureService: Cuando el IPSService quirúrgico tiene AssociatedMaterialIPSServiceId no nulo y >0, se agrega un renglón de material de sutura con ServiceAmount=1, DefaultService=1, ClassService=''Materiales Sutura''; [UPDATE] @listSurgicalProcedureService: Tras la carga, se fuerza ValueItemServiceOrderDetail=0 y PerformsHealthProfessionalCode=NULL en todos los renglones; [DELETE] @ServiceOrderDetail: Cuando GetServiceValueBySurgicalProcedureService retorna StatusResult=1, se borra el contenido previo del detalle para reemplazarlo por el recalculado; [INSERT] @ServiceOrderDetail: Cuando StatusResult=1 del recálculo, se reinserta el detalle con los valores devueltos por GetServiceValueBySurgicalProcedureService; [UPDATE] @ServiceOrderDetail: Cuando StatusResult del recálculo no es 1, se marca StatusResult=0 y se propaga el MessageResult del resultado temporal; [UPDATE] @ServiceOrderDetail: Siempre al final se serializa la lista de servicios del procedimiento quirúrgico (incluido el material de sutura) como XML en ListSurgicalProcedureServiceXml; [RETURN_RESULT] @ServiceOrderDetail: Devuelve la tabla @ServiceOrderDetail con el detalle recalculado o con el mensaje de error según el resultado del recálculo', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetIPSSutureMaterials';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AssociatedMaterialIPSServiceId del IPSService quirúrgico no es nulo y mayor a 0 → Consulta el código-nombre del IPS de material y agrega un renglón de ''Materiales Sutura'' a la lista de servicios quirúrgicos else No se agrega material de sutura a la lista; si StatusResult del resultado de GetServiceValueBySurgicalProcedureService = 1 → Reemplaza el detalle de la orden con el resultado recalculado else Conserva el detalle original pero marca StatusResult=0 y propaga el MessageResult del error', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetIPSSutureMaterials';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Contract.GetServiceValueBySurgicalProcedureService', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetIPSSutureMaterials';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.IPSService', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetIPSSutureMaterials';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetIPSSutureMaterials';
GO
