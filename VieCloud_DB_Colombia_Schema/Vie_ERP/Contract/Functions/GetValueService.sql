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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que calcula y retorna el valor tarifado de un servicio de salud para un ingreso específico, considerando parámetros como el número de admisión, centro de atención, código CUPS, servicio IPS, unidad funcional, especialidad, fecha del servicio, datos del paciente (sexo y fecha de nacimiento), cantidad facturada y profesional de salud ejecutante. Internamente invoca la función [Contract].[GetServiceValue] para obtener la tarifa base del servicio según el contrato vigente, y si el resultado es exitoso, aplica un proceso de refactorización del valor mediante [Contract].[RefactorServiceValue] para ajustar conceptos quirúrgicos y de liquidación. Devuelve una tabla detallada con el resultado de la tarifación que incluye precios de venta (subtotal, total, gran total), valor en costo, tipo de liquidación, recargos, impuestos (IVA), concepto de facturación y datos del profesional que ejecuta el servicio; es usada en el proceso de facturación y liquidación de órdenes de servicio médico dentro del módulo de contratos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetValueService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetValueService';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcular el valor de un servicio médico para una admisión, aplicando el cálculo tarifario base y, si éste es exitoso, refactorizando los valores (incluido el componente quirúrgico) para entregar el detalle final liquidable.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetValueService';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir información tarifaria/contractual consultable por Contract.GetServiceValue para los parámetros entregados (admisión, CUPS/Servicio IPS, unidad funcional, especialidad, fecha y datos del paciente); Los parámetros de paciente (género, fecha de nacimiento) y de servicio (fecha, cantidad facturada) deben ser válidos para que las funciones dependientes puedan calcular tarifa; Contract.RefactorServiceValue debe poder interpretar el XML generado a partir del resultado de GetServiceValue', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetValueService';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Si el cálculo inicial de GetServiceValue falla (StatusResult <> 1) no se ejecuta la refactorización de valores; El resultado final siempre proviene exclusivamente de RefactorServiceValue cuando el cálculo inicial es exitoso (la tabla original es eliminada antes de reinsertar); Solo se toma el primer ServiceOrderDetailSurgicalXml encontrado para alimentar la refactorización; La función no realiza escrituras sobre tablas físicas, solo retorna una tabla calculada', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetValueService';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión del paciente; Centro de atención; CUPS; Servicio IPS; Grupo de atención (CareGroup); Unidad funcional; Especialidad médica; Profesional de la salud; Tarifa / Manual tarifario; SOAT; Liquidación de servicios; Facturación (BillingConcept); Intervención quirúrgica; Centro de costo; IVA / impuestos; RIAS (Rutas Integrales de Atención en Salud); Género y fecha de nacimiento del paciente; Copago/recargos (SurchargeApply)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetValueService';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @ServiceOrderDetail: Siempre se carga inicialmente con el resultado de Contract.GetServiceValue para los parámetros recibidos; [INSERT] @ServiceOrderDetailTmp: Cuando StatusResult del primer registro = 1, se inserta el resultado de Contract.RefactorServiceValue alimentado con el XML del detalle y del componente quirúrgico; [DELETE] @ServiceOrderDetail: Cuando StatusResult = 1, se vacía la tabla original previo a reemplazarla con los valores refactorizados; [INSERT] @ServiceOrderDetail: Cuando StatusResult = 1, se reinserta con el contenido refactorizado proveniente de RefactorServiceValue; [RETURN_RESULT] @ServiceOrderDetail: Se retorna la tabla resultante (refactorizada si el cálculo fue exitoso, o el resultado base de GetServiceValue en caso contrario)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetValueService';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El primer registro retornado por GetServiceValue tiene StatusResult = 1 (cálculo exitoso) → Se serializa el resultado a XML y se invoca RefactorServiceValue para recalcular/ajustar los valores; el resultado refactorizado reemplaza el contenido original else Se conserva el resultado original de GetServiceValue sin refactorización', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetValueService';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Contract.GetServiceValue; Contract.RefactorServiceValue', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetValueService';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.GetServiceValue; Contract.RefactorServiceValue', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetValueService';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetValueService';
GO
