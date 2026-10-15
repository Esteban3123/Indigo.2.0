-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 21/04/2020
-- Description:	Procedimiento para el reporte de estado de resultado
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportResulStatusComparative]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @InitialRangeYear INT,
			@InitialRangeMonthStart INT,
			@InitialRangeMonthEnd INT,
			@FinalRangeYear INT,
			@FinalRangeMonthStart INT,
			@FinalRangeMonthEnd INT,
			@LegalBookId INT,
			@LevelAccount INT,
			@AccountsZero BIT,
			@HandlesCostCenter BIT,
			@Natures VARCHAR(MAX),
			@CostCenters VARCHAR(MAX),
			@BranchOffices VARCHAR(MAX),
			---------------------------------------------------------------------------------------
			@SubInitialXml XML,
			@SubEndXml XML,
			----------------------------------------------------------------------------------------
			@LegalBookCurrency VARCHAR(200),
			@LegalBookCurrencyAbbreviation VARCHAR(200),
			@OfficialCurrencyName VARCHAR(200),
			@OfficialCurrencyAbbreviatio VARCHAR(200)

	--Tabla temporal en donde se almacena el periodo inicial
	CREATE TABLE #Table_ReportResulStatusComparative_Initial
	(
		id INT,
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
		------------------------------------------------------
		LegalBookCurrency VARCHAR(200),
	    LegalBookCurrencyAbbreviation VARCHAR(200)
	)

	--Tabla temporal en donde se almacena el periodo final
	CREATE TABLE #Table_ReportResulStatusComparative_Final
	(
		id INT,
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
		---------------------------------------------
		LegalBookCurrency VARCHAR(200),
	    LegalBookCurrencyAbbreviation VARCHAR(200)
	)

	BEGIN TRY

		/*************************************** CRITERIOS ***************************************/

		SELECT	@InitialRangeYear = t.x.value('InitialRangeYear[1]','int'),
				@InitialRangeMonthStart = t.x.value('InitialRangeMonthStart[1]','int'),
				@InitialRangeMonthEnd = t.x.value('InitialRangeMonthEnd[1]','int'),
				@FinalRangeYear = t.x.value('FinalRangeYear[1]','int'),
				@FinalRangeMonthStart = t.x.value('FinalRangeMonthStart[1]','int'),
				@FinalRangeMonthEnd = t.x.value('FinalRangeMonthEnd[1]','int'),
				@LegalBookId = t.x.value('LegalBookId[1]','int'),
				@LevelAccount = t.x.value('LevelAccount[1]','int'),
				@AccountsZero = t.x.value('AccountsZero[1]','bit'),
				@HandlesCostCenter = t.x.value('HandlesCostCenter[1]','bit'),
				@Natures = t.x.value('Natures[1]','varchar(max)'),
				@CostCenters = t.x.value('CostCenters[1]','varchar(max)'),
				@BranchOffices = t.x.value('BranchOffices[1]','varchar(max)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		-------------------------------------------------------------------------------------------------
		SELECT	@OfficialCurrencyName = c.Name,
				@OfficialCurrencyAbbreviatio =c.Abbreviation
		FROM GeneralLedger.CompanySettings cs
	    join Common.Currency c on c.Id = cs.OfficialCurrencyId

		SELECT  @LegalBookCurrency = ISNULL(C.Name,@OfficialCurrencyName),
				@LegalBookCurrencyAbbreviation = ISNULL(C.Abbreviation,@OfficialCurrencyAbbreviatio)
		FROM GeneralLedger.LegalBook lb
		left join Common.Currency c on c.Id = lb.OfficialCurrencyId
		WHERE lb.Id = @LegalBookId
		--------------------------------------------------------------------------------------------------

		SELECT @SubInitialXml = CONVERT
		(
			XML, 
			(
				SELECT *
				FROM
				( 
					SELECT	@InitialRangeYear InitialRangeYear,
							@InitialRangeMonthStart InitialRangeMonthStart,
							@InitialRangeMonthEnd FinalRangeMonthStart,
							@LegalBookId LegalBookId,
							@LevelAccount LevelAccount,
							@AccountsZero AccountsZero,
							@HandlesCostCenter HandlesCostCenter,
							@Natures Natures,
							@CostCenters CostCenters,
							@BranchOffices BranchOffices
				) Data
				FOR XML AUTO,TYPE, ELEMENTS
			)
		)

		SELECT @SubEndXml = CONVERT
		(
			XML, 
			(
				SELECT *
				FROM
				( 
					SELECT	@FinalRangeYear InitialRangeYear,
							@FinalRangeMonthStart InitialRangeMonthStart,
							@FinalRangeMonthEnd FinalRangeMonthStart,
							@LegalBookId LegalBookId,
							@LevelAccount LevelAccount,
							@AccountsZero AccountsZero,
							@HandlesCostCenter HandlesCostCenter,
							@Natures Natures,
							@CostCenters CostCenters,
							@BranchOffices BranchOffices
				) Data
				FOR XML AUTO,TYPE, ELEMENTS
			)
		)

		-------------------------------------------------------------------------------------------------

		--Se insertan los datos del periodo inicial
		INSERT INTO #Table_ReportResulStatusComparative_Initial
			EXEC [GeneralLedger].[SP_ReportResulStatus] @SubInitialXml
		
		--Se insertan los datos del periodo final
		INSERT INTO #Table_ReportResulStatusComparative_Final
			EXEC [GeneralLedger].[SP_ReportResulStatus] @SubEndXml

		-------------------------------------------------------------------------------------------------

		--Mostramos los resultados	
		SELECT  @LegalBookCurrency LegalBookCurrency,
			    @LegalBookCurrencyAbbreviation LegalBookCurrencyAbbreviation,
				temp.mainAccountCode, 
				temp.mainAccountName,
		 		temp.mainAccountNameByAnual, 
				temp.mainAccountLevel, 
				temp.classCode, 
				temp.availability,
				temp.costCenterCode, temp.costCenterName,
				temp.branchOfficeCode, temp.branchOfficeName,
				SUM(temp.newBalanceInitial) newBalanceInitial, 
				SUM(temp.newBalanceFinal) newBalanceFinal
		FROM
		(	
				SELECT	t.mainAccountCode, 
						t.mainAccountName,
						t.mainAccountNameByAnual, 
						t.mainAccountLevel, 
						t.classCode, 
						t.availability, 
						t.costCenterCode,
						t.costCenterName,
						t.branchOfficeCode,
						t.branchOfficeName,
						t.newBalance newBalanceInitial, 
						0 newBalanceFinal
				FROM #Table_ReportResulStatusComparative_Initial t		
			UNION ALL
				SELECT	t.mainAccountCode,
						t.mainAccountName,
						t.mainAccountNameByAnual, 
						t.mainAccountLevel, 
						t.classCode, 
						t.availability, 
						t.costCenterCode,
						t.costCenterName,
						t.branchOfficeCode,
						t.branchOfficeName,
						0 newBalanceInitial, 
						t.newBalance newBalanceFinal
				FROM #Table_ReportResulStatusComparative_Final t
		) temp
		GROUP BY	temp.mainAccountCode, temp.mainAccountName, temp.mainAccountNameByAnual, 
					temp.mainAccountLevel, temp.classCode, temp.availability,
					temp.costCenterCode, temp.costCenterName,
					temp.branchOfficeCode, temp.branchOfficeName
	END TRY
	BEGIN CATCH
		PRINT CONCAT('Error SP_ReportResulStatusComparative: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
	END CATCH
END