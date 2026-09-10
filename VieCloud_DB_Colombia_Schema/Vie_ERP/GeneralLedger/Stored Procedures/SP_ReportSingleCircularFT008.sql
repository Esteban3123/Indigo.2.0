
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte individual de la Circular FT-008 exigido por la Superintendencia de Salud para la revelación de inversiones financieras. Para un período contable (mes y año) dado, extrae de cada cuenta contable sus parámetros específicos de inversión (tipo de inversión, emisora, fechas de emisión/vencimiento/compra, valores nominales y de mercado, tasas, modalidad, periodicidad, duración, rendimientos y estado), calculando el valor de mercado como la diferencia entre débitos y créditos del balance del libro mayor. Cruza las cuentas contables del plan de cuentas con los parámetros de la Superalud para el formato 8 (HealthSuperParametersFt008), los datos del tercero emisor (NIT, nombre, dígito de verificación) y los saldos contables del período, filtrando únicamente los registros activos del formato FT-008. El resultado alimenta directamente el archivo de reporte regulatorio que se envía a la Superintendencia Nacional de Salud.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSingleCircularFT008';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSingleCircularFT008';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte regulatorio de inversiones financieras del formato FT-008 de la Superintendencia Nacional de Salud para un período (año/mes) y libro legal específicos, incluyendo datos del título, emisor y valor de mercado contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT008';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener los nodos Year, Month y LegalBookId bajo /Data.; Debe existir al menos un registro en GeneralLedger.CompanySettings para obtener la línea de negocio.; Deben existir parámetros configurados en HealthSuperParameters con Format=8 y Status=1 para que el reporte retorne filas.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT008';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha de cierre se calcula siempre como el último día del mes/año recibido.; Las fechas nulas (emisión, vencimiento, compra, medida) se reemplazan por la cadena ''00000000''.; El valor de mercado contable se obtiene siempre como DebitValue menos CreditValue del saldo del período.; Solo se consideran parámetros de Supersalud activos (Status=1) y de formato FT-008 (Format=8).; La línea de negocio reportada es la primera encontrada en CompanySettings.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT008';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Formato FT-008 Supersalud; Inversiones financieras; Tercero emisor (NIT y dígito de verificación); Tipo de inversión; Valor nominal y valor de compra; Tasa facial y tasa de mercado; Modalidad y periodicidad de la inversión; Duración y participación; Rendimientos y gravámenes; Valor de mercado contable; Plan único de cuentas (cuenta principal); Saldo contable mensual (débito/crédito); Línea de negocio de la compañía; Vinculación del tercero', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT008';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultado tabular: Cuando hsp.Format=8 y hsp.Status=1, retorna las inversiones FT-008 con datos del tercero emisor, parámetros del título y el valor de mercado calculado como (DebitValue - CreditValue) del balance del mes/año indicado.; [RETURN_RESULT] Resultado tabular de error: Ante cualquier excepción, retorna CodeResult=''999'' junto con el mensaje de error y la línea donde ocurrió.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT008';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si hsp.Format = 8 AND hsp.Status = 1 → Incluye el registro de la cuenta principal en el reporte FT-008. else Excluye el registro del resultado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT008';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; GeneralLedger.MainAccounts; GeneralLedger.HealthSuperParametersFt008; GeneralLedger.HealthSuperParameters; common.ThirdParty; GeneralLedger.GeneralLedgerBalance; Treasury.EntityBankAccounts', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT008';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT008';
-- GO
