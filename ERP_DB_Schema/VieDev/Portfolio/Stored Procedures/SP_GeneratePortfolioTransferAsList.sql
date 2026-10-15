-- =============================================
-- Author:		Diego Roldan
-- Create date: 2024-01-21
-- Description:	Genera un cruce de anticipos vs CxC
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_GeneratePortfolioTransferAsList]
	@ListPortfolioAdvanceCrossingXml XML,
	@OperativeUnitId INT,
	@UserCode VARCHAR(20),
	@AccountReceivableId INT,
	@CompanyType TINYINT
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		DECLARE @CodeResult INT,
				@MessageResult VARCHAR(MAX)

		EXEC Portfolio.SP_GeneratePortfolioTransfer	
			@ListPortfolioAdvanceCrossingXml,
			@OperativeUnitId, 
			@UserCode, 
			@AccountReceivableId, 
			@CompanyType, 
			@CodeResult OUTPUT, 
			@MessageResult OUTPUT

		SELECT	@CodeResult AS Code, @MessageResult AS Message
	END TRY
	BEGIN CATCH
		SELECT	99 AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH	
END