CREATE Function [Contract].[GetConditionByTimeRIAS]
(
	@ServiceDate DateTime,
	@RiasId Int,
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
		, SalesValueWithSurcharge Decimal(18, 2) Null
		, RIASId2 Int Null)

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
		t.x.value('SalesValueWithSurcharge[1]','Decimal(18, 2)'),
		t.x.value('RIASId2[1]','Int')
	From @rateConditionListXml.nodes('/RateConditionList') t(x)
		
	If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And RIASId2 = @RiasId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And RIASId2 = @RiasId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And RIASId2 <> @RiasId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And RIASId2 <> @RiasId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And RIASId2 = @RiasId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And RIASId2 = @RiasId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And RIASId2 <> @RiasId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And RIASId2 <> @RiasId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a un servicio RIAS (Rutas Integrales de Atención en Salud) según la hora en que se prestó el servicio. Recibe como entrada la fecha y hora del servicio, el identificador de la RIAS y una lista de condiciones tarifarias en formato XML, las cuales deserializa internamente para evaluar franjas horarias y operadores de comparación. Retorna la condición tarifaria que coincide con la hora del servicio (dentro o fuera de un rango horario) y el tipo de RIAS asociado, incluyendo el tipo de liquidación, tipo de manual, variación de tarifa, valor de venta y valor con recargo. Se utiliza en la liquidación y facturación de contratos para aplicar la tarifa correcta según condiciones horarias definidas en el manual tarifario vigente.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByTimeRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByTimeRIAS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona, a partir de una lista XML de condiciones tarifarias, la primera que aplica según la franja horaria del servicio y la coincidencia (igual o distinta) con la RIAS dada.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones debe tener la estructura /RateConditionList con los nodos esperados.; Cada condición debe traer Operator y Operator2 con valores 1 o 2.; ServiceDate debe ser convertible a Time para la comparación de franja.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La tabla resultante contiene a lo sumo una fila (Top 1 en cada rama y ramas mutuamente excluyentes vía Else If).; Las ramas se evalúan en orden de prioridad: (1,1) > (1,2) > (2,1) > (2,2).; Operator define la semántica horaria (1=dentro del rango, 2=fuera del rango) y Operator2 define la semántica de RIAS (1=igual, 2=distinta).; Solo se compara la parte de hora del ServiceDate, no la fecha.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIAS (Rutas Integrales de Atención en Salud); Condición de tarifa; Manual tarifario; Variación de tarifa; Valor de venta; Valor de venta con recargo; Tipo de liquidación; Franja horaria del servicio', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando Operator=1 y Operator2=1 y la hora del servicio está entre StartTime y EndTime y RIASId2 coincide con la RIAS dada, inserta TOP 1 esa condición.; [INSERT] @DefinitionRateDetailCondition: Cuando no aplica el caso anterior y Operator=1 y Operator2=2 y la hora del servicio está entre StartTime y EndTime y RIASId2 difiere de la RIAS dada, inserta TOP 1 esa condición.; [INSERT] @DefinitionRateDetailCondition: Cuando no aplican los casos previos y Operator=2 y Operator2=1 y la hora del servicio es menor a StartTime y mayor a EndTime y RIASId2 coincide con la RIAS dada, inserta TOP 1 esa condición.; [INSERT] @DefinitionRateDetailCondition: Cuando no aplican los casos previos y Operator=2 y Operator2=2 y la hora del servicio es menor a StartTime y mayor a EndTime y RIASId2 difiere de la RIAS dada, inserta TOP 1 esa condición.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Operator=1 (rango horario inclusivo) y Operator2=1 (RIAS igual) → Devuelve la primera condición cuya hora del servicio cae dentro de [StartTime, EndTime] y cuyo RIASId2 = RIAS dada else Evalúa la siguiente combinación de operadores; si Operator=1 y Operator2=2 (RIAS distinta) → Devuelve la primera condición dentro del rango horario cuyo RIASId2 ≠ RIAS dada else Evalúa la siguiente combinación; si Operator=2 (fuera de rango horario) y Operator2=1 → Devuelve la primera condición con hora < StartTime y > EndTime cuyo RIASId2 = RIAS dada else Evalúa la última combinación; si Operator=2 y Operator2=2 → Devuelve la primera condición con hora < StartTime y > EndTime cuyo RIASId2 ≠ RIAS dada else No retorna ninguna fila', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeRIAS';
GO
