CREATE Function [Contract].[GetConditionByDescriptionFunctionalUnit]
(
	@ContractDescriptionId int,	
	@FunctionalUnitId Int,
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
		, SpecialtyId Char(3) Null
		, FunctionalUnitId2 Int Null
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
		t.x.value('SpecialtyId[1]','Char(3)'),
		t.x.value('FunctionalUnitId2[1]','Int'),
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
		Where Operator = 1 And Operator2 = 1 And ContractDescriptionId = @ContractDescriptionId And FunctionalUnitId2 = @FunctionalUnitId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
			Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
			From @rateConditionList
			Where Operator = 1 And Operator2 = 1 And ContractDescriptionId = @ContractDescriptionId And FunctionalUnitId2 = @FunctionalUnitId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And ContractDescriptionId = @ContractDescriptionId And FunctionalUnitId2 <> @FunctionalUnitId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And ContractDescriptionId = @ContractDescriptionId And FunctionalUnitId2 <> @FunctionalUnitId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And ContractDescriptionId <> @ContractDescriptionId And FunctionalUnitId2 = @FunctionalUnitId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And ContractDescriptionId <> @ContractDescriptionId And FunctionalUnitId2 = @FunctionalUnitId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And ContractDescriptionId <> @ContractDescriptionId And FunctionalUnitId2 <> @FunctionalUnitId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And ContractDescriptionId <> @ContractDescriptionId And FunctionalUnitId2 <> @FunctionalUnitId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a un ítem del contrato (descripción de contrato) según la unidad funcional de atención. Recibe un listado de condiciones tarifarias en formato XML y aplica una lógica de prioridad por coincidencia exacta o parcial: primero busca una condición que coincida exactamente con la descripción de contrato Y la unidad funcional; si no existe, aplica reglas de fallback en orden descendente de especificidad (unidad funcional diferente, descripción diferente, o ambas diferentes). Retorna la condición seleccionada con su tipo de liquidación, tipo de manual tarifario, vigencia, variación de tarifa y valor de venta (con y sin recargo), usada en la facturación y liquidación de servicios pactados en contratos con aseguradores o pagadores.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByDescriptionFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByDescriptionFunctionalUnit';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona, a partir de una lista XML de condiciones tarifarias, la más específica que aplique a una combinación de descripción de contrato y unidad funcional, usando un orden de fallback por especificidad.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones debe respetar el esquema /RateConditionList con los nodos esperados (Id, Operator, Operator2, ContractDescriptionId, FunctionalUnitId2, etc.); Los Id dentro del XML deben ser únicos (clave primaria de la tabla intermedia y de la tabla de retorno); Los códigos de Operator y Operator2 deben usar 1 (igual) o 2 (distinto) para que el flujo seleccione alguna rama', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna a lo sumo una condición (TOP 1) según el primer nivel de coincidencia satisfecho; La evaluación de coincidencias sigue un orden estricto de especificidad: exacto > misma descripción > misma unidad funcional > ninguna coincidencia; Operator=1 indica igualdad con la descripción de contrato y Operator=2 indica diferencia; Operator2=1/2 aplica la misma semántica para la unidad funcional; Si ningún nivel de coincidencia tiene registros, la tabla resultado queda vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Contrato; Descripción de contrato; Unidad funcional; Condición tarifaria; Tipo de liquidación; Manual tarifario; Vigencia de manual tarifario; Variación de tarifa; Valor de venta; Recargo', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando existe registro con Operator=1, Operator2=1, ContractDescriptionId igual al parámetro y FunctionalUnitId2 igual al parámetro, inserta TOP 1 de esa coincidencia exacta; [INSERT] @DefinitionRateDetailCondition: Cuando no hay match exacto pero existe registro con Operator=1, Operator2=2, misma descripción y FunctionalUnitId2 distinto, inserta TOP 1 de ese fallback; [INSERT] @DefinitionRateDetailCondition: Cuando los anteriores fallan y existe registro con Operator=2, Operator2=1, descripción distinta pero misma unidad funcional, inserta TOP 1 de ese fallback; [INSERT] @DefinitionRateDetailCondition: Cuando ninguno de los anteriores aplica y existe registro con Operator=2, Operator2=2, descripción y unidad funcional ambas distintas, inserta TOP 1 como fallback más amplio; [RETURN_RESULT] @DefinitionRateDetailCondition: Devuelve la tabla con la condición seleccionada (Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge) o vacía si ningún nivel coincide', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe condición con Operator=1 y Operator2=1, misma descripción de contrato y misma unidad funcional → Retorna esa condición (match exacto) else Evalúa siguiente nivel de fallback; si Existe condición con Operator=1 y Operator2=2, misma descripción de contrato pero unidad funcional distinta → Retorna esa condición else Evalúa siguiente nivel de fallback; si Existe condición con Operator=2 y Operator2=1, descripción de contrato distinta pero misma unidad funcional → Retorna esa condición else Evalúa siguiente nivel de fallback; si Existe condición con Operator=2 y Operator2=2, descripción de contrato y unidad funcional ambas distintas → Retorna esa condición (fallback más laxo) else Retorna tabla vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionFunctionalUnit';
GO
