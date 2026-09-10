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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de Estado de Resultados contable para un libro legal seleccionado, consolidando movimientos débito y crédito por cuenta principal, tercero, centro de costo y sucursal dentro de un rango de año y meses indicados. Recibe criterios de filtro en formato XML (visualización, período, libro legal, nivel de cuenta, naturalezas, centros de costo y sucursales) y utiliza la función Split para descomponer listas de filtros. Consulta la configuración de la empresa y el catálogo de monedas para determinar la moneda oficial y la moneda del libro, y verifica si el año anterior ya fue cerrado contablemente para calcular correctamente los saldos iniciales, movimientos del período y saldos finales. El resultado sirve para reportería financiera de pérdidas y ganancias, análisis de ingresos y gastos por período, con soporte a libros de estados financieros bajo distintos marcos normativos (p. ej. NIIF).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportResulStatus';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportResulStatus';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte contable de Estado de Resultados consolidando saldos iniciales, movimientos y saldos finales por cuenta, tercero, centro de costo y sucursal, con mayorización a niveles superiores y filtros configurables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResulStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener Visualization, año, rango de meses, LegalBookId y nivel de cuenta; Debe existir registro en GeneralLedger.CompanySettings con OfficialCurrencyId válido en Common.Currency; El LegalBook indicado debe existir en GeneralLedger.LegalBook; Si TypeBook = 3 (libro de estados financieros), debe existir un LegalBook con OfficialBook = 1 y Status = 1 para obtener el cierre del año anterior; Las listas Natures, CostCenters y BranchOffices deben ser cadenas separadas por coma de enteros válidos', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResulStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se reportan cuentas de clase Type = 2 (cuentas de resultado); Sólo se incluyen cuentas pertenecientes al @LegalBookId indicado; El signo del saldo siempre depende de la naturaleza: Débito suma positivo, Crédito invierte signo; La moneda y abreviatura del libro caen al oficial de la empresa cuando el libro no tiene moneda asignada; Los errores no abortan: el CATCH sólo imprime mensaje, sin RAISERROR ni rollback explícito; El resultado final se filtra por nivel jerárquico (mainAccountLevel <= @LevelAccount); Las cuentas ''4'' y ''5'' mayorizadas se rotulan como OPERACIONALES o NO OPERACIONALES según Availability (4 o 5)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResulStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] #Table_ReportResulStatus: Inserta movimientos de cuentas contables clase Type=2 (resultado) del libro indicado, distinguiendo saldos iniciales (períodos previos) de movimientos del rango (Year=@Year y Month entre @MonthStart y @MonthEnd); [INSERT] #Table_ReportResulStatus: Cuando @IsClosedLastYear = 0 se consideran saldos del año anterior con Month<13 o del año-2 con Month=14; cuando =1 sólo se considera Year=@Year-1 con Month=14 (cierre); [INSERT] #Table_ReportResulStatus: Cuando @Visualization = 2, calcula movementMonthStart/Intermediate/End como (Credit-Debit) en los meses @MonthStart, @MonthStart+1 y @MonthEnd respectivamente; [INSERT] #Table_ReportResulStatus: Cuando @LegalBookType = 3, hace join por HomologationAccount.OfficialMainAccountId; en otro caso, join directo por MainAccount.Id; [INSERT] #Table_ReportResulStatus: Mayoriza saldos hacia niveles superiores insertando filas agregadas por cada cuenta padre cuyo Number es prefijo del mainAccountCode (LIKE CONCAT(ma.Number,''%'')); [INSERT] #Table_ReportResulStatus: Para cuentas mayorizadas con Number=''4'' o ''5'' y Availability=4 concatena '' OPERACIONALES'' al nombre; con Availability=5 concatena '' NO OPERACIONALES''; [UPDATE] #Table_ReportResulStatus: Calcula PreviousBalance=(DebitInitial-CreditInitial)*signo y NewBalance=(DebitInitial-CreditInitial+DebitMovement-CreditMovement)*signo; signo=+1 si Nature=''Debito'', -1 si ''Credito''; [DELETE] #Table_ReportResulStatus: Cuando @AccountsZero = 0, elimina filas con todos los valores (initial, movement, balances) en cero; [RETURN_RESULT] #Table_ReportResulStatus: Retorna las filas con mainAccountLevel <= @LevelAccount ordenadas por mainAccountCode, thirdPartyNit, costCenterCode', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResulStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Natures, @CostCenters o @BranchOffices no son nulos/vacíos → Activa el filtro correspondiente (@FilterByNature/CostCenter/BranchOffice=1) y carga los IDs en tablas tipo tabla else No se aplica filtro y se incluyen todos los registros; si @LegalBookType = 3 (libro de estados financieros) → Sobrescribe @LastYearClose con el del libro oficial (OfficialBook=1, Status=1) y mapea cuentas vía HomologationAccount else Usa el LastYearClose del libro indicado y join directo a MainAccounts; si @LastYearClose >= (@Year - 1) → @IsClosedLastYear = 1: sólo considera saldos de cierre (Month=14) del año anterior else @IsClosedLastYear = 0: considera todos los meses del año anterior y cierre del año-2; si @Visualization = 2 → Calcula columnas movementMonthStart, movementMonthIntermediate y movementMonthEnd separadas por mes else Esos campos quedan en 0; si @AccountsZero = 0 → Elimina del resultado las filas cuyos saldos y movimientos son todos cero else Conserva las cuentas en cero', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResulStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResulStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Common.Currency; GeneralLedger.LegalBook; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; GeneralLedger.MainAccountLevels; GeneralLedger.HomologationAccount; GeneralLedger.GeneralLedgerBalance; Common.ThirdParty; Payroll.CostCenter; Payroll.BranchOffice', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResulStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResulStatus';
-- GO
