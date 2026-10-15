-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-17
-- Description:	Obtiene el balance hasta un periodo, con los campos requeridos
-- =============================================
CREATE FUNCTION [GeneralLedger].[GetGeneralLedgerBalanceCalculated]
(	
	@LegalBookId INT,
	@Year INT,
	@InitialMonth INT,
	@FinalMonth INT,
	@IsTestBalance BIT,
	@CalculateResultExercise BIT,
	@MainAccountClassNature TINYINT,
	@MainAccountMemorandum BIT,
	@MainAccountInitial VARCHAR(MAX),
	@MainAccountFinal VARCHAR(MAX),
	@ThirdPartyInitial VARCHAR(MAX),
	@ThirdPartyFinal VARCHAR(MAX),
	@CostCenterInitial VARCHAR(MAX),
	@CostCenterFinal VARCHAR(MAX)
)
RETURNS @GeneralLedgerBalance TABLE 
(
	LegalBookName VARCHAR(200) NOT NULL,
	MainAccountId INT NOT NULL, 
	MainAccountCode VARCHAR(20) NOT NULL, 
	MainAccountName VARCHAR(100) NOT NULL, 
	MainAccountNature TINYINT NOT NULL, 
	MainAccountAllowsMovement BIT NOT NULL, 
	MainAccountAvailability TINYINT NOT NULL, 
	MainAccountClassType TINYINT NOT NULL, 
	MainAccountClassCode VARCHAR(20) NOT NULL, 
	MainAccountClassName VARCHAR(100) NOT NULL, 
	MainAccountLevel INT NOT NULL, 
	-----------------------------------------------------------------------------------------------------------------
	ThirdPartyId INT, 
	ThirdPartyNit VARCHAR(20), 
	ThirdPartyName VARCHAR(300), 
	CostCenterId INT, 
	CostCenterCode VARCHAR(20), 
	CostCenterName VARCHAR(200), 
	-----------------------------------------------------------------------------------------------------------------
	ValueDebitInitial DECIMAL(18,2) NOT NULL DEFAULT(0), 
	ValueCreditInitial DECIMAL(18,2) NOT NULL DEFAULT(0), 
	PreviousBalance DECIMAL(18,2) NOT NULL DEFAULT(0), 
	-----------------------------------------------------------------------------------------------------------------
	ValueDebitMovement DECIMAL(18,2) NOT NULL DEFAULT(0), 
	ValueCreditMovement DECIMAL(18,2) NOT NULL DEFAULT(0),
	NewBalance DECIMAL(18,2) NOT NULL DEFAULT(0),
	-----------------------------------------------------------------------------------------------------------------
	ResultExercise DECIMAL(18,2) NOT NULL DEFAULT(0)
)
AS
BEGIN
	/*****************************  VERIFICAMOS EL TIPO DE LIBRO *****************************/

	--Identificar si ya se cerro el año anterior al seleccionado
	DECLARE @LegalBookType TINYINT,
			@LastYearClose INT,
			@LegalBookName VARCHAR(200)

	SELECT	@LegalBookType = lb.TypeBook,
			@LegalBookName = lb.Name,
			@LastYearClose = lb.LastYearClose
	FROM GeneralLedger.LegalBook lb WITH (NOLOCK)
	WHERE lb.Id = @LegalBookId

	--Si es un libro de estados financieros consultamos el libro oficial
	IF @LegalBookType = 3
	BEGIN
		SELECT	@LastYearClose = lb.LastYearClose
		FROM GeneralLedger.LegalBook lb WITH (NOLOCK)
		WHERE lb.OfficialBook = 1 AND lb.Status = 1
	END

	/*********************** OBTENEMOS LOS REGISTROS CONTABLES DEL PERIODO ***********************/

	--Insertamos los movimientos del periodo
	INSERT INTO @GeneralLedgerBalance
		(
			LegalBookName,
			MainAccountId, MainAccountCode, MainAccountName, MainAccountNature, MainAccountAllowsMovement, MainAccountAvailability,
			MainAccountClassType, MainAccountClassCode, MainAccountClassName, MainAccountLevel,
			ThirdPartyId, ThirdPartyNit, ThirdPartyName,
			CostCenterId, CostCenterCode, CostCenterName,
			ValueDebitInitial, ValueCreditInitial, PreviousBalance,
			ValueDebitMovement, ValueCreditMovement, NewBalance
		)
		SELECT 	@LegalBookName,
				glb.MainAccountId, glb.MainAccountCode, glb.MainAccountName, glb.MainAccountNature, glb.MainAccountAllowsMovement, glb.MainAccountAvailability,
				glb.MainAccountClassType, glb.MainAccountClassCode, glb.MainAccountClassName, glb.MainAccountLevel, 
				-----------------------------------------------------------------------------------------------------------------
				tp.Id AS ThirdPartyId, tp.Nit AS ThirdPartyNit, tp.Name AS ThirdPartyName, 
				-----------------------------------------------------------------------------------------------------------------
				cc.Id AS CostCenterId, cc.Code AS CostCenterCode, cc.Name AS CostCenterName, 
				-----------------------------------------------------------------------------------------------------------------
				glb.ValueDebitInitial, glb.ValueCreditInitial, 
				((glb.ValueDebitInitial - glb.ValueCreditInitial) * IIF(glb.MainAccountNature = 1, 1, -1)) PreviousBalance, 
				-----------------------------------------------------------------------------------------------------------------
				glb.ValueDebitMovement, glb.ValueCreditMovement, 
				((glb.ValueDebitInitial - glb.ValueCreditInitial + glb.ValueDebitMovement - glb.ValueCreditMovement) * IIF(glb.MainAccountNature = 1, 1, -1)) NewBalance
		FROM [GeneralLedger].[GetGeneralLedgerBalance](@LegalBookType, @LegalBookId, @LastYearClose, @Year, @InitialMonth, @FinalMonth, 1) glb
		LEFT JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON glb.ThirdPartyId  = tp.Id
		LEFT JOIN Payroll.CostCenter AS cc WITH (NOLOCK) ON glb.CostCenterId = cc.Id
		WHERE	(
					@IsTestBalance = 1
					OR
					(
						glb.MainAccountClassType = 1									-- Cuentas del Balance
						OR 
						(@MainAccountMemorandum = 1 AND glb.MainAccountClassType = 3)	-- Cuentas de Orden
					)
				)
				AND ISNULL(@MainAccountClassNature, glb.MainAccountClassNature) = glb.MainAccountClassNature
				AND glb.MainAccountCode BETWEEN ISNULL(@MainAccountInitial, '0') AND ISNULL(@MainAccountFinal, 'ZZZZZZZZZZZZZZZZZZZZ')
				AND ISNULL(tp.Nit, '0') BETWEEN ISNULL(@ThirdPartyInitial, '0') AND ISNULL(@ThirdPartyFinal, 'ZZZZZZZZZZZZZZZZZZZZ')
				AND ISNULL(cc.Code, '0') BETWEEN ISNULL(@CostCenterInitial, '0') AND ISNULL(@CostCenterFinal, 'ZZZZZZZZZZZZZZZZZZZZ')

	/***************************  CALCULAMOS LA UTILIDAD O PERDIDA ***************************/
    
	IF @CalculateResultExercise = 1 AND ISNULL(@MainAccountClassNature, 2) = 2
	BEGIN
		DECLARE @MainAccountDeficitId INT,
				@MainAccountSuperAvitId INT,
				@ResultExercise DECIMAL(18,2) = 0
		
		SELECT	@MainAccountDeficitId = IdDeficitAccount, 
				@MainAccountSuperAvitId = IdSuperavitAccount 
		FROM GeneralLedger.GeneralLedgerSettings WITH (NOLOCK)

		IF @MainAccountClassNature IS NULL
		BEGIN
			SELECT	@ResultExercise = SUM(glb.ValueDebitInitial - glb.ValueCreditInitial + glb.ValueDebitMovement - glb.ValueCreditMovement)
			FROM @GeneralLedgerBalance glb
			WHERE glb.MainAccountClassType = 1
		END
		ELSE
		BEGIN
			SELECT	@ResultExercise = SUM(glb.ValueDebitInitial - glb.ValueCreditInitial + glb.ValueDebitMovement - glb.ValueCreditMovement)
			FROM [GeneralLedger].[GetGeneralLedgerBalance](@LegalBookType, @LegalBookId, @LastYearClose, @Year, @InitialMonth, @FinalMonth, 1) glb
			WHERE glb.MainAccountClassType = 1
		END

		SET @ResultExercise = ISNULL(@ResultExercise, 0)

		IF EXISTS (SELECT 1 FROM @GeneralLedgerBalance WHERE MainAccountId = IIF(@ResultExercise > 0, @MainAccountSuperAvitId, @MainAccountDeficitId) )
		BEGIN
			UPDATE TOP(1) @GeneralLedgerBalance
				SET ValueDebitMovement += IIF((@ResultExercise > 0), 0, ABS(@ResultExercise)),
					ValueCreditMovement += IIF((@ResultExercise > 0), ABS(@ResultExercise), 0), 
					newBalance += @ResultExercise 
			WHERE MainAccountId = IIF(@ResultExercise > 0, @MainAccountSuperAvitId, @MainAccountDeficitId)
		END
		ELSE BEGIN
			INSERT INTO @GeneralLedgerBalance
				(
					LegalBookName,
					MainAccountId, MainAccountCode, MainAccountName, MainAccountNature, MainAccountAllowsMovement, MainAccountAvailability,
					MainAccountClassType, MainAccountClassCode, MainAccountClassName, MainAccountLevel,
					ValueDebitMovement, ValueCreditMovement, NewBalance
				)
				SELECT	@LegalBookName,
						ma.Id, ma.Number, ma.Name, ma.Nature, ma.AllowsMovement, ma.Availability,
						mac.Type, mac.Code, mac.Name, mal.Level,
						IIF((@ResultExercise > 0), 0, @ResultExercise), IIF((@ResultExercise > 0), @ResultExercise, 0), @ResultExercise
				FROM GeneralLedger.MainAccounts ma WITH (NOLOCK)
				JOIN GeneralLedger.MainAccountClasses mac WITH (NOLOCK) on mac.Id = ma.IdAccountClass 
				JOIN GeneralLedger.MainAccountLevels mal WITH (NOLOCK) on mal.Id = ma.IdAccountLevel
				WHERE ma.Id = IIF((@ResultExercise > 0), @MainAccountSuperAvitId, @MainAccountDeficitId)
					AND ma.LegalBookId = @LegalBookId
		END

		----Actualizamos el resultado del ejercicio
		UPDATE @GeneralLedgerBalance
			SET ResultExercise = @ResultExercise
	END

	/******************************* MAYORIZAMOS LOS REGISTROS *******************************/

	-- Mayorizamos los saldos
	UPDATE ste 
		SET ste.valueDebitInitial = te.valueDebitInitial,
			ste.valueCreditInitial = te.valueCreditInitial,
			ste.PreviousBalance = (te.ValueDebitInitial - te.ValueCreditInitial) * IIF(ste.MainAccountNature = 1, 1, -1),
			ste.valueDebitMovement = ISNULL(te.valueDebitMovement, 0),
			ste.valueCreditMovement = ISNULL(te.valueCreditMovement, 0),
			ste.NewBalance = (te.ValueDebitInitial - te.ValueCreditInitial + te.valueDebitMovement - te.valueCreditMovement) * IIF(ste.MainAccountNature = 1, 1, -1)
	FROM @GeneralLedgerBalance ste
	JOIN
	(
		SELECT
			ste.mainAccountId,
			SUM(te.valueDebitInitial) valueDebitInitial,
			SUM(te.valueCreditInitial) valueCreditInitial,
			SUM(te.valueDebitMovement) valueDebitMovement,
			SUM(te.valueCreditMovement) valueCreditMovement
		FROM @GeneralLedgerBalance ste
		JOIN
		(
			SELECT
				te.mainAccountCode,
				SUM(ISNULL(te.valueDebitInitial,0)) valueDebitInitial,
				SUM(ISNULL(te.valueCreditInitial,0)) valueCreditInitial,
				SUM(ISNULL(te.valueDebitMovement,0)) valueDebitMovement,
				SUM(ISNULL(te.valueCreditMovement,0)) valueCreditMovement
			FROM @GeneralLedgerBalance AS te
			WHERE te.MainAccountAllowsMovement = 1
			GROUP BY te.mainAccountCode
		) te ON te.mainAccountCode LIKE (ste.mainAccountCode + '%')
		WHERE ste.MainAccountAllowsMovement = 0
		GROUP BY ste.mainAccountId
	) te ON ste.mainAccountId = te.mainAccountId

	RETURN
END