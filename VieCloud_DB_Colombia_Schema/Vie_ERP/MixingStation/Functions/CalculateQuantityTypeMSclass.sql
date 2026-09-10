CREATE Function [MixingStation].[CalculateQuantityTypeMSclass]
(
	@Quantity DECIMAL(18,2),
	@MSclass INT,
	@Weight NUMERIC(18,2)
)
Returns Decimal(18, 2)
As
Begin
	Declare @Result Decimal(18, 2) = 0
	---Calcula la cantidad de tipo de dosis unitaria de tipo reempaque o reenvase
	set @Result = IIF(@MSclass = 5 or @MSclass = 7, @Quantity/@Weight, @Quantity)
	Return @Result
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la cantidad ajustada de una dosis unitaria según el tipo de clase de estación de mezclas (MSclass). Si la clase corresponde a reempaque (5) o reenvase (7), divide la cantidad entre el peso para obtener el número de unidades resultantes; de lo contrario, devuelve la cantidad tal como fue ingresada. Se utiliza en el proceso de preparación y dispensación de medicamentos en la estación de mezclas para determinar correctamente cuántas unidades se generan a partir de un insumo o medicamento reconformado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'FUNCTION', @level1name = N'CalculateQuantityTypeMSclass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'FUNCTION', @level1name = N'CalculateQuantityTypeMSclass';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula la cantidad efectiva de unidades a generar en la estación de mezclas, dividiendo cantidad sobre peso cuando se trata de reempaque/reenvase (clases 5 o 7) y dejándola igual en otros casos.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculateQuantityTypeMSclass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@Weight debe ser distinto de cero cuando @MSclass = 5 o @MSclass = 7 para evitar división por cero.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculateQuantityTypeMSclass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo las clases de estación de mezclas 5 y 7 aplican el factor de conversión por peso; cualquier otra clase preserva la cantidad original.; El resultado siempre se entrega como DECIMAL(18,2).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculateQuantityTypeMSclass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'dosis unitaria; reempaque; reenvase; estación de mezclas; preparación y dispensación de medicamentos', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculateQuantityTypeMSclass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Cuando @MSclass IN (5,7) retorna @Quantity/@Weight; en otro caso retorna @Quantity.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculateQuantityTypeMSclass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @MSclass = 5 OR @MSclass = 7 → Devuelve @Quantity / @Weight (cantidad ajustada por peso unitario, propia de reempaque/reenvase) else Devuelve @Quantity sin transformación', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculateQuantityTypeMSclass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculateQuantityTypeMSclass';
GO
