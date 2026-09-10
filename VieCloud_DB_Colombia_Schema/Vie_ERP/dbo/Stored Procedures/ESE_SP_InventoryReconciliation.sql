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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de conciliación de inventario versus contabilidad para un rango de fechas dado. Compara los movimientos del kardex de inventario (entradas, salidas, devoluciones, traslados, dispensaciones farmacéuticas, ajustes, consignaciones y préstamos de mercancía) con los comprobantes contables registrados en el libro oficial, cruzando las cuentas del grupo 14 (inventarios) asignadas a cada grupo de producto. Produce un informe lado a lado que muestra, por tipo de documento de inventario y comprobante contable, los valores de débito y crédito tanto desde el inventario como desde la contabilidad, permitiendo identificar diferencias o descuadres entre ambos módulos. Es utilizado por el área contable y de auditoría para verificar que cada movimiento de bodega tenga su correspondiente registro en el libro mayor oficial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_InventoryReconciliation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_InventoryReconciliation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de conciliación entre los movimientos del kardex de inventario y los comprobantes contables del libro oficial, mostrando solo los documentos cuyos valores de débito o crédito no coinciden entre inventario y contabilidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_InventoryReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en GeneralLedger.LegalBook con OfficialBook = 1 (libro oficial); Los productos deben estar asociados a un ProductGroup con la cuenta contable de inventario configurada (InventoryAccountPayableConceptId, ReferenceInputDebitAccountId o ConsignmentMerchandiseDebitAccountId según el tipo de movimiento); Las cuentas contables relacionadas deben pertenecer al libro oficial y a cuentas cuyo Number inicia con ''14'' (grupo de inventarios); El parámetro de rango de fechas (@DateStart, @DateEnd) debe estar definido para acotar tanto DocumentDate del kardex como VoucherDate del comprobante contable', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_InventoryReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran cuentas contables del libro marcado como oficial (OfficialBook = 1); Solo se evalúan cuentas cuyo número inicia con ''14'' (grupo de inventarios del PUC); Solo se consideran comprobantes contables con Status = 2 (estado considerado válido/contabilizado); El emparejamiento inventario-contabilidad se hace por (EntityName, EntityId, MainAccountId), tratando NULL como cadena vacía o 0; El rango de fechas se aplica tanto a DocumentDate del kardex como a VoucherDate del comprobante contable; Los valores nulos de débito/crédito se tratan como 0 al comparar e informar; El resultado se ordena por la fecha del documento de inventario, o por la del comprobante contable cuando no exista contraparte de inventario; Cada tipo de documento de inventario usa la cuenta contable definida en su configuración: InventoryAccountPayableConceptId para la mayoría, ReferenceInputDebitAccountId para remisiones de entrada y sus devoluciones, y ConsignmentMerchandiseDebitAccountId para remisiones de inventario en consignación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_InventoryReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Kardex de inventario; Remisión de entrada; Devolución de remisión; Inventario en consignación; Comprobante de entrada; Devolución de compra; Préstamo de mercancía; Orden de traslado; Dispensación farmacéutica; Ajuste de inventarios; Comprobante contable (Journal Voucher); Plan de cuentas (cuentas mayores); Libro oficial contable; Cuentas por pagar; Notas de pago; Grupo/clasificación contable de productos; Conciliación inventario vs contabilidad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_InventoryReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve únicamente las combinaciones documento-cuenta donde ISNULL(k.DebitValue,0) <> ISNULL(jv.DebitValue,0) OR ISNULL(k.CreditValue,0) <> ISNULL(jv.CreditValue,0), es decir, las diferencias entre inventario y contabilidad para cuentas del grupo ''14'' del libro oficial dentro del rango de fechas dado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_InventoryReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EntityName del Kardex (mapeada vía CASE para devoluciones de remisión según DevolutionType 1 o 3) → Traduce el código técnico de la entidad a una etiqueta de negocio en español (Control de Inventario, Remisión de Entrada, Devolución de Compra, Dispensación Farmacéutica, Ajuste de Inventarios, etc.); si k.MovementType = 1 → El valor (Quantity * Value) se acumula como Débito de inventario else El valor (Quantity * Value) se acumula como Crédito de inventario; si k.EntityName = ''RemissionDevolution'' AND e.DevolutionType IN (1, 3) → Se reclasifica como ''RemissionEntranceDevolution'' (tipo 1) o ''ConsignmentInventoryRemissionDevolution'' (tipo 3) para emparejar con el comprobante contable correspondiente; si jv.EntityName = ''AccountPayable'' → Se obtiene EntityName/EntityCode/EntityId desde Payments.AccountPayable (no desde JournalVouchers) para enlazar con el documento de inventario originador; si jv.EntityName = ''PaymentNotes'' → Se obtiene EntityName/EntityCode/EntityId desde Payments.PaymentNotes para enlazar con el documento de inventario originador; si ISNULL(k.DebitValue,0) <> ISNULL(jv.DebitValue,0) OR ISNULL(k.CreditValue,0) <> ISNULL(jv.CreditValue,0) → La fila se incluye en el resultado por ser una diferencia (descuadre) entre inventario y contabilidad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_InventoryReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; Inventory.Kardex; Inventory.InventoryProduct; Inventory.ProductGroup; Payments.AccountPayableConcepts; GeneralLedger.MainAccounts; Inventory.RemissionDevolution; Inventory.EntranceVoucher; Inventory.EntranceVoucherDevolution; Inventory.LoanMerchandise; Inventory.TransferOrder; Inventory.TransferOrderDevolution; Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDevolution; Inventory.InventoryAdjustment; GeneralLedger.JournalVoucherTypes; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; Payments.AccountPayable; Payments.PaymentNotes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_InventoryReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_InventoryReconciliation';
-- GO
