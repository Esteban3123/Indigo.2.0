CREATE Function [Contract].[GetConditionByUnitTypeTime]
(
	@UnitType Tinyint,
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
		, StartTime2 Time
		, EndTime2 Time
		, UnitTypeId Tinyint Null
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
		t.x.value('UnitTypeId[1]','Tinyint'),
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
		Where Operator = 1 And Operator2 = 1 And UnitTypeId = @UnitType And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And UnitTypeId = @UnitType And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And UnitTypeId = @UnitType And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And UnitTypeId = @UnitType And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And UnitTypeId <> @UnitType And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And UnitTypeId <> @UnitType And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And UnitTypeId <> @UnitType And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And UnitTypeId <> @UnitType And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que evalúa y selecciona la condición tarifaria aplicable a un servicio según el tipo de unidad funcional (por ejemplo, urgencias, hospitalización, consulta) y la hora en que se prestó el servicio. Recibe como entrada un XML con una lista de condiciones tarifarias del manual de tarifas, las parsea y filtra aplicando operadores de comparación (igual / diferente al tipo de unidad, dentro / fuera de un rango horario) para devolver la condición que corresponde. Retorna datos clave de tarificación como el tipo de liquidación, tipo de manual, variación de tarifa, valor de venta y valor de venta con recargo, usados en la liquidación y facturación de servicios contratados.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByUnitTypeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByUnitTypeTime';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona la condición tarifaria aplicable evaluando, en orden de prioridad, combinaciones de operadores sobre tipo de unidad y rango horario respecto a la fecha/hora del servicio.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones debe seguir la estructura /RateConditionList con los nodos esperados (Id, Operator, Operator2, StartTime2, EndTime2, UnitTypeId, etc.); Cada condición debe traer Operator y Operator2 con valores 1 o 2 para ser considerada; StartTime2 y EndTime2 deben estar definidos para evaluar el rango horario', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado contiene como máximo una fila (TOP 1 en cada rama y ramas mutuamente excluyentes por orden IF/ELSE); Operator=1 representa coincidencia (igual) y Operator=2 representa exclusión (distinto) sobre el tipo de unidad; Operator2=1 representa horario dentro del rango y Operator2=2 representa horario fuera del rango; La prioridad de evaluación es: (1,1) > (1,2) > (2,1) > (2,2); Solo se compara la parte de tiempo de @ServiceDate, no la fecha', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Condición tarifaria; Tipo de unidad; Vigencia de manual tarifario; Tipo de liquidación; Variación de tarifa; Valor de venta; Valor de venta con recargo; Rango horario de servicio', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando existe condición con Operator=1 y Operator2=1, UnitTypeId igual al tipo de unidad recibido y la hora del servicio está dentro de [StartTime2, EndTime2], inserta la primera coincidencia (TOP 1); [INSERT] @DefinitionRateDetailCondition: Si no aplicó la anterior y existe condición con Operator=1 y Operator2=2, UnitTypeId igual y la hora del servicio está fuera del rango (hora < StartTime2 AND hora > EndTime2), inserta TOP 1; [INSERT] @DefinitionRateDetailCondition: Si no aplicaron las anteriores y existe condición con Operator=2 y Operator2=1, UnitTypeId distinto al recibido y la hora del servicio está dentro de [StartTime2, EndTime2], inserta TOP 1; [INSERT] @DefinitionRateDetailCondition: Si no aplicaron las anteriores y existe condición con Operator=2 y Operator2=2, UnitTypeId distinto al recibido y la hora del servicio está fuera del rango (hora < StartTime2 AND hora > EndTime2), inserta TOP 1; [RETURN_RESULT] @DefinitionRateDetailCondition: Retorna la tabla con cero o una fila correspondiente a la primera rama satisfecha', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Operator=1 AND Operator2=1 AND UnitTypeId = @UnitType AND hora del servicio dentro de [StartTime2, EndTime2] → Selecciona TOP 1 de esas condiciones e inserta en el resultado else Evalúa la siguiente rama; si Operator=1 AND Operator2=2 AND UnitTypeId = @UnitType AND hora del servicio < StartTime2 AND hora > EndTime2 → Selecciona TOP 1 e inserta en el resultado else Evalúa la siguiente rama; si Operator=2 AND Operator2=1 AND UnitTypeId <> @UnitType AND hora del servicio dentro de [StartTime2, EndTime2] → Selecciona TOP 1 e inserta en el resultado else Evalúa la siguiente rama; si Operator=2 AND Operator2=2 AND UnitTypeId <> @UnitType AND hora del servicio < StartTime2 AND hora > EndTime2 → Selecciona TOP 1 e inserta en el resultado else Retorna tabla vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeTime';
GO
