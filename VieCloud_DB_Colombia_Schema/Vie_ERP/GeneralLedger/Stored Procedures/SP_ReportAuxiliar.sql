
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
		@PageNumber = ISNULL(t.x.value('PageNumber[1]', 'int'), 0),
		@PageSize = ISNULL(t.x.value('PageSize[1]', 'int'), 0)
	FROM @Data.nodes('/Data') t(x)

	-- Optimización: Usar tabla temporal con índice en lugar de variable de tabla
	-- Las variables de tabla no tienen estadísticas y causan table scans
	CREATE TABLE #TableMainAccounts (Id int PRIMARY KEY CLUSTERED, Number varchar(50))
	CREATE NONCLUSTERED INDEX IX_TMP_Number ON #TableMainAccounts(Number)
	
	INSERT INTO #TableMainAccounts (Id, Number)
	SELECT Id, Number 
	FROM GeneralLedger.MainAccounts 
	WHERE Number >= @accountStart AND Number <= @accountEnd

	DECLARE @IsClosedLastYear BIT = 0,
			@LegalBookName VARCHAR(200),
			@LegalBookCurrency VARCHAR(80),
			@LegalBookCurrencyAbbreviation VARCHAR(5),
			@OfficialCurrencyName VARCHAR(80),
			@OfficialCurrencyAbbreviatio VARCHAR(5),
			------------------------------------------------------------------------
			@InitialYear INT, @InitialMonth INT, 
			@FirstDayOfInitialMonthDate AS DATE, @LastDayOfInitialMonthDate AS DATE,
			------------------------------------------------------------------------
			@DetallingAccount BIT = 0, 
			@DetallingThirdParty BIT = 0,
			@DetallingCostCenter BIT = 0,
			@TotalRecords INT = 0,
			@TotalPages INT = 0,
			@EffectivePageNumber INT = 1,
			@EffectivePageSize INT = 1

	CREATE TABLE #EntityNames
	(
		EntityName VARCHAR(220) collate Modern_Spanish_CI_AS,
		Description VARCHAR(500) collate Modern_Spanish_CI_AS
	)

	CREATE TABLE #Table_Result 
	(
		Id INT IDENTITY(1,1),
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

		/*****************************************************************************************/

		SELECT	@InitialYear = YEAR(@InitialDate), @InitialMonth = MONTH(@InitialDate), 
				@FirstDayOfInitialMonthDate = DATEADD(MONTH, DATEDIFF(MONTH, 0, @InitialDate), 0),
				@LastDayOfInitialMonthDate = DATEADD(DAY, -1, @InitialDate),
				-----------------------------------------------------------------------------------
				--A nivel de consulta trae todos los campos, y el agrupamiento se hace a nivel de reporte o formulario
				@DetallingAccount = 1,
				@DetallingThirdParty = IIF(@Criteria = 2 OR @SubCriteria = 2, 1, 0),
				@DetallingCostCenter = IIF(@Criteria = 3 OR @SubCriteria = 3, 1, 0)

		SELECT	@OfficialCurrencyName = c.Name,
				@OfficialCurrencyAbbreviatio =c.Abbreviation
		FROM GeneralLedger.CompanySettings cs WITH (NOLOCK)
	    join Common.Currency c WITH (NOLOCK) on c.Id = cs.OfficialCurrencyId
		
	    
		SELECT	@LegalBookName = lb.Name,
				@IsClosedLastYear = IIf(lb.LastYearClose >= (@InitialYear - 1), 1, 0),
				@LegalBookCurrency = ISNULL(C.Name,@OfficialCurrencyName),
				@LegalBookCurrencyAbbreviation = ISNULL(C.Abbreviation,@OfficialCurrencyAbbreviatio)
		FROM GeneralLedger.LegalBook lb WITH (NOLOCK)
		left join Common.Currency c WITH (NOLOCK) on c.Id = lb.OfficialCurrencyId
		WHERE lb.Id = @bookId
	
		;WITH CTR_Mov AS 
		(
			SELECT 
				ma.Id IdMainAccount, ma.Number, ma.Name AS NameAccount, ma.Nature,
				tp.Id IdThirdParty, tp.Nit, tp.Name AS NameThirdParty,
				cc.Id IdCostCenter, cc.Code CodeCostCenter, cc.Name NameCostCenter,
				CASE ma.Nature 
					WHEN 1 THEN SUM(gb.DebitValue) - SUM(gb.CreditValue) 
					WHEN 2 THEN SUM(gb.CreditValue) - SUM(gb.DebitValue) 
				END AS Balance
			FROM GeneralLedger.GeneralLedgerBalance gb WITH (NOLOCK)
			INNER JOIN #TableMainAccounts TMA ON GB.IdMainAccount = TMA.Id
			JOIN GeneralLedger.MainAccounts AS ma WITH (NOLOCK) ON ma.Id = TMA.Id
			LEFT JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON tp.Id = gb.IdThirdParty
			LEFT JOIN Payroll.CostCenter AS cc WITH (NOLOCK) ON cc.Id = gb.IdCostCenter
			WHERE ma.LegalBookId = @bookId AND 
				(
					(
						(@IsClosedLastYear = 1 AND gb.Year = (@InitialYear - 1) AND gb.Month = 14)
						OR
						(@IsClosedLastYear = 0 AND ((gb.Year = (@InitialYear - 1) AND gb.Month BETWEEN 1 AND 12) OR (gb.Year = (@InitialYear - 2) AND gb.Month = 14)))
					)
					OR
					(gb.Year = @InitialYear AND gb.Month < @InitialMonth)
				)
				AND ISNULL(tp.Nit, '0') BETWEEN @thirdPartyStart AND @thirdPartyEnd 
				AND ISNULL(cc.Code,'0') BETWEEN @costCenterStart AND @costCenterEnd
			GROUP BY	ma.Id, ma.Number, ma.Name, ma.Nature,
							tp.Id, tp.Nit, tp.Name,
							cc.Id, cc.Code, cc.Name

		UNION ALL

			SELECT 
				ma.Id, ma.Number, ma.Name, ma.Nature,
				tp.Id, tp.Nit, tp.Name, 
				cc.Id, cc.Code, cc.Name,
				CASE ma.Nature 
					WHEN 1 THEN SUM(jvd.DebitValue) - SUM(jvd.CreditValue) 
					WHEN 2 THEN SUM(jvd.CreditValue) - SUM(jvd.DebitValue) 
				END AS Balance
			FROM GeneralLedger.JournalVouchers jv WITH (NOLOCK)
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			INNER JOIN #TableMainAccounts TMA ON TMA.Id = JVD.IdAccounting
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.Id = TMA.Id
			LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON tp.Id = jvd.IdThirdParty
			LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON cc.Id = jvd.IdCostCenter
			WHERE jv.LegalBookId = @bookId AND jv.IsClosedYear = 0
				AND (@status = 4 OR jv.Status = @status)
				-- Optimización: Evitar CAST para permitir uso de índices en VoucherDate
				AND jv.VoucherDate >= @FirstDayOfInitialMonthDate 
				AND jv.VoucherDate < DATEADD(DAY, 1, @LastDayOfInitialMonthDate)
				AND ISNULL(tp.Nit,'0') BETWEEN @thirdPartyStart AND @thirdPartyEnd 
				AND ISNULL(cc.Code,'0') BETWEEN @costCenterStart AND @costCenterEnd
			GROUP BY	ma.Id, ma.Number, ma.Name, ma.Nature, 
						tp.Id, tp.Nit, tp.Name,
						cc.Id, cc.Code, cc.Name
		)

		/*****************************************************************************************/
		--select * from 
		INSERT INTO #Table_Result 
			(
				IdAccount, Number, NameAccount, Nature,
				IdThirdParty, Nit, NameThirdParty, 
				IdCostCenter, CodeCostCenter, NameCodeCenter,
				DebitValue, CreditValue, Balance, AccumulatedBalance
			)
			SELECT	IdMainAccount IdAccount, Number, NameAccount, Nature,
					IdThirdParty, Nit, NameThirdParty, 
					IdCostCenter, CodeCostCenter, NameCostCenter,
					0, 0, SUM(Balance) Balance, SUM(Balance) AccumulatedBalance
			FROM 
			(
				SELECT 
					IdMainAccount, Number, NameAccount, Nature,
					IdThirdParty, Nit, NameThirdParty,
					IdCostCenter, CodeCostCenter, NameCostCenter,
					Balance
				FROM CTR_Mov

			) AS data
			GROUP BY	IdMainAccount, Number, NameAccount, Nature,
						IdThirdParty, Nit, NameThirdParty,
						IdCostCenter, CodeCostCenter, NameCostCenter
		
		INSERT INTO #Table_Result 
			SELECT	ma.Id AS IdAccount, ma.Number, ma.Name AS NameAccount, ma.Nature,
					t.Id AS IdThirdParty, t.Nit, t.Name AS NameThirdParty, 
					c.Id AS IdCostCenter, c.Code AS CodeCostCenter, c.Name AS NameCostCenter, 
					jv.VoucherDate AS DocumentDate, jv.Consecutive, jvt.Code + ' - ' + jvt.Name AS JournalVoucherType, 
					jv.EntityCode, gend.Description EntityName, jv.Status, 
					CASE jv.Status
						WHEN 1 THEN 'Registrado'
						WHEN 2 THEN 'Confirmado'
						WHEN 3 THEN 'Anulado'
						ELSE 'N/A'
					END StatusName, 
					ISNULL(convert(varchar(500),jv.Detail) + '. ', '') + ISNULL(convert(varchar (500),jvd.Detail), '') AS Detail, 
					jvd.DebitValue, jvd.CreditValue, 0, 
					IIF(ma.Nature = 1, jvd.DebitValue - jvd.CreditValue, jvd.CreditValue - jvd.DebitValue), 1
			FROM GeneralLedger.JournalVouchers jv WITH (NOLOCK)
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
			INNER JOIN #TableMainAccounts TMA ON TMA.Id = jvd.IdMainAccount
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.Id = TMA.Id
			LEFT JOIN Common.ThirdParty t WITH (NOLOCK) ON t.Id = jvd.IdThirdParty
			LEFT JOIN Payroll.CostCenter c WITH (NOLOCK) ON c.Id = jvd.IdCostCenter
			LEFT JOIN #EntityNames gend ON jv.EntityName = gend.EntityName
			WHERE jv.LegalBookId = @bookId AND jv.IsClosedYear = 0
				AND (@status = 4 OR jv.Status = @status)
				-- Optimización: Evitar CAST para permitir uso de índices en VoucherDate
				AND jv.VoucherDate >= CAST(@InitialDate AS DATETIME) 
				AND jv.VoucherDate < DATEADD(DAY, 1, CAST(@EndDate AS DATETIME))
				AND ISNULL(t.Nit,'0') BETWEEN @thirdPartyStart AND @thirdPartyEnd 
				AND ISNULL(c.Code,'0') BETWEEN @costCenterStart AND @costCenterEnd
			ORDER BY
				CASE @orderBy
					WHEN 1 THEN IIF(@summarized = 0, jv.Consecutive, NULL)
					WHEN 2 THEN IIF(@summarized = 0, jv.VoucherDate, NULL)
				END,
				jvd.Id

		/*****************************************************************************************/
		
		IF @summarized = 0 AND @accumulatedBalance = 1
		BEGIN
			-- Optimización: Crear índice en tabla temporal para acelerar el self-join
			CREATE NONCLUSTERED INDEX IX_TableResult_Acum 
				ON #Table_Result(IdAccount, IdThirdParty, IdCostCenter, Id) 
				INCLUDE (AccumulatedBalance, IsMovement);
			
			;WITH CTE_Acumulado AS
			(
				SELECT Id,
					SUM(AccumulatedBalance) OVER (
						PARTITION BY IdAccount, ISNULL(IdThirdParty, 0), ISNULL(IdCostCenter, 0)
						ORDER BY Id
						ROWS UNBOUNDED PRECEDING
					) AS SaldoAcumulado
				FROM #Table_Result
			)
			UPDATE tr
				SET tr.Balance = cte.SaldoAcumulado
			FROM #Table_Result tr
			INNER JOIN CTE_Acumulado cte ON tr.Id = cte.Id
			WHERE tr.IsMovement = 1
		END

		-- Sin paginar, el dataset completo excede el buffer WCF (2GB) con volúmenes altos (>119K filas) y produce error 500.
		SELECT @TotalRecords = COUNT(*) FROM #Table_Result

		IF @PageSize > 0
			SET @TotalPages = CEILING(CAST(@TotalRecords AS FLOAT) / @PageSize)
		ELSE
			SET @TotalPages = 1

		SET @EffectivePageNumber = IIF(@PageNumber > 0, @PageNumber, 1)
		SET @EffectivePageSize = CASE WHEN @PageSize > 0 THEN @PageSize WHEN @TotalRecords > 0 THEN @TotalRecords ELSE 1 END

	END TRY
	BEGIN CATCH
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
		RETURN
	END CATCH

	SELECT	@TotalRecords AS TotalRecords,
			@TotalPages AS TotalPages,
			@PageNumber AS CurrentPage,
			@PageSize AS PageSize,
			@LegalBookName LegalBookName,
			@LegalBookCurrency LegalBookCurrency,
			@LegalBookCurrencyAbbreviation LegalBookCurrencyAbbreviation,
			IIF(@summarized = 0, tr.IsMovement, NULL) IsMovement,
			IIF(@summarized = 0, IIF(tr.IsMovement = 0, NULL, tr.Id), NULL) Id,
			----------------------------
			IIF(@DetallingAccount = 1, tr.IdAccount, NULL) IdAccount,
			IIF(@DetallingAccount = 1, tr.Number, NULL) Number,
			IIF(@DetallingAccount = 1, tr.NameAccount, NULL) NameAccount,
			tr.Nature,
			CASE tr.Nature
				WHEN 1 THEN 'Débito'
				WHEN 2 THEN 'Crédito'
			END NatureName,
			----------------------------
			IIF(@DetallingThirdParty = 1, tr.IdThirdParty, NULL) IdThirdParty,
			IIF(@DetallingThirdParty = 1, tr.Nit, NULL) Nit,
			IIF(@DetallingThirdParty = 1, tr.NameThirdParty, NULL) NameThirdParty,
			----------------------------
			IIF(@DetallingCostCenter = 1, tr.IdCostCenter, NULL) IdCostCenter,
			IIF(@DetallingCostCenter = 1, tr.CodeCostCenter, NULL) CodeCostCenter,
			IIF(@DetallingCostCenter = 1, tr.NameCodeCenter, NULL) NameCodeCenter,
			----------------------------
			IIF(@summarized = 0, tr.DocumentDate, NULL) DocumentDate,
			IIF(@summarized = 0, tr.Consecutive, NULL) Consecutive,
			IIF(@summarized = 0, tr.JournalVoucherType, NULL) JournalVoucherType,			
			IIF(@summarized = 0, tr.EntityCode, NULL) EntityCode,
			IIF(@summarized = 0, tr.EntityName, NULL) EntityName,
			IIF(@summarized = 0, tr.Status, NULL) Status,
			IIF(@summarized = 0, tr.StatusName, NULL) StatusName,
			IIF(@summarized = 0, tr.Detail, NULL) Detail,
			----------------------------
			SUM(IIF(tr.IsMovement = 0, tr.Balance, 0)) PreviousBalance,
			SUM(tr.DebitValue) DebitValue, 
			SUM(tr.CreditValue) CreditValue,
			SUM(IIF(@summarized = 1, tr.AccumulatedBalance, tr.Balance)) Balance
	FROM #Table_Result  tr
	GROUP BY	IIF(@summarized = 0, tr.IsMovement, NULL),
				IIF(@summarized = 0, IIF(tr.IsMovement = 0, NULL, tr.Id), NULL),
				-------------------------------------------------
				IIF(@DetallingAccount = 1, tr.IdAccount, NULL),
				IIF(@DetallingAccount = 1, tr.Number, NULL),
				IIF(@DetallingAccount = 1, tr.NameAccount, NULL),
				tr.Nature,
				-------------------------------------------------
				IIF(@DetallingThirdParty = 1, tr.IdThirdParty, NULL),
				IIF(@DetallingThirdParty = 1, tr.Nit, NULL),
				IIF(@DetallingThirdParty = 1, tr.NameThirdParty, NULL),
				-------------------------------------------------
				IIF(@DetallingCostCenter = 1, tr.IdCostCenter, NULL),
				IIF(@DetallingCostCenter = 1, tr.CodeCostCenter, NULL),
				IIF(@DetallingCostCenter = 1, tr.NameCodeCenter, NULL),
				-------------------------------------------------
				IIF(@summarized = 0, tr.DocumentDate, NULL),
				IIF(@summarized = 0, tr.Consecutive, NULL),
				IIF(@summarized = 0, tr.JournalVoucherType, NULL),
				IIF(@summarized = 0, tr.EntityCode, NULL),
				IIF(@summarized = 0, tr.EntityName, NULL),
				IIF(@summarized = 0, tr.Status, NULL),
				IIF(@summarized = 0, tr.StatusName, NULL),
				IIF(@summarized = 0, tr.Detail, NULL)
	ORDER BY	IIF(@DetallingAccount = 1, tr.Number, NULL),
				IIF(@DetallingThirdParty = 1, tr.Nit, NULL),
				IIF(@DetallingCostCenter = 1, tr.CodeCostCenter, NULL),
				IIF(@summarized = 0, IIF(tr.IsMovement = 0, NULL, tr.Id), NULL)
	OFFSET (@EffectivePageNumber - 1) * @EffectivePageSize ROWS
	FETCH NEXT @EffectivePageSize ROWS ONLY
END

GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte del libro auxiliar contable para un período y libro legal seleccionado. Consolida los movimientos y saldos de las cuentas del plan contable (cuentas mayores), cruzando débitos y créditos con terceros (proveedores, aseguradoras, entidades) y centros de costo, calculando saldos iniciales, movimientos del período y saldo acumulado. Permite filtrar por rango de cuentas, rango de terceros, rango de centros de costo, fechas, estado del comprobante y ordenamiento; además soporta vista resumida o detallada, agrupación por cuenta, tercero o centro de costo, y tiene en cuenta si el año anterior fue cerrado contablemente. Se apoya en las cuentas principales (MainAccounts), los saldos del libro mayor (GeneralLedgerBalance), los terceros (ThirdParty), la configuración de la empresa (CompanySettings) y el libro legal (LegalBook) para obtener la moneda oficial y el nombre del libro, produciendo la información necesaria para reportería contable y auditoría de movimientos.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportAuxiliar';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportAuxiliar';
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte auxiliar contable mostrando saldo anterior y movimientos del libro mayor por cuenta, tercero y/o centro de costo en un rango de fechas, con opción resumida/detallada y saldo acumulado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAuxiliar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro XML @Data debe contener el nodo /Data con InitialDate, EndDate, BookId, Criteria, SubCriteria, OrderBy y rangos de cuenta/tercero/centro de costo.; Debe existir un registro en GeneralLedger.LegalBook para el @bookId indicado.; Debe existir configuración en GeneralLedger.CompanySettings con OfficialCurrencyId válido para obtener moneda oficial.; Los rangos AccountStart/AccountEnd, ThirdPartyStart/ThirdPartyEnd y CostCenterStart/CostCenterEnd deben estar definidos para filtrar correctamente.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAuxiliar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] #TableMainAccounts: Carga cuentas principales cuyo Number esté entre @accountStart y @accountEnd para acotar el universo de cuentas del reporte.; [INSERT] #EntityNames: Carga nombres y descripciones de entidades desde Common.GetEntityNameDescriptions() para resolver el EntityName de los comprobantes.; [INSERT] #Table_Result: Inserta el saldo anterior agregado (IsMovement=0): si el año anterior está cerrado (LastYearClose >= @InitialYear-1) usa Year=@InitialYear-1 y Month=14; si no, suma Year=@InitialYear-1 meses 1..12 más Year=@InitialYear-2 mes 14; y siempre suma los movimientos del año actual con Month < @InitialMonth.; [INSERT] #Table_Result: Inserta también los movimientos no cerrados (jv.IsClosedYear=0) de JournalVouchers cuya VoucherDate esté entre el primer día del mes inicial y el día anterior a @InitialDate, usados como parte del saldo anterior.; [INSERT] #Table_Result: Inserta los movimientos detallados (IsMovement=1) de comprobantes con jv.IsClosedYear=0, VoucherDate entre @InitialDate y @EndDate, filtrados por @status (si @status=4 trae todos), por rango de tercero y centro de costo.; [UPDATE] #Table_Result: Cuando @summarized=0 y @accumulatedBalance=1, recalcula Balance como la suma acumulada del AccumulatedBalance de los registros previos (Id menor) con misma cuenta, tercero y centro de costo, más el propio AccumulatedBalance.; [RETURN_RESULT] RESULT: Devuelve el reporte agregando saldo anterior, débitos, créditos y saldo, mostrando u ocultando columnas según @summarized, @DetallingAccount, @DetallingThirdParty y @DetallingCostCenter.; [RETURN_RESULT] RESULT: Si ocurre cualquier excepción dentro del TRY, devuelve un resultset con Code=''999'', el mensaje de error y la línea.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAuxiliar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si lb.LastYearClose >= (@InitialYear - 1) → Considera el año anterior cerrado y toma el saldo del cierre (Month=14) del año anterior como base. else Toma los movimientos mensuales (Month 1..12) del año anterior y el cierre (Month=14) de dos años atrás para construir el saldo inicial.; si @Criteria = 2 OR @SubCriteria = 2 → Activa @DetallingThirdParty=1 y el reporte detalla por tercero (Nit, NameThirdParty). else No detalla por tercero; estos campos se devuelven NULL.; si @Criteria = 3 OR @SubCriteria = 3 → Activa @DetallingCostCenter=1 y el reporte detalla por centro de costo. else No detalla por centro de costo; estos campos se devuelven NULL.; si @status = 4 → No filtra por estado del comprobante (incluye todos los estados). else Filtra los comprobantes con jv.Status = @status.; si @summarized = 0 AND @accumulatedBalance = 1 → Calcula y actualiza el saldo acumulado por movimiento sumando los AccumulatedBalance previos del mismo grupo (cuenta/tercero/centro). else No calcula saldo acumulado fila a fila.; si @summarized = 1 → El reporte oculta detalles de movimiento (fecha, consecutivo, tipo, entidad, estado, detalle) y devuelve el saldo basado en AccumulatedBalance. else Devuelve la información detallada de cada comprobante y usa Balance como saldo de la fila.; si @orderBy = 1 → Ordena los movimientos por jv.Consecutive cuando no es resumido. else Si @orderBy=2 ordena por jv.VoucherDate cuando no es resumido.; si ma.Nature = 1 (Débito) → Calcula Balance como SUM(DebitValue) - SUM(CreditValue) y NatureName=''Débito''. else Si Nature=2 calcula Balance como SUM(CreditValue) - SUM(DebitValue) y NatureName=''Crédito''.; si jv.Status IN (1,2,3) → Asigna StatusName ''Registrado'', ''Confirmado'' o ''Anulado'' respectivamente. else Asigna StatusName ''N/A''.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAuxiliar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportAuxiliar';
-- GO
