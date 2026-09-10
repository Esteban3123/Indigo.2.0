CREATE Function [Contract].[GetConditionBySpecialtyUnitType]
(
	@SpecialtyId Char(3),
	@UnitType Tinyint,
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
		, UnitTypeId2 Tinyint Null
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
		t.x.value('UnitTypeId2[1]','Tinyint'),
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
		Where Operator = 1 And Operator2 = 1 And SpecialtyId = @SpecialtyId And UnitTypeId2 = @UnitType) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And SpecialtyId = @SpecialtyId And UnitTypeId2 = @UnitType

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And SpecialtyId = @SpecialtyId And UnitTypeId2 <> @UnitType) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And SpecialtyId = @SpecialtyId And UnitTypeId2 <> @UnitType

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And SpecialtyId <> @SpecialtyId And UnitTypeId2 = @UnitType) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And SpecialtyId <> @SpecialtyId And UnitTypeId2 = @UnitType

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And SpecialtyId <> @SpecialtyId And UnitTypeId2 <> @UnitType) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And SpecialtyId <> @SpecialtyId And UnitTypeId2 <> @UnitType

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a un servicio según la especialidad médica y el tipo de unidad de atención. Recibe un listado de condiciones tarifarias en formato XML (con sus operadores de comparación, especialidad y tipo de unidad), lo parsea y aplica una lógica de prioridad en cascada: primero busca coincidencia exacta en ambos criterios (especialidad igual Y tipo de unidad igual), luego combinaciones parciales (especialidad igual con tipo de unidad distinto, o viceversa), y como último recurso condiciones donde ambos difieren. Retorna la condición ganadora con su tipo de liquidación, tipo de manual tarifario, vigencia del manual, variación de tarifa y valores de venta (con y sin recargo). Es usada en la parametrización y liquidación de contratos para resolver qué tarifa aplicar a un procedimiento o servicio según las reglas pactadas con el pagador.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionBySpecialtyUnitType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionBySpecialtyUnitType';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona la condición tarifaria aplicable para una especialidad y tipo de unidad evaluando combinaciones de operadores de igualdad/desigualdad en orden de prioridad.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe respetar el esquema /RateConditionList con los nodos esperados (Id, Operator, Operator2, SpecialtyId, UnitTypeId2, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge).; Los Id dentro del XML deben ser únicos (clave primaria de la tabla temporal).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La tabla resultado contiene a lo sumo una fila (uso de TOP 1 y ramas mutuamente excluyentes).; La prioridad de selección es estricta: igual-igual > igual-distinto > distinto-igual > distinto-distinto.; Operator=1 se interpreta como igualdad y Operator=2 como desigualdad respecto a especialidad; Operator2 aplica el mismo criterio sobre el tipo de unidad.; Si ninguna combinación se cumple, no se inserta ninguna fila.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Especialidad; Tipo de unidad; Condición de tarifa; Tipo de liquidación; Manual tarifario; Vigencia de manual tarifario; Variación de tarifa; Valor de venta; Valor de venta con recargo', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando existe condición con Operator=1 y Operator2=1, SpecialtyId igual al parámetro y UnitTypeId2 igual al parámetro, se inserta TOP 1 de ese conjunto (match exacto especialidad y unidad).; [INSERT] @DefinitionRateDetailCondition: Si no aplica el caso anterior y existe condición con Operator=1, Operator2=2, SpecialtyId igual y UnitTypeId2 distinto al parámetro, se inserta TOP 1 (match especialidad, distinta unidad).; [INSERT] @DefinitionRateDetailCondition: Si no aplican los anteriores y existe condición con Operator=2, Operator2=1, SpecialtyId distinto y UnitTypeId2 igual al parámetro, se inserta TOP 1 (distinta especialidad, match unidad).; [INSERT] @DefinitionRateDetailCondition: Si no aplican los anteriores y existe condición con Operator=2, Operator2=2, SpecialtyId distinto y UnitTypeId2 distinto al parámetro, se inserta TOP 1 (distinta especialidad y distinta unidad).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Operator=1 AND Operator2=1 AND SpecialtyId=@SpecialtyId AND UnitTypeId2=@UnitType existe → Devuelve la primera condición con coincidencia exacta de especialidad y unidad else Evalúa siguiente combinación; si Operator=1 AND Operator2=2 AND SpecialtyId=@SpecialtyId AND UnitTypeId2<>@UnitType existe → Devuelve la primera condición con especialidad coincidente y unidad distinta else Evalúa siguiente combinación; si Operator=2 AND Operator2=1 AND SpecialtyId<>@SpecialtyId AND UnitTypeId2=@UnitType existe → Devuelve la primera condición con especialidad distinta y unidad coincidente else Evalúa siguiente combinación; si Operator=2 AND Operator2=2 AND SpecialtyId<>@SpecialtyId AND UnitTypeId2<>@UnitType existe → Devuelve la primera condición con especialidad y unidad ambas distintas else Devuelve tabla vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyUnitType';
GO
