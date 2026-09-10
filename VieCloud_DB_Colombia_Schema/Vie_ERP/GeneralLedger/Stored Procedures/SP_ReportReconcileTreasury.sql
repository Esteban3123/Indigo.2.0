-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-10-06
-- Description:	Procedimiento almacenado para conciliar tesoreria con contabilidad
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportReconcileTreasury]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON
	set DATEFORMAT DMY

	DECLARE	@DateStart DATE,
			@DateEnd DATE,
			@TypeReport INT,
			@EntityNames VARCHAR(MAX),
			@JournalVoucherTypes VARCHAR(MAX),
			@MainAccounts VARCHAR(MAX),
			-------------
			@FilterByEntityName BIT = 0,
			@FilterByJournalVoucherTypes BIT = 0,
			@FilterByMainAccounts BIT = 0
	
	DECLARE @Table_EntityNames AS TABLE(EntityName VARCHAR(250))
	DECLARE @Table_JournalVoucherTypes AS TABLE(Id INT)
	DECLARE @Table_MainAccounts AS TABLE(Id INT)
	DECLARE @TreasuryAccount AS TABLE(MainAccountId INT)

	BEGIN TRY

		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@DateStart = t.x.value('DateStart[1]','date'),
				@DateEnd = t.x.value('DateEnd[1]','date'),
				@TypeReport = t.x.value('TypeReport[1]','int'),
				@EntityNames = t.x.value('EntityNames[1]','varchar(max)'),
				@JournalVoucherTypes = t.x.value('JournalVoucherTypes[1]','varchar(max)'),
				@MainAccounts = t.x.value('MainAccounts[1]','varchar(max)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		IF ISNULL(@EntityNames, '') <> ''
		BEGIN
			SET @FilterByEntityName = 1

			INSERT INTO @Table_EntityNames
				SELECT Data Data 
				FROM dbo.Split(@EntityNames, ',')
		END

		IF ISNULL(@JournalVoucherTypes, '') <> ''
		BEGIN
			SET @FilterByJournalVoucherTypes = 1

			INSERT INTO @Table_JournalVoucherTypes
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@JournalVoucherTypes, ',')
		END

		IF ISNULL(@MainAccounts, '') <> ''
		BEGIN
			SET @FilterByMainAccounts = 1

			INSERT INTO @Table_MainAccounts
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@MainAccounts, ',')
		END

		INSERT INTO @TreasuryAccount
			SELECT DISTINCT t.IdMainAccount
			FROM 
			(
					SELECT cr.IdMainAccount
					FROM Treasury.CashRegisters cr WITH (NOLOCK)
					GROUP BY cr.IdMainAccount
				UNION ALL
					SELECT eba.IdMainAccount
					FROM Treasury.EntityBankAccounts eba WITH (NOLOCK)
					GROUP BY eba.IdMainAccount
			) t

		/******************************************  OBTENCION DE DATOS ******************************************/

		SELECT	k.DocumentDate,
				jv.DocumentDate AccountingDocumentDate,
				en.Description,
				ISNULL(k.EntityName, jv.EntityName) EntityName,
				ISNULL(k.EntityCode, jv.EntityCode) EntityCode,
				jv.JournalVoucherType,
				jv.Consecutive,
				ISNULL(k.MainAccount, jv.MainAccount) MainAccount,
				--------------------------------------------------------			
				ISNULL(k.DebitValue, 0) DebitValue,--8
				---Se realiza la conversion del valor del comprobante si llega a tener currency diferente al del documento original creado
				ROUND(Common.CurrencyConverterWithDate(ISNULL(jv.DebitValue, 0),jv.OfficialCurrencyId,ISNULL(k.CurrencyId,jv.OfficialCurrencyId),jv.CreationDate),2) AccountingDebitValue, 
				--------------------------------------------------------
				ISNULL(k.CreditValue, 0) CreditValue,--10 
				ROUND(Common.CurrencyConverterWithDate(ISNULL(jv.CreditValue, 0),jv.OfficialCurrencyId,ISNULL(k.CurrencyId,jv.OfficialCurrencyId),jv.CreationDate),2) AccountingCreditValue, 
				isnull(k.CurrencyId,jv.OfficialCurrencyId) CurrencyId,--12
				cy.Abbreviation

				 
		FROM
		(
			SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('InitialBalance' AS VARCHAR(250)) EntityName, ccs.Code EntityCode, ccs.Id EntityId,
						CAST(ccs.DocumentDate AS DATE) DocumentDate,
						SUM(ccs.DebitValue) DebitValue, 
						SUM(ccs.CreditValue) CreditValue,
						ccs.CurrencyId
				FROM 
				(
						SELECT	cr.IdMainAccount, cr.Id, cr.Code, cr.CreationDate DocumentDate,
								cr.InitialBalance DebitValue,
								0 CreditValue,
								cr.CurrencyId
						FROM Treasury.CashRegisters cr WITH (NOLOCK)
						WHERE cr.InitialBalance <> 0
					UNION ALL
						SELECT	eba.IdMainAccount, eba.Id, eba.Code, eba.CreationDate DocumentDate,
								eba.InitialBalance DebitValue,
								0 CreditValue,
								eba.CurrencyId
						FROM Treasury.EntityBankAccounts eba WITH (NOLOCK)
						WHERE eba.InitialBalance <> 0
				) ccs
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ccs.IdMainAccount = ma.Id
				WHERE CAST(ccs.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							ccs.Code, ccs.Id, CAST(ccs.DocumentDate AS DATE),ccs.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('ConstitutionCashSmaller' AS VARCHAR(250)) EntityName, ccs.Code EntityCode, ccs.Id EntityId,
						CAST(ccs.DocumentDate AS DATE) DocumentDate,
						SUM(ccs.DebitValue) DebitValue, 
						SUM(ccs.CreditValue) CreditValue,
						ccs.CurrencyId
				FROM 
				(
						SELECT	cr.IdMainAccount, ccs.Id, ccs.Code, ccs.DocumentDate,
								IIF(ccs.DocumentType = 1, ccs.Value, 0) DebitValue,
								IIF(ccs.DocumentType = 1, 0, ccs.Value) CreditValue,
								cr.CurrencyId
						FROM Treasury.ConstitutionCashSmaller ccs WITH (NOLOCK)
						JOIN Treasury.CashRegisters cr WITH (NOLOCK) ON ccs.CashRegisterSmallerId = cr.Id
						WHERE ccs.Status = 2
					UNION ALL
						SELECT	cr.IdMainAccount, ccs.Id, ccs.Code, ccs.DocumentDate,
								IIF(ccs.DocumentType = 1, 0, ccs.Value) DebitValue,
								IIF(ccs.DocumentType = 1, ccs.Value, 0) CreditValue,
								cr.CurrencyId
						FROM Treasury.ConstitutionCashSmaller ccs WITH (NOLOCK)
						JOIN Treasury.CashRegisters cr WITH (NOLOCK) ON ccs.CashRegisterId = cr.Id
						WHERE ccs.Status = 2
					UNION ALL
						SELECT	eba.IdMainAccount, ccs.Id, ccs.Code, ccs.DocumentDate,
								IIF(ccs.DocumentType = 1, 0, ccs.Value) DebitValue,
								IIF(ccs.DocumentType = 1, ccs.Value, 0) CreditValue,
								eba.CurrencyId
						FROM Treasury.ConstitutionCashSmaller ccs WITH (NOLOCK)
						JOIN Treasury.EntityBankAccounts eba WITH (NOLOCK) ON ccs.EntityBankAccountId = eba.Id
						WHERE ccs.Status = 2
				) ccs
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ccs.IdMainAccount = ma.Id
				WHERE CAST(ccs.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							ccs.Code, ccs.Id, CAST(ccs.DocumentDate AS DATE),ccs.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('CashReceipts' AS VARCHAR(250)) EntityName, ccs.Code EntityCode, ccs.Id EntityId,
						CAST(ccs.DocumentDate AS DATE) DocumentDate,
						SUM(ccs.DebitValue) DebitValue, 
						SUM(ccs.CreditValue) CreditValue,
						ccs.CurrencyId
				FROM 
				(
						SELECT	cr.IdMainAccount, crc.Id, crc.Code, crc.DocumentDate,
								pm.Value DebitValue,
								0 CreditValue,
								cr.CurrencyId
						FROM Treasury.CashReceipts crc WITH (NOLOCK)
						JOIN Treasury.CashRegisters cr WITH (NOLOCK) ON crc.IdCashRegister = cr.Id
						JOIN Treasury.PaymentMethods pm WITH (NOLOCK) ON crc.Id = pm.IdCashReceipt
						WHERE crc.Status IN (2, 4)
					UNION ALL
						SELECT	eba.IdMainAccount, crc.Id, crc.Code, crc.DocumentDate,
								pm.Value DebitValue,
								0 CreditValue,
								eba.CurrencyId
						FROM Treasury.CashReceipts crc WITH (NOLOCK)
						JOIN Treasury.EntityBankAccounts eba WITH (NOLOCK) ON crc.IdBankAccount = eba.Id
						JOIN Treasury.PaymentMethods pm WITH (NOLOCK) ON crc.Id = pm.IdCashReceipt
						WHERE crc.Status IN (2, 4)
				) ccs
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ccs.IdMainAccount = ma.Id
				WHERE CAST(ccs.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							ccs.Code, ccs.Id, CAST(ccs.DocumentDate AS DATE), ccs.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('VoucherTransaction' AS VARCHAR(250)) EntityName, ccs.Code EntityCode, ccs.Id EntityId,
						CAST(ccs.DocumentDate AS DATE) DocumentDate,
						SUM(ccs.DebitValue) DebitValue, 
						SUM(ccs.CreditValue) CreditValue,
						ccs.CurrencyId
				FROM 
				(
						SELECT	cr.IdMainAccount, vt.Id, vt.Code, vt.DocumentDate,
								0 DebitValue,
								vt.Value CreditValue,
								vt.CurrencyId
						FROM Treasury.VoucherTransaction vt WITH (NOLOCK)
						JOIN Treasury.CashRegisters cr WITH (NOLOCK) ON vt.IdCashRegister = cr.Id
						WHERE vt.Status IN (2, 4)
					UNION ALL
						SELECT	vtd.IdMainAccount, vt.Id, vt.Code, vt.DocumentDate,
								IIF(vtd.Nature = 1, vtd.Value, 0) DebitValue,
								IIF(vtd.Nature = 1, 0, vtd.Value) CreditValue,
								vt.CurrencyId
						FROM Treasury.VoucherTransaction vt WITH (NOLOCK)
						JOIN Treasury.VoucherTransactionDetails vtd WITH (NOLOCK) ON vt.Id = vtd.IdVoucherTransaction
						WHERE vt.Status IN (2, 4)
					UNION ALL
						SELECT	eba.IdMainAccount, vt.Id, vt.Code, vt.DocumentDate,
								0 DebitValue,
								vt.Value CreditValue,
								vt.CurrencyId
						FROM Treasury.VoucherTransaction vt WITH (NOLOCK)
						JOIN Treasury.EntityBankAccounts eba WITH (NOLOCK) ON vt.IdEntityBankAccount = eba.Id
						WHERE vt.Status IN (2, 4)
					UNION ALL
						SELECT	eba.IdMainAccount, vt.Id, vt.Code, vt.DocumentDate,
								IIF(vtd.Nature = 1, vtd.Value, 0) DebitValue,
								IIF(vtd.Nature = 1, 0, vtd.Value) CreditValue,
								vt.CurrencyId
						FROM Treasury.VoucherTransaction vt WITH (NOLOCK)
						JOIN Treasury.VoucherTransactionDetails vtd WITH (NOLOCK) ON vt.Id = vtd.IdVoucherTransaction
						JOIN Treasury.EntityBankAccounts eba WITH (NOLOCK) ON vtd.IdEntityBankAccount = eba.Id
						WHERE vt.Status IN (2, 4)
				) ccs
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ccs.IdMainAccount = ma.Id
				WHERE CAST(ccs.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							ccs.Code, ccs.Id, CAST(ccs.DocumentDate AS DATE), ccs.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('Consignment' AS VARCHAR(250)) EntityName, ccs.Code EntityCode, ccs.Id EntityId,
						CAST(ccs.DocumentDate AS DATE) DocumentDate,
						SUM(ccs.DebitValue) DebitValue, 
						SUM(ccs.CreditValue) CreditValue,
						ccs.CurrencyId
				FROM 
				(
						SELECT	cr.IdMainAccount, c.Id, c.Code, c.DocumentDate,
								0 DebitValue,
								cd.Value CreditValue,
								cr.CurrencyId
						FROM Treasury.Consignment c WITH (NOLOCK)
						JOIN Treasury.ConsignmentDetail cd WITH (NOLOCK) ON c.Id = cd.ConsignmentTransferId
						JOIN Treasury.CashRegisters cr WITH (NOLOCK) ON cd.CashRegisterId = cr.Id
						WHERE c.Status IN (2, 4)
					UNION ALL
						SELECT	eba.IdMainAccount, c.Id, c.Code, c.DocumentDate,
								c.Value DebitValue,
								0 CreditValue,
								eba.CurrencyId
						FROM Treasury.Consignment c WITH (NOLOCK)
						JOIN Treasury.EntityBankAccounts eba WITH (NOLOCK) ON c.EntityBankAccountId = eba.Id
						WHERE c.Status IN (2, 4)
				) ccs
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ccs.IdMainAccount = ma.Id
				WHERE CAST(ccs.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							ccs.Code, ccs.Id, CAST(ccs.DocumentDate AS DATE), ccs.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('TreasuryNote' AS VARCHAR(250)) EntityName, ccs.Code EntityCode, ccs.Id EntityId,
						CAST(ccs.DocumentDate AS DATE) DocumentDate,
						SUM(ccs.DebitValue) DebitValue, 
						SUM(ccs.CreditValue) CreditValue,
						ccs.CurrencyId
				FROM 
				(
						SELECT	cr.IdMainAccount, tn.Id, tn.Code, tn.NoteDate DocumentDate,
								IIF(tn.Nature = 1, tn.Value, 0) DebitValue,
								IIF(tn.Nature = 1, 0, tn.Value) CreditValue,
								tn.CurrencyId
						FROM Treasury.TreasuryNote tn WITH (NOLOCK)
						JOIN Treasury.CashRegisters cr WITH (NOLOCK) ON tn.CashRegisterId = cr.Id
						WHERE tn.Status = 2
					UNION ALL
						SELECT	eba.IdMainAccount, tn.Id, tn.Code, tn.NoteDate DocumentDate,
								IIF(tn.Nature = 1, tn.Value, 0) DebitValue,
								IIF(tn.Nature = 1, 0, tn.Value) CreditValue,
								tn.CurrencyId
						FROM Treasury.TreasuryNote tn WITH (NOLOCK)
						JOIN Treasury.EntityBankAccounts eba WITH (NOLOCK) ON tn.EntityBankAccountId = eba.Id
						WHERE tn.Status = 2
				) ccs
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ccs.IdMainAccount = ma.Id
				WHERE CAST(ccs.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							ccs.Code, ccs.Id, CAST(ccs.DocumentDate AS DATE),ccs.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('TreasuryNote' AS VARCHAR(250)) EntityName, tn.Code EntityCode, tn.Id EntityId,
						CAST(tn.NoteDate AS DATE) DocumentDate,
						SUM(ccs.CreditValue) DebitValue, 
						SUM(ccs.DebitValue) CreditValue,
						tn.CurrencyId
				FROM Treasury.TreasuryNote tn WITH (NOLOCK)
				JOIN
				(
						SELECT	cr.IdMainAccount, crc.Id, crc.Code, crc.DocumentDate,
								pm.Value DebitValue,
								0 CreditValue
						FROM Treasury.CashReceipts crc WITH (NOLOCK)
						JOIN Treasury.CashRegisters cr WITH (NOLOCK) ON crc.IdCashRegister = cr.Id
						JOIN Treasury.PaymentMethods pm WITH (NOLOCK) ON crc.Id = pm.IdCashReceipt
						WHERE crc.Status IN (2, 4)
					UNION ALL
						SELECT	eba.IdMainAccount, crc.Id, crc.Code, crc.DocumentDate,
								pm.Value DebitValue,
								0 CreditValue
						FROM Treasury.CashReceipts crc WITH (NOLOCK)
						JOIN Treasury.EntityBankAccounts eba WITH (NOLOCK) ON crc.IdBankAccount = eba.Id
						JOIN Treasury.PaymentMethods pm WITH (NOLOCK) ON crc.Id = pm.IdCashReceipt
						WHERE crc.Status IN (2, 4)
				) ccs ON tn.CashReceiptId = ccs.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ccs.IdMainAccount = ma.Id
				WHERE tn.Status = 2
					AND CAST(tn.NoteDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							tn.Code, tn.Id, CAST(tn.NoteDate AS DATE), tn.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('TreasuryNote' AS VARCHAR(250)) EntityName, tn.Code EntityCode, tn.Id EntityId,
						CAST(tn.NoteDate AS DATE) DocumentDate,
						SUM(ccs.CreditValue) DebitValue, 
						SUM(ccs.DebitValue) CreditValue,
						tn.CurrencyId
				FROM Treasury.TreasuryNote tn WITH (NOLOCK)
				JOIN
				(
						SELECT	cr.IdMainAccount, vt.Id, vt.Code, vt.DocumentDate,
								0 DebitValue,
								vt.Value CreditValue
						FROM Treasury.VoucherTransaction vt WITH (NOLOCK)
						JOIN Treasury.CashRegisters cr WITH (NOLOCK) ON vt.IdCashRegister = cr.Id
						WHERE vt.Status IN (2, 4)
					UNION ALL
						SELECT	cr.IdMainAccount, vt.Id, vt.Code, vt.DocumentDate,
								IIF(vtd.Nature = 1, vtd.Value, 0) DebitValue,
								IIF(vtd.Nature = 1, 0, vtd.Value) CreditValue
						FROM Treasury.VoucherTransaction vt WITH (NOLOCK)
						JOIN Treasury.VoucherTransactionDetails vtd WITH (NOLOCK) ON vt.Id = vtd.IdVoucherTransaction
						JOIN Treasury.CashRegisters cr WITH (NOLOCK) ON vtd.CashRegisterId = cr.Id
						WHERE vt.Status IN (2, 4)
					UNION ALL
						SELECT	eba.IdMainAccount, vt.Id, vt.Code, vt.DocumentDate,
								0 DebitValue,
								vt.Value CreditValue
						FROM Treasury.VoucherTransaction vt WITH (NOLOCK)
						JOIN Treasury.EntityBankAccounts eba WITH (NOLOCK) ON vt.IdEntityBankAccount = eba.Id
						WHERE vt.Status IN (2, 4)
					UNION ALL
						SELECT	eba.IdMainAccount, vt.Id, vt.Code, vt.DocumentDate,
								IIF(vtd.Nature = 1, vtd.Value, 0) DebitValue,
								IIF(vtd.Nature = 1, 0, vtd.Value) CreditValue
						FROM Treasury.VoucherTransaction vt WITH (NOLOCK)
						JOIN Treasury.VoucherTransactionDetails vtd WITH (NOLOCK) ON vt.Id = vtd.IdVoucherTransaction
						JOIN Treasury.EntityBankAccounts eba WITH (NOLOCK) ON vtd.IdEntityBankAccount = eba.Id
						WHERE vt.Status IN (2, 4)
				) ccs ON tn.VoucherTransactionId = ccs.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ccs.IdMainAccount = ma.Id
				WHERE tn.Status = 2
					AND CAST(tn.NoteDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							tn.Code, tn.Id, CAST(tn.NoteDate AS DATE),tn.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('TreasuryNote' AS VARCHAR(250)) EntityName, tn.Code EntityCode, tn.Id EntityId,
						CAST(tn.NoteDate AS DATE) DocumentDate,
						SUM(ccs.CreditValue) DebitValue, 
						SUM(ccs.DebitValue) CreditValue,
						tn.CurrencyId
				FROM Treasury.TreasuryNote tn WITH (NOLOCK)
				JOIN
				(
						SELECT	cr.IdMainAccount, c.Id, c.Code, c.DocumentDate,
								0 DebitValue,
								cd.Value CreditValue
						FROM Treasury.Consignment c WITH (NOLOCK)
						JOIN Treasury.ConsignmentDetail cd WITH (NOLOCK) ON c.Id = cd.ConsignmentTransferId
						JOIN Treasury.CashRegisters cr WITH (NOLOCK) ON cd.CashRegisterId = cr.Id
						WHERE c.Status IN (2, 4)
					UNION ALL
						SELECT	eba.IdMainAccount, c.Id, c.Code, c.DocumentDate,
								c.Value DebitValue,
								0 CreditValue
						FROM Treasury.Consignment c WITH (NOLOCK)
						JOIN Treasury.EntityBankAccounts eba WITH (NOLOCK) ON c.EntityBankAccountId = eba.Id
						WHERE c.Status IN (2, 4)
				) ccs ON tn.ConsignmentId = ccs.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ccs.IdMainAccount = ma.Id
				WHERE tn.Status = 2
					AND CAST(tn.NoteDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							tn.Code, tn.Id, CAST(tn.NoteDate AS DATE), tn.CurrencyId

		) k
		FULL JOIN
		(
			SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
					jv.EntityName, jv.EntityCode, jv.EntityId,
					CAST(jv.VoucherDate AS DATE) DocumentDate,
					jv.Consecutive, 
					jvt.Id JournalVoucherTypeId,
					CONCAT(jvt.Code, ' - ', jvt.Name) JournalVoucherType,
					SUM(jvd.DebitValue) DebitValue,
					SUM(jvd.CreditValue) CreditValue,
					lb.OfficialCurrencyId,
					jv.CreationDate
			FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
			JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			join GeneralLedger.LegalBook lb WITH (NOLOCK) on jv.LegalBookId = lb.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id 
			JOIN @TreasuryAccount apa ON ma.Id = apa.MainAccountId
			WHERE jv.Status = 2
				AND CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
						jv.EntityName, jv.EntityCode, jv.EntityId, 
						CAST(jv.VoucherDate AS DATE), 
						jv.Consecutive, jvt.Id, CONCAT(jvt.Code, ' - ', jvt.Name),lb.OfficialCurrencyId,jv.CreationDate
		) jv ON ISNULL(k.EntityName, '') = ISNULL(jv.EntityName, '') 
			AND ISNULL(k.EntityId, 0) = ISNULL(jv.EntityId, 0) 
			AND k.MainAccountId = jv.MainAccountId
		LEFT JOIN Common.GetEntityNameDescriptions() en ON ISNULL(k.EntityName, jv.EntityName) = en.EntityName
		LEFT JOIN @Table_EntityNames ten ON ISNULL(k.EntityName, jv.EntityName) = ten.EntityName
		LEFT JOIN @Table_JournalVoucherTypes tjvt ON jv.JournalVoucherTypeId = tjvt.Id
		LEFT JOIN @Table_MainAccounts tma ON ISNULL(k.MainAccountId, jv.MainAccountId) = tma.Id
		JOIN Common.Currency cy WITH(NOLOCK) on cy.Id = ISNULL(k.CurrencyId,jv.OfficialCurrencyId)
		WHERE (@FilterByEntityName = 0 OR ten.EntityName IS NOT NULL)
			AND (@FilterByJournalVoucherTypes = 0 OR tjvt.Id IS NOT NULL)
			AND (@FilterByMainAccounts = 0 OR tma.Id IS NOT NULL)
			AND 
			(
				@TypeReport = 3
				OR
				(
					@TypeReport = 2
					AND
					(
						ROUND(ISNULL(k.DebitValue, 0), 2) = ROUND(ISNULL(jv.DebitValue, 0), 2)
						OR 
						ROUND(ISNULL(k.CreditValue, 0), 2) = ROUND(ISNULL(jv.CreditValue, 0), 2)
					)
				)
				OR
				(
					@TypeReport = 1
					AND
					(
						ROUND(ISNULL(k.DebitValue, 0), 2) <> ROUND(ISNULL(jv.DebitValue, 0), 2)
						OR 
						ROUND(ISNULL(k.CreditValue, 0), 2) <> ROUND(ISNULL(jv.CreditValue, 0), 2)
					)
				)
			)
		ORDER BY	ISNULL(k.DocumentDate, jv.DocumentDate), 
					ISNULL(k.EntityName, jv.EntityName), 
					ISNULL(k.EntityCode, jv.EntityCode),
					ISNULL(k.MainAccount, jv.MainAccount),
					ISNULL(k.CurrencyId, jv.OfficialCurrencyId)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de conciliación entre tesorería y contabilidad para un rango de fechas determinado. Consolida los movimientos de cajas menores (CashRegisters) y cuentas bancarias de la entidad (EntityBankAccounts) con sus respectivas cuentas contables (MainAccounts), incluyendo saldos iniciales, constitución de cajas menores, pagos, recaudos y demás transacciones de tesorería, para comparar los valores registrados en tesorería contra los comprobantes contables generados. Permite filtrar por nombres de terceros, tipos de comprobante contable y cuentas contables específicas, y realiza conversión de moneda cuando los documentos fueron registrados en divisas distintas a la moneda oficial. Se utiliza para identificar diferencias o partidas en tránsito entre el módulo de tesorería y el libro mayor contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportReconcileTreasury';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportReconcileTreasury';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de conciliación entre los movimientos del módulo de tesorería (cajas, bancos, recibos, comprobantes, consignaciones, notas) y los asientos contables del libro mayor, mostrando coincidencias, diferencias o ambos según el tipo de reporte.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileTreasury';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@xmlCriterias debe contener nodo /Data con DateStart, DateEnd y TypeReport válidos; TypeReport debe ser 1 (diferencias), 2 (coincidencias) o 3 (todos); Las listas EntityNames, JournalVoucherTypes y MainAccounts, si vienen, deben ser cadenas separadas por coma; los IDs deben ser convertibles a INT; Debe existir al menos una caja o cuenta bancaria con IdMainAccount asociado para que se incluya en la conciliación; Las cuentas contables referenciadas deben existir en GeneralLedger.MainAccounts y la moneda en Common.Currency', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileTreasury';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen movimientos de tesorería con Status = 2 (constituciones, notas de tesorería) o Status IN (2,4) (recibos, comprobantes, consignaciones); Solo se consideran asientos contables con Status = 2 (contabilizados); Las cuentas contables consideradas son únicamente las asociadas a cajas registradoras o cuentas bancarias de la entidad (cuentas de tesorería); Las fechas de los documentos deben estar dentro del rango DateStart-DateEnd; Los valores contables se convierten a la moneda del documento original mediante CurrencyConverterWithDate cuando difieren; Las comparaciones de conciliación se hacen redondeando a 2 decimales; Los saldos iniciales solo se incluyen cuando son distintos de 0; Ante cualquier excepción se devuelve un único resultset con CodeResult=''999'' y el mensaje y línea del error', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileTreasury';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Conciliación tesorería vs contabilidad; Caja registradora / caja menor; Cuenta bancaria de la entidad; Constitución de caja menor; Recibo de caja; Método de pago; Comprobante de egreso (VoucherTransaction); Consignación bancaria; Nota de tesorería; Comprobante contable / asiento (JournalVoucher); Plan de cuentas / cuenta principal (PUC); Libro legal y moneda oficial; Conversión de moneda con fecha; Naturaleza débito/crédito; Saldo inicial', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileTreasury';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve filas con fecha del documento, fecha contable, entidad, tipo y consecutivo de comprobante, cuenta principal y valores débito/crédito de tesorería vs contabilidad, filtradas por TypeReport (1=diferencias, 2=coincidencias, 3=todos) y por los filtros opcionales activos; [RETURN_RESULT] RESULTSET: En el bloque CATCH devuelve un resultset con CodeResult=''999'' y MessageResult con ERROR_MESSAGE() + línea del error', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileTreasury';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EntityNames recibidos no vacíos → Activa filtro por nombre de entidad y carga la lista en tabla temporal vía Split; si JournalVoucherTypes recibidos no vacíos → Activa filtro por tipos de comprobante contable y carga IDs; si MainAccounts recibidos no vacíos → Activa filtro por cuentas contables y carga IDs; si TypeReport = 1 → Devuelve solo registros con diferencias entre tesorería y contabilidad (débito o crédito redondeados a 2 decimales NO coinciden); si TypeReport = 2 → Devuelve solo registros conciliados (débito o crédito redondeados a 2 decimales coinciden); si TypeReport = 3 → Devuelve todos los registros sin filtrar por coincidencia; si ConstitutionCashSmaller con DocumentType = 1 → Para CashRegisterSmaller suma como débito y para CashRegister origen suma como crédito; en caso contrario invierte; si TreasuryNote/VoucherTransactionDetails con Nature = 1 → El valor se considera débito; en caso contrario crédito', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileTreasury';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split; Common.CurrencyConverterWithDate; Common.GetEntityNameDescriptions', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileTreasury';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.CashRegisters; Treasury.EntityBankAccounts; Treasury.ConstitutionCashSmaller; Treasury.CashReceipts; Treasury.PaymentMethods; Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; Treasury.Consignment; Treasury.ConsignmentDetail; Treasury.TreasuryNote; GeneralLedger.MainAccounts; GeneralLedger.JournalVoucherTypes; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; GeneralLedger.LegalBook; Common.Currency; Common.GetEntityNameDescriptions', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileTreasury';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileTreasury';
-- GO
