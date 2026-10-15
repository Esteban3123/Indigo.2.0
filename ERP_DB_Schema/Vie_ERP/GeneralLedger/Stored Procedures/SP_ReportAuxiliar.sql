
-- =============================================
-- Author:		Juan Bermudez
-- Create date: 18/10/2016
-- Description:	Procedimiento para el reporte Auxiliar
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportAuxiliar]
	@Data AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE 
		@InitialDate AS date,
		@EndDate AS date,	
		@summarized AS bit, --Resumido	
		@Criteria AS integer, --Cuenta, Tercero, Centro	
		@SubCriteria AS integer, --Cuenta, Tercero, Centro, Fecha
		@bookId AS integer,
		@status AS integer,	
		@orderBy AS integer, --Consecutivo, Fecha
		@accumulatedBalance AS BIT, -- calculara saldo acumulado
		@accountStart AS varchar(20),
		@accountEnd AS varchar(20),
		@thirdPartyStart AS varchar(20),
		@thirdPartyEnd AS varchar(20),
		@costCenterStart AS varchar(20),
		@costCenterEnd AS varchar(20),
		-- Parámetros de paginación
		@PageNumber AS INT,
		@PageSize AS INT

	SELECT 
		@InitialDate = t.x.value('InitialDate[1]', 'date'),
		@EndDate = t.x.value('EndDate[1]', 'date'),
		@summarized = t.x.value('Summarized[1]', 'bit'),
		@Criteria = t.x.value('Criteria[1]', 'int'),
		@SubCriteria = t.x.value('SubCriteria[1]', 'int'),
		@bookId = t.x.value('BookId[1]', 'int'),
		@status = t.x.value('Status[1]', 'int'),
		@orderBy = t.x.value('OrderBy[1]', 'int'),
		@accumulatedBalance = t.x.value('AccumulatedBalance[1]', 'bit'),
		@accountStart = t.x.value('AccountStart[1]', 'varchar(20)'),
		@accountEnd = t.x.value('AccountEnd[1]', 'varchar(20)'),
		@thirdPartyStart = t.x.value('ThirdPartyStart[1]', 'varchar(20)'),
		@thirdPartyEnd = t.x.value('ThirdPartyEnd[1]', 'varchar(20)'),
		@costCenterStart = t.x.value('CostCenterStart[1]', 'varchar(20)'),
		@costCenterEnd = t.x.value('CostCenterEnd[1]', 'varchar(20)'),
		-- Leer parámetros de paginación (0 = sin paginación para compatibilidad)
		@PageNumber = ISNULL(t.x.value('PageNumber[1]', 'int'), 0),
		@PageSize = ISNULL(t.x.value('PageSize[1]', 'int'), 0)
	FROM @Data.nodes('/Data') t(x)

	-- Tabla temporal con índice para cuentas
	CREATE TABLE #TableMainAccounts (Id int PRIMARY KEY CLUSTERED, Number varchar(50))
	
	INSERT INTO #TableMainAccounts (Id, Number)
	SELECT Id, Number 
	FROM GeneralLedger.MainAccounts WITH (NOLOCK)
	WHERE Number >= @accountStart AND Number <= @accountEnd

	CREATE NONCLUSTERED INDEX IX_TMP_Number ON #TableMainAccounts(Number)

	DECLARE @IsClosedLastYear BIT = 0,
			@LegalBookName VARCHAR(200),
			@LegalBookCurrency VARCHAR(80),
			@LegalBookCurrencyAbbreviation VARCHAR(5),
			@OfficialCurrencyName VARCHAR(80),
			@OfficialCurrencyAbbreviatio VARCHAR(5),
			@InitialYear INT, @InitialMonth INT, 
			@FirstDayOfInitialMonthDate AS DATE, @LastDayOfInitialMonthDate AS DATE,
			@DetallingAccount BIT = 0, 
			@DetallingThirdParty BIT = 0,
			@DetallingCostCenter BIT = 0,
			@TotalRecords INT = 0,
			@TotalPages INT = 0

	CREATE TABLE #EntityNames
	(
		EntityName VARCHAR(220) collate Modern_Spanish_CI_AS PRIMARY KEY,
		Description VARCHAR(500) collate Modern_Spanish_CI_AS
	)

	CREATE TABLE #Table_Result 
	(
		Id INT IDENTITY(1,1) PRIMARY KEY CLUSTERED,
		IdAccount INT,
		Number VARCHAR(50),
		NameAccount VARCHAR(500),
		Nature TINYINT,
		IdThirdParty INT,
		Nit VARCHAR(50),
		NameThirdParty VARCHAR(500),
		IdCostCenter INT,
		CodeCostCenter VARCHAR(50),
		NameCodeCenter VARCHAR(500),
		DocumentDate DATETIME,
		Consecutive BIGINT,
		JournalVoucherType VARCHAR(500),
		EntityCode VARCHAR(20),
		EntityName VARCHAR(500),
		Status TINYINT,
		StatusName VARCHAR(100),
		Detail VARCHAR(1100),
		DebitValue DECIMAL(20,4),
		CreditValue DECIMAL(20,4),
		Balance DECIMAL(20,4),
		AccumulatedBalance DECIMAL(20,4),
		IsMovement BIT DEFAULT(0)
	)

	BEGIN TRY

		INSERT INTO #EntityNames 
			SELECT EntityName, Description
			FROM Common.GetEntityNameDescriptions()

		SELECT	@InitialYear = YEAR(@InitialDate), @InitialMonth = MONTH(@InitialDate), 
				@FirstDayOfInitialMonthDate = DATEADD(MONTH, DATEDIFF(MONTH, 0, @InitialDate), 0),
				@LastDayOfInitialMonthDate = DATEADD(DAY, -1, @InitialDate),
				@DetallingAccount = 1,
				@DetallingThirdParty = IIF(@Criteria = 2 OR @SubCriteria = 2, 1, 0),
				@DetallingCostCenter = IIF(@Criteria = 3 OR @SubCriteria = 3, 1, 0)

		SELECT	@OfficialCurrencyName = c.Name,
				@OfficialCurrencyAbbreviatio = c.Abbreviation
		FROM GeneralLedger.CompanySettings cs WITH (NOLOCK)
		JOIN Common.Currency c WITH (NOLOCK) ON c.Id = cs.OfficialCurrencyId
		
		SELECT	@LegalBookName = lb.Name,
				@IsClosedLastYear = IIF(lb.LastYearClose >= (@InitialYear - 1), 1, 0),
				@LegalBookCurrency = ISNULL(C.Name, @OfficialCurrencyName),
				@LegalBookCurrencyAbbreviation = ISNULL(C.Abbreviation, @OfficialCurrencyAbbreviatio)
		FROM GeneralLedger.LegalBook lb WITH (NOLOCK)
		LEFT JOIN Common.Currency c WITH (NOLOCK) ON c.Id = lb.OfficialCurrencyId
		WHERE lb.Id = @bookId
	
		-- INSERT: Saldos de GeneralLedgerBalance
		INSERT INTO #Table_Result 
			(IdAccount, Number, NameAccount, Nature, IdThirdParty, Nit, NameThirdParty, 
			 IdCostCenter, CodeCostCenter, NameCodeCenter, DebitValue, CreditValue, Balance, AccumulatedBalance)
		SELECT	ma.Id, ma.Number, ma.Name, ma.Nature,
				tp.Id, tp.Nit, tp.Name,
				cc.Id, cc.Code, cc.Name,
				0, 0, 
				SUM(CASE ma.Nature 
					WHEN 1 THEN gb.DebitValue - gb.CreditValue 
					WHEN 2 THEN gb.CreditValue - gb.DebitValue 
				END),
				SUM(CASE ma.Nature 
					WHEN 1 THEN gb.DebitValue - gb.CreditValue 
					WHEN 2 THEN gb.CreditValue - gb.DebitValue 
				END)
		FROM GeneralLedger.GeneralLedgerBalance gb WITH (NOLOCK)
		INNER JOIN #TableMainAccounts TMA ON GB.IdMainAccount = TMA.Id
		INNER JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.Id = TMA.Id
		LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON tp.Id = gb.IdThirdParty
		LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON cc.Id = gb.IdCostCenter
		WHERE ma.LegalBookId = @bookId 
			AND (
				((@IsClosedLastYear = 1 AND gb.Year = (@InitialYear - 1) AND gb.Month = 14)
				 OR
				 (@IsClosedLastYear = 0 AND ((gb.Year = (@InitialYear - 1) AND gb.Month BETWEEN 1 AND 12) OR (gb.Year = (@InitialYear - 2) AND gb.Month = 14))))
				OR
				(gb.Year = @InitialYear AND gb.Month < @InitialMonth)
			)
			AND (tp.Nit >= @thirdPartyStart AND tp.Nit <= @thirdPartyEnd OR tp.Id IS NULL)
			AND (cc.Code >= @costCenterStart AND cc.Code <= @costCenterEnd OR cc.Id IS NULL)
		GROUP BY ma.Id, ma.Number, ma.Name, ma.Nature,
				 tp.Id, tp.Nit, tp.Name,
				 cc.Id, cc.Code, cc.Name

		-- INSERT: JournalVouchers del mes anterior
		INSERT INTO #Table_Result 
			(IdAccount, Number, NameAccount, Nature, IdThirdParty, Nit, NameThirdParty, 
			 IdCostCenter, CodeCostCenter, NameCodeCenter, DebitValue, CreditValue, Balance, AccumulatedBalance)
		SELECT	ma.Id, ma.Number, ma.Name, ma.Nature,
				tp.Id, tp.Nit, tp.Name, 
				cc.Id, cc.Code, cc.Name,
				0, 0,
				SUM(CASE ma.Nature 
					WHEN 1 THEN jvd.DebitValue - jvd.CreditValue 
					WHEN 2 THEN jvd.CreditValue - jvd.DebitValue 
				END),
				SUM(CASE ma.Nature 
					WHEN 1 THEN jvd.DebitValue - jvd.CreditValue 
					WHEN 2 THEN jvd.CreditValue - jvd.DebitValue 
				END)
		FROM GeneralLedger.JournalVouchers jv WITH (NOLOCK)
		INNER JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
		INNER JOIN #TableMainAccounts TMA ON TMA.Id = jvd.IdMainAccount
		INNER JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.Id = TMA.Id
		LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON tp.Id = jvd.IdThirdParty
		LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON cc.Id = jvd.IdCostCenter
		WHERE jv.LegalBookId = @bookId 
			AND jv.IsClosedYear = 0
			AND (@status = 4 OR jv.Status = @status)
			AND jv.VoucherDate >= @FirstDayOfInitialMonthDate 
			AND jv.VoucherDate < DATEADD(DAY, 1, @LastDayOfInitialMonthDate)
			AND (tp.Nit >= @thirdPartyStart AND tp.Nit <= @thirdPartyEnd OR tp.Id IS NULL)
			AND (cc.Code >= @costCenterStart AND cc.Code <= @costCenterEnd OR cc.Id IS NULL)
		GROUP BY ma.Id, ma.Number, ma.Name, ma.Nature, 
				 tp.Id, tp.Nit, tp.Name,
				 cc.Id, cc.Code, cc.Name
		
		-- INSERT: Movimientos del período
		INSERT INTO #Table_Result 
			(IdAccount, Number, NameAccount, Nature, IdThirdParty, Nit, NameThirdParty, 
			 IdCostCenter, CodeCostCenter, NameCodeCenter, DocumentDate, Consecutive, 
			 JournalVoucherType, EntityCode, EntityName, Status, StatusName, Detail,
			 DebitValue, CreditValue, Balance, AccumulatedBalance, IsMovement)
		SELECT	ma.Id, ma.Number, ma.Name, ma.Nature,
				t.Id, t.Nit, t.Name, 
				c.Id, c.Code, c.Name, 
				jv.VoucherDate, jv.Consecutive, 
				jvt.Code + ' - ' + jvt.Name, 
				jv.EntityCode, gend.Description, jv.Status, 
				CASE jv.Status
					WHEN 1 THEN 'Registrado'
					WHEN 2 THEN 'Confirmado'
					WHEN 3 THEN 'Anulado'
					ELSE 'N/A'
				END, 
				ISNULL(CONVERT(varchar(500), jv.Detail) + '. ', '') + ISNULL(CONVERT(varchar(500), jvd.Detail), ''), 
				jvd.DebitValue, jvd.CreditValue, 0, 
				IIF(ma.Nature = 1, jvd.DebitValue - jvd.CreditValue, jvd.CreditValue - jvd.DebitValue), 
				1
		FROM GeneralLedger.JournalVouchers jv WITH (NOLOCK)
		INNER JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
		INNER JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
		INNER JOIN #TableMainAccounts TMA ON TMA.Id = jvd.IdMainAccount
		INNER JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.Id = TMA.Id
		LEFT JOIN Common.ThirdParty t WITH (NOLOCK) ON t.Id = jvd.IdThirdParty
		LEFT JOIN Payroll.CostCenter c WITH (NOLOCK) ON c.Id = jvd.IdCostCenter
		LEFT JOIN #EntityNames gend ON jv.EntityName = gend.EntityName
		WHERE jv.LegalBookId = @bookId 
			AND jv.IsClosedYear = 0
			AND (@status = 4 OR jv.Status = @status)
			AND jv.VoucherDate >= CAST(@InitialDate AS DATETIME) 
			AND jv.VoucherDate < DATEADD(DAY, 1, CAST(@EndDate AS DATETIME))
			AND (t.Nit >= @thirdPartyStart AND t.Nit <= @thirdPartyEnd OR t.Id IS NULL)
			AND (c.Code >= @costCenterStart AND c.Code <= @costCenterEnd OR c.Id IS NULL)

		-- Índice para saldo acumulado
		CREATE NONCLUSTERED INDEX IX_TMP_Result_Acumulado 
			ON #Table_Result(IdAccount, IdThirdParty, IdCostCenter, Id) 
			INCLUDE (AccumulatedBalance, IsMovement)
		
		IF @summarized = 0 AND @accumulatedBalance = 1
		BEGIN
			;WITH CTE_Acumulado AS (
				SELECT 
					Id,
					SUM(AccumulatedBalance) OVER (
						PARTITION BY IdAccount, ISNULL(IdThirdParty, 0), ISNULL(IdCostCenter, 0) 
						ORDER BY Id 
						ROWS UNBOUNDED PRECEDING
					) AS SaldoAcumulado
				FROM #Table_Result
				WHERE IsMovement = 1
			)
			UPDATE tr 
			SET tr.Balance = cte.SaldoAcumulado
			FROM #Table_Result tr
			INNER JOIN CTE_Acumulado cte ON tr.Id = cte.Id
		END

		-- Contar total de registros para paginación
		SELECT @TotalRecords = COUNT(*) FROM #Table_Result
		
		-- Calcular total de páginas
		IF @PageSize > 0
			SET @TotalPages = CEILING(CAST(@TotalRecords AS FLOAT) / @PageSize)
		ELSE
			SET @TotalPages = 1

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
		RETURN
	END CATCH

	-- SELECT FINAL CON PAGINACIÓN
	IF @PageSize > 0 AND @PageNumber > 0
	BEGIN
		-- CON PAGINACIÓN: Retorna página específica + metadatos
		SELECT	@TotalRecords AS TotalRecords,
				@TotalPages AS TotalPages,
				@PageNumber AS CurrentPage,
				@PageSize AS PageSize,
				@LegalBookName LegalBookName,
				@LegalBookCurrency LegalBookCurrency,
				@LegalBookCurrencyAbbreviation LegalBookCurrencyAbbreviation,
				IIF(@summarized = 0, tr.IsMovement, NULL) IsMovement,
				IIF(@summarized = 0, IIF(tr.IsMovement = 0, NULL, tr.Id), NULL) Id,
				IIF(@DetallingAccount = 1, tr.IdAccount, NULL) IdAccount,
				IIF(@DetallingAccount = 1, tr.Number, NULL) Number,
				IIF(@DetallingAccount = 1, tr.NameAccount, NULL) NameAccount,
				tr.Nature,
				CASE tr.Nature
					WHEN 1 THEN 'Débito'
					WHEN 2 THEN 'Crédito'
				END NatureName,
				IIF(@DetallingThirdParty = 1, tr.IdThirdParty, NULL) IdThirdParty,
				IIF(@DetallingThirdParty = 1, tr.Nit, NULL) Nit,
				IIF(@DetallingThirdParty = 1, tr.NameThirdParty, NULL) NameThirdParty,
				IIF(@DetallingCostCenter = 1, tr.IdCostCenter, NULL) IdCostCenter,
				IIF(@DetallingCostCenter = 1, tr.CodeCostCenter, NULL) CodeCostCenter,
				IIF(@DetallingCostCenter = 1, tr.NameCodeCenter, NULL) NameCodeCenter,
				IIF(@summarized = 0, tr.DocumentDate, NULL) DocumentDate,
				IIF(@summarized = 0, tr.Consecutive, NULL) Consecutive,
				IIF(@summarized = 0, tr.JournalVoucherType, NULL) JournalVoucherType,			
				IIF(@summarized = 0, tr.EntityCode, NULL) EntityCode,
				IIF(@summarized = 0, tr.EntityName, NULL) EntityName,
				IIF(@summarized = 0, tr.Status, NULL) Status,
				IIF(@summarized = 0, tr.StatusName, NULL) StatusName,
				IIF(@summarized = 0, tr.Detail, NULL) Detail,
				SUM(IIF(tr.IsMovement = 0, tr.Balance, 0)) PreviousBalance,
				SUM(tr.DebitValue) DebitValue, 
				SUM(tr.CreditValue) CreditValue,
				SUM(IIF(@summarized = 1, tr.AccumulatedBalance, tr.Balance)) Balance
		FROM #Table_Result tr
		GROUP BY	IIF(@summarized = 0, tr.IsMovement, NULL),
					IIF(@summarized = 0, IIF(tr.IsMovement = 0, NULL, tr.Id), NULL),
					IIF(@DetallingAccount = 1, tr.IdAccount, NULL),
					IIF(@DetallingAccount = 1, tr.Number, NULL),
					IIF(@DetallingAccount = 1, tr.NameAccount, NULL),
					tr.Nature,
					IIF(@DetallingThirdParty = 1, tr.IdThirdParty, NULL),
					IIF(@DetallingThirdParty = 1, tr.Nit, NULL),
					IIF(@DetallingThirdParty = 1, tr.NameThirdParty, NULL),
					IIF(@DetallingCostCenter = 1, tr.IdCostCenter, NULL),
					IIF(@DetallingCostCenter = 1, tr.CodeCostCenter, NULL),
					IIF(@DetallingCostCenter = 1, tr.NameCodeCenter, NULL),
					IIF(@summarized = 0, tr.DocumentDate, NULL),
					IIF(@summarized = 0, tr.Consecutive, NULL),
					IIF(@summarized = 0, tr.JournalVoucherType, NULL),
					IIF(@summarized = 0, tr.EntityCode, NULL),
					IIF(@summarized = 0, tr.EntityName, NULL),
					IIF(@summarized = 0, tr.Status, NULL),
					IIF(@summarized = 0, tr.StatusName, NULL),
					IIF(@summarized = 0, tr.Detail, NULL)
		ORDER BY	
					-- 1. Agrupaciones Principales
					IIF(@DetallingAccount = 1, tr.Number, NULL),
					IIF(@DetallingThirdParty = 1, tr.Nit, NULL),
					IIF(@DetallingCostCenter = 1, tr.CodeCostCenter, NULL),
					
					-- 2. Asegurar que el Saldo Anterior (IsMovement=0) salga primero
					IIF(@summarized = 0, tr.IsMovement, NULL), 

					-- 3. Ordenar cronológicamente por fecha
					IIF(@summarized = 0, tr.DocumentDate, NULL),

					-- 4. Si es la misma fecha, ordenar por consecutivo
					IIF(@summarized = 0, tr.Consecutive, NULL)
		OFFSET (@PageNumber - 1) * @PageSize ROWS
		FETCH NEXT @PageSize ROWS ONLY
	END
	ELSE
	BEGIN
		-- SIN PAGINACIÓN (comportamiento original para compatibilidad)
		SELECT	@TotalRecords AS TotalRecords,
				@TotalPages AS TotalPages,
				0 AS CurrentPage,
				0 AS PageSize,
				@LegalBookName LegalBookName,
				@LegalBookCurrency LegalBookCurrency,
				@LegalBookCurrencyAbbreviation LegalBookCurrencyAbbreviation,
				IIF(@summarized = 0, tr.IsMovement, NULL) IsMovement,
				IIF(@summarized = 0, IIF(tr.IsMovement = 0, NULL, tr.Id), NULL) Id,
				IIF(@DetallingAccount = 1, tr.IdAccount, NULL) IdAccount,
				IIF(@DetallingAccount = 1, tr.Number, NULL) Number,
				IIF(@DetallingAccount = 1, tr.NameAccount, NULL) NameAccount,
				tr.Nature,
				CASE tr.Nature
					WHEN 1 THEN 'Débito'
					WHEN 2 THEN 'Crédito'
				END NatureName,
				IIF(@DetallingThirdParty = 1, tr.IdThirdParty, NULL) IdThirdParty,
				IIF(@DetallingThirdParty = 1, tr.Nit, NULL) Nit,
				IIF(@DetallingThirdParty = 1, tr.NameThirdParty, NULL) NameThirdParty,
				IIF(@DetallingCostCenter = 1, tr.IdCostCenter, NULL) IdCostCenter,
				IIF(@DetallingCostCenter = 1, tr.CodeCostCenter, NULL) CodeCostCenter,
				IIF(@DetallingCostCenter = 1, tr.NameCodeCenter, NULL) NameCodeCenter,
				IIF(@summarized = 0, tr.DocumentDate, NULL) DocumentDate,
				IIF(@summarized = 0, tr.Consecutive, NULL) Consecutive,
				IIF(@summarized = 0, tr.JournalVoucherType, NULL) JournalVoucherType,			
				IIF(@summarized = 0, tr.EntityCode, NULL) EntityCode,
				IIF(@summarized = 0, tr.EntityName, NULL) EntityName,
				IIF(@summarized = 0, tr.Status, NULL) Status,
				IIF(@summarized = 0, tr.StatusName, NULL) StatusName,
				IIF(@summarized = 0, tr.Detail, NULL) Detail,
				SUM(IIF(tr.IsMovement = 0, tr.Balance, 0)) PreviousBalance,
				SUM(tr.DebitValue) DebitValue, 
				SUM(tr.CreditValue) CreditValue,
				SUM(IIF(@summarized = 1, tr.AccumulatedBalance, tr.Balance)) Balance
		FROM #Table_Result tr
		GROUP BY	IIF(@summarized = 0, tr.IsMovement, NULL),
					IIF(@summarized = 0, IIF(tr.IsMovement = 0, NULL, tr.Id), NULL),
					IIF(@DetallingAccount = 1, tr.IdAccount, NULL),
					IIF(@DetallingAccount = 1, tr.Number, NULL),
					IIF(@DetallingAccount = 1, tr.NameAccount, NULL),
					tr.Nature,
					IIF(@DetallingThirdParty = 1, tr.IdThirdParty, NULL),
					IIF(@DetallingThirdParty = 1, tr.Nit, NULL),
					IIF(@DetallingThirdParty = 1, tr.NameThirdParty, NULL),
					IIF(@DetallingCostCenter = 1, tr.IdCostCenter, NULL),
					IIF(@DetallingCostCenter = 1, tr.CodeCostCenter, NULL),
					IIF(@DetallingCostCenter = 1, tr.NameCodeCenter, NULL),
					IIF(@summarized = 0, tr.DocumentDate, NULL),
					IIF(@summarized = 0, tr.Consecutive, NULL),
					IIF(@summarized = 0, tr.JournalVoucherType, NULL),
					IIF(@summarized = 0, tr.EntityCode, NULL),
					IIF(@summarized = 0, tr.EntityName, NULL),
					IIF(@summarized = 0, tr.Status, NULL),
					IIF(@summarized = 0, tr.StatusName, NULL),
					IIF(@summarized = 0, tr.Detail, NULL)
		ORDER BY	
					IIF(@DetallingAccount = 1, tr.Number, NULL),
					IIF(@DetallingThirdParty = 1, tr.Nit, NULL),
					IIF(@DetallingCostCenter = 1, tr.CodeCostCenter, NULL),
					IIF(@summarized = 0, tr.IsMovement, NULL), 
					IIF(@summarized = 0, tr.DocumentDate, NULL),
					IIF(@summarized = 0, tr.Consecutive, NULL)
	END

	-- Limpiar tablas temporales
	DROP TABLE #TableMainAccounts
	DROP TABLE #EntityNames
	DROP TABLE #Table_Result
END