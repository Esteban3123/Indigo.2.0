-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-02
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una distribución secundaria
-- =============================================
CREATE PROCEDURE [Cost].[SP_SaveDistributionSecondary]
    @DistributionSecondaryXml AS XML,
	@DistributionSecondaryDetailForDeleteXml AS XML,
	@CostLogisticsProductionCenterDetail AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeMessage INT,
			@Message VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC Cost.SP_SaveDistributionSecondary_Output 
		@DistributionSecondaryXml, 
		@DistributionSecondaryDetailForDeleteXml, 
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