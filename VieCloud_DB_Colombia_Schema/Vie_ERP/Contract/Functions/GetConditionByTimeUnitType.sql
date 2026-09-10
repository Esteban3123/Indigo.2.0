CREATE Function [Contract].[GetConditionByTimeUnitType]
(
	@ServiceDate DateTime,
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
		, StartTime Time Null
		, EndTime Time Null
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
		t.x.value('StartTime[1]','Time'),
		t.x.value('EndTime[1]','Time'),
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
		Where Operator = 1 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And UnitTypeId2 = @UnitType) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And UnitTypeId2 = @UnitType

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And UnitTypeId2 <> @UnitType) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And UnitTypeId2 <> @UnitType

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And UnitTypeId2 = @UnitType) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And UnitTypeId2 = @UnitType

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And UnitTypeId2 <> @UnitType) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And UnitTypeId2 <> @UnitType

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina qué condición tarifaria aplica a un servicio según la hora en que se prestó y el tipo de unidad de tiempo (por ejemplo, diurno, nocturno, fin de semana). Recibe como entrada la fecha y hora del servicio, el tipo de unidad tarifaria y una lista de condiciones de tarifa en formato XML, que parsea internamente para evaluar combinaciones de operadores de rango horario e igualdad/diferencia de tipo de unidad. Retorna la condición tarifaria que cumple los criterios, incluyendo el tipo de liquidación, tipo de manual, vigencia y manual de tarifas, porcentaje de variación, valor de venta y valor de venta con recargo. Se usa en la liquidación de contratos para aplicar la tarifa correcta según la franja horaria y modalidad del servicio prestado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByTimeUnitType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByTimeUnitType';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona, entre una lista de condiciones tarifarias en XML, la primera que aplique a la hora del servicio y al tipo de unidad según los operadores de rango horario y de coincidencia de tipo de unidad.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones debe ajustarse al esquema /RateConditionList con los nodos esperados (Id, Operator, Operator2, StartTime, EndTime, UnitTypeId2, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge); La fecha de servicio debe ser convertible a Time; Operator y Operator2 deben tomar valores válidos (1 o 2) para que alguna rama aplique', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Devuelve a lo sumo una condición tarifaria (TOP 1) por invocación; La evaluación de ramas es excluyente y prioriza el operador de rango inclusivo (Operator=1) sobre el excluyente (Operator=2), y dentro de cada uno prioriza coincidencia de tipo de unidad (Operator2=1) sobre la no coincidencia (Operator2=2); Operator=1 representa rango horario inclusivo (entre StartTime y EndTime); Operator=2 representa rango horario fuera de [StartTime,EndTime]; Operator2=1 exige igualdad de tipo de unidad; Operator2=2 exige diferencia de tipo de unidad; Solo se considera la parte horaria de la fecha de servicio (CAST a Time) para evaluar el rango', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tarifa; Condición tarifaria; Manual tarifario; Tipo de liquidación; Variación de tarifa; Valor de venta; Recargo; Vigencia de manual tarifario', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @DefinitionRateDetailCondition: Cuando hay coincidencia con Operator=1 y Operator2=1 (hora dentro del rango y mismo tipo de unidad), retorna TOP 1 de esa condición; [RETURN_RESULT] @DefinitionRateDetailCondition: Cuando no aplica la rama anterior y hay coincidencia con Operator=1 y Operator2=2 (hora dentro del rango y distinto tipo de unidad), retorna TOP 1 de esa condición; [RETURN_RESULT] @DefinitionRateDetailCondition: Cuando no aplican las ramas anteriores y hay coincidencia con Operator=2 y Operator2=1 (hora fuera del rango y mismo tipo de unidad), retorna TOP 1 de esa condición; [RETURN_RESULT] @DefinitionRateDetailCondition: Cuando no aplican las ramas anteriores y hay coincidencia con Operator=2 y Operator2=2 (hora fuera del rango y distinto tipo de unidad), retorna TOP 1 de esa condición', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe condición con Operator=1 y Operator2=1, hora del servicio dentro de [StartTime,EndTime] y UnitTypeId2 igual al tipo de unidad recibido → Devuelve la primera condición que cumpla (rango horario inclusivo, mismo tipo de unidad) else Evalúa siguiente rama; si Existe condición con Operator=1 y Operator2=2, hora dentro de [StartTime,EndTime] y UnitTypeId2 distinto al tipo de unidad recibido → Devuelve la primera condición que cumpla (rango horario inclusivo, distinto tipo de unidad) else Evalúa siguiente rama; si Existe condición con Operator=2 y Operator2=1, hora menor a StartTime y mayor a EndTime y UnitTypeId2 igual al tipo de unidad → Devuelve la primera condición que cumpla (rango horario excluyente, mismo tipo de unidad) else Evalúa siguiente rama; si Existe condición con Operator=2 y Operator2=2, hora menor a StartTime y mayor a EndTime y UnitTypeId2 distinto al tipo de unidad → Devuelve la primera condición que cumpla (rango horario excluyente, distinto tipo de unidad) else Retorna tabla vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeUnitType';
GO
