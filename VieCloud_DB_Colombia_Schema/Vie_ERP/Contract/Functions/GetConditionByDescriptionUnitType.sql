CREATE Function [Contract].[GetConditionByDescriptionUnitType]
(
	@ContractDescriptionId Int,
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
		, ContractDescriptionId Int Null)

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
		t.x.value('ContractDescriptionId[1]','Int')
	From @rateConditionListXml.nodes('/RateConditionList') t(x)
		
	If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And ContractDescriptionId = @ContractDescriptionId And UnitTypeId2 = @UnitType) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And ContractDescriptionId = @ContractDescriptionId And UnitTypeId2 = @UnitType

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And ContractDescriptionId = @ContractDescriptionId And UnitTypeId2 <> @UnitType) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And ContractDescriptionId = @ContractDescriptionId And UnitTypeId2 <> @UnitType

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And ContractDescriptionId <> @ContractDescriptionId And UnitTypeId2 = @UnitType) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And ContractDescriptionId <> @ContractDescriptionId And UnitTypeId2 = @UnitType

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And ContractDescriptionId <> @ContractDescriptionId And UnitTypeId2 <> @UnitType) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And ContractDescriptionId <> @ContractDescriptionId And UnitTypeId2 <> @UnitType

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a un ítem contractual (descripción de contrato) según el tipo de unidad de liquidación. Recibe como entrada una lista de condiciones tarifarias en formato XML, el identificador de la descripción del contrato y el tipo de unidad, y evalúa en orden de prioridad cuál condición encaja mejor: primero la que coincide exactamente con la descripción y el tipo de unidad, luego coincidencia solo por descripción, luego solo por tipo de unidad, y finalmente ninguna coincidencia exacta. Retorna los parámetros de liquidación correspondientes: tipo de liquidación, tipo de manual tarifario, vigencia del manual, variación de tarifa, valor de venta y valor de venta con recargo. Se usa en el proceso de facturación y liquidación de servicios para resolver qué tarifa o condición económica aplicar a un servicio según el contrato y la unidad funcional del paciente.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByDescriptionUnitType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByDescriptionUnitType';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona la condición tarifaria aplicable a una descripción contractual y tipo de unidad, evaluando combinaciones de operadores de igualdad/desigualdad con prioridad jerárquica.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones debe seguir la estructura /RateConditionList con los nodos esperados (Id, Operator, Operator2, ContractDescriptionId, UnitTypeId2, etc.).; Cada condición en el XML debe tener un Id único (clave primaria de la tabla temporal).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Operator=1 representa igualdad y Operator=2 representa desigualdad respecto al ContractDescriptionId.; Operator2=1 representa igualdad y Operator2=2 representa desigualdad respecto al UnitType.; La selección sigue una prioridad estricta: (=,=) > (=,<>) > (<>,=) > (<>,<>); solo se evalúa una rama.; Siempre se devuelve a lo sumo una fila (Top 1) en la tabla resultado.; Si ninguna combinación tiene coincidencias, la tabla retornada queda vacía.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Contrato; Descripción de contrato; Tipo de unidad funcional; Condición tarifaria; Manual tarifario; Variación de tarifa; Valor de venta; Recargo; Tipo de liquidación', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando existe condición con Operator=1 Y Operator2=1 Y ContractDescriptionId coincide Y UnitTypeId2 coincide, inserta el TOP 1 (match exacto en ambos criterios).; [INSERT] @DefinitionRateDetailCondition: Si no hay match exacto, cuando existe condición con Operator=1 Y Operator2=2 Y ContractDescriptionId coincide Y UnitTypeId2 distinto, inserta el TOP 1.; [INSERT] @DefinitionRateDetailCondition: Si los anteriores fallan, cuando existe condición con Operator=2 Y Operator2=1 Y ContractDescriptionId distinto Y UnitTypeId2 coincide, inserta el TOP 1.; [INSERT] @DefinitionRateDetailCondition: Como último recurso, cuando existe condición con Operator=2 Y Operator2=2 Y ContractDescriptionId distinto Y UnitTypeId2 distinto, inserta el TOP 1.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Operator=1 AND Operator2=1 AND ContractDescriptionId = parámetro AND UnitTypeId2 = parámetro → Devuelve la primera condición que cumple coincidencia exacta de descripción y unidad. else Evalúa la siguiente combinación de operadores.; si Operator=1 AND Operator2=2 AND ContractDescriptionId = parámetro AND UnitTypeId2 <> parámetro → Devuelve la primera condición con descripción coincidente y unidad distinta. else Evalúa la siguiente combinación.; si Operator=2 AND Operator2=1 AND ContractDescriptionId <> parámetro AND UnitTypeId2 = parámetro → Devuelve la primera condición con descripción distinta y unidad coincidente. else Evalúa la última combinación.; si Operator=2 AND Operator2=2 AND ContractDescriptionId <> parámetro AND UnitTypeId2 <> parámetro → Devuelve la primera condición con ambos criterios distintos. else Retorna tabla vacía.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionUnitType';
GO
