-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-19
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar un reintegro
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveReimbursementResource]
    @ReimbursementResourceXml AS XML,
	@ReimbursementResourceDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@AuxiliaryResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Budget].[SP_SaveReimbursementResource_Output] 
		@ReimbursementResourceXml, 
		@ReimbursementResourceDetailForDeleteXml,
		@CodeUser,
		--------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT, 
		@AuxiliaryResult OUTPUT,
		--------------------------------------------
		@Id OUTPUT, 
		@Code OUTPUT

	SELECT 
		@CodeResult AS CodeMessage, 
		@MessageResult AS Message, 
		@Id as Id, 
		@Code as Code
END