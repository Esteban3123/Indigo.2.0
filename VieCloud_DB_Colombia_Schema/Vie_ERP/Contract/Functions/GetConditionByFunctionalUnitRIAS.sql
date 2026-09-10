CREATE Function [Contract].[GetConditionByFunctionalUnitRIAS]
(
	@FunctionalUnitId Int,
	@RiasId int,
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
		, RIASId2 Int Null)

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
		t.x.value('RIASId2[1]','Int')
	From @rateConditionListXml.nodes('/RateConditionList') t(x)
		
	If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And FunctionalUnitId = @FunctionalUnitId And RIASId2 = @RiasId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And FunctionalUnitId = @FunctionalUnitId And RIASId2 = @RiasId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And FunctionalUnitId = @FunctionalUnitId And RIASId2 <> @RiasId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And FunctionalUnitId = @FunctionalUnitId And RIASId2 <> @RiasId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And FunctionalUnitId <> @FunctionalUnitId And RIASId2 = @RiasId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And FunctionalUnitId <> @FunctionalUnitId And RIASId2 = @RiasId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And FunctionalUnitId <> @FunctionalUnitId And RIASId2 <> @RiasId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And FunctionalUnitId <> @FunctionalUnitId And RIASId2 <> @RiasId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a un servicio RIAS (Rutas Integrales de Atención en Salud) según la unidad funcional de atención. Recibe como entrada un XML con la lista de condiciones de tarifa definidas en el contrato (tarifas manuales, variaciones, valores de venta con y sin recargo) y, mediante una jerarquía de prioridad basada en operadores de coincidencia, selecciona la condición más específica que corresponde a la combinación unidad funcional / RIAS. Retorna el detalle de liquidación aplicable: tipo de liquidación, tipo de manual tarifario, vigencia, variación porcentual y valores de venta, siendo utilizada en el proceso de facturación y liquidación de servicios contratados bajo el modelo RIAS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByFunctionalUnitRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByFunctionalUnitRIAS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona, según una jerarquía de coincidencia entre unidad funcional y RIAS, una única condición tarifaria desde una lista XML para aplicar al cálculo de tarifas.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones debe respetar la estructura /RateConditionList con los nodos esperados (Id, Operator, Operator2, FunctionalUnitId, RIASId2, etc.); Cada condición debe traer valores válidos en Operator (1 o 2) y Operator2 (1 o 2) para poder ser seleccionada', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna como máximo una condición (TOP 1) por invocación; La selección sigue una jerarquía estricta de prioridad: coincidencia total > coincidencia solo en unidad funcional > coincidencia solo en RIAS > sin coincidencia; Operator=1 representa igualdad respecto a la unidad funcional; Operator=2 representa diferencia; Operator2=1 representa igualdad respecto al RIAS; Operator2=2 representa diferencia; Si ninguna condición del XML cumple alguno de los cuatro patrones evaluados, el resultado es vacío', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Unidad funcional; RIAS (Rutas Integrales de Atención en Salud); Manual tarifario; Tipo de liquidación; Variación tarifaria; Valor de venta; Valor de venta con recargo; Vigencia de manual tarifario', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando existe registro con Operator=1, Operator2=1, FunctionalUnitId igual al parámetro y RIASId2 igual al parámetro, inserta el TOP 1 de esa coincidencia exacta; [INSERT] @DefinitionRateDetailCondition: Cuando no hay coincidencia exacta pero existe registro con Operator=1, Operator2=2, FunctionalUnitId igual y RIASId2 distinto, inserta el TOP 1 de esa coincidencia parcial por unidad funcional; [INSERT] @DefinitionRateDetailCondition: Cuando no aplican los casos previos pero existe registro con Operator=2, Operator2=1, FunctionalUnitId distinto y RIASId2 igual, inserta el TOP 1 de esa coincidencia parcial por RIAS; [INSERT] @DefinitionRateDetailCondition: Cuando no aplican los casos previos pero existe registro con Operator=2, Operator2=2, FunctionalUnitId distinto y RIASId2 distinto, inserta el TOP 1 como condición por defecto; [RETURN_RESULT] @DefinitionRateDetailCondition: Retorna la tabla con a lo sumo una fila correspondiente al primer nivel de prioridad satisfecho, o vacía si ninguno aplica', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe condición con Operator=1 y Operator2=1 cuya unidad funcional coincide con la solicitada y cuyo RIAS coincide con el solicitado → Devuelve la primera condición que cumple coincidencia exacta de unidad funcional y RIAS else Evalúa siguiente nivel de prioridad; si Existe condición con Operator=1 y Operator2=2 cuya unidad funcional coincide y cuyo RIAS es distinto al solicitado → Devuelve la primera condición que coincide en unidad funcional pero difiere en RIAS else Evalúa siguiente nivel de prioridad; si Existe condición con Operator=2 y Operator2=1 cuya unidad funcional es distinta y cuyo RIAS coincide con el solicitado → Devuelve la primera condición que difiere en unidad funcional pero coincide en RIAS else Evalúa siguiente nivel de prioridad; si Existe condición con Operator=2 y Operator2=2 cuya unidad funcional es distinta y cuyo RIAS es distinto al solicitado → Devuelve la primera condición que difiere en ambos criterios else Devuelve tabla vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitRIAS';
GO
