CREATE VIEW [Portfolio].[ViewReportInvoiceTraceability]
AS
SELECT	CONCAT(ar.Id, '-', vrear.VoucherName, '-', vrear.MovesCode, '-', vrear.AccountNumber) Id,
		ar.OperatingUnitId,
		ar.Id AS AccountReceivableId,
		ar.InvoiceNumber,
		ar.AccountReceivableDate AS InvoiceDate,
		ar.PortfolioStatus,
		CASE ar.PortfolioStatus
			WHEN 1 THEN 'Sin Radicar'
			WHEN 2 THEN 'Radicada'
			WHEN 3 THEN 'Radicada Entidad'
			WHEN 4 THEN 'Objetada'
			WHEN 5 THEN 'Contestada Radicada'
			WHEN 6 THEN 'Aceptada'
			WHEN 7 THEN 'Certificada Parcial'
			WHEN 8 THEN 'Certificada Total'
			WHEN 9 THEN 'No Subsanable'
			WHEN 10 THEN 'Dificil Recaudo'
			WHEN 11 THEN 'Factura Devuelta'
			WHEN 12 THEN 'Glosa Ratificada'
			WHEN 13 THEN 'Radicacion Tramite Objecion'
			WHEN 14 THEN 'Devolucion Factura'
			WHEN 15 THEN 'Cuenta Dificil Recaudo'
			WHEN 16 THEN 'Cobro Juridico'
		END AS PortfolioStatusName,
		c.Id AS CustomerId,
		c.Nit CustomerNit,
		c.Name AS CustomerName,
		CASE c.State
			WHEN 0 THEN 'Inactivo'
			WHEN 1 THEN 'Activo'
		END AS CustomerStatusName,
		c.EPSCode AS CustomerEPSCode,
		gor.RadicatedConsecutive AS ObjectionsReceptionConsecutive,
		gor.RadicatedDate AS ObjectionsReceptionRadicatedDate,
		gor.DocumentDate AS ObjectionsReceptionDocumentDate,
		gor.ConfirmDate AS ObjectionsReceptionConfirmDate,
		gor.Comment AS ObjectionsReceptionComment,
		gor.CreationUser AS ObjectionsReceptionUserCreation,
		CASE gor.State
			WHEN 1 THEN 'Sin Confirmar'
			WHEN 2 THEN 'Radicado Confirmado'
			WHEN 3 THEN 'Oficio con Respuesta'
			WHEN 4 THEN 'Anulada'
		END AS ObjectionsReceptionStatusName,
		gc.ConciliationConsecutive AS ConciliationConsecutive,
		gc.ConciliationDate AS ConciliationDate,
		gc.DocumentDate AS ConciliationDocumentDate,
		gc.ConfirmDate AS ConciliationConfirmDate,
		CASE
			WHEN gc.State = 1 THEN 'Sin Confirmar'
			WHEN gc.State = 2 THEN 'Confirmado'
		END AS ConciliationStatusName,
		ar.Value EntityValue,
		ISNULL(arp.Value, 0) PatientValue,
		ar.Value + ISNULL(arp.Value, 0) InvoiceValue,
		ISNULL(gpg.ValueGlosado, 0) AS ValueGlosado,
		ISNULL(gpg.ValueAcceptedFirstInstance, 0) AS ValueAcceptedFirstInstance,
		ISNULL(gpg.ValueReiterated, 0) AS ValueReiterated,
		ISNULL(gpg.ValueAcceptedSecondInstance, 0) AS ValueAcceptedSecondInstance,
		ISNULL(gpg.ValueAcceptedIPSconciliation, 0) AS ValueAcceptedIPSconciliation,
		ISNULL(gpg.ValueAcceptedEAPBconciliation, 0) AS ValueAcceptedEAPBconciliation,
		ISNULL(gpg.BalanceGlosa, 0) AS BalanceGlosa,
		ISNULL(gpg.ValuePayments, 0) AS ValuePayments,
		ISNULL(gtjcd.LegalTransferValue, 0) AS LegalTransferValue,
		ar.Balance,
		gpg.Id AS PortfolioGlosadaId,
		gpg.RadicatedNumber AS PortfolioGlosadaRadicatedNumber,
		gpg.RadicatedDate AS PortfolioGlosadaRadicateDate,
		gpg.AccountantAccountCustomers AS PortfolioGlosadaAccountCustomer,
		gpg.EvaluationDateGlosa AS PortfolioGlosadaDate,
		vrear.VoucherName,
		vrear.MovesCode,
		ISNULL(vrear.BillValueInitial, 0) BillValueInitial,
		ISNULL(vrear.MovesDebit, 0) MovesDebit,
		ISNULL(vrear.MovesCredit, 0) MovesCredit,
		ISNULL(vrear.BillCurrentBalance, 0) BillCurrentBalance
FROM Portfolio.AccountReceivable AS ar
LEFT JOIN Common.Customer AS c ON ar.CustomerId = c.Id
LEFT JOIN
(
	SELECT ar.InvoiceNumber, SUM(ar.Value) Value
	FROM Portfolio.AccountReceivable ar
	WHERE ar.AccountReceivableType IN (4, 6)
	GROUP BY ar.InvoiceNumber
) arp ON ar.AccountReceivableType = 2 AND ar.InvoiceNumber = arp.InvoiceNumber
LEFT JOIN Glosas.GlosaPortfolioGlosada AS gpg ON ar.InvoiceNumber = gpg.InvoiceNumber
LEFT JOIN 
(
	SELECT
		D.DocumentType
	   ,D.InvoiceNumber
	   ,D.GlosaObjectionsReceptionCId
	   ,D.PortfolioGlosaId
	FROM Glosas.GlosaObjectionsReceptionD AS D
	INNER JOIN 
	(
		SELECT
			MAX(GlosaObjectionsReceptionCId) AS GlosaObjectionsReceptionCId
		   ,PortfolioGlosaId
		   ,InvoiceNumber
		FROM Glosas.GlosaObjectionsReceptionD
		GROUP BY PortfolioGlosaId,InvoiceNumber
	) AS G
		ON G.GlosaObjectionsReceptionCId = D.GlosaObjectionsReceptionCId
		AND D.InvoiceNumber = G.InvoiceNumber
		AND D.PortfolioGlosaId = G.PortfolioGlosaId
) AS gord ON gord.PortfolioGlosaId = gpg.Id
LEFT JOIN Glosas.GlosaObjectionsReceptionC AS gor ON gord.GlosaObjectionsReceptionCId = gor.Id
LEFT OUTER JOIN 
(
	SELECT
		MAX(ConciliationCId) AS ConciliationCId
	   ,GlosaPortfolioId
	   ,InvoiceNumber
	FROM Glosas.ConciliationD
	GROUP BY GlosaPortfolioId,InvoiceNumber
) AS gcd ON gpg.InvoiceNumber = gcd.InvoiceNumber
LEFT JOIN Glosas.ConciliationC AS gc ON gcd.ConciliationCId = gc.Id
LEFT JOIN 
(
	SELECT InvoiceNumber, SUM(tjdcd.LegalTransferValue) LegalTransferValue
	FROM Glosas.TransferJuridicalDebtCollectionC tjdcc
	JOIN Glosas.TransferJuridicalDebtCollectionD tjdcd ON tjdcc.Id = tjdcd.TransferJuridicalDebtCollectionCId
	WHERE tjdcc.State = '2'
	GROUP BY InvoiceNumber
) AS gtjcd ON gtjcd.InvoiceNumber = gpg.InvoiceNumber
LEFT JOIN 
(
	SELECT AccountReceivableId, VoucherName, MovesCode, AccountNumber, MIN(BillValueInitial) BillValueInitial, MAX(BillCurrentBalance) BillCurrentBalance, Status, SUM(MovesDebit) MovesDebit, SUM(MovesCredit) MovesCredit
	FROM Portfolio.VReportExtractAccountReceivable 
	GROUP BY AccountReceivableId, VoucherName, MovesCode, AccountNumber, Status
) AS vrear ON ar.Id = vrear.AccountReceivableId AND vrear.Status IN (2, 4)