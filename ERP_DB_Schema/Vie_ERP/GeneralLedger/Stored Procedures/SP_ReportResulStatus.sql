-- =============================================
-- Author:		Juan Bermudez
-- Create date: 07/04/2016
-- Description:	Procedimiento para el reporte de estado de resultado
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportResulStatus]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @Visualization INT,
			@Year INT,
			@MonthStart INT,
			@MonthEnd INT,
			@LegalBookId INT,
			@LevelAccount INT,
			@AccountsZero BIT,
			@HandlesCostCenter BIT,
			@Natures VARCHAR(MAX),
			@CostCenters VARCHAR(MAX),
			@BranchOffices VARCHAR(MAX),
			---------------------------------------------------------------------------------------
			@FilterByNature BIT = 0,
			@FilterByCostCenter BIT = 0,
			@FilterByBranchOffice BIT = 0,
			---------------------------------------------------------------------------------------
			--Identificar si ya se cerro el año anterior al seleccionado
			@LastYearClose INT, 
			@LegalBookType TINYINT, 
			@IsClosedLastYear BIT,
			@LegalBookCurrency VARCHAR(200),
			@LegalBookCurrencyAbbreviation VARCHAR(200),
			@OfficialCurrencyName VARCHAR(200),
			@OfficialCurrencyAbbreviatio VARCHAR(200)
	
	DECLARE @Table_Natures AS TABLE(Nature INT)
	DECLARE @Table_CostCenters AS TABLE(CostCenterId INT)
	DECLARE @Table_BranchOffices AS TABLE(BranchOfficeId INT)

	-- Creamos la tabla temporal que devolveremos con los datos
	CREATE TABLE #Table_ReportResulStatus
	(
		id INT IDENTITY(1,1) PRIMARY KEY,
		mainAccountId INT,
		mainAccountCode VARCHAR(20) INDEX IX1 CLUSTERED,
		mainAccountName VARCHAR(MAX),
		nature VARCHAR(10), 
		thirdPartyId INT, 
		thirdPartyNit VARCHAR(20), 
		thirdPartyName VARCHAR(MAX),
		costCenterId INT, 
		costCenterCode VARCHAR(20),
		costCenterName VARCHAR(MAX), 
		branchOfficeId INT, 
		branchOfficeCode VARCHAR(20),
		branchOfficeName VARCHAR(MAX), 
		valueDebitInitial DECIMAL(20,2), 
		valueCreditInitial DECIMAL(20,2),
		previousBalance DECIMAL(20,2), 
		valueDebitMovement DECIMAL(20,2), 
		valueCreditMovement DECIMAL(20,2), 
		newBalance DECIMAL(20,2),
		---------------------------------------------------
		movementMonthStart DECIMAL(20,2), 
		movementMonthIntermediate DECIMAL(20,2), 
		movementMonthEnd DECIMAL(20,2), 
		---------------------------------------------------		
		mainAccountClassType INT, 
		mainAccountLevel INT, 
		allowsMovement BIT, 
		classCode VARCHAR(20),
		availability TINYINT,
		mainAccountNameByAnual VARCHAR(MAX),
		-----------------------------------------------------
		LegalBookCurrency VARCHAR(200),
	    LegalBookCurrencyAbbreviation VARCHAR(200)
	)

	BEGIN TRY

		/*************************************** CRITERIOS ***************************************/

		SELECT	@Visualization = t.x.value('Visualization[1]','int'),
				@Year = t.x.value('InitialRangeYear[1]','int'),
				@MonthStart = t.x.value('InitialRangeMonthStart[1]','int'),
				@MonthEnd = t.x.value('FinalRangeMonthStart[1]','int'),
				@LegalBookId = t.x.value('LegalBookId[1]','int'),
				@LevelAccount = t.x.value('LevelAccount[1]','int'),
				@AccountsZero = t.x.value('AccountsZero[1]','bit'),
				@HandlesCostCenter = t.x.value('HandlesCostCenter[1]','bit'),
				@Natures = t.x.value('Natures[1]','varchar(max)'),
				@CostCenters = t.x.value('CostCenters[1]','varchar(max)'),
				@BranchOffices = t.x.value('BranchOffices[1]','varchar(max)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		-------------------------------------------------------------------------------------------------

		IF ISNULL(@Natures, '') <> ''
		BEGIN
			SET @FilterByNature = 1

			INSERT INTO @Table_Natures
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@Natures, ',')
		END

		IF ISNULL(@CostCenters, '') <> ''
		BEGIN
			SET @FilterByCostCenter = 1

			INSERT INTO @Table_CostCenters
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@CostCenters, ',')
		END

		IF ISNULL(@BranchOffices, '') <> ''
		BEGIN
			SET @FilterByBranchOffice = 1

			INSERT INTO @Table_BranchOffices
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@BranchOffices, ',')
		END

		/********************************** OBTENCION DE DATOS **********************************/
		SELECT	@OfficialCurrencyName = c.Name,
				@OfficialCurrencyAbbreviatio =c.Abbreviation
		FROM GeneralLedger.CompanySettings cs
	    join Common.Currency c on c.Id = cs.OfficialCurrencyId

		SELECT	@LastYearClose = LastYearClose, 
				@LegalBookType = TypeBook,
				@LegalBookCurrency = ISNULL(C.Name,@OfficialCurrencyName),
				@LegalBookCurrencyAbbreviation = ISNULL(C.Abbreviation,@OfficialCurrencyAbbreviatio)
		FROM GeneralLedger.LegalBook lb
		left join Common.Currency c on c.Id = lb.OfficialCurrencyId
		WHERE lb.Id = @LegalBookId

		--Si es un libro de estados financieros consultamos el libro oficial
		IF @LegalBookType = 3
		BEGIN
			SELECT @LastYearClose = LastYearClose
			FROM GeneralLedger.LegalBook
			WHERE OfficialBook = 1 AND Status = 1
		END

		SET @IsClosedLastYear = IIF(@LastYearClose >= (@Year - 1), 1, 0)
		
		--Insertamos los movimientos que hasta la fecha final dada
		INSERT INTO #Table_ReportResulStatus
			(
				mainAccountId, mainAccountCode, mainAccountName, nature, 
				thirdPartyId, thirdPartyNit, thirdPartyName,
				costCenterId, costCenterCode, costCenterName, 
				branchOfficeId, branchOfficeCode, branchOfficeName,
				valueDebitInitial, valueCreditInitial, previousBalance, 
				valueDebitMovement, valueCreditMovement, newBalance,
				movementMonthStart, movementMonthIntermediate, movementMonthEnd,
				mainAccountClassType, mainAccountLevel, allowsMovement, classCode, 
				availability, LegalBookCurrency, LegalBookCurrencyAbbreviation
			)
			SELECT 	
				ma.Id AS mainAccountId, ma.Number AS mainAccountCode, ma.Name AS mainAccountName, IIF(ma.Nature = 1, 'Debito', 'Credito') AS nature, 
				glb.IdThirdParty AS thirdPartyId, tp.Nit AS thirdPartyNit, tp.Name AS thirdPartyName, 
				glb.IdCostCenter AS costCenterId, cc.Code as costCenterCode, cc.Name as costCenterName, 
				bo.Id AS branchOfficeId, bo.Code as branchOfficeCode, bo.Name as branchOfficeName, 
				ISNULL(glb.DebitInitial, 0) AS valueDebitInitial, ISNULL(glb.CreditInitial, 0) AS valueCreditInitial, CAST(0 AS DECIMAL(20,4)) newBalance,
				ISNULL(glb.Debit, 0) AS DebitoMovimiento, ISNULL(glb.Credit, 0) AS CreditoMovimiento, CAST(0 AS DECIMAL(20,4)) Saldo,
				ISNULL(glb.MovementMonthStart, 0) AS movementMonthStart, ISNULL(glb.MovementMonthIntermediate, 0) AS movementMonthIntermediate, ISNULL(glb.MovementMonthEnd, 0) AS movementMonthEnd,
				mac.Type AS mainAccountClassType, mal.Level AS mainAccountLevel, ma.AllowsMovement AS allowsMovement, mac.Code classCode, ma.Availability, @LegalBookCurrency, @LegalBookCurrencyAbbreviation
			FROM GeneralLedger.MainAccounts AS ma WITH (NOLOCK) 
			JOIN GeneralLedger.MainAccountClasses AS mac WITH (NOLOCK) ON mac.Id = ma.IdAccountClass 
			JOIN GeneralLedger.MainAccountLevels AS mal WITH (NOLOCK) ON mal.Id = ma.IdAccountLevel 
			LEFT JOIN GeneralLedger.HomologationAccount ha WITH (NOLOCK) ON ma.Id = ha.MainAccountId AND @LegalBookType = 3
			LEFT JOIN
			(
				SELECT 
					glb.IdMainAccount as IdMainAccount, glb.IdThirdParty, glb.IdCostCenter, 
					SUM(glb.DebitInitial) AS DebitInitial, 
					SUM(glb.CreditInitial) AS CreditInitial, 
					SUM(glb.Debit) AS Debit, 
					SUM(glb.Credit) AS Credit,
					---------------------------------------------------
					SUM(glb.MovementMonthStart) AS MovementMonthStart, 
					SUM(glb.MovementMonthIntermediate) AS MovementMonthIntermediate,
					SUM(glb.MovementMonthEnd) AS MovementMonthEnd
				FROM 
				(
					SELECT
						IdMainAccount,  IdThirdParty, IdCostCenter,
						IIF(([Year] = @Year and [Month] >= @MonthStart), 0, DebitValue) AS DebitInitial,
						IIF(([Year] = @Year and [Month] >= @MonthStart), 0, CreditValue) AS CreditInitial,
						IIF(([Year] = @Year and [Month] >= @MonthStart), DebitValue, 0) AS Debit,
						IIF(([Year] = @Year and [Month] >= @MonthStart), CreditValue, 0) AS Credit,
						---------------------------------------------------
						IIF((@Visualization = 2 AND [Year] = @Year and [Month] = @MonthStart), CreditValue - DebitValue, 0) AS MovementMonthStart,
						IIF((@Visualization = 2 AND [Year] = @Year and [Month] = (@MonthStart + 1)), CreditValue - DebitValue, 0) AS MovementMonthIntermediate,
						IIF((@Visualization = 2 AND [Year] = @Year and [Month] = @MonthEnd), CreditValue - DebitValue, 0) AS MovementMonthEnd
					FROM GeneralLedger.GeneralLedgerBalance WITH (NOLOCK)
					WHERE 
					(
						(@IsClosedLastYear = 0 AND (([Year] = (@Year - 1) AND [Month] < 13) OR ([Year] = (@Year - 2) AND [Month] = 14))) OR 
						(@IsClosedLastYear = 1 AND  ([Year] = (@Year - 1) AND [Month] = 14))
					) OR ([Year] = @Year AND [Month] <= @MonthEnd)
				) AS glb 
				GROUP BY glb.IdMainAccount, glb.IdThirdParty, glb.IdCostCenter
			) AS glb on (@LegalBookType <> 3 AND ma.Id = glb.IdMainAccount) OR (@LegalBookType = 3 AND ha.OfficialMainAccountId = glb.IdMainAccount)
			LEFT JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON tp.Id = glb.IdThirdParty
			LEFT JOIN Payroll.CostCenter AS cc WITH (NOLOCK) ON cc.Id = glb.IdCostCenter
			LEFT JOIN Payroll.BranchOffice AS bo WITH (NOLOCK) ON cc.BranchOfficeId = bo.Id
			/**************************************** FILTROS ****************************************/
			LEFT JOIN @Table_Natures tn ON ma.Nature = tn.Nature
			LEFT JOIN @Table_CostCenters tcc ON cc.Id = tcc.CostCenterId
			LEFT JOIN @Table_BranchOffices tbo ON bo.Id = tbo.BranchOfficeId
			WHERE ma.LegalBookId = @LegalBookId AND mac.Type = 2
				AND (@FilterByNature = 0 OR tn.Nature IS NOT NULL)
				AND (@FilterByCostCenter = 0 OR tcc.CostCenterId IS NOT NULL)
				AND (@FilterByBranchOffice = 0 OR tbo.BranchOfficeId IS NOT NULL)
			ORDER BY ma.Number

		--Mayorizamos los saldos a nivel superior
		INSERT INTO #Table_ReportResulStatus 
			(
				mainAccountId, mainAccountCode, mainAccountName, nature, 
				thirdPartyId, thirdPartyNit, thirdPartyName,
				costCenterId, costCenterCode, costCenterName, 
				branchOfficeId, branchOfficeCode, branchOfficeName,
				valueDebitInitial, valueCreditInitial, previousBalance, 
				valueDebitMovement, valueCreditMovement, newBalance,
				movementMonthStart, movementMonthIntermediate, movementMonthEnd,
				mainAccountClassType, mainAccountLevel, allowsMovement, classCode, availability, mainAccountNameByAnual,
				LegalBookCurrency, LegalBookCurrencyAbbreviation
			)
			SELECT 	
				ma.Id AS mainAccountId, ma.Number AS mainAccountCode, ma.Name AS mainAccountName, IIF(ma.Nature = 1, 'Debito', 'Credito') AS nature,
				tp.Id AS thirdPartyId, tp.Nit AS thirdPartyNit, tp.Name AS thirdPartyName,
				cc.Id AS costCenterId, cc.Code AS costCenterCode, cc.Name AS costCenterName,
				bo.Id AS branchOfficeId, bo.Code as branchOfficeCode, bo.Name as branchOfficeName, 
				SUM(te.valueDebitInitial) valueDebitInitial, SUM(te.valueCreditInitial) valueCreditInitial, CAST(0 AS DECIMAL(20,4)) previousBalance,
				SUM(te.valueDebitMovement) valueDebitMovement, SUM(te.valueCreditMovement) valueCreditMovement, CAST(0 AS DECIMAL(20,4)) newBalance,
				SUM(te.movementMonthStart) movementMonthStart, SUM(te.movementMonthIntermediate) movementMonthIntermediate, SUM(te.movementMonthEnd) movementMonthEnd,
				mac.Type AS mainAccountClassType, ma.IdAccountLevel AS mainAccountLevel, ma.AllowsMovement AS allowsMovement, mac.Code classCode, 
				te.Availability, ma.Name + IIF((ma.Number = '4' OR ma.Number = '5') and te.Availability = 4, ' OPERACIONALES', IIF((ma.Number = '4' OR ma.Number = '5') and te.Availability = 5, ' NO OPERACIONALES', '')),
				@LegalBookCurrency, @LegalBookCurrencyAbbreviation
			FROM #Table_ReportResulStatus AS te
			JOIN GeneralLedger.MainAccounts AS ma WITH (NOLOCK) ON te.mainAccountId <> ma.Id AND te.mainAccountCode LIKE CONCAT(ma.Number, '%')  COLLATE DATABASE_DEFAULT
			JOIN GeneralLedger.MainAccountClasses AS mac WITH (NOLOCK) ON mac.Id = ma.IdAccountClass 
			JOIN GeneralLedger.MainAccountLevels AS mal WITH (NOLOCK) ON mal.Id = ma.IdAccountLevel 
			LEFT JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON tp.Id = te.thirdPartyId
			LEFT JOIN Payroll.CostCenter AS cc WITH (NOLOCK) ON cc.Id = te.costCenterId
			LEFT JOIN Payroll.BranchOffice AS bo WITH (NOLOCK) ON cc.BranchOfficeId = bo.Id
			WHERE ma.LegalBookId = @LegalBookId
			GROUP BY ma.Id, ma.Number, ma.Name, ma.Nature,
				tp.Id, tp.Nit, tp.Name,
				cc.Id, cc.Code, cc.Name,
				bo.Id, bo.Code, bo.Name,
				mac.Type, ma.IdAccountLevel, ma.AllowsMovement, mac.Code, te.Availability

		--Actualizamos los saldos
		UPDATE #Table_ReportResulStatus
			SET PreviousBalance = (ValueDebitInitial - ValueCreditInitial) * IIF(Nature = 'Debito', 1, -1),
				NewBalance = (ValueDebitInitial - ValueCreditInitial + ValueDebitMovement - ValueCreditMovement) * IIF(Nature = 'Debito', 1, -1)

		IF @AccountsZero = 0
		BEGIN
			DELETE FROM #Table_ReportResulStatus Where valueDebitInitial = 0 AND valueCreditInitial = 0 AND previousBalance = 0 AND valueDebitMovement = 0 AND valueCreditMovement = 0 AND newBalance = 0
		END
	END TRY
	BEGIN CATCH	
		PRINT CONCAT('Error SP_ReportResulStatus: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
	END CATCH

	--Mostramos los resultados
	SELECT * 
	FROM #Table_ReportResulStatus
	WHERE mainAccountLevel <= @LevelAccount
	ORDER BY mainAccountCode, thirdPartyNit, costCenterCode	
END