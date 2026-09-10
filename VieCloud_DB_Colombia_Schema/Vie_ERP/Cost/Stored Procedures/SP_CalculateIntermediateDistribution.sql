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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la distribución intermedia (secundaria) de costos para un período específico (año y mes) a partir de un elemento de distribución intermedia identificado por su ID. Consolida los resultados agrupando por centro de producción, sumando el porcentaje y el valor distribuido, excluyendo registros con valor cero. Utiliza la función [Cost].[GetCalculateDistributionIntermediate] para obtener el detalle del reparto de costos indirectos entre centros de producción, y devuelve el resumen final con el código, nombre, porcentaje acumulado y monto total asignado a cada centro.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CalculateIntermediateDistribution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CalculateIntermediateDistribution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula y agrega la distribución secundaria de costos por centro de producción a partir de un elemento de distribución intermedia, para un periodo (año/mes) determinado.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateIntermediateDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el elemento de distribución intermedia indicado; Debe existir información de distribución para el año y mes indicados en la función Cost.GetCalculateDistributionIntermediate', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateIntermediateDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan centros de producción cuyo valor distribuido sea distinto de cero; Los porcentajes y valores se agregan (SUM) por centro de producción; La distribución se calcula para un periodo específico (año, mes) y un elemento de distribución intermedia', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateIntermediateDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución secundaria/intermedia de costos; Centro de producción; Elemento de distribución intermedia', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateIntermediateDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Cost.GetCalculateDistributionIntermediate: Cuando Value <> 0, se retorna el resultado agrupado por ProductionCenterId y ProductionCenterCodeName con la suma de Percentage y Value', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateIntermediateDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.GetCalculateDistributionIntermediate', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateIntermediateDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateIntermediateDistribution';
-- GO
