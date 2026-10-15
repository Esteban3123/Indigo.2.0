-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-06-19
-- Description:	Procedimiento para el reporte de la circular 014
-- =============================================
CREATE  PROCEDURE [Portfolio].[SP_ReportCircularAccountsReceivable]
	@HisContainer VARCHAR(30),
	@xmlFilters AS XML
AS
BEGIN
	DECLARE @Year INT,
			@Month INT,
			@HealthAdministratorIds VARCHAR(MAX),
			@Status VARCHAR(MAX),
			@EntityNit AS VARCHAR(20), 
			@EntityDigitVerification AS VARCHAR(1)

	BEGIN TRY

		--Se obtienen los datos de los filtros
		SELECT 
			@Year = t.x.value('Year[1]','int'),
			@Month = t.x.value('Month[1]','int'),
			@HealthAdministratorIds = t.x.value('HealthAdministratorIds[1]','varchar(max)'),
			@Status = t.x.value('Status[1]','varchar(max)')
		FROM @xmlFilters.nodes('/Data') t(x)

		SELECT 
			@EntityNit = tp.Nit, 
			@EntityDigitVerification = tp.DigitVerification
		FROM GeneralLedger.GeneralLedgerSettings gls
		JOIN Common.ThirdParty tp ON gls.IdDian = tp.Id

		DECLARE @query VARCHAR(MAX)

		SET @Query =	N'
SELECT ''' + @EntityNit + ''' EntityNit, 
	''' + @EntityDigitVerification + ''' EntityDigitVerification,
	YEAR(ri.RadicatedDate) RadicatedYear,
	MONTH(ri.RadicatedDate) RadicatedMonth,
	IIF(rid.InvoiceDocumentType = 4, 1, 2) InvoiceType,
	rid.InvoiceNumber,
	CAST(rid.InvoiceValueEntity + rid.InvoiceValuePacient AS DECIMAL) InvoiceValue,
	ha.HealthEntityCode,
	ISNULL(ca.DEPMUNCOD,'''') MunicipalityCode,
	0 PaymentAgreement
FROM Portfolio.RadicateInvoiceC ri
LEFT JOIN Portfolio.RadicateInvoiceD rid ON ri.Id = rid.RadicateInvoiceCId
LEFT JOIN Billing.Invoice i ON rid.InvoiceNumber = i.InvoiceNumber
LEFT JOIN Portfolio.AccountReceivable ar on rid.InvoiceNumber = ar.InvoiceNumber
LEFT JOIN Contract.HealthAdministrator ha ON ar.ThirdPartyId = ha.ThirdPartyId
LEFT JOIN ' + @HisContainer + '.dbo.ADINGRESO ing ON i.AdmissionNumber = ing.NUMINGRES
LEFT JOIN ' + @HisContainer + '.dbo.ADCENATEN ca ON ing.CODCENATE = ca.CODCENATE
WHERE rid.InvoiceDocumentType IN (1, 2, 4) 
	AND YEAR(ri.RadicatedDate) = ' + CAST(@Year AS VARCHAR(4)) + '
	AND MONTH(ri.RadicatedDate) = ' + CAST(@Month AS VARCHAR(2)) + '
	AND ha.Id IN (' + @HealthAdministratorIds + ')
	AND ri.State IN (' + @Status + ')'
	
		EXEC(@Query)	

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END