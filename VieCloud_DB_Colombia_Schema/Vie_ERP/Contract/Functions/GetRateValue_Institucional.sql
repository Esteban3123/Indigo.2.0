CREATE Function [Contract].[GetRateValue_Institucional]
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
		From [Contract].[GetRateValueCondition_Institucional](@CupsEntityId, @CareGroupId, @FunctionalUnitId, @SpecialtyId, @ServiceDate, @InitialService, @RiasId, @ContractDescriptionId, @IPSServiceId)
		
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que determina el valor tarifario institucional aplicable a un procedimiento CUPS dentro de un contrato, dado un grupo de atención, unidad funcional, especialidad, fecha de servicio y servicio de IPS. Primero verifica que el código CUPS esté incluido en la plantilla de procedimientos asociada al grupo de atención del contrato (Contract.CareGroup y Contract.ProcedureCups); si el CUPS está cubierto, delega el cálculo de condiciones y tarifas a la función Contract.GetRateValueCondition_Institucional, que evalúa reglas de liquidación, tipo de manual tarifario, valor de venta y recargos. Si el CUPS no está contratado para ese grupo de atención, retorna un resultado negativo con un mensaje explicativo indicando que el procedimiento no está cubierto. Se usa en el proceso de facturación y liquidación de servicios institucionales para validar cobertura tarifaria antes de liquidar una prestación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetRateValue_Institucional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetRateValue_Institucional';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina el valor tarifario institucional de un CUPS para un grupo de atención: si está contratado delega en la función de condiciones; si no, devuelve un resultado negativo informando la no cobertura.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValue_Institucional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El CareGroupId debe existir en Contract.CareGroup para obtener su ProcedureTemplateId, código y nombre.; El CupsEntityId debe existir en Contract.CUPSEntity para poder construir el mensaje cuando no hay cobertura.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValue_Institucional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna al menos una fila en la tabla resultado (sea de cobertura o de no cobertura).; La validación de cobertura se realiza por la combinación plantilla de procedimientos del grupo de atención + CUPS, respetando el ContractDescriptionId.; El mensaje de no cobertura siempre acompaña a StateResult=0 y deja en NULL/0 los valores tarifarios.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValue_Institucional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'CUPS; Grupo de atención; Plantilla de procedimientos; Tarifa institucional; Manual tarifario; Liquidación; Cobertura contractual; Valor de venta; Recargo tarifario', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValue_Institucional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetail: Cuando existe al menos un ProcedureCups con la plantilla del grupo de atención, el CUPS dado y (ContractDescriptionId nulo o igual al recibido), se insertan las filas devueltas por GetRateValueCondition_Institucional.; [INSERT] @DefinitionRateDetail: Cuando no existe ProcedureCups que vincule el CUPS con la plantilla del grupo de atención, se inserta una fila con StateResult=0 y mensaje ''El CUPS <code> - <name> no esta cubierto en el grupo de atencion <code> - <name>''.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValue_Institucional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe al menos un registro en Contract.ProcedureCups para la plantilla del grupo de atención y el CUPS (filtrando por ContractDescriptionId nulo o igual) → Se delega el cálculo a Contract.GetRateValueCondition_Institucional y se devuelven sus resultados. else Se devuelve un único registro con StateResult=0 y mensaje indicando que el CUPS no está cubierto en el grupo de atención.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValue_Institucional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Contract.GetRateValueCondition_Institucional', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValue_Institucional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CareGroup; Contract.ProcedureCups; Contract.CUPSEntity; Contract.GetRateValueCondition_Institucional', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValue_Institucional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValue_Institucional';
GO
