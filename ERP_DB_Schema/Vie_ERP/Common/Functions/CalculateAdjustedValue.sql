-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-04
-- Description:	Calcular el valor a ajustar
-- =============================================
CREATE FUNCTION [Common].[CalculateAdjustedValue]
(
	@Difference DECIMAL(20,4),	
	@ValueIfDifferenceNegative DECIMAL(20,4),
	@ValueIfDifferencePositive DECIMAL(20,4)
)
RETURNS DECIMAL(20,4)
AS
BEGIN
	DECLARE @AdjustedValue AS DECIMAL(20,4)

	SET @AdjustedValue = IIF
	(
		@Difference = 0, 0,
		IIF
		(
			@Difference > 0,
			IIF -- Positiva
			(
				@ValueIfDifferencePositive = 0,
				0,
				IIF
				(
					@Difference > @ValueIfDifferencePositive, 
					@ValueIfDifferencePositive, 
					@Difference
				)
			),
			IIF -- Negativa
			(
				@ValueIfDifferencePositive < 0, 
				IIF
				(
					@ValueIfDifferencePositive = 0,
					0,
					IIF
					(
						@Difference > @ValueIfDifferencePositive, 							
						@Difference,
						@ValueIfDifferencePositive
					)
				), 
				IIF
				(
					@ValueIfDifferenceNegative = 0,
					0,
					IIF
					(	
						@ValueIfDifferenceNegative < 0, 
						IIF
						(
							@Difference > @ValueIfDifferenceNegative, 							
							@Difference,
							@ValueIfDifferenceNegative
						) * -1, 
						@Difference
					)
				)
			)
		)
	)

	RETURN @AdjustedValue
END