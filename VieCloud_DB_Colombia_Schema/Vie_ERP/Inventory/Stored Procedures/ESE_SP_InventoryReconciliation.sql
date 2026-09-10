CREATE PROCEDURE [Inventory].[ESE_SP_InventoryReconciliation]
	@DateStart DATE = '2020-01-01',
	@DateEnd DATE = '2020-12-31'
AS
BEGIN
	DECLARE @LegalBookId INT,
			@MainAccountGroup VARCHAR(2) = '14'

	SELECT @LegalBookId = Id
	FROM GeneralLedger.LegalBook
	WHERE OfficialBook = 1

	SELECT CASE ISNULL(k.EntityName, jv.EntityName)
				WHEN 'InventoryControl' THEN 'Control de Inventario'
				WHEN 'RemissionEntrance' THEN 'Remisión de Entrada'
				WHEN 'RemissionEntranceDevolution' THEN 'Devolución de Remisión de Entrada'
				WHEN 'ConsignmentInventoryRemission' THEN 'Devolución de Remisión de Inventario en Consignación'
				WHEN 'ConsignmentInventoryRemissionDevolution' THEN 'Devolución de Remisión de Inventario en Consignación'
				WHEN 'EntranceVoucher' THEN 'Comprobante de Entrada'
				WHEN 'EntranceVoucherDevolution' THEN 'Devolución de Compra'
				WHEN 'LoanMerchandise' THEN 'Prestamo de Mercancía'
				WHEN 'LoanMerchandiseDevolution' THEN 'Devolución de Prestamo de Mercancía'
				WHEN 'TransferOrder' THEN 'Orden de Traslado'
				WHEN 'TransferOrderDevolution' THEN 'Devolución de Orden de Traslado'
				WHEN 'PharmaceuticalDispensing' THEN 'Dispensación Farmacéutica'
				WHEN 'PharmaceuticalDispensingDevolution' THEN 'Devolución de Dispensación Farmacéutica'
				WHEN 'InventoryAdjustment' THEN 'Ajuste de Inventarios'
				WHEN 'JournalVouchers' THEN 'Comprobante Contable'
				ELSE ISNULL(k.EntityName, jv.EntityName)
			END [Tipo Documento Inventario],
			ISNULL(k.EntityCode, jv.EntityCode) [Código Documento Inventario],
			k.DocumentDate [Fecha Documento Inventario],
			--------------------------------------------------------
			jv.JournalVoucherTypeName [Tipo Comprobante Contable],
			jv.Consecutive [Consecutivo Comprobante Contable],
			jv.VoucherDate [Fecha Comprobante Contable],
			--------------------------------------------------------
			ISNULL(k.MainAccountNumberName, jv.MainAccountNumberName) [Cuenta Contable],
			ISNULL(k.DebitValue, 0) [Inventario Débito],
			ISNULL(jv.DebitValue, 0) [Contabilidad Débito],
			--------------------------------------------------------
			ISNULL(k.CreditValue, 0) [Inventario Crédito],
			ISNULL(jv.CreditValue, 0) [Contabilidad Crédito]
	FROM
	(
			SELECT	k.EntityName,
					k.EntityCode,
					k.EntityId,
					CAST(k.DocumentDate AS DATE) DocumentDate,
					ma.Id MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(IIF(k.MovementType = 1, k.Quantity * k.Value, 0)) DebitValue, 
					SUM(IIF(k.MovementType = 1, 0, k.Quantity * k.Value)) CreditValue
			FROM Inventory.Kardex k WITH (NOLOCK)
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON k.ProductId = ip.Id
			JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
			JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON pg.InventoryAccountPayableConceptId = apc.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON apc.IdAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			WHERE k.EntityName IN ('InventoryControl')
				AND CAST(k.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY k.EntityName, k.EntityCode, k.EntityId, CAST(k.DocumentDate AS DATE), ma.Id, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	k.EntityName,
					k.EntityCode,
					k.EntityId,
					CAST(k.DocumentDate AS DATE) DocumentDate,
					ma.Id MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(IIF(k.MovementType = 1, k.Quantity * k.Value, 0)) DebitValue, 
					SUM(IIF(k.MovementType = 1, 0, k.Quantity * k.Value)) CreditValue
			FROM Inventory.Kardex k WITH (NOLOCK)
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON k.ProductId = ip.Id
			JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON pg.ReferenceInputDebitAccountId = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			WHERE k.EntityName IN ('RemissionEntrance')
				AND CAST(k.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY k.EntityName, k.EntityCode, k.EntityId, CAST(k.DocumentDate AS DATE), ma.Id, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	k.EntityName,
					k.EntityCode,
					k.EntityId,
					CAST(k.DocumentDate AS DATE) DocumentDate,
					ma.Id MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(IIF(k.MovementType = 1, k.Quantity * k.Value, 0)) DebitValue, 
					SUM(IIF(k.MovementType = 1, 0, k.Quantity * k.Value)) CreditValue
			FROM Inventory.Kardex k WITH (NOLOCK)
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON k.ProductId = ip.Id
			JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON pg.ConsignmentMerchandiseDebitAccountId = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			WHERE k.EntityName IN ('ConsignmentInventoryRemission')
				AND CAST(k.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY k.EntityName, k.EntityCode, k.EntityId, CAST(k.DocumentDate AS DATE), ma.Id, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	CASE e.DevolutionType
						WHEN 1 THEN 'RemissionEntranceDevolution'
						WHEN 3 THEN 'ConsignmentInventoryRemissionDevolution'
					END EntityName,
					k.EntityCode,
					k.EntityId,
					CAST(k.DocumentDate AS DATE) DocumentDate,
					ma.Id MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(IIF(k.MovementType = 1, k.Quantity * k.Value, 0)) DebitValue, 
					SUM(IIF(k.MovementType = 1, 0, k.Quantity * k.Value)) CreditValue
			FROM Inventory.Kardex k WITH (NOLOCK)
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON k.ProductId = ip.Id
			JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON pg.ReferenceInputDebitAccountId = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			LEFT JOIN Inventory.RemissionDevolution e WITH (NOLOCK) ON k.EntityId = e.Id
			WHERE k.EntityName = 'RemissionDevolution' AND e.DevolutionType IN (1, 3)
				AND CAST(k.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY e.DevolutionType, k.EntityCode, k.EntityId, CAST(k.DocumentDate AS DATE), ma.Id, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	k.EntityName,
					k.EntityCode,
					k.EntityId,
					CAST(ISNULL(ev.DocumentDate, k.DocumentDate) AS DATE) DocumentDate,
					ma.Id MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(IIF(k.MovementType = 1, k.Quantity * k.Value, 0)) DebitValue, 
					SUM(IIF(k.MovementType = 1, 0, k.Quantity * k.Value)) CreditValue
			FROM Inventory.Kardex k WITH (NOLOCK)
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON k.ProductId = ip.Id
			JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
			JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON pg.InventoryAccountPayableConceptId = apc.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON apc.IdAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			LEFT JOIN Inventory.EntranceVoucher ev WITH (NOLOCK) ON k.EntityId = ev.Id
			WHERE k.EntityName IN ('EntranceVoucher')
				AND CAST(k.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY k.EntityName, k.EntityCode, k.EntityId, CAST(ISNULL(ev.DocumentDate, k.DocumentDate) AS DATE), ma.Id, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	k.EntityName,
					k.EntityCode,
					k.EntityId,
					CAST(ISNULL(evd.DocumentDate, k.DocumentDate) AS DATE) DocumentDate,
					ma.Id MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(IIF(k.MovementType = 1, k.Quantity * k.Value, 0)) DebitValue, 
					SUM(IIF(k.MovementType = 1, 0, k.Quantity * k.Value)) CreditValue
			FROM Inventory.Kardex k WITH (NOLOCK)
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON k.ProductId = ip.Id
			JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
			JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON pg.InventoryAccountPayableConceptId = apc.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON apc.IdAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			LEFT JOIN Inventory.EntranceVoucherDevolution evd WITH (NOLOCK) ON k.EntityId = evd.Id
			WHERE k.EntityName IN ('EntranceVoucherDevolution')
				AND CAST(k.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY k.EntityName, k.EntityCode, k.EntityId, CAST(ISNULL(evd.DocumentDate, k.DocumentDate) AS DATE), ma.Id, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	k.EntityName,
					k.EntityCode,
					k.EntityId,
					CAST(ISNULL(lm.DocumentDate, k.DocumentDate) AS DATE) DocumentDate,
					ma.Id MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(IIF(k.MovementType = 1, k.Quantity * k.Value, 0)) DebitValue, 
					SUM(IIF(k.MovementType = 1, 0, k.Quantity * k.Value)) CreditValue
			FROM Inventory.Kardex k WITH (NOLOCK)
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON k.ProductId = ip.Id
			JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
			JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON pg.InventoryAccountPayableConceptId = apc.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON apc.IdAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			LEFT JOIN Inventory.LoanMerchandise lm WITH (NOLOCK) ON k.EntityId = lm.Id
			WHERE k.EntityName IN ('LoanMerchandise')
				AND CAST(k.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY k.EntityName, k.EntityCode, k.EntityId, CAST(ISNULL(lm.DocumentDate, k.DocumentDate) AS DATE), ma.Id, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	k.EntityName,
					k.EntityCode,
					k.EntityId,
					CAST(k.DocumentDate AS DATE) DocumentDate,
					ma.Id MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(IIF(k.MovementType = 1, k.Quantity * k.Value, 0)) DebitValue, 
					SUM(IIF(k.MovementType = 1, 0, k.Quantity * k.Value)) CreditValue
			FROM Inventory.Kardex k WITH (NOLOCK)
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON k.ProductId = ip.Id
			JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
			JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON pg.InventoryAccountPayableConceptId = apc.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON apc.IdAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			WHERE k.EntityName IN ('LoanMerchandiseDevolution')
				AND CAST(k.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY k.EntityName, k.EntityCode, k.EntityId, CAST(k.DocumentDate AS DATE), ma.Id, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	k.EntityName,
					k.EntityCode,
					k.EntityId,
					CAST(ISNULL(tro.DocumentDate, k.DocumentDate) AS DATE) DocumentDate,
					ma.Id MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(IIF(k.MovementType = 1, k.Quantity * k.Value, 0)) DebitValue, 
					SUM(IIF(k.MovementType = 1, 0, k.Quantity * k.Value)) CreditValue
			FROM Inventory.Kardex k WITH (NOLOCK)
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON k.ProductId = ip.Id
			JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
			JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON pg.InventoryAccountPayableConceptId = apc.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON apc.IdAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			LEFT JOIN Inventory.TransferOrder tro WITH (NOLOCK) ON k.EntityId = tro.Id
			WHERE k.EntityName IN ('TransferOrder')
				AND CAST(k.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY k.EntityName, k.EntityCode, k.EntityId, CAST(ISNULL(tro.DocumentDate, k.DocumentDate) AS DATE), ma.Id, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	k.EntityName,
					k.EntityCode,
					k.EntityId,
					CAST(ISNULL(trodev.DocumentDate, k.DocumentDate) AS DATE) DocumentDate,
					ma.Id MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(IIF(k.MovementType = 1, k.Quantity * k.Value, 0)) DebitValue, 
					SUM(IIF(k.MovementType = 1, 0, k.Quantity * k.Value)) CreditValue
			FROM Inventory.Kardex k WITH (NOLOCK)
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON k.ProductId = ip.Id
			JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
			JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON pg.InventoryAccountPayableConceptId = apc.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON apc.IdAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			LEFT JOIN Inventory.TransferOrderDevolution trodev WITH (NOLOCK) ON k.EntityId = trodev.Id
			WHERE k.EntityName IN ('TransferOrderDevolution')
				AND CAST(k.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY k.EntityName, k.EntityCode, k.EntityId, CAST(ISNULL(trodev.DocumentDate, k.DocumentDate) AS DATE), ma.Id, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	k.EntityName,
					k.EntityCode,
					k.EntityId,
					CAST(ISNULL(pd.DocumentDate, k.DocumentDate) AS DATE) DocumentDate,
					ma.Id MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(IIF(k.MovementType = 1, k.Quantity * k.Value, 0)) DebitValue, 
					SUM(IIF(k.MovementType = 1, 0, k.Quantity * k.Value)) CreditValue
			FROM Inventory.Kardex k WITH (NOLOCK)
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON k.ProductId = ip.Id
			JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
			JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON pg.InventoryAccountPayableConceptId = apc.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON apc.IdAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			LEFT JOIN Inventory.PharmaceuticalDispensing pd WITH (NOLOCK) ON k.EntityId = pd.Id
			WHERE k.EntityName IN ('PharmaceuticalDispensing')
				AND CAST(k.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY k.EntityName, k.EntityCode, k.EntityId, CAST(ISNULL(pd.DocumentDate, k.DocumentDate) AS DATE), ma.Id, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	k.EntityName,
					k.EntityCode,
					k.EntityId,
					CAST(ISNULL(pddev.DocumentDate, k.DocumentDate) AS DATE) DocumentDate,
					ma.Id MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(IIF(k.MovementType = 1, k.Quantity * k.Value, 0)) DebitValue, 
					SUM(IIF(k.MovementType = 1, 0, k.Quantity * k.Value)) CreditValue
			FROM Inventory.Kardex k WITH (NOLOCK)
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON k.ProductId = ip.Id
			JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
			JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON pg.InventoryAccountPayableConceptId = apc.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON apc.IdAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			LEFT JOIN Inventory.PharmaceuticalDispensingDevolution pddev WITH (NOLOCK) ON k.EntityId = pddev.Id
			WHERE k.EntityName IN ('PharmaceuticalDispensingDevolution')
				AND CAST(k.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY k.EntityName, k.EntityCode, k.EntityId, CAST(ISNULL(pddev.DocumentDate, k.DocumentDate) AS DATE), ma.Id, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	k.EntityName,
					k.EntityCode,
					k.EntityId,
					CAST(ISNULL(ia.DocumentDate, k.DocumentDate) AS DATE) DocumentDate,
					ma.Id MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(IIF(k.MovementType = 1, k.Quantity * k.Value, 0)) DebitValue, 
					SUM(IIF(k.MovementType = 1, 0, k.Quantity * k.Value)) CreditValue
			FROM Inventory.Kardex k WITH (NOLOCK)
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON k.ProductId = ip.Id
			JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
			JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON pg.InventoryAccountPayableConceptId = apc.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON apc.IdAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			LEFT JOIN Inventory.InventoryAdjustment ia WITH (NOLOCK) ON k.EntityId = ia.Id
			WHERE k.EntityName IN ('InventoryAdjustment')
				AND CAST(k.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY k.EntityName, k.EntityCode, k.EntityId, CAST(ISNULL(ia.DocumentDate, k.DocumentDate) AS DATE), ma.Id, CONCAT(ma.Number, ' - ', ma.Name)
	) k
	FULL JOIN
	(
			SELECT	jvt.Name JournalVoucherTypeName, 
					jv.Consecutive,
					jv.EntityName EntityName, 
					jv.EntityCode EntityCode,
					jv.EntityId EntityId,
					CAST(jv.VoucherDate AS DATE) VoucherDate, 
					jvd.IdMainAccount MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(jvd.DebitValue) DebitValue, 
					SUM(jvd.CreditValue) CreditValue
			FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
			JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			WHERE jv.Status = 2 
				AND jv.EntityName IN ('JournalVouchers')
				AND CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY jvt.Name, jv.Consecutive, jv.EntityName, jv.EntityCode, jv.EntityId, jv.VoucherDate, jvd.IdMainAccount, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	jvt.Name JournalVoucherTypeName, 
					jv.Consecutive,
					jv.EntityName EntityName, 
					jv.EntityCode EntityCode,
					jv.EntityId EntityId,
					CAST(jv.VoucherDate AS DATE) VoucherDate, 
					jvd.IdMainAccount MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(jvd.DebitValue) DebitValue, 
					SUM(jvd.CreditValue) CreditValue
			FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
			JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			WHERE jv.Status = 2 
				AND jv.EntityName IN ('RemissionEntrance')
				AND CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY jvt.Name, jv.Consecutive, jv.EntityName, jv.EntityCode, jv.EntityId, jv.VoucherDate, jvd.IdMainAccount, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	jvt.Name JournalVoucherTypeName, 
					jv.Consecutive,
					jv.EntityName EntityName, 
					jv.EntityCode EntityCode,
					jv.EntityId EntityId,
					CAST(jv.VoucherDate AS DATE) VoucherDate, 
					jvd.IdMainAccount MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(jvd.DebitValue) DebitValue, 
					SUM(jvd.CreditValue) CreditValue
			FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
			JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			WHERE jv.Status = 2 
				AND jv.EntityName IN ('ConsignmentInventoryRemission')
				AND CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY jvt.Name, jv.Consecutive, jv.EntityName, jv.EntityCode, jv.EntityId, jv.VoucherDate, jvd.IdMainAccount, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	jvt.Name JournalVoucherTypeName, 
					jv.Consecutive,
					jv.EntityName EntityName, 
					jv.EntityCode EntityCode,
					jv.EntityId EntityId,
					CAST(jv.VoucherDate AS DATE) VoucherDate, 
					jvd.IdMainAccount MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(jvd.DebitValue) DebitValue, 
					SUM(jvd.CreditValue) CreditValue
			FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
			JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			WHERE jv.Status = 2 
				AND jv.EntityName IN ('RemissionEntranceDevolution', 'ConsignmentInventoryRemissionDevolution')
				AND CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY jvt.Name, jv.Consecutive, jv.EntityName, jv.EntityCode, jv.EntityId, jv.VoucherDate, jvd.IdMainAccount, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	jvt.Name JournalVoucherTypeName, 
					jv.Consecutive,
					ap.EntityName EntityName, 
					ap.EntityCode EntityCode,
					ap.EntityId EntityId,
					CAST(jv.VoucherDate AS DATE) VoucherDate, 
					jvd.IdMainAccount MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(jvd.DebitValue) DebitValue, 
					SUM(jvd.CreditValue) CreditValue
			FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
			JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			LEFT JOIN Payments.AccountPayable ap ON jv.EntityId = ap.Id
			WHERE jv.Status = 2 
				AND jv.EntityName IN ('AccountPayable')
				AND CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY jvt.Name, jv.Consecutive, ap.EntityName, ap.EntityCode, ap.EntityId, jv.VoucherDate, jvd.IdMainAccount, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	jvt.Name JournalVoucherTypeName, 
					jv.Consecutive,
					pn.EntityName EntityName, 
					pn.EntityCode EntityCode,
					pn.EntityId EntityId,
					CAST(jv.VoucherDate AS DATE) VoucherDate, 
					jvd.IdMainAccount MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(jvd.DebitValue) DebitValue, 
					SUM(jvd.CreditValue) CreditValue
			FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
			JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			LEFT JOIN Payments.PaymentNotes pn ON jv.EntityId = pn.Id
			WHERE jv.Status = 2 
				AND jv.EntityName IN ('PaymentNotes')
				AND CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY jvt.Name, jv.Consecutive, pn.EntityName, pn.EntityCode, pn.EntityId, jv.VoucherDate, jvd.IdMainAccount, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	jvt.Name JournalVoucherTypeName, 
					jv.Consecutive,
					jv.EntityName EntityName, 
					jv.EntityCode EntityCode,
					jv.EntityId EntityId,
					CAST(jv.VoucherDate AS DATE) VoucherDate, 
					jvd.IdMainAccount MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(jvd.DebitValue) DebitValue, 
					SUM(jvd.CreditValue) CreditValue
			FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
			JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			WHERE jv.Status = 2 
				AND jv.EntityName IN ('LoanMerchandise')
				AND CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY jvt.Name, jv.Consecutive, jv.EntityName, jv.EntityCode, jv.EntityId, jv.VoucherDate, jvd.IdMainAccount, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	jvt.Name JournalVoucherTypeName, 
					jv.Consecutive,
					jv.EntityName EntityName, 
					jv.EntityCode EntityCode,
					jv.EntityId EntityId,
					CAST(jv.VoucherDate AS DATE) VoucherDate, 
					jvd.IdMainAccount MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(jvd.DebitValue) DebitValue, 
					SUM(jvd.CreditValue) CreditValue
			FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
			JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			WHERE jv.Status = 2 
				AND jv.EntityName IN ('LoanMerchandiseDevolution')
				AND CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY jvt.Name, jv.Consecutive, jv.EntityName, jv.EntityCode, jv.EntityId, jv.VoucherDate, jvd.IdMainAccount, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	jvt.Name JournalVoucherTypeName, 
					jv.Consecutive,
					jv.EntityName EntityName, 
					jv.EntityCode EntityCode,
					jv.EntityId EntityId,
					CAST(jv.VoucherDate AS DATE) VoucherDate, 
					jvd.IdMainAccount MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(jvd.DebitValue) DebitValue, 
					SUM(jvd.CreditValue) CreditValue
			FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
			JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			WHERE jv.Status = 2 
				AND jv.EntityName IN ('TransferOrder')
				AND CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY jvt.Name, jv.Consecutive, jv.EntityName, jv.EntityCode, jv.EntityId, jv.VoucherDate, jvd.IdMainAccount, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	jvt.Name JournalVoucherTypeName, 
					jv.Consecutive,
					jv.EntityName EntityName, 
					jv.EntityCode EntityCode,
					jv.EntityId EntityId,
					CAST(jv.VoucherDate AS DATE) VoucherDate, 
					jvd.IdMainAccount MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(jvd.DebitValue) DebitValue, 
					SUM(jvd.CreditValue) CreditValue
			FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
			JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			WHERE jv.Status = 2 
				AND jv.EntityName IN ('TransferOrderDevolution')
				AND CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY jvt.Name, jv.Consecutive, jv.EntityName, jv.EntityCode, jv.EntityId, jv.VoucherDate, jvd.IdMainAccount, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	jvt.Name JournalVoucherTypeName, 
					jv.Consecutive,
					jv.EntityName EntityName, 
					jv.EntityCode EntityCode,
					jv.EntityId EntityId,
					CAST(jv.VoucherDate AS DATE) VoucherDate, 
					jvd.IdMainAccount MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(jvd.DebitValue) DebitValue, 
					SUM(jvd.CreditValue) CreditValue
			FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
			JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			WHERE jv.Status = 2 
				AND jv.EntityName IN ('PharmaceuticalDispensing')
				AND CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY jvt.Name, jv.Consecutive, jv.EntityName, jv.EntityCode, jv.EntityId, jv.VoucherDate, jvd.IdMainAccount, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	jvt.Name JournalVoucherTypeName, 
					jv.Consecutive,
					jv.EntityName EntityName, 
					jv.EntityCode EntityCode,
					jv.EntityId EntityId,
					CAST(jv.VoucherDate AS DATE) VoucherDate, 
					jvd.IdMainAccount MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(jvd.DebitValue) DebitValue, 
					SUM(jvd.CreditValue) CreditValue
			FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
			JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			WHERE jv.Status = 2 
				AND jv.EntityName IN ('PharmaceuticalDispensingDevolution')
				AND CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY jvt.Name, jv.Consecutive, jv.EntityName, jv.EntityCode, jv.EntityId, jv.VoucherDate, jvd.IdMainAccount, CONCAT(ma.Number, ' - ', ma.Name)
		UNION ALL
			SELECT	jvt.Name JournalVoucherTypeName, 
					jv.Consecutive,
					jv.EntityName EntityName, 
					jv.EntityCode EntityCode,
					jv.EntityId EntityId,
					CAST(jv.VoucherDate AS DATE) VoucherDate, 
					jvd.IdMainAccount MainAccountId,
					CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
					SUM(jvd.DebitValue) DebitValue, 
					SUM(jvd.CreditValue) CreditValue
			FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
			JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id AND ma.LegalBookId = @LegalBookId AND ma.Number LIKE CONCAT(@MainAccountGroup, '%')
			WHERE jv.Status = 2 
				AND jv.EntityName IN ('InventoryAdjustment')
				AND CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY jvt.Name, jv.Consecutive, jv.EntityName, jv.EntityCode, jv.EntityId, jv.VoucherDate, jvd.IdMainAccount, CONCAT(ma.Number, ' - ', ma.Name)
	) jv ON ISNULL(k.EntityName, '') = ISNULL(jv.EntityName, '') 
		AND ISNULL(k.EntityId, 0) = ISNULL(jv.EntityId, 0) 
		AND k.MainAccountId = jv.MainAccountId
	--WHERE 
	--(
	--	ISNULL(k.DebitValue, 0) <> ISNULL(jv.DebitValue, 0) 
	--	OR 
	--	ISNULL(k.CreditValue, 0) <> ISNULL(jv.CreditValue, 0)
	--)
	ORDER BY ISNULL(k.DocumentDate, jv.VoucherDate)
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de conciliación contable de inventario: compara los movimientos valorizados registrados en el kardex de inventario (entradas, salidas, traslados, dispensaciones, ajustes, remisiones, consignaciones y sus respectivas devoluciones) contra los comprobantes contables registrados en el libro oficial de contabilidad, para un rango de fechas dado. Cruza el kardex con el catálogo de productos, grupos de productos y sus cuentas contables asociadas (cuentas por pagar, libro mayor), mostrando lado a lado los valores en débito y crédito tanto desde la perspectiva del inventario como de la contabilidad. Su propósito es detectar diferencias o brechas entre lo que registró el módulo de inventario y lo que quedó asentado contablemente, facilitando el cierre contable, auditorías internas y la conciliación financiera del inventario de medicamentos e insumos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_InventoryReconciliation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_InventoryReconciliation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta la conciliación entre los movimientos valorizados del kardex de inventario y los comprobantes contables del libro mayor, comparando débitos y créditos por cuenta del grupo 14 dentro de un rango de fechas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_InventoryReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un libro contable marcado como oficial (GeneralLedger.LegalBook.OfficialBook = 1) para resolver @LegalBookId.; Los productos del kardex deben tener ProductGroup y este debe tener configuradas las cuentas contables (InventoryAccountPayableConceptId, ReferenceInputDebitAccountId o ConsignmentMerchandiseDebitAccountId) apuntando al plan de cuentas oficial.; Las cuentas contables consideradas deben pertenecer al libro oficial y a la clase de cuenta que comienza con el prefijo ''14'' (inventarios).; Los comprobantes contables deben estar en estado 2 para ser tenidos en cuenta en la comparación.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_InventoryReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera cuentas del libro oficial (LegalBookId = @LegalBookId con OfficialBook=1) y cuyo número inicia con ''14'' (clase inventarios).; Solo se incluyen comprobantes contables con Status = 2.; Los movimientos del kardex se filtran por DocumentDate (CAST a DATE) entre @DateStart y @DateEnd; los comprobantes por VoucherDate (CAST a DATE) en el mismo rango.; El cruce entre inventario y contabilidad se realiza por (EntityName, EntityId, MainAccountId), tratando NULL como '''' o 0 mediante ISNULL.; Para tipos de documento con tabla de encabezado (EntranceVoucher, EntranceVoucherDevolution, LoanMerchandise, TransferOrder, TransferOrderDevolution, PharmaceuticalDispensing, PharmaceuticalDispensingDevolution, InventoryAdjustment) la fecha del documento se prioriza desde el encabezado y, si es nula, se usa la del kardex.; Todas las consultas usan WITH (NOLOCK), por lo que pueden producirse lecturas sucias.; El resultado se ordena por la fecha del documento de inventario (o, si es nulo, por la fecha del comprobante contable).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_InventoryReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Conciliación inventario-contabilidad; Kardex valorizado; Plan único de cuentas (clase 14 - Inventarios); Libro contable oficial; Comprobantes contables (Journal Vouchers); Remisiones de entrada y devoluciones; Inventario en consignación; Comprobantes de entrada y devolución de compra; Préstamo de mercancía; Órdenes de traslado; Dispensación farmacéutica; Ajustes de inventario; Cuentas por pagar y notas de pago; Movimientos débito/crédito (MovementType); Grupo de productos y cuentas contables asociadas', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_InventoryReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve un conjunto de filas con el cruce (FULL JOIN) entre kardex y comprobantes contables por EntityName, EntityId y MainAccountId, mostrando débitos y créditos de inventario vs contabilidad y traduciendo EntityName a etiquetas en español del tipo de documento.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_InventoryReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si k.EntityName = ''RemissionDevolution'' y e.DevolutionType IN (1,3) → Reclasifica el tipo de documento: DevolutionType=1 → ''RemissionEntranceDevolution''; DevolutionType=3 → ''ConsignmentInventoryRemissionDevolution'', usando pg.ReferenceInputDebitAccountId como cuenta contable. else Las demás devoluciones de remisión no se incluyen.; si EntityName del kardex = ''InventoryControl'' → Toma la cuenta contable desde Payments.AccountPayableConcepts (apc.IdAccount) ligado al ProductGroup vía InventoryAccountPayableConceptId. else Para ''RemissionEntrance'' y ''ConsignmentInventoryRemission'' usa cuentas específicas del ProductGroup (ReferenceInputDebitAccountId / ConsignmentMerchandiseDebitAccountId).; si k.MovementType = 1 → El valor (Quantity * Value) se acumula como Débito de inventario. else El valor (Quantity * Value) se acumula como Crédito de inventario.; si jv.EntityName = ''AccountPayable'' (lado contable) → Toma EntityName/EntityCode/EntityId desde Payments.AccountPayable (LEFT JOIN por jv.EntityId), no desde el voucher. else Para ''PaymentNotes'' los toma desde Payments.PaymentNotes; para los demás los toma directamente del JournalVoucher.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_InventoryReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; Inventory.Kardex; Inventory.InventoryProduct; Inventory.ProductGroup; Payments.AccountPayableConcepts; GeneralLedger.MainAccounts; Inventory.RemissionDevolution; Inventory.EntranceVoucher; Inventory.EntranceVoucherDevolution; Inventory.LoanMerchandise; Inventory.TransferOrder; Inventory.TransferOrderDevolution; Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDevolution; Inventory.InventoryAdjustment; GeneralLedger.JournalVoucherTypes; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; Payments.AccountPayable; Payments.PaymentNotes', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_InventoryReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_InventoryReconciliation';
-- GO
