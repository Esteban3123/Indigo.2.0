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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que genera el reporte comparativo de Estado de Resultados contable para dos períodos distintos (período inicial y período final), permitiendo analizar la evolución de ingresos, gastos y resultados entre rangos de meses y años seleccionados. Recibe los criterios de filtro en formato XML (libro legal, nivel de cuenta, centros de costo, sucursales, naturalezas contables) y ejecuta internamente el SP de Estado de Resultados simple (SP_ReportResulStatus) dos veces —una por cada período— almacenando los resultados en tablas temporales separadas para luego consolidarlos en una salida comparativa. Determina la moneda del libro legal consultando las tablas de configuración de la compañía y el catálogo de monedas, mostrando los saldos, movimientos débito/crédito y balances nuevos de cada cuenta contable en ambos períodos lado a lado. Sirve para la toma de decisiones financieras y el cierre contable periódico, comparando el desempeño económico de la organización entre dos intervalos de tiempo.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportResulStatusComparative';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportResulStatusComparative';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte comparativo del estado de resultados entre dos rangos de periodos contables, consolidando saldos finales por cuenta, centro de costo y sucursal.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResulStatusComparative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener los nodos esperados (rangos de año/mes inicial y final, libro contable, nivel de cuenta, naturalezas, centros de costo y sucursales).; Debe existir un registro en GeneralLedger.CompanySettings con la moneda oficial referenciada en Common.Currency.; El LegalBookId provisto debe existir en GeneralLedger.LegalBook; si no tiene moneda asociada se usa la moneda oficial de la empresa.; El procedimiento GeneralLedger.SP_ReportResulStatus debe existir y devolver el conjunto de columnas compatible con las tablas temporales declaradas.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResulStatusComparative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El reporte siempre se calcula invocando dos veces SP_ReportResulStatus, una para el rango inicial y otra para el rango final, con los mismos filtros de libro, nivel, naturalezas, centros de costo y sucursales.; El saldo del periodo inicial siempre se ubica en newBalanceInitial y el del periodo final en newBalanceFinal mediante UNION ALL con ceros en la columna contraria.; La agrupación final se realiza siempre por cuenta principal, nivel, clase, disponibilidad, centro de costo y sucursal.; La moneda de presentación queda determinada por el libro contable, con fallback a la moneda oficial de la empresa.; Los errores en tiempo de ejecución no propagan excepción al llamador (solo se imprimen).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResulStatusComparative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estado de resultados; Libro contable (LegalBook); Cuenta principal y nivel de cuenta; Naturaleza contable; Centro de costo; Sucursal; Moneda oficial de la empresa; Saldo anterior y nuevo saldo; Movimientos débito/crédito; Comparativo entre periodos contables', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResulStatusComparative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve un resultset con saldos comparativos (newBalanceInitial vs newBalanceFinal) agrupados por cuenta principal, nivel, clase, disponibilidad, centro de costo y sucursal, junto con la moneda del libro contable.; [RAISERROR] N/A: En caso de error, no se relanza la excepción: se imprime el mensaje y la línea con PRINT dentro del CATCH.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResulStatusComparative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si La moneda del libro contable (LegalBook.OfficialCurrencyId) es NULL o no existe en Common.Currency → Se usa como moneda del reporte la moneda oficial definida en GeneralLedger.CompanySettings else Se usa el nombre y abreviatura de la moneda asociada al libro contable', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResulStatusComparative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_ReportResulStatus', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResulStatusComparative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Common.Currency; GeneralLedger.LegalBook', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResulStatusComparative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResulStatusComparative';
-- GO
