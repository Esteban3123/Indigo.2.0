-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-07-16
-- Description:	Procedimiento que se encarga de validar los detalles de la orden de servicio
-- =============================================
CREATE PROCEDURE [Billing].[SP_ValidateServiceOrderDetail]
	@ServiceOrderDetailXml XML,
	@UserCode VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX)

	EXEC [Billing].[SP_ValidateServiceOrderDetail_Output]
		@ServiceOrderDetailXml,
		@UserCode,
		--------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT

	SELECT	@CodeResult AS CodeResult, 
			@MessageResult AS MessageResult
END