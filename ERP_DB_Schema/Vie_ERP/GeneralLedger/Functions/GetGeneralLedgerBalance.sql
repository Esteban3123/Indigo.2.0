-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-17
-- Description:	Obtiene el balance hasta un periodo
-- =============================================
CREATE FUNCTION [GeneralLedger].[GetGeneralLedgerBalance]
(
	@LegalBookType INT,
	@LegalBookId INT,
	@LastYearClose INT,
	@Year INT,
	@InitialMonth INT,
	@FinalMonth INT,
	@IncludeParents BIT
)
RETURNS @GeneralLedgerBalance TABLE 
(
	MainAccountId INT NOT NULL, 
	MainAccountCode VARCHAR(20), 
	MainAccountName VARCHAR(100), 
	MainAccountNature TINYINT, 
	MainAccountAllowsMovement BIT, 
	MainAccountAvailability TINYINT, 
	MainAccountClassType TINYINT, 
	MainAccountClassNature TINYINT, 
	MainAccountClassCode VARCHAR(20), 
	MainAccountClassName VARCHAR(100), 
	MainAccountLevel INT, 
	-----------------------------------------------------------------------------------------------------------------
	ThirdPartyId INT, 
	CostCenterId INT, 
	-----------------------------------------------------------------------------------------------------------------
	ValueDebitInitial DECIMAL(18,2) NOT NULL, 
	ValueCreditInitial DECIMAL(18,2) NOT NULL, 
	-----------------------------------------------------------------------------------------------------------------
	ValueDebitMovement DECIMAL(18,2) NOT NULL, 
	ValueCreditMovement DECIMAL(18,2) NOT NULL
)
AS
BEGIN
	DECLARE @GeneralLedgerBalanceLegalBookId INT = @LegalBookId,
			@IsClosedLastYear BIT = IIf(@LastYearClose >= (@Year - 1), 1, 0)

	IF @LegalBookType = 3
	BEGIN
		SELECT @GeneralLedgerBalanceLegalBookId = Id
		FROM GeneralLedger.LegalBook WITH (NOLOCK)
		WHERE OfficialBook = 1 AND Status = 1
	END

	-----------------------------------------------------------------------------------------------------------------

	INSERT INTO @GeneralLedgerBalance
	SELECT	ma.Id, ma.Number, ma.Name, ma.Nature, ma.AllowsMovement, ma.Availability,
			mac.Type, mac.Nature, mac.Code, mac.Name, mal.Level,
			glb.IdThirdParty, glb.IdCostCenter,
			SUM(IIF(([Year] = @Year and [Month] >= @InitialMonth), 0, DebitValue)) AS InitialDebitValue,
			SUM(IIF(([Year] = @Year and [Month] >= @InitialMonth), 0, CreditValue)) AS InitialCreditValue,
			SUM(IIF(([Year] = @Year and [Month] >= @InitialMonth), DebitValue, 0)) AS DebitValue,
			SUM(IIF(([Year] = @Year and [Month] >= @InitialMonth), CreditValue, 0)) AS CreditValue
	FROM GeneralLedger.MainAccounts AS ma WITH (NOLOCK)
	JOIN GeneralLedger.MainAccountClasses AS mac WITH (NOLOCK) ON ma.IdAccountClass = mac.Id
	JOIN GeneralLedger.MainAccountLevels AS mal WITH (NOLOCK) ON ma.IdAccountLevel = mal.Id
	JOIN GeneralLedger.GeneralLedgerBalance glb WITH (NOLOCK) ON ma.Id = glb.IdMainAccount
	WHERE @GeneralLedgerBalanceLegalBookId = ma.LegalBookId AND
	(
		(
			(@IsClosedLastYear = 0 AND ((glb.[Year] = (@Year - 1) AND glb.[Month] < 13) OR (glb.[Year] = (@Year - 2) AND glb.[Month] = 14))) 
			OR 
			(@IsClosedLastYear = 1 AND  (glb.[Year] = (@Year - 1) AND glb.[Month] = 14))
		) 
		OR 
		(glb.[Year] = @Year AND glb.[Month] <= @FinalMonth)
	)
	GROUP BY ma.Id, ma.Number, ma.Name, ma.Nature, ma.AllowsMovement, ma.Availability,
		mac.Type, mac.Nature, mac.Code, mac.Name, mal.Level,
		glb.IdThirdParty, glb.IdCostCenter

	-----------------------------------------------------------------------------------------------------------------

	IF @LegalBookType = 3
	BEGIN
		UPDATE glb
			SET glb.MainAccountId = ma.Id, 
				glb.MainAccountCode = ma.Number, 
				glb.MainAccountName = ma.Name, 
				glb.MainAccountNature = ma.Nature, 
				glb.MainAccountAllowsMovement = ma.AllowsMovement, 
				glb.MainAccountAvailability = ma.Availability,
				glb.MainAccountClassType = mac.Type, 
				glb.MainAccountClassNature = mac.Nature, 
				glb.MainAccountClassCode = mac.Code, 
				glb.MainAccountClassName = mac.Name, 
				glb.MainAccountLevel = mal.Level
		FROM @GeneralLedgerBalance AS glb
		JOIN GeneralLedger.HomologationAccount ha WITH (NOLOCK) ON glb.MainAccountId = ha.OfficialMainAccountId
		JOIN GeneralLedger.MainAccounts AS ma WITH (NOLOCK) ON ha.MainAccountId = ma.Id
		JOIN GeneralLedger.MainAccountClasses AS mac WITH (NOLOCK) ON ma.IdAccountClass = mac.Id
		JOIN GeneralLedger.MainAccountLevels AS mal WITH (NOLOCK) ON ma.IdAccountLevel = mal.Id
		WHERE ma.LegalBookId = @LegalBookId
	END

	-------------------------------------------------------------------------------------------------------------------

	IF @IncludeParents = 1
	BEGIN
		INSERT INTO @GeneralLedgerBalance 
			SELECT	ma.Id, ma.Number, ma.Name, ma.Nature, ma.AllowsMovement, ma.Availability,
					mac.Type, mac.Nature, mac.Code, mac.Name, mal.Level,
					NULL, NULL,
					0, 0,
					0, 0
			FROM GeneralLedger.MainAccounts AS ma WITH (NOLOCK)
			JOIN GeneralLedger.MainAccountClasses AS mac WITH (NOLOCK) ON mac.Id = ma.IdAccountClass 
			JOIN GeneralLedger.MainAccountLevels AS mal WITH (NOLOCK) ON mal.Id = ma.IdAccountLevel 
			LEFT JOIN @GeneralLedgerBalance glb ON ma.Id = glb.MainAccountId
			WHERE ma.LegalBookId = @LegalBookId AND glb.MainAccountId IS NULL
	END

	RETURN
END