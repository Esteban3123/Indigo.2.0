-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-10
-- Description:	Procedimiento que se encarga de guardar y confirmar el cruce de anticipo vs cxc
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_SavePortfolioTransfer]
	@PortfolioTransferXml XML,
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CompanyType TINYINT
AS
BEGIN
    SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Portfolio].[SP_SavePortfolioTransfer_Output] @PortfolioTransferXml, @CodeUser, @CompanyType, @CodeResult OUTPUT, @MessageResult OUTPUT, @Id OUTPUT, @Code OUTPUT

	SELECT 
		@CodeResult AS CodeResult, 
		@MessageResult AS MessageResult, 
		@Id as Id, 
		@Code as Code    
END