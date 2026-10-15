-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-17
-- Description:	Procedimiento para el reporte de balance general comparativo
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportGeneralBalanceComparative]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @YearInitial INT,
			@MonthInitial INT,
			@YearFinal INT,
			@MonthFinal INT,
			@LegalBookId INT,
			@AccountLevel TINYINT,
			@Nature TINYINT,
			@DetailingThird BIT,
			@DetailingCostCenter BIT = 0,
			@MemorandumAccounts BIT,
			@AccountsZero BIT,
			@CostCenterInitial VARCHAR(MAX),
			@CostCenterFinal VARCHAR(MAX)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT	@YearInitial = t.x.value('YearInitial[1]','int'),
				@MonthInitial = t.x.value('MonthInitial[1]','int'),
				@YearFinal = t.x.value('YearFinal[1]','int'),
				@MonthFinal = t.x.value('MonthFinal[1]','int'),
				@LegalBookId = t.x.value('LegalBookId[1]','int'),
				@AccountLevel = t.x.value('AccountLevel[1]','tinyint'),				
				@Nature = t.x.value('Nature[1]','tinyint'),				
				@DetailingThird = t.x.value('DetailingThird[1]','bit'),
				@MemorandumAccounts = t.x.value('MemorandumAccounts[1]','bit'),
				@AccountsZero = t.x.value('AccountsZero[1]','bit'),
				@CostCenterInitial = t.x.value('CostCenterInitial[1]','varchar(max)'),
				@CostCenterFinal = t.x.value('CostCenterFinal[1]','varchar(max)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		SELECT	@CostCenterInitial = IIF(ISNULL(@CostCenterInitial, '') = '', '0', @CostCenterInitial),
				@CostCenterFinal = IIF(ISNULL(@CostCenterFinal, '') = '', 'ZZZZZZZZZZZZZZZZZZZZ', @CostCenterFinal)

		/***************** OBTENEMOS LOS REGISTROS CONTABLES DEL PERIODO INICIAL *****************/

		--Insertamos los movimientos del periodo inicial
			SELECT 	glb.LegalBookName,
					glb.MainAccountId, glb.MainAccountCode, glb.MainAccountName, glb.MainAccountNature, glb.MainAccountAllowsMovement, glb.MainAccountAvailability,
					glb.MainAccountClassType, glb.MainAccountClassCode, glb.MainAccountClassName, glb.MainAccountLevel, 
					-----------------------------------------------------------------------------------------------------------------
					glb.ThirdPartyId, glb.ThirdPartyNit, glb.ThirdPartyName, 
					-----------------------------------------------------------------------------------------------------------------
					glb.CostCenterId, glb.CostCenterCode, glb.CostCenterName, 
					-----------------------------------------------------------------------------------------------------------------
					glb.ValueDebitInitial, glb.ValueCreditInitial, glb.PreviousBalance, 
					-----------------------------------------------------------------------------------------------------------------
					glb.ValueDebitMovement, glb.ValueCreditMovement, glb.NewBalance,
					-----------------------------------------------------------------------------------------------------------------
					glb.ResultExercise
			INTO #SP_ReportGeneralBalanceInitialPeriod
			FROM [GeneralLedger].[GetGeneralLedgerBalanceCalculated](@LegalBookId, @YearInitial, @MonthInitial, @MonthInitial, 0, 1, @Nature, @MemorandumAccounts, NULL, NULL, NULL, NULL, @CostCenterInitial, @CostCenterFinal) glb
			ORDER BY glb.MainAccountCode

		/************************************ APLICAMOS LOS FILTROS ************************************/

		--Filtros del reporte
		IF @AccountsZero = 0 
		BEGIN
			IF @DetailingCostCenter = 0 and @DetailingThird = 0 
			BEGIN			
				DELETE e 
				FROM #SP_ReportGeneralBalanceInitialPeriod e 
				JOIN 
				(
					SELECT mainAccountId 
					FROM #SP_ReportGeneralBalanceInitialPeriod
					GROUP BY mainAccountId 
					HAVING SUM(newBalance) = 0
				) as data ON data.mainAccountId = e.mainAccountId
			END
			IF @DetailingCostCenter = 1 and @DetailingThird = 1 
			BEGIN			
				DELETE e 
				FROM #SP_ReportGeneralBalanceInitialPeriod e 
				JOIN 
				(
					SELECT mainAccountId, thirdPartyId,  costCenterId 
					FROM #SP_ReportGeneralBalanceInitialPeriod
					GROUP BY mainAccountId, thirdPartyId,  costCenterId
					HAVING SUM(newBalance) = 0
				) as data ON data.mainAccountId = e.mainAccountId 
					AND ISNULL(data.thirdPartyId, 0) = ISNULL(e.thirdPartyId, 0)
					AND ISNULL(data.costCenterId, 0) = ISNULL(e.costCenterId, 0)
			END
			ELSE IF @DetailingCostCenter = 1 
			BEGIN			
				DELETE e 
				FROM #SP_ReportGeneralBalanceInitialPeriod e 
				JOIN 
				(
					SELECT mainAccountId, costCenterId 
					FROM #SP_ReportGeneralBalanceInitialPeriod 
					GROUP BY mainAccountId, costCenterId 
					HAVING SUM(newBalance) = 0
				) as data on data.mainAccountId = e.mainAccountId AND ISNULL(data.costCenterId, 0) = ISNULL(e.costCenterId, 0)
			END
			ELSE IF @DetailingThird = 1 
			BEGIN			
				DELETE e 
				FROM #SP_ReportGeneralBalanceInitialPeriod e 
				JOIN 
				(
					SELECT mainAccountId, thirdPartyId 
					FROM #SP_ReportGeneralBalanceInitialPeriod 
					GROUP BY mainAccountId, thirdPartyId 
					HAVING SUM(newBalance) = 0
				) as data on data.mainAccountId = e.mainAccountId AND ISNULL(data.thirdPartyId, 0) = ISNULL(e.thirdPartyId, 0)
			END
		END

		/****************** OBTENEMOS LOS REGISTROS CONTABLES DEL PERIODO FINAL ******************/

		--Insertamos los movimientos del periodo inicial
			SELECT 	glb.LegalBookName,
					glb.MainAccountId, glb.MainAccountCode, glb.MainAccountName, glb.MainAccountNature, glb.MainAccountAllowsMovement, glb.MainAccountAvailability,
					glb.MainAccountClassType, glb.MainAccountClassCode, glb.MainAccountClassName, glb.MainAccountLevel, 
					-----------------------------------------------------------------------------------------------------------------
					glb.ThirdPartyId, glb.ThirdPartyNit, glb.ThirdPartyName, 
					-----------------------------------------------------------------------------------------------------------------
					glb.CostCenterId, glb.CostCenterCode, glb.CostCenterName, 
					-----------------------------------------------------------------------------------------------------------------
					glb.ValueDebitInitial, glb.ValueCreditInitial, glb.PreviousBalance, 
					-----------------------------------------------------------------------------------------------------------------
					glb.ValueDebitMovement, glb.ValueCreditMovement, glb.NewBalance,
					-----------------------------------------------------------------------------------------------------------------
					glb.ResultExercise
			INTO #SP_ReportGeneralBalanceFinalPeriod
			FROM [GeneralLedger].[GetGeneralLedgerBalanceCalculated](@LegalBookId, @YearFinal, @MonthFinal, @MonthFinal, 0, 1, @Nature, @MemorandumAccounts, NULL, NULL, NULL, NULL, @CostCenterInitial, @CostCenterFinal) glb
			ORDER BY glb.MainAccountCode

		/************************************ APLICAMOS LOS FILTROS ************************************/

		--Filtros del reporte
		IF @AccountsZero = 0 
		BEGIN
			IF @DetailingCostCenter = 0 and @DetailingThird = 0 
			BEGIN			
				DELETE e 
				FROM #SP_ReportGeneralBalanceFinalPeriod e 
				JOIN 
				(
					SELECT mainAccountId 
					FROM #SP_ReportGeneralBalanceFinalPeriod
					GROUP BY mainAccountId 
					HAVING SUM(newBalance) = 0
				) as data ON data.mainAccountId = e.mainAccountId
			END
			IF @DetailingCostCenter = 1 and @DetailingThird = 1 
			BEGIN			
				DELETE e 
				FROM #SP_ReportGeneralBalanceFinalPeriod e 
				JOIN 
				(
					SELECT mainAccountId, thirdPartyId,  costCenterId 
					FROM #SP_ReportGeneralBalanceFinalPeriod
					GROUP BY mainAccountId, thirdPartyId,  costCenterId
					HAVING SUM(newBalance) = 0
				) as data ON data.mainAccountId = e.mainAccountId 
					AND ISNULL(data.thirdPartyId, 0) = ISNULL(e.thirdPartyId, 0)
					AND ISNULL(data.costCenterId, 0) = ISNULL(e.costCenterId, 0)
			END
			ELSE IF @DetailingCostCenter = 1 
			BEGIN			
				DELETE e 
				FROM #SP_ReportGeneralBalanceFinalPeriod e 
				JOIN 
				(
					SELECT mainAccountId, costCenterId 
					FROM #SP_ReportGeneralBalanceFinalPeriod 
					GROUP BY mainAccountId, costCenterId 
					HAVING SUM(newBalance) = 0
				) as data on data.mainAccountId = e.mainAccountId AND ISNULL(data.costCenterId, 0) = ISNULL(e.costCenterId, 0)
			END
			ELSE IF @DetailingThird = 1 
			BEGIN			
				DELETE e 
				FROM #SP_ReportGeneralBalanceFinalPeriod e 
				JOIN 
				(
					SELECT mainAccountId, thirdPartyId 
					FROM #SP_ReportGeneralBalanceFinalPeriod 
					GROUP BY mainAccountId, thirdPartyId 
					HAVING SUM(newBalance) = 0
				) as data on data.mainAccountId = e.mainAccountId AND ISNULL(data.thirdPartyId, 0) = ISNULL(e.thirdPartyId, 0)
			END
		END
    
		/*******************************  MOSTRAMOS LOS RESULTADOS *******************************/

		--Mostramos los resultados
		SELECT	ISNULL(gbip.LegalBookName, gbfp.LegalBookName) LegalBookName, 
				ISNULL(gbip.MainAccountId, gbfp.MainAccountId) MainAccountId, 
				ISNULL(gbip.MainAccountCode, gbfp.MainAccountCode) MainAccountCode, 
				ISNULL(gbip.MainAccountName, gbfp.MainAccountName) MainAccountName, 
				ISNULL(gbip.MainAccountNature, gbfp.MainAccountNature) MainAccountNature, 
				ISNULL(gbip.MainAccountAllowsMovement, gbfp.MainAccountAllowsMovement) MainAccountAllowsMovement,
				ISNULL(gbip.MainAccountAvailability, gbfp.MainAccountAvailability) MainAccountAvailability,
				ISNULL(gbip.MainAccountClassType, gbfp.MainAccountClassType) MainAccountClassType, 
				ISNULL(gbip.MainAccountClassCode, gbfp.MainAccountClassCode) MainAccountClassCode, 
				ISNULL(gbip.MainAccountClassName, gbfp.MainAccountClassName) MainAccountClassName, 
				ISNULL(gbip.MainAccountLevel, gbfp.MainAccountLevel) MainAccountLevel,
				ISNULL(gbip.ThirdPartyId, gbfp.ThirdPartyId) ThirdPartyId, 
				ISNULL(gbip.ThirdPartyNit, gbfp.ThirdPartyNit) ThirdPartyNit, 
				ISNULL(gbip.ThirdPartyName, gbfp.ThirdPartyName) ThirdPartyName,
				ISNULL(gbip.CostCenterId, gbfp.CostCenterId) CostCenterId, 
				ISNULL(gbip.CostCenterCode, gbfp.CostCenterCode) CostCenterCode, 
				ISNULL(gbip.CostCenterName, gbfp.CostCenterName) CostCenterName,
				-------------------------------------------------------------------------------------------------
				ISNULL(gbip.ValueDebitInitial, 0) InitialPeriodValueDebitInitial, 
				ISNULL(gbip.ValueCreditInitial, 0) InitialPeriodValueCreditInitial, 
				ISNULL(gbip.PreviousBalance, 0) InitialPeriodPreviousBalance,
				ISNULL(gbip.ValueDebitMovement, 0) InitialPeriodValueDebitMovement, 
				ISNULL(gbip.ValueCreditMovement, 0) InitialPeriodValueCreditMovement, 
				ISNULL(gbip.NewBalance, 0) InitialPeriodNewBalance,
				ISNULL(gbip.ResultExercise, 0) InitialPeriodResultExercise,
				-------------------------------------------------------------------------------------------------
				ISNULL(gbfp.ValueDebitInitial, 0) FinalPeriodValueDebitInitial, 
				ISNULL(gbfp.ValueCreditInitial, 0) FinalPeriodValueCreditInitial, 
				ISNULL(gbfp.PreviousBalance, 0) FinalPeriodPreviousBalance,
				ISNULL(gbfp.ValueDebitMovement, 0) FinalPeriodValueDebitMovement, 
				ISNULL(gbfp.ValueCreditMovement, 0) FinalPeriodValueCreditMovement, 
				ISNULL(gbfp.NewBalance, 0) FinalPeriodNewBalance,
				ISNULL(gbfp.ResultExercise, 0) FinalPeriodResultExercise
		FROM
		(
			SELECT	glb.LegalBookName,
					glb.MainAccountId, glb.MainAccountCode, glb.MainAccountName, glb.MainAccountNature, glb.MainAccountAllowsMovement, glb.MainAccountAvailability,
					glb.MainAccountClassType, glb.MainAccountClassCode, glb.MainAccountClassName, glb.MainAccountLevel, 
					-----------------------------------------------------------------------------------------------------------------
					IIF(@DetailingThird = 0, NULL, glb.ThirdPartyId) ThirdPartyId, IIF(@DetailingThird = 0, NULL, glb.ThirdPartyNit) ThirdPartyNit, IIF(@DetailingThird = 0, NULL, glb.ThirdPartyName) ThirdPartyName,
					IIF(@DetailingCostCenter = 0, NULL, glb.CostCenterId) CostCenterId, IIF(@DetailingCostCenter = 0, NULL, glb.CostCenterCode) CostCenterCode, IIF(@DetailingCostCenter = 0, NULL, glb.CostCenterName) CostCenterName,
					SUM(glb.ValueDebitInitial) ValueDebitInitial, SUM(glb.ValueCreditInitial) ValueCreditInitial, SUM(glb.PreviousBalance) PreviousBalance,
					SUM(glb.ValueDebitMovement) ValueDebitMovement, SUM(glb.ValueCreditMovement) ValueCreditMovement, SUM(glb.NewBalance) NewBalance,
					glb.ResultExercise
			FROM #SP_ReportGeneralBalanceInitialPeriod glb
			WHERE (
					mainAccountLevel = @accountLevel 
					OR  
					(
						@accountLevel = 5 AND mainAccountLevel >= 5
					)
				  )
			GROUP BY glb.LegalBookName,
					glb.MainAccountId, glb.MainAccountCode, glb.MainAccountName, glb.MainAccountNature, glb.MainAccountAllowsMovement, glb.MainAccountAvailability,
					glb.MainAccountClassType, glb.MainAccountClassCode, glb.MainAccountClassName, glb.MainAccountLevel, 
					IIF(@DetailingThird = 0, NULL, glb.ThirdPartyId), IIF(@DetailingThird = 0, NULL, glb.ThirdPartyNit), IIF(@DetailingThird = 0, NULL, glb.ThirdPartyName),
					IIF(@DetailingCostCenter = 0, NULL, glb.CostCenterId), IIF(@DetailingCostCenter = 0, NULL, glb.CostCenterCode), IIF(@DetailingCostCenter = 0, NULL, glb.CostCenterName),
					glb.ResultExercise
		) gbip
		FULL JOIN
		(
			SELECT	glb.LegalBookName,
					glb.MainAccountId, glb.MainAccountCode, glb.MainAccountName, glb.MainAccountNature, glb.MainAccountAllowsMovement, glb.MainAccountAvailability,
					glb.MainAccountClassType, glb.MainAccountClassCode, glb.MainAccountClassName, glb.MainAccountLevel, 
					-----------------------------------------------------------------------------------------------------------------
					IIF(@DetailingThird = 0, NULL, glb.ThirdPartyId) ThirdPartyId, IIF(@DetailingThird = 0, NULL, glb.ThirdPartyNit) ThirdPartyNit, IIF(@DetailingThird = 0, NULL, glb.ThirdPartyName) ThirdPartyName,
					IIF(@DetailingCostCenter = 0, NULL, glb.CostCenterId) CostCenterId, IIF(@DetailingCostCenter = 0, NULL, glb.CostCenterCode) CostCenterCode, IIF(@DetailingCostCenter = 0, NULL, glb.CostCenterName) CostCenterName,
					SUM(glb.ValueDebitInitial) ValueDebitInitial, SUM(glb.ValueCreditInitial) ValueCreditInitial, SUM(glb.PreviousBalance) PreviousBalance,
					SUM(glb.ValueDebitMovement) ValueDebitMovement, SUM(glb.ValueCreditMovement) ValueCreditMovement, SUM(glb.NewBalance) NewBalance,
					glb.ResultExercise
			FROM #SP_ReportGeneralBalanceFinalPeriod glb
			WHERE (
					mainAccountLevel = @accountLevel 
					OR  
					(
						@accountLevel = 5 AND mainAccountLevel >= 5
					)
				  )
			GROUP BY glb.LegalBookName,
					glb.MainAccountId, glb.MainAccountCode, glb.MainAccountName, glb.MainAccountNature, glb.MainAccountAllowsMovement, glb.MainAccountAvailability,
					glb.MainAccountClassType, glb.MainAccountClassCode, glb.MainAccountClassName, glb.MainAccountLevel, 
					IIF(@DetailingThird = 0, NULL, glb.ThirdPartyId), IIF(@DetailingThird = 0, NULL, glb.ThirdPartyNit), IIF(@DetailingThird = 0, NULL, glb.ThirdPartyName),
					IIF(@DetailingCostCenter = 0, NULL, glb.CostCenterId), IIF(@DetailingCostCenter = 0, NULL, glb.CostCenterCode), IIF(@DetailingCostCenter = 0, NULL, glb.CostCenterName),
					glb.ResultExercise
		) gbfp ON gbip.MainAccountId = gbfp.MainAccountId AND ISNULL(gbip.ThirdPartyId, 0) = ISNULL(gbfp.ThirdPartyId, 0) AND ISNULL(gbip.CostCenterId, 0) = ISNULL(gbfp.CostCenterId, 0)
		ORDER BY 3, 11
	END TRY
	BEGIN CATCH
		PRINT CONCAT('Error: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
	END CATCH

	-- Eliminamos Las tablas temporales
	IF OBJECT_ID('tempdb..#SP_ReportGeneralBalanceInitialPeriod') IS NOT NULL DROP TABLE #SP_ReportGeneralBalanceInitialPeriod
	IF OBJECT_ID('tempdb..#SP_ReportGeneralBalanceFinalPeriod') IS NOT NULL DROP TABLE #SP_ReportGeneralBalanceFinalPeriod
END