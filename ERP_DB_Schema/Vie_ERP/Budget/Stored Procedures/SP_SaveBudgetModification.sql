-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-10
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una modificación de presupuesto
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveBudgetModification]
    @BudgetModificationXml AS XML,
	@BudgetModificationDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@AuxiliaryResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Budget].[SP_SaveBudgetModification_Output] 
		@BudgetModificationXml, 
		@BudgetModificationDetailForDeleteXml, 
		@CodeUser, 
		--------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT, 
		@AuxiliaryResult OUTPUT,
		--------------------------------------------
		@Id OUTPUT, 
		@Code OUTPUT

	SELECT 
		@CodeResult AS CodeResult, 
		@MessageResult AS MessageResult, 
		@AuxiliaryResult AS AuxiliaryResult, 
		@Id as Id, 
		@Code as Code
END