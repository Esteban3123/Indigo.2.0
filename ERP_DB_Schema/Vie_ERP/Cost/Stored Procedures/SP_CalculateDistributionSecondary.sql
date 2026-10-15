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