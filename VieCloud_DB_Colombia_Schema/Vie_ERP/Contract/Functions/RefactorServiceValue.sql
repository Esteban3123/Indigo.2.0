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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que recalcula y desglosa los valores de los ítems de una orden de servicio a partir de dos entradas XML: el detalle general del servicio y el detalle quirúrgico opcional. Convierte el XML en filas estructuradas con precios de venta, subtotales, impuestos, valor de costo, tipo de liquidación y datos del profesional que ejecuta el servicio. Cuando el ítem corresponde a una presentación de tipo paquete quirúrgico (Presentation = 2), consulta el catálogo de servicios asociados al procedimiento quirúrgico en Contract.SurgicalProcedureService y Contract.IPSService para expandir automáticamente los servicios e insumos incluidos en la cirugía, respetando los valores por defecto, cantidades pactadas en contrato y el profesional asignado. El resultado se usa en el proceso de facturación y liquidación de órdenes de servicio, garantizando que cada concepto cobrado tenga su valor tarifario, código CUPS, centro de costo y datos de SOAT o tarifa manual correctamente refactorizados antes de generar la factura en Billing.Invoice.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'RefactorServiceValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'RefactorServiceValue';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reconstruye desde XML el detalle de una orden de servicio y, cuando es un paquete quirúrgico, recalcula los servicios componentes (cirujano, anestesiólogo, ayudante, derecho de sala, materiales de sutura, etc.) ajustando valores y materiales según el tipo de intervención.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'RefactorServiceValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de detalle debe contener nodos /ServiceOrderDetail con los campos esperados.; Si se envía XML quirúrgico debe contener nodos /ServiceOrderDetailSurgical.; Para procesar lógica quirúrgica, Presentation debe ser 2 y debe existir IPSServiceId con configuración en Contract.SurgicalProcedureService.; Para la rama de cirugía cruenta (SurgicalInterventionType >= 9) debe existir un RateManual con MaterialNoBloodyIPSServiceId definido.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'RefactorServiceValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La clasificación textual de ServiceClass se mapea a etiquetas fijas: 1=Ninguno, 2=Cirujano, 3=Anestesiólogo, 4=Ayudante, 5=Derecho Sala, 6=Materiales Sutura, 7=Instrumentación Quirúrgica.; Los servicios componentes solo se procesan cuando son DefaultService = 1.; Si no hay surgical con el IPS buscado, ValueItemServiceOrderDetail queda en 0 (Coalesce).; La lógica quirúrgica solo aplica para Presentation = 2.; SurgicalInterventionType >= 9 se considera cirugía ''cruenta'' y obliga a tomar materiales desde el manual de tarifas en lugar de calcularlos.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'RefactorServiceValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @ServiceOrderDetail: Siempre se materializa el contenido del XML de entrada en la tabla resultado, mapeando cada nodo /ServiceOrderDetail a una fila.; [UPDATE] @ServiceOrderDetail: Cuando Presentation=2, el SurgicalInterventionType es nulo o <9, y la función Contract.GetIPSSutureMaterials retorna StatusResult=0, se marca StatusResult=0 y se asigna su MessageResult, retornando inmediatamente.; [DELETE] @ServiceOrderDetail: Cuando Presentation=2 y se obtienen materiales de sutura exitosamente desde GetIPSSutureMaterials, se borra el resultado previo y se reemplaza con la salida de esa función.; [UPDATE] @ServiceOrderDetail: Tras recargar los surgicals desde el nuevo XML, se actualiza ServiceOrderDetailSurgicalXml con la lista quirúrgica recompuesta cuando se incorporó el último registro de materiales.; [INSERT] @serviceOrderDetailSurgical: Si el último RowId producido por GetIPSSutureMaterials no existe aún en la lista quirúrgica, se inserta ese registro (representa el ítem de materiales de sutura agregado).; [INSERT] @listSurgicalProcedureService: En cirugía cruenta (SurgicalInterventionType>=9 y clase ''Derecho Sala''), se agrega manualmente un ítem ''Materiales Sutura'' con el IPS configurado en RateManual.MaterialNoBloodyIPSServiceId.; [UPDATE] @listSurgicalProcedureService: En cirugía cruenta, el ítem ''Materiales Sutura'' recibe ValueItemServiceOrderDetail y PerformsHealthProfessionalCode tomados del surgical correspondiente al IPS de materiales.; [UPDATE] @listSurgicalDefault: Para cada servicio default del paquete, se asigna ValueItemServiceOrderDetail con TotalSalesPrice del surgical homólogo (o 0 si no existe) y se copia PerformsHealthProfessionalCode.; [UPDATE] @serviceOrderDetailSurgicalTmp: Al último RowId del XML quirúrgico recompuesto se le asigna IncomeMainAccountId tomado del detalle principal.; [RETURN_RESULT] RETURN_RESULT: La función retorna la tabla @ServiceOrderDetail tras los recálculos quirúrgicos (o tal cual si Presentation<>2).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'RefactorServiceValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Presentation = 2 → Se ejecuta la lógica de paquete quirúrgico: se cargan servicios componentes desde SurgicalProcedureService y se itera sobre los servicios default. else Se retorna el detalle tal como vino del XML, sin recálculos.; si ClassService del ítem iterado = ''Derecho Sala'' → Se procesa la obtención/asignación de materiales de sutura. else Solo se actualiza ValueItemServiceOrderDetail y PerformsHealthProfessionalCode del default con los datos del surgical correspondiente.; si SurgicalInterventionType IS NULL OR SurgicalInterventionType < 9 → Se invoca Contract.GetIPSSutureMaterials para obtener los materiales y se reemplaza el detalle y la lista quirúrgica con su salida. else Cirugía cruenta: se obtiene MaterialNoBloodyIPSServiceId desde RateManual y se agrega manualmente el ítem ''Materiales Sutura'' a la lista.; si GetIPSSutureMaterials retorna StatusResult = 0 → Se propaga StatusResult=0 y MessageResult al detalle y se aborta la función.; si El RowId del último surgical no existe ya en @serviceOrderDetailSurgical → Se inserta el nuevo ítem y se reconstruye el ServiceOrderDetailSurgicalXml del detalle.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'RefactorServiceValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Contract.GetIPSSutureMaterials', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'RefactorServiceValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.SurgicalProcedureService; Contract.IPSService; Billing.Invoice; Contract.RateManual; Contract.GetIPSSutureMaterials', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'RefactorServiceValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'RefactorServiceValue';
GO
