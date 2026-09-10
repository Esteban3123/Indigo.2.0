-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-06-18
-- Description:	Procedimiento para el reporte de edades de cuentas por pagar
-- =============================================
CREATE PROCEDURE [Payments].[SP_ReportPaymentsByAge]
	@HisContainer AS VARCHAR(20),
	@xmlCriterias AS XML,
	@xmlFilters AS XML
AS
BEGIN
	SET NOCOUNT ON
	SET DATEFORMAT DMY

	DECLARE -- CRITERIOS --
			@IncludeZero INT,
			@OrderBy INT,
			@IsValorization AS BIT,
			@ValoritationCurrencyId AS INT,
			-- FILTROS --
			@ClosingDate DATE,
			@AfterClosingDate DATE,
			@OperatingUnits VARCHAR(MAX),
			@Suppliers VARCHAR(MAX),
			@CostCenters VARCHAR(MAX),
			---------------------------------------------------------------------------------------
			@FilterByOperatingUnit BIT = 0,
			@FilterBySupplier BIT = 0,
			@FilterByCostCenter BIT = 0,
			---------------------------------------------------------------------------------------
			@OfficialCurrencyId INT

	DECLARE @Table_OperatingUnits AS TABLE(Id INT)
	DECLARE @Table_Suppliers AS TABLE(Id INT)
	DECLARE @Table_CostCenters AS TABLE(Id INT)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT	@IncludeZero = t.x.value('IncludeZero[1]','int'),
				@OrderBy = t.x.value('OrderBy[1]','int'),
				@ValoritationCurrencyId = T.x.value('ValoritationCurrencyId[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		--Se obtienen los datos de los filtros
		SELECT	@ClosingDate = t.x.value('ClosingDate[1]','date'),
				---------------------------------------------------------------------------------------
				@OperatingUnits = t.x.value('OperatingUnits[1]','varchar(max)'),
				@Suppliers = t.x.value('Suppliers[1]','varchar(max)'),
				@CostCenters = t.x.value('CostCenters[1]','varchar(max)')
		FROM @xmlFilters.nodes('/Data') t(x)

		SELECT @AfterClosingDate = DATEADD(DAY, 1, @ClosingDate)
		SET @IsValorization = IIF(@ValoritationCurrencyId IS NOT NULL, 1, 0)
		-------------------------------------------------------------------------------------------------

		IF ISNULL(@OperatingUnits, '') <> ''
		BEGIN
			SET @FilterByOperatingUnit = 1

			INSERT INTO @Table_OperatingUnits
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@OperatingUnits, ',')
		END

		IF ISNULL(@Suppliers, '') <> ''
		BEGIN
			SET @FilterBySupplier = 1

			INSERT INTO @Table_Suppliers
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@Suppliers, ',')
		END

		IF ISNULL(@CostCenters, '') <> ''
		BEGIN
			SET @FilterByCostCenter = 1

			INSERT INTO @Table_CostCenters
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@CostCenters, ',')
		END

		/********************************** OBTENCION DE DATOS **********************************/

		SET @OfficialCurrencyId  = (SELECT TOP 1 cs.OfficialCurrencyId
									FROM GeneralLedger.CompanySettings cs
								   )
									

		SELECT 
			CASE @OrderBy
				WHEN 1 THEN d.ThirdPartyNit
				WHEN 2 THEN d.ThirdPartyName
				WHEN 3 THEN d.DistributionLineCode
				WHEN 4 THEN d.DistributionLineName
				WHEN 5 THEN CONVERT(VARCHAR(20), d.BillDate, 112)
				ELSE RIGHT('00000' + CAST(d.Age AS VARCHAR(5)), 5)
			END AS OrderBy,
			d.*,
			CONCAT('Cuenta por Pagar: ', d.Code, CASE d.EntityName
															WHEN 'InitialBalance' THEN CONCAT(' - Saldo Inicial: ', d.EntityCode)
															WHEN 'EntranceVoucher' THEN CONCAT(' - Comprobante de Entrada: ', d.EntityCode)
															WHEN 'FixedAssetEntry' THEN CONCAT(' - Ingreso de Activos: ', d.EntityCode)
															WHEN 'CostDistributionDirectCost' THEN CONCAT(' - Distribucion Elementos del Costo: ', d.EntityCode)
														END
			) AS OriginCodeName
		FROM
		(
			SELECT	ap.Id,
					ou.UnitName OperatingUnitName,
					ap.Code,
					ap.BillNumber,
					c.Abbreviation CurrencyAbbreviation,
					ap.BillDate,
					ap.Term,
					DATEADD(DAY, ap.Term, ap.BillDate) AS ExpiredDate,
					IIF(DATEDIFF(DAY, DATEADD(DAY, ap.Term, ap.ServicePeriodDate), @ClosingDate)< 0 ,0,DATEDIFF(DAY, DATEADD(DAY, ap.Term, ap.ServicePeriodDate), @ClosingDate)) AS Age,
					ap.NumberFiling AS RadicatedConsecutive,
					ap.ServicePeriodDate AS RadicatedDate,
					ap.CreationUser AS RadicatedUser,
					ap.Shares,
					IIF
					(
						@IsValorization = 0, 
						ap.InitialBalance, 
						Common.CurrencyConverterWithDate(ap.InitialBalance, ISNULL(ap.CurrencyId, @OfficialCurrencyId), @ValoritationCurrencyId, @ClosingDate)
					) AS InitialBalance,
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
					IIF
					(
						@IsValorization = 0,
						ap.InvoiceValue,
						Common.CurrencyConverterWithDate(ap.InvoiceValue, ISNULL(ap.CurrencyId, @OfficialCurrencyId), @ValoritationCurrencyId, @ClosingDate)
					) AS InvoiceValue,
					IIF
					(
						@IsValorization = 0,
						ap.Value,
						Common.CurrencyConverterWithDate(ap.Value, ISNULL(ap.CurrencyId, @OfficialCurrencyId), @ValoritationCurrencyId, @ClosingDate)
					) AS DocumentValue,
					IIF
					(
						@IsValorization = 0,
						(ap.Value - ISNULL(ibap.Balance, ap.Value)),
						Common.CurrencyConverterWithDate((ap.Value - ISNULL(ibap.Balance, ap.Value)), ISNULL(ap.CurrencyId, @OfficialCurrencyId), @ValoritationCurrencyId, @ClosingDate)
					) AS InitialValue,
					IIF
					(
						@IsValorization = 0,
						ISNULL(pn.DebitValue, 0),
						Common.CurrencyConverterWithDate(ISNULL(pn.DebitValue, 0), ISNULL(ap.CurrencyId, @OfficialCurrencyId), @ValoritationCurrencyId, @ClosingDate)
					) AS DebitValue,
					IIF
					(
						@IsValorization = 0,
						ISNULL(pn.CreditValue, 0),
						Common.CurrencyConverterWithDate(ISNULL(pn.CreditValue, 0), ISNULL(ap.CurrencyId, @OfficialCurrencyId), @ValoritationCurrencyId, @ClosingDate)
					) AS CreditValue,
					IIF
					(
						@IsValorization = 0,
						ISNULL(pt.TransferValue, 0),
						Common.CurrencyConverterWithDate(ISNULL(pt.TransferValue, 0), ISNULL(ap.CurrencyId, @OfficialCurrencyId), @ValoritationCurrencyId, @ClosingDate)
					) AS TransferValue,
					IIF
					(
						@IsValorization = 0,
						ISNULL(vt.VoucherTransactionValue, 0),
						Common.CurrencyConverterWithDate(ISNULL(vt.VoucherTransactionValue, 0), ISNULL(ap.CurrencyId, @OfficialCurrencyId), @ValoritationCurrencyId, @ClosingDate)
					) AS VoucherTransactionValue,
					IIF
					(
						@IsValorization = 0,
						ISNULL(cr.CashReceiptValue, 0),
						Common.CurrencyConverterWithDate(ISNULL(cr.CashReceiptValue, 0), ISNULL(ap.CurrencyId, @OfficialCurrencyId), @ValoritationCurrencyId, @ClosingDate)
					) AS CashReceiptValue,
					IIF
					(
						@IsValorization = 0,
						ISNULL(ca.CrossingValue, 0),
						Common.CurrencyConverterWithDate(ISNULL(ca.CrossingValue, 0), ISNULL(ap.CurrencyId, @OfficialCurrencyId), @ValoritationCurrencyId, @ClosingDate)
					) AS CrossingValue,
					----Saldo ---
					IIF(
						@IsValorization = 0, 
						calc.BalanceCalculated,
						Common.CurrencyConverterWithDate(calc.BalanceCalculated,ISNULL(ap.CurrencyId, @OfficialCurrencyId),@ValoritationCurrencyId,@ClosingDate)
					) AS Balance,
					------------
					IIF
					(
						@IsValorization = 0,
						ISNULL(ap.CurrencyId, @OfficialCurrencyId),
						@ValoritationCurrencyId
					) CurrencyId
			FROM Payments.AccountPayable ap WITH (NOLOCK)
			JOIN Common.Supplier s WITH (NOLOCK) ON ap.IdSupplier = s.Id
			JOIN Common.ThirdParty tp WITH (NOLOCK) ON s.IdThirdParty = tp.Id
			JOIN Common.SuppliersDistributionLines sdl WITH (NOLOCK) ON ap.IdSuppliersDistributionLines = sdl.Id
			JOIN Common.DistributionLines dl WITH (NOLOCK) ON sdl.IdDistributionLine = dl.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ap.IdAccount = ma.Id
			LEFT JOIN Common.Currency c ON c.Id = ISNULL(ap.CurrencyId, @OfficialCurrencyId)
			LEFT JOIN Common.OperatingUnit ou WITH (NOLOCK) ON ap.IdOperatingUnit = ou.Id
			LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON ap.IdCostCenter = cc.Id
			LEFT JOIN Payments.InitialBalanceAccountPayable ibap WITH (NOLOCK) ON ap.Id = ibap.AccountPayableId
			LEFT JOIN
			(
				SELECT	pn.AccountPayableId, SUM(DebitValue) DebitValue, SUM(CreditValue) CreditValue
				FROM
				(
						SELECT
							pnapa.AccountPayableId,
							CASE 
								WHEN pn.Nature = 1 THEN ISNULL(pnapa.AdjustmentValueShare, pnapa.AdjusmentValue)
								ELSE 0 
							END DebitValue,
							CASE 
								WHEN pn.Nature = 2 THEN ISNULL(pnapa.AdjustmentValueShare, pnapa.AdjusmentValue)
								ELSE 0 
							END CreditValue
						FROM Payments.PaymentNotes pn WITH (NOLOCK)
						JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH (NOLOCK) ON pn.Id = pnapa.PaymentNoteId
						WHERE pn.Status = 2
							AND CAST(pn.NoteDate AS DATE) <= @ClosingDate
					UNION ALL
						SELECT
							ap.Id AccountPayableId,
							ap.Value DebitValue,
							0 CreditValue
						FROM Payments.PaymentNotes pn WITH (NOLOCK)
						JOIN Payments.AccountPayable ap WITH (NOLOCK) ON pn.IdAccountPayable = ap.Id
						WHERE pn.Status = 2
							AND CAST(pn.NoteDate AS DATE) <= @ClosingDate
				) pn
				GROUP BY pn.AccountPayableId
			) pn ON ap.Id = pn.AccountPayableId
			LEFT JOIN
			(
				SELECT
					ptd.AccountPayableId,
					SUM(IIF(ptd.ValueInCurrencyInvoice = 0, ptd.Value, ptd.ValueInCurrencyInvoice)) TransferValue
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
					SUM( db.AdvancedValue) VoucherTransactionValue
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
					SUM(IIF(crd.ValueInCurrencyHeader = 0, crdap.RefundValue, crd.ValueInCurrencyHeader)) CashReceiptValue
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
			CROSS APPLY (
				SELECT
					BalanceCalculated =
						ROUND(
							ap.Value
							- (ap.Value - ISNULL(ibap.Balance, ap.Value))
							- ISNULL(pn.DebitValue, 0.00) + ISNULL(pn.CreditValue, 0.00)
							- ISNULL(pt.TransferValue, 0.00)
							- ISNULL(vt.VoucherTransactionValue, 0.00)
							- ISNULL(cr.CashReceiptValue, 0.00)
							- ISNULL(ca.CrossingValue, 0.00),
							2
						)
			) AS calc
			/**************************************** FILTROS ****************************************/
			LEFT JOIN @Table_OperatingUnits tou ON ap.IdOperatingUnit = tou.Id
			LEFT JOIN @Table_Suppliers ts ON ap.IdSupplier = ts.Id
			LEFT JOIN @Table_CostCenters tcc ON ap.IdCostCenter = tcc.Id
			WHERE ap.Status = 2
				AND CAST(ap.ServicePeriodDate AS DATE) <= @ClosingDate
				AND (@FilterByOperatingUnit = 0 OR tou.Id IS NOT NULL)
				AND (@FilterBySupplier = 0 OR ts.Id IS NOT NULL)
				AND (@FilterByCostCenter = 0 OR tcc.Id IS NOT NULL)
		) AS d
		WHERE (@IncludeZero = 1 OR d.Balance <> 0)
		ORDER BY 1
	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de antigüedad (edades) de cuentas por pagar a proveedores. Calcula cuántos días de vencimiento tiene cada documento o factura pendiente de pago, agrupando la información por proveedor, línea de distribución contable, centro de costo y unidad operativa, con corte a una fecha de cierre definida. Permite ordenar los resultados por NIT del proveedor, nombre, línea de distribución, fecha de factura o antigüedad, y soporta valorización de los saldos en una moneda alternativa usando tasas de cambio históricas. Se usa para el seguimiento y control de obligaciones vencidas o por vencer en el módulo de pagos y cuentas por pagar.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_ReportPaymentsByAge';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_ReportPaymentsByAge';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de antigüedad (edades) de cuentas por pagar a una fecha de corte, calculando saldos por documento con descuentos de notas, transferencias, comprobantes de egreso, recibos de caja y cruces, con opción de valorización en otra moneda.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPaymentsByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los XML de criterios y filtros deben tener la estructura /Data con los nodos esperados (IncludeZero, OrderBy, ValoritationCurrencyId, ClosingDate, OperatingUnits, Suppliers, CostCenters).; Debe existir al menos un registro en GeneralLedger.CompanySettings para obtener la moneda oficial.; Las listas de OperatingUnits, Suppliers y CostCenters deben venir como cadenas separadas por coma con IDs convertibles a INT.; La fecha de corte (ClosingDate) debe ser válida; se asume formato DMY.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPaymentsByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen cuentas por pagar con Status=2 (aprobadas/contabilizadas) y con ServicePeriodDate <= fecha de corte.; Las notas de pago consideradas son las de Status=2 con NoteDate <= ClosingDate.; Las transferencias consideradas son las de Status=2 con DocumentDate <= ClosingDate.; Comprobantes de egreso y recibos de caja se consideran solo si no están reversados antes o en la fecha de corte (ReversedDate > ClosingDate o nula).; Los cruces de cuentas se consideran con Status=2 y DocumentDate <= ClosingDate.; La edad nunca es negativa (se acota a 0).; El saldo se calcula como Value - (Value - InitialBalance) - DebitNotes + CreditNotes - Transferencias - Egresos - RecibosCaja - Cruces, redondeado a 2 decimales.; Si la cuenta por pagar no tiene CurrencyId se usa la moneda oficial de la empresa.; Si no hay InitialBalance asociado, se asume que el saldo inicial equivale al valor del documento (no aporta movimiento).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPaymentsByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas de cuentas por pagar con Status=2 y ServicePeriodDate <= ClosingDate, incluyendo edad, saldo calculado y datos de tercero/línea/centro de costo.; [RETURN_RESULT] resultset: Cuando @IncludeZero=1 se incluyen filas con Balance=0; en caso contrario solo se devuelven cuentas con Balance<>0.; [RETURN_RESULT] resultset: Cuando @ValoritationCurrencyId no es nulo se aplica Common.CurrencyConverterWithDate a todos los valores monetarios para convertirlos a la moneda de valorización a la ClosingDate.; [RETURN_RESULT] resultset: Si DATEDIFF(DAY, BillDate+Term, ClosingDate) < 0, la edad (Age) se fuerza a 0; de lo contrario es esa diferencia en días.; [RETURN_RESULT] resultset: En caso de error de ejecución (CATCH) se devuelve un resultset con Code=''999'', Message=ERROR_MESSAGE() y Line=ERROR_LINE() en lugar del reporte.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPaymentsByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @OrderBy IN (1..5) o ELSE → Define la columna de ordenamiento: 1=NIT tercero, 2=Nombre tercero, 3=Código línea distribución, 4=Nombre línea distribución, 5=BillDate (yyyymmdd); ELSE ordena por Edad rellenada a 5 dígitos.; si d.EntityName IN (''InitialBalance'',''EntranceVoucher'',''FixedAssetEntry'',''CostDistributionDirectCost'') → Construye OriginCodeName concatenando una etiqueta específica por tipo de origen (''Saldo Inicial'', ''Comprobante de Entrada'', ''Ingreso de Activos'' o ''Distribucion Elementos del Costo'') con el EntityCode.; si @ValoritationCurrencyId IS NOT NULL → @IsValorization=1 y todos los importes se convierten con Common.CurrencyConverterWithDate usando la moneda del documento (o la oficial si es nula) hacia la moneda de valorización en la fecha de corte. else Se reportan los importes en la moneda original del documento.; si ISNULL(@OperatingUnits,'''')<>'''' / ISNULL(@Suppliers,'''')<>'''' / ISNULL(@CostCenters,'''')<>'''' → Activa el flag correspondiente y carga las tablas variables; el WHERE exige que la cuenta por pagar coincida con alguno de los IDs filtrados.; si PaymentNotes.Nature = 1 vs Nature = 2 → Si Nature=1 el ajuste de la nota se trata como DebitValue; si Nature=2 se trata como CreditValue (usando AdjustmentValueShare o AdjusmentValue).; si VoucherTransaction.Status IN (2,4) y CashReceipts.Status IN (2,4) → Solo se consideran comprobantes de egreso y recibos de caja en estado 2 o 4 cuya fecha sea <= ClosingDate y cuya ReversedDate sea posterior a ClosingDate (o nula).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPaymentsByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split; Common.CurrencyConverterWithDate', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPaymentsByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Payments.AccountPayable; Common.Supplier; Common.ThirdParty; Common.SuppliersDistributionLines; Common.DistributionLines; GeneralLedger.MainAccounts; Common.Currency; Common.OperatingUnit; Payroll.CostCenter; Payments.InitialBalanceAccountPayable; Payments.PaymentNotes; Payments.PaymentNotesAccountPayableAdvance; Payments.PaymentTransfer; Payments.PaymentTransferDetail; Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; Treasury.DischargeBill; Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.CashReceiptDetailAccountPayable; Treasury.CrossingAccount; Treasury.CrossingAccountDetailCxP', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPaymentsByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPaymentsByAge';
-- GO
