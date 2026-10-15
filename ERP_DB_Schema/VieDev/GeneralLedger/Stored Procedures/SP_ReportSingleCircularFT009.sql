
CREATE PROCEDURE [GeneralLedger].[SP_ReportSingleCircularFT009]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Year int,
			@Month int,			
			@LegalBookId INT,
			@BusinessLine TINYINT

			DECLARE @Table_Result AS TABLE(
			BusinessLine  TINYINT,
			TipoMoneda  CHAR(3),
			Activos  DECIMAL(18,2),
			Pasivos  DECIMAL(18,2)			
			)
			
	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@Year = t.x.value('Year[1]','int'),
				@Month = t.x.value('Month[1]','int'),
				@LegalBookId = t.x.value('LegalBookId[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/
		SET @BusinessLine = (SELECT TOP 1 cs.BusinessLine FROM GeneralLedger.CompanySettings cs)


		 INSERT INTO @Table_Result
		 SELECT @BusinessLine,
				'US' AS TipoMoneda,
			(	SELECT SUM(glb.DebitValue-glb.CreditValue) SumActive
				FROM GeneralLedger.MainAccounts ma
				LEFT JOIN GeneralLedger.MainAccountClasses mac		WITH(NOLOCK) ON ma.IdAccountClass = mac.Id
				LEFT JOIN GeneralLedger.GeneralLedgerBalance glb	WITH(NOLOCK) ON ma.Id = glb.IdMainAccount AND glb.Month = @Month AND glb.Year = @Year
				LEFT JOIN GeneralLedger.HealthSuperParametersFt009 hspf WITH(NOLOCK) ON ma.Id = hspf.MainAccountId
				INNER JOIN GeneralLedger.HealthSuperParameters hsp WITH(NOLOCK) ON hspf.HealthSuperParametersId = hsp.Id
				WHERE mac.Code = 1 AND ma.LegalBookId = @LegalBookId AND hsp.Format = 9 AND hsp.Status= 1) AS Activos,

		(SELECT SUM(glb.DebitValue-glb.CreditValue) SumPassive
				FROM GeneralLedger.MainAccounts ma
				LEFT JOIN GeneralLedger.MainAccountClasses mac		WITH(NOLOCK) ON ma.IdAccountClass = mac.Id
				LEFT JOIN GeneralLedger.GeneralLedgerBalance glb	WITH(NOLOCK) ON ma.Id = glb.IdMainAccount AND glb.Month = @Month AND glb.Year = @Year
				LEFT JOIN GeneralLedger.HealthSuperParametersFt009 hspf WITH(NOLOCK) ON ma.Id = hspf.MainAccountId
				INNER JOIN GeneralLedger.HealthSuperParameters hsp WITH(NOLOCK) ON hspf.HealthSuperParametersId = hsp.Id
				WHERE mac.Code = 2 AND ma.LegalBookId = @LegalBookId AND hsp.Format = 9 AND hsp.Status= 1) Pasivos 
---------------------------------------------------------------------------------------------------------------------------------------------------------
			SELECT	tr.BusinessLine,
					tr.TipoMoneda,
					tr.Activos,
					tr.Pasivos
			FROM @Table_Result tr
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END