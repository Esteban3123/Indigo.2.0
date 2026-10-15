-- =============================================
-- Author:		Carlos Cordoba
-- Create date: 09-06-2016
-- Description:	Procedimiento para generar un recibo de caja
-- =============================================
CREATE PROCEDURE [Treasury].[SP_SaveCashReceipts] 
	@CashReceiptsXml AS XML,
	@User VARCHAR(20),
	@CompanyType int
AS
BEGIN
    SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@StatusResult TINYINT,
			@CashReceiptId INT

	EXEC [Treasury].[SP_SaveCashReceipts_Output] 
			@CashReceiptsXml, 
			@User, 
			@CompanyType, 
			-----------------------------------------------
			@CodeResult OUTPUT, 
			@MessageResult OUTPUT,
			@StatusResult OUTPUT,
			@CashReceiptId OUTPUT

	SELECT	CAST(@CodeResult AS VARCHAR(20)) AS CodeMessage, 
			@MessageResult AS Message, 
			@CashReceiptId AS CashReceiptId,
			@StatusResult Status
END