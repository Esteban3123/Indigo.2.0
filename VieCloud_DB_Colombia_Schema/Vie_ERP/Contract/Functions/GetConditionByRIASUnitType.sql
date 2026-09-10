CREATE Function [Contract].[GetConditionByRIASUnitType]
(
	@RiasId Int,
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
		, FunctionalUnitId Int Null
		, UnitTypeId2 Tinyint Null
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
		t.x.value('FunctionalUnitId[1]','Int'),
		t.x.value('UnitTypeId2[1]','Tinyint'),
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
		Where Operator = 1 And Operator2 = 1 And RIASId = @RiasId And UnitTypeId2 = @UnitType) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And RIASId = @RiasId And UnitTypeId2 = @UnitType

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And RIASId = @RiasId And UnitTypeId2 <> @UnitType) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And RIASId = @RiasId And UnitTypeId2 <> @UnitType

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And RIASId <> @RiasId And UnitTypeId2 = @UnitType) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And RIASId <> @RiasId And UnitTypeId2 = @UnitType

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And RIASId <> @RiasId And UnitTypeId2 <> @UnitType) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And RIASId <> @RiasId And UnitTypeId2 <> @UnitType

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a un servicio según su RIAS (Ruta Integral de Atención en Salud) y tipo de unidad funcional. Recibe como entrada el identificador del RIAS, el tipo de unidad y una lista de condiciones tarifarias en formato XML, las cuales parsea y evalúa en orden de prioridad: primero busca coincidencia exacta en RIAS y tipo de unidad, luego coincidencia parcial en distintas combinaciones (RIAS igual/distinto, tipo de unidad igual/distinto). Retorna la condición tarifaria más específica que aplica, incluyendo el tipo de liquidación, tipo de manual tarifario, vigencia del manual, variación de tarifa, valor de venta y valor de venta con recargo. Se usa en la liquidación de contratos para resolver qué tarifa y condiciones económicas corresponden a un servicio de salud prestado bajo una ruta de atención específica.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByRIASUnitType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByRIASUnitType';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona la condición tarifaria aplicable según la combinación de operadores de igualdad/diferencia entre la RIAS y el tipo de unidad funcional, devolviendo a lo sumo una condición priorizada.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones debe seguir la estructura /RateConditionList con los nodos esperados (Id, Operator, Operator2, RIASId, UnitTypeId2, etc.).; Operator y Operator2 deben usar los códigos 1 (igual) o 2 (distinto) para que alguna rama aplique.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La tabla resultante contendrá como máximo una fila (uso de TOP 1 y ramas mutuamente excluyentes con IF/ELSE).; La prioridad de selección es estricta: (igual,igual) > (igual,distinto) > (distinto,igual) > (distinto,distinto).; Operator codifica la comparación con RIASId (1=igual, 2=distinto) y Operator2 codifica la comparación con UnitTypeId2 (1=igual, 2=distinto).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIAS (Ruta Integral de Atención en Salud); Tipo de unidad funcional; Condición tarifaria; Manual tarifario; Variación de tarifa; Valor de venta con recargo; Tipo de liquidación', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando existe una condición con Operator=1 y Operator2=1, RIASId igual al recibido y UnitTypeId2 igual al tipo de unidad recibido, se inserta el TOP 1 de esas filas.; [INSERT] @DefinitionRateDetailCondition: Si la rama anterior no aplica y existe Operator=1, Operator2=2, RIASId igual y UnitTypeId2 distinto al recibido, se inserta el TOP 1 de esas filas.; [INSERT] @DefinitionRateDetailCondition: Si las ramas anteriores no aplican y existe Operator=2, Operator2=1, RIASId distinto y UnitTypeId2 igual al recibido, se inserta el TOP 1 de esas filas.; [INSERT] @DefinitionRateDetailCondition: Si ninguna de las ramas anteriores aplica y existe Operator=2, Operator2=2, RIASId distinto y UnitTypeId2 distinto al recibido, se inserta el TOP 1 de esas filas.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe condición con Operator=1, Operator2=1, RIASId igual y UnitTypeId2 igual → Devuelve TOP 1 de coincidencias exactas RIAS+UnitType else Evalúa siguiente rama; si Existe condición con Operator=1, Operator2=2, RIASId igual y UnitTypeId2 distinto → Devuelve TOP 1 con RIAS igual y UnitType distinto else Evalúa siguiente rama; si Existe condición con Operator=2, Operator2=1, RIASId distinto y UnitTypeId2 igual → Devuelve TOP 1 con RIAS distinto y UnitType igual else Evalúa siguiente rama; si Existe condición con Operator=2, Operator2=2, RIASId distinto y UnitTypeId2 distinto → Devuelve TOP 1 con RIAS distinto y UnitType distinto else Retorna tabla vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASUnitType';
GO
