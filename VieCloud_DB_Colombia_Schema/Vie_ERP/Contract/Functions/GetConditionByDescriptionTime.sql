CREATE Function [Contract].[GetConditionByDescriptionTime]
(
	@ContractDescriptionId int,
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
		, SalesValueWithSurcharge Decimal(18, 2) Null
		, ContractDescriptionId Int Null)

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
		t.x.value('SalesValueWithSurcharge[1]','Decimal(18, 2)'),
		t.x.value('ContractDescriptionId[1]','Int')
	From @rateConditionListXml.nodes('/RateConditionList') t(x)
		
	If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2 And ContractDescriptionId = @ContractDescriptionId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2 And ContractDescriptionId = @ContractDescriptionId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2 And ContractDescriptionId = @ContractDescriptionId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2 And ContractDescriptionId = @ContractDescriptionId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2 And ContractDescriptionId <> @ContractDescriptionId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2 And ContractDescriptionId <> @ContractDescriptionId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2 And ContractDescriptionId <> @ContractDescriptionId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2 And ContractDescriptionId <> @ContractDescriptionId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a un servicio según la franja horaria en que fue prestado. Recibe como entrada el identificador de la descripción del contrato, la fecha y hora del servicio, y una lista de condiciones tarifarias en formato XML; a partir de esa lista evalúa si la hora del servicio cae dentro o fuera de los rangos horarios definidos en cada condición, aplicando operadores de inclusión o exclusión y filtrando por la descripción de contrato correspondiente o cualquier otra. Retorna la condición tarifaria que aplica, incluyendo el tipo de liquidación, tipo de manual tarifario, variación de tarifa, valor de venta y valor de venta con recargo, para ser usada en la liquidación de contratos con tarifas diferenciadas por horario, como guardias nocturnas, festivos o franjas especiales.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByDescriptionTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByDescriptionTime';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona, a partir de una lista XML de condiciones tarifarias, la primera que aplique según la franja horaria del servicio y si la condición es para la descripción de contrato indicada o para otras, devolviendo sus parámetros de liquidación.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones debe tener nodos /RateConditionList con los campos esperados (Id, Operator, Operator2, StartTime2, EndTime2, ContractDescriptionId, etc.); Operator debe codificar inclusión/exclusión por descripción de contrato (1=igual, 2=distinto); Operator2 debe codificar inclusión/exclusión por rango horario (1=dentro del rango, 2=fuera del rango); StartTime2 y EndTime2 deben estar definidos para evaluar la franja horaria', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Retorna a lo sumo una fila (Top 1) en la tabla resultado; Las ramas se evalúan en orden de prioridad: (1,1) > (1,2) > (2,1) > (2,2); la primera que tenga coincidencias define el resultado; Operator=1 implica filtro por igualdad de ContractDescriptionId; Operator=2 implica filtro por desigualdad; Operator2=1 implica que la hora debe estar dentro del rango [StartTime2, EndTime2]; Operator2=2 implica que debe estar fuera (estrictamente menor que StartTime2 y mayor que EndTime2); Solo se considera la parte horaria de @ServiceDate (Cast a Time), ignorando la fecha', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'condición tarifaria; tarifa; liquidación de contratos; manual tarifario; variación de tarifa; valor de venta; valor de venta con recargo; franja horaria; descripción de contrato', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando Operator=1 y Operator2=1 y la hora de @ServiceDate está entre StartTime2 y EndTime2 y ContractDescriptionId coincide con @ContractDescriptionId, inserta TOP 1 condición coincidente.; [INSERT] @DefinitionRateDetailCondition: Cuando Operator=1 y Operator2=2 y la hora de @ServiceDate es < StartTime2 y > EndTime2 y ContractDescriptionId coincide con @ContractDescriptionId, inserta TOP 1 condición coincidente.; [INSERT] @DefinitionRateDetailCondition: Cuando Operator=2 y Operator2=1 y la hora de @ServiceDate está entre StartTime2 y EndTime2 y ContractDescriptionId es distinto de @ContractDescriptionId, inserta TOP 1 condición coincidente.; [INSERT] @DefinitionRateDetailCondition: Cuando Operator=2 y Operator2=2 y la hora de @ServiceDate es < StartTime2 y > EndTime2 y ContractDescriptionId es distinto de @ContractDescriptionId, inserta TOP 1 condición coincidente.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe condición con Operator=1, Operator2=1, hora del servicio dentro de [StartTime2, EndTime2] y misma descripción de contrato → Devuelve TOP 1 de ese grupo (descripción incluida + horario incluido) else Evalúa siguiente rama; si Existe condición con Operator=1, Operator2=2, hora fuera del rango y misma descripción de contrato → Devuelve TOP 1 de ese grupo (descripción incluida + horario excluido) else Evalúa siguiente rama; si Existe condición con Operator=2, Operator2=1, hora dentro del rango y descripción de contrato distinta → Devuelve TOP 1 de ese grupo (descripción excluida + horario incluido) else Evalúa siguiente rama; si Existe condición con Operator=2, Operator2=2, hora fuera del rango y descripción de contrato distinta → Devuelve TOP 1 de ese grupo (descripción excluida + horario excluido) else Retorna tabla vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionTime';
GO
