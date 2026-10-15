-- =============================================
-- Author:		Diego A. Roldan Lozano
-- Create date:	2025-07-06
-- Description:	Calcula la distribución secundaria a partir de un Elemento de Distribución Intermedia
-- =============================================
CREATE PROCEDURE [Cost].[SP_CalculateIntermediateDistribution]
	@CostIntermediateDistributionId INT,
	@Year INT,
	@Month INT
AS
BEGIN
	SET NOCOUNT ON;

	/***************************************************** RESULTADO *****************************************************/

	SELECT ProductionCenterId, ProductionCenterCodeName, SUM(Percentage) Percentage, SUM(Value) Value
	FROM [Cost].[GetCalculateDistributionIntermediate](@Year, @Month, @CostIntermediateDistributionId, 0, 0)
	WHERE Value <> 0
	GROUP BY ProductionCenterId, ProductionCenterCodeName
END