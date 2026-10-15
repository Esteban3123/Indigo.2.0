-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-16
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una distribución de mano de obra
-- =============================================
CREATE PROCEDURE [Cost].[SP_SaveCostDistributionManpower]
    @CostDistributionManpowerXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeMessage INT,
			@Message VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Cost].[SP_SaveCostDistributionManpower_Output] @CostDistributionManpowerXml, @CodeUser, @CodeMessage OUTPUT, @Message OUTPUT, @Id OUTPUT, @Code OUTPUT

	SELECT 
		@CodeMessage AS CodeMessage, 
		@Message AS Message, 
		@Id as Id, 
		@Code as Code
END