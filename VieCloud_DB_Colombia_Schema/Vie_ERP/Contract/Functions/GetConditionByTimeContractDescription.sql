CREATE Function [Contract].[GetConditionByTimeContractDescription]
(
	@ServiceDate DateTime,
	@ContractDescriptionId Int,
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
		, ContractDescriptionId2 Int Null)

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
		t.x.value('ContractDescriptionId2[1]','Int')
	From @rateConditionListXml.nodes('/RateConditionList') t(x)
		
	If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And ContractDescriptionId2 = @ContractDescriptionId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And ContractDescriptionId2 = @ContractDescriptionId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And ContractDescriptionId2 <> @ContractDescriptionId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And Cast(@ServiceDate As Time) >= StartTime And Cast(@ServiceDate As Time) <= EndTime And ContractDescriptionId2 <> @ContractDescriptionId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And ContractDescriptionId2 = @ContractDescriptionId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And ContractDescriptionId2 = @ContractDescriptionId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And ContractDescriptionId2 <> @ContractDescriptionId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime And Cast(@ServiceDate As Time) > EndTime And ContractDescriptionId2 <> @ContractDescriptionId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina qué condición tarifaria aplica a un servicio según la hora en que fue prestado y el ítem del contrato (descripción de contrato) al que pertenece. Recibe como entrada la fecha y hora del servicio, el identificador de la descripción de contrato vigente y una lista de condiciones tarifarias en formato XML; parsea ese XML en una tabla temporal y evalúa rangos horarios (hora inicio / hora fin) combinados con operadores de comparación (dentro o fuera del rango) y la coincidencia o no con la descripción de contrato indicada. Retorna la condición ganadora con su tipo de liquidación, tipo de manual tarifario, vigencia del manual, variación de tarifa y valores de venta (con y sin recargo), usada en la liquidación automática de contratos con tarifas diferenciales por franja horaria.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByTimeContractDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByTimeContractDescription';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona la condición tarifaria aplicable a una fecha/hora de servicio según rangos horarios y coincidencia con la descripción de contrato, devolviendo parámetros de liquidación y valores de venta.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeContractDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones debe contener nodos /RateConditionList con los campos esperados (Id, Operator, Operator2, StartTime, EndTime, ContractDescriptionId2, etc.); Operator y Operator2 deben tomar valores 1 o 2 para que alguna rama aplique; StartTime y EndTime deben estar definidos para evaluar el rango horario', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeContractDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La tabla retornada contiene como máximo una fila (uso de TOP 1 y ramas mutuamente excluyentes vía IF/ELSE IF); El orden de prioridad entre condiciones es: dentro del rango con misma descripción > dentro del rango con distinta descripción > fuera del rango con misma descripción > fuera del rango con distinta descripción; Operator codifica la pertenencia al rango horario (1=dentro, 2=fuera) y Operator2 codifica la coincidencia de descripción de contrato (1=igual, 2=distinta); Si ninguna rama se cumple, no se inserta ninguna fila', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeContractDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Contrato; Descripción de contrato; Tarifa diferencial por franja horaria; Tipo de liquidación; Manual tarifario; Vigencia de manual tarifario; Variación de tarifa; Valor de venta; Recargo; Liquidación automática', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeContractDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando Operator=1 y Operator2=1 y la hora del servicio está dentro de [StartTime, EndTime] y ContractDescriptionId2 coincide con la descripción del contrato, inserta TOP 1 con los datos de tarifa de esa condición; [INSERT] @DefinitionRateDetailCondition: Cuando Operator=1 y Operator2=2 y la hora del servicio está dentro de [StartTime, EndTime] y ContractDescriptionId2 NO coincide con la descripción del contrato, inserta TOP 1 con los datos de tarifa de esa condición; [INSERT] @DefinitionRateDetailCondition: Cuando Operator=2 y Operator2=1 y la hora del servicio está fuera del rango (hora < StartTime AND hora > EndTime) y ContractDescriptionId2 coincide con la descripción del contrato, inserta TOP 1 con los datos de tarifa; [INSERT] @DefinitionRateDetailCondition: Cuando Operator=2 y Operator2=2 y la hora del servicio está fuera del rango (hora < StartTime AND hora > EndTime) y ContractDescriptionId2 NO coincide con la descripción del contrato, inserta TOP 1 con los datos de tarifa; [RETURN_RESULT] @DefinitionRateDetailCondition: Devuelve la tabla con a lo sumo una fila correspondiente a la primera rama que se cumpla siguiendo el orden de evaluación (1-1, 1-2, 2-1, 2-2)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeContractDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Operator=1 y Operator2=1: hora dentro del rango y misma descripción de contrato → Inserta la primera condición coincidente y omite las demás ramas else Evalúa siguiente rama; si Operator=1 y Operator2=2: hora dentro del rango y distinta descripción de contrato → Inserta la primera condición coincidente y omite las demás ramas else Evalúa siguiente rama; si Operator=2 y Operator2=1: hora fuera del rango y misma descripción de contrato → Inserta la primera condición coincidente y omite la última rama else Evalúa última rama; si Operator=2 y Operator2=2: hora fuera del rango y distinta descripción de contrato → Inserta la primera condición coincidente else No inserta nada y retorna tabla vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeContractDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByTimeContractDescription';
GO
