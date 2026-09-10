
CREATE PROCEDURE [GeneralLedger].[SP_ReportSingleCircularFT007]
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
				hspf.Instrument					AS	Instrumento,
				hspf.InvestmentType				AS	TipoInversion,
				hspf.Mnemonic					AS	Nemotécnico,
				hspf.CodeTitle					AS	CodigoTitulo,
				tp.Nit							AS	IdEmisora,
				tp.DigitVerification			AS	DvEmisora,
				hspf.SIMEVCode					AS	CodigoSIMEV,
				hspf.RatingEntity				AS EntidadCalificadora,
				hspf.AnotherQualifier			AS OtraCalificadora,
				hspf.RiskRating					AS CalificacionRiesgo,
				ISNULL(CAST(hspf.BroadcastDate AS VARCHAR),'00000000') AS FechaEmision,
				ISNULL(CAST(hspf.ExpirationDate AS VARCHAR),'00000000') AS FechaVencimiento,
				ISNULL(CAST(hspf.PurchaseDate AS VARCHAR),'00000000') AS FechaCompra,
				hspf.PurchaseRate					AS TasaCompra,
				hspf.PurchaseValue					AS ValorCompra,
				hspf.NominalValue					AS ValorNominal,
				hspf.CurrencyType				AS TipoMoneda,
				hspf.FacialRate					AS TasaFacial,
				hspf.Modality					AS Modalidad,
				hspf.Periodicity				AS Periodicidad,
				hspf.MarketRate					AS TasaMercado,
				(glb.DebitValue - glb.CreditValue)	AS ValorMercado,
				hspf.Duration					AS Duracion,
				hspf.Assessment					AS Gravamen,
				hspf.Status						AS Estado,
				ISNULL(CAST(hspf.MeasureDate AS VARCHAR),'00000000') AS FechaMedida,
				hspf.MeasuredValue					AS	ValorMedida,
				hspf.Linked							AS	Vinculado,
				hspf.Dematerialized					AS	Desmaterializado,
				hspf.InvestmentTechnicalReserves	AS	InversionReservas
		FROM GeneralLedger.MainAccounts ma WITH(NOLOCK)
		LEFT JOIN GeneralLedger.HealthSuperParametersFt007 hspf WITH(NOLOCK) ON ma.Id = hspf.MainAccountId
		LEFT JOIN GeneralLedger.HealthSuperParameters hsp WITH(NOLOCK) ON hspf.HealthSuperParametersId = hsp.Id
		LEFT JOIN common.ThirdParty tp WITH(NOLOCK) ON hspf.ThirdPartyId =tp.Id
		LEFT JOIN GeneralLedger.GeneralLedgerBalance glb  WITH(NOLOCK) ON ma.Id = glb.IdMainAccount AND glb.Month = @Month AND glb.Year=@Year
		LEFT JOIN Treasury.EntityBankAccounts eba  WITH(NOLOCK) ON ma.Id = eba.IdMainAccount
		WHERE ma.Id = hspf.MainAccountId AND hsp.Format = 7 AND hsp.Status =1		
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte individual de la Circular FT007 exigido por la Superintendencia de Salud para la revelación de inversiones del portafolio financiero de la entidad. Para un período contable (mes y año) determinado, consolida los parámetros de cada título valor registrado en HealthSuperParametersFt007 —instrumento, tipo de inversión, nemotécnico, código del título, código SIMEV, calificación de riesgo, fechas de emisión/vencimiento/compra, tasas, valores nominal y de compra, moneda, modalidad, periodicidad y estado— cruzando la cuenta contable principal (MainAccounts) con su saldo del libro mayor (GeneralLedgerBalance) para obtener el valor de mercado, y con el tercero emisor (ThirdParty) para obtener el NIT y dígito de verificación de la entidad emisora. También vincula las cuentas bancarias de tesorería (EntityBankAccounts) asociadas a esas cuentas. El resultado es el conjunto de datos estructurado que alimenta el formulario FT007 de reporte a la Supersalud.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSingleCircularFT007';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSingleCircularFT007';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte regulatorio FT007 de Supersalud con el detalle del portafolio de inversiones financieras y su valor de mercado contable para un periodo (mes/año) determinado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT007';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en GeneralLedger.CompanySettings para obtener la línea de negocio.; Los parámetros Year, Month y LegalBookId deben venir en el XML de criterios.; Deben existir parámetros de Supersalud (HealthSuperParameters) con Format = 7 y Status = 1 vinculados a los registros FT007.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT007';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha de cierre se calcula como el último día del mes indicado (@Year/@Month).; El valor de mercado contable se obtiene siempre como DebitValue - CreditValue del balance del periodo.; Solo se consideran parámetros de Supersalud con formato 7 y estado activo (1).; La línea de negocio reportada proviene siempre del primer registro de CompanySettings.; El procedimiento nunca relanza excepciones; siempre devuelve un resultset (datos o error 999).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT007';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reporte FT007 Supersalud; Portafolio de inversiones; Títulos valores; Calificación de riesgo; Código SIMEV; Entidad calificadora; Valor nominal y de compra; Tasa facial y de mercado; Duración; Gravamen; Reservas técnicas de inversión; Cuenta contable (PUC); Tercero emisor (NIT y dígito de verificación); Saldo contable débito/crédito; Línea de negocio; Libro legal contable', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT007';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Cuando hsp.Format = 7 y hsp.Status = 1, retorna el detalle del portafolio FT007 con su valor de mercado calculado como (glb.DebitValue - glb.CreditValue) del periodo @Month/@Year.; [RETURN_RESULT] Resultset: En caso de error en TRY, retorna un resultset con CodeResult=''999'' y MessageResult con el mensaje de error y línea.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT007';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si hsp.Format = 7 AND hsp.Status = 1 → Incluye el registro de inversión en el reporte FT007. else Excluye el registro del reporte.; si Fechas (BroadcastDate, ExpirationDate, PurchaseDate, MeasureDate) son NULL → Se reemplazan por la cadena ''00000000'' en la salida.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT007';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; GeneralLedger.MainAccounts; GeneralLedger.HealthSuperParametersFt007; GeneralLedger.HealthSuperParameters; common.ThirdParty; GeneralLedger.GeneralLedgerBalance; Treasury.EntityBankAccounts', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT007';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT007';
-- GO
