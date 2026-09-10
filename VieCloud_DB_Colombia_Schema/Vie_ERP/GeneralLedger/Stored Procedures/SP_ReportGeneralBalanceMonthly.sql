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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de Balance General Mensual contable, mostrando para un año y mes específicos los saldos iniciales, movimientos débito y crédito, y saldo final de cada cuenta contable del plan de cuentas. Recibe criterios de filtrado en formato XML (libro contable, nivel de cuenta, naturaleza, rango de centros de costo, cuentas de orden y opciones de detalle por tercero o centro de costo) y consulta la función GetGeneralLedgerBalanceCalculated para obtener los saldos calculados. Aplica filtros opcionales para excluir cuentas con saldos y movimientos en cero, y permite desglosar el resultado por tercero (NIT/razón social) y/o por centro de costo según la configuración del reporte. Es el procedimiento central del módulo de contabilidad general para la generación del balance general periódico.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportGeneralBalanceMonthly';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportGeneralBalanceMonthly';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte contable de balance general mensual agregando saldos iniciales, movimientos y nuevos saldos por cuenta, con opción de detallar por tercero y centro de costo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalanceMonthly';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibirse un XML con nodo /Data que contenga año, mes, libro legal, nivel de cuenta, naturaleza y banderas de detalle; Debe existir información disponible en la función GetGeneralLedgerBalanceCalculated para los criterios indicados; La tabla GeneralLedger.MainAccountLevels debe contener al menos un nivel cuando @AccountLevel = 5', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalanceMonthly';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'@DetailingCostCenter siempre se inicializa en 0 dentro del procedimiento (no se lee del XML), por lo que el detalle por centro de costo nunca se activa en esta versión; Los rangos vacíos de centro de costo se normalizan a un rango abierto (''0'' a ''ZZZZZZZZZZZZZZZZZZZZ''); Los valores monetarios se agregan con SUM en la salida final agrupando por cuenta (y opcionalmente tercero/centro de costo); La tabla temporal #SP_ReportGeneralBalanceMonthly siempre se elimina al finalizar la ejecución, incluso si ocurre una excepción; Los errores se capturan y se imprimen mediante PRINT sin propagarse al llamador', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalanceMonthly';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Balance general mensual; Libro legal contable; Cuenta contable principal; Naturaleza de la cuenta; Cuentas de orden (memorando); Tercero (NIT); Centro de costo; Saldo anterior; Movimientos débito y crédito; Nuevo saldo; Resultado del ejercicio; Niveles del plan de cuentas', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalanceMonthly';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] tempdb..#SP_ReportGeneralBalanceMonthly: Se cargan los saldos calculados retornados por GeneralLedger.GetGeneralLedgerBalanceCalculated para el periodo final; [DELETE] tempdb..#SP_ReportGeneralBalanceMonthly: Cuando @AccountsZero=0 y no hay detalle por tercero ni centro de costo: elimina cuentas cuyo SUM(PreviousBalance), SUM(ValueDebitMovement), SUM(ValueCreditMovement) y SUM(NewBalance) sean todos cero agrupados por MainAccountId; [DELETE] tempdb..#SP_ReportGeneralBalanceMonthly: Cuando @AccountsZero=0, @DetailingCostCenter=1 y @DetailingThird=1: elimina filas cuyo agrupado por (MainAccountId, ThirdPartyId, CostCenterId) tenga saldos y movimientos totales en cero; [DELETE] tempdb..#SP_ReportGeneralBalanceMonthly: Cuando @AccountsZero=0 y solo @DetailingCostCenter=1: elimina filas cuyo agrupado por (MainAccountId, CostCenterId) tenga saldos y movimientos en cero; [DELETE] tempdb..#SP_ReportGeneralBalanceMonthly: Cuando @AccountsZero=0 y solo @DetailingThird=1: elimina filas cuyo agrupado por (MainAccountId, ThirdPartyId) tenga saldos y movimientos en cero; [RETURN_RESULT] N/A: Devuelve el balance agregado filtrando por MainAccountLevel <= @AccountLevel (o el máximo nivel definido en MainAccountLevels cuando @AccountLevel=5), enmascarando con NULL los datos de tercero/centro de costo cuando sus banderas de detalle están en 0', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalanceMonthly';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CostCenterInitial vacío → Se reemplaza por ''0'' como límite inferior; si @CostCenterFinal vacío → Se reemplaza por ''ZZZZZZZZZZZZZZZZZZZZ'' como límite superior; si @AccountsZero = 0 → Se aplican filtros para eliminar cuentas/agrupaciones con saldos y movimientos totalmente en cero else Se conservan todas las cuentas incluso con saldos en cero; si @AccountLevel = 5 → Se usa el nivel máximo definido en GeneralLedger.MainAccountLevels else Se limita al nivel indicado por @AccountLevel; si @DetailingThird = 0 → En el resultado final se enmascaran ThirdPartyId, ThirdPartyNit y ThirdPartyName como NULL; si @DetailingCostCenter = 0 → En el resultado final se enmascaran CostCenterId, CostCenterCode y CostCenterName como NULL', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalanceMonthly';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.GetGeneralLedgerBalanceCalculated; GeneralLedger.MainAccountLevels', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalanceMonthly';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalanceMonthly';
-- GO
