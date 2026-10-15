
CREATE PROCEDURE [GeneralLedger].[SP_ReportSingleCircularFT008]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Year int,
			@Month int,			
			@LegalBookId INT,
			@BusinessLine TINYINT
			
	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@Year = t.x.value('Year[1]','int'),
				@Month = t.x.value('Month[1]','int'),
				@LegalBookId = t.x.value('LegalBookId[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/
		DECLARE @ClosingDate DATE = '01/' + RIGHT('0' + CAST(@Month AS VARCHAR(20)), 2)+ '/' + CAST(@Year AS VARCHAR(20)),
				@AfterClosingDate DATE
		SET @ClosingDate = DATEADD(DAY, -1, DATEADD(MONTH, 1, @ClosingDate))
		SET @AfterClosingDate = DATEADD(DAY, 1, @ClosingDate)
		SET @BusinessLine = (SELECT TOP 1 cs.BusinessLine FROM GeneralLedger.CompanySettings cs)


		SELECT	@BusinessLine					AS	BusinessLine,
				tp.Id							AS	ThirdPartyId,
				ma.Id							AS	MainAccountId,
				hspf.Country					AS	Pais,
				tp.Nit							AS	IdEmisora,
				tp.DigitVerification			AS	DvEmisora,
				tp.Name							AS	NombreEmisora,
				hspf.InvestmentType				AS	TipoInversion,
				hspf.AnotherInvestmentType		AS  OtraInversion,
				ISNULL(CAST(hspf.BroadcastDate AS VARCHAR),'00000000') AS FechaEmision,
				ISNULL(CAST(hspf.ExpirationDate AS VARCHAR),'00000000') AS FechaVencimiento,
				ISNULL(CAST(hspf.PurchaseDate AS VARCHAR),'00000000') AS FechaCompra,
				hspf.PurchaseValue					AS ValorCompra,
				hspf.PurchaseRate					AS TasaCompra,
				hspf.NominalValue					AS ValorNominal,
				hspf.CurrencyType				AS TipoMoneda,
				hspf.FacialRate					AS TasaFacial,
				hspf.Modality					AS Modalidad,
				hspf.Periodicity				AS Periodicidad,
				hspf.MarketRate					AS TasaMercado,
				(glb.DebitValue - glb.CreditValue)	AS ValorMercado,
				hspf.Duration					AS Duracion,
				hspf.Participation				AS Participacion,
				hspf.Yields						AS Rendimientos,
				hspf.Assessment					AS Gravamen,
				hspf.Status						AS Estado,
				ISNULL(CAST(hspf.MeasureDate AS VARCHAR),'00000000') AS FechaMedida,
				hspf.MeasuredValue					AS	ValorMedida,
				hspf.Linked							AS	Vinculado
				FROM GeneralLedger.MainAccounts ma WITH(NOLOCK)
		LEFT JOIN GeneralLedger.HealthSuperParametersFt008 hspf WITH(NOLOCK) ON ma.Id = hspf.MainAccountId
		LEFT JOIN GeneralLedger.HealthSuperParameters hsp WITH(NOLOCK) ON hspf.HealthSuperParametersId = hsp.Id
		LEFT JOIN common.ThirdParty tp WITH(NOLOCK) ON hspf.ThirdPartyId =tp.Id
		LEFT JOIN GeneralLedger.GeneralLedgerBalance glb  WITH(NOLOCK) ON ma.Id = glb.IdMainAccount AND glb.Month = @Month AND glb.Year=@Year
		LEFT JOIN Treasury.EntityBankAccounts eba  WITH(NOLOCK) ON ma.Id = eba.IdMainAccount
		WHERE ma.Id = hspf.MainAccountId AND hsp.Format = 8 AND hsp.Status =1		
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END