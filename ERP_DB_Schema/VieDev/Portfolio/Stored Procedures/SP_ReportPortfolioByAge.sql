
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-06-06
-- Description:	Procedimiento para el reporte de edades de cartera FIERRROOOOO
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ReportPortfolioByAge]
	@HisContainer AS VARCHAR(20),
	@xmlCriterias AS XML,
	@xmlFilters AS XML
	WITH RECOMPILE
AS
BEGIN
	SET NOCOUNT ON
	BEGIN TRY
	
	DECLARE @OfficialCurrencyId integer = (
										   SELECT OfficialCurrencyId 
										   FROM GeneralLedger.LegalBook
										   WHERE OfficialBook = 1
										   GROUP by OfficialCurrencyId										   
										  ) 

										  


	IF @OfficialCurrencyId > 1
	BEGIN
		EXEC [Portfolio].[SP_ReportPortfolioByAge_International] @HisContainer,
																 @xmlCriterias,
																 @xmlFilters
																 
	END
	ELSE
	BEGIN
		EXEC [Portfolio].[SP_ReportPortfolioByAge_Native] @HisContainer,
														  @xmlCriterias,
														  @xmlFilters
	END

	END TRY
	BEGIN CATCH
	 SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END