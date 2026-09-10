CREATE Function [Contract].[GetConditionByDescriptionRIAS]
(
	@ContractDescriptionId Tinyint,
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
		, FunctionalUnitId2 Int Null
		, UnitTypeId Tinyint Null
		, LiquidationType Tinyint Null
		, ManualType Tinyint NULL
		, RateManualValidityId INT NULL
		, RateManualId Int Null
		, RateVariation Decimal(5, 2) Null
		, SalesValue Decimal(18, 2) Null
		, SalesValueWithSurcharge Decimal(18, 2) Null
		, RIASId2 Int Null
		, ContractDescriptionId int null)

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
		t.x.value('SalesValueWithSurcharge[1]','Decimal(18, 2)'),
		t.x.value('RIASId2[1]','Int'),
		t.x.value('ContractDescriptionId[1]','Int')
	From @rateConditionListXml.nodes('/RateConditionList') t(x)
		
	If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And ContractDescriptionId = @ContractDescriptionId And RIASId2 = @RiasId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And ContractDescriptionId = @ContractDescriptionId And RIASId2 = @RiasId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And ContractDescriptionId = @ContractDescriptionId And RIASId2 <> @RiasId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And ContractDescriptionId = @ContractDescriptionId And RIASId2 <> @RiasId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And ContractDescriptionId <> @ContractDescriptionId And RIASId2 = @RiasId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And ContractDescriptionId <> @ContractDescriptionId And RIASId2 = @RiasId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And ContractDescriptionId <> @ContractDescriptionId And RIASId2 <> @RiasId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And ContractDescriptionId <> @ContractDescriptionId And RIASId2 <> @RiasId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contratos que determina la condición tarifaria aplicable a una descripción de contrato y una RIAS (Ruta Integral de Atención en Salud) específicas, evaluando una lista de condiciones de tarifa enviada en formato XML. Recibe el identificador de la descripción del contrato, el código de la RIAS y una lista de reglas tarifarias con sus operadores de coincidencia, y aplica una lógica de prioridad: primero busca la condición que coincida exactamente con la descripción y la RIAS, luego condiciones con coincidencia parcial en distintas combinaciones. Retorna la condición ganadora con sus datos de liquidación: tipo de liquidación, tipo de manual, vigencia del manual tarifario, variación de tarifa, valor de venta y valor de venta con recargo. Se usa en el proceso de facturación y tarifación de servicios asociados a rutas de atención (RIAS) dentro de los contratos con aseguradoras o pagadores.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByDescriptionRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByDescriptionRIAS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona la condición tarifaria aplicable según la combinación de operadores que define si la descripción contractual y el RIAS deben coincidir o diferir respecto a los valores recibidos.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones debe seguir la estructura /RateConditionList con los nodos esperados (Id, Operator, Operator2, ContractDescriptionId, RIASId2, etc.).; Los operadores válidos son 1 (igualdad) y 2 (diferencia).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se devuelve a lo sumo una condición (TOP 1).; El orden de prioridad es: ambos coincidentes > descripción coincidente y RIAS distinto > descripción distinta y RIAS coincidente > ambos distintos.; Operator=1 se interpreta como ''igualdad'' y Operator=2 como ''diferencia'' respecto al parámetro correspondiente.; Solo se evalúa una rama; las restantes se descartan al cumplirse la primera con coincidencias.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Condición tarifaria; Descripción de contrato; RIAS (Rutas Integrales de Atención en Salud); Manual tarifario; Tipo de liquidación; Variación tarifaria; Valor de venta; Valor de venta con recargo', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando existe condición con Operator=1 y Operator2=1, ContractDescriptionId igual al recibido y RIASId2 igual al RIAS recibido, se inserta el TOP 1 que cumpla esas condiciones.; [INSERT] @DefinitionRateDetailCondition: Si no aplica el caso anterior y existe condición con Operator=1, Operator2=2, ContractDescriptionId igual al recibido y RIASId2 distinto del RIAS recibido, se inserta el TOP 1 correspondiente.; [INSERT] @DefinitionRateDetailCondition: Si no aplican los anteriores y existe condición con Operator=2, Operator2=1, ContractDescriptionId distinto al recibido y RIASId2 igual al RIAS recibido, se inserta el TOP 1 correspondiente.; [INSERT] @DefinitionRateDetailCondition: Si no aplica ninguno de los previos y existe condición con Operator=2, Operator2=2, ContractDescriptionId distinto al recibido y RIASId2 distinto del RIAS recibido, se inserta el TOP 1 correspondiente.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Operator=1 AND Operator2=1 AND ContractDescriptionId=@ContractDescriptionId AND RIASId2=@RiasId → Selecciona la condición con coincidencia exacta de descripción contractual y RIAS. else Evalúa la siguiente combinación de operadores.; si Operator=1 AND Operator2=2 AND ContractDescriptionId=@ContractDescriptionId AND RIASId2<>@RiasId → Selecciona la condición con descripción contractual coincidente pero RIAS distinto. else Evalúa la siguiente combinación.; si Operator=2 AND Operator2=1 AND ContractDescriptionId<>@ContractDescriptionId AND RIASId2=@RiasId → Selecciona la condición con descripción contractual distinta pero RIAS coincidente. else Evalúa la siguiente combinación.; si Operator=2 AND Operator2=2 AND ContractDescriptionId<>@ContractDescriptionId AND RIASId2<>@RiasId → Selecciona la condición con descripción contractual y RIAS distintos. else Retorna tabla vacía.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByDescriptionRIAS';
GO
