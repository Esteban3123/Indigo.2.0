CREATE PROCEDURE [Inventory].[SP_ReportDocumentsDisorganizedInventoryVsAccounting]
	@DateStart DATE,
	@DateEnd DATE
AS
BEGIN
	DECLARE @GroupInventory VARCHAR(3),
			@LegalBookId INT

	SELECT @GroupInventory = SUBSTRING(ma.Number, 1, 2) + '%'
	FROM Inventory.ProductGroup pg (NOLOCK)
	JOIN Payments.AccountPayableConcepts apc (NOLOCK) ON pg.InventoryAccountPayableConceptId = apc.Id
	JOIN GeneralLedger.MainAccounts ma ON apc.IdAccount = ma.Id
	WHERE pg.Status = 1

	SELECT @LegalBookId = lb.Id
	FROM GeneralLedger.LegalBook lb (NOLOCK)
	WHERE lb.OfficialBook = 1

	SELECT 
		ISNULL(k.EntityName, jv.EntityName) EntityName,
		CASE ISNULL(k.EntityName, jv.EntityName)
			WHEN 'ConsignmentInventoryRemission' THEN 'Remission de Inventario en Consignación'
			WHEN 'EntranceVoucher' THEN 'Comprobante de Entrada'
			WHEN 'EntranceVoucherDevolution' THEN 'Devolución de Compra'
			WHEN 'InventoryAdjustment' THEN 'Ajuste de Inventario'
			WHEN 'PharmaceuticalDispensing' THEN 'Dispensación Farmacéutica'
			WHEN 'PharmaceuticalDispensingDevolution' THEN 'Devolución de Dispensación'
			WHEN 'RemissionDevolution' THEN 'Devolución de Remisiones'
			WHEN 'RemissionEntrance' THEN 'Remission de Entrada'			
			WHEN 'TransferOrder' THEN 'Orden de Traslado'
			WHEN 'TransferOrderDevolution' THEN 'Devolución de Orden de Traslado'
			ELSE ISNULL(k.EntityName, jv.EntityName)
		END EntityNameText,
		ISNULL(k.EntityCode, jv.EntityCode) EntityCode,
		ISNULL(k.EntityId, jv.EntityId) EntityId,
		SUM(ISNULL(jv.Value, 0)) AccountingValue, 
		SUM(ISNULL(k.Value, 0)) KardexValue,
		ABS(SUM(ISNULL(jv.Value, 0) - ISNULL(k.Value, 0))) Diff
	FROM
	(
		SELECT 
			k.EntityName, k.EntityCode, k.EntityId, 
			SUM(ROUND(k.Quantity * k.Value, 2)) Value
		FROM
		(
			SELECT 
				CASE k.EntityName
					WHEN 'InventoryAdjustment' THEN ia.DocumentDate
					WHEN 'EntranceVoucher' THEN ev.DocumentDate
					WHEN 'EntranceVoucherDevolution' THEN evd.DocumentDate
					WHEN 'TransferOrder' THEN tro.DocumentDate
					WHEN 'TransferOrderDevolution' THEN trod.DocumentDate
					ELSE k.CreationDate
				END AS VoucherDate,
				k.EntityName,
				k.EntityCode,
				k.EntityId,
				k.MovementType,
				k.Quantity * IIF(k.MovementType = 1, 1, -1) Quantity,
				k.Value
			FROM Inventory.Kardex k WITH (NOLOCK)
			LEFT JOIN Inventory.InventoryAdjustment ia WITH (NOLOCK) ON k.EntityName = 'InventoryAdjustment' AND k.EntityId = ia.Id
			LEFT JOIN Inventory.EntranceVoucher ev WITH (NOLOCK) ON k.EntityName = 'EntranceVoucher' AND k.EntityId = ev.Id
			LEFT JOIN Inventory.EntranceVoucherDevolution evd WITH (NOLOCK) ON k.EntityName = 'EntranceVoucherDevolution' AND k.EntityId = evd.Id
			LEFT JOIN Inventory.TransferOrder tro WITH (NOLOCK) ON k.EntityName = 'TransferOrder' AND k.EntityId = tro.Id
			LEFT JOIN Inventory.TransferOrderDevolution trod WITH (NOLOCK) ON k.EntityName = 'TransferOrderDevolution' AND k.EntityId = trod.Id	
			WHERE k.EntityName NOT IN ('InventoryControl')
		) k
		WHERE CAST(k.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
		GROUP BY k.EntityName, k.EntityCode, k.EntityId
	) k
	FULL JOIN
	(
		SELECT 
			CASE jv.EntityName
				WHEN 'AccountPayable' THEN ap.EntityName
				WHEN 'PaymentNotes' THEN pn.EntityName
				WHEN 'RemissionEntranceDevolution' THEN 'RemissionDevolution'
				ELSE jv.EntityName
			END EntityName, 
			CASE jv.EntityName
				WHEN 'AccountPayable' THEN ap.EntityCode
				WHEN 'PaymentNotes' THEN pn.EntityCode
				ELSE jv.EntityCode
			END EntityCode, 
			CASE jv.EntityName
				WHEN 'AccountPayable' THEN ap.EntityId
				WHEN 'PaymentNotes' THEN pn.EntityId
				ELSE jv.EntityId
			END EntityId, 
			SUM(ROUND(jvd.DebitValue - jvd.CreditValue, 2)) Value
		FROM GeneralLedger.JournalVouchers jv (NOLOCK)
		JOIN GeneralLedger.JournalVoucherDetails jvd (NOLOCK) ON jv.Id = jvd.IdAccounting
		JOIN GeneralLedger.MainAccounts ma (NOLOCK) ON jvd.IdMainAccount = ma.Id AND ma.Number LIKE @GroupInventory
		LEFT JOIN Payments.AccountPayable ap (NOLOCK) ON jv.EntityName = 'AccountPayable' AND jv.EntityCode = ap.Code
		LEFT JOIN Payments.PaymentNotes pn (NOLOCK) ON jv.EntityName = 'PaymentNotes' AND jv.EntityCode = pn.Code
		WHERE jv.LegalBookId = @LegalBookId
			AND jv.Status = 2
			AND CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
		GROUP BY jv.EntityName, jv.EntityCode, jv.EntityId,
			ap.EntityName, ap.EntityCode, ap.EntityId,
			pn.EntityName, pn.EntityCode, pn.EntityId
	) jv ON k.EntityName = jv.EntityName AND k.EntityCode = jv.EntityCode
	WHERE ISNULL(k.Value, 0) <> ISNULL(jv.Value, 0)
	GROUP BY ISNULL(k.EntityName, jv.EntityName),
		ISNULL(k.EntityCode, jv.EntityCode),
		ISNULL(k.EntityId, jv.EntityId)
END