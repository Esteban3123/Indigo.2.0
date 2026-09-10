CREATE Function [Contract].[GetConditionByRIASSpecialty]
(
	@RiasId Int,
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
		, SalesValueWithSurcharge Decimal(18, 2) Null
		, RIASId Int Null)

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
		t.x.value('RIASId[1]','Int')
	From @rateConditionListXml.nodes('/RateConditionList') t(x)
		
	If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And RIASId = @RiasId And SpecialtyId2 = @SpecialtyId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 1 And RIASId = @RiasId And SpecialtyId2 = @SpecialtyId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And RIASId = @RiasId And SpecialtyId2 <> @SpecialtyId) > 0 Begin

		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 1 And Operator2 = 2 And RIASId = @RiasId And SpecialtyId2 <> @SpecialtyId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And RIASId <> @RiasId And SpecialtyId2 = @SpecialtyId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 1 And RIASId <> @RiasId And SpecialtyId2 = @SpecialtyId

	End
	Else If (Select Count(*)
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And RIASId <> @RiasId And SpecialtyId2 <> @SpecialtyId) > 0 Begin
							
		Insert Into @DefinitionRateDetailCondition
		Select Top 1 Id, LiquidationType, ManualType, RateManualValidityId, RateManualId, RateVariation, SalesValue, SalesValueWithSurcharge 
		From @rateConditionList
		Where Operator = 2 And Operator2 = 2 And RIASId <> @RiasId And SpecialtyId2 <> @SpecialtyId

	End

	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de contrato que determina la condición tarifaria aplicable a un servicio según la RIAS (Ruta Integral de Atención en Salud) y la especialidad médica indicadas. Recibe un listado de condiciones tarifarias en formato XML, lo descompone y evalúa por orden de prioridad: primero busca una condición que coincida exactamente con la RIAS y la especialidad; si no encuentra, aplica condiciones con combinaciones de igualdad o diferencia entre la RIAS y la especialidad recibidas. Retorna la condición tarifaria ganadora con su tipo de liquidación, tipo de manual, vigencia del manual de tarifas, variación porcentual, valor de venta y valor con recargo. Se usa en la liquidación de contratos para resolver qué tarifa cobrar cuando un servicio pertenece a una RIAS y es atendido por una especialidad determinada.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByRIASSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetConditionByRIASSpecialty';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona, dentro de una lista de condiciones tarifarias en XML, la condición aplicable según coincidencia o diferencia de la RIAS y la especialidad recibidas, siguiendo un orden de prioridad para resolver la tarifa a cobrar.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de condiciones tarifarias debe respetar la estructura /RateConditionList con los nodos esperados (Id, Operator, Operator2, RIASId, SpecialtyId2, etc.); Cada condición debe traer valores válidos en Operator y Operator2 (1=igualdad, 2=diferencia) para ser considerada; El Id de cada condición debe ser único (clave primaria de la tabla temporal)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se evalúan las combinaciones de operadores en orden de prioridad estricto: (=RIAS,=Especialidad) > (=RIAS,≠Especialidad) > (≠RIAS,=Especialidad) > (≠RIAS,≠Especialidad); Solo se retorna a lo sumo una condición tarifaria (TOP 1); Operator=1 representa igualdad y Operator=2 representa diferencia respecto al valor recibido; Operator aplica sobre RIAS y Operator2 aplica sobre Especialidad; Si ninguna combinación tiene coincidencias, se retorna la tabla vacía sin error', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIAS (Rutas Integrales de Atención en Salud); Especialidad; Tarifa / Condición tarifaria; Manual de tarifas y vigencia; Tipo de liquidación; Variación tarifaria; Valor de venta y valor con recargo; Liquidación de contratos', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @DefinitionRateDetailCondition: Cuando existe condición con Operator=1, Operator2=1, RIASId=@RiasId y SpecialtyId2=@SpecialtyId, retorna TOP 1 de esa coincidencia; [RETURN_RESULT] @DefinitionRateDetailCondition: Cuando no aplica la primera regla y existe condición con Operator=1, Operator2=2, RIASId=@RiasId y SpecialtyId2<>@SpecialtyId, retorna TOP 1 de esa coincidencia; [RETURN_RESULT] @DefinitionRateDetailCondition: Cuando no aplican las anteriores y existe condición con Operator=2, Operator2=1, RIASId<>@RiasId y SpecialtyId2=@SpecialtyId, retorna TOP 1 de esa coincidencia; [RETURN_RESULT] @DefinitionRateDetailCondition: Cuando no aplican las anteriores y existe condición con Operator=2, Operator2=2, RIASId<>@RiasId y SpecialtyId2<>@SpecialtyId, retorna TOP 1 de esa coincidencia', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe condición con Operator=1 y Operator2=1 cuyo RIAS coincide con el recibido y la especialidad coincide con la recibida → Selecciona la primera coincidencia (igualdad RIAS e igualdad especialidad) else Evalúa la siguiente combinación; si Existe condición con Operator=1 y Operator2=2 cuyo RIAS coincide y la especialidad es distinta a la recibida → Selecciona la primera coincidencia (igualdad RIAS y diferencia de especialidad) else Evalúa la siguiente combinación; si Existe condición con Operator=2 y Operator2=1 cuyo RIAS es distinto al recibido y la especialidad coincide → Selecciona la primera coincidencia (diferencia RIAS e igualdad especialidad) else Evalúa la siguiente combinación; si Existe condición con Operator=2 y Operator2=2 cuyo RIAS es distinto y la especialidad es distinta a los recibidos → Selecciona la primera coincidencia (diferencia RIAS y diferencia de especialidad) else Retorna tabla vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASSpecialty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetConditionByRIASSpecialty';
GO
