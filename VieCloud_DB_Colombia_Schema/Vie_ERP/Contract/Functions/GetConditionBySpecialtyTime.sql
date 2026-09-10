CREATE Function [Contract].[GetConditionBySpecialtyTime]
(
	@SpecialtyId Char(3),
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
		, SpecialtyId Char(3) Null
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
		t.x.value('SpecialtyId[1]','Char(3)'),
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
		Where Operator = 1 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2 And SpecialtyId = @SpecialtyId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2 And SpecialtyId = @SpecialtyId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2 And SpecialtyId = @SpecialtyId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2 And SpecialtyId = @SpecialtyId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2 And SpecialtyId <> @SpecialtyId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2 And SpecialtyId <> @SpecialtyId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2 And SpecialtyId <> @SpecialtyId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2 And SpecialtyId <> @SpecialtyId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina qué condición tarifaria aplica a un servicio según la especialidad médica y la hora en que se prestó. Recibe como entrada un XML con una lista de condiciones de tarifa (cada una con rangos horarios, operadores de comparación y especialidad), la fecha/hora del servicio y el código de especialidad; luego evalúa combinaciones de operadores (igual/distinto a la especialidad, dentro/fuera del rango horario) en orden de prioridad para devolver la primera condición tarifaria que coincide. El resultado incluye el tipo de liquidación, tipo de manual tarifario, variación de tarifa, valor de venta y valor de venta con recargo, y se usa en la liquidación de contratos para aplicar tarifas diferenciales según especialidad y franja horaria de atención.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionBySpecialtyTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionBySpecialtyTime';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona la primera condición tarifaria aplicable según especialidad y franja horaria del servicio, evaluando combinaciones de operadores de igualdad/desigualdad y de inclusión/exclusión horaria.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones debe seguir la estructura /RateConditionList con los elementos esperados (Id, Operator, Operator2, StartTime2, EndTime2, SpecialtyId, etc.); Operator y Operator2 deben tomar valores 1 o 2 para que alguna rama coincida; Se requiere SpecialtyId y ServiceDate para evaluar las condiciones', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La tabla resultado contiene a lo sumo un registro (uso de TOP 1 y ramas mutuamente excluyentes vía IF/ELSE IF); La prioridad de evaluación es: especialidad igual + dentro de rango → especialidad igual + fuera de rango → especialidad distinta + dentro de rango → especialidad distinta + fuera de rango; Operator=1 representa coincidencia de especialidad y Operator=2 representa exclusión de especialidad; Operator2=1 representa inclusión horaria y Operator2=2 representa exclusión horaria; Solo se considera la parte horaria del ServiceDate (Cast a Time)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Especialidad; Tarifa; Manual tarifario; Liquidación de contratos; Variación de tarifa; Valor de venta; Recargo; Franja horaria de atención', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando Operator=1 y Operator2=1 y la hora del ServiceDate está dentro de [StartTime2, EndTime2] y SpecialtyId coincide con el parámetro, inserta TOP 1 registro coincidente; [INSERT] @DefinitionRateDetailCondition: Cuando Operator=1 y Operator2=2 y la hora del ServiceDate es < StartTime2 y > EndTime2 y SpecialtyId coincide, inserta TOP 1 registro coincidente; [INSERT] @DefinitionRateDetailCondition: Cuando Operator=2 y Operator2=1 y la hora del ServiceDate está dentro de [StartTime2, EndTime2] y SpecialtyId es distinto al parámetro, inserta TOP 1 registro coincidente; [INSERT] @DefinitionRateDetailCondition: Cuando Operator=2 y Operator2=2 y la hora del ServiceDate es < StartTime2 y > EndTime2 y SpecialtyId es distinto al parámetro, inserta TOP 1 registro coincidente', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Operator=1 (especialidad igual) y Operator2=1 (hora dentro del rango) con coincidencias → Devuelve TOP 1 condición con SpecialtyId = parámetro y hora ∈ [StartTime2, EndTime2] else Evalúa siguiente combinación; si Operator=1 y Operator2=2 (hora fuera del rango) con coincidencias → Devuelve TOP 1 condición con SpecialtyId = parámetro y hora < StartTime2 y > EndTime2 else Evalúa siguiente combinación; si Operator=2 (especialidad distinta) y Operator2=1 con coincidencias → Devuelve TOP 1 condición con SpecialtyId <> parámetro y hora ∈ [StartTime2, EndTime2] else Evalúa siguiente combinación; si Operator=2 y Operator2=2 con coincidencias → Devuelve TOP 1 condición con SpecialtyId <> parámetro y hora < StartTime2 y > EndTime2 else Retorna tabla vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyTime';
GO
