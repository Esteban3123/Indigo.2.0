-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-22
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una orden de pago
-- =============================================
CREATE PROCEDURE [Budget].[SP_SavePaymentOrder]
    @PaymentOrderXml AS XML,
	@PaymentOrderDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeMessage INT,
			@Message VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Budget].[SP_SavePaymentOrder_Output] @PaymentOrderXml, @PaymentOrderDetailForDeleteXml, @CodeUser, @CodeMessage OUTPUT, @Message OUTPUT, @Id OUTPUT, @Code OUTPUT

	SELECT 
		@CodeMessage AS CodeMessage, 
		@Message AS Message, 
		@Id as Id, 
		@Code as Code
END