CREATE Function [Contract].[GetConditionBySpecialtyDescription]
(
	@SpecialtyId Char(3),
	@ContractDescriptionId int,
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
		, UnitTypeId2 Tinyint Null
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
		t.x.value('SpecialtyId[1]','Char(3)'),
		t.x.value('UnitTypeId2[1]','Tinyint'),
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
		Where Operator = 1 And Operator2 = 1 And SpecialtyId = @SpecialtyId And ContractDescriptionId2 = @ContractDescriptionId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And SpecialtyId = @SpecialtyId And ContractDescriptionId2 = @ContractDescriptionId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And SpecialtyId = @SpecialtyId And ContractDescriptionId2 <> @ContractDescriptionId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And SpecialtyId = @SpecialtyId And ContractDescriptionId2 <> @ContractDescriptionId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And SpecialtyId <> @SpecialtyId And ContractDescriptionId2 = @ContractDescriptionId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And SpecialtyId <> @SpecialtyId And ContractDescriptionId2 = @ContractDescriptionId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And SpecialtyId <> @SpecialtyId And ContractDescriptionId2 <> @ContractDescriptionId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And SpecialtyId <> @SpecialtyId And ContractDescriptionId2 <> @ContractDescriptionId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina las condiciones tarifarias aplicables a un ítem de contrato según la especialidad médica y la descripción del contrato. Recibe como entrada un XML con una lista de condiciones tarifarias candidatas (cada una con operadores de comparación, especialidad, tipo de liquidación, manual de tarifas y valores de venta), y aplica una lógica de prioridad en cascada: primero busca una condición donde tanto la especialidad como la descripción del contrato coincidan exactamente; si no encuentra, prueba combinaciones parciales (especialidad igual y descripción diferente, o viceversa), y finalmente acepta una condición donde ninguna de las dos coincida. Retorna la condición ganadora con su tipo de liquidación, tipo de manual, vigencia del manual de tarifas, variación porcentual de tarifa y valor de venta con y sin recargo. Se usa en el proceso de liquidación contractual para resolver qué tarifa aplicar a un servicio prestado por una especialidad médica bajo un ítem específico del contrato con el asegurador.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionBySpecialtyDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionBySpecialtyDescription';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona, a partir de una lista XML de condiciones de tarifa, la condición aplicable según coincidencia (igual/distinto) entre la especialidad y la descripción del contrato, devolviendo sus parámetros tarifarios.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /RateConditionList con los nodos esperados (Id, Operator, Operator2, SpecialtyId, ContractDescriptionId2, etc.); Cada condición debe tener Operator y Operator2 con valores 1 (igualdad) o 2 (desigualdad); Los Id de las condiciones deben ser únicos (clave primaria de la tabla retornada)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'A lo sumo se devuelve una fila (uso de TOP 1 y ramas mutuamente excluyentes con If/Else If).; Las ramas se evalúan en orden estricto de prioridad: (igual,igual) > (igual,distinto) > (distinto,igual) > (distinto,distinto).; Operator=1 representa igualdad y Operator=2 representa desigualdad respecto a especialidad y descripción del contrato.; Si ninguna rama encuentra coincidencias, la función retorna la tabla vacía.; Solo se propagan al resultado los atributos tarifarios (liquidación, manual, vigencia, variación, valores de venta), descartando Operator/Operator2/SpecialtyId/ContractDescriptionId2.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'especialidad médica; contrato con asegurador; tarifa; tipo de liquidación; manual de tarifas; vigencia de manual de tarifas; variación de tarifa; valor de venta; recargo', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando existe condición con Operator=1 y Operator2=1, SpecialtyId igual al parámetro y ContractDescriptionId2 igual al parámetro, se inserta el TOP 1 de esa coincidencia exacta.; [INSERT] @DefinitionRateDetailCondition: Si no hay coincidencia exacta y existe condición con Operator=1, Operator2=2, SpecialtyId igual y ContractDescriptionId2 distinto, se inserta el TOP 1 (especialidad coincide, descripción difiere).; [INSERT] @DefinitionRateDetailCondition: Si tampoco aplica la anterior y existe condición con Operator=2, Operator2=1, SpecialtyId distinto y ContractDescriptionId2 igual, se inserta el TOP 1 (descripción coincide, especialidad difiere).; [INSERT] @DefinitionRateDetailCondition: Si ninguna anterior aplica y existe condición con Operator=2 y Operator2=2, SpecialtyId distinto y ContractDescriptionId2 distinto, se inserta el TOP 1 (ninguna coincide).; [RETURN_RESULT] @DefinitionRateDetailCondition: Retorna la tabla con la condición ganadora (a lo sumo una fila) incluyendo LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue y SalesValueWithSurcharge.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe registro con Operator=1 AND Operator2=1 AND SpecialtyId=@SpecialtyId AND ContractDescriptionId2=@ContractDescriptionId → Se elige esa condición (match exacto especialidad+descripción) y se omiten las demás ramas. else Evalúa la siguiente prioridad.; si Existe registro con Operator=1 AND Operator2=2 AND SpecialtyId=@SpecialtyId AND ContractDescriptionId2<>@ContractDescriptionId → Se elige condición con especialidad igual y descripción distinta. else Evalúa la siguiente prioridad.; si Existe registro con Operator=2 AND Operator2=1 AND SpecialtyId<>@SpecialtyId AND ContractDescriptionId2=@ContractDescriptionId → Se elige condición con especialidad distinta y descripción igual. else Evalúa la última prioridad.; si Existe registro con Operator=2 AND Operator2=2 AND SpecialtyId<>@SpecialtyId AND ContractDescriptionId2<>@ContractDescriptionId → Se elige condición sin coincidencia en especialidad ni descripción. else No se inserta nada y se retorna tabla vacía.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyDescription';
GO
