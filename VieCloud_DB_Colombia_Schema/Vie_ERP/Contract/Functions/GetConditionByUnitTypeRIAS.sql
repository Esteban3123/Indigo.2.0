CREATE Function [Contract].[GetConditionByUnitTypeRIAS]
(
	@UnitType Tinyint,
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
		, RIASId2 Int Null)

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
		t.x.value('RIASId2[1]','Int')
	From @rateConditionListXml.nodes('/RateConditionList') t(x)
		
	If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And UnitTypeId = @UnitType And RIASId2 = @RiasId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And UnitTypeId = @UnitType And RIASId2 = @RiasId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And UnitTypeId = @UnitType And RIASId2 <> @RiasId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And UnitTypeId = @UnitType And RIASId2 <> @RiasId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And UnitTypeId <> @UnitType And RIASId2 = @RiasId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And UnitTypeId <> @UnitType And RIASId2 = @RiasId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And UnitTypeId <> @UnitType And RIASId2 <> @RiasId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And UnitTypeId <> @UnitType And RIASId2 <> @RiasId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a un servicio según el tipo de unidad funcional y el código RIAS (Rutas Integrales de Atención en Salud). Recibe como entrada un XML con una lista de condiciones tarifarias definidas en el contrato, el tipo de unidad y el identificador RIAS, y aplica una lógica de prioridad en cascada para seleccionar la condición más específica que coincida: primero busca coincidencia exacta en tipo de unidad Y RIAS, luego coincidencia en unidad con RIAS diferente, luego RIAS exacto con unidad diferente, y finalmente condición genérica donde ambos difieren. Retorna la condición tarifaria seleccionada con su tipo de liquidación, tipo de manual tarifario, vigencia, variación de tarifa y valores de venta (con y sin recargo), para ser usada en la facturación y liquidación de servicios de salud bajo contratos RIAS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByUnitTypeRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByUnitTypeRIAS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona, de una lista de condiciones tarifarias en XML, la condición aplicable según el tipo de unidad funcional y el RIAS, priorizando coincidencias exactas sobre exclusiones.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe respetar la estructura /RateConditionList con los nodos esperados (Id, Operator, Operator2, UnitTypeId, RIASId2, etc.); Los Id dentro del XML deben ser únicos (clave primaria de la tabla temporal); Operator y Operator2 deben tomar los valores 1 (igualdad) o 2 (desigualdad) para que alguna rama aplique', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna a lo sumo una condición (TOP 1) por invocación; La selección de condiciones sigue una jerarquía estricta de prioridad: (=,=) > (=,<>) > (<>,=) > (<>,<>) respecto a UnitType y RIAS; Operator=1 representa igualdad y Operator=2 representa desigualdad sobre UnitType; Operator2=1/2 aplica la misma semántica sobre RIAS; Si ninguna combinación de operadores coincide, la tabla resultante queda vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tarifa (rate); Manual tarifario; Vigencia de manual tarifario; Tipo de unidad funcional; RIAS (Rutas Integrales de Atención en Salud); Liquidación; Variación tarifaria; Valor de venta; Recargo (surcharge)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando hay condiciones con Operator=1, Operator2=1, UnitTypeId=@UnitType y RIASId2=@RiasId, inserta la primera (TOP 1) en la tabla de retorno; [INSERT] @DefinitionRateDetailCondition: Cuando no aplica la regla anterior pero existen condiciones con Operator=1, Operator2=2, UnitTypeId=@UnitType y RIASId2<>@RiasId, inserta la primera en la tabla de retorno; [INSERT] @DefinitionRateDetailCondition: Cuando no aplican las anteriores pero existen condiciones con Operator=2, Operator2=1, UnitTypeId<>@UnitType y RIASId2=@RiasId, inserta la primera en la tabla de retorno; [INSERT] @DefinitionRateDetailCondition: Cuando no aplican las anteriores pero existen condiciones con Operator=2, Operator2=2, UnitTypeId<>@UnitType y RIASId2<>@RiasId, inserta la primera en la tabla de retorno; [RETURN_RESULT] @DefinitionRateDetailCondition: Retorna la tabla con a lo sumo una condición tarifaria seleccionada según la jerarquía de operadores; vacía si ninguna coincide', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe condición con Operator=1 y Operator2=1, UnitType igual al solicitado y RIAS igual al solicitado → Devuelve la primera condición que coincide igualdad/igualdad (match exacto en tipo de unidad y RIAS) else Evalúa siguiente combinación de operadores; si Existe condición con Operator=1 y Operator2=2, UnitType igual y RIAS distinto al solicitado → Devuelve la primera condición que coincide igualdad de unidad y diferencia de RIAS else Evalúa siguiente combinación; si Existe condición con Operator=2 y Operator2=1, UnitType distinto y RIAS igual al solicitado → Devuelve la primera condición que coincide diferencia de unidad e igualdad de RIAS else Evalúa siguiente combinación; si Existe condición con Operator=2 y Operator2=2, UnitType distinto y RIAS distinto al solicitado → Devuelve la primera condición que coincide diferencia/diferencia else Retorna tabla vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByUnitTypeRIAS';
GO
