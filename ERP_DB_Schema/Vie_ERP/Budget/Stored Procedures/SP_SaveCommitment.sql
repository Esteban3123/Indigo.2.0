-- ===============================================================================================================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-10
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar un compromiso
-- ===============================================================================================================================
CREATE PROCEDURE [Budget].[SP_SaveCommitment]
	@CommitmentXml AS XML,
	@CommitmentDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Budget].[SP_SaveCommitment_Output] 
		@CommitmentXml, 
		@CommitmentDetailForDeleteXml,
		@CodeUser, 
		--------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT, 
		--------------------------------------------
		@Id OUTPUT, 
		@Code OUTPUT

	SELECT 
		@CodeResult AS CodeResult, 
		@MessageResult AS MessageResult, 
		@Id as Id, 
		@Code as Code
END