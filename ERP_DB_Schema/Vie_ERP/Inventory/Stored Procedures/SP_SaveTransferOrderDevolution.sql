-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-26
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una devolución de orden de traslado de inventario
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SaveTransferOrderDevolution]
    @TransferOrderDevolutionXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@MessageResultAux VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC  [Inventory].[SP_SaveTransferOrderDevolution_Output]
		@TransferOrderDevolutionXml, 
		@CodeUser, 
		--------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT, 
		@MessageResultAux OUTPUT,
		--------------------------------------------
		@Id OUTPUT, 
		@Code OUTPUT

	SELECT 
		@CodeResult AS CodeResult, 
		@MessageResult AS MessageResult, 
		@MessageResultAux AS MessageResultAux,
		@Id as Id, 
		@Code as Code
END