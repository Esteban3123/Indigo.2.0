CREATE Function [Contract].[GetConditionByFunctionalUnitTime]
(
	@FunctionalUnitId Int,
	@ServiceDate DateTime,
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
		, StartTime2 Time Null
		, EndTime2 Time Null
		, FunctionalUnitId Int Null
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
		t.x.value('StartTime2[1]','Time'),
		t.x.value('EndTime2[1]','Time'),
		t.x.value('FunctionalUnitId[1]','Int'),
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
		Where Operator = 1 And Operator2 = 1 And FunctionalUnitId = @FunctionalUnitId And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And FunctionalUnitId = @FunctionalUnitId And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And FunctionalUnitId = @FunctionalUnitId And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And FunctionalUnitId = @FunctionalUnitId And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And FunctionalUnitId <> @FunctionalUnitId And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And FunctionalUnitId <> @FunctionalUnitId And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And FunctionalUnitId <> @FunctionalUnitId And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And FunctionalUnitId <> @FunctionalUnitId And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a un servicio según la unidad funcional donde se presta y la hora en que ocurre. Recibe como entrada la unidad funcional, la fecha y hora del servicio, y una lista de condiciones tarifarias en formato XML; evalúa cuál condición cumple los criterios de franja horaria (entre o fuera de un rango de horas) y de coincidencia o exclusión de la unidad funcional. Retorna la condición tarifaria vigente que corresponde, incluyendo el tipo de liquidación, tipo de manual tarifario, variación porcentual de la tarifa, valor de venta y valor de venta con recargo, usada para calcular el precio de un servicio en facturación o contratos con condiciones diferenciadas por horario o sede.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByFunctionalUnitTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByFunctionalUnitTime';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona la condición tarifaria aplicable según la unidad funcional y la hora del servicio, evaluando reglas de inclusión/exclusión de unidad y de franja horaria dentro o fuera de rango.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones debe respetar el esquema /RateConditionList con los nodos esperados (Id, Operator, Operator2, StartTime2, EndTime2, FunctionalUnitId, etc.).; Operator y Operator2 deben tomar valores 1 o 2 para que alguna rama aplique.; ServiceDate debe ser convertible a Time para comparar con StartTime2/EndTime2.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'A lo sumo una fila se retorna (TOP 1 en la rama que aplica).; Las cuatro combinaciones de Operator/Operator2 son mutuamente excluyentes y se evalúan en orden de prioridad: (1,1) → (1,2) → (2,1) → (2,2).; Operator=1 implica filtrar por la unidad funcional dada; Operator=2 implica filtrar por unidades distintas.; Operator2=1 implica franja horaria inclusiva (dentro del rango); Operator2=2 implica franja horaria fuera del rango.; Si ninguna combinación se cumple, la tabla resultado se devuelve vacía.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Condición tarifaria; Unidad funcional; Franja horaria de servicio; Tipo de liquidación; Manual tarifario; Variación de tarifa; Valor de venta; Valor de venta con recargo', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando Operator=1 y Operator2=1 y FunctionalUnitId coincide con el parámetro y la hora del servicio está entre StartTime2 y EndTime2, inserta TOP 1 esa condición.; [INSERT] @DefinitionRateDetailCondition: Cuando Operator=1 y Operator2=2 y FunctionalUnitId coincide con el parámetro y la hora del servicio es menor que StartTime2 y mayor que EndTime2 (fuera de rango), inserta TOP 1 esa condición.; [INSERT] @DefinitionRateDetailCondition: Cuando Operator=2 y Operator2=1 y FunctionalUnitId es distinto del parámetro y la hora del servicio está entre StartTime2 y EndTime2, inserta TOP 1 esa condición.; [INSERT] @DefinitionRateDetailCondition: Cuando Operator=2 y Operator2=2 y FunctionalUnitId es distinto del parámetro y la hora del servicio es menor que StartTime2 y mayor que EndTime2, inserta TOP 1 esa condición.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Operator=1 (incluir unidad) y Operator2=1 (dentro de franja) → Devuelve la primera condición cuya unidad coincida y cuya hora esté dentro del rango. else Evalúa la siguiente combinación.; si Operator=1 y Operator2=2 (fuera de franja) → Devuelve la primera condición de unidad coincidente cuya hora esté fuera del rango. else Evalúa la siguiente combinación.; si Operator=2 (excluir unidad) y Operator2=1 → Devuelve la primera condición de unidad distinta cuya hora esté dentro del rango. else Evalúa la siguiente combinación.; si Operator=2 y Operator2=2 → Devuelve la primera condición de unidad distinta cuya hora esté fuera del rango. else No retorna ninguna fila.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitTime';
GO
