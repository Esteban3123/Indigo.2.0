-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-17
-- Description:	Procedimiento para el reporte de balance general mensual
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportGeneralBalanceMonthly]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @YearFinal INT,
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
		SELECT	@YearFinal = t.x.value('YearFinal[1]','int'),
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

		SELECT	@CostCenterInitial = IIF(@CostCenterInitial = '', '0', @CostCenterInitial),
				@CostCenterFinal = IIF(@CostCenterFinal = '', 'ZZZZZZZZZZZZZZZZZZZZ', @CostCenterFinal)

		/****************************** OBTENEMOS LOS REGISTROS CONTABLES ******************************/

		--Insertamos los movimientos del periodo final
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
			INTO #SP_ReportGeneralBalanceMonthly
			FROM [GeneralLedger].[GetGeneralLedgerBalanceCalculated](@LegalBookId, @YearFinal, @MonthFinal, @MonthFinal, 0, 1, @Nature, @MemorandumAccounts, NULL, NULL, NULL, NULL, @CostCenterInitial, @CostCenterFinal) glb
			ORDER BY glb.MainAccountCode

		/************************************ APLICAMOS LOS FILTROS ************************************/

		--Filtros del reporte
		IF @AccountsZero = 0 
		BEGIN
			IF @DetailingCostCenter = 0 and @DetailingThird = 0 
			BEGIN			
				DELETE e 
				FROM #SP_ReportGeneralBalanceMonthly e 
				JOIN 
				(
					SELECT MainAccountId 
					FROM #SP_ReportGeneralBalanceMonthly
					GROUP BY mainAccountId 
					HAVING SUM(PreviousBalance)= 0 AND SUM(valueDebitMovement) = 0 AND SUM(valueCreditMovement) = 0 AND SUM(newBalance) = 0
				) as data ON data.mainAccountId = e.mainAccountId
			END
			IF @DetailingCostCenter = 1 and @DetailingThird = 1 
			BEGIN			
				DELETE e 
				FROM #SP_ReportGeneralBalanceMonthly e 
				JOIN 
				(
					SELECT mainAccountId, thirdPartyId,  costCenterId 
					FROM #SP_ReportGeneralBalanceMonthly
					GROUP BY mainAccountId, thirdPartyId,  costCenterId
					HAVING SUM(previousBalance)= 0 AND SUM(valueDebitMovement) = 0 AND SUM(valueCreditMovement) = 0 AND SUM(newBalance) = 0
				) as data ON data.mainAccountId = e.mainAccountId 
					AND ISNULL(data.thirdPartyId, 0) = ISNULL(e.thirdPartyId, 0)
					AND ISNULL(data.costCenterId, 0) = ISNULL(e.costCenterId, 0)
			END
			ELSE IF @DetailingCostCenter = 1 
			BEGIN			
				DELETE e 
				FROM #SP_ReportGeneralBalanceMonthly e 
				JOIN 
				(
					SELECT mainAccountId, costCenterId 
					FROM #SP_ReportGeneralBalanceMonthly 
					GROUP BY mainAccountId, costCenterId 
					HAVING SUM(previousBalance)= 0 AND SUM(valueDebitMovement) = 0 and SUM(valueCreditMovement) = 0 AND SUM(newBalance) = 0
				) as data on data.mainAccountId = e.mainAccountId AND ISNULL(data.costCenterId, 0) = ISNULL(e.costCenterId, 0)
			END
			ELSE IF @DetailingThird = 1 
			BEGIN			
				DELETE e 
				FROM #SP_ReportGeneralBalanceMonthly e 
				JOIN 
				(
					SELECT mainAccountId, thirdPartyId 
					FROM #SP_ReportGeneralBalanceMonthly 
					GROUP BY mainAccountId, thirdPartyId 
					HAVING SUM(previousBalance)= 0 AND SUM(valueDebitMovement) = 0 and SUM(valueCreditMovement) = 0 AND SUM(newBalance) = 0
				) as data on data.mainAccountId = e.mainAccountId AND ISNULL(data.thirdPartyId, 0) = ISNULL(e.thirdPartyId, 0)
			END
		END

		/*******************************  MOSTRAMOS LOS RESULTADOS *******************************/

		--Mostramos los resultados
		SELECT	glb.LegalBookName,
				glb.MainAccountId, glb.MainAccountCode, glb.MainAccountName, glb.MainAccountNature, glb.MainAccountAllowsMovement, glb.MainAccountAvailability,
				glb.MainAccountClassType, glb.MainAccountClassCode, glb.MainAccountClassName, glb.MainAccountLevel, 
				-----------------------------------------------------------------------------------------------------------------
				IIF(@DetailingThird = 0, NULL, glb.ThirdPartyId) ThirdPartyId, IIF(@DetailingThird = 0, NULL, glb.ThirdPartyNit) ThirdPartyNit, IIF(@DetailingThird = 0, NULL, glb.ThirdPartyName) ThirdPartyName,
				IIF(@DetailingCostCenter = 0, NULL, glb.CostCenterId) CostCenterId, IIF(@DetailingCostCenter = 0, NULL, glb.CostCenterCode) CostCenterCode, IIF(@DetailingCostCenter = 0, NULL, glb.CostCenterName) CostCenterName,
				SUM(glb.ValueDebitInitial) ValueDebitInitial, SUM(glb.ValueCreditInitial) ValueCreditInitial, SUM(glb.PreviousBalance) PreviousBalance,
				SUM(glb.ValueDebitMovement) ValueDebitMovement, SUM(glb.ValueCreditMovement) ValueCreditMovement, SUM(glb.NewBalance) NewBalance,
				glb.ResultExercise
		FROM #SP_ReportGeneralBalanceMonthly glb
		WHERE mainAccountLevel <= CASE @accountLevel 
										WHEN 5 THEN (SELECT MAX(Level) FROM GeneralLedger.MainAccountLevels)
										ELSE @accountLevel 
									END
		GROUP BY glb.LegalBookName,
				glb.MainAccountId, glb.MainAccountCode, glb.MainAccountName, glb.MainAccountNature, glb.MainAccountAllowsMovement, glb.MainAccountAvailability,
				glb.MainAccountClassType, glb.MainAccountClassCode, glb.MainAccountClassName, glb.MainAccountLevel, 
				IIF(@DetailingThird = 0, NULL, glb.ThirdPartyId), IIF(@DetailingThird = 0, NULL, glb.ThirdPartyNit), IIF(@DetailingThird = 0, NULL, glb.ThirdPartyName),
				IIF(@DetailingCostCenter = 0, NULL, glb.CostCenterId), IIF(@DetailingCostCenter = 0, NULL, glb.CostCenterCode), IIF(@DetailingCostCenter = 0, NULL, glb.CostCenterName),
				glb.ResultExercise
		ORDER BY 3, 13
	END TRY
	BEGIN CATCH
		PRINT CONCAT('Error: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
	END CATCH

	-- Eliminamos La tabla temporal
	IF OBJECT_ID('tempdb..#SP_ReportGeneralBalanceMonthly') IS NOT NULL DROP TABLE #SP_ReportGeneralBalanceMonthly
END