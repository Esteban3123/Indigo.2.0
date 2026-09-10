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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de documentos descuadrados entre inventario y contabilidad para un rango de fechas. Compara el valor registrado en el kardex de inventario (entradas, salidas, ajustes, traslados, devoluciones y dispensaciones farmacéuticas) contra el valor contabilizado en los comprobantes del libro oficial (mayor general), identificando los documentos donde ambos valores no coinciden. Utiliza el grupo contable de inventario configurado en los grupos de producto para filtrar solo las cuentas contables de inventario, y presenta por cada documento la entidad (tipo de movimiento), el código, el valor en kardex, el valor contable y la diferencia absoluta. Sirve para auditoría, cierre contable y conciliación entre el módulo de inventario y el módulo de contabilidad general, detectando descuadres o inconsistencias en comprobantes de entrada, devoluciones de compra, ajustes, órdenes de traslado y otros movimientos de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportDocumentsDisorganizedInventoryVsAccounting';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportDocumentsDisorganizedInventoryVsAccounting';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de descuadre entre el valor contabilizado en el libro oficial y el valor del kardex de inventario, agrupado por documento, dentro de un rango de fechas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDocumentsDisorganizedInventoryVsAccounting';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un ProductGroup activo (Status=1) cuya cuenta contable de inventario permita derivar el prefijo de cuentas (primeros 2 caracteres del Number); Debe existir un LegalBook marcado como OfficialBook=1; Los comprobantes contables considerados deben tener Status=2 (contabilizados/aprobados)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDocumentsDisorganizedInventoryVsAccounting';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran asientos contables del libro oficial (OfficialBook=1) y en estado contabilizado (Status=2); Solo se evalúan líneas contables cuya cuenta principal pertenezca al grupo de inventario (Number LIKE prefijo derivado del ProductGroup activo); El valor contable se calcula como Débito - Crédito redondeado a 2 decimales; El valor de kardex se calcula como Quantity*Value con signo según MovementType, redondeado a 2 decimales; El cruce entre kardex y contabilidad se hace por (EntityName, EntityCode), normalizando documentos referenciados desde AccountPayable, PaymentNotes y RemissionEntranceDevolution; Se usa FULL JOIN para incluir documentos que existan solo en kardex o solo en contabilidad; Se excluyen movimientos de tipo InventoryControl del kardex', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDocumentsDisorganizedInventoryVsAccounting';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Kardex de inventario; Comprobante contable / Journal Voucher; Libro oficial contable; Plan de cuentas (PUC); Grupo de inventario; Cuentas por pagar; Notas de pago; Ajuste de inventario; Comprobante de entrada; Devolución de compra; Orden de traslado; Devolución de orden de traslado; Dispensación farmacéutica; Devolución de dispensación; Remisión de inventario en consignación; Remisión de entrada; Devolución de remisiones; Conciliación inventario vs contabilidad', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDocumentsDisorganizedInventoryVsAccounting';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve únicamente documentos donde ISNULL(k.Value,0) <> ISNULL(jv.Value,0), es decir, donde el valor del kardex difiere del valor contabilizado; [RETURN_RESULT] resultset: Traduce EntityName técnico a etiqueta legible en español (p.ej. ''EntranceVoucher'' -> ''Comprobante de Entrada'', ''InventoryAdjustment'' -> ''Ajuste de Inventario''); si no hay mapeo definido, devuelve el nombre original; [RETURN_RESULT] resultset: Calcula la diferencia absoluta entre AccountingValue y KardexValue como columna Diff', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDocumentsDisorganizedInventoryVsAccounting';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si k.MovementType = 1 (entrada) → La cantidad del kardex se toma con signo positivo else La cantidad se invierte a negativo (salida); si k.EntityName ∈ {InventoryAdjustment, EntranceVoucher, EntranceVoucherDevolution, TransferOrder, TransferOrderDevolution} → La fecha de filtrado (VoucherDate) se toma del documento origen correspondiente else Se usa k.CreationDate como fecha del movimiento; si jv.EntityName = ''AccountPayable'' → Se reemplaza EntityName/EntityCode/EntityId por los de la AccountPayable referenciada (documento original de inventario); si jv.EntityName = ''PaymentNotes'' → Se reemplaza EntityName/EntityCode/EntityId por los de la PaymentNotes referenciada; si jv.EntityName = ''RemissionEntranceDevolution'' → Se normaliza el nombre a ''RemissionDevolution'' para empatar con el kardex; si k.EntityName = ''InventoryControl'' → Se excluye del cálculo del kardex', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDocumentsDisorganizedInventoryVsAccounting';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ProductGroup; Payments.AccountPayableConcepts; GeneralLedger.MainAccounts; GeneralLedger.LegalBook; Inventory.Kardex; Inventory.InventoryAdjustment; Inventory.EntranceVoucher; Inventory.EntranceVoucherDevolution; Inventory.TransferOrder; Inventory.TransferOrderDevolution; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; Payments.AccountPayable; Payments.PaymentNotes', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDocumentsDisorganizedInventoryVsAccounting';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDocumentsDisorganizedInventoryVsAccounting';
-- GO
