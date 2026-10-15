
CREATE PROCEDURE [GeneralLedger].[SP_ReportSingleCircularFT006]
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


		SELECT	@BusinessLine					AS BusinessLine,
				tp.Id							AS ThirdPartyId,
				ma.Id						AS MainAccountId,
				hspf.Storehouse				AS	Establecimiento,
				tp.Nit						AS IdEstablecimiento,
				hspf.SIMEVCode				AS CodigoSIMEV,
				hspf.RiskRating				AS CalificacionRiesgo,
				hspf.RatingEntity			AS EntidadCalificadora,
				hspf.AnotherQualifier		AS OtraCalificadora,
				hspf.ClassAccount			AS clasecuenta,
				hspf.CurrencyType				AS tipoMoneda,
				ISNULL(eba.Number,ma.Number)	AS IdCuenta,
				ma.Name								AS NombreCuenta,
				ISNULL(br.ExtractValue,0)						AS SaldoExtracto,
				ISNULL(br.EntityBankAccountValue,0)	AS SaldoLibros,
				IIF(hspf.ClassAccount= 1,eba.Quota,0) AS Sobregiro,
				hspf.Yields							AS Rendimientos,
				IIF(hspf.Assessment=1,'1','0')	AS Gravamen,
				hspf.Status							AS Estado,
				IIF(CAST(hspf.MeasureDate AS VARCHAR) = '0001-01-01','00000000',CAST(hspf.MeasureDate AS VARCHAR))		AS	FechaMedida,
				hspf.MeasuredValue		AS	valorMedida,
				hspf.InvestmentReserves AS InversionReservas
		FROM GeneralLedger.MainAccounts ma WITH(NOLOCK)
		JOIN GeneralLedger.HealthSuperParametersFt006 hspf WITH(NOLOCK) ON ma.Id = hspf.MainAccountId
		JOIN GeneralLedger.HealthSuperParameters hsp WITH(NOLOCK) ON hspf.HealthSuperParametersId = hsp.Id
		LEFT JOIN common.ThirdParty tp WITH(NOLOCK) ON hspf.ThirdPartyId =tp.Id
		LEFT JOIN GeneralLedger.GeneralLedgerBalance glb  WITH(NOLOCK) ON ma.Id = glb.IdMainAccount AND glb.Month = @Month AND glb.Year=@Year
		LEFT JOIN Treasury.EntityBankAccounts eba  WITH(NOLOCK) ON ma.Id = eba.IdMainAccount 
		LEFT JOIN Treasury.BankReconciliation br WITH(NOLOCK) ON eba.Id = br.EntityBankAccountId AND (YEAR(br.DocumentDate) = @Year AND Month(br.DocumentDate) = @Month)
		WHERE ma.Id = hspf.MainAccountId AND hsp.Format = 6 AND hsp.Status =1
		AND ma.LegalBookId =@LegalBookId
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END