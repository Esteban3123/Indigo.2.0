-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-08-18
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar un traslado de dispensación por ingreso
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SavePharmaceuticalDispensingTransfer]
    @PharmaceuticalDispensingTransferXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Inventory].[SP_SavePharmaceuticalDispensingTransfer_Output] 
		@PharmaceuticalDispensingTransferXml, 
		@CodeUser, 
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