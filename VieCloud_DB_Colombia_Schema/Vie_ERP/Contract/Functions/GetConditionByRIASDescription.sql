CREATE Function [Contract].[GetConditionByRIASDescription]
(
	@RiasId Int,
	@ContractDescriptionId Char(3),
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
		, FunctionalUnitId Int
		, SpecialtyId2 Char(3) Null
		, LiquidationType Tinyint Null
		, ManualType Tinyint NULL
		, RateManualValidityId INT NULL
		, RateManualId Int Null
		, RateVariation Decimal(5, 2) Null
		, SalesValue Decimal(18, 2) Null
		, SalesValueWithSurcharge Decimal(18, 2) Null
		, RIASId Int Null
		, ContractDescriptionId2 int null)

	Insert Into @rateConditionList
	Select t.x.value('Id[1]','Int'),
		t.x.value('Operator[1]','Tinyint'),
		t.x.value('Operator2[1]','Tinyint'),
		t.x.value('FunctionalUnitId[1]','Int'),
		t.x.value('SpecialtyId2[1]','Char(3)'),
		t.x.value('LiquidationType[1]','Tinyint'),
		t.x.value('ManualType[1]','Tinyint'),
		t.x.value('RateManualValidityId[1]','Int'),
		t.x.value('RateManualId[1]','Int'),
		t.x.value('RateVariation[1]','Decimal(5, 2)'),
		t.x.value('SalesValue[1]','Decimal(18, 2)'),
		t.x.value('SalesValueWithSurcharge[1]','Decimal(18, 2)'),
		t.x.value('RIASId[1]','Int'),
		t.x.value('ContractDescriptionId2[1]','Int')
	From @rateConditionListXml.nodes('/RateConditionList') t(x)
		
	If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And RIASId = @RiasId And ContractDescriptionId2 = @ContractDescriptionId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And RIASId = @RiasId And ContractDescriptionId2 = @ContractDescriptionId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And RIASId = @RiasId And ContractDescriptionId2 <> @ContractDescriptionId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And RIASId = @RiasId And ContractDescriptionId2 <> @ContractDescriptionId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And RIASId <> @RiasId And ContractDescriptionId2 = @ContractDescriptionId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And RIASId <> @RiasId And ContractDescriptionId2 = @ContractDescriptionId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And RIASId <> @RiasId And ContractDescriptionId2 <> @ContractDescriptionId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And RIASId <> @RiasId And ContractDescriptionId2 <> @ContractDescriptionId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a un servicio según su RIAS (Rutas Integrales de Atención en Salud) y la descripción del contrato. Recibe como entrada el identificador del RIAS, el código de descripción del contrato y una lista de condiciones tarifarias en formato XML, la parsea y evalúa por orden de prioridad (RIAS exacto + descripción exacta, RIAS exacto + descripción diferente, RIAS diferente + descripción exacta, RIAS diferente + descripción diferente) para retornar la condición tarifaria más específica que coincida. Devuelve el detalle de liquidación: tipo de liquidación, tipo de manual, vigencia del manual de tarifas, variación porcentual de la tarifa, valor de venta y valor de venta con recargo, usada en la facturación y liquidación de contratos de prestación de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByRIASDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByRIASDescription';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona, a partir de una lista de condiciones tarifarias en XML, la primera condición aplicable según la combinación de operadores (igualdad/desigualdad) frente al RIAS y a la descripción de contrato dados.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones debe seguir la estructura /RateConditionList con los nodos esperados (Id, Operator, Operator2, RIASId, ContractDescriptionId2, etc.).; Operator y Operator2 deben usar los códigos 1 (igualdad) o 2 (desigualdad) para que alguna rama coincida.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Devuelve a lo sumo una fila (uso de TOP 1 en cada rama y ramas mutuamente excluyentes vía If/Else If).; La precedencia de selección es estricta: igualdad-igualdad > igualdad-desigualdad > desigualdad-igualdad > desigualdad-desigualdad.; Operator=1 se interpreta como ''igual a'' y Operator=2 como ''distinto de'' respecto a RIAS y descripción de contrato.; Si ninguna combinación de operadores se cumple, la tabla resultante queda vacía.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIAS (Rutas Integrales de Atención en Salud); Descripción de contrato; Condición tarifaria; Manual tarifario; Vigencia de manual tarifario; Tipo de liquidación; Variación tarifaria; Valor de venta con recargo', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando existe alguna condición con Operator=1 y Operator2=1, RIASId igual al parámetro y ContractDescriptionId2 igual al parámetro, se inserta el TOP 1 de esas coincidencias.; [INSERT] @DefinitionRateDetailCondition: Si la rama anterior no aplica y existe alguna con Operator=1, Operator2=2, RIASId igual y ContractDescriptionId2 distinto, se inserta el TOP 1 de esas coincidencias.; [INSERT] @DefinitionRateDetailCondition: Si las ramas anteriores no aplican y existe alguna con Operator=2, Operator2=1, RIASId distinto y ContractDescriptionId2 igual, se inserta el TOP 1 de esas coincidencias.; [INSERT] @DefinitionRateDetailCondition: Si ninguna rama anterior aplica y existe alguna con Operator=2, Operator2=2, RIASId distinto y ContractDescriptionId2 distinto, se inserta el TOP 1 de esas coincidencias.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe condición con Operator=1, Operator2=1, RIASId=@RiasId y ContractDescriptionId2=@ContractDescriptionId → Devuelve la primera condición que coincide exactamente en RIAS y descripción de contrato. else Evalúa la siguiente combinación de operadores.; si Existe condición con Operator=1, Operator2=2, RIASId=@RiasId y ContractDescriptionId2<>@ContractDescriptionId → Devuelve la primera condición con RIAS igual pero descripción de contrato diferente. else Evalúa la siguiente combinación.; si Existe condición con Operator=2, Operator2=1, RIASId<>@RiasId y ContractDescriptionId2=@ContractDescriptionId → Devuelve la primera condición con RIAS distinto pero descripción de contrato igual. else Evalúa la siguiente combinación.; si Existe condición con Operator=2, Operator2=2, RIASId<>@RiasId y ContractDescriptionId2<>@ContractDescriptionId → Devuelve la primera condición con RIAS y descripción de contrato distintos. else Retorna tabla vacía.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASDescription';
GO
