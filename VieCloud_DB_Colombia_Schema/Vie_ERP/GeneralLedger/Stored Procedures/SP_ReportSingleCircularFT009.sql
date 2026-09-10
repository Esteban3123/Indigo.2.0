
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte contable de la Circular FT009 exigido por la Superintendencia de Salud, calculando el total de Activos y Pasivos en dólares (USD) para un mes, año y libro legal específicos. Toma los criterios de filtro (año, mes, libro legal) desde un XML de entrada, consulta los saldos del libro mayor (débitos y créditos) por cuenta contable y los cruza con los parámetros de la Supersalud para el formato 9, clasificando las cuentas según su clase (código 1 = Activos, código 2 = Pasivos). Retorna una fila con la línea de negocio de la empresa, el tipo de moneda y los totales de activos y pasivos, sirviendo como base para el reporte regulatorio de estados financieros ante la Superintendencia Nacional de Salud.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSingleCircularFT009';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSingleCircularFT009';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula totales de activos y pasivos en moneda US para el formato regulatorio FT009 de la Superintendencia de Salud, en un período (mes/año) y libro legal específicos.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT009';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en CompanySettings para obtener la línea de negocio.; El XML de criterios debe contener los nodos Year, Month y LegalBookId bajo /Data.; Deben existir parámetros de Supersalud con Format = 9 y Status = 1 vinculados a cuentas contables vía HealthSuperParametersFt009.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT009';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los saldos siempre se calculan como DebitValue - CreditValue.; El reporte siempre se emite en moneda ''US''.; Solo se consideran parámetros de Supersalud con Format=9 y Status=1 (FT009 activos).; El filtro por mes y año se aplica siempre al saldo (GeneralLedgerBalance).; El filtro por LegalBookId restringe las cuentas al libro legal solicitado.; La línea de negocio (BusinessLine) proviene del primer registro de CompanySettings.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT009';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activos contables; Pasivos contables; Libro mayor (General Ledger); Plan de cuentas (PUC); Saldos contables (débito/crédito); Período contable (mes/año); Libro legal (LegalBook); Superintendencia de Salud; Formato regulatorio FT009; Línea de negocio', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT009';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultado consulta: Devuelve un único registro con BusinessLine, TipoMoneda=''US'', suma de Activos (cuentas con MainAccountClasses.Code=1) y suma de Pasivos (cuentas con MainAccountClasses.Code=2) calculados como DebitValue-CreditValue del período indicado.; [RETURN_RESULT] Resultado consulta: Ante cualquier error del TRY, retorna un conjunto con CodeResult=''999'' y MessageResult con el mensaje y línea del error.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT009';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MainAccountClasses.Code = 1 → La cuenta se considera de naturaleza Activo y suma a la columna Activos.; si MainAccountClasses.Code = 2 → La cuenta se considera de naturaleza Pasivo y suma a la columna Pasivos.; si HealthSuperParameters.Format = 9 AND Status = 1 → Solo se incluyen cuentas vinculadas a parámetros de Supersalud activos del formato FT009.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT009';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; GeneralLedger.GeneralLedgerBalance; GeneralLedger.HealthSuperParametersFt009; GeneralLedger.HealthSuperParameters', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT009';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT009';
-- GO
