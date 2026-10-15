-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 23-01-2015
-- Description:	Store para crear ordenes de servicio
-- =============================================
CREATE PROCEDURE [Billing].[SP_GenerateServiceOrder]
	@ServiceOrderXml as xml,
	@User varchar(20)
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @CodeResult VARCHAR(3),
			@MessageResult VARCHAR(MAX),
			@StatusResult TINYINT,
			@Id INT

	EXEC Billing.SP_GenerateServiceOrder_Output	@ServiceOrderXml, 
													@User, 
													--Salidas
													@CodeResult OUTPUT, 
													@MessageResult OUTPUT, 
													@StatusResult OUTPUT,	
													@Id OUTPUT

	select @CodeResult CodeMessage, @MessageResult [Message], @Id ServiceOrderId, @StatusResult [Status]
END