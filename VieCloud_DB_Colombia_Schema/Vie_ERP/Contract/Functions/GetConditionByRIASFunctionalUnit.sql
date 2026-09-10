CREATE Function [Contract].[GetConditionByRIASFunctionalUnit]
(
	@RiasId int,	
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
		, SpecialtyId Char(3) Null
		, FunctionalUnitId2 Int Null
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
		t.x.value('SpecialtyId[1]','Char(3)'),
		t.x.value('FunctionalUnitId2[1]','Int'),
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
		Where Operator = 1 And Operator2 = 1 And RIASId = @RiasId And FunctionalUnitId2 = @FunctionalUnitId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And RIASId = @RiasId And FunctionalUnitId2 = @FunctionalUnitId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And RIASId = @RiasId And FunctionalUnitId2 <> @FunctionalUnitId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And RIASId = @RiasId And FunctionalUnitId2 <> @FunctionalUnitId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And RIASId <> @RiasId And FunctionalUnitId2 = @FunctionalUnitId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And RIASId <> @RiasId And FunctionalUnitId2 = @FunctionalUnitId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And RIASId <> @RiasId And FunctionalUnitId2 <> @FunctionalUnitId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And RIASId <> @RiasId And FunctionalUnitId2 <> @FunctionalUnitId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a un servicio de salud según la RIAS (Ruta Integral de Atención en Salud) y la unidad funcional indicadas. Recibe un listado de condiciones tarifarias en formato XML y evalúa por orden de prioridad cuál condición coincide exactamente o aproximadamente con la combinación de RIAS y unidad funcional, usando operadores de igualdad o diferencia. Retorna la condición ganadora con su tipo de liquidación, tipo de manual tarifario, vigencia, variación de tarifa, valor de venta y valor de venta con recargo, para ser usada en la facturación y liquidación de contratos de salud.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByRIASFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByRIASFunctionalUnit';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona la condición tarifaria aplicable según la combinación de RIAS y Unidad Funcional, evaluando operadores de igualdad/desigualdad por orden de prioridad.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones tarifarias debe tener la estructura /RateConditionList con los nodos esperados (Id, Operator, Operator2, RIASId, FunctionalUnitId2, etc.); Operator y Operator2 deben tomar valores 1 (igual) o 2 (distinto) para que alguna rama aplique', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La tabla resultante contiene como máximo una fila (uso de TOP 1 y ramas mutuamente excluyentes); Las ramas se evalúan en orden de prioridad: igualdad-igualdad, igualdad-desigualdad, desigualdad-igualdad, desigualdad-desigualdad; Operator=1 representa igualdad y Operator=2 representa desigualdad respecto a los parámetros recibidos; Solo se consideran condiciones provenientes del XML de entrada; no consulta tablas físicas', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIAS (Rutas Integrales de Atención en Salud); Unidad Funcional; Condición tarifaria; Manual tarifario; Vigencia de manual tarifario; Tipo de liquidación; Variación tarifaria; Valor de venta; Valor de venta con recargo; Especialidad', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando existe condición con Operator=1 y Operator2=1 que coincida en RIAS y Unidad Funcional, se inserta el TOP 1 de esa coincidencia (igualdad estricta en ambos); [INSERT] @DefinitionRateDetailCondition: Si no aplica la regla anterior y existe condición con Operator=1 y Operator2=2 que coincida en RIAS pero con Unidad Funcional distinta, se inserta el TOP 1 de esa coincidencia; [INSERT] @DefinitionRateDetailCondition: Si no aplican las anteriores y existe condición con Operator=2 y Operator2=1 con RIAS distinto y Unidad Funcional igual, se inserta el TOP 1 de esa coincidencia; [INSERT] @DefinitionRateDetailCondition: Si no aplica ninguna anterior y existe condición con Operator=2 y Operator2=2 con RIAS distinto y Unidad Funcional distinta, se inserta el TOP 1 de esa coincidencia; [RETURN_RESULT] @DefinitionRateDetailCondition: Devuelve a lo sumo una fila con la condición tarifaria seleccionada según la primera rama que se cumpla; si ninguna se cumple, retorna vacío', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen condiciones con Operator=1 AND Operator2=1 AND RIASId = @RiasId AND FunctionalUnitId2 = @FunctionalUnitId → Selecciona TOP 1 de coincidencia exacta en RIAS y Unidad Funcional else Evalúa siguiente rama; si Existen condiciones con Operator=1 AND Operator2=2 AND RIASId = @RiasId AND FunctionalUnitId2 <> @FunctionalUnitId → Selecciona TOP 1 con RIAS igual y Unidad Funcional distinta else Evalúa siguiente rama; si Existen condiciones con Operator=2 AND Operator2=1 AND RIASId <> @RiasId AND FunctionalUnitId2 = @FunctionalUnitId → Selecciona TOP 1 con RIAS distinto y Unidad Funcional igual else Evalúa siguiente rama; si Existen condiciones con Operator=2 AND Operator2=2 AND RIASId <> @RiasId AND FunctionalUnitId2 <> @FunctionalUnitId → Selecciona TOP 1 con RIAS distinto y Unidad Funcional distinta else No inserta nada y retorna tabla vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASFunctionalUnit';
GO
