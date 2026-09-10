CREATE Function [Contract].[GetConditionByTimeFunctionalUnit]
(
	@ServiceDate DateTime,
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
		, StartTime Time Null
		, EndTime Time Null
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
		t.x.value('StartTime[1]','Time'),
		t.x.value('EndTime[1]','Time'),
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
		Where Operator = 1 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And FunctionalUnitId2 = @FunctionalUnitId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And FunctionalUnitId2 = @FunctionalUnitId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And FunctionalUnitId2 <> @FunctionalUnitId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And FunctionalUnitId2 <> @FunctionalUnitId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And FunctionalUnitId2 = @FunctionalUnitId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And FunctionalUnitId2 = @FunctionalUnitId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And FunctionalUnitId2 <> @FunctionalUnitId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And FunctionalUnitId2 <> @FunctionalUnitId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a un servicio según la hora de atención y la unidad funcional donde se prestó. Recibe como parámetros la fecha/hora del servicio, el identificador de la unidad funcional y una lista de condiciones tarifarias en formato XML; a partir de esa lista evalúa combinaciones de operadores de comparación horaria (dentro o fuera de un rango de horas) y coincidencia o no coincidencia con la unidad funcional, devolviendo la primera condición que cumple los criterios. El resultado incluye el tipo de liquidación, tipo de manual tarifario, vigencia y código del manual, porcentaje de variación de tarifa, valor de venta y valor de venta con recargo, datos que se usan en la liquidación y facturación de servicios contratados. Es utilizada para aplicar tarifas diferenciales por franja horaria y sede o unidad funcional dentro de los contratos de prestación de servicios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByTimeFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByTimeFunctionalUnit';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona, entre una lista de condiciones tarifarias, la primera que aplica según la franja horaria del servicio y la coincidencia con la unidad funcional, retornando los parámetros de tarifación a usar.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones debe seguir el esquema /RateConditionList con los nodos esperados; Los valores de Operator y Operator2 deben ser 1 o 2 para que alguna rama aplique; Las condiciones con rango horario deben tener StartTime y EndTime definidos para ser evaluadas', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las cuatro combinaciones de Operator/Operator2 son mutuamente excluyentes: solo se evalúa una y se retorna a lo sumo una fila; Operator=1 representa ''dentro del rango horario'' y Operator=2 representa ''fuera del rango horario''; Operator2=1 representa ''misma unidad funcional'' y Operator2=2 representa ''diferente unidad funcional''; La comparación horaria usa solo la parte Time de la fecha de servicio, ignorando la fecha; Se prioriza la coincidencia exacta (dentro de rango + misma unidad) sobre las demás combinaciones; Solo se retorna una condición (TOP 1) aun si múltiples cumplen el criterio', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tarifa; Manual tarifario; Vigencia de manual tarifario; Liquidación; Unidad funcional; Variación de tarifa; Valor de venta; Recargo; Franja horaria', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando hay condiciones con Operator=1 y Operator2=1 cuya franja [StartTime,EndTime] contiene la hora del servicio y cuyo FunctionalUnitId2 coincide con la unidad funcional, se inserta TOP 1 de esas condiciones; [INSERT] @DefinitionRateDetailCondition: Cuando no aplica la primera y existen condiciones con Operator=1 y Operator2=2 dentro del rango horario y con FunctionalUnitId2 distinto a la unidad funcional, se inserta TOP 1; [INSERT] @DefinitionRateDetailCondition: Cuando no aplican las anteriores y existen condiciones con Operator=2 y Operator2=1 fuera del rango (hora < StartTime y hora > EndTime) con misma unidad funcional, se inserta TOP 1; [INSERT] @DefinitionRateDetailCondition: Cuando no aplican las anteriores y existen condiciones con Operator=2 y Operator2=2 fuera del rango con FunctionalUnitId2 distinto a la unidad funcional, se inserta TOP 1; [RETURN_RESULT] @DefinitionRateDetailCondition: Si ninguna combinación de Operator/Operator2 se cumple, retorna la tabla vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe condición con Operator=1 y Operator2=1, hora del servicio dentro de [StartTime,EndTime] y FunctionalUnitId2 igual a la unidad funcional → Devuelve la primera condición que cumple (dentro de rango horario y misma unidad funcional) else Evalúa siguiente combinación; si Existe condición con Operator=1 y Operator2=2, hora del servicio dentro de [StartTime,EndTime] y FunctionalUnitId2 distinto a la unidad funcional → Devuelve la primera condición que cumple (dentro de rango horario y diferente unidad funcional) else Evalúa siguiente combinación; si Existe condición con Operator=2 y Operator2=1, hora del servicio fuera del rango (menor a StartTime y mayor a EndTime) y FunctionalUnitId2 igual a la unidad funcional → Devuelve la primera condición que cumple (fuera de rango horario y misma unidad funcional) else Evalúa siguiente combinación; si Existe condición con Operator=2 y Operator2=2, hora del servicio fuera del rango (menor a StartTime y mayor a EndTime) y FunctionalUnitId2 distinto a la unidad funcional → Devuelve la primera condición que cumple (fuera de rango horario y diferente unidad funcional) else No retorna ninguna fila', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeFunctionalUnit';
GO
