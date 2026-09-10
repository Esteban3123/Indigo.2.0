CREATE Function [Contract].[GetConditionByFunctionalUnitSpecialty]
(
	@FunctionalUnitId Int,
	@SpecialtyId Char(3),
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
		, SalesValueWithSurcharge Decimal(18, 2) Null)

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
		t.x.value('SalesValueWithSurcharge[1]','Decimal(18, 2)')
	From @rateConditionListXml.nodes('/RateConditionList') t(x)
		
	If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And FunctionalUnitId = @FunctionalUnitId And SpecialtyId2 = @SpecialtyId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And FunctionalUnitId = @FunctionalUnitId And SpecialtyId2 = @SpecialtyId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And FunctionalUnitId = @FunctionalUnitId And SpecialtyId2 <> @SpecialtyId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And FunctionalUnitId = @FunctionalUnitId And SpecialtyId2 <> @SpecialtyId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And FunctionalUnitId <> @FunctionalUnitId And SpecialtyId2 = @SpecialtyId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And FunctionalUnitId <> @FunctionalUnitId And SpecialtyId2 = @SpecialtyId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And FunctionalUnitId <> @FunctionalUnitId And SpecialtyId2 <> @SpecialtyId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And FunctionalUnitId <> @FunctionalUnitId And SpecialtyId2 <> @SpecialtyId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que determina la condición tarifaria aplicable (tipo de liquidación, manual de tarifas, variación de tasa y valores de venta) para una combinación específica de unidad funcional y especialidad médica dentro de un contrato. Recibe como entrada un XML con la lista de condiciones tarifarias definidas en el contrato, los desempaqueta y aplica una lógica de prioridad en cascada: primero busca una condición que coincida exactamente tanto en unidad funcional como en especialidad, luego donde coincida la unidad pero la especialidad sea diferente, después donde la unidad sea diferente pero coincida la especialidad, y finalmente una condición genérica donde ambas sean diferentes. Se usa en el proceso de facturación y liquidación de servicios de salud para resolver qué tarifa contractual corresponde a un servicio prestado según el área y la especialidad involucrada.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByFunctionalUnitSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByFunctionalUnitSpecialty';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona, dentro de una lista de condiciones tarifarias provista por XML, la mejor coincidencia para una unidad funcional y especialidad dadas, aplicando un orden de prioridad según operadores de igualdad/desigualdad.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe respetar la estructura /RateConditionList con los nodos esperados (Id, Operator, Operator2, FunctionalUnitId, SpecialtyId2, etc.).; Los valores de Operator y Operator2 deben ser 1 (igual) o 2 (distinto) para que se aplique alguna rama.; El Id de cada condición en el XML debe ser único (clave primaria de la tabla intermedia).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna como máximo una fila (Top 1) según la primera combinación de operadores que tenga coincidencias.; El orden de prioridad de evaluación es: (1,1) coincidencia exacta, luego (1,2), luego (2,1) y por último (2,2).; Operator=1 representa igualdad sobre unidad funcional y Operator=2 representa desigualdad; análogamente Operator2 aplica sobre la especialidad.; Las condiciones evaluadas provienen exclusivamente del XML de entrada; no se consulta ninguna tabla persistente.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Unidad Funcional; Especialidad; Condición de tarifa; Manual tarifario; Tipo de liquidación; Variación de tarifa; Valor de venta; Recargo', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando existen condiciones con Operator=1 AND Operator2=1 AND FunctionalUnitId=@FunctionalUnitId AND SpecialtyId2=@SpecialtyId, se inserta el TOP 1 de esas condiciones.; [INSERT] @DefinitionRateDetailCondition: Si no aplica el caso anterior y existen condiciones con Operator=1 AND Operator2=2 AND FunctionalUnitId=@FunctionalUnitId AND SpecialtyId2<>@SpecialtyId, se inserta el TOP 1 de esas condiciones.; [INSERT] @DefinitionRateDetailCondition: Si no aplican los casos anteriores y existen condiciones con Operator=2 AND Operator2=1 AND FunctionalUnitId<>@FunctionalUnitId AND SpecialtyId2=@SpecialtyId, se inserta el TOP 1 de esas condiciones.; [INSERT] @DefinitionRateDetailCondition: Si no aplican los casos anteriores y existen condiciones con Operator=2 AND Operator2=2 AND FunctionalUnitId<>@FunctionalUnitId AND SpecialtyId2<>@SpecialtyId, se inserta el TOP 1 de esas condiciones.; [RETURN_RESULT] @DefinitionRateDetailCondition: Devuelve la tabla resultado, que contendrá 0 o 1 fila según la primera rama de prioridad que tenga coincidencias.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe condición con Operator=1 y Operator2=1 que coincida con la unidad funcional y la especialidad indicadas → Devuelve la primera condición que cumpla igualdad exacta de unidad funcional e igualdad de especialidad else Evalúa la siguiente combinación de operadores; si Existe condición con Operator=1 y Operator2=2 con unidad funcional igual y especialidad distinta → Devuelve la primera condición con unidad funcional igual y especialidad distinta a la indicada else Evalúa la siguiente combinación; si Existe condición con Operator=2 y Operator2=1 con unidad funcional distinta y especialidad igual → Devuelve la primera condición con unidad funcional distinta y especialidad igual a la indicada else Evalúa la siguiente combinación; si Existe condición con Operator=2 y Operator2=2 con unidad funcional distinta y especialidad distinta → Devuelve la primera condición con unidad funcional y especialidad distintas a las indicadas else No retorna ninguna fila', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByFunctionalUnitSpecialty';
GO
