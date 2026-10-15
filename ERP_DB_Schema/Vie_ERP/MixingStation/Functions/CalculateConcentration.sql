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