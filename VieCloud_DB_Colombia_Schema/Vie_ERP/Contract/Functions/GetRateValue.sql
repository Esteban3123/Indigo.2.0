CREATE Function [Contract].[GetRateValue]
(
	@CupsEntityId Int,
	@CareGroupId Int,
	@FunctionalUnitId Int,
	@SpecialtyId Char(3),
	@ServiceDate DateTime,
	@InitialService Tinyint,
	@RiasId int = 0,
	@ContractDescriptionId int = 0,
	@IPSServiceId INT
)
Returns @DefinitionRateDetail Table
(
	StateResult Bit,
	MessageResult Varchar(Max),
	ConditionType Tinyint,	
	LogicalOperator Tinyint,
	ConditionType2 Tinyint,

	DefinitionRateDetailId Int,
	LiquidateionType Tinyint Null,
	ManualType Tinyint Null,
	RateManualValidityId Int Null,
	RateManualId Int Null,
	AllowValueChange Bit,
	RateVariation Decimal(5, 2) Null,
	SalesValue Decimal(18, 2) Null,
	SalesValueWithSurcharge Decimal(18, 2) Null,

	DefinitionRateDetailConditionId Int Null,	
	LiquidateionTypeAux Tinyint Null,
	ManualTypeAux Tinyint Null,
	RateManualValidityIdAux Int Null,
	RateManualIdAux Int Null,
	RateVariationAux Decimal(5, 2) Null,
	SalesValueAux Decimal(18, 2) Null,
	SalesValueWithSurchargeAux Decimal(18, 2) Null
) 
As
Begin
	
	Declare @Id Int, @NProcedureCups Int, @ProcedureTemplateId Int
		, @CareGroupCode Varchar(20), @CareGroupName Varchar(100)

	Select @Id = cg.Id
		, @CareGroupCode = cg.Code
		, @CareGroupName = cg.[Name]
		, @ProcedureTemplateId = cg.ProcedureTemplateId
	From [Contract].CareGroup cg With(Nolock)
	Where Id = @CareGroupId
	
	Select @NProcedureCups = Count(1) 
	From [Contract].ProcedureCups With(Nolock)
	Where ProceduresTemplateId = @ProcedureTemplateId And CupsId = @CupsEntityId and (ContractDescriptionId is null or ContractDescriptionId = @ContractDescriptionId )

	If @NProcedureCups > 0 Begin
		
		Insert Into @DefinitionRateDetail
		Select StateResult, MessageResult, ConditionType, LogicalOperator, ConditionType2
			, DefinitionRateDetailId, LiquidateionType, ManualType, RateManualValidityId, RateManualId, AllowValueChange, RateVariation, SalesValue, SalesValueWithSurcharge
			, DefinitionRateDetailConditionId, LiquidateionTypeAux, ManualTypeAux, RateManualValidityIdAux, RateManualIdAux, RateVariationAux, SalesValueAux, SalesValueWithSurchargeAux
		From [Contract].[GetRateValueCondition](@CupsEntityId, @CareGroupId, @FunctionalUnitId, @SpecialtyId, @ServiceDate, @InitialService, @RiasId, @ContractDescriptionId, @IPSServiceId)
		
	End
	Else Begin
		Declare @CodeCups Varchar(20), @NameCups Varchar(300)
		Select @CodeCups = Code, @NameCups = [Description] From [Contract].CUPSEntity With(Nolock)
		Where Id = @CupsEntityId
		Insert Into @DefinitionRateDetail
		Values (0, 'El CUPS ' + @CodeCups + ' - ' + @NameCups + ' no esta cubierto en el grupo de atencion ' + @CareGroupCode + ' - ' + @CareGroupName
			,0,0,0,0,Null,Null,Null,0,Null,Null,Null,Null,Null,Null,Null,Null,Null,NULL, NULL, NULL)
	End	
	Return

End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que determina el valor tarifario aplicable a un procedimiento CUPS dentro de un contrato, dado un grupo de atención, unidad funcional, especialidad y fecha de servicio. Primero verifica si el CUPS está cubierto en la plantilla de procedimientos del grupo de atención (Contract.ProcedureCups); si está cubierto, delega el cálculo de condiciones tarifarias a la función Contract.GetRateValueCondition, que evalúa reglas y condiciones contractuales para devolver el detalle de liquidación (tipo de liquidación, valor de venta, recargo, manual de tarifas, variación). Si el CUPS no está cubierto en el grupo de atención, retorna un resultado negativo con un mensaje descriptivo indicando que el procedimiento no está contratado. Es el núcleo del motor tarifario contractual, utilizado en la liquidación y facturación de servicios de salud prestados a una entidad pagadora.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetRateValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetRateValue';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina el valor tarifario de un procedimiento CUPS dentro de un grupo de atención contractual, delegando el cálculo a una función de condiciones o devolviendo un mensaje de no cobertura.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El grupo de atención debe existir en Contract.CareGroup y tener una plantilla de procedimientos asociada (ProcedureTemplateId).; El CUPS debe existir en Contract.CUPSEntity para poder construir el mensaje de no cobertura.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre devuelve exactamente una fila de resultado (positiva con tarifa, o negativa con mensaje de no cobertura).; Un CUPS solo se considera cubierto si está asociado a la plantilla de procedimientos del grupo de atención.; El filtro por ContractDescriptionId solo se aplica si el ProcedureCups tiene ese campo informado; ProcedureCups con ContractDescriptionId NULL aplica para cualquier descripción de contrato.; No realiza modificaciones en tablas físicas; es una función de solo lectura.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'CUPS (Clasificación Única de Procedimientos en Salud); Grupo de atención; Plantilla de procedimientos; Contrato / descripción de contrato; Tarifa contractual; Liquidación; Manual tarifario; Valor de venta y recargo; Cobertura de procedimiento', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetail: Cuando existe al menos un ProcedureCups que vincule el CUPS con la plantilla del grupo de atención (y opcionalmente con la descripción de contrato), se inserta el resultado devuelto por Contract.GetRateValueCondition.; [INSERT] @DefinitionRateDetail: Cuando no existe vínculo ProcedureCups para el CUPS en la plantilla del grupo de atención, se inserta una fila con StateResult=0 y mensaje indicando que el CUPS no está cubierto en el grupo de atención.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe al menos un registro en ProcedureCups que relacione la plantilla de procedimientos del grupo de atención con el CUPS (filtrando opcionalmente por ContractDescriptionId) → Se delega el cálculo tarifario a Contract.GetRateValueCondition y se retorna su resultado. else Se retorna un resultado negativo (StateResult=0) con mensaje indicando que el CUPS no está cubierto en el grupo de atención.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Contract.GetRateValueCondition', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CareGroup; Contract.ProcedureCups; Contract.CUPSEntity; Contract.GetRateValueCondition', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValue';
GO
