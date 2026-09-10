-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date:	2019-09-04
-- Description:	Calcula la distribución secundaria a partir de un Elemento de Distribución Secundaria
-- =============================================
CREATE PROCEDURE [Cost].[SP_CalculateDistributionSecondary]
	@CostDistributionSecondaryId INT,
	@Year INT,
	@Month INT
AS
BEGIN
	SET NOCOUNT ON;

	/***************************************************** RESULTADO *****************************************************/

	SELECT ProductionCenterId, ProductionCenterCodeName, SUM(Percentage) Percentage, SUM(Value) Value
	FROM [Cost].[GetCalculateDistributionSecondary](@Year, @Month, @CostDistributionSecondaryId, 0, 0)
	WHERE Value <> 0
	GROUP BY ProductionCenterId, ProductionCenterCodeName
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la distribución secundaria de costos para un período (año y mes) y un elemento de distribución secundaria específico. Utiliza la función [Cost].[GetCalculateDistributionSecondary] para obtener los valores y porcentajes de distribución por centro de producción, agrupando y sumando los resultados para presentar el total distribuido por cada centro. Se usa en el módulo de costos para determinar cómo se reparten los costos indirectos o secundarios entre los distintos centros de producción o unidades funcionales de la institución.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CalculateDistributionSecondary';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CalculateDistributionSecondary';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcular y consolidar por centro de producción la distribución secundaria de costos para un elemento, año y mes determinados, devolviendo solo los centros con valor distribuido distinto de cero.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un elemento de distribución secundaria identificado por el parámetro recibido.; Año y mes deben corresponder a un periodo válido reconocido por la función Cost.GetCalculateDistributionSecondary.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan centros de producción cuyo valor distribuido es distinto de cero (WHERE Value <> 0).; Los resultados se agregan (SUM) por centro de producción, consolidando porcentaje y valor.; El cálculo siempre se delega a la función Cost.GetCalculateDistributionSecondary parametrizada por año, mes y elemento de distribución.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución secundaria de costos; Centro de producción; Elemento de distribución secundaria', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Cost.GetCalculateDistributionSecondary: Devuelve un resultset agrupado por ProductionCenterId y ProductionCenterCodeName con la suma de Percentage y Value, filtrando filas donde Value <> 0.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.GetCalculateDistributionSecondary', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateDistributionSecondary';
-- GO
