CREATE PROCEDURE [Billing].[SP_ReversePortfolioByBasicBillingIdAsList]
	@OperatingUnitId INT,
	@BasicBillingId INT,
	@AnnulmentDate DATETIME,
	@ReversalReasonDescription VARCHAR(300),
	@CodeUser VARCHAR(20),
	@CompanyType TINYINT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @CodeResult VARCHAR(20), @MessageResult VARCHAR(MAX)

	EXEC [Billing].[SP_ReversePortfolioByBasicBillingId_Output] @OperatingUnitId, @BasicBillingId, @AnnulmentDate, @ReversalReasonDescription, @CodeUser, @CompanyType, @CodeResult OUTPUT, @MessageResult OUTPUT

	SELECT @CodeResult AS Code, @MessageResult AS Message
END