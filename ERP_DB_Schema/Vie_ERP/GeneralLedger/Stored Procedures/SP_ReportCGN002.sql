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