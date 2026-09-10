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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de Balance General Comparativo contable, comparando los saldos y movimientos de dos períodos distintos (período inicial y período final) dentro de un libro contable legal. Consume la función GetGeneralLedgerBalanceCalculated para obtener los saldos calculados de cada período —incluyendo saldos anteriores, débitos, créditos y nuevo saldo por cuenta— y luego combina ambos resultados para mostrar la evolución financiera entre períodos. Permite filtrar por nivel de cuenta contable, naturaleza, cuentas de orden (cuentas de memorando), terceros (NIT/razón social), centros de costo y rango de fechas, con opción de excluir cuentas con saldo cero. Se usa en contabilidad para comparar la posición financiera de la empresa entre dos cortes de tiempo, típicamente para informes de gestión, cierre de período o auditoría contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportGeneralBalanceComparative';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportGeneralBalanceComparative';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un balance general contable comparativo entre dos periodos (año/mes inicial vs final) consolidando saldos por cuenta, tercero y/o centro de costo según los niveles de detalle solicitados.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalanceComparative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener YearInitial, MonthInitial, YearFinal, MonthFinal, LegalBookId, AccountLevel, Nature, DetailingThird, MemorandumAccounts, AccountsZero, CostCenterInitial y CostCenterFinal; La función GeneralLedger.GetGeneralLedgerBalanceCalculated debe estar disponible y operativa; Si CostCenterInitial es nulo o vacío se asume ''0''; si CostCenterFinal es nulo o vacío se asume ''ZZZZZZZZZZZZZZZZZZZZ'' como límite superior', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalanceComparative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'@DetailingCostCenter siempre se inicializa en 0 internamente, por lo que la rama de detalle por centro de costo nunca se activa en la práctica desde este procedimiento; Los rangos de centro de costo se acotan entre ''0'' y ''ZZZZZZZZZZZZZZZZZZZZ'' cuando no se especifican; Las cuentas se filtran por nivel exacto excepto cuando se solicita nivel 5, que actúa como ''nivel 5 o superior''; Los valores numéricos de saldos en la salida nunca son NULL (se aplica ISNULL con 0); Las tablas temporales se eliminan al final independientemente del resultado de TRY/CATCH; Solo se consultan periodos de un único mes a la vez (MonthInitial-MonthInitial y MonthFinal-MonthFinal) por cada extremo del comparativo', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalanceComparative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Balance general comparativo; Libro legal contable; Cuenta contable principal; Naturaleza de la cuenta; Cuentas de orden (memorando); Tercero (NIT); Centro de costo; Saldo anterior; Movimientos débito/crédito; Nuevo saldo; Resultado del ejercicio; Nivel de cuenta; Periodo contable (año/mes)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalanceComparative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve un conjunto de resultados con saldos contables del periodo inicial y final emparejados por MainAccountId, ThirdPartyId y CostCenterId mediante FULL JOIN, filtrando por MainAccountLevel = @AccountLevel (o niveles >= 5 si @AccountLevel = 5); [DELETE] #SP_ReportGeneralBalanceInitialPeriod: Cuando @AccountsZero = 0 se eliminan los registros cuyo SUM(NewBalance) por agrupación = 0; la agrupación depende de @DetailingCostCenter y @DetailingThird (cuenta sola, cuenta+tercero, cuenta+centro o cuenta+tercero+centro); [DELETE] #SP_ReportGeneralBalanceFinalPeriod: Cuando @AccountsZero = 0 se eliminan los registros cuyo SUM(NewBalance) por agrupación = 0, aplicando la misma lógica condicional que en el periodo inicial; [RAISERROR] tempdb: En caso de excepción se imprime (PRINT) el mensaje y línea del error mediante ERROR_MESSAGE() y ERROR_LINE() en el bloque CATCH', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalanceComparative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @AccountsZero = 0 → Se ejecutan los DELETE para excluir cuentas/agrupaciones con saldo neto cero en ambas tablas temporales else Se conservan todos los registros incluidos los de saldo cero; si @DetailingCostCenter = 0 AND @DetailingThird = 0 → El filtrado de saldo cero agrupa solo por MainAccountId; si @DetailingCostCenter = 1 AND @DetailingThird = 1 → El filtrado de saldo cero agrupa por MainAccountId, ThirdPartyId y CostCenterId; si @DetailingCostCenter = 1 (y no ambos en 1) → El filtrado de saldo cero agrupa por MainAccountId y CostCenterId; si @DetailingThird = 1 (y no ambos en 1) → El filtrado de saldo cero agrupa por MainAccountId y ThirdPartyId; si @AccountLevel = 5 → Se incluyen cuentas con MainAccountLevel >= 5 (auxiliares) else Solo se incluyen cuentas con MainAccountLevel exactamente igual a @AccountLevel; si @DetailingThird = 0 → Se anulan (NULL) ThirdPartyId, ThirdPartyNit y ThirdPartyName en la salida y en el agrupamiento final; si @DetailingCostCenter = 0 → Se anulan (NULL) CostCenterId, CostCenterCode y CostCenterName en la salida y en el agrupamiento final', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalanceComparative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.GetGeneralLedgerBalanceCalculated', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalanceComparative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.GetGeneralLedgerBalanceCalculated', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalanceComparative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalanceComparative';
-- GO
