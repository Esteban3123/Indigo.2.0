CREATE Function [Contract].[GetConditionByUnitTypeDescription]
(
	@UnitType Tinyint,
	@ContractDescriptionId Int,
	@rateConditionListXml Xml
)
Returns @DefinitionRateDetailCondition Table
(
	Id Int Primary Key,
	LiquidationType Tinyint Null,
	ManualType Tinyint Null,
	RateManualValidityId INT NULL,
	RateManualId Int Null,
	RateVariation Decimal(5, 2) Null,
	SalesValue Decimal(18, 2) Null,
	SalesValueWithSurcharge Decimal(18, 2) Null
) 
As
Begin

	Declare @rateConditionList Table(Id Int Primary Key
		, Operator Tinyint
		, Operator2 Tinyint
		, FunctionalUnitId2 Int Null
		, UnitTypeId Tinyint Null
		, LiquidationType Tinyint Null
		, ManualType Tinyint NULL
		, RateManualValidityId INT NULL
		, RateManualId Int Null
		, RateVariation Decimal(5, 2) Null
		, SalesValue Decimal(18, 2) Null
		, SalesValueWithSurcharge Decimal(18, 2) Null
		, ContractDescriptionId2 Int Null)

	Insert Into @rateConditionList
	Select t.x.value('Id[1]','Int'),
		t.x.value('Operator[1]','Tinyint'),
		t.x.value('Operator2[1]','Tinyint'),
		t.x.value('FunctionalUnitId2[1]','Int'),
		t.x.value('UnitTypeId[1]','Tinyint'),
		t.x.value('LiquidationType[1]','Tinyint'),
		t.x.value('ManualType[1]','Tinyint'),
		t.x.value('RateManualValidityId[1]','Int'),
		t.x.value('RateManualId[1]','Int'),
		t.x.value('RateVariation[1]','Decimal(5, 2)'),
		t.x.value('SalesValue[1]','Decimal(18, 2)'),
		t.x.value('SalesValueWithSurcharge[1]','Decimal(18, 2)'),
		t.x.value('ContractDescriptionId2[1]','Int')
	From @rateConditionListXml.nodes('/RateConditionList') t(x)
		
	If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And UnitTypeId = @UnitType And ContractDescriptionId2 = @ContractDescriptionId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And UnitTypeId = @UnitType And ContractDescriptionId2 = @ContractDescriptionId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And UnitTypeId = @UnitType And ContractDescriptionId2 <> @ContractDescriptionId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And UnitTypeId = @UnitType And ContractDescriptionId2 <> @ContractDescriptionId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And UnitTypeId <> @UnitType And ContractDescriptionId2 = @ContractDescriptionId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And UnitTypeId <> @UnitType And ContractDescriptionId2 = @ContractDescriptionId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And UnitTypeId <> @UnitType And ContractDescriptionId2 <> @ContractDescriptionId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And UnitTypeId <> @UnitType And ContractDescriptionId2 <> @ContractDescriptionId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina, a partir de una lista de condiciones tarifarias enviada en formato XML, cuál es la condición de tarifa aplicable según el tipo de unidad funcional y la descripción del contrato indicados. Evalúa las condiciones por prioridad usando operadores de coincidencia (igual o diferente) para el tipo de unidad y la descripción del contrato, y retorna la primera condición que cumple: primero busca coincidencia exacta en ambos criterios, luego coincidencia parcial en distintas combinaciones. Devuelve los parámetros de liquidación de la tarifa encontrada: tipo de liquidación, tipo de manual tarifario, vigencia del manual, identificador del manual, variación de tarifa, valor de venta y valor de venta con recargo. Se usa en la liquidación de contratos para seleccionar automáticamente la regla tarifaria correcta según el contexto de la atención (tipo de unidad y descripción contractual).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByUnitTypeDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByUnitTypeDescription';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona, dentro de un listado XML de condiciones tarifarias, la condición de tarifa más específica aplicable a un tipo de unidad y descripción de contrato dados, según una jerarquía de coincidencia/no coincidencia.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones de tarifa debe respetar la estructura /RateConditionList con los nodos esperados (Id, Operator, Operator2, UnitTypeId, ContractDescriptionId2, etc.).; Los Id presentes en el XML deben ser únicos (clave primaria de la tabla temporal).; Operator y Operator2 deben tomar valores 1 (igual) o 2 (distinto) para que alguna rama aplique.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La tabla resultante contiene a lo sumo un registro (uso de TOP 1 en cada rama mutuamente excluyente).; Las cuatro combinaciones de Operator/Operator2 se evalúan en orden de prioridad estricta: (1,1) > (1,2) > (2,1) > (2,2); la primera que tenga coincidencias gana.; Operator=1 se interpreta como igualdad y Operator=2 como desigualdad respecto al parámetro recibido.; Operator aplica sobre UnitTypeId y Operator2 aplica sobre ContractDescriptionId2.; Si ninguna combinación tiene coincidencias, la función retorna sin filas.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Condición de tarifa contractual; Tipo de unidad funcional; Descripción de contrato; Manual tarifario; Vigencia de manual de tarifas; Variación de tarifa; Valor de venta; Valor de venta con recargo; Tipo de liquidación', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando existe al menos una fila con Operator=1 And Operator2=1 And UnitTypeId=@UnitType And ContractDescriptionId2=@ContractDescriptionId, se inserta el TOP 1 de esa coincidencia (máxima especificidad).; [INSERT] @DefinitionRateDetailCondition: Si no hay match (1,1) pero existe Operator=1 And Operator2=2 And UnitTypeId=@UnitType And ContractDescriptionId2<>@ContractDescriptionId, se inserta el TOP 1 con igual unidad y distinta descripción de contrato.; [INSERT] @DefinitionRateDetailCondition: Si no hay match anterior pero existe Operator=2 And Operator2=1 And UnitTypeId<>@UnitType And ContractDescriptionId2=@ContractDescriptionId, se inserta el TOP 1 con distinta unidad e igual descripción de contrato.; [INSERT] @DefinitionRateDetailCondition: Si no hay match anterior pero existe Operator=2 And Operator2=2 And UnitTypeId<>@UnitType And ContractDescriptionId2<>@ContractDescriptionId, se inserta el TOP 1 (caso más genérico).; [RETURN_RESULT] @DefinitionRateDetailCondition: Devuelve la tabla con cero o un registro que representa la condición tarifaria seleccionada.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe condición con Operator=1 (igual) y Operator2=1 (igual) cuyo UnitTypeId coincide con el tipo de unidad y ContractDescriptionId2 coincide con la descripción de contrato → Devuelve la primera coincidencia exacta (igual tipo unidad e igual descripción de contrato) else Evalúa la siguiente combinación de operadores; si Existe condición con Operator=1 y Operator2=2 (distinto) donde UnitTypeId coincide pero ContractDescriptionId2 es distinto al de contrato → Devuelve la primera coincidencia con igual tipo de unidad y diferente descripción de contrato else Evalúa la siguiente combinación; si Existe condición con Operator=2 (distinto) y Operator2=1 donde UnitTypeId difiere pero ContractDescriptionId2 coincide → Devuelve la primera coincidencia con distinto tipo de unidad e igual descripción de contrato else Evalúa la última combinación; si Existe condición con Operator=2 y Operator2=2 donde UnitTypeId y ContractDescriptionId2 difieren de los recibidos → Devuelve la primera coincidencia con distinto tipo de unidad y distinta descripción de contrato else Retorna tabla vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeDescription';
GO
