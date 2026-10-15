-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-04-12
-- Description:	Procedimiento que se encarga de guardar, actualizar, anular, confirmar, reversar una liquidación de nómina
-- =============================================
CREATE PROCEDURE [Payroll].[SP_SaveLiquidation]
    @LiquidationXml AS XML,
	@UserCode AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	--Se declaran las variables para obtener la cabecera
	DECLARE @Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)
			

	EXEC [Payroll].[SP_SaveLiquidation_Output]
		@LiquidationXml,
		@UserCode,
		--------------------------------------------
		@Code_Output OUT, 
		@Message_Output OUT,
		--------------------------------------------
		@Id OUTPUT, 
		@Code OUTPUT

	SELECT	@Code_Output AS CodeResult, 
			@Message_Output AS MessageResult, 
			@Id as Id, 
			@Code as Code
END