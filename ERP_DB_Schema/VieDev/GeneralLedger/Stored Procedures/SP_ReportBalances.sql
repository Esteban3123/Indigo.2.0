-- =============================================
-- Author:        Juan Bermudez
-- Create date: 30/03/2016
-- Description:    Procedimiento para el reporte de balance prueba
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportBalances]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @Year INT,
			@MonthStart INT,
			@MonthEnd INT,
			@LevelAccount INT,
			@DetallingThird BIT,
			@DetailingCostCenter BIT,
			@AccountsZero BIT,
			@IncludeClosed BIT,
			@LegalBookId INT,
			@AccountStart VARCHAR(50),
			@AccountEnd VARCHAR(50),
			@ThirdPartyStart VARCHAR(50),
			@ThirdPartyEnd VARCHAR(50),
			@CostCenterStart VARCHAR(50),
			@CostCenterEnd VARCHAR(50),
			--Identificar si ya se cerro el año anterior al seleccionado
			@IsClosedLastYear BIT = 0,
			@LegalBookName VARCHAR(200),
			@LegalBookCurrency VARCHAR(200),
			@LegalBookCurrencyAbbreviation VARCHAR(200),
			@OfficialCurrencyName VARCHAR(200),
			@OfficialCurrencyAbbreviatio VARCHAR(200)

	BEGIN TRY

		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT	@Year = t.x.value('Year[1]','int'),
				@MonthStart = t.x.value('MonthStart[1]','int'),
				@MonthEnd = t.x.value('MonthEnd[1]','int'),
				@LevelAccount = t.x.value('LevelAccount[1]','int'),
				@DetallingThird = t.x.value('DetailingThird[1]','bit'),
				@DetailingCostCenter = t.x.value('DetailingCostCenter[1]','bit'),
				@AccountsZero = t.x.value('AccountsZero[1]','bit'),
				@IncludeClosed = t.x.value('IncludeClosed[1]','bit'),
				@LegalBookId = t.x.value('LegalBookId[1]','int'),
				@AccountStart = t.x.value('AccountStart[1]','varchar(max)'),
				@AccountEnd = t.x.value('AccountEnd[1]','varchar(max)'),
				@ThirdPartyStart = t.x.value('ThirdPartyStart[1]','varchar(max)'),
				@ThirdPartyEnd = t.x.value('ThirdPartyEnd[1]','varchar(max)'),
				@CostCenterStart = t.x.value('CostCenterStart[1]','varchar(max)'),
				@CostCenterEnd = t.x.value('CostCenterEnd[1]','varchar(max)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/***************** OBTENEMOS LOS REGISTROS CONTABLES DEL PERIODO INICIAL *****************/

		SELECT	@AccountStart = IIF(ISNULL(@AccountStart, '') = '', '0', @AccountStart),
				@AccountEnd = IIF(ISNULL(@AccountEnd, '') = '', 'ZZZZZZZZZZZZZZZZZZZZ', @AccountEnd),
				@ThirdPartyStart = IIF(ISNULL(@ThirdPartyStart, '') = '', '0', @ThirdPartyStart),
				@ThirdPartyEnd = IIF(ISNULL(@ThirdPartyEnd, '') = '', 'ZZZZZZZZZZZZZZZZZZZZ', @ThirdPartyEnd),
				@CostCenterStart = IIF(ISNULL(@CostCenterStart, '') = '', '0', @CostCenterStart),
				@CostCenterEnd = IIF(ISNULL(@CostCenterEnd, '') = '', 'ZZZZZZZZZZZZZZZZZZZZ', @CostCenterEnd)

		SELECT	@OfficialCurrencyName = c.Name,
				@OfficialCurrencyAbbreviatio =c.Abbreviation
		FROM GeneralLedger.CompanySettings cs
	    join Common.Currency c on c.Id = cs.OfficialCurrencyId

		SELECT	@LegalBookName = lb.Name,
				@IsClosedLastYear = IIf(lb.LastYearClose >= (@Year - 1), 1, 0),
				@LegalBookCurrency = ISNULL(C.Name,@OfficialCurrencyName),
				@LegalBookCurrencyAbbreviation = ISNULL(C.Abbreviation,@OfficialCurrencyAbbreviatio)
		FROM GeneralLedger.LegalBook lb
		left join Common.Currency c on c.Id = lb.OfficialCurrencyId
		WHERE lb.Id = @LegalBookId

		IF @IncludeClosed = 1 AND @IsClosedLastYear = 1 BEGIN
			SET @MonthEnd = IIF(@MonthEnd = 12, 13, @MonthEnd)
		END

		/***************** OBTENEMOS LOS REGISTROS CONTABLES DEL PERIODO INICIAL *****************/

		--Insertamos los movimientos que hasta la fecha final dada
			SELECT 	
				mac.[Type] AS mainAccountClassType, mac.Name mainAccountClassName, mal.[Level] AS mainAccountLevel, 
				ma.Id AS mainAccountId, ma.Number AS mainAccountCode, ma.Name AS mainAccountName, ma.Nature AS nature, 
				glb.IdThirdParty AS thirdPartyId, tp.Nit AS thirdPartyNit, tp.Name AS thirdPartyName, 
				glb.IdCostCenter AS costCenterId, cc.Code as costCenterCode, cc.Name as costCenterName, 
				ISNULL(glb.DebitInitial, 0) AS valueDebitInitial, ISNULL(glb.CreditInitial, 0) AS valueCreditInitial, CAST(0 AS DECIMAL(20,4)) previousBalance, 
				ISNULL(glb.Debit, 0) AS valueDebitMovement, ISNULL(glb.Credit, 0) AS valueCreditMovement, CAST(0 AS DECIMAL(20,4)) newBalance,
				ma.AllowsMovement AS allowsMovement, ISNULL(map.Number, ma.Number) as mainAccountParentNumber,
				CAST(0 AS DECIMAL(20,4)) totalPreviousBalanceDB, CAST(0 AS DECIMAL(20,4)) totalPreviousBalanceCR,
				CAST(0 AS DECIMAL(20,4)) totalValueDebitMovementDB, CAST(0 AS DECIMAL(20,4)) totalValueDebitMovementCR,
				CAST(0 AS DECIMAL(20,4)) totalValueCreditMovementDB, CAST(0 AS DECIMAL(20,4)) totalValueCreditMovementCR,
				CAST(0 AS DECIMAL(20,4)) totalNewBalanceDB, CAST(0 AS DECIMAL(20,4)) totalNewBalanceCR
			INTO #Table_SP_ReportBalances
			FROM 
			(
				SELECT 
					glb.IdMainAccount as IdMainAccount, glb.IdThirdParty, glb.IdCostCenter, 
					SUM(glb.DebitInitial) AS DebitInitial, 
					SUM(glb.CreditInitial) AS CreditInitial, 
					SUM(glb.Debit) AS Debit, 
					SUM(glb.Credit) AS Credit
				FROM 
				(
					SELECT
						IdMainAccount,  IdThirdParty, IdCostCenter,
						IIF(([Year] = @Year and [Month] >= @MonthStart), 0, DebitValue) AS DebitInitial,
						IIF(([Year] = @Year and [Month] >= @MonthStart), 0, CreditValue) AS CreditInitial,
						IIF(([Year] = @Year and [Month] >= @MonthStart), DebitValue, 0) AS Debit,
						IIF(([Year] = @Year and [Month] >= @MonthStart), CreditValue, 0) AS Credit
					FROM GeneralLedger.GeneralLedgerBalance WITH (NOLOCK)
					WHERE 
					(
						(@IsClosedLastYear = 0 AND (([Year] = (@Year - 1) AND [Month] < 13) OR ([Year] = (@Year - 2) AND [Month] = 14))) OR 
						(@IsClosedLastYear = 1 AND  ([Year] = (@Year - 1) AND [Month] = 14))
					) OR ([Year] = @Year AND [Month] <= @MonthEnd)
				) AS glb 
				GROUP BY glb.IdMainAccount, glb.IdThirdParty, glb.IdCostCenter
			) AS glb
			RIGHT OUTER JOIN GeneralLedger.MainAccounts AS ma WITH (NOLOCK) ON ma.id = glb.IdMainAccount 
			INNER JOIN GeneralLedger.MainAccountClasses AS mac WITH (NOLOCK) ON mac.Id = ma.IdAccountClass 
			INNER JOIN GeneralLedger.MainAccountLevels AS mal WITH (NOLOCK) ON mal.Id = ma.IdAccountLevel 
			LEFT JOIN GeneralLedger.MainAccounts AS map WITH (NOLOCK) ON map.Id = ma.IdParent 
			LEFT JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON tp.Id = glb.IdThirdParty
			LEFT JOIN Payroll.CostCenter AS cc WITH (NOLOCK) ON cc.Id = glb.IdCostCenter
			WHERE ma.LegalBookId = @LegalBookId 
				AND	ma.Number  >= ISNULL(@AccountStart,'0') AND ma.Number <= ISNULL(@AccountEnd, 'ZZZ') 
				AND	(ISNULL(tp.Nit, '0') >= ISNULL(@ThirdPartyStart, '0') AND ISNULL(tp.Nit, '0') <= ISNULL(@ThirdPartyEnd, 'ZZZ'))
				AND ISNULL(cc.Code, '0') >= ISNULL(@CostCenterStart, '0') AND ISNULL(cc.Code, '0') <= ISNULL(@CostCenterEnd, 'ZZZ')
			ORDER BY ma.Number
			OPTION (RECOMPILE)
    
	--select * from  #Table_SP_ReportBalances 

		-- Mayorizamos los saldos
		UPDATE ste 
			SET ste.valueDebitInitial = ISNULL(te.valueDebitInitial, 0),
				ste.valueCreditInitial = ISNULL(te.valueCreditInitial, 0),						
				ste.valueDebitMovement = ISNULL(te.valueDebitMovement, 0),
				ste.valueCreditMovement = ISNULL(te.valueCreditMovement, 0)
		FROM #Table_SP_ReportBalances ste
		JOIN
		(
			SELECT
				ste.mainAccountId,
				SUM(te.valueDebitInitial) valueDebitInitial,
				SUM(te.valueCreditInitial) valueCreditInitial,
				SUM(te.valueDebitMovement) valueDebitMovement,
				SUM(te.valueCreditMovement) valueCreditMovement
			FROM #Table_SP_ReportBalances ste
			JOIN
			(
				SELECT
					te.mainAccountCode,
					SUM(ISNULL(te.valueDebitInitial,0)) valueDebitInitial,
					SUM(ISNULL(te.valueCreditInitial,0)) valueCreditInitial,
					SUM(ISNULL(te.valueDebitMovement,0)) valueDebitMovement,
					SUM(ISNULL(te.valueCreditMovement,0)) valueCreditMovement
				FROM #Table_SP_ReportBalances AS te
				GROUP BY te.mainAccountCode
			) te ON te.mainAccountCode LIKE (ste.mainAccountCode + '%')
			WHERE ste.allowsMovement = 0
			GROUP BY ste.mainAccountId
		) te ON ste.mainAccountId = te.mainAccountId


		
		----Actualizamos los saldos
		UPDATE #Table_SP_ReportBalances
			SET PreviousBalance = (ValueDebitInitial - ValueCreditInitial) * IIF(Nature = 1, 1, -1),
				NewBalance = (ValueDebitInitial - ValueCreditInitial + ValueDebitMovement - ValueCreditMovement) * IIF(Nature = 1, 1, -1)
		
		--select * from #Table_SP_ReportBalances
		--Para actualizar los totales del reporte cuando se usan filtros
		DECLARE @mainAccountLevel INT = (SELECT MIN(mainAccountLevel) FROM #Table_SP_ReportBalances)
				
		UPDATE ste
			SET ste.totalPreviousBalanceDB = t.previousBalanceDB, 
				ste.totalValueDebitMovementDB = t.valueDebitMovementDB, 
				ste.totalValueCreditMovementDB = t.valueCreditMovementDB,
				ste.totalNewBalanceDB = t.newBalanceDB,

				totalPreviousBalanceCR = t.previousBalanceCR, 
				totalValueDebitMovementCR = t.valueDebitMovementCR, 
				totalValueCreditMovementCR = t.valueCreditMovementCR,
				totalNewBalanceCR = t.newBalanceCR
		FROM #Table_SP_ReportBalances ste
		JOIN 
		(
			SELECT SUM(IIF(nature = 1, previousBalance, 0)) as previousBalanceDB,
					SUM(IIF(nature = 1, valueDebitMovement, 0)) as valueDebitMovementDB,
					SUM(IIF(nature = 1, valueCreditMovement, 0)) as valueCreditMovementDB,
					SUM(IIF(nature = 1, newBalance, 0)) as newBalanceDB,

					SUM(IIF(nature = 2, previousBalance, 0)) as previousBalanceCR,
					SUM(IIF(nature = 2, valueDebitMovement, 0)) as valueDebitMovementCR,
					SUM(IIF(nature = 2, valueCreditMovement, 0)) as valueCreditMovementCR,
					SUM(IIF(nature = 2, newBalance, 0)) as newBalanceCR
			FROM #Table_SP_ReportBalances 
			WHERE mainAccountLevel = @mainAccountLevel
		) t ON @mainAccountLevel = @mainAccountLevel -- Obtener la sumatoria siempre ha de ser verdadera
		print(@AccountsZero)

		
		--Filtros del reporte
		IF @AccountsZero = 0 
		BEGIN
			IF @DetailingCostCenter = 0 and @DetallingThird = 0 
			BEGIN			
				DELETE e 
				FROM #Table_SP_ReportBalances e 
				JOIN 
				(
					SELECT mainAccountId 
					FROM #Table_SP_ReportBalances
					GROUP BY mainAccountId 
					HAVING SUM(previousBalance)= 0 AND SUM(valueDebitMovement) = 0 AND SUM(valueCreditMovement) = 0 AND SUM(newBalance) = 0
				) as data ON data.mainAccountId = e.mainAccountId
			END
			IF @DetailingCostCenter = 1 and @DetallingThird = 1 
			BEGIN			
				DELETE e 
				FROM #Table_SP_ReportBalances e 
				JOIN 
				(
					SELECT mainAccountId, thirdPartyId,  costCenterId 
					FROM #Table_SP_ReportBalances
					GROUP BY mainAccountId, thirdPartyId,  costCenterId
					HAVING SUM(previousBalance)= 0 AND SUM(valueDebitMovement) = 0 AND SUM(valueCreditMovement) = 0 AND SUM(newBalance) = 0
				) as data ON data.mainAccountId = e.mainAccountId 
					AND ISNULL(data.thirdPartyId, 0) = ISNULL(e.thirdPartyId, 0)
					AND ISNULL(data.costCenterId, 0) = ISNULL(e.costCenterId, 0)
			END
			ELSE IF @DetailingCostCenter = 1 
			BEGIN			
				DELETE e 
				FROM #Table_SP_ReportBalances e 
				JOIN 
				(
					SELECT mainAccountId, costCenterId 
					FROM #Table_SP_ReportBalances 
					GROUP BY mainAccountId, costCenterId 
					HAVING SUM(previousBalance)= 0 AND SUM(valueDebitMovement) = 0 and SUM(valueCreditMovement) = 0 AND SUM(newBalance) = 0
				) as data on data.mainAccountId = e.mainAccountId AND ISNULL(data.costCenterId, 0) = ISNULL(e.costCenterId, 0)
			END
			ELSE IF @DetallingThird = 1 
			BEGIN			
				DELETE e 
				FROM #Table_SP_ReportBalances e 
				JOIN 
				(
					SELECT mainAccountId, thirdPartyId 
					FROM #Table_SP_ReportBalances 
					GROUP BY mainAccountId, thirdPartyId 
					HAVING SUM(previousBalance)= 0 AND SUM(valueDebitMovement) = 0 and SUM(valueCreditMovement) = 0 AND SUM(newBalance) = 0
				) as data on data.mainAccountId = e.mainAccountId AND ISNULL(data.thirdPartyId, 0) = ISNULL(e.thirdPartyId, 0)
			END
		END

		--Mostramos los resultados
		SELECT
			@LegalBookName legalBookName,
			@LegalBookCurrency LegalBookCurrency,
			@LegalBookCurrencyAbbreviation LegalBookCurrencyAbbreviation ,
			mainAccountClassType, mainAccountClassName, mainAccountLevel,
			mainAccountId, mainAccountCode, mainAccountName, IIF(nature = 1, 'Debito', 'Credito') nature, 
			IIF
			(
				nature = 1,
				IIF(SUM(previousBalance) >= 0, 'Debito', 'Credito'),
				IIF(SUM(previousBalance) >= 0, 'Credito', 'Debito')
			) naturePreviousBalance,
			IIF
			(
				nature = 1,
				IIF(SUM(newBalance) >= 0, 'Debito', 'Credito'),
				IIF(SUM(newBalance) >= 0, 'Credito', 'Debito')
			) natureNewBalance,
			IIF(@DetallingThird = 0, NULL, thirdPartyId) thirdPartyId, IIF(@DetallingThird = 0, NULL, thirdPartyNit) thirdPartyNit, IIF(@DetallingThird = 0, NULL, thirdPartyName) thirdPartyName,
			IIF(@DetailingCostCenter = 0, NULL, costCenterId) costCenterId, IIF(@DetailingCostCenter = 0, NULL, costCenterCode) costCenterCode, IIF(@DetailingCostCenter = 0, NULL, costCenterName) costCenterName,
			SUM(valueDebitInitial) valueDebitInitial, SUM(valueCreditInitial) valueCreditInitial, SUM(previousBalance) previousBalance,
			SUM(valueDebitMovement) valueDebitMovement, SUM(valueCreditMovement) valueCreditMovement, SUM(newBalance) newBalance,
			allowsMovement, mainAccountParentNumber,
			MIN(totalPreviousBalanceDB) totalPreviousBalanceDB, MIN(totalPreviousBalanceCR) totalPreviousBalanceCR,
			MIN(totalValueDebitMovementDB) totalValueDebitMovementDB, MIN(totalValueDebitMovementCR) totalValueDebitMovementCR,
			MIN(totalValueCreditMovementDB) totalValueCreditMovementDB, MIN(totalValueCreditMovementCR) totalValueCreditMovementCR,
			MIN(totalNewBalanceDB) totalNewBalanceDB, MIN(totalNewBalanceCR) totalNewBalanceCR
		FROM #Table_SP_ReportBalances
		WHERE mainAccountLevel <= CASE @LevelAccount 
										WHEN 5 THEN (SELECT MAX(Level) FROM GeneralLedger.MainAccountLevels)
										ELSE @LevelAccount 
								  END
		GROUP BY 
			mainAccountClassType, mainAccountClassName, mainAccountLevel,
			mainAccountId, mainAccountCode, mainAccountName, nature, 
			allowsMovement, mainAccountParentNumber,
			IIF(@DetallingThird = 0, NULL, thirdPartyId), IIF(@DetallingThird = 0, NULL, thirdPartyNit), IIF(@DetallingThird = 0, NULL, thirdPartyName),
			IIF(@DetailingCostCenter = 0, NULL, costCenterId), IIF(@DetailingCostCenter = 0, NULL, costCenterCode), IIF(@DetailingCostCenter = 0, NULL, costCenterName)
			ORDER BY mainAccountCode
	END TRY
	BEGIN CATCH	
		SELECT * FROM #Table_SP_ReportBalances WHERE mainAccountId IS NULL
	END CATCH

	-- Eliminamos La tabla temporal
	IF OBJECT_ID('tempdb..#Table_SP_ReportBalances') IS NOT NULL DROP TABLE #Table_SP_ReportBalances
END