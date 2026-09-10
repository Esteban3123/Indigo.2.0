CREATE Function [Contract].[GetConditionByRIASTime]
(
	@RiasId int,
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
		, RIASId Int Null)

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
		t.x.value('RIASId[1]','Int')
	From @rateConditionListXml.nodes('/RateConditionList') t(x)
		
	If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2 And RIASId = @RiasId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2 And RIASId = @RiasId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2 And RIASId = @RiasId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2 And RIASId = @RiasId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2 And RIASId <> @RiasId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And Cast(@ServiceDate As Time) >= StartTime2 And Cast(@ServiceDate As Time) <= EndTime2 And RIASId <> @RiasId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2 And RIASId <> @RiasId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And Cast(@ServiceDate As Time) < StartTime2 And Cast(@ServiceDate As Time) > EndTime2 And RIASId <> @RiasId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que determina la condición tarifaria aplicable a un servicio RIAS (Rutas Integrales de Atención en Salud) según la hora en que fue prestado. Recibe como entrada el identificador del RIAS, la fecha y hora del servicio, y una lista de condiciones tarifarias en formato XML; evalúa cada condición comparando la hora del servicio contra rangos horarios definidos (StartTime2 y EndTime2) y el tipo de operador lógico (igual o diferente al RIAS indicado), retornando la condición que corresponde con su tipo de liquidación, tipo de manual tarifario, variación de tarifa y valor de venta (con y sin recargo). Se usa en la liquidación y facturación de contratos para aplicar tarifas diferenciales según el horario de atención de los servicios RIAS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByRIASTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByRIASTime';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona la condición tarifaria aplicable según la hora del servicio y la coincidencia (o no) con un RIAS, devolviendo sus parámetros de liquidación y valores de venta.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones debe tener estructura /RateConditionList con los nodos esperados (Id, Operator, Operator2, StartTime2, EndTime2, RIASId, etc.).; Operator debe valer 1 (igual al RIAS indicado) o 2 (distinto al RIAS indicado).; Operator2 debe valer 1 (rango horario normal) o 2 (rango horario invertido/nocturno).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'A lo sumo se devuelve una sola condición (TOP 1) por la primera rama satisfecha.; Las ramas son mutuamente excluyentes: la prioridad es Operator=1/Operator2=1, luego 1/2, luego 2/1, luego 2/2.; Operator=1 implica filtrar por igualdad con el RIAS indicado; Operator=2 implica filtrar por desigualdad.; Operator2=1 evalúa rango horario directo (>=Start y <=End); Operator2=2 evalúa rango horario invertido (<Start y >End).; Solo la parte de hora (Time) del ServiceDate se usa para comparar contra los rangos.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIAS (Rutas Integrales de Atención en Salud); Tarifa / variación de tarifa; Manual tarifario; Tipo de liquidación; Valor de venta; Recargo; Vigencia de manual tarifario; Especialidad; Horario de atención del servicio', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando existe condición con Operator=1 y Operator2=1 y la hora del servicio está entre StartTime2 y EndTime2 y RIASId coincide con el RIAS indicado, inserta TOP 1 esa condición.; [INSERT] @DefinitionRateDetailCondition: Si no aplica la primera y existe condición con Operator=1 y Operator2=2 y la hora del servicio es menor a StartTime2 y mayor a EndTime2 y RIASId coincide con el RIAS, inserta TOP 1 esa condición.; [INSERT] @DefinitionRateDetailCondition: Si no aplican las anteriores y existe condición con Operator=2 y Operator2=1 y hora del servicio entre StartTime2 y EndTime2 y RIASId distinto al RIAS indicado, inserta TOP 1 esa condición.; [INSERT] @DefinitionRateDetailCondition: Si no aplican las anteriores y existe condición con Operator=2 y Operator2=2 y hora del servicio menor a StartTime2 y mayor a EndTime2 y RIASId distinto al RIAS indicado, inserta TOP 1 esa condición.; [RETURN_RESULT] @DefinitionRateDetailCondition: Retorna la tabla con a lo sumo una condición seleccionada según la primera rama que se cumpla; vacía si ninguna rama aplica.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Operator=1 y Operator2=1 y hora del servicio dentro del rango [StartTime2, EndTime2] y RIASId = RIAS indicado → Selecciona TOP 1 esa condición como resultado else Evalúa siguiente rama; si Operator=1 y Operator2=2 y hora del servicio < StartTime2 y > EndTime2 y RIASId = RIAS indicado → Selecciona TOP 1 esa condición (rango horario invertido) coincidente con el RIAS else Evalúa siguiente rama; si Operator=2 y Operator2=1 y hora del servicio dentro del rango [StartTime2, EndTime2] y RIASId <> RIAS indicado → Selecciona TOP 1 esa condición para RIAS distinto else Evalúa siguiente rama; si Operator=2 y Operator2=2 y hora del servicio < StartTime2 y > EndTime2 y RIASId <> RIAS indicado → Selecciona TOP 1 esa condición (rango invertido) para RIAS distinto else No retorna ninguna condición', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASTime';
GO
