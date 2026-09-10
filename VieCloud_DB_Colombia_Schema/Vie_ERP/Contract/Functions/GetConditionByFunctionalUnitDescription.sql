CREATE Function [Contract].[GetConditionByFunctionalUnitDescription]
(
	@FunctionalUnitId Int,
	@ContractDescriptionId int,
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
		, FunctionalUnitId Int Null
		, UnitTypeId2 Tinyint Null
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
		t.x.value('FunctionalUnitId[1]','Int'),
		t.x.value('UnitTypeId2[1]','Tinyint'),
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
		Where Operator = 1 And Operator2 = 1 And FunctionalUnitId = @FunctionalUnitId And ContractDescriptionId2 = @ContractDescriptionId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And FunctionalUnitId = @FunctionalUnitId And ContractDescriptionId2 = @ContractDescriptionId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And FunctionalUnitId = @FunctionalUnitId And ContractDescriptionId2 <> @ContractDescriptionId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And FunctionalUnitId = @FunctionalUnitId And ContractDescriptionId2 <> @ContractDescriptionId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And FunctionalUnitId <> @FunctionalUnitId And ContractDescriptionId2 = @ContractDescriptionId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And FunctionalUnitId <> @FunctionalUnitId And ContractDescriptionId2 = @ContractDescriptionId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And FunctionalUnitId <> @FunctionalUnitId And ContractDescriptionId2 <> @ContractDescriptionId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And FunctionalUnitId <> @FunctionalUnitId And ContractDescriptionId2 <> @ContractDescriptionId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a un ítem contractual según la unidad funcional y la descripción del contrato (renglón o ítem del contrato). Recibe como entrada un XML con una lista de condiciones tarifarias candidatas, cada una con sus operadores de comparación, y aplica una lógica de prioridad en cascada: primero busca la condición donde tanto la unidad funcional como la descripción del contrato coinciden exactamente, luego condiciones donde coincide solo la unidad funcional, luego solo la descripción del contrato, y finalmente condiciones genéricas donde ninguna coincide exactamente. Retorna la condición ganadora con su tipo de liquidación, tipo de manual tarifario, vigencia del manual, variación de tarifa y valor de venta (con y sin recargo). Se usa en el motor de tarifación y liquidación de contratos con aseguradoras o pagadores para resolver qué tarifa cobra la IPS según el contexto de atención (unidad funcional donde se prestó el servicio).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByFunctionalUnitDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByFunctionalUnitDescription';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona la condición tarifaria aplicable a una unidad funcional y descripción de contrato evaluando combinaciones de operadores de igualdad/desigualdad sobre una lista de condiciones recibida en XML.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones debe seguir la estructura /RateConditionList con los nodos esperados (Id, Operator, Operator2, FunctionalUnitId, ContractDescriptionId2, etc.).; Los Id en el XML deben ser únicos (clave primaria de la tabla temporal).; Operator y Operator2 deben tomar valores 1 (igual) o 2 (distinto) para que alguna rama aplique.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La tabla devuelta contiene como máximo una fila (uso de TOP 1 en cada rama y estructura If/Else If excluyente).; Las ramas se evalúan en orden de prioridad: igualdad/igualdad > igualdad/desigualdad > desigualdad/igualdad > desigualdad/desigualdad.; Operator=1 se interpreta como igualdad y Operator=2 como desigualdad respecto al valor de entrada correspondiente.; Si ninguna rama se cumple, la función devuelve una tabla vacía sin error.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Unidad funcional; Contrato (descripción de contrato); Condición tarifaria; Manual de tarifas; Tipo de liquidación; Variación de tarifa; Valor de venta; Recargo en valor de venta', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Si existe alguna condición con Operator=1 y Operator2=1 cuya FunctionalUnitId coincide con la entrada y ContractDescriptionId2 coincide con la entrada, se inserta la primera (TOP 1) coincidencia.; [INSERT] @DefinitionRateDetailCondition: Si la rama anterior no aplica y existe alguna con Operator=1 y Operator2=2, FunctionalUnitId igual a la entrada y ContractDescriptionId2 distinto a la entrada, se inserta la primera (TOP 1) coincidencia.; [INSERT] @DefinitionRateDetailCondition: Si las ramas anteriores no aplican y existe alguna con Operator=2 y Operator2=1, FunctionalUnitId distinto a la entrada y ContractDescriptionId2 igual a la entrada, se inserta la primera (TOP 1) coincidencia.; [INSERT] @DefinitionRateDetailCondition: Si las ramas anteriores no aplican y existe alguna con Operator=2 y Operator2=2, FunctionalUnitId distinto a la entrada y ContractDescriptionId2 distinto a la entrada, se inserta la primera (TOP 1) coincidencia.; [RETURN_RESULT] @DefinitionRateDetailCondition: Devuelve la tabla con a lo sumo una fila correspondiente a la condición tarifaria seleccionada según la primera rama que se cumpla.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Operator=1 AND Operator2=1 AND FunctionalUnitId = entrada AND ContractDescriptionId2 = entrada (coincidencia exacta en ambos) → Inserta TOP 1 de esa coincidencia y termina la evaluación. else Evalúa la siguiente rama.; si Operator=1 AND Operator2=2 AND FunctionalUnitId = entrada AND ContractDescriptionId2 <> entrada → Inserta TOP 1 de esa coincidencia. else Evalúa la siguiente rama.; si Operator=2 AND Operator2=1 AND FunctionalUnitId <> entrada AND ContractDescriptionId2 = entrada → Inserta TOP 1 de esa coincidencia. else Evalúa la siguiente rama.; si Operator=2 AND Operator2=2 AND FunctionalUnitId <> entrada AND ContractDescriptionId2 <> entrada → Inserta TOP 1 de esa coincidencia. else No inserta nada y retorna tabla vacía.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitDescription';
GO
