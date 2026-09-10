
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte individual de la Circular FT006 exigida por la Superintendencia de Salud para un período contable (mes y año) específico. Consolida información de cuentas contables principales, parámetros de inversiones y disponible configurados en HealthSuperParametersFt006, cuentas bancarias de la entidad (EntityBankAccounts) y su conciliación bancaria (BankReconciliation) para calcular saldos en extracto y en libros. Incluye datos del tercero asociado (banco o entidad financiera), el código SIMEV, la calificación de riesgo, el tipo de moneda, gravámenes, rendimientos y estado de cada cuenta o inversión reportada, todo filtrado por libro legal y formato 6 de la Supersalud.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSingleCircularFT006';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSingleCircularFT006';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte FT-006 de Supersalud con la información de inversiones financieras y cuentas bancarias de la entidad para un período (mes/año) y libro contable, incluyendo saldos en libros y extracto bancario.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT006';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener los nodos Year, Month y LegalBookId bajo /Data.; Debe existir al menos un registro en GeneralLedger.CompanySettings para obtener la línea de negocio.; Deben existir parámetros HealthSuperParameters con Format=6 y Status=1 asociados a las cuentas a reportar.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT006';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El reporte solo incluye cuentas asociadas a parámetros Supersalud con formato 6 y estado activo (Status=1).; Solo se consideran cuentas pertenecientes al libro legal indicado por @LegalBookId.; La fecha de cierre se calcula como el último día del mes/año indicados.; Los saldos de extracto y libros se toman de la conciliación bancaria del mes/año filtrados; si no existe conciliación se reportan como 0.; La línea de negocio (BusinessLine) es única para toda la entidad y se obtiene del primer registro de CompanySettings.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT006';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reporte FT-006 Supersalud; Inversiones financieras; Cuenta contable (PUC); Libro legal; Tercero; Conciliación bancaria; Saldo en libros; Saldo en extracto; Sobregiro; Cupo bancario; Rendimientos; Gravamen a movimientos financieros; Calificación de riesgo; Entidad calificadora; Código SIMEV; Reservas de inversión; Línea de negocio', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT006';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Devuelve el detalle de cuentas FT-006 filtrando por hsp.Format=6, hsp.Status=1 y ma.LegalBookId=@LegalBookId, con saldos del mes/año indicados.; [RAISERROR] ResultSet: En caso de excepción retorna un resultset con CodeResult=''999'' y MessageResult con el error y línea (manejo en CATCH).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT006';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si hspf.ClassAccount = 1 → El campo Sobregiro toma el valor de eba.Quota (cupo de la cuenta bancaria). else Sobregiro se reporta como 0.; si hspf.Assessment = 1 → Gravamen se reporta como ''1''. else Gravamen se reporta como ''0''.; si CAST(hspf.MeasureDate AS VARCHAR) = ''0001-01-01'' → FechaMedida se reporta como ''00000000'' (fecha nula/no aplica). else FechaMedida se reporta con la fecha real de la medida.; si eba.Number es NULL → IdCuenta usa ma.Number (número de la cuenta contable). else IdCuenta usa el número de la cuenta bancaria (eba.Number).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT006';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; GeneralLedger.MainAccounts; GeneralLedger.HealthSuperParametersFt006; GeneralLedger.HealthSuperParameters; common.ThirdParty; GeneralLedger.GeneralLedgerBalance; Treasury.EntityBankAccounts; Treasury.BankReconciliation', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT006';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT006';
-- GO
