CREATE Function [Contract].[GetConditionBySpecialtyFunctionalUnit]
(
	@SpecialtyId Char(3),	
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
		, SalesValueWithSurcharge Decimal(18, 2) Null)

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
		t.x.value('SalesValueWithSurcharge[1]','Decimal(18, 2)')
	From @rateConditionListXml.nodes('/RateConditionList') t(x)
		
	If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And SpecialtyId = @SpecialtyId And FunctionalUnitId2 = @FunctionalUnitId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And SpecialtyId = @SpecialtyId And FunctionalUnitId2 = @FunctionalUnitId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And SpecialtyId = @SpecialtyId And FunctionalUnitId2 <> @FunctionalUnitId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And SpecialtyId = @SpecialtyId And FunctionalUnitId2 <> @FunctionalUnitId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And SpecialtyId <> @SpecialtyId And FunctionalUnitId2 = @FunctionalUnitId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And SpecialtyId <> @SpecialtyId And FunctionalUnitId2 = @FunctionalUnitId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And SpecialtyId <> @SpecialtyId And FunctionalUnitId2 <> @FunctionalUnitId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And SpecialtyId <> @SpecialtyId And FunctionalUnitId2 <> @FunctionalUnitId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a un servicio según la especialidad médica y la unidad funcional indicadas. Recibe como parámetros el código de especialidad, el identificador de unidad funcional y una lista de condiciones tarifarias en formato XML, y aplica una lógica de prioridad en cascada: primero busca una condición que coincida exactamente con ambos criterios (especialidad igual Y unidad funcional igual), luego combinaciones parciales (especialidad igual con cualquier unidad funcional, o cualquier especialidad con unidad funcional exacta, o cualquier combinación). Retorna la condición tarifaria ganadora con sus atributos de liquidación: tipo de liquidación, tipo de manual tarifario, vigencia del manual, variación de tarifa, valor de venta y valor de venta con recargo. Se usa en la facturación y liquidación de contratos para resolver qué tarifa o manual aplica a un prestador según su especialidad y el lugar de atención.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionBySpecialtyFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionBySpecialtyFunctionalUnit';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona, desde una lista de condiciones tarifarias en XML, la condición aplicable según la coincidencia (igualdad/desigualdad) entre la especialidad y la unidad funcional dadas y las definidas en cada condición.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones debe seguir la estructura /RateConditionList con los nodos esperados (Id, Operator, Operator2, SpecialtyId, FunctionalUnitId2, etc.).; Los operadores Operator y Operator2 deben usar los valores 1 (igual) o 2 (distinto) para que se evalúe alguna rama.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelve, como máximo, una fila (TOP 1) en la tabla resultado.; Las ramas se evalúan en orden de prioridad: coincidencia exacta > coincidencia parcial por especialidad > coincidencia parcial por unidad funcional > sin coincidencia.; Operator=1 representa igualdad y Operator=2 representa desigualdad respecto a los valores de entrada.; Si ninguna combinación de operadores se cumple, el resultado queda vacío.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Condición tarifaria; Especialidad; Unidad funcional; Manual de tarifas; Vigencia de manual de tarifas; Tipo de liquidación; Variación de tarifa; Valor de venta; Valor de venta con recargo', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando existe una condición con Operator=1 y Operator2=1 cuyo SpecialtyId coincide con el parámetro y FunctionalUnitId2 coincide con el parámetro, se inserta el TOP 1 de esa condición.; [INSERT] @DefinitionRateDetailCondition: En caso contrario, si existe una condición con Operator=1 y Operator2=2 con SpecialtyId igual al parámetro y FunctionalUnitId2 distinto del parámetro, se inserta el TOP 1 de esa condición.; [INSERT] @DefinitionRateDetailCondition: En caso contrario, si existe una condición con Operator=2 y Operator2=1 con SpecialtyId distinto del parámetro y FunctionalUnitId2 igual al parámetro, se inserta el TOP 1 de esa condición.; [INSERT] @DefinitionRateDetailCondition: En caso contrario, si existe una condición con Operator=2 y Operator2=2 con SpecialtyId distinto del parámetro y FunctionalUnitId2 distinto del parámetro, se inserta el TOP 1 de esa condición.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Operator=1 AND Operator2=1 AND SpecialtyId = @SpecialtyId AND FunctionalUnitId2 = @FunctionalUnitId → Selecciona la condición de coincidencia exacta de especialidad y unidad funcional. else Evalúa la siguiente combinación de operadores.; si Operator=1 AND Operator2=2 AND SpecialtyId = @SpecialtyId AND FunctionalUnitId2 <> @FunctionalUnitId → Selecciona condición que coincide en especialidad pero difiere en unidad funcional. else Evalúa la siguiente combinación.; si Operator=2 AND Operator2=1 AND SpecialtyId <> @SpecialtyId AND FunctionalUnitId2 = @FunctionalUnitId → Selecciona condición que difiere en especialidad pero coincide en unidad funcional. else Evalúa la siguiente combinación.; si Operator=2 AND Operator2=2 AND SpecialtyId <> @SpecialtyId AND FunctionalUnitId2 <> @FunctionalUnitId → Selecciona condición que difiere tanto en especialidad como en unidad funcional. else No inserta nada y retorna tabla vacía.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyFunctionalUnit';
GO
