CREATE Function [Contract].[GetConditionByUnitTypeSpecialty]
(
	@UnitType Tinyint,
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
		, SpecialtyId2 Char(3) Null
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
		t.x.value('SpecialtyId2[1]','Char(3)'),
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
		Where Operator = 1 And Operator2 = 1 And UnitTypeId = @UnitType And SpecialtyId2 = @SpecialtyId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And UnitTypeId = @UnitType And SpecialtyId2 = @SpecialtyId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And UnitTypeId = @UnitType And SpecialtyId2 <> @SpecialtyId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And UnitTypeId = @UnitType And SpecialtyId2 <> @SpecialtyId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And UnitTypeId <> @UnitType And SpecialtyId2 = @SpecialtyId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And UnitTypeId <> @UnitType And SpecialtyId2 = @SpecialtyId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And UnitTypeId <> @UnitType And SpecialtyId2 <> @SpecialtyId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And UnitTypeId <> @UnitType And SpecialtyId2 <> @SpecialtyId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a un servicio de salud según el tipo de unidad de atención y la especialidad médica. Recibe como entrada un XML con una lista de condiciones tarifarias definidas en el contrato, y evalúa en orden de prioridad cuál condición coincide mejor con el tipo de unidad y la especialidad indicados, usando operadores de igualdad o diferencia para cada criterio. Devuelve la condición ganadora con su tipo de liquidación, tipo de manual tarifario, vigencia del manual, variación de tarifa, valor de venta y valor de venta con recargo. Se usa en el proceso de liquidación y facturación de servicios contratados para seleccionar automáticamente la regla tarifaria correcta según las características del servicio prestado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByUnitTypeSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByUnitTypeSpecialty';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona, según prioridad, una condición tarifaria aplicable a partir de una lista XML evaluando coincidencia o exclusión de tipo de unidad y especialidad.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones debe ajustarse al esquema /RateConditionList con los nodos esperados; Operator y Operator2 deben tomar valores 1 (igual/incluye) o 2 (distinto/excluye) para que las ramas apliquen', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La función evalúa los cuatro escenarios en orden de prioridad y retorna como máximo una condición (Top 1); Operator y Operator2 actúan como inclusión (=1) o exclusión (=2) sobre tipo de unidad y especialidad respectivamente; El match exacto unidad+especialidad tiene mayor prioridad que cualquier escenario con exclusión; La exclusión total (unidad y especialidad distintas) es la de menor prioridad; Si ningún escenario tiene coincidencias, retorna tabla vacía sin error', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tipo de unidad; Especialidad; Condición de tarifa; Manual tarifario; Vigencia de manual tarifario; Tipo de liquidación; Variación tarifaria; Valor de venta; Valor de venta con recargo', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @DefinitionRateDetailCondition: Cuando existe condición con Operator=1 y Operator2=1 con UnitTypeId=@UnitType y SpecialtyId2=@SpecialtyId, retorna Top 1 de ese subconjunto; [RETURN_RESULT] @DefinitionRateDetailCondition: Cuando no aplica el caso anterior y existe Operator=1, Operator2=2 con UnitTypeId=@UnitType y SpecialtyId2<>@SpecialtyId, retorna Top 1 de ese subconjunto; [RETURN_RESULT] @DefinitionRateDetailCondition: Cuando no aplican los anteriores y existe Operator=2, Operator2=1 con UnitTypeId<>@UnitType y SpecialtyId2=@SpecialtyId, retorna Top 1 de ese subconjunto; [RETURN_RESULT] @DefinitionRateDetailCondition: Cuando no aplican los anteriores y existe Operator=2, Operator2=2 con UnitTypeId<>@UnitType y SpecialtyId2<>@SpecialtyId, retorna Top 1 de ese subconjunto', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe condición con Operator=1 y Operator2=1 cuyo UnitTypeId coincide con el tipo de unidad y SpecialtyId2 coincide con la especialidad → Devuelve esa condición (Top 1) como match exacto de unidad y especialidad else Evalúa el siguiente caso; si Existe condición con Operator=1 y Operator2=2 cuyo UnitTypeId coincide con la unidad y SpecialtyId2 es distinto a la especialidad → Devuelve esa condición (Top 1): match de unidad y exclusión de especialidad else Evalúa el siguiente caso; si Existe condición con Operator=2 y Operator2=1 cuyo UnitTypeId es distinto a la unidad y SpecialtyId2 coincide con la especialidad → Devuelve esa condición (Top 1): exclusión de unidad y match de especialidad else Evalúa el siguiente caso; si Existe condición con Operator=2 y Operator2=2 cuyo UnitTypeId es distinto a la unidad y SpecialtyId2 distinto a la especialidad → Devuelve esa condición (Top 1): exclusión de unidad y exclusión de especialidad else Devuelve tabla vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeSpecialty';
GO
