CREATE Function [Contract].[GetConditionByUnitTypeFunctionalUnit]
(
	@UnitType Tinyint,
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
		, FunctionalUnitId2 Int Null
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
		t.x.value('FunctionalUnitId2[1]','Int'),
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
		Where Operator = 1 And Operator2 = 1 And UnitTypeId = @UnitType And FunctionalUnitId2 = @FunctionalUnitId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And UnitTypeId = @UnitType And FunctionalUnitId2 = @FunctionalUnitId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And UnitTypeId = @UnitType And FunctionalUnitId2 <> @FunctionalUnitId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And UnitTypeId = @UnitType And FunctionalUnitId2 <> @FunctionalUnitId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And UnitTypeId <> @UnitType And FunctionalUnitId2 = @FunctionalUnitId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And UnitTypeId <> @UnitType And FunctionalUnitId2 = @FunctionalUnitId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And UnitTypeId <> @UnitType And FunctionalUnitId2 <> @FunctionalUnitId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And UnitTypeId <> @UnitType And FunctionalUnitId2 <> @FunctionalUnitId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a un servicio según el tipo de unidad funcional y la unidad funcional específica. Recibe un listado de condiciones tarifarias en formato XML (con operadores de comparación, tipo de unidad, unidad funcional, tipo de liquidación, tipo de manual, variación de tarifa y valores de venta) y aplica una lógica de prioridad en cascada: primero busca una coincidencia exacta de tipo de unidad y unidad funcional; si no hay, busca tipo de unidad igual pero unidad funcional diferente; luego tipo de unidad diferente pero unidad funcional igual; y finalmente ambos diferentes. Retorna la condición tarifaria ganadora con su tipo de liquidación, manual de tarifas, variación porcentual, valor de venta y valor de venta con recargo, usada para calcular el precio de un servicio en la contratación con aseguradoras o pagadores.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByUnitTypeFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByUnitTypeFunctionalUnit';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona, desde una lista XML de condiciones tarifarias, la condición aplicable según coincidencia de tipo de unidad y unidad funcional, priorizando coincidencias exactas y degradando a coincidencias parciales o negadas.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe tener estructura /RateConditionList con los nodos esperados (Id, Operator, Operator2, UnitTypeId, FunctionalUnitId2, etc.).; Los operadores se interpretan como 1 = igualdad y 2 = desigualdad respecto al tipo de unidad y unidad funcional recibidos.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La tabla de retorno contiene como máximo una fila (uso de TOP 1 y bifurcaciones excluyentes con If/Else If).; El orden de prioridad es estricto: (igual, igual) > (igual, distinto) > (distinto, igual) > (distinto, distinto).; Operator=1 representa condición de igualdad y Operator=2 representa condición de desigualdad sobre tipo de unidad / unidad funcional.; Si ninguna condición del XML satisface alguno de los cuatro patrones, la función retorna conjunto vacío.; El Id de la condición se preserva como clave primaria en el resultado.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Condición tarifaria; Tipo de unidad; Unidad funcional; Tipo de liquidación; Manual de tarifas; Vigencia de manual de tarifas; Variación de tarifa; Valor de venta; Valor de venta con recargo', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando existe alguna condición con Operator=1 y Operator2=1, UnitTypeId igual al parámetro y FunctionalUnitId2 igual al parámetro, se inserta la primera (TOP 1) coincidencia.; [INSERT] @DefinitionRateDetailCondition: Si no hubo coincidencia previa y existe alguna con Operator=1, Operator2=2, UnitTypeId igual al parámetro y FunctionalUnitId2 distinto del parámetro, se inserta la primera coincidencia.; [INSERT] @DefinitionRateDetailCondition: Si no hubo coincidencia previa y existe alguna con Operator=2, Operator2=1, UnitTypeId distinto del parámetro y FunctionalUnitId2 igual al parámetro, se inserta la primera coincidencia.; [INSERT] @DefinitionRateDetailCondition: Si no hubo coincidencia previa y existe alguna con Operator=2 y Operator2=2, UnitTypeId distinto del parámetro y FunctionalUnitId2 distinto del parámetro, se inserta la primera coincidencia.; [RETURN_RESULT] @DefinitionRateDetailCondition: Devuelve a lo sumo una fila con la condición tarifaria seleccionada (Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe condición con Operator=1, Operator2=1, UnitTypeId = @UnitType y FunctionalUnitId2 = @FunctionalUnitId → Selecciona la primera de coincidencia exacta (tipo de unidad igual y unidad funcional igual). else Evalúa siguiente bifurcación.; si Existe condición con Operator=1, Operator2=2, UnitTypeId = @UnitType y FunctionalUnitId2 <> @FunctionalUnitId → Selecciona la primera con tipo de unidad igual y unidad funcional diferente. else Evalúa siguiente bifurcación.; si Existe condición con Operator=2, Operator2=1, UnitTypeId <> @UnitType y FunctionalUnitId2 = @FunctionalUnitId → Selecciona la primera con tipo de unidad diferente y unidad funcional igual. else Evalúa siguiente bifurcación.; si Existe condición con Operator=2, Operator2=2, UnitTypeId <> @UnitType y FunctionalUnitId2 <> @FunctionalUnitId → Selecciona la primera con tipo de unidad y unidad funcional ambos diferentes. else No retorna ninguna fila.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeFunctionalUnit';
GO
