-- =============================================
-- Author:		Juan Bermudez
-- Create date: 18/03/2016
-- Description:	Procedimiento para el reporte de la CGN-2005-002
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportCGN002]
	@dateStart as date,
	@dateEnd as date,
	@accountStart as varchar(20),
	@accountEnd as varchar(20),
	@levelSubAccount as bit,
	@bookId as integer
AS
BEGIN
	DECLARE @Year INT,
			@InitialMonth INT,
			@FinalMonth INT,
			@IsClosedLastYear BIT,
			@CompanyThirdPartyId INT = 0

	SELECT	@Year = YEAR(@dateStart),
			@InitialMonth = MONTH(@dateStart),
			@FinalMonth = MONTH(@dateEnd),
			@IsClosedLastYear = IIf(lb.LastYearClose >= (YEAR(@dateStart) - 1), 1, 0)
	FROM GeneralLedger.LegalBook lb
	WHERE lb.Id = @bookId

	SELECT @CompanyThirdPartyId = IdDian
	FROM GeneralLedger.GeneralLedgerSettings

	-----------------------------------------------------------------------------------------------------------------

	-- Creamos la tabla temporal que devolveremos con los datos
	DECLARE @tableExecution table
	(
		mainAccountId int,
		mainAccountCode varchar(20),
		mainAccountName varchar(max),
		LevelAccount int,
		thirdPartyId int, 
		thirdPartyNit varchar(15),
		thirdPartyName varchar(max),
		balanceCurrent decimal(20,4), 
		balanceNotCurrent decimal(20,4)		
	)

	-----------------------------------------------------------------------------------------------------------------

	INSERT INTO @tableExecution 
		SELECT	ma.Id, ma.Number, ma.Name, mal.Level,
				tp.Id, tp.EntityCode, tp.Name,
				IIF(ma.Availability = 1, (ISNULL(glb.InitialDebitValue, 0) - ISNULL(glb.InitialCreditValue, 0) + ISNULL(glb.DebitValue, 0) - ISNULL(glb.CreditValue, 0)) * IIF(ma.Nature = 1, 1, -1), 0) balanceCurrent,
				IIF(ma.Availability = 1, 0, (ISNULL(glb.InitialDebitValue, 0) - ISNULL(glb.InitialCreditValue, 0) + ISNULL(glb.DebitValue, 0) - ISNULL(glb.CreditValue, 0)) * IIF(ma.Nature = 1, 1, -1)) balanceNotCurrent
		FROM GeneralLedger.MainAccounts AS ma
		JOIN GeneralLedger.MainAccountClasses AS mac ON mac.Id = ma.IdAccountClass 
		JOIN GeneralLedger.MainAccountLevels AS mal ON mal.Id = ma.IdAccountLevel 
		JOIN
		(
			SELECT 
				glb.IdMainAccount, glb.IdThirdParty, 
				SUM(glb.InitialDebitValue) AS InitialDebitValue, 
				SUM(glb.InitialCreditValue) AS InitialCreditValue, 
				SUM(glb.DebitValue) AS DebitValue, 
				SUM(glb.CreditValue) AS CreditValue
			FROM 
			(
				SELECT
					IdMainAccount,  IdThirdParty, IdCostCenter,
					IIF(([Year] = @Year and [Month] >= @InitialMonth), 0, DebitValue) AS InitialDebitValue,
					IIF(([Year] = @Year and [Month] >= @InitialMonth), 0, CreditValue) AS InitialCreditValue,
					IIF(([Year] = @Year and [Month] >= @InitialMonth), DebitValue, 0) AS DebitValue,
					IIF(([Year] = @Year and [Month] >= @InitialMonth), CreditValue, 0) AS CreditValue
				FROM GeneralLedger.GeneralLedgerBalance
				WHERE 
				(
					(@IsClosedLastYear = 0 AND (([Year] = (@Year - 1) AND [Month] < 13) OR ([Year] = (@Year - 2) AND [Month] = 14))) OR 
					(@IsClosedLastYear = 1 AND  ([Year] = (@Year - 1) AND [Month] = 14))
				) OR ([Year] = @Year AND [Month] <= @FinalMonth)
			) AS glb 
			WHERE ISNULL(glb.IdThirdParty, 0) <> @CompanyThirdPartyId
			GROUP BY glb.IdMainAccount, glb.IdThirdParty
		) AS glb ON ma.Id = glb.IdMainAccount
		JOIN Common.ThirdParty tp ON glb.IdThirdParty = tp.Id AND tp.ContributionType = 2
		WHERE ma.LegalBookId = @bookId 
			AND ma.ShowCGN2 = 1
			AND ma.Number BETWEEN ISNULL(@accountStart, '0') AND ISNULL(@accountEnd, 'ZZZZZZZZZZZZZZZZZZZZ')
			AND (@levelSubAccount = 0 OR mac.Code NOT IN ('8', '9'))

	-----------------------------------------------------------------------------------------------------------------

	DELETE FROM @tableExecution WHERE balanceCurrent = 0 AND balanceNotCurrent = 0

	-----------------------------------------------------------------------------------------------------------------
	
	SELECT	ISNULL(ma.Id, t.mainAccountId) mainAccountId,
			ISNULL(ma.Number, t.mainAccountCode) mainAccountCode,
			ISNULL(ma.Name, t.mainAccountName) mainAccountName,
			ISNULL(ma.Level, t.LevelAccount) LevelAccount,
			t.thirdPartyId, 
			t.thirdPartyNit,
			t.thirdPartyName,
			ROUND(SUM(t.balanceCurrent), 0) balanceCurrent, 
			ROUND(SUM(t.balanceNotCurrent), 0) balanceNotCurrent
	FROM @TableExecution t
	LEFT JOIN
	(
		SELECT ma.Id, ma.Number, ma.Name, mal.Level
		FROM GeneralLedger.MainAccounts ma
		JOIN GeneralLedger.MainAccountLevels mal ON mal.Id = ma.IdAccountLevel 
		WHERE ma.LegalBookId = @bookId 
			AND mal.Level = 4			
	) ma ON t.mainAccountCode LIKE CONCAT(ma.Number, '%')
	GROUP BY ISNULL(ma.Id, t.mainAccountId), ISNULL(ma.Number, t.mainAccountCode), ISNULL(ma.Name, t.mainAccountName), ISNULL(ma.Level, t.LevelAccount),
			t.thirdPartyId, t.thirdPartyNit, t.thirdPartyName
	ORDER BY ISNULL(ma.Number, t.mainAccountCode), t.thirdPartyName

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte contable CGN-2005-002 (formato oficial de Contaduría General de la Nación) para un libro contable legal en un rango de fechas determinado. Consolida saldos contables por cuenta y tercero, separando el valor en corriente y no corriente según la disponibilidad de la cuenta, y calculando el saldo inicial más los movimientos débito/crédito del período solicitado. Considera si el año anterior fue cerrado para determinar cómo tomar los saldos de apertura, excluye el tercero propio de la empresa (identificado por el NIT DIAN configurado en los parámetros contables) y filtra únicamente los terceros con tipo de contribución 2. El resultado final muestra código, nombre y nivel de cuenta contable, NIT y nombre del tercero, y los saldos redondeados, ordenados para su presentación oficial ante entes de control.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportCGN002';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportCGN002';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte contable CGN-2005-002 (saldos por cuenta y tercero, separados en corriente y no corriente) para un libro legal y rango de cuentas/fechas.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCGN002';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El libro legal debe existir en GeneralLedger.LegalBook para determinar el año de último cierre.; Debe existir registro en GeneralLedger.GeneralLedgerSettings con el IdDian (tercero propio de la compañía) para excluirlo del reporte.; Las cuentas a reportar deben tener la marca ShowCGN2 = 1 y pertenecer al libro indicado.; Los terceros considerados deben tener ContributionType = 2.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCGN002';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca incluye en el reporte movimientos del tercero que representa a la propia compañía (IdDian de GeneralLedgerSettings).; Solo procesa terceros con ContributionType = 2.; Solo incluye cuentas marcadas con ShowCGN2 = 1.; Elimina del resultado las filas con balanceCurrent y balanceNotCurrent ambos en cero.; La clasificación corriente/no corriente es excluyente: si Availability=1 todo el saldo va a corriente, si no, todo va a no corriente.; El rango de cuentas usa por defecto ''0'' a ''ZZZZ...'' cuando no se especifican límites.; Los saldos finales se redondean a entero (cero decimales).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCGN002';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reporte CGN-2005-002 (Contaduría General de la Nación); Libro legal contable; Plan único de cuentas (PUC); Saldo corriente y no corriente; Naturaleza débito/crédito de la cuenta; Cierre anual (Month=14); Tercero (NIT); Tipo de contribución del tercero; Cuentas de orden (clases 8 y 9); Tercero propio de la compañía (IdDian)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCGN002';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas agrupadas por cuenta de nivel 4 (o la propia cuenta si no encuentra padre nivel 4) y tercero, con balanceCurrent y balanceNotCurrent redondeados, ordenadas por número de cuenta y nombre del tercero.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCGN002';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LegalBook.LastYearClose >= (Año(@dateStart)-1) → @IsClosedLastYear = 1: solo se toman saldos iniciales del cierre anual del año anterior (Month=14, Year=@Year-1) else @IsClosedLastYear = 0: se toman saldos del año anterior (Month<13) o cierre del año-2 (Month=14); si MainAccounts.Availability = 1 → El saldo calculado se asigna a balanceCurrent (corriente) else El saldo calculado se asigna a balanceNotCurrent (no corriente); si MainAccounts.Nature = 1 → El saldo se calcula con signo positivo (débito - crédito) else El saldo se invierte multiplicando por -1 (cuentas de naturaleza crédito); si Year = @Year AND Month >= @InitialMonth → Los valores se consideran movimiento del período (DebitValue/CreditValue) else Los valores se consideran saldo inicial (InitialDebitValue/InitialCreditValue); si @levelSubAccount = 1 → Se excluyen las clases de cuentas con Code ''8'' y ''9'' (cuentas de orden) else Se incluyen todas las clases de cuentas', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCGN002';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; GeneralLedger.GeneralLedgerSettings; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; GeneralLedger.MainAccountLevels; GeneralLedger.GeneralLedgerBalance; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCGN002';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCGN002';
-- GO
