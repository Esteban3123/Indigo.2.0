-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	SP que genera la informacion para el XML FormatoArchiveFT004
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportArchiveFT004]
	@mesFinal INT,
    @Year INT,
    @bookId AS INT	
AS
BEGIN
	SET NOCOUNT ON
	SET DATEFORMAT DMY

	DECLARE @ClosingDate DATE = '01/' + RIGHT('0' + CAST(@mesFinal AS VARCHAR(20)), 2)+ '/' + CAST(@Year AS VARCHAR(20)),
			@AfterClosingDate DATE

	SET @ClosingDate = DATEADD(DAY, -1, DATEADD(MONTH, 1, @ClosingDate))
	SET @AfterClosingDate = DATEADD(DAY, 1, @ClosingDate)
	
	SELECT 
		TipoIdAcreedor, IdAcreedor, DvAcreedor, NombreAcreedor, ActividadAcreedor, ConceptoAcreencia, medicionPosterior, 
		SUM(CxPNoVencidas) AS CxPNoVencidas, 
		SUM(CxPMora30dias) AS CxPMora30dias, 
		SUM(CxPMora60dias) AS CxPMora60dias, 
		SUM(CxPMora90dias) AS CxPMora90dias, 
		SUM(CxPMora180dias) AS CxPMora180dias, 
		SUM(CxPMora360dias) AS CxPMora360dias, 
		SUM(CxPMoraMayor360dias) AS CxPMoraMayor360dias, 
		0 AS ajuste, 
		SUM(CxPNoVencidas) + SUM(CxPMora30dias) + SUM(CxPMora60dias) + SUM(CxPMora90dias) + SUM(CxPMora180dias) + SUM(CxPMora360dias) + SUM(CxPMoraMayor360dias) AS saldo
	FROM 
	(
		SELECT
			CASE cp.IdentificationType WHEN 0 THEN 'CC' WHEN 1 THEN 'CE' WHEN 7 THEN 'NI' ELSE 'OT' END AS TipoIdAcreedor,
			ct.Nit AS IdAcreedor,
			ct.DigitVerification AS DvAcreedor,
			ct.[Name] AS NombreAcreedor,
			ISNULL(ct.CodeCIIU,'NA') AS ActividadAcreedor,
			dl.AccusationConcept AS ConceptoAcreencia,
			dl.FinancialInstrument AS medicionPosterior,
			IIF(ap.Age <= 0, ap.Balance, 0) AS CxPNoVencidas,
			IIF(ap.Age > 0 AND ap.Age <= 30, ap.Balance, 0) as CxPMora30dias,
			IIF(ap.Age > 30 AND ap.Age <= 60, ap.Balance, 0) as CxPMora60dias,
			IIF(ap.Age > 60 AND ap.Age <= 90, ap.Balance, 0) as CxPMora90dias,
			IIF(ap.Age > 90 AND ap.Age <= 180, ap.Balance, 0) as CxPMora180dias,
			IIF(ap.Age > 180 AND ap.Age <= 360, ap.Balance, 0) as CxPMora360dias,
			IIF(ap.Age > 360, ap.Balance, 0) as CxPMoraMayor360dias
		FROM 
		(
			SELECT 
				ap.Id,
				ap.IdThirdParty,
				ap.IdSuppliersDistributionLines,
				DATEDIFF(DAY, ap.BillDate, @ClosingDate) AS Age,
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
				AND CAST(ap.BillDate AS DATE) <= @ClosingDate
		) AS ap
		INNER JOIN Common.ThirdParty AS ct ON ct.Id = ap.IdThirdParty
		INNER JOIN Common.Person AS cp ON cp.id = ct.PersonId
		LEFT JOIN Common.SuppliersDistributionLines AS sdl ON sdl.id = ap.IdSuppliersDistributionLines
		LEFT JOIN Common.DistributionLines AS dl ON dl.id = sdl.IdDistributionLine
		WHERE ap.Balance <> 0
	) AS Info
	GROUP BY Info.TipoIdAcreedor, Info.IdAcreedor, Info.DvAcreedor, Info.NombreAcreedor, Info.ActividadAcreedor, Info.ConceptoAcreencia, Info.medicionPosterior

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de cuentas por pagar por antigüedad de saldos requerido para el archivo XML en formato FT004 (formato de reporte financiero regulatorio). Para un mes y año determinados, calcula el saldo vigente de cada factura de proveedor descontando pagos confirmados (comprobantes de egreso, traslados de pago, recibos de caja, cruces de cuentas y notas de pago) y clasifica ese saldo según los días de mora: al día, hasta 30, 60, 90, 180, 360 días y más de 360 días. El resultado se agrupa por acreedor (tipo y número de identificación, nombre, actividad CIIU) y por concepto de acreencia e instrumento financiero, consolidando el saldo total adeudado a cada proveedor o tercero a la fecha de cierre del período indicado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportArchiveFT004';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportArchiveFT004';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera la información para el reporte XML FT004 (cartera por edades de cuentas por pagar) agrupando saldos de proveedores por tramos de mora respecto a la fecha de cierre del periodo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportArchiveFT004';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El mes y año recibidos deben permitir construir una fecha válida (último día del mes de cierre); Deben existir cuentas por pagar en estado 2 (aprobadas/vigentes) con BillDate menor o igual a la fecha de cierre; Cada AccountPayable debe tener un IdThirdParty existente en Common.ThirdParty con PersonId válido en Common.Person', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportArchiveFT004';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha de cierre se calcula como el último día del mes indicado por (mes,año); Solo se incluyen cuentas por pagar con Status=2 y BillDate <= fecha de cierre; Las notas de pago aplicadas requieren Status=2 y NoteDate <= fecha de cierre; suman débito si Nature=1 y crédito en caso contrario; Las transferencias de pago se consideran solo si Status=2 y DocumentDate <= fecha de cierre; Comprobantes de egreso (VoucherTransaction) y recibos de caja se incluyen solo si Status IN (2,4), DocumentDate <= cierre y no están reversados antes o en la fecha de cierre (ReversedDate > cierre); Los cruces entre cuentas (CrossingAccount) requieren Status=2 y DocumentDate <= fecha de cierre; El saldo se calcula como Valor inicial - (Valor - saldo inicial) - notas débito + notas crédito - cruces de anticipo - egresos - recibos de caja - cruces con CxP; Solo se incluyen registros con saldo distinto de cero; El campo ''ajuste'' siempre se reporta en cero; El campo ''saldo'' es la suma aritmética de todos los tramos de edad por acreedor', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportArchiveFT004';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuentas por pagar; Cartera por edades de mora; Acreedor; Tipo de identificación tributaria (CC, CE, NIT); Código CIIU (actividad económica); Concepto de acreencia; Medición posterior (instrumento financiero); Notas débito/crédito a proveedores; Anticipos a proveedores; Comprobante de egreso; Recibo de caja; Cruce de cuentas; Saldo inicial contable; Reporte FT004 (XML normativo); Fecha de cierre contable', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportArchiveFT004';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve un conjunto de resultados agrupado por acreedor, concepto de acreencia y medición posterior, con saldos distribuidos en tramos de edad: no vencidas (Age<=0), 1-30, 31-60, 61-90, 91-180, 181-360 y mayor a 360 días', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportArchiveFT004';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si cp.IdentificationType = 0 → TipoIdAcreedor = ''CC'' (cédula de ciudadanía); si cp.IdentificationType = 1 → TipoIdAcreedor = ''CE'' (cédula de extranjería); si cp.IdentificationType = 7 → TipoIdAcreedor = ''NI'' (NIT); si cp.IdentificationType no está en (0,1,7) → TipoIdAcreedor = ''OT'' (otro); si ap.Age <= 0 → El saldo se clasifica como CxPNoVencidas; si ap.Age entre 1 y 30 días → Se clasifica como CxPMora30dias; si ap.Age entre 31 y 60 días → Se clasifica como CxPMora60dias; si ap.Age entre 61 y 90 días → Se clasifica como CxPMora90dias; si ap.Age entre 91 y 180 días → Se clasifica como CxPMora180dias; si ap.Age entre 181 y 360 días → Se clasifica como CxPMora360dias; si ap.Age > 360 días → Se clasifica como CxPMoraMayor360dias; si ct.CodeCIIU es NULL → ActividadAcreedor toma el valor ''NA''', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportArchiveFT004';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayable; Payments.InitialBalanceAccountPayable; Payments.PaymentNotes; Payments.PaymentNotesAccountPayableAdvance; Payments.PaymentTransfer; Payments.PaymentTransferDetail; Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; Treasury.DischargeBill; Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.CashReceiptDetailAccountPayable; Treasury.CrossingAccount; Treasury.CrossingAccountDetailCxP; Common.ThirdParty; Common.Person; Common.SuppliersDistributionLines; Common.DistributionLines', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportArchiveFT004';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportArchiveFT004';
-- GO
