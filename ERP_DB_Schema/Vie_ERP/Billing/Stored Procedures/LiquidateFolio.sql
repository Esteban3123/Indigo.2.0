-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-07-19
-- Description:	Liquidación
-- =============================================
CREATE PROCEDURE [Billing].[LiquidateFolio]
	@PatientCode VARCHAR(20),
	@AdmissionNumber VARCHAR(20),
	@ContainerCrystal VARCHAR(10),
	@BillingAuthorizationId INT,
	@OperativeUnitId INT,
	@ThirdPartyPatientId INT,
	@UserCode VARCHAR(20),
	@CompanyType TINYINT,
	@RevenueControlDetailCrossingListXml XML,
	@SkipAccountControlValidations BIT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @ResultStatus BIT, 
			@ResultMessageInvoice VARCHAR(MAX), 
			@ResultMessage VARCHAR(MAX),
			@ResultXml XML

	DECLARE @InvoiceResult AS TABLE
	(
		InvoiceId INT,
		InvoiceNumber VARCHAR(20)
	)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY
		EXEC Billing.SP_LiquidateFolio_Output	@PatientCode, 
												@AdmissionNumber, 
												@ContainerCrystal, 
												@BillingAuthorizationId, 
												@OperativeUnitId, 
												@ThirdPartyPatientId, 
												@UserCode, 
												@CompanyType, 
												@RevenueControlDetailCrossingListXml,
												@SkipAccountControlValidations,
												--Salidas
												@ResultStatus OUTPUT, 
												@ResultMessageInvoice OUTPUT, 
												@ResultMessage OUTPUT,	
												@ResultXml OUTPUT

		--Se obtiene las facturas que vienen en el XML
		INSERT INTO @InvoiceResult
			SELECT	t.x.value('InvoiceId[1]','INT') InvoiceId,
					t.x.value('InvoiceNumber[1]','VARCHAR(20)') InvoiceNumber
			FROM @ResultXml.nodes('/Data') t(x)

		IF NOT EXISTS (SELECT 1 FROM @InvoiceResult)
		BEGIN
			SELECT @ResultStatus AS [StatusResult], @ResultMessageInvoice AS [MessageResult], @ResultMessage AS [Message], 0 AS InvoiceId, '' AS InvoiceNumber
			RETURN
		END
		
		SELECT @ResultStatus AS [StatusResult], @ResultMessageInvoice AS [MessageResult], @ResultMessage AS [Message], InvoiceId, InvoiceNumber
		FROM @InvoiceResult
	END TRY
	BEGIN CATCH
		SELECT CONVERT(BIT, 0) AS [StatusResult], '' AS [MessageResult], CONCAT('Error llamando la liquidación de Folios: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE()) AS [Message], 0 AS InvoiceId, '' AS InvoiceNumber
	END CATCH	

	--SELECT CONVERT(BIT, 0) AS [StatusResult], '' AS [MessageResult], '' AS [Message], 0 AS InvoiceId, '' AS InvoiceNumber
END