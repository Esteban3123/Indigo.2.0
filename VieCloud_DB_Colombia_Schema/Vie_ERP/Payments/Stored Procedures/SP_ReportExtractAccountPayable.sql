-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-01-30
-- Description:	Procedimiento para el reporte de extracto de cuentas por pagar
-- =============================================
CREATE PROCEDURE [Payments].[SP_ReportExtractAccountPayable]
	@InitialDate AS DATE,
	@EndDate AS DATE,
	@InitialNit AS varchar(20),
	@EndNit AS varchar(20),
	@InitialBillNumber AS varchar(100),
	@EndBillNumber AS varchar(100),
	@Type AS TINYINT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		DECLARE @OffialCurrencyId AS INT
		SET @OffialCurrencyId = (SELECT TOP 1 cs.OfficialCurrencyId FROM GeneralLedger.CompanySettings cs)
		SELECT
			tp.Nit AS ThirdPartyNit, 
			tp.Name AS ThirdPartyName, 
			dl.Name AS DistributionLineName, 
			ma.Number AS AccountNumber, 
			ma.Name AS AccountName, 
			ap.BillNumber AS BillNumber, 
			CASE 
				WHEN ap.EntityName = 'AccountPayable' THEN 'Cuenta Por Pagar' 
				WHEN ap.EntityName = 'InitialBalance' THEN 'Saldo Inicial' 
			END AS EntityName, 
			IIF(@Type = 3, ap.Value, Common.CurrencyConverter(ap.Value, ISNULL(ap.CurrencyId, @OffialCurrencyId), @OffialCurrencyId)) AS BillValueInitial, 
			IIF(@Type = 3, ap.Balance, Common.CurrencyConverter(ap.Balance, ISNULL(ap.CurrencyId, @OffialCurrencyId), @OffialCurrencyId)) AS BillCurrentBalance, 
			data.MovesCode,
			data.MovesDate,
			IIF(@Type = 3, data.MovesDebit, Common.CurrencyConverter(data.MovesDebit, ISNULL(ap.CurrencyId, @OffialCurrencyId), @OffialCurrencyId)) MovesDebit,
			IIF(@Type = 3, data.MovesCredit, Common.CurrencyConverter(data.MovesCredit, ISNULL(ap.CurrencyId, @OffialCurrencyId), @OffialCurrencyId)) MovesCredit,
			data.VoucherName,
			ap.IdSupplier AS IdSupplier,
			ap.Id AS IdBills,
			data.Status,
			ap.Code AS CodeAccountPayable,
			ISNULL(data.Abbreviation, c.Abbreviation) CurrencyAbbreviation
		FROM Payments.AccountPayable ap WITH (NOLOCK)
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON tp.Id = ap.IdThirdParty 
		JOIN Common.SuppliersDistributionLines sdl WITH (NOLOCK) ON sdl.Id = ap.IdSuppliersDistributionLines 
		JOIN Common.DistributionLines dl WITH (NOLOCK) ON dl.Id = sdl.IdDistributionLine 
		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.Id = dl.IdMainAccount
		JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ISNULL(ap.CurrencyId, @OffialCurrencyId)
		JOIN 
		(
			SELECT 
				ap.Id AS AccountPayableId,
				ap.Code AS MovesCode, 
				ap.DocumentDate AS MovesDate, 
				0 AS MovesDebit, 
				CASE ap.InitialBalance 
					WHEN 1 THEN ISNULL(iniap.Balance, ap.Value) 
					ELSE ap.Value 
				END AS MovesCredit, 
				'Cuenta por pagar' AS VoucherName, 
				ap.Status AS Status,
				C.Abbreviation
			FROM [Payments].[AccountPayable] AP WITH (NOLOCK) 	
			JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ISNULL(ap.CurrencyId, @OffialCurrencyId)
			LEFT JOIN Payments.InitialBalanceAccountPayable iniap WITH (NOLOCK) on iniap.AccountPayableId = ap.Id
			WHERE ap.Status = 2
			-------------------------------------------------------------------------------------------------------
			UNION ALL
			-------------------------------------------------------------------------------------------------------
			SELECT 
				papa.AccountPayableId,
				pn.Code AS MovesCode, 
				pn.NoteDate AS MovesDate, 
				SUM(CASE 
					WHEN pn.Nature = 1 THEN ISNULL(papa.AdjustmentValueShare, papa.AdjusmentValue)
					ELSE 0 
				END) AS MovesDebit, 
				SUM(CASE 
					WHEN pn.Nature = 2 THEN ISNULL(papa.AdjustmentValueShare, papa.AdjusmentValue)
					ELSE 0 
				END) AS MovesCredit, 
				'Nota de CxP' AS VoucherName, 		
				pn.Status AS Status,
				C.Abbreviation
			FROM [Payments].[PaymentNotesAccountPayableAdvance] papa  WITH (NOLOCK) 
			JOIN Payments.PaymentNotes pn WITH (NOLOCK) ON pn.Id = papa.PaymentNoteId 
			JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ISNULL(pn.CurrencyId, @OffialCurrencyId)
			WHERE pn.Status = 2
			GROUP BY papa.AccountPayableId, pn.Code, pn.NoteDate, pn.Status, c.Abbreviation
			-------------------------------------------------------------------------------------------------------
			UNION ALL
			-------------------------------------------------------------------------------------------------------
			SELECT 
				ap.Id AccountPayableId,
				pn.Code AS MovesCode, 
				pn.NoteDate AS MovesDate, 
				ap.Value AS MovesDebit, 
				0 AS MovesCredit, 
				'Nota de CxP' AS VoucherName, 		
				pn.Status AS Status,
				C.Abbreviation
			FROM [Payments].[AccountPayable] ap WITH (NOLOCK) 
			JOIN Payments.PaymentNotes pn WITH (NOLOCK) ON pn.IdAccountPayable = ap.Id 
			JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ISNULL(ap.CurrencyId, @OffialCurrencyId)
			WHERE pn.Status = 2
			-------------------------------------------------------------------------------------------------------
			UNION ALL
			-------------------------------------------------------------------------------------------------------
			SELECT 
				ptd.AccountPayableId,
				pt.Code AS MovesCode, 
				pt.DocumentDate AS MovesDate, 
				IIF(ptd.ValueInCurrencyInvoice = 0, ptd.Value, ptd.ValueInCurrencyInvoice) AS MovesDebit, 
				0 AS MovesCredit, 
				'Cruce Anticipo vs CxP' AS VoucherName, 
				pt.Status AS Status,
				NULL
			FROM [Payments].[PaymentTransferDetail] ptd WITH (NOLOCK) 
			JOIN Payments.PaymentTransfer PT WITH (NOLOCK) ON pt.Id = ptd.PaymentTransferId 
			WHERE pt.Status = 2
			-------------------------------------------------------------------------------------------------------
			UNION ALL
			-------------------------------------------------------------------------------------------------------
			SELECT 
				cadcxp.AccountPayableId,
				ca.Code AS MovesCode, 
				ca.DocumentDate AS MovesDate, 
				cadcxp.CrossingValue AS MovesDebit, 
				0 AS MovesCredit, 
				'Cruce de Cuenta' AS VoucherName, 
				ca.Status AS Status,
				NULL
			FROM [Treasury].[CrossingAccountDetailCxP] cadcxp WITH (NOLOCK) 
			JOIN Treasury.CrossingAccount ca WITH (NOLOCK) ON ca.Id = cadcxp.CrossingAccountId
			WHERE ca.Status = 2
			-------------------------------------------------------------------------------------------------------
			UNION ALL
			-------------------------------------------------------------------------------------------------------
			SELECT 
				db.IdAccountPayable AS AccountPayableId,
				vt.Code AS MovesCode, 
				vt.DocumentDate AS MovesDate, 
				IIF(db.ValueInCurrencyHeader = 0, db.AdvancedValue, db.ValueInCurrencyHeader) AS MovesDebit, 
				0 AS MovesCredit, 
				'Comprobante de Egreso' AS VoucherName, 
				vt.Status AS Status,
				C.Abbreviation
			FROM [Treasury].[DischargeBill] db WITH (NOLOCK) 
			JOIN Treasury.VoucherTransactionDetails vtd WITH (NOLOCK) ON vtd.Id = db.IdVoucherTransactionD 
			JOIN Treasury.VoucherTransaction vt WITH (NOLOCK) ON vt.Id = vtd.IdVoucherTransaction 
			JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ISNULL(VT.CurrencyId, @OffialCurrencyId)
			WHERE vt.Status IN (2, 4)
			-------------------------------------------------------------------------------------------------------
			UNION ALL
			-------------------------------------------------------------------------------------------------------	
			SELECT
				db.IdAccountPayable,
				tn.Code AS MovesCode, 
				tn.NoteDate AS MovesDate, 
				0 AS MovesDebit, 
				IIF(db.ValueInCurrencyHeader = 0, db.AdvancedValue, db.ValueInCurrencyHeader) AS MovesCredit, 
				'Nota de Tesoreria' AS VoucherName, 		
				tn.Status AS Status,
				C.Abbreviation
			FROM [Treasury].[TreasuryNote] tn WITH (NOLOCK) 
			JOIN Treasury.VoucherTransaction vt WITH (NOLOCK) ON vt.Id = tn.VoucherTransactionId 
			JOIN Treasury.VoucherTransactionDetails vtd WITH (NOLOCK) ON vt.Id = vtd.IdVoucherTransaction 
			JOIN Treasury.DischargeBill db WITH (NOLOCK) ON vtd.Id = DB.IdVoucherTransactionD 
			JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ISNULL(TN.CurrencyId, @OffialCurrencyId)
			WHERE tn.Status = 2 AND vt.Status IN (2, 4)
			-------------------------------------------------------------------------------------------------------
			UNION ALL
			-------------------------------------------------------------------------------------------------------
			SELECT
				crdap.AccountPayableId,
				CR.Code AS MovesCode, 
				CR.DocumentDate AS MovesDate, 
				0 AS MovesDebit, 
				IIF(crd.ValueInCurrencyHeader = 0, crdap.RefundValue, crd.ValueInCurrencyHeader) AS MovesCredit, 
				'R. Caja Reintegro CxP' AS VoucherName, 
				cr.Status AS Status,
				C.Abbreviation
			FROM Treasury.CashReceipts cr WITH (NOLOCK) 
			JOIN Treasury.CashReceiptDetails AS crd WITH (NOLOCK) ON cr.id = crd.IdCashReceipt 
			JOIN Treasury.CashReceiptDetailAccountPayable as crdap WITH (NOLOCK) ON crd.ID = crdap.CashReceiptDetailId 	
			JOIN Common.Currency c WITH (NOLOCK) ON c.Id = ISNULL(CRD.CurrencyId, @OffialCurrencyId)
			WHERE cr.Status = 2
		) AS data ON ap.Id = data.AccountPayableId
		WHERE 
			(
				@InitialDate IS NULL
				OR
				CAST(data.MovesDate AS DATE) BETWEEN @InitialDate AND @EndDate
			)
			AND
			(
				@InitialNit IS NULL
				OR
				tp.Nit BETWEEN @InitialNit AND @EndNit
			)
			AND
			(
				@InitialBillNumber IS NULL
				OR
				ap.BillNumber BETWEEN @InitialBillNumber AND @EndBillNumber
			)
		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de extracto de cuentas por pagar a proveedores y terceros para un rango de fechas, NITs y números de factura indicados. Consolida en un solo resultado todos los movimientos que afectan cada cuenta por pagar: el registro inicial de la factura, notas de cuentas por pagar, cruces de anticipos contra CxP, cruces de cuenta y pagos realizados, mostrando para cada movimiento su débito y crédito correspondiente. Integra información del tercero (NIT y nombre del proveedor), la línea de distribución contable y la cuenta contable principal asociada, y convierte los valores a la moneda oficial de la compañía según el tipo de reporte solicitado (admite moneda original o moneda convertida). Se usa para auditoría, conciliación y seguimiento del saldo vigente de obligaciones con proveedores.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_ReportExtractAccountPayable';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_ReportExtractAccountPayable';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el extracto de cuentas por pagar consolidando movimientos (CxP, notas, cruces, comprobantes de egreso, notas de tesorería y reintegros) por tercero, con conversión a moneda oficial cuando aplica.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExtractAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en GeneralLedger.CompanySettings con OfficialCurrencyId definido.; Las cuentas por pagar deben estar enlazadas a un tercero, una línea de distribución de proveedor, una línea de distribución y una cuenta contable principal.; Los filtros de fecha, NIT y número de factura solo se aplican cuando su valor inicial no es NULL.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExtractAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan documentos en estado contabilizado (Status = 2), salvo comprobantes de egreso que también admiten Status = 4.; Cuando una entidad no tiene moneda asignada (CurrencyId NULL) se asume la moneda oficial de la empresa.; Los movimientos siempre se asocian a una cuenta por pagar mediante AccountPayableId.; Los errores no se propagan: se devuelven como un resultset con código ''999''.; La abreviatura de moneda mostrada proviene del movimiento; si no existe, se usa la de la cuenta por pagar.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExtractAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por pagar; Saldo inicial; Tercero/Proveedor; Línea de distribución; Cuenta contable principal; Nota de cuenta por pagar (débito/crédito); Cruce anticipo vs CxP; Cruce de cuenta; Comprobante de egreso; Nota de tesorería; Recibo de caja por reintegro; Conversión de moneda; Moneda oficial de la empresa; NIT; Número de factura', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExtractAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve filas del extracto con datos del tercero, cuenta contable, factura y movimientos asociados aplicando conversión de moneda salvo cuando @Type = 3.; [RETURN_RESULT] Resultset: Cuando ocurre una excepción en el TRY, retorna una fila con Code=''999'', Message=ERROR_MESSAGE() y Line=ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExtractAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Type = 3 → Retorna los valores (Value, Balance, MovesDebit, MovesCredit) en su moneda original sin convertir. else Convierte los valores monetarios a la moneda oficial de la empresa usando Common.CurrencyConverter.; si ap.EntityName = ''AccountPayable'' → Etiqueta el movimiento como ''Cuenta Por Pagar''. else Si EntityName = ''InitialBalance'' lo etiqueta como ''Saldo Inicial''.; si En la rama de AccountPayable, ap.InitialBalance = 1 → Toma como MovesCredit el saldo desde InitialBalanceAccountPayable (o ap.Value si es NULL). else Toma ap.Value como MovesCredit.; si En notas de pago aplicadas, pn.Nature = 1 → El valor ajustado se suma como MovesDebit. else Si pn.Nature = 2, el valor ajustado se suma como MovesCredit.; si En cruces/egresos/notas de tesorería/reintegros, ValueInCurrencyHeader = 0 (o ValueInCurrencyInvoice = 0) → Usa el valor base (AdvancedValue, RefundValue o Value) como monto del movimiento. else Usa el valor expresado en la moneda del encabezado/factura.; si Documento es comprobante de egreso (VoucherTransaction) → Solo se incluye cuando vt.Status IN (2,4). else Para los demás documentos (CxP, notas, cruces, reintegros) solo se incluyen con Status = 2.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExtractAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.CurrencyConverter', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExtractAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Payments.AccountPayable; Common.ThirdParty; Common.SuppliersDistributionLines; Common.DistributionLines; GeneralLedger.MainAccounts; Common.Currency; Payments.InitialBalanceAccountPayable; Payments.PaymentNotesAccountPayableAdvance; Payments.PaymentNotes; Payments.PaymentTransferDetail; Payments.PaymentTransfer; Treasury.CrossingAccountDetailCxP; Treasury.CrossingAccount; Treasury.DischargeBill; Treasury.VoucherTransactionDetails; Treasury.VoucherTransaction; Treasury.TreasuryNote; Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.CashReceiptDetailAccountPayable', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExtractAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExtractAccountPayable';
-- GO
