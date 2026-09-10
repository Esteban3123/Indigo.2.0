-- =============================================
-- Author:		Andrea Coqueco
-- Create date: 09/12/2022
-- Description:	Cálculo de la concentración de un medicamento a partir del tipo de formula (Peso, Volumen)
-- =============================================
CREATE Function [MixingStation].[CalculateConcentration]
(
	@FormulationType Tinyint,
	@Weight Decimal(18, 2),
	@Volume Decimal(18, 2)
)
Returns Decimal(18, 4)
As
Begin
	Declare @Concentration Decimal(18, 4) = 0
	
	SELECT @Concentration = CAST(CASE @FormulationType  -- 1 - Peso 2 - Volumen 3 - Peso - Volumen 4 - Unidad de administración
								WHEN 1 THEN @Weight
								WHEN 2 THEN @Volume
								WHEN 3 THEN IIF(@Volume > 0, @Weight/@Volume, 0)
								ELSE 0
							END AS DECIMAL(18,4))

	Return @Concentration
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la concentración de un medicamento preparado en la estación de mezclas, a partir del tipo de formulación farmacéutica y los valores de peso y volumen ingresados. Según el tipo de fórmula, retorna el peso (mg/g) si es formulación tipo Peso, el volumen (mL) si es tipo Volumen, o la relación peso/volumen (mg/mL) si es tipo Peso-Volumen; para otros tipos como unidad de administración devuelve cero. Se usa en el proceso de preparación y validación de mezclas magistrales y medicamentos de alto riesgo, garantizando que la concentración calculada sea coherente con el tipo de formulación prescrita.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'FUNCTION', @level1name = N'CalculateConcentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'FUNCTION', @level1name = N'CalculateConcentration';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula la concentración de un medicamento según el tipo de formulación, retornando peso, volumen, su división, o cero según el caso.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculateConcentration';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El tipo de formulación debe corresponder a uno de los valores codificados (1=Peso, 2=Volumen, 3=Peso-Volumen, 4=Unidad de administración); Para el tipo 3 (Peso-Volumen), el volumen debe ser mayor a cero para evitar división; si es 0 se retorna 0', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculateConcentration';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca produce división por cero: si el volumen es 0 en formulación Peso-Volumen retorna 0; El resultado siempre se entrega con precisión Decimal(18,4); Para tipos de formulación no soportados explícitamente, la concentración resultante es 0', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculateConcentration';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Concentración de medicamento; Tipo de formulación (Peso, Volumen, Peso-Volumen, Unidad de administración); Mezclas magistrales; Medicamentos de alto riesgo; Preparación y validación de mezclas', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculateConcentration';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de formulación = 1 (Peso) → Retorna el peso como concentración; si Tipo de formulación = 2 (Volumen) → Retorna el volumen como concentración; si Tipo de formulación = 3 (Peso-Volumen) → Retorna Peso/Volumen si Volumen > 0, en caso contrario retorna 0 else 0; si Cualquier otro tipo (incluida Unidad de administración = 4) → Retorna 0', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculateConcentration';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'CalculateConcentration';
GO
