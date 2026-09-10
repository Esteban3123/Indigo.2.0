CREATE Function [Contract].[GetConditionBySpecialtyRIAS]
(
	@SpecialtyId Char(3),
	@RiasId int,
	@rateConditionListXml Xml
)
Returns @DefinitionRateDetailCondition Table
(
	Id Int Primary Key,
	LiquidationType Tinyint Null,
	ManualType Tinyint Null,
	RateManualId Int Null,
	RateManualValidityId INT NULL,
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
		, RIASId2 Int Null)

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
		t.x.value('RIASId2[1]','Int')
	From @rateConditionListXml.nodes('/RateConditionList') t(x)
		
	If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And SpecialtyId = @SpecialtyId And RIASId2 = @RiasId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And SpecialtyId = @SpecialtyId And RIASId2 = @RiasId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And SpecialtyId = @SpecialtyId And RIASId2 <> @RiasId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And SpecialtyId = @SpecialtyId And RIASId2 <> @RiasId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And SpecialtyId <> @SpecialtyId And RIASId2 = @RiasId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And SpecialtyId <> @SpecialtyId And RIASId2 = @RiasId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And SpecialtyId <> @SpecialtyId And RIASId2 <> @RiasId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And SpecialtyId <> @SpecialtyId And RIASId2 <> @RiasId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a un servicio según la especialidad médica y la RIAS (Ruta Integral de Atención en Salud) informadas. Recibe como entrada un XML con una lista de condiciones tarifarias definidas en el contrato, y evalúa por prioridad cuál condición coincide con la combinación de especialidad y RIAS suministradas, usando operadores de igualdad o diferencia para cada criterio. Retorna la condición ganadora con su tipo de liquidación, tipo de manual tarifario, vigencia del manual, variación de tarifa y valor de venta (con y sin recargo), permitiendo así calcular el precio correcto al facturar o liquidar un servicio dentro de una RIAS en un contrato de salud.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionBySpecialtyRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionBySpecialtyRIAS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona la condición tarifaria aplicable según especialidad y RIAS, evaluando jerárquicamente coincidencias exactas y luego diferencias mediante operadores de comparación.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe tener estructura con nodos /RateConditionList y elementos hijos esperados (Id, Operator, Operator2, SpecialtyId, RIASId2, etc.); Los valores de Operator y Operator2 deben ser 1 (igual) o 2 (distinto) para que aplique alguna rama', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna a lo sumo una condición tarifaria (TOP 1); La precedencia de evaluación es: (igual especialidad, igual RIAS) > (igual especialidad, distinto RIAS) > (distinta especialidad, igual RIAS) > (distinta especialidad, distinto RIAS); Operator=1 representa coincidencia/igualdad y Operator=2 representa diferencia respecto a especialidad; Operator2 aplica la misma semántica para RIAS; Si ninguna combinación de operadores coincide, la tabla resultante queda vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Especialidad; RIAS (Rutas Integrales de Atención en Salud); Manual tarifario; Tipo de liquidación; Variación tarifaria; Valor de venta; Recargo', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando existe registro con Operator=1, Operator2=1, SpecialtyId=@SpecialtyId y RIASId2=@RiasId, inserta el TOP 1 que cumple esa condición; [INSERT] @DefinitionRateDetailCondition: Cuando no hay match exacto pero existe Operator=1, Operator2=2, SpecialtyId=@SpecialtyId y RIASId2<>@RiasId, inserta el TOP 1 de esa coincidencia; [INSERT] @DefinitionRateDetailCondition: Cuando aplican Operator=2, Operator2=1, SpecialtyId<>@SpecialtyId y RIASId2=@RiasId, inserta el TOP 1 que cumple esa condición; [INSERT] @DefinitionRateDetailCondition: Cuando aplican Operator=2, Operator2=2, SpecialtyId<>@SpecialtyId y RIASId2<>@RiasId, inserta el TOP 1 que cumple esa condición; [RETURN_RESULT] @DefinitionRateDetailCondition: Retorna la tabla con cero o un registro según la primera rama que se cumpla en orden de prioridad', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe condición con Operator=1 y Operator2=1, especialidad igual a la solicitada y RIAS igual al solicitado → Devuelve la primera condición que coincide exactamente por especialidad y RIAS else Evalúa siguientes combinaciones de operadores; si Existe condición con Operator=1 y Operator2=2, especialidad igual y RIAS distinto → Devuelve la primera condición con especialidad coincidente y RIAS distinto else Evalúa siguiente combinación; si Existe condición con Operator=2 y Operator2=1, especialidad distinta y RIAS igual → Devuelve la primera condición con especialidad distinta y RIAS coincidente else Evalúa última combinación; si Existe condición con Operator=2 y Operator2=2, especialidad distinta y RIAS distinto → Devuelve la primera condición con ambos atributos distintos else Retorna tabla vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionBySpecialtyRIAS';
GO
