-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-05-17
-- Description:	Procedimiento que se encarga de guardar, actualizar, anular, confirmar una orden de trabajo
-- =============================================
CREATE PROCEDURE [Maintenance].[SP_SaveWorkOrder]
    @WorkOrderXml AS XML,
	@UserCode AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Maintenance].[SP_SaveWorkOrder_Output]
		@WorkOrderXml, 
		@UserCode, 
		--------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT, 
		--------------------------------------------
		@Id OUTPUT, 
		@Code OUTPUT

	SELECT	@CodeResult AS CodeResult, 
			@MessageResult AS MessageResult, 
			@Id as Id, 
			@Code as Code
END