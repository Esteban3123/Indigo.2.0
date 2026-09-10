CREATE Function [Contract].[GetConditionByDescriptionSpecialty]
(
	@ContractDescriptionId Int,
	@SpecialtyId Char(3),
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
		, FunctionalUnitId Int
		, SpecialtyId2 Char(3) Null
		, LiquidationType Tinyint Null
		, ManualType Tinyint NULL
		, RateManualValidityId INT NULL
		, RateManualId Int Null
		, RateVariation Decimal(5, 2) Null
		, SalesValue Decimal(18, 2) Null
		, SalesValueWithSurcharge Decimal(18, 2) Null
		, ContractDescriptionId Int Null)

	Insert Into @rateConditionList
	Select t.x.value('Id[1]','Int'),
		t.x.value('Operator[1]','Tinyint'),
		t.x.value('Operator2[1]','Tinyint'),
		t.x.value('FunctionalUnitId[1]','Int'),
		t.x.value('SpecialtyId2[1]','Char(3)'),
		t.x.value('LiquidationType[1]','Tinyint'),
		t.x.value('ManualType[1]','Tinyint'),
		t.x.value('RateManualValidityId[1]','Int'),
		t.x.value('RateManualId[1]','Int'),
		t.x.value('RateVariation[1]','Decimal(5, 2)'),
		t.x.value('SalesValue[1]','Decimal(18, 2)'),
		t.x.value('SalesValueWithSurcharge[1]','Decimal(18, 2)'),
		t.x.value('ContractDescriptionId[1]','Int')
	From @rateConditionListXml.nodes('/RateConditionList') t(x)
		
	If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And ContractDescriptionId = @ContractDescriptionId And SpecialtyId2 = @SpecialtyId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And ContractDescriptionId = @ContractDescriptionId And SpecialtyId2 = @SpecialtyId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And ContractDescriptionId = @ContractDescriptionId And SpecialtyId2 <> @SpecialtyId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And ContractDescriptionId = @ContractDescriptionId And SpecialtyId2 <> @SpecialtyId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And ContractDescriptionId <> @ContractDescriptionId And SpecialtyId2 = @SpecialtyId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And ContractDescriptionId <> @ContractDescriptionId And SpecialtyId2 = @SpecialtyId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And ContractDescriptionId <> @ContractDescriptionId And SpecialtyId2 <> @SpecialtyId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And ContractDescriptionId <> @ContractDescriptionId And SpecialtyId2 <> @SpecialtyId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a una descripción de contrato y especialidad médica específicas. Recibe como entrada un XML con una lista de condiciones tarifarias (condiciones de liquidación, tipo de manual, variación de tarifa, valor de venta con y sin recargo) y evalúa por prioridad cuál condición coincide: primero busca coincidencia exacta en descripción de contrato Y especialidad, luego coincidencia por descripción pero con especialidad diferente, luego por especialidad exacta con descripción diferente, y finalmente ninguna coincidencia exacta en ambas. Retorna una tabla con la condición tarifaria ganadora que incluye el tipo de liquidación, manual de tarifas, vigencia del manual, variación porcentual y valor comercial a aplicar. Se usa en el módulo de contratos y facturación para resolver qué tarifa corresponde a un servicio según la especialidad del médico tratante y la descripción del ítem contratado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByDescriptionSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByDescriptionSpecialty';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona, desde una lista XML de condiciones tarifarias, la condición aplicable a una combinación contrato-especialidad evaluando en orden de prioridad coincidencia exacta, parcial o exclusión.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe seguir la estructura /RateConditionList con los nodos esperados (Id, Operator, Operator2, ContractDescriptionId, SpecialtyId2, etc.).; Los operadores deben usar la convención: 1 = igualdad, 2 = desigualdad.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La tabla resultante contiene a lo sumo una fila (TOP 1 en cada rama).; Las cuatro ramas son mutuamente excluyentes: solo se evalúa la siguiente si la anterior no produjo coincidencias.; Operator=1 representa igualdad y Operator=2 representa desigualdad respecto al contrato; Operator2 sigue la misma convención respecto a la especialidad.; La precedencia de selección es: (contrato=, especialidad=) > (contrato=, especialidad≠) > (contrato≠, especialidad=) > (contrato≠, especialidad≠).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Contrato; Descripción de contrato; Especialidad; Condición tarifaria; Manual tarifario; Vigencia de manual tarifario; Tipo de liquidación; Variación tarifaria; Valor de venta; Valor de venta con recargo', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando existe condición con Operator=1 y Operator2=1, ContractDescriptionId igual al parámetro y SpecialtyId2 igual a la especialidad, se inserta TOP 1 de esa coincidencia (máxima prioridad).; [INSERT] @DefinitionRateDetailCondition: Si no hubo match anterior y existe Operator=1 y Operator2=2 con ContractDescriptionId igual y SpecialtyId2 distinto, se inserta TOP 1 de esa coincidencia.; [INSERT] @DefinitionRateDetailCondition: Si no hubo match anterior y existe Operator=2 y Operator2=1 con ContractDescriptionId distinto y SpecialtyId2 igual, se inserta TOP 1 de esa coincidencia.; [INSERT] @DefinitionRateDetailCondition: Si no hubo match anterior y existe Operator=2 y Operator2=2 con ContractDescriptionId distinto y SpecialtyId2 distinto, se inserta TOP 1 de esa coincidencia (mínima prioridad).; [RETURN_RESULT] @DefinitionRateDetailCondition: Devuelve la tabla con como máximo una fila correspondiente al primer nivel de prioridad cumplido; vacía si ningún criterio aplica.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Operator=1 AND Operator2=1 AND ContractDescriptionId = @ContractDescriptionId AND SpecialtyId2 = @SpecialtyId → Selecciona la condición con coincidencia exacta de contrato y especialidad. else Evalúa siguiente nivel de prioridad.; si Operator=1 AND Operator2=2 AND ContractDescriptionId = @ContractDescriptionId AND SpecialtyId2 <> @SpecialtyId → Selecciona condición que aplica al contrato pero excluye la especialidad indicada. else Evalúa siguiente nivel.; si Operator=2 AND Operator2=1 AND ContractDescriptionId <> @ContractDescriptionId AND SpecialtyId2 = @SpecialtyId → Selecciona condición que excluye el contrato pero aplica a la especialidad. else Evalúa siguiente nivel.; si Operator=2 AND Operator2=2 AND ContractDescriptionId <> @ContractDescriptionId AND SpecialtyId2 <> @SpecialtyId → Selecciona condición que excluye tanto contrato como especialidad (caso por defecto/genérico). else No se inserta nada y la tabla queda vacía.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionSpecialty';
GO
