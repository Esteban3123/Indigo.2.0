

-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-06-18
-- Description:	Procedimiento para el reporte de edades de cuentas por pagar
-- =============================================
CREATE     PROCEDURE [Payments].[SP_Onco_ReportPaymentsByAge]
	---@HisContainer AS VARCHAR(20),
	---@xmlCriterias AS XML,
	@xmlFilters AS XML
AS
BEGIN
	SET NOCOUNT ON;
	SET DATEFORMAT DMY

	DECLARE -- CRITERIOS --
			--@IncludeZero INT,
			--@OrderBy INT,
			-- FILTROS --
			@ClosingDate DATE,
			@AfterClosingDate DATE--,
			---@OperatingUnit INT,
			---@SupplierStart VARCHAR(MAX),
			---@SupplierEnd VARCHAR(MAX),
			---@CostCenterStart VARCHAR(MAX),
			---@CostCenterEnd VARCHAR(MAX)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		----SELECT 
		----	@IncludeZero = t.x.value('IncludeZero[1]','int'),
		----	@OrderBy = t.x.value('OrderBy[1]','int')
		----FROM @xmlCriterias.nodes('/Data') t(x)

		--Se obtienen los datos de los filtros
		SELECT 
			@ClosingDate = t.x.value('ClosingDate[1]','date')---,
			--@OperatingUnit = t.x.value('OperatingUnit[1]','int'),
			--@SupplierStart = t.x.value('SupplierStart[1]','varchar(max)'),
			--@SupplierEnd = t.x.value('SupplierEnd[1]','varchar(max)'),
			--@CostCenterStart = t.x.value('CostCenterStart[1]','varchar(max)'),
			--@CostCenterEnd = t.x.value('CostCenterEnd[1]','varchar(max)')
		FROM @xmlFilters.nodes('/Data') t(x)

		SELECT @AfterClosingDate = DATEADD(DAY, 1, @ClosingDate)---,
				--@SupplierStart = IIF(@SupplierStart = '', '0', @SupplierStart),
				--@SupplierEnd = IIF(@SupplierEnd = '', 'ZZZZZZZZZZ', @SupplierEnd),
				--@CostCenterStart = IIF(@CostCenterStart = '', '0', @CostCenterStart),
				--@CostCenterEnd = IIF(@CostCenterEnd = '', 'ZZZZZZZZZZ', @CostCenterEnd)

		/********************************** OBTENCION DE DATOS **********************************/

		SELECT 
			--CASE @OrderBy
			--	WHEN 1 THEN d.ThirdPartyNit
			--	WHEN 2 THEN d.ThirdPartyName
			--	WHEN 3 THEN d.DistributionLineCode
			--	WHEN 4 THEN d.DistributionLineName
			--	WHEN 5 THEN CONVERT(VARCHAR(20), d.BillDate, 112)
			--	ELSE RIGHT('00000' + CAST(d.Age AS VARCHAR(5)), 5)
			--END AS OrderBy,
			--d.*,
			CASE d.EntityName
				WHEN 'InitialBalance' THEN CONCAT('Saldo Inicial', ' - ', d.EntityCode)
				WHEN 'EntranceVoucher' THEN CONCAT('Comprobante de Entrada', ' - ', d.EntityCode)
				WHEN 'FixedAssetEntry' THEN CONCAT('Ingreso de Activos', ' - ', d.EntityCode)
				WHEN 'CostDistributionDirectCost' THEN CONCAT('Distribucion Elementos del Costo', ' - ', d.EntityCode)
				ELSE CONCAT('Cuenta por Pagar', ' - ', d.Code)
			END AS OriginCodeName
		FROM
		(
			SELECT 
				ap.Id,
				ap.Code,
				ap.BillNumber,
				ap.BillDate,
				ap.Term,
				DATEADD(DAY, ap.Term, ap.BillDate) AS ExpiredDate,
				DATEDIFF(DAY, ap.ServicePeriodDate, @ClosingDate) AS Age,
				ap.NumberFiling AS RadicatedConsecutive,
				ap.ServicePeriodDate AS RadicatedDate,
				ap.CreationUser AS RadicatedUser,
				ap.Shares,
				ap.InitialBalance,
				ap.EntityName,
				ap.EntityCode,
				tp.Nit AS ThirdPartyNit,
				tp.Name AS ThirdPartyName, 
				dl.Code AS DistributionLineCode,
				dl.Name AS DistributionLineName,
				ma.Number AS MainAccountNumber, 
				ma.Name AS MainAccountName,
				cc.Code AS CostCenterCode, 
				cc.Name AS CostCenterName,
				ap.Value AS DocumentValue,
				(ap.Value - ISNULL(ibap.Balance, ap.Value)) InitialValue,
				ISNULL(pn.DebitValue, 0) AS DebitValue,
				ISNULL(pn.CreditValue, 0) AS CreditValue,
				ISNULL(pt.TransferValue, 0) AS TransferValue,
				ISNULL(vt.VoucherTransactionValue, 0) AS VoucherTransactionValue,
				ISNULL(cr.CashReceiptValue, 0) AS CashReceiptValue,
				ISNULL(ca.CrossingValue, 0) AS CrossingValue,
				(
					ap.Value --Valor Inicial
					- (ap.Value - ISNULL(ibap.Balance, ap.Value)) --Valor saldo inicial
					- ISNULL(pn.DebitValue, 0) + ISNULL(pn.CreditValue, 0) --Valor Notas
					- ISNULL(pt.TransferValue, 0) --Valor Cruce de Anticipo
					- ISNULL(vt.VoucherTransactionValue, 0) --Valor comprobante de egreso
					- ISNULL(cr.CashReceiptValue, 0) --Valor recibos de caja
					- ISNULL(ca.CrossingValue, 0) --Valor cruzado con una cxp
				) AS Balance
			FROM Payments.AccountPayable ap WITH (NOLOCK)
			JOIN Common.Supplier s WITH (NOLOCK) ON ap.IdSupplier = s.Id
			JOIN Common.ThirdParty tp WITH (NOLOCK) ON s.IdThirdParty = tp.Id
			JOIN Common.SuppliersDistributionLines sdl WITH (NOLOCK) ON ap.IdSuppliersDistributionLines = sdl.Id
			JOIN Common.DistributionLines dl WITH (NOLOCK) ON sdl.IdDistributionLine = dl.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ap.IdAccount = ma.Id
			LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON ap.IdCostCenter = cc.Id
			LEFT JOIN Payments.InitialBalanceAccountPayable ibap WITH (NOLOCK) ON ap.Id = ibap.AccountPayableId
			LEFT JOIN
			(
				SELECT
					pnapa.AccountPayableId,
					SUM(IIF(pn.Nature = 1, pnapa.AdjusmentValue, 0)) DebitValue,
					SUM(IIF(pn.Nature = 1, 0, pnapa.AdjusmentValue)) CreditValue
				FROM Payments.PaymentNotes pn WITH (NOLOCK)
				JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH (NOLOCK) ON pn.Id = pnapa.PaymentNoteId
				WHERE pn.Status = 2
					AND CAST(pn.NoteDate AS DATE) <= @ClosingDate
				GROUP BY pnapa.AccountPayableId
			) pn ON ap.Id = pn.AccountPayableId
			LEFT JOIN
			(
				SELECT
					ptd.AccountPayableId,
					SUM(ptd.Value) TransferValue
				FROM Payments.PaymentTransfer pt WITH (NOLOCK)
				JOIN Payments.PaymentTransferDetail ptd WITH (NOLOCK) ON pt.Id = ptd.PaymentTransferId
				WHERE pt.Status = 2
					AND CAST(pt.DocumentDate AS DATE) <= @ClosingDate
				GROUP BY ptd.AccountPayableId
			) pt ON ap.Id = pt.AccountPayableId
			LEFT JOIN
			(
				SELECT 
					db.IdAccountPayable,
					SUM(db.AdvancedValue) VoucherTransactionValue
				FROM Treasury.VoucherTransaction vt WITH (NOLOCK)
				JOIN Treasury.VoucherTransactionDetails vtd WITH (NOLOCK) ON vt.Id = vtd.IdVoucherTransaction
				JOIN Treasury.DischargeBill db WITH (NOLOCK) ON vtd.Id = db.IdVoucherTransactionD
				WHERE vt.Status IN (2 , 4)
					AND CAST(vt.DocumentDate AS DATE) <= @ClosingDate
					AND CAST(ISNULL(vt.ReversedDate, @AfterClosingDate) AS DATE) > @ClosingDate
				GROUP BY db.IdAccountPayable
			) vt ON ap.Id = vt.IdAccountPayable
			LEFT JOIN
			(
				SELECT 
					crdap.AccountPayableId,
					SUM(crdap.RefundValue) CashReceiptValue
				FROM Treasury.CashReceipts cr WITH (NOLOCK)
				JOIN Treasury.CashReceiptDetails crd WITH (NOLOCK) ON cr.Id = crd.IdCashReceipt
				JOIN Treasury.CashReceiptDetailAccountPayable crdap WITH (NOLOCK) ON crd.Id = crdap.CashReceiptDetailId
				WHERE cr.Status IN (2 , 4)
					AND CAST(cr.DocumentDate AS DATE) <= @ClosingDate
					AND CAST(ISNULL(cr.ReversedDate, @AfterClosingDate) AS DATE) > @ClosingDate
				GROUP BY crdap.AccountPayableId
			) cr ON ap.Id = cr.AccountPayableId
			LEFT JOIN
			(
				SELECT 
					cad.AccountPayableId,
					SUM(cad.CrossingValue) CrossingValue
				FROM Treasury.CrossingAccount ca WITH (NOLOCK)
				JOIN Treasury.CrossingAccountDetailCxP cad WITH (NOLOCK) ON ca.Id = cad.CrossingAccountId
				WHERE CAST(ca.DocumentDate AS DATE) <= @ClosingDate
					AND ca.Status = 2
				GROUP BY cad.AccountPayableId
			) ca ON ap.Id = ca.AccountPayableId
			WHERE ap.Status = 2
				--AND ISNULL(ap.IdOperatingUnit, @OperatingUnit) = @OperatingUnit
				--por fecha de documento--AND CAST(ap.ServicePeriodDate AS DATE) <= @ClosingDate  --por fecha de Radicado--
				AND CAST(ap.BillDate AS DATE) <= @ClosingDate
				--AND tp.Nit BETWEEN @SupplierStart AND @SupplierEnd
				--AND ISNULL(cc.Code, '0') BETWEEN @CostCenterStart AND @CostCenterEnd
		) AS d
		WHERE (--@IncludeZero = 1 OR 
		d.Balance <> 0)
		ORDER BY 1
		OPTION (RECOMPILE)

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de antigüedad (edades) de cuentas por pagar a proveedores para el módulo oncológico. Dado una fecha de corte, calcula el saldo vigente de cada factura o documento pendiente de pago, considerando el valor original, saldo inicial, notas débito y crédito, cruces de anticipos, comprobantes de egreso, recibos de caja y cruces entre cuentas por pagar. Consolida información del proveedor (NIT, nombre), línea de distribución contable, cuenta contable principal y centro de costo, permitiendo analizar cuántos días lleva pendiente cada obligación y cuánto se debe a cada tercero a una fecha determinada. Es utilizado para la gestión financiera de cartera por pagar, conciliación con proveedores y control presupuestal por antigüedad de deuda.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_Onco_ReportPaymentsByAge';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_Onco_ReportPaymentsByAge';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de antigüedad de saldos de cuentas por pagar a una fecha de corte, calculando el saldo pendiente por documento tras aplicar notas, traslados, egresos, recibos de caja y cruces.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_Onco_ReportPaymentsByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de filtros debe contener el nodo /Data/ClosingDate con la fecha de corte del reporte.; Las cuentas por pagar deben existir con Status=2 (aprobadas/activas) para ser consideradas.; Cada cuenta por pagar debe tener proveedor, tercero, línea de distribución y cuenta contable principal asociados (joins obligatorios).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_Onco_ReportPaymentsByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran cuentas por pagar con BillDate menor o igual a la fecha de corte.; Solo se incluyen notas de pago con Status=2 y NoteDate <= fecha de corte.; Solo se incluyen traslados de pago (PaymentTransfer) con Status=2 y DocumentDate <= fecha de corte.; Comprobantes de egreso (VoucherTransaction) y recibos de caja (CashReceipts) se consideran solo con Status IN (2,4), DocumentDate <= fecha de corte y que no hayan sido reversados antes o en la fecha de corte (ReversedDate > ClosingDate).; Los cruces de cuentas (CrossingAccount) solo se consideran con Status=2 y DocumentDate <= fecha de corte.; El Balance se calcula como: Valor del documento − Valor saldo inicial − DebitValue + CreditValue − TransferValue − VoucherTransactionValue − CashReceiptValue − CrossingValue.; La antigüedad (Age) se calcula en días entre ServicePeriodDate (fecha de radicado) y la fecha de corte.; La fecha de vencimiento se calcula sumando el Term en días a la BillDate.; El reporte usa NOLOCK en todas las consultas (lectura sucia permitida).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_Onco_ReportPaymentsByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuentas por pagar; Antigüedad de saldos (aging); Saldo inicial; Notas débito y crédito; Comprobante de egreso; Recibo de caja; Cruce de anticipos; Cruce de cuentas por pagar; Proveedor / Tercero; Línea de distribución contable; Centro de costo; Cuenta contable principal (PUC); Fecha de radicación; Fecha de vencimiento; Reversión de documentos de tesorería', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_Onco_ReportPaymentsByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve el detalle de cuentas por pagar con su antigüedad y saldo, excluyendo aquellas cuyo Balance calculado sea 0.; [RETURN_RESULT] resultset: En caso de error, retorna una fila con Code=''999'', el mensaje y la línea del error en lugar del reporte.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_Onco_ReportPaymentsByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EntityName de la cuenta por pagar es ''InitialBalance'', ''EntranceVoucher'', ''FixedAssetEntry'' o ''CostDistributionDirectCost'' → Etiqueta el origen como ''Saldo Inicial'', ''Comprobante de Entrada'', ''Ingreso de Activos'' o ''Distribucion Elementos del Costo'' concatenado con EntityCode else Etiqueta el origen como ''Cuenta por Pagar'' concatenado con el Code del documento; si PaymentNotes.Nature = 1 → El valor del ajuste se suma como DebitValue else El valor del ajuste se suma como CreditValue; si Balance del documento <> 0 → Se incluye la fila en el reporte else Se excluye del resultado final', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_Onco_ReportPaymentsByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayable; Common.Supplier; Common.ThirdParty; Common.SuppliersDistributionLines; Common.DistributionLines; GeneralLedger.MainAccounts; Payroll.CostCenter; Payments.InitialBalanceAccountPayable; Payments.PaymentNotes; Payments.PaymentNotesAccountPayableAdvance; Payments.PaymentTransfer; Payments.PaymentTransferDetail; Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; Treasury.DischargeBill; Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.CashReceiptDetailAccountPayable; Treasury.CrossingAccount; Treasury.CrossingAccountDetailCxP', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_Onco_ReportPaymentsByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_Onco_ReportPaymentsByAge';
-- GO
