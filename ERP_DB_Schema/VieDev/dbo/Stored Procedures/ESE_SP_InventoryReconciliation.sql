CREATE PROCEDURE [dbo].[ESE_SP_InventoryReconciliation]
	@DateStart DATE,
	@DateEnd DATE
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
	WHERE 
	(
		ISNULL(k.DebitValue, 0) <> ISNULL(jv.DebitValue, 0) 
		OR 
		ISNULL(k.CreditValue, 0) <> ISNULL(jv.CreditValue, 0)
	)
	ORDER BY ISNULL(k.DocumentDate, jv.VoucherDate)
END