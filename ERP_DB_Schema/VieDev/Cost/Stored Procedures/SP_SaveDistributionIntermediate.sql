-- =============================================
-- Author:		Diego A. Roldan Lozano
-- Create date: 2025-07-23
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una distribución intermedia
-- =============================================
CREATE PROCEDURE [Cost].[SP_SaveDistributionIntermediate]
    @DistributionIntermediateXml AS XML,
	@DistributionIntermediateDetailForDeleteXml AS XML,
	@CostLogisticsProductionCenterDetail AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeMessage INT,
			@Message VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC Cost.SP_SaveDistributionIntermediate_Output 
		@DistributionIntermediateXml, 
		@DistributionIntermediateDetailForDeleteXml, 
		@CostLogisticsProductionCenterDetail,
		@CodeUser, 
		-----------------------------------------
		@CodeMessage OUTPUT, 
		@Message OUTPUT, 
		@Id OUTPUT, 
		@Code OUTPUT

	SELECT 
		@CodeMessage AS CodeMessage, 
		@Message AS Message, 
		@Id as Id, 
		@Code as Code
END