CREATE Function [Contract].[GetConditionByTimeSpecialty]
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
		, StartTime Time Null
		, EndTime Time Null
		, SpecialtyId2 Char(3) Null
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
		t.x.value('SpecialtyId2[1]','Char(3)'),
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
		Where Operator = 1 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And SpecialtyId2 = @SpecialtyId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And SpecialtyId2 = @SpecialtyId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And SpecialtyId2 <> @SpecialtyId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And SpecialtyId2 <> @SpecialtyId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And SpecialtyId2 = @SpecialtyId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And SpecialtyId2 = @SpecialtyId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And SpecialtyId2 <> @SpecialtyId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And SpecialtyId2 <> @SpecialtyId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contratos que determina la condición tarifaria aplicable a un servicio según la hora de atención y la especialidad médica. Recibe como entrada un XML con la lista de condiciones de tarifa definidas en el manual de tarifas del contrato, la fecha y hora del servicio, y el código de especialidad; evalúa qué condición cumple los criterios de franja horaria (hora inicio y hora fin) y coincidencia o diferencia de especialidad para devolver el detalle tarifario correspondiente. El resultado incluye el tipo de liquidación, tipo de manual, variación de tarifa, valor de venta y valor de venta con recargo, permitiendo calcular el precio correcto de un servicio según las condiciones pactadas en el contrato con el asegurador o pagador.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByTimeSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByTimeSpecialty';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona, de una lista XML de condiciones tarifarias, aquella que aplica según la franja horaria del servicio y la coincidencia/diferencia de especialidad, devolviendo su detalle de liquidación y valores de venta.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones debe respetar el esquema /RateConditionList con los nodos esperados (Id, Operator, Operator2, StartTime, EndTime, SpecialtyId2, etc.).; Cada condición debe traer Operator (1 o 2) y Operator2 (1 o 2) que determinan la lógica horaria y de especialidad.; Se requiere SpecialtyId y ServiceDate para evaluar las condiciones.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La tabla retornada nunca contiene más de una fila (uso de TOP 1 en cada rama y evaluación excluyente if/else).; Las ramas se evalúan en orden de prioridad: primero rango horario directo con misma especialidad, luego con distinta, y por último rango invertido (mismas dos variantes).; Operator=1 implica rango horario directo (hora >= Inicio y <= Fin); Operator=2 implica rango invertido (hora < Inicio y > Fin).; Operator2=1 exige coincidencia de especialidad; Operator2=2 exige especialidad distinta.; Solo se consideran condiciones cuyo SpecialtyId2 no es nulo cuando se compara con la especialidad recibida.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Especialidad; Tarifa / Manual tarifario; Vigencia de manual tarifario; Tipo de liquidación; Variación de tarifa; Valor de venta; Valor de venta con recargo; Franja horaria del servicio; Condición tarifaria contractual', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando Operator=1 y Operator2=1 y la hora del servicio está dentro de [StartTime, EndTime] y SpecialtyId2 = especialidad recibida, se inserta el TOP 1 de esa condición.; [INSERT] @DefinitionRateDetailCondition: Cuando Operator=1 y Operator2=2 y la hora del servicio está dentro de [StartTime, EndTime] y SpecialtyId2 <> especialidad recibida, se inserta el TOP 1 de esa condición.; [INSERT] @DefinitionRateDetailCondition: Cuando Operator=2 y Operator2=1 y la hora del servicio es < StartTime y > EndTime (rango invertido/nocturno) y SpecialtyId2 = especialidad recibida, se inserta el TOP 1 de esa condición.; [INSERT] @DefinitionRateDetailCondition: Cuando Operator=2 y Operator2=2 y la hora del servicio es < StartTime y > EndTime y SpecialtyId2 <> especialidad recibida, se inserta el TOP 1 de esa condición.; [RETURN_RESULT] @DefinitionRateDetailCondition: Devuelve a lo sumo una fila correspondiente a la primera rama de condición que se cumpla; si ninguna se cumple, retorna tabla vacía.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Operator=1 y Operator2=1 y hora del servicio dentro de [StartTime,EndTime] y SpecialtyId2 = SpecialtyId → Se selecciona TOP 1 de esta condición y se omiten las demás ramas.; si No aplica la rama anterior y Operator=1 y Operator2=2 y hora dentro de [StartTime,EndTime] y SpecialtyId2 <> SpecialtyId → Se selecciona TOP 1 de esta condición.; si No aplican ramas previas y Operator=2 y Operator2=1 y hora < StartTime y > EndTime y SpecialtyId2 = SpecialtyId → Se selecciona TOP 1 de esta condición (franja horaria invertida con misma especialidad).; si No aplican ramas previas y Operator=2 y Operator2=2 y hora < StartTime y > EndTime y SpecialtyId2 <> SpecialtyId → Se selecciona TOP 1 de esta condición (franja invertida con especialidad distinta). else No se inserta ninguna fila y la tabla de retorno queda vacía.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeSpecialty';
GO
