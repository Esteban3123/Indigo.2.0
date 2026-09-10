CREATE Function [Contract].[GetConditionByFunctionalUnitUnitType]
(
	@FunctionalUnitId Int,
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
		, SalesValueWithSurcharge Decimal(18, 2) Null)

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
		t.x.value('SalesValueWithSurcharge[1]','Decimal(18, 2)')
	From @rateConditionListXml.nodes('/RateConditionList') t(x)
		
	If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And FunctionalUnitId = @FunctionalUnitId And UnitTypeId2 = @UnitType) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And FunctionalUnitId = @FunctionalUnitId And UnitTypeId2 = @UnitType

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And FunctionalUnitId = @FunctionalUnitId And UnitTypeId2 <> @UnitType) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And FunctionalUnitId = @FunctionalUnitId And UnitTypeId2 <> @UnitType

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And FunctionalUnitId <> @FunctionalUnitId And UnitTypeId2 = @UnitType) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And FunctionalUnitId <> @FunctionalUnitId And UnitTypeId2 = @UnitType

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And FunctionalUnitId <> @FunctionalUnitId And UnitTypeId2 <> @UnitType) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And FunctionalUnitId <> @FunctionalUnitId And UnitTypeId2 <> @UnitType

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a una unidad funcional y tipo de unidad específicos, evaluando una lista de condiciones de tarifa enviada en formato XML. Aplica una lógica de prioridad en cascada: primero busca condiciones donde tanto la unidad funcional como el tipo de unidad coincidan exactamente, luego donde solo coincide la unidad funcional pero el tipo de unidad difiere, luego donde solo coincide el tipo de unidad pero la unidad funcional difiere, y finalmente donde ninguna coincide (condición genérica). Retorna la condición seleccionada con sus parámetros de liquidación: tipo de liquidación, tipo de manual tarifario, vigencia del manual, variación de tarifa, valor de venta y valor de venta con recargo. Se usa en el proceso de facturación y liquidación de servicios de salud para determinar qué tarifa contractual aplica según la unidad funcional que presta el servicio.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByFunctionalUnitUnitType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByFunctionalUnitUnitType';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona, desde una lista XML de condiciones tarifarias, la condición aplicable a una unidad funcional y tipo de unidad, según una jerarquía de coincidencia exacta o exclusión.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /RateConditionList con los nodos esperados (Id, Operator, Operator2, FunctionalUnitId, UnitTypeId2, etc.).; Operator y Operator2 deben tomar valores 1 (igualdad) o 2 (desigualdad) para que alguna rama aplique.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las ramas se evalúan con prioridad estricta: igual/igual, igual/distinto, distinto/igual, distinto/distinto; solo una se ejecuta.; Operator=1 se interpreta como igualdad y Operator=2 como desigualdad respecto a los parámetros.; La tabla retornada contiene a lo sumo una fila (uso de TOP 1).; Si ninguna rama tiene coincidencias, la tabla retornada queda vacía.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Condición tarifaria (RateCondition); Unidad funcional (FunctionalUnit); Tipo de unidad (UnitType); Tipo de liquidación (LiquidationType); Manual tarifario (RateManual / RateManualValidity); Variación tarifaria (RateVariation); Valor de venta (SalesValue) y valor de venta con recargo (SalesValueWithSurcharge)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Si existe alguna condición con Operator=1 y Operator2=1 cuya FunctionalUnitId coincide con el parámetro y UnitTypeId2 coincide con el tipo de unidad, se inserta el TOP 1 de esas filas.; [INSERT] @DefinitionRateDetailCondition: Si no se cumple la rama anterior y existe alguna condición con Operator=1 y Operator2=2 con FunctionalUnitId igual al parámetro pero UnitTypeId2 distinto, se inserta el TOP 1 de esas filas.; [INSERT] @DefinitionRateDetailCondition: Si no se cumplen las anteriores y existe alguna condición con Operator=2 y Operator2=1 con FunctionalUnitId distinto y UnitTypeId2 igual, se inserta el TOP 1 de esas filas.; [INSERT] @DefinitionRateDetailCondition: Si no se cumple ninguna anterior y existe alguna condición con Operator=2 y Operator2=2 con FunctionalUnitId distinto y UnitTypeId2 distinto, se inserta el TOP 1 de esas filas.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe condición con Operator=1, Operator2=1, FunctionalUnitId = parámetro y UnitTypeId2 = tipo de unidad → Devuelve la primera condición que coincide exactamente en unidad funcional y tipo de unidad else Evalúa la siguiente rama; si Existe condición con Operator=1, Operator2=2, FunctionalUnitId = parámetro y UnitTypeId2 <> tipo de unidad → Devuelve la primera condición que coincide en unidad funcional pero excluye el tipo de unidad else Evalúa la siguiente rama; si Existe condición con Operator=2, Operator2=1, FunctionalUnitId <> parámetro y UnitTypeId2 = tipo de unidad → Devuelve la primera condición que excluye la unidad funcional y coincide en tipo de unidad else Evalúa la siguiente rama; si Existe condición con Operator=2, Operator2=2, FunctionalUnitId <> parámetro y UnitTypeId2 <> tipo de unidad → Devuelve la primera condición que excluye tanto la unidad funcional como el tipo de unidad else Devuelve tabla vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitUnitType';
GO
